namespace Server.Forms
{
    partial class MonitorConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorConfigForm));
            AntdUI.Tabs.StyleCard2 styleCard21 = new AntdUI.Tabs.StyleCard2();
            AntdUI.Tabs.StyleLine styleLine1 = new AntdUI.Tabs.StyleLine();
            pageHeader1 = new AntdUI.PageHeader();
            tabMonitor = new AntdUI.Tabs();
            pageConfig = new AntdUI.TabPage();
            panel1 = new AntdUI.Panel();
            tabContent = new AntdUI.Tabs();
            pageOpen = new AntdUI.TabPage();
            pageSubwin = new AntdUI.TabPage();
            pageRollup = new AntdUI.TabPage();
            divider1 = new AntdUI.Divider();
            panel2 = new AntdUI.Panel();
            checkbox3 = new AntdUI.Checkbox();
            checkbox2 = new AntdUI.Checkbox();
            checkbox1 = new AntdUI.Checkbox();
            panel3 = new AntdUI.Panel();
            btnMessageMonitorSave = new AntdUI.Button();
            pageFriend = new AntdUI.TabPage();
            tabMonitor.SuspendLayout();
            pageConfig.SuspendLayout();
            panel1.SuspendLayout();
            tabContent.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // pageHeader1
            // 
            pageHeader1.Dock = DockStyle.Top;
            pageHeader1.Font = new Font("Microsoft YaHei UI", 12F);
            pageHeader1.Icon = (Image)resources.GetObject("pageHeader1.Icon");
            pageHeader1.Location = new Point(0, 0);
            pageHeader1.MaximizeBox = false;
            pageHeader1.Name = "pageHeader1";
            pageHeader1.ShowButton = true;
            pageHeader1.ShowIcon = true;
            pageHeader1.Size = new Size(856, 69);
            pageHeader1.TabIndex = 1;
            pageHeader1.Text = "监听设置";
            // 
            // tabMonitor
            // 
            tabMonitor.Controls.Add(pageConfig);
            tabMonitor.Controls.Add(pageFriend);
            tabMonitor.Dock = DockStyle.Fill;
            tabMonitor.Location = new Point(0, 69);
            tabMonitor.Name = "tabMonitor";
            tabMonitor.Pages.Add(pageConfig);
            tabMonitor.Pages.Add(pageFriend);
            tabMonitor.Size = new Size(856, 559);
            styleCard21.Closable = AntdUI.Tabs.StyleCard2.CloseType.none;
            tabMonitor.Style = styleCard21;
            tabMonitor.TabIndex = 2;
            tabMonitor.Text = "tabs1";
            tabMonitor.Type = AntdUI.TabType.Card2;
            // 
            // pageConfig
            // 
            pageConfig.Controls.Add(panel1);
            pageConfig.Controls.Add(panel3);
            pageConfig.IconSvg = "SettingOutlined";
            pageConfig.Location = new Point(0, 38);
            pageConfig.Name = "pageConfig";
            pageConfig.Size = new Size(856, 521);
            pageConfig.TabIndex = 0;
            pageConfig.Text = "消息监听";
            // 
            // panel1
            // 
            panel1.Controls.Add(tabContent);
            panel1.Controls.Add(divider1);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(856, 475);
            panel1.TabIndex = 0;
            panel1.Text = "panel1";
            // 
            // tabContent
            // 
            tabContent.Controls.Add(pageOpen);
            tabContent.Controls.Add(pageSubwin);
            tabContent.Controls.Add(pageRollup);
            tabContent.Dock = DockStyle.Fill;
            tabContent.Location = new Point(0, 76);
            tabContent.Name = "tabContent";
            tabContent.Pages.Add(pageOpen);
            tabContent.Pages.Add(pageSubwin);
            tabContent.Pages.Add(pageRollup);
            tabContent.Size = new Size(856, 399);
            tabContent.Style = styleLine1;
            tabContent.TabIndex = 2;
            tabContent.TabMenuVisible = false;
            // 
            // pageOpen
            // 
            pageOpen.BackColor = Color.White;
            pageOpen.Location = new Point(0, 0);
            pageOpen.Name = "pageOpen";
            pageOpen.Size = new Size(856, 399);
            pageOpen.TabIndex = 0;
            pageOpen.Text = "开放式监听";
            pageOpen.Click += pageOpen_Click;
            // 
            // pageSubwin
            // 
            pageSubwin.Location = new Point(0, 0);
            pageSubwin.Name = "pageSubwin";
            pageSubwin.Size = new Size(0, 0);
            pageSubwin.TabIndex = 1;
            pageSubwin.Text = "弹窗式监听";
            // 
            // pageRollup
            // 
            pageRollup.Location = new Point(0, 0);
            pageRollup.Name = "pageRollup";
            pageRollup.Size = new Size(0, 0);
            pageRollup.TabIndex = 2;
            pageRollup.Text = "轮询监听";
            // 
            // divider1
            // 
            divider1.Dock = DockStyle.Top;
            divider1.Location = new Point(0, 56);
            divider1.Name = "divider1";
            divider1.Size = new Size(856, 20);
            divider1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(checkbox3);
            panel2.Controls.Add(checkbox2);
            panel2.Controls.Add(checkbox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(856, 56);
            panel2.TabIndex = 0;
            panel2.Text = "panel2";
            // 
            // checkbox3
            // 
            checkbox3.Location = new Point(362, 10);
            checkbox3.Name = "checkbox3";
            checkbox3.Size = new Size(174, 36);
            checkbox3.TabIndex = 2;
            checkbox3.Text = "轮询监听";
            // 
            // checkbox2
            // 
            checkbox2.Location = new Point(198, 10);
            checkbox2.Name = "checkbox2";
            checkbox2.Size = new Size(174, 36);
            checkbox2.TabIndex = 1;
            checkbox2.Text = "弹窗式监听";
            // 
            // checkbox1
            // 
            checkbox1.Location = new Point(30, 10);
            checkbox1.Name = "checkbox1";
            checkbox1.Size = new Size(174, 36);
            checkbox1.TabIndex = 0;
            checkbox1.Text = "开放式监听";
            // 
            // panel3
            // 
            panel3.Controls.Add(btnMessageMonitorSave);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 475);
            panel3.Name = "panel3";
            panel3.Size = new Size(856, 46);
            panel3.TabIndex = 1;
            panel3.Text = "panel3";
            // 
            // btnMessageMonitorSave
            // 
            btnMessageMonitorSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMessageMonitorSave.Location = new Point(730, 6);
            btnMessageMonitorSave.Name = "btnMessageMonitorSave";
            btnMessageMonitorSave.Size = new Size(94, 36);
            btnMessageMonitorSave.TabIndex = 0;
            btnMessageMonitorSave.Text = "保存";
            btnMessageMonitorSave.Type = AntdUI.TTypeMini.Success;
            btnMessageMonitorSave.Click += button1_Click;
            // 
            // pageFriend
            // 
            pageFriend.IconSvg = "UsergroupAddOutlined";
            pageFriend.Location = new Point(-1712, -1042);
            pageFriend.Name = "pageFriend";
            pageFriend.Size = new Size(856, 521);
            pageFriend.TabIndex = 1;
            pageFriend.Text = "新好友接入";
            // 
            // MonitorConfigForm
            // 
            AutoScaleDimensions = new SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 628);
            Controls.Add(tabMonitor);
            Controls.Add(pageHeader1);
            Font = new Font("Microsoft YaHei UI", 10F);
            Name = "MonitorConfigForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            tabMonitor.ResumeLayout(false);
            pageConfig.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tabContent.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Tabs tabMonitor;
        private AntdUI.TabPage pageConfig;
        private AntdUI.TabPage pageFriend;
        private AntdUI.Panel panel1;
        private AntdUI.Divider divider1;
        private AntdUI.Panel panel2;
        private AntdUI.Tabs tabContent;
        private AntdUI.TabPage pageOpen;
        private AntdUI.TabPage pageSubwin;
        private AntdUI.TabPage pageRollup;
        private AntdUI.Panel panel3;
        private AntdUI.Button btnSave;
        private AntdUI.Button btnMessageMonitorSave;
        private AntdUI.Checkbox checkbox1;
        private AntdUI.Checkbox checkbox3;
        private AntdUI.Checkbox checkbox2;
    }
}