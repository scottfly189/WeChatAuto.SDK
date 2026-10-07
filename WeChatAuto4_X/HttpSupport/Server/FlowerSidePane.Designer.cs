namespace wechatbot
{
    partial class FlowerSidePane
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
            stackPanel1 = new AntdUI.StackPanel();
            btnMCP = new AntdUI.Button();
            btnSkill = new AntdUI.Button();
            btnKnowlege = new AntdUI.Button();
            ddConfig = new AntdUI.Dropdown();
            btnStart = new AntdUI.Button();
            divider2 = new AntdUI.Divider();
            divider1 = new AntdUI.Divider();
            stackPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // stackPanel1
            // 
            stackPanel1.Controls.Add(btnMCP);
            stackPanel1.Controls.Add(btnSkill);
            stackPanel1.Controls.Add(btnKnowlege);
            stackPanel1.Controls.Add(ddConfig);
            stackPanel1.Controls.Add(btnStart);
            stackPanel1.Controls.Add(divider2);
            stackPanel1.Controls.Add(divider1);
            stackPanel1.Dock = DockStyle.Fill;
            stackPanel1.Location = new Point(0, 0);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Size = new Size(397, 47);
            stackPanel1.TabIndex = 0;
            stackPanel1.Text = "stackPanel1";
            // 
            // btnMCP
            // 
            btnMCP.AutoSizeMode = AntdUI.TAutoSize.Width;
            btnMCP.Location = new Point(279, 3);
            btnMCP.Name = "btnMCP";
            btnMCP.Size = new Size(72, 41);
            btnMCP.TabIndex = 13;
            btnMCP.Text = "MCP";
            btnMCP.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnSkill
            // 
            btnSkill.AutoSizeMode = AntdUI.TAutoSize.Width;
            btnSkill.Location = new Point(207, 3);
            btnSkill.Name = "btnSkill";
            btnSkill.Size = new Size(66, 41);
            btnSkill.TabIndex = 12;
            btnSkill.Text = "技能";
            btnSkill.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnKnowlege
            // 
            btnKnowlege.AutoSizeMode = AntdUI.TAutoSize.Width;
            btnKnowlege.Location = new Point(119, 3);
            btnKnowlege.Name = "btnKnowlege";
            btnKnowlege.Size = new Size(82, 41);
            btnKnowlege.TabIndex = 11;
            btnKnowlege.Text = "知识库";
            btnKnowlege.Type = AntdUI.TTypeMini.Primary;
            // 
            // ddConfig
            // 
            ddConfig.DisplayStyle = AntdUI.TButtonDisplayStyle.Image;
            ddConfig.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            ddConfig.IconHoverSvg = "";
            ddConfig.IconSvg = "SettingFilled";
            ddConfig.Items.AddRange(new object[] { "配置", "好友接入", "计划消息", "消息转发", "语音/克隆" });
            ddConfig.JoinMode = AntdUI.TJoinMode.Right;
            ddConfig.Location = new Point(84, 3);
            ddConfig.Margin = new Padding(0, 3, 3, 3);
            ddConfig.MaxCount = 5;
            ddConfig.Name = "ddConfig";
            ddConfig.Size = new Size(29, 41);
            ddConfig.TabIndex = 5;
            ddConfig.Text = "设置";
            ddConfig.Trigger = AntdUI.Trigger.Hover;
            ddConfig.Type = AntdUI.TTypeMini.Success;
            // 
            // btnStart
            // 
            btnStart.DisplayStyle = AntdUI.TButtonDisplayStyle.Image;
            btnStart.IconSvg = "PlayCircleFilled";
            btnStart.JoinMode = AntdUI.TJoinMode.Left;
            btnStart.Location = new Point(23, 3);
            btnStart.Margin = new Padding(0, 3, 0, 3);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(61, 41);
            btnStart.TabIndex = 4;
            btnStart.ToggleIconSvg = "PauseCircleFilled";
            btnStart.ToggleType = AntdUI.TTypeMini.Success;
            btnStart.Type = AntdUI.TTypeMini.Error;
            // 
            // divider2
            // 
            divider2.Cursor = Cursors.SizeAll;
            divider2.Location = new Point(13, 5);
            divider2.Margin = new Padding(0, 5, 0, 5);
            divider2.Name = "divider2";
            divider2.Size = new Size(10, 37);
            divider2.TabIndex = 3;
            divider2.Text = "";
            divider2.TextPadding = 0F;
            divider2.Thickness = 1F;
            divider2.Vertical = true;
            // 
            // divider1
            // 
            divider1.Cursor = Cursors.SizeAll;
            divider1.Location = new Point(2, 5);
            divider1.Margin = new Padding(2, 5, 0, 5);
            divider1.Name = "divider1";
            divider1.Size = new Size(11, 37);
            divider1.TabIndex = 2;
            divider1.Text = "";
            divider1.TextPadding = 0F;
            divider1.Thickness = 1F;
            divider1.Vertical = true;
            // 
            // FlowerSidePane
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(397, 47);
            ControlBox = false;
            Controls.Add(stackPanel1);
            Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            MinimizeBox = false;
            Mode = AntdUI.TAMode.Light;
            Name = "FlowerSidePane";
            Resizable = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            stackPanel1.ResumeLayout(false);
            stackPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.StackPanel stackPanel1;
        private AntdUI.Divider divider2;
        private AntdUI.Divider divider1;
        private AntdUI.Button btnStart;
        private AntdUI.Button btnMCP;
        private AntdUI.Button btnSkill;
        private AntdUI.Button btnKnowlege;
        private AntdUI.Dropdown ddConfig;
    }
}