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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        pageHeader1 = new AntdUI.PageHeader();
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
        pageHeader1.Size = new Size(622, 65);
        pageHeader1.SubGap = 10;
        pageHeader1.SubText = "- 为你提供微信Agent基座";
        pageHeader1.TabIndex = 0;
        pageHeader1.Text = "WECHATBOT";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(10F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(622, 465);
        Controls.Add(pageHeader1);
        Font = new Font("Microsoft YaHei UI", 10F);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(4);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "WeChatBot";
        ResumeLayout(false);
    }

    #endregion

    private AntdUI.PageHeader pageHeader1;
}
