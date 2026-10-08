namespace Server.Forms
{
    partial class SideBarForm
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
            btnHistory = new AntdUI.Button();
            btnMonitorConfig = new AntdUI.Button();
            btnStart = new AntdUI.Button();
            divider2 = new AntdUI.Divider();
            divider1 = new AntdUI.Divider();
            stackPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // stackPanel1
            // 
            stackPanel1.Controls.Add(btnHistory);
            stackPanel1.Controls.Add(btnMonitorConfig);
            stackPanel1.Controls.Add(btnStart);
            stackPanel1.Controls.Add(divider2);
            stackPanel1.Controls.Add(divider1);
            stackPanel1.Dock = DockStyle.Fill;
            stackPanel1.Location = new Point(0, 0);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Size = new Size(434, 47);
            stackPanel1.TabIndex = 0;
            stackPanel1.Text = "stackPanel1";
            // 
            // btnSkill
            // 
            btnHistory.Location = new Point(225, 3);
            btnHistory.Name = "btnSkill";
            btnHistory.Size = new Size(99, 41);
            btnHistory.TabIndex = 12;
            btnHistory.Text = "历史消息";
            btnHistory.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnMonitorConfig
            // 
            btnMonitorConfig.Location = new Point(104, 3);
            btnMonitorConfig.Name = "btnMonitorConfig";
            btnMonitorConfig.Size = new Size(115, 41);
            btnMonitorConfig.TabIndex = 11;
            btnMonitorConfig.Text = "监听设置";
            btnMonitorConfig.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnStart
            // 
            btnStart.DisplayStyle = AntdUI.TButtonDisplayStyle.Image;
            btnStart.IconSvg = "PlayCircleFilled";
            btnStart.Location = new Point(23, 3);
            btnStart.Margin = new Padding(0, 3, 0, 3);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(78, 41);
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
            // SideBarForm
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(434, 47);
            ControlBox = false;
            Controls.Add(stackPanel1);
            Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 134);
            MinimizeBox = false;
            Mode = AntdUI.TAMode.Light;
            Name = "SideBarForm";
            Resizable = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            stackPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.StackPanel stackPanel1;
        private AntdUI.Divider divider2;
        private AntdUI.Divider divider1;
        private AntdUI.Button btnStart;
        private AntdUI.Button btnHistory;
        private AntdUI.Button btnMonitorConfig;
    }
}