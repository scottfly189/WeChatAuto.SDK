namespace ui;

using AntdUI;
using wechatbot;

public partial class MainForm : AntdUI.Window
{
    private NotifyIcon? _notifyIcon;
    private bool _allowVisible = false;

    public MainForm()
    {
        this.Visible = false;
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
}