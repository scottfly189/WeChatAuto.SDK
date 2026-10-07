namespace ui;

using AntdUI;
using wechatbot;

public partial class MainForm : AntdUI.Window
{
    private NotifyIcon? _notifyIcon;
    private bool _allowVisible = false;

    public MainForm()
    {
        InitializeComponent();
        _InitTrayIcon();
    }

    protected override void SetVisibleCore(bool value)
    {
        base.SetVisibleCore(value);
        base.SetVisibleCore(_allowVisible && value);
    }

    private void _InitTrayIcon()
    {
        _notifyIcon = new NotifyIcon
        {
            Icon = new Icon("Assets/wechat.ico"),
            Text = "WeChatAuto.SDK - 为你提供微信Agent基座",
            Visible = true
        };

        // 右键弹出 AntdUI 风格快捷菜单（带 SVG 图标）
        _notifyIcon.MouseClick += (s, e) =>
        {
            if (e.Button == MouseButtons.Right)
                ShowContextMenu();
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

    /// <summary>
    ///  弹出 AntdUI ContextMenuStrip 快捷菜单，带内置 SVG 矢量图标。
    /// </summary>
    private void ShowContextMenu()
    {
        var items = new IContextMenuStripItem[]
        {
            new ContextMenuStripItem("主控台")
            {
                IconSvg = "WechatFilled",
            },
            new ContextMenuStripItem("关于")
            {
                IconSvg = "SettingFilled",
            },
            new ContextMenuStripItemDivider(),
            new ContextMenuStripItem("退出")
            {
                IconSvg = "CloseCircleFilled",
            },
        };

        AntdUI.ContextMenuStrip.open(this, item =>
        {
            if (item.Text == "显示主窗口")
                ShowMainForm();
            else if (item.Text == "设置")
                ShowSettings();
            else if (item.Text == "退出")
                ExitApplication();
        },items);
    }

    private void ShowMainForm()
    {
        this._allowVisible = true;
        this.Show();
        this.WindowState = FormWindowState.Normal;
        this.Activate();
    }

    private async void ShowSettings()
    {
        // TODO: 打开设置界面（可替换为独立的设置窗体）
        FlowerSidePane pane = new FlowerSidePane();
        await pane.ShowAsync();
    }

    private void ExitApplication()
    {
        _notifyIcon?.Visible = false;
        _notifyIcon?.Dispose();
        Application.Exit();
    }
}