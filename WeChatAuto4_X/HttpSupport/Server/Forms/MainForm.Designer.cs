namespace ui;

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
        pageHeader1 = new AntdUI.PageHeader();
        notifyIcon1 = new NotifyIcon(components);
        contextMenuStrip1 = new ContextMenuStrip(components);
        itemConsole = new ToolStripMenuItem();
        itemAbout = new ToolStripMenuItem();
        toolStripMenuItem1 = new ToolStripSeparator();
        itemExit = new ToolStripMenuItem();
        contextMenuStrip1.SuspendLayout();
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
        pageHeader1.SubText = "- 为你提供微信Agent基座";
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
        contextMenuStrip1.Size = new Size(215, 143);
        // 
        // itemConsole
        // 
        itemConsole.AutoSize = false;
        itemConsole.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        itemConsole.ForeColor = Color.FromArgb(90, 90, 90);
        itemConsole.Image = (Image)resources.GetObject("itemConsole.Image");
        itemConsole.Name = "itemConsole";
        itemConsole.Size = new Size(210, 35);
        itemConsole.Text = "主控台";
        // 
        // itemAbout
        // 
        itemAbout.AutoSize = false;
        itemAbout.Font = new Font("Microsoft YaHei UI", 10F);
        itemAbout.ForeColor = Color.FromArgb(90, 90, 90);
        itemAbout.Image = (Image)resources.GetObject("itemAbout.Image");
        itemAbout.Name = "itemAbout";
        itemAbout.Size = new Size(210, 35);
        itemAbout.Text = "关于SDK";
        // 
        // toolStripMenuItem1
        // 
        toolStripMenuItem1.Name = "toolStripMenuItem1";
        toolStripMenuItem1.Size = new Size(211, 6);
        // 
        // itemExit
        // 
        itemExit.AutoSize = false;
        itemExit.Font = new Font("Microsoft YaHei UI", 10F);
        itemExit.ForeColor = Color.FromArgb(90, 90, 90);
        itemExit.Image = (Image)resources.GetObject("itemExit.Image");
        itemExit.Name = "itemExit";
        itemExit.Size = new Size(210, 35);
        itemExit.Text = "退出";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(10F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(711, 491);
        Controls.Add(pageHeader1);
        Font = new Font("Microsoft YaHei UI", 10F);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(4);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "WeChatBot";
        contextMenuStrip1.ResumeLayout(false);
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
}
