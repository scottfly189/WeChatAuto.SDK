namespace Server.Forms
{
    partial class AboutForm
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
            AntdUI.HyperlinkLabel.LinkAppearance linkAppearance1 = new AntdUI.HyperlinkLabel.LinkAppearance();
            AntdUI.HyperlinkLabel.LinkAppearance linkAppearance2 = new AntdUI.HyperlinkLabel.LinkAppearance();
            lblTitle = new AntdUI.Label();
            lblDesc = new AntdUI.Label();
            lblVersion = new AntdUI.Label();
            linkGithub = new AntdUI.HyperlinkLabel();
            btnOk = new AntdUI.Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 42);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(420, 42);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "WeChatAuto.SDK";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDesc
            // 
            lblDesc.Font = new Font("Microsoft YaHei UI", 10F);
            lblDesc.ForeColor = Color.FromArgb(128, 128, 128);
            lblDesc.Location = new Point(0, 90);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(420, 26);
            lblDesc.TabIndex = 1;
            lblDesc.Text = "为你提供微信 Agent 基座";
            lblDesc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            lblVersion.Font = new Font("Microsoft YaHei UI", 9F);
            lblVersion.ForeColor = Color.FromArgb(128, 128, 128);
            lblVersion.Location = new Point(0, 120);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(420, 24);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "版本 1.0.0";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // linkGithub
            // 
            linkGithub.Font = new Font("Microsoft YaHei UI", 10F);
            linkGithub.HoverStyle = linkAppearance1;
            linkGithub.LinkAutoNavigation = true;
            linkGithub.Location = new Point(0, 150);
            linkGithub.Name = "linkGithub";
            linkGithub.NormalStyle = linkAppearance2;
            linkGithub.Size = new Size(420, 86);
            linkGithub.TabIndex = 3;
            linkGithub.Text = "项目主页：<a href=https://github.com/scottfly189/WeChatAuto.SDK'>https://github.com/scottfly189/WeChatAuto.SDK</a>";
            linkGithub.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(160, 242);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 40);
            btnOk.TabIndex = 4;
            btnOk.Text = "确定";
            btnOk.Type = AntdUI.TTypeMini.Primary;
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 300);
            Controls.Add(btnOk);
            Controls.Add(linkGithub);
            Controls.Add(lblVersion);
            Controls.Add(lblDesc);
            Controls.Add(lblTitle);
            Font = new Font("Microsoft YaHei UI", 10F);
            MaximizeBox = false;
            MinimizeBox = false;
            Mode = AntdUI.TAMode.Light;
            Name = "AboutForm";
            Resizable = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "关于SDK";
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Label lblTitle;
        private AntdUI.Label lblDesc;
        private AntdUI.Label lblVersion;
        private AntdUI.HyperlinkLabel linkGithub;
        private AntdUI.Button btnOk;
    }
}
