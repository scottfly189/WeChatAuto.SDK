namespace Server.Forms;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        AntdUI.Tabs.StyleLine styleLine1 = new AntdUI.Tabs.StyleLine();
        AntdUI.HyperlinkLabel.LinkAppearance linkAppearance1 = new AntdUI.HyperlinkLabel.LinkAppearance();
        AntdUI.HyperlinkLabel.LinkAppearance linkAppearance2 = new AntdUI.HyperlinkLabel.LinkAppearance();
        AntdUI.HyperlinkLabel.LinkAppearance linkAppearance3 = new AntdUI.HyperlinkLabel.LinkAppearance();
        AntdUI.HyperlinkLabel.LinkAppearance linkAppearance4 = new AntdUI.HyperlinkLabel.LinkAppearance();
        AntdUI.HyperlinkLabel.LinkAppearance linkAppearance5 = new AntdUI.HyperlinkLabel.LinkAppearance();
        AntdUI.HyperlinkLabel.LinkAppearance linkAppearance6 = new AntdUI.HyperlinkLabel.LinkAppearance();
        pageHeader1 = new AntdUI.PageHeader();
        notifyIcon1 = new NotifyIcon(components);
        contextMenuStrip1 = new ContextMenuStrip(components);
        itemConsole = new ToolStripMenuItem();
        itemAbout = new ToolStripMenuItem();
        toolStripMenuItem1 = new ToolStripSeparator();
        itemExit = new ToolStripMenuItem();
        tabs1 = new AntdUI.Tabs();
        pageConsole = new AntdUI.TabPage();
        panelMain = new AntdUI.Panel();
        panel1 = new AntdUI.Panel();
        panelMCPServer = new AntdUI.StackPanel();
        stackPanel6 = new AntdUI.StackPanel();
        hyperlinkLabel4 = new AntdUI.HyperlinkLabel();
        label6 = new AntdUI.Label();
        stackPanel7 = new AntdUI.StackPanel();
        label9 = new AntdUI.Label();
        divider1 = new AntdUI.Divider();
        panel2 = new AntdUI.Panel();
        panelAutomation = new AntdUI.StackPanel();
        stackPanel3 = new AntdUI.StackPanel();
        hyperlinkLabel2 = new AntdUI.HyperlinkLabel();
        label5 = new AntdUI.Label();
        stackPanel1 = new AntdUI.StackPanel();
        hyperlinkLabel1 = new AntdUI.HyperlinkLabel();
        label2 = new AntdUI.Label();
        stackPanel10 = new AntdUI.StackPanel();
        label14 = new AntdUI.Label();
        label17 = new AntdUI.Label();
        stackPanel2 = new AntdUI.StackPanel();
        label3 = new AntdUI.Label();
        label1 = new AntdUI.Label();
        lblAutomation = new AntdUI.Label();
        pageLog = new AntdUI.TabPage();
        contextMenuStrip1.SuspendLayout();
        tabs1.SuspendLayout();
        pageConsole.SuspendLayout();
        panelMain.SuspendLayout();
        panel1.SuspendLayout();
        panelMCPServer.SuspendLayout();
        stackPanel6.SuspendLayout();
        stackPanel7.SuspendLayout();
        panel2.SuspendLayout();
        panelAutomation.SuspendLayout();
        stackPanel3.SuspendLayout();
        stackPanel1.SuspendLayout();
        stackPanel10.SuspendLayout();
        stackPanel2.SuspendLayout();
        SuspendLayout();
        // 
        // pageHeader1
        // 
        pageHeader1.Dock = DockStyle.Top;
        pageHeader1.Font = new Font("Microsoft YaHei UI", 12F);
        pageHeader1.Icon = (Image)resources.GetObject("pageHeader1.Icon");
        pageHeader1.Location = new Point(0, 0);
        pageHeader1.Margin = new Padding(4);
        pageHeader1.MaximizeBox = false;
        pageHeader1.Name = "pageHeader1";
        pageHeader1.ShowButton = true;
        pageHeader1.ShowIcon = true;
        pageHeader1.Size = new Size(711, 65);
        pageHeader1.SubGap = 10;
        pageHeader1.SubText = "- 安全构建微信 Agent 的开发基座";
        pageHeader1.TabIndex = 0;
        pageHeader1.Text = "WECHATAUTO.SDK";
        // 
        // notifyIcon1
        // 
        notifyIcon1.Text = "notifyIcon1";
        notifyIcon1.Visible = true;
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Font = new Font("Microsoft YaHei UI", 10F);
        contextMenuStrip1.ImageScalingSize = new Size(20, 20);
        contextMenuStrip1.Items.AddRange(new ToolStripItem[] { itemConsole, itemAbout, toolStripMenuItem1, itemExit });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new Size(153, 115);
        // 
        // itemConsole
        // 
        itemConsole.AutoSize = false;
        itemConsole.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        itemConsole.ForeColor = Color.FromArgb(60, 60, 60);
        itemConsole.Image = (Image)resources.GetObject("itemConsole.Image");
        itemConsole.Name = "itemConsole";
        itemConsole.Size = new Size(210, 35);
        itemConsole.Text = "主控台";
        // 
        // itemAbout
        // 
        itemAbout.AutoSize = false;
        itemAbout.Font = new Font("Microsoft YaHei UI", 10F);
        itemAbout.ForeColor = Color.FromArgb(60, 60, 60);
        itemAbout.Image = (Image)resources.GetObject("itemAbout.Image");
        itemAbout.Name = "itemAbout";
        itemAbout.Size = new Size(210, 35);
        itemAbout.Text = "关于SDK";
        // 
        // toolStripMenuItem1
        // 
        toolStripMenuItem1.Name = "toolStripMenuItem1";
        toolStripMenuItem1.Size = new Size(149, 6);
        // 
        // itemExit
        // 
        itemExit.AutoSize = false;
        itemExit.Font = new Font("Microsoft YaHei UI", 10F);
        itemExit.ForeColor = Color.FromArgb(60, 60, 60);
        itemExit.Image = (Image)resources.GetObject("itemExit.Image");
        itemExit.Name = "itemExit";
        itemExit.Size = new Size(210, 35);
        itemExit.Text = "退出";
        // 
        // tabs1
        // 
        tabs1.Controls.Add(pageConsole);
        tabs1.Controls.Add(pageLog);
        tabs1.Dock = DockStyle.Fill;
        tabs1.Location = new Point(0, 65);
        tabs1.Name = "tabs1";
        tabs1.Pages.Add(pageConsole);
        tabs1.Pages.Add(pageLog);
        tabs1.Size = new Size(711, 426);
        tabs1.Style = styleLine1;
        tabs1.TabIndex = 1;
        tabs1.Text = "tabs1";
        // 
        // pageConsole
        // 
        pageConsole.Controls.Add(panelMain);
        pageConsole.IconSvg = "DashboardOutlined";
        pageConsole.Location = new Point(0, 38);
        pageConsole.Name = "pageConsole";
        pageConsole.Size = new Size(711, 388);
        pageConsole.TabIndex = 0;
        pageConsole.Text = "主控台";
        // 
        // panelMain
        // 
        panelMain.AutoScroll = true;
        panelMain.Controls.Add(panel1);
        panelMain.Controls.Add(divider1);
        panelMain.Controls.Add(panel2);
        panelMain.Dock = DockStyle.Fill;
        panelMain.Location = new Point(0, 0);
        panelMain.Name = "panelMain";
        panelMain.Size = new Size(711, 388);
        panelMain.TabIndex = 0;
        panelMain.Text = "panel1";
        // 
        // panel1
        // 
        panel1.Controls.Add(panelMCPServer);
        panel1.Dock = DockStyle.Top;
        panel1.Location = new Point(0, 167);
        panel1.Name = "panel1";
        panel1.Size = new Size(711, 67);
        panel1.TabIndex = 2;
        panel1.Text = "panel1";
        // 
        // panelMCPServer
        // 
        panelMCPServer.Controls.Add(stackPanel6);
        panelMCPServer.Controls.Add(stackPanel7);
        panelMCPServer.Dock = DockStyle.Fill;
        panelMCPServer.Location = new Point(0, 0);
        panelMCPServer.Name = "panelMCPServer";
        panelMCPServer.Size = new Size(711, 67);
        panelMCPServer.TabIndex = 0;
        panelMCPServer.Text = "stackPanel1";
        panelMCPServer.Vertical = true;
        // 
        // stackPanel6
        // 
        stackPanel6.Controls.Add(hyperlinkLabel4);
        stackPanel6.Controls.Add(label6);
        stackPanel6.Location = new Point(3, 38);
        stackPanel6.Name = "stackPanel6";
        stackPanel6.Size = new Size(705, 29);
        stackPanel6.TabIndex = 2;
        stackPanel6.Text = "stackPanel6";
        // 
        // hyperlinkLabel4
        // 
        hyperlinkLabel4.HoverStyle = linkAppearance1;
        hyperlinkLabel4.Location = new Point(189, 3);
        hyperlinkLabel4.Name = "hyperlinkLabel4";
        hyperlinkLabel4.NormalStyle = linkAppearance2;
        hyperlinkLabel4.Size = new Size(516, 23);
        hyperlinkLabel4.TabIndex = 2;
        hyperlinkLabel4.Text = "<a href='http://localhost:5000/'>http://localhost:5000/</a>";
        // 
        // label6
        // 
        label6.Location = new Point(3, 3);
        label6.Name = "label6";
        label6.Padding = new Padding(25, 0, 0, 0);
        label6.PrefixColor = Color.Red;
        label6.Size = new Size(180, 23);
        label6.TabIndex = 1;
        label6.Text = "客户端设置教程:";
        // 
        // stackPanel7
        // 
        stackPanel7.Controls.Add(label9);
        stackPanel7.Location = new Point(3, 3);
        stackPanel7.Name = "stackPanel7";
        stackPanel7.Size = new Size(705, 29);
        stackPanel7.TabIndex = 1;
        stackPanel7.Text = "stackPanel7";
        // 
        // label9
        // 
        label9.AutoSizeMode = AntdUI.TAutoSize.Width;
        label9.Location = new Point(3, 3);
        label9.Name = "label9";
        label9.Padding = new Padding(10, 0, 0, 0);
        label9.PrefixColor = Color.Red;
        label9.PrefixSvg = "SafetyCertificateFilled";
        label9.Size = new Size(287, 23);
        label9.TabIndex = 1;
        label9.Text = " 微信MCP Server服务已经开启";
        // 
        // divider1
        // 
        divider1.Dock = DockStyle.Top;
        divider1.Location = new Point(0, 152);
        divider1.Name = "divider1";
        divider1.Size = new Size(711, 15);
        divider1.TabIndex = 1;
        divider1.Text = "";
        // 
        // panel2
        // 
        panel2.Controls.Add(panelAutomation);
        panel2.Dock = DockStyle.Top;
        panel2.Location = new Point(0, 0);
        panel2.Name = "panel2";
        panel2.Size = new Size(711, 152);
        panel2.TabIndex = 0;
        panel2.Text = "panel2";
        // 
        // panelAutomation
        // 
        panelAutomation.Controls.Add(stackPanel3);
        panelAutomation.Controls.Add(stackPanel1);
        panelAutomation.Controls.Add(stackPanel10);
        panelAutomation.Controls.Add(stackPanel2);
        panelAutomation.Dock = DockStyle.Fill;
        panelAutomation.Location = new Point(0, 0);
        panelAutomation.Name = "panelAutomation";
        panelAutomation.Padding = new Padding(0, 10, 0, 0);
        panelAutomation.Size = new Size(711, 152);
        panelAutomation.TabIndex = 0;
        panelAutomation.Text = "stackPanel1";
        panelAutomation.Vertical = true;
        // 
        // stackPanel3
        // 
        stackPanel3.Controls.Add(hyperlinkLabel2);
        stackPanel3.Controls.Add(label5);
        stackPanel3.Location = new Point(3, 118);
        stackPanel3.Name = "stackPanel3";
        stackPanel3.Size = new Size(705, 29);
        stackPanel3.TabIndex = 4;
        stackPanel3.Text = "stackPanel3";
        // 
        // hyperlinkLabel2
        // 
        hyperlinkLabel2.HoverStyle = linkAppearance3;
        hyperlinkLabel2.Location = new Point(189, 3);
        hyperlinkLabel2.Name = "hyperlinkLabel2";
        hyperlinkLabel2.NormalStyle = linkAppearance4;
        hyperlinkLabel2.Size = new Size(516, 23);
        hyperlinkLabel2.TabIndex = 2;
        hyperlinkLabel2.Text = "<a href='http://localhost:5000/'>http://localhost:5000/</a>";
        // 
        // label5
        // 
        label5.Location = new Point(3, 3);
        label5.Name = "label5";
        label5.Padding = new Padding(25, 0, 0, 0);
        label5.PrefixColor = Color.Red;
        label5.Size = new Size(180, 23);
        label5.TabIndex = 1;
        label5.Text = "调用API入门教程:";
        // 
        // stackPanel1
        // 
        stackPanel1.Controls.Add(hyperlinkLabel1);
        stackPanel1.Controls.Add(label2);
        stackPanel1.Location = new Point(3, 83);
        stackPanel1.Name = "stackPanel1";
        stackPanel1.Size = new Size(705, 29);
        stackPanel1.TabIndex = 3;
        stackPanel1.Text = "stackPanel1";
        // 
        // hyperlinkLabel1
        // 
        hyperlinkLabel1.HoverStyle = linkAppearance5;
        hyperlinkLabel1.Location = new Point(189, 3);
        hyperlinkLabel1.Name = "hyperlinkLabel1";
        hyperlinkLabel1.NormalStyle = linkAppearance6;
        hyperlinkLabel1.Size = new Size(516, 23);
        hyperlinkLabel1.TabIndex = 2;
        hyperlinkLabel1.Text = "<a href='http://localhost:5000/'>http://localhost:5000/</a>";
        // 
        // label2
        // 
        label2.Location = new Point(3, 3);
        label2.Name = "label2";
        label2.Padding = new Padding(25, 0, 0, 0);
        label2.PrefixColor = Color.Red;
        label2.Size = new Size(180, 23);
        label2.TabIndex = 1;
        label2.Text = "HTTP Server:";
        // 
        // stackPanel10
        // 
        stackPanel10.Controls.Add(label14);
        stackPanel10.Controls.Add(label17);
        stackPanel10.Location = new Point(3, 48);
        stackPanel10.Name = "stackPanel10";
        stackPanel10.Size = new Size(705, 29);
        stackPanel10.TabIndex = 2;
        stackPanel10.Text = "stackPanel10";
        // 
        // label14
        // 
        label14.Location = new Point(189, 3);
        label14.Name = "label14";
        label14.Size = new Size(507, 23);
        label14.TabIndex = 2;
        label14.Text = "";
        // 
        // label17
        // 
        label17.Location = new Point(3, 3);
        label17.Name = "label17";
        label17.Padding = new Padding(25, 0, 0, 0);
        label17.PrefixColor = Color.Red;
        label17.Size = new Size(180, 23);
        label17.TabIndex = 1;
        label17.Text = "监测到微信列表:";
        // 
        // stackPanel2
        // 
        stackPanel2.Controls.Add(label3);
        stackPanel2.Controls.Add(label1);
        stackPanel2.Controls.Add(lblAutomation);
        stackPanel2.Location = new Point(3, 13);
        stackPanel2.Name = "stackPanel2";
        stackPanel2.Size = new Size(705, 29);
        stackPanel2.TabIndex = 1;
        stackPanel2.Text = "stackPanel2";
        // 
        // label3
        // 
        label3.Font = new Font("Microsoft YaHei UI", 9F);
        label3.ForeColor = Color.Gray;
        label3.Location = new Point(277, 3);
        label3.Name = "label3";
        label3.Size = new Size(439, 23);
        label3.TabIndex = 3;
        label3.Text = "感知微信消息，以及UI Automation操作微信";
        // 
        // label1
        // 
        label1.Location = new Point(249, 3);
        label1.Name = "label1";
        label1.Size = new Size(22, 23);
        label1.TabIndex = 2;
        label1.Text = "-";
        label1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblAutomation
        // 
        lblAutomation.AutoSizeMode = AntdUI.TAutoSize.Width;
        lblAutomation.Location = new Point(3, 3);
        lblAutomation.Name = "lblAutomation";
        lblAutomation.Padding = new Padding(10, 0, 0, 0);
        lblAutomation.PrefixColor = Color.Red;
        lblAutomation.PrefixSvg = "SafetyCertificateFilled";
        lblAutomation.Size = new Size(240, 23);
        lblAutomation.TabIndex = 1;
        lblAutomation.Text = " 微信自动化能力已经开启";
        // 
        // pageLog
        // 
        pageLog.IconSvg = "LoginOutlined";
        pageLog.Location = new Point(0, 0);
        pageLog.Name = "pageLog";
        pageLog.Size = new Size(0, 0);
        pageLog.TabIndex = 1;
        pageLog.Text = "日志";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(10F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(711, 491);
        Controls.Add(tabs1);
        Controls.Add(pageHeader1);
        Font = new Font("Microsoft YaHei UI", 10F);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(4);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "WeChatAuto.SDK";
        contextMenuStrip1.ResumeLayout(false);
        tabs1.ResumeLayout(false);
        pageConsole.ResumeLayout(false);
        panelMain.ResumeLayout(false);
        panel1.ResumeLayout(false);
        panelMCPServer.ResumeLayout(false);
        stackPanel6.ResumeLayout(false);
        stackPanel7.ResumeLayout(false);
        stackPanel7.PerformLayout();
        panel2.ResumeLayout(false);
        panelAutomation.ResumeLayout(false);
        stackPanel3.ResumeLayout(false);
        stackPanel1.ResumeLayout(false);
        stackPanel10.ResumeLayout(false);
        stackPanel2.ResumeLayout(false);
        stackPanel2.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private AntdUI.PageHeader pageHeader1;
    private NotifyIcon notifyIcon1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem itemConsole;
    private ToolStripMenuItem itemAbout;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem itemExit;
    private AntdUI.Tabs tabs1;
    private AntdUI.TabPage pageConsole;
    private AntdUI.TabPage pageLog;
    private AntdUI.Panel panel1;
    private AntdUI.Divider divider1;
    private AntdUI.Panel panel2;
    private AntdUI.Label lblAutomation;
    private AntdUI.StackPanel panlAutomation;
    private AntdUI.StackPanel stackPanel2;
    private AntdUI.Label label3;
    private AntdUI.Label label1;
    private AntdUI.StackPanel stackPanel1;
    private AntdUI.Label label5;
    private AntdUI.StackPanel stackPanel3;
    private AntdUI.Label label2;
    private AntdUI.HyperlinkLabel hyperlinkLabel1;
    private AntdUI.HyperlinkLabel hyperlinkLabel2;
    private AntdUI.StackPanel panelMCPServer;
    private AntdUI.StackPanel stackPanel6;
    private AntdUI.HyperlinkLabel hyperlinkLabel4;
    private AntdUI.Label label6;
    private AntdUI.StackPanel stackPanel7;
    private AntdUI.Label label8;
    private AntdUI.Label label9;
    private AntdUI.Divider divider2;
    private AntdUI.StackPanel panelAutomation;
    private AntdUI.Panel panelMain;
    private AntdUI.StackPanel pnlHistoryMessage;
    private AntdUI.StackPanel stackPanel5;
    private AntdUI.Label label4;
    private AntdUI.StackPanel stackPanel8;
    private AntdUI.Button button1;
    private AntdUI.Label label7;
    private AntdUI.Label label10;
    private AntdUI.StackPanel stackPanel9;
    private AntdUI.Button button2;
    private AntdUI.Label label12;
    private AntdUI.Button button3;
    private AntdUI.Divider divider3;
    private AntdUI.StackPanel pnlNewFriends;
    private AntdUI.StackPanel stackPanel12;
    private AntdUI.Button button6;
    private AntdUI.Label label15;
    private AntdUI.Label label16;
    private AntdUI.StackPanel stackPanel4;
    private AntdUI.Label label11;
    private AntdUI.Label label13;
    private AntdUI.Button button4;
    private AntdUI.StackPanel stackPanel10;
    private AntdUI.Label label14;
    private AntdUI.Label label17;
}
