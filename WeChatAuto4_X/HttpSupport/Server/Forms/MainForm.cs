namespace ui;

using AntdUI;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon;
using WeChatAuto.Components;
using WeChatAuto.Exceptions;
using WeChatAuto.Models;
using WeChatAuto.Services;
using WeChatAuto.Utils;
using wechatbot;

public partial class MainForm : AntdUI.Window
{
    private NotifyIcon? _notifyIcon;
    private bool _allowVisible = false;
    private WeChatAuto.Components.WeChatClientFactory? factory;
    private Dictionary<string, WeChatClient> clientDict = new Dictionary<string, WeChatClient>();
    private readonly Dictionary<string, SideBarForm> _sideBars = new();
    private System.Windows.Forms.Timer? _syncTimer;
    private int _lastFgPid = -1;
    private const int SideBarGap = 8;

    public MainForm()
    {
        this.Visible = false;
        InitializeComponent();
        _InitTrayIcon();
        _InitEvents();
    }

    protected override void SetVisibleCore(bool value)
    {
        base.SetVisibleCore(value);
        base.SetVisibleCore(_allowVisible && value);
    }

    private void _InitTrayIcon()
    {
        notifyIcon1.Icon = new Icon("Assets/wechat.ico");
        notifyIcon1.Text = "WeChatAuto.SDK - 为你提供微信Agent基座";
        notifyIcon1.Visible = true;
        notifyIcon1.ContextMenuStrip = contextMenuStrip1;

        itemExit.Click += (s, e) =>
        {
            ExitApplication();
        };
        itemConsole.Click += (s, e) =>
        {
            ShowMainForm();
        };
        itemAbout.Click += (s, e) =>
        {
            ShowAbout();
        };

        this.FormClosing += (s, e) =>
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        };
    }


    private void ShowMainForm()
    {
        this._allowVisible = true;
        this.Show();
        this.WindowState = FormWindowState.Normal;
        this.Activate();
    }

    private async void ShowAbout()
    {
        AboutForm about = new AboutForm();
        await about.ShowAsync();
    }

    private void ExitApplication()
    {
        _notifyIcon?.Visible = false;
        _notifyIcon?.Dispose();
        Application.Exit();
    }

    private void _InitEvents()
    {
        this.Load += MainForm_Load;
        this.FormClosed += MainForm_FormClosed;
    }

    private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        _syncTimer?.Stop();
        _syncTimer?.Dispose();
        _syncTimer = null;

        foreach (var sideBar in _sideBars.Values)
        {
            sideBar.Dispose();
        }
        _sideBars.Clear();

        if (factory != null)
        {
            factory.Dispose();    //这里最好手动释放一下资源，避免微信客户端进程残留
        }
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        _InitWechatAutoSDK();
        _InitSidebar();
    }

    private void _InitSidebar()
    {
        if (clientDict.Count == 0)
            return;

        foreach (var client in clientDict)
        {
            var sideBarForm = new SideBarForm(client.Value);
            _sideBars[client.Key] = sideBarForm;

            // 捕获启动时尺寸，用于“固定大小”
            if (Win32WindowHelper.TryGetWindowRect(sideBarForm.WeChatHwnd, out _, out _, out int w, out int h))
            {
                sideBarForm.LockedSize = new Size(w, h);
            }

            // 先显示（不激活，仅创建句柄），随后由 SyncSidebars 统一摆位与显隐
            sideBarForm.Show();
        }

        SyncSidebars();

        _syncTimer = new System.Windows.Forms.Timer { Interval = 60 };
        _syncTimer.Tick += (_, _) => SyncSidebars();
        _syncTimer.Start();
    }

    /// <summary>
    /// 每 60ms 同步一次：固定大小 → 位置跟随 → 显隐切换。
    /// </summary>
    private void SyncSidebars()
    {
        int fgPid = Win32WindowHelper.GetForegroundProcessId();
        bool fgChanged = fgPid != _lastFgPid;
        _lastFgPid = fgPid;

        foreach (var sideBar in _sideBars.Values)
        {
            IntPtr hwnd = sideBar.WeChatHwnd;

            if (!Win32WindowHelper.IsValidWindow(hwnd))
            {
                if (sideBar.Visible)
                    sideBar.Hide();
                continue;
            }

            // 1) 固定大小（对所有微信，与焦点无关；最小化/最大化时跳过）
            if (sideBar.LockedSize.Width > 0
                && !WinApi.IsIconic(hwnd)
                && !Win32WindowHelper.IsWindowZoomed(hwnd)
                && Win32WindowHelper.TryGetWindowRect(hwnd, out _, out _, out int w, out int h)
                && (w != sideBar.LockedSize.Width || h != sideBar.LockedSize.Height))
            {
                Win32WindowHelper.ResizeWindow(hwnd, sideBar.LockedSize.Width, sideBar.LockedSize.Height);
            }

            // 2) 位置跟随（物理像素；居中贴在下沿下方；仅在变化时移动）
            if (Win32WindowHelper.TryGetWindowRect(hwnd, out int wx, out int wy, out int ww, out int wh)
                && Win32WindowHelper.TryGetWindowRect(sideBar.Handle, out _, out _, out int sbW, out _))
            {
                int sx = wx + (ww - sbW) / 2;
                int sy = wy + wh + SideBarGap;
                if (sideBar.LastX != sx || sideBar.LastY != sy)
                {
                    Win32WindowHelper.MoveWindow(sideBar.Handle, sx, sy);
                    sideBar.LastX = sx;
                    sideBar.LastY = sy;
                }
            }

            // 3) 显隐（进程 ID 匹配前台）
            bool active = fgPid != 0 && fgPid == sideBar.WeChatProcessId;
            if (active && !sideBar.Visible)
                sideBar.Show();
            else if (!active && sideBar.Visible)
                sideBar.Hide();

            // 4) 前台切换到该微信时，把侧栏 Z 序插到微信窗口正下方（高于其它窗口、低于微信），避免被遮挡
            if (active && fgChanged && sideBar.Visible)
            {
                Win32WindowHelper.SetWindowBelow(sideBar.Handle, hwnd);
            }
        }
    }

    private void _InitWechatAutoSDK()
    {
        try
        {
            var _serviceProvider = WeAutomation.Initialize(options =>
            {
                options.DebugMode = false;
                options.EnableOCR = true;
            });
            factory = _serviceProvider.GetRequiredService<WeChatClientFactory>();
            clientDict = factory.GetWeChatClientList();
        }
        catch (WechatNotOpenedException ex)
        {
            ShowError("没有发现微信客户端或者微信客户端未打开");
        }
        catch (WechatClientNotExistException ex)
        {
            ShowError("微信客户端不存在");
        }
        catch (WechatNotSupportUITreeException ex)
        {
            ShowError("你的微信没有开放UI Tree,请先打开微信的UI Tree!");
        }
        catch (Exception ex)
        {
            ShowError("初始化微信客户端失败");
        }
    }

    private void ShowError(string message)
    {
        // 在 Form.Load 阶段（消息循环尚未启动）直接调用 Notification 会因主窗体被立即隐藏/消息循环未就绪而不显示，
        // 这里用 BeginInvoke 将通知延后到消息循环运行后再弹出，保证稳定显示。
        BeginInvoke(() =>
        {
            AntdUI.Notification.error(this, "错误", message, autoClose: 5, align: TAlignFrom.Top);
        });
    }
}