namespace wechatbot
{
    partial class HistoryMessageForm
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
            AntdUI.Tabs.StyleLine styleLine1 = new AntdUI.Tabs.StyleLine();
            pageHeader1 = new AntdUI.PageHeader();
            tabs1 = new AntdUI.Tabs();
            pageConfig = new AntdUI.TabPage();
            btnSave = new AntdUI.Button();
            btnTest = new AntdUI.Button();
            inputConnStr = new AntdUI.Input();
            selectDbType = new AntdUI.Select();
            lblConnStr = new AntdUI.Label();
            lblDbType = new AntdUI.Label();
            pageMessage = new AntdUI.TabPage();
            tableMessage = new AntdUI.Table();
            panelToolbar = new Panel();
            selectFilter = new AntdUI.Select();
            lblFilter = new AntdUI.Label();
            btnDelete = new AntdUI.Button();
            btnRefresh = new AntdUI.Button();
            tabs1.SuspendLayout();
            pageConfig.SuspendLayout();
            pageMessage.SuspendLayout();
            panelToolbar.SuspendLayout();
            SuspendLayout();
            // 
            // pageHeader1
            // 
            pageHeader1.Dock = DockStyle.Top;
            pageHeader1.Font = new Font("Microsoft YaHei UI", 12F);
            pageHeader1.Location = new Point(0, 0);
            pageHeader1.Name = "pageHeader1";
            pageHeader1.ShowButton = true;
            pageHeader1.Size = new Size(760, 60);
            pageHeader1.TabIndex = 0;
            pageHeader1.Text = "数据库";
            // 
            // tabs1
            // 
            tabs1.Controls.Add(pageConfig);
            tabs1.Controls.Add(pageMessage);
            tabs1.Dock = DockStyle.Fill;
            tabs1.Location = new Point(0, 60);
            tabs1.Name = "tabs1";
            tabs1.Pages.Add(pageConfig);
            tabs1.Pages.Add(pageMessage);
            tabs1.Size = new Size(760, 460);
            tabs1.Style = styleLine1;
            tabs1.TabIndex = 1;
            tabs1.Text = "tabs1";
            // 
            // pageConfig
            // 
            pageConfig.Controls.Add(btnSave);
            pageConfig.Controls.Add(btnTest);
            pageConfig.Controls.Add(inputConnStr);
            pageConfig.Controls.Add(selectDbType);
            pageConfig.Controls.Add(lblConnStr);
            pageConfig.Controls.Add(lblDbType);
            pageConfig.IconSvg = "SettingOutlined";
            pageConfig.Location = new Point(0, 38);
            pageConfig.Name = "pageConfig";
            pageConfig.Size = new Size(760, 422);
            pageConfig.TabIndex = 0;
            pageConfig.Text = "配置";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(262, 284);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 40);
            btnSave.TabIndex = 5;
            btnSave.Text = "保存配置";
            // 
            // btnTest
            // 
            btnTest.Location = new Point(140, 284);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(110, 40);
            btnTest.TabIndex = 4;
            btnTest.Text = "测试连接";
            btnTest.Type = AntdUI.TTypeMini.Primary;
            // 
            // inputConnStr
            // 
            inputConnStr.Location = new Point(140, 84);
            inputConnStr.Multiline = true;
            inputConnStr.Name = "inputConnStr";
            inputConnStr.PlaceholderText = "请输入连接字符串，例如 Data Source=wechat.db";
            inputConnStr.Size = new Size(460, 180);
            inputConnStr.TabIndex = 3;
            // 
            // selectDbType
            // 
            selectDbType.Location = new Point(140, 24);
            selectDbType.Name = "selectDbType";
            selectDbType.Size = new Size(240, 36);
            selectDbType.TabIndex = 1;
            // 
            // lblConnStr
            // 
            lblConnStr.Location = new Point(24, 84);
            lblConnStr.Name = "lblConnStr";
            lblConnStr.Size = new Size(100, 36);
            lblConnStr.TabIndex = 2;
            lblConnStr.Text = "连接字符串";
            // 
            // lblDbType
            // 
            lblDbType.Location = new Point(24, 24);
            lblDbType.Name = "lblDbType";
            lblDbType.Size = new Size(100, 36);
            lblDbType.TabIndex = 0;
            lblDbType.Text = "数据库类型";
            // 
            // pageMessage
            // 
            pageMessage.Controls.Add(tableMessage);
            pageMessage.Controls.Add(panelToolbar);
            pageMessage.IconSvg = "MessageOutlined";
            pageMessage.Location = new Point(-1520, -844);
            pageMessage.Name = "pageMessage";
            pageMessage.Size = new Size(760, 422);
            pageMessage.TabIndex = 1;
            pageMessage.Text = "查看消息";
            // 
            // tableMessage
            // 
            tableMessage.Dock = DockStyle.Fill;
            tableMessage.EmptyText = "暂无消息";
            tableMessage.Gap = 12;
            tableMessage.Location = new Point(0, 52);
            tableMessage.Name = "tableMessage";
            tableMessage.Size = new Size(760, 370);
            tableMessage.TabIndex = 1;
            tableMessage.Text = "tableMessage";
            // 
            // panelToolbar
            // 
            panelToolbar.Controls.Add(selectFilter);
            panelToolbar.Controls.Add(lblFilter);
            panelToolbar.Controls.Add(btnDelete);
            panelToolbar.Controls.Add(btnRefresh);
            panelToolbar.Dock = DockStyle.Top;
            panelToolbar.Location = new Point(0, 0);
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Size = new Size(760, 52);
            panelToolbar.TabIndex = 0;
            // 
            // selectFilter
            // 
            selectFilter.Location = new Point(60, 8);
            selectFilter.Name = "selectFilter";
            selectFilter.Size = new Size(180, 36);
            selectFilter.TabIndex = 1;
            // 
            // lblFilter
            // 
            lblFilter.Location = new Point(16, 8);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(40, 36);
            lblFilter.TabIndex = 0;
            lblFilter.Text = "筛选";
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Location = new Point(588, 8);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(76, 36);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "删除";
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Location = new Point(672, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(76, 36);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "刷新";
            // 
            // DatabaseForm
            // 
            AutoScaleDimensions = new SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 520);
            Controls.Add(tabs1);
            Controls.Add(pageHeader1);
            Font = new Font("Microsoft YaHei UI", 10F);
            Mode = AntdUI.TAMode.Light;
            Name = "DatabaseForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "数据库";
            tabs1.ResumeLayout(false);
            pageConfig.ResumeLayout(false);
            pageMessage.ResumeLayout(false);
            panelToolbar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Tabs tabs1;
        private AntdUI.TabPage pageConfig;
        private AntdUI.TabPage pageMessage;
        private AntdUI.Label lblDbType;
        private AntdUI.Select selectDbType;
        private AntdUI.Label lblConnStr;
        private AntdUI.Input inputConnStr;
        private AntdUI.Button btnTest;
        private AntdUI.Button btnSave;
        private Panel panelToolbar;
        private AntdUI.Label lblFilter;
        private AntdUI.Select selectFilter;
        private AntdUI.Button btnDelete;
        private AntdUI.Button btnRefresh;
        private AntdUI.Table tableMessage;
    }
}
