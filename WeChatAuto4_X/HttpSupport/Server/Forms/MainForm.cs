namespace ui;

using AntdUI;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon;
using WeChatAuto.Components;
using WeChatAuto.Exceptions;
using WeChatAuto.Models;
using WeChatAuto.Services;
using wechatbot;

public partial class MainForm : AntdUI.Window
{
    private NotifyIcon? _notifyIcon;
    private bool _allowVisible = false;
    private WeChatAuto.Components.WeChatClientFactory? factory;
    private Dictionary<string, WeChatClient> clientDict = new Dictionary<string, WeChatClient>();

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
        if (clientDict.Count() == 0)
            return;

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
            AntdUI.Notification.error(this, "错误","没有发现微信客户端或者微信客户端未打开", autoClose: 3,align:TAlignFrom.Top);
        }
        catch (WechatClientNotExistException ex)
        {
            AntdUI.Notification.error(this, "错误", "微信客户端不存在", autoClose: 3,align:TAlignFrom.Top);
        }
        catch (WechatNotSupportUITreeException ex)
        {
            AntdUI.Notification.error(this, "错误", "你的微信客户端不支持UI Tree", autoClose: 3,align:TAlignFrom.Top);
        }
        catch (Exception ex)
        {
            AntdUI.Notification.error(this, "错误", "初始化微信客户端失败", autoClose: 3,align:TAlignFrom.Top);
        }
    }
}