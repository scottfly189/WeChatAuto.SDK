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
