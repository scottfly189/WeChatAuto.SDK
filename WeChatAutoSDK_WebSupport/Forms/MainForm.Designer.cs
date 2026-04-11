namespace WeChatAutoSDK_WebSupport
{
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
            AntdUI.Tabs.StyleLine styleLine2 = new AntdUI.Tabs.StyleLine();
            AntdUI.Tabs.StyleCard2 styleCard22 = new AntdUI.Tabs.StyleCard2();
            pageHeader1 = new AntdUI.PageHeader();
            btnTopMost = new AntdUI.Button();
            btnHelp = new AntdUI.Button();
            pnlMain = new AntdUI.Panel();
            panel4 = new AntdUI.Panel();
            panel3 = new AntdUI.Panel();
            tabsWX = new AntdUI.Tabs();
            tabPage1 = new AntdUI.TabPage();
            label2 = new AntdUI.Label();
            pnlRight = new AntdUI.Panel();
            tabsMain = new AntdUI.Tabs();
            pageOverview = new AntdUI.TabPage();
            panel2 = new AntdUI.Panel();
            panel6 = new AntdUI.Panel();
            stackPanel1 = new AntdUI.StackPanel();
            txtLog = new RichTextBox();
            stackPanel2 = new AntdUI.StackPanel();
            panel7 = new AntdUI.Panel();
            btnCopy = new AntdUI.Button();
            btnClear = new AntdUI.Button();
            label1 = new AntdUI.Label();
            panel5 = new AntdUI.Panel();
            btnStatus = new AntdUI.Button();
            btnStart = new AntdUI.Button();
            pageTools = new AntdUI.TabPage();
            panel1 = new AntdUI.Panel();
            pnlAvator = new AntdUI.FlowPanel();
            pageHeader1.SuspendLayout();
            pnlMain.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            tabsWX.SuspendLayout();
            tabPage1.SuspendLayout();
            pnlRight.SuspendLayout();
            tabsMain.SuspendLayout();
            pageOverview.SuspendLayout();
            panel2.SuspendLayout();
            panel6.SuspendLayout();
            stackPanel1.SuspendLayout();
            stackPanel2.SuspendLayout();
            panel7.SuspendLayout();
            panel5.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pageHeader1
            // 
            pageHeader1.BackColor = Color.BlueViolet;
            pageHeader1.Controls.Add(btnTopMost);
            pageHeader1.Controls.Add(btnHelp);
            pageHeader1.DividerThickness = 0F;
            pageHeader1.Dock = DockStyle.Top;
            pageHeader1.ForeColor = Color.White;
            pageHeader1.Location = new Point(0, 0);
            pageHeader1.Margin = new Padding(2, 3, 2, 3);
            pageHeader1.MaximizeBox = false;
            pageHeader1.Name = "pageHeader1";
            pageHeader1.ShowButton = true;
            pageHeader1.Size = new Size(1047, 34);
            pageHeader1.TabIndex = 0;
            pageHeader1.Text = "WeChatAuto.SDK - Web Support";
            pageHeader1.UseForeColorDrawIcons = true;
            pageHeader1.UseSystemStyleColor = true;
            // 
            // btnTopMost
            // 
            btnTopMost.Dock = DockStyle.Right;
            btnTopMost.ForeColor = Color.White;
            btnTopMost.Ghost = true;
            btnTopMost.IconSvg = "TagOutlined";
            btnTopMost.Location = new Point(867, 0);
            btnTopMost.Margin = new Padding(2, 3, 2, 3);
            btnTopMost.Name = "btnTopMost";
            btnTopMost.Size = new Size(42, 34);
            btnTopMost.TabIndex = 1;
            btnTopMost.WaveSize = 0;
            // 
            // btnHelp
            // 
            btnHelp.Dock = DockStyle.Right;
            btnHelp.ForeColor = Color.White;
            btnHelp.Ghost = true;
            btnHelp.IconSvg = "QuestionOutlined";
            btnHelp.Location = new Point(909, 0);
            btnHelp.Margin = new Padding(2, 3, 2, 3);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(42, 34);
            btnHelp.TabIndex = 0;
            btnHelp.WaveSize = 0;
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(panel4);
            pnlMain.Controls.Add(panel1);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 34);
            pnlMain.Margin = new Padding(2, 3, 2, 3);
            pnlMain.Name = "pnlMain";
            pnlMain.RadiusAlign = AntdUI.TAlignRound.Bottom;
            pnlMain.Size = new Size(1047, 703);
            pnlMain.TabIndex = 1;
            pnlMain.Text = "panel1";
            // 
            // panel4
            // 
            panel4.BackColor = Color.BlueViolet;
            panel4.Controls.Add(panel3);
            panel4.Controls.Add(pnlRight);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(73, 0);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.RadiusAlign = AntdUI.TAlignRound.BR;
            panel4.Size = new Size(974, 703);
            panel4.TabIndex = 1;
            panel4.Text = "panel4";
            // 
            // panel3
            // 
            panel3.BackColor = Color.BlueViolet;
            panel3.Controls.Add(tabsWX);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(739, 703);
            panel3.TabIndex = 2;
            panel3.Text = "panel3";
            // 
            // tabsWX
            // 
            tabsWX.Controls.Add(tabPage1);
            tabsWX.Dock = DockStyle.Fill;
            tabsWX.Location = new Point(0, 0);
            tabsWX.Margin = new Padding(0);
            tabsWX.Name = "tabsWX";
            tabsWX.Pages.Add(tabPage1);
            tabsWX.Size = new Size(739, 703);
            tabsWX.Style = styleLine2;
            tabsWX.TabIndex = 0;
            tabsWX.TabMenuVisible = false;
            tabsWX.Text = "tabs1";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label2);
            tabPage1.Location = new Point(0, 0);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(739, 703);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label2.ForeColor = Color.White;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(739, 703);
            label2.TabIndex = 0;
            label2.Text = "请不要动鼠标，静候微信初始化完成...";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlRight
            // 
            pnlRight.Back = SystemColors.Control;
            pnlRight.Controls.Add(tabsMain);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlRight.Location = new Point(739, 0);
            pnlRight.Margin = new Padding(0);
            pnlRight.Name = "pnlRight";
            pnlRight.RadiusAlign = AntdUI.TAlignRound.BR;
            pnlRight.Size = new Size(235, 703);
            pnlRight.TabIndex = 1;
            pnlRight.Text = "panel4";
            // 
            // tabsMain
            // 
            tabsMain.BackColor = Color.BlueViolet;
            tabsMain.Centered = true;
            tabsMain.Controls.Add(pageOverview);
            tabsMain.Controls.Add(pageTools);
            tabsMain.Dock = DockStyle.Fill;
            tabsMain.ForeColor = Color.White;
            tabsMain.Gap = 10;
            tabsMain.ItemSize = 60;
            tabsMain.Location = new Point(0, 0);
            tabsMain.Margin = new Padding(0);
            tabsMain.Name = "tabsMain";
            tabsMain.Pages.Add(pageOverview);
            tabsMain.Pages.Add(pageTools);
            tabsMain.Size = new Size(235, 703);
            styleCard22.Closable = AntdUI.Tabs.StyleCard2.CloseType.none;
            tabsMain.Style = styleCard22;
            tabsMain.TabIndex = 0;
            tabsMain.Text = "tabs1";
            tabsMain.Type = AntdUI.TabType.Card2;
            // 
            // pageOverview
            // 
            pageOverview.Controls.Add(panel2);
            pageOverview.ForeColor = Color.White;
            pageOverview.IconSvg = "BankOutlined";
            pageOverview.Location = new Point(0, 32);
            pageOverview.Margin = new Padding(2, 3, 2, 3);
            pageOverview.Name = "pageOverview";
            pageOverview.Size = new Size(235, 671);
            pageOverview.TabIndex = 0;
            pageOverview.Text = "概况";
            // 
            // panel2
            // 
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(panel5);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(2, 3, 2, 3);
            panel2.Name = "panel2";
            panel2.Radius = 0;
            panel2.Size = new Size(235, 671);
            panel2.TabIndex = 0;
            panel2.Text = "panel2";
            // 
            // panel6
            // 
            panel6.Controls.Add(stackPanel1);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 133);
            panel6.Margin = new Padding(2, 3, 2, 3);
            panel6.Name = "panel6";
            panel6.Radius = 0;
            panel6.Size = new Size(235, 538);
            panel6.TabIndex = 2;
            panel6.Text = "panel6";
            // 
            // stackPanel1
            // 
            stackPanel1.Controls.Add(txtLog);
            stackPanel1.Controls.Add(stackPanel2);
            stackPanel1.Dock = DockStyle.Fill;
            stackPanel1.Location = new Point(0, 0);
            stackPanel1.Margin = new Padding(2, 3, 2, 3);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Padding = new Padding(0, 0, 2, 0);
            stackPanel1.Size = new Size(235, 538);
            stackPanel1.TabIndex = 0;
            stackPanel1.Text = "stackPanel1";
            stackPanel1.Vertical = true;
            // 
            // txtLog
            // 
            txtLog.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            txtLog.ForeColor = SystemColors.GrayText;
            txtLog.Location = new Point(2, 37);
            txtLog.Margin = new Padding(2, 3, 2, 3);
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.Size = new Size(229, 496);
            txtLog.TabIndex = 2;
            txtLog.Text = "";
            // 
            // stackPanel2
            // 
            stackPanel2.Controls.Add(panel7);
            stackPanel2.Controls.Add(label1);
            stackPanel2.Location = new Point(3, 3);
            stackPanel2.Name = "stackPanel2";
            stackPanel2.Size = new Size(227, 28);
            stackPanel2.TabIndex = 1;
            stackPanel2.Text = "stackPanel2";
            // 
            // panel7
            // 
            panel7.Back = Color.Transparent;
            panel7.BackColor = Color.Transparent;
            panel7.Controls.Add(btnCopy);
            panel7.Controls.Add(btnClear);
            panel7.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panel7.Location = new Point(107, 3);
            panel7.Name = "panel7";
            panel7.Radius = 0;
            panel7.Size = new Size(122, 22);
            panel7.TabIndex = 2;
            panel7.Text = "panel7";
            // 
            // btnCopy
            // 
            btnCopy.Dock = DockStyle.Right;
            btnCopy.ForeColor = Color.White;
            btnCopy.Ghost = true;
            btnCopy.IconSvg = "CopyOutlined";
            btnCopy.Location = new Point(60, 0);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(31, 22);
            btnCopy.TabIndex = 1;
            // 
            // btnClear
            // 
            btnClear.Dock = DockStyle.Right;
            btnClear.ForeColor = Color.White;
            btnClear.Ghost = true;
            btnClear.IconSvg = "DeleteOutlined";
            btnClear.Location = new Point(91, 0);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(31, 22);
            btnClear.TabIndex = 0;
            // 
            // label1
            // 
            label1.ForeColor = Color.White;
            label1.Location = new Point(2, 3);
            label1.Margin = new Padding(2, 3, 2, 3);
            label1.Name = "label1";
            label1.PrefixSvg = "BellOutlined";
            label1.Size = new Size(100, 22);
            label1.TabIndex = 1;
            label1.Text = " 服务日志:";
            // 
            // panel5
            // 
            panel5.Controls.Add(btnStatus);
            panel5.Controls.Add(btnStart);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Margin = new Padding(2, 3, 2, 3);
            panel5.Name = "panel5";
            panel5.Radius = 0;
            panel5.Size = new Size(235, 133);
            panel5.TabIndex = 1;
            panel5.Text = "panel5";
            // 
            // btnStatus
            // 
            btnStatus.DisplayStyle = AntdUI.TButtonDisplayStyle.Image;
            btnStatus.Dock = DockStyle.Top;
            btnStatus.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnStatus.Ghost = true;
            btnStatus.IconHoverSvg = "PlayCircleOutlined";
            btnStatus.IconRatio = 1.6F;
            btnStatus.IconSvg = "PlayCircleOutlined";
            btnStatus.Location = new Point(0, 0);
            btnStatus.Margin = new Padding(2, 3, 2, 3);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(235, 70);
            btnStatus.TabIndex = 1;
            btnStatus.Type = AntdUI.TTypeMini.Success;
            btnStatus.WaveSize = 0;
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnStart.Location = new Point(29, 79);
            btnStart.Margin = new Padding(2, 3, 2, 3);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(181, 41);
            btnStart.TabIndex = 0;
            btnStart.Text = "开始";
            btnStart.Type = AntdUI.TTypeMini.Success;
            btnStart.WaveSize = 0;
            // 
            // pageTools
            // 
            pageTools.IconSvg = "SettingOutlined";
            pageTools.Location = new Point(0, 0);
            pageTools.Margin = new Padding(2, 3, 2, 3);
            pageTools.Name = "pageTools";
            pageTools.Size = new Size(0, 0);
            pageTools.TabIndex = 1;
            pageTools.Text = "工具";
            // 
            // panel1
            // 
            panel1.Back = Color.BlueViolet;
            panel1.Controls.Add(pnlAvator);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.RadiusAlign = AntdUI.TAlignRound.BL;
            panel1.Size = new Size(73, 703);
            panel1.TabIndex = 0;
            panel1.Text = "panel1";
            // 
            // pnlAvator
            // 
            pnlAvator.Align = AntdUI.TAlignFlow.Center;
            pnlAvator.BackColor = Color.Transparent;
            pnlAvator.Dock = DockStyle.Fill;
            pnlAvator.Gap = 5;
            pnlAvator.Location = new Point(0, 0);
            pnlAvator.Margin = new Padding(0);
            pnlAvator.Name = "pnlAvator";
            pnlAvator.Padding = new Padding(0, 10, 0, 0);
            pnlAvator.Size = new Size(73, 703);
            pnlAvator.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1047, 737);
            Controls.Add(pnlMain);
            Controls.Add(pageHeader1);
            ForeColor = Color.White;
            Margin = new Padding(2, 3, 2, 3);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            pageHeader1.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            tabsWX.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            tabsMain.ResumeLayout(false);
            pageOverview.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel6.ResumeLayout(false);
            stackPanel1.ResumeLayout(false);
            stackPanel2.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Panel pnlMain;
        private AntdUI.Panel panel2;
        private AntdUI.Panel panel3;
        private AntdUI.Panel panel4;
        private AntdUI.Panel panel1;
        private AntdUI.Panel pnlRight;
        private AntdUI.Tabs tabsMain;
        private AntdUI.TabPage pageOverview;
        private AntdUI.Panel panel6;
        private AntdUI.StackPanel stackPanel1;
        private RichTextBox txtLog;
        private AntdUI.Label label1;
        private AntdUI.Panel panel5;
        private AntdUI.Button btnStart;
        private AntdUI.TabPage pageTools;
        private AntdUI.Button btnStatus;
        private AntdUI.FlowPanel flowPanel1;
        private AntdUI.Button btnHelp;
        private AntdUI.Tabs tabsWX;
        private AntdUI.FlowPanel pnlAvator;
        private AntdUI.StackPanel stackPanel2;
        private AntdUI.Panel panel7;
        private AntdUI.Button btnCopy;
        private AntdUI.Button btnClear;
        private AntdUI.TabPage tabPage1;
        private AntdUI.Label label2;
        private AntdUI.Button btnTopMost;
    }
}
