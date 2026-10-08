using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeChatAuto.Components;

namespace Server.Forms
{
    public partial class MonitorConfigForm : AntdUI.Window
    {
        private readonly WeChatClient? client;
        public MonitorConfigForm(WeChatClient client)
        {
            this.client = client;
            InitializeComponent();
            InitData();
            InitEvents();
        }

        private void InitData()
        {
            pageHeader1.Text = "微信号 - " + client?.NickName;
        }

        private void InitEvents()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pageOpen_Click(object sender, EventArgs e)
        {

        }
    }
}
