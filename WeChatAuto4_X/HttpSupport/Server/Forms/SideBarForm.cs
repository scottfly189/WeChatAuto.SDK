using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeChatAuto.Components;

namespace Server.Forms
{
    public partial class SideBarForm : AntdUI.Window
    {
        private WeChatClient? client;
        private HistoryMessageForm? historyForm;
        private MonitorConfigForm? monitorConfigForm;
        /// <summary>绑定的微信主窗口 HWND。</summary>
        public IntPtr WeChatHwnd { get; }

        /// <summary>绑定的微信进程 ID。</summary>
        public int WeChatProcessId { get; }

        /// <summary>启动时捕获的微信窗口尺寸，用于“固定大小”。</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size LockedSize { get; set; }

        /// <summary>最近一次摆位的物理像素坐标，用于去抖。</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LastX { get; set; } = int.MinValue;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LastY { get; set; } = int.MinValue;

        public SideBarForm(WeChatClient client)
        {
            this.client = client;
            WeChatHwnd = client.GetHandler();
            WeChatProcessId = client.ClientProcessId;
            InitializeComponent();
            InitEvents();
        }

        private void InitEvents()
        {
            btnHistory.Click += btnHistory_Click;
            btnMonitorConfig.Click += BtnMonitorConfig_Click;
            buttonTools.Click += ButtonTools_Click;
        }

        private void BtnMonitorConfig_Click(object? sender, EventArgs e)
        {
            if (client == null)
                return;

            if (monitorConfigForm == null || monitorConfigForm.IsDisposed)
            {
                monitorConfigForm = new MonitorConfigForm(client);
            }
            monitorConfigForm.Show();
            monitorConfigForm.Activate();
        }

        private void btnHistory_Click(object? sender, EventArgs e)
        {
            if (client == null)
                return;

            if (historyForm == null || historyForm.IsDisposed)
            {
                historyForm = new HistoryMessageForm(client);
            }
            historyForm.Show();
            historyForm.Activate();
        }

        /// <summary>点击“工具集”按钮时弹出 AntdUI 风格的下拉菜单。</summary>
        private void ButtonTools_Click(object? sender, EventArgs e)
        {
            AntdUI.ContextMenuStrip.open(buttonTools, OnToolMenuClick, new AntdUI.IContextMenuStripItem[]
            {
                new AntdUI.ContextMenuStripItem("发送消息"),
                new AntdUI.ContextMenuStripItem("定时发送"),
                new AntdUI.ContextMenuStripItem("消息转发"),
                new AntdUI.ContextMenuStripItem("自动接入好友"),
            });
        }

        /// <summary>菜单项点击回调（占位，后续按 item.Text 接入实际逻辑）。</summary>
        private void OnToolMenuClick(AntdUI.IContextMenuStrip item)
        {
            switch (item.Text)
            {
                case "发送消息":
                    AntdUI.Notification.info(this, "提示", "发送消息功能开发中，敬请期待。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                    break;
                case "定时发送":
                    AntdUI.Notification.info(this, "提示", "定时发送功能开发中，敬请期待。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                    break;
                case "消息转发":
                    AntdUI.Notification.info(this, "提示", "消息转发功能开发中，敬请期待。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                    break;
                case "自动接入好友":
                    AntdUI.Notification.info(this, "提示", "自动接入好友功能开发中，敬请期待。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                    break;
            }
        }

        /// <summary>
        /// 显示但不抢占焦点，避免侧栏出现/被点击时把前台从微信切走。
        /// </summary>
        protected override bool ShowWithoutActivation => true;

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_NOACTIVATE = 0x08000000;
                const int WS_EX_TOOLWINDOW = 0x00000080;

                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW;
                return cp;
            }
        }
    }
}
