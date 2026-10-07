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
            btnSkill = new AntdUI.Button();
            btnKnowlege = new AntdUI.Button();
            btnStart = new AntdUI.Button();
            divider2 = new AntdUI.Divider();
            divider1 = new AntdUI.Divider();
            stackPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // stackPanel1
            // 
            stackPanel1.Controls.Add(btnSkill);
            stackPanel1.Controls.Add(btnKnowlege);
            stackPanel1.Controls.Add(btnStart);
            stackPanel1.Controls.Add(divider2);
            stackPanel1.Controls.Add(divider1);
            stackPanel1.Dock = DockStyle.Fill;
            stackPanel1.Location = new Point(0, 0);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Size = new Size(366, 47);
            stackPanel1.TabIndex = 0;
            stackPanel1.Text = "stackPanel1";
            // 
            // btnSkill
            // 
            btnSkill.Location = new Point(195, 3);
            btnSkill.Name = "btnSkill";
            btnSkill.Size = new Size(99, 41);
            btnSkill.TabIndex = 12;
            btnSkill.Text = "数据库";
            btnSkill.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnKnowlege
            // 
            btnKnowlege.Location = new Point(104, 3);
            btnKnowlege.Name = "btnKnowlege";
            btnKnowlege.Size = new Size(85, 41);
            btnKnowlege.TabIndex = 11;
            btnKnowlege.Text = "消息";
            btnKnowlege.Type = AntdUI.TTypeMini.Primary;
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
            // FlowerSidePane
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(366, 47);
            ControlBox = false;
            Controls.Add(stackPanel1);
            Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 134);
            MinimizeBox = false;
            Mode = AntdUI.TAMode.Light;
            Name = "FlowerSidePane";
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
        private AntdUI.Button btnSkill;
        private AntdUI.Button btnKnowlege;
    }
}