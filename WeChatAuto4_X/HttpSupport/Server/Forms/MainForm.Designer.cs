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
        pageHeader1 = new AntdUI.PageHeader();
        notifyIcon1 = new NotifyIcon(components);
        contextMenuStrip1 = new ContextMenuStrip(components);
        itemConsole = new ToolStripMenuItem();
        itemAbout = new ToolStripMenuItem();
        toolStripMenuItem1 = new ToolStripSeparator();
        itemExit = new ToolStripMenuItem();
        tabs1 = new AntdUI.Tabs();
        pageConsole = new AntdUI.TabPage();
        pageLog = new AntdUI.TabPage();
        contextMenuStrip1.SuspendLayout();
        tabs1.SuspendLayout();
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
        pageHeader1.SubText = "- 为微信提供Agent基座";
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
        pageConsole.IconSvg = "DashboardOutlined";
        pageConsole.Location = new Point(0, 38);
        pageConsole.Name = "pageConsole";
        pageConsole.Size = new Size(711, 388);
        pageConsole.TabIndex = 0;
        pageConsole.Text = "主控台";
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
}
