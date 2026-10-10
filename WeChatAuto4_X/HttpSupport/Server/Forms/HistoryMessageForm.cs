using System.Data;
using WeChatAuto.Components;
using WeChatAuto.Models;
using Server.WebApi.Models;
using Server.WebApi.Services;

namespace Server.Forms
{
    public partial class HistoryMessageForm : AntdUI.Window
    {
        private readonly WeChatClient client;

        /// <summary>
        /// 常见数据库名称列表，用于“配置”页的数据库类型下拉框。
        /// 后续接入新的数据库时，只需在此追加名称即可。
        /// </summary>
        public static readonly List<string> CommonDatabaseNames = new List<string>
        {
            "SQLite",
            "MySQL",
            "SQL Server",
            "PostgreSQL",
            "Oracle",
            "MariaDB",
            "MongoDB",
            "Redis",
            "ClickHouse",
            "DB2",
        };

        /// <summary>消息监听默认的 sqlite 连接字符串，与 <c>MessageMonitorOptions.ConnectionString</c> 保持一致。</summary>
        private const string DefaultSqliteConnectionString = "Data Source=wechat.db";

        /// <summary>“查看消息”表格的数据源，首列为多选状态列。</summary>
        private readonly DataTable messageTable = new DataTable();

        public HistoryMessageForm(WeChatClient client)
        {
            this.client = client;
            InitializeComponent();
            Text = client.NickName;
            pageHeader1.Text = "微信号 - "+client.NickName;
            InitDatabaseTypes();
            LoadExistingConfig();
            InitFilter();
            InitTable();
            InitEvents();
        }

        private void InitEvents()
        {
            btnTest.Click += async (s, e) => await TestConnection();
            btnSave.Click += async (s, e) => await SaveConfig();
            btnRefresh.Click += (s, e) => RefreshMessages();
            btnDelete.Click += (s, e) => DeleteSelectedMessages();
            tableMessage.CellButtonClick += TableMessage_CellButtonClick;
            tableMessage.CheckedChanged += TableMessage_CheckedChanged;
        }

        private void InitDatabaseTypes()
        {
            selectDbType.Items.Clear();
            foreach (var name in CommonDatabaseNames)
            {
                selectDbType.Items.Add(name);
            }
            selectDbType.SelectedIndex = 0; // 默认 SQLite
            inputConnStr.Text = DefaultSqliteConnectionString;
        }

        /// <summary>把该微信号已保存的数据库配置回填到界面；读取失败时保持默认值。</summary>
        private void LoadExistingConfig()
        {
            try
            {
                var appConfig = new AppConfigStore().Load();
                var clientConfig = appConfig.Config.FirstOrDefault(c => c.WechatNickName == client.NickName);
                var db = clientConfig?.Database;
                if (db == null)
                    return;

                if (!string.IsNullOrWhiteSpace(db.ConnectionString))
                    inputConnStr.Text = db.ConnectionString;

                if (SqlSugarDatabaseHelper.TryParseDbType(db.Type, out var dbType))
                {
                    var displayName = SqlSugarDatabaseHelper.GetDisplayName(dbType);
                    if (displayName == null)
                        return;

                    for (int i = 0; i < selectDbType.Items.Count; i++)
                    {
                        if (string.Equals(selectDbType.Items[i]?.ToString(), displayName, StringComparison.Ordinal))
                        {
                            selectDbType.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch
            {
                // 读取已有配置失败时保持默认值，不影响页面展示
            }
        }

        private void InitFilter()
        {
            selectFilter.Items.Clear();
            //selectFilter.Items.AddRange(new object[] { "全部", "文本", "图片", "语音", "视频", "文件", "表情", "链接" });
            selectFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// 表格字段参照 <see cref="WeChatMessage"/> 的字段定义，最前面为多选列，最后一列为操作栏。
        /// </summary>
        private void InitTable()
        {
            BuildMessageTable();

            tableMessage.Columns.Clear();

            // 最前面的多选列，绑定到数据源中的 bool 列 "Selected"
            tableMessage.Columns.Add(new AntdUI.ColumnCheck("Selected")
            {
                Width = "48",
                Align = AntdUI.ColumnAlign.Center,
            });

            tableMessage.Columns.Add(new AntdUI.Column(nameof(WeChatMessage.FromWechat), "微信账号") { Width = "110" });
            tableMessage.Columns.Add(new AntdUI.Column(nameof(WeChatMessage.Who), "好友/群") { Width = "120" });
            tableMessage.Columns.Add(new AntdUI.Column(nameof(WeChatMessage.Sender), "发送者") { Width = "100" });
            tableMessage.Columns.Add(new AntdUI.Column(nameof(WeChatMessage.MessageType), "消息类型") { Width = "90" });
            tableMessage.Columns.Add(new AntdUI.Column(nameof(WeChatMessage.Content), "消息内容") { Width = "240", Ellipsis = true });
            tableMessage.Columns.Add(new AntdUI.Column(nameof(WeChatMessage.MessageTime), "消息时间") { Width = "130" });

            var colIsSelf = new AntdUI.Column(nameof(WeChatMessage.IsSelf), "是否为自己")
            {
                Width = "90",
                Align = AntdUI.ColumnAlign.Center,
            };
            colIsSelf.Render = (value, record, index) => new AntdUI.CellText(value is bool b && b ? "是" : "否");
            tableMessage.Columns.Add(colIsSelf);

            tableMessage.Columns.Add(new AntdUI.Column(nameof(WeChatMessage.CreateTime), "写入时间") { Width = "150" });

            var colOperation = new AntdUI.Column("operation", "操作")
            {
                Width = "80",
                Align = AntdUI.ColumnAlign.Center,
            };
            colOperation.Render = (value, record, index) => new AntdUI.CellButton("delete")
            {
                IconSvg = "DeleteOutlined",
                Type = AntdUI.TTypeMini.Error,
                Tooltip = "删除",
                Radius = 6,
            };
            tableMessage.Columns.Add(colOperation);

            tableMessage.DataSource = messageTable;
        }

        private void BuildMessageTable()
        {
            messageTable.Columns.Clear();
            messageTable.Columns.Add("Selected", typeof(bool));
            messageTable.Columns.Add(nameof(WeChatMessage.FromWechat), typeof(string));
            messageTable.Columns.Add(nameof(WeChatMessage.Who), typeof(string));
            messageTable.Columns.Add(nameof(WeChatMessage.Sender), typeof(string));
            messageTable.Columns.Add(nameof(WeChatMessage.MessageType), typeof(string));
            messageTable.Columns.Add(nameof(WeChatMessage.Content), typeof(string));
            messageTable.Columns.Add(nameof(WeChatMessage.MessageTime), typeof(string));
            messageTable.Columns.Add(nameof(WeChatMessage.IsSelf), typeof(bool));
            messageTable.Columns.Add(nameof(WeChatMessage.CreateTime), typeof(DateTime));

            // 示例数据，接入真实数据库查询后移除
            AddSampleRow("wxid_test01", "产品交流群", "张三", "文本", "大家好，欢迎使用", "10:30:15", false, DateTime.Now.AddMinutes(-5));
            AddSampleRow("wxid_test01", "李四", "wxid_lisi", "图片", "[图片]", "10:31:02", true, DateTime.Now.AddMinutes(-4));
            AddSampleRow("wxid_test02", "项目讨论组", "王五", "文件", "[文件] 需求文档.pdf", "10:33:40", false, DateTime.Now.AddMinutes(-2));
        }

        private void AddSampleRow(string fromWechat, string who, string sender, string messageType, string content, string messageTime, bool isSelf, DateTime createTime)
        {
            var row = messageTable.NewRow();
            row["Selected"] = false;
            row[nameof(WeChatMessage.FromWechat)] = fromWechat;
            row[nameof(WeChatMessage.Who)] = who;
            row[nameof(WeChatMessage.Sender)] = sender;
            row[nameof(WeChatMessage.MessageType)] = messageType;
            row[nameof(WeChatMessage.Content)] = content;
            row[nameof(WeChatMessage.MessageTime)] = messageTime;
            row[nameof(WeChatMessage.IsSelf)] = isSelf;
            row[nameof(WeChatMessage.CreateTime)] = createTime;
            messageTable.Rows.Add(row);
        }

        /// <summary>单条删除（操作列按钮）。</summary>
        private void TableMessage_CellButtonClick(object sender, AntdUI.TableButtonEventArgs e)
        {
            if (e.Btn is not AntdUI.CellButton btn || btn.Id != "delete")
                return;

            DataRow? row = (e.Record as DataRowView)?.Row;
            if (row != null)
            {
                messageTable.Rows.Remove(row);
                ReloadTable();
            }
        }

        /// <summary>勾选/取消勾选时，把多选状态同步回数据源，供“删除”使用。</summary>
        private void TableMessage_CheckedChanged(object sender, AntdUI.TableCheckEventArgs e)
        {
            if (e.Record is DataRowView view)
            {
                view.Row["Selected"] = e.Value;
            }
        }

        private void ReloadTable()
        {
            tableMessage.DataSource = null;
            tableMessage.DataSource = messageTable;
        }

        /// <summary>测试当前界面填写的数据库连接（后台执行，避免阻塞界面）。</summary>
        private async Task TestConnection()
        {
            var displayName = selectDbType.SelectedValue?.ToString() ?? "";
            var connectionString = inputConnStr.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                AntdUI.Notification.info(this, "提示", "请先填写连接字符串。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                return;
            }

            if (!SqlSugarDatabaseHelper.TryGetDbType(displayName, out var dbType))
            {
                AntdUI.Notification.error(this, "提示", $"不支持的数据库类型：{displayName}", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                return;
            }

            btnTest.Enabled = false;
            btnSave.Enabled = false;
            try
            {
                var (ok, error) = await Task.Run(() =>
                {
                    var succeeded = SqlSugarDatabaseHelper.TestConnection(dbType, connectionString, out var err);
                    return (succeeded, err);
                });

                if (ok)
                    AntdUI.Notification.success(this, "提示", "连接测试成功。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                else
                    AntdUI.Notification.error(this, "提示", $"连接测试失败：{error}", autoClose: 5, align: AntdUI.TAlignFrom.Top);
            }
            finally
            {
                btnTest.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        /// <summary>
        /// 保存配置：先确认连接测试成功，再保存到 App.json（按微信昵称定位配置项），
        /// 若旧库与新库不同且旧库有数据，则导入新库。整个过程在后台执行，避免阻塞界面。
        /// </summary>
        private async Task SaveConfig()
        {
            var displayName = selectDbType.SelectedValue?.ToString() ?? "";
            var connectionString = inputConnStr.Text?.Trim() ?? "";
            var nickName = client.NickName;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                AntdUI.Notification.info(this, "提示", "请先填写连接字符串。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                return;
            }

            if (!SqlSugarDatabaseHelper.TryGetDbType(displayName, out var newDbType))
            {
                AntdUI.Notification.error(this, "提示", $"不支持的数据库类型：{displayName}", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                return;
            }

            btnTest.Enabled = false;
            btnSave.Enabled = false;
            try
            {
                var (ok, imported, error) = await Task.Run(() => SaveCore(nickName, newDbType, connectionString));

                if (ok)
                {
                    var message = imported > 0 ? $"配置已保存，并从旧库导入了 {imported} 条消息。" : "配置已保存。";
                    AntdUI.Notification.success(this, "提示", message, autoClose: 5, align: AntdUI.TAlignFrom.Top);
                }
                else
                {
                    AntdUI.Notification.error(this, "提示", error ?? "保存配置失败。", autoClose: 5, align: AntdUI.TAlignFrom.Top);
                }
            }
            finally
            {
                btnTest.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        /// <summary>保存配置的后台逻辑，返回（是否成功、导入条数、错误信息）。</summary>
        private static (bool ok, int imported, string? error) SaveCore(string nickName, SqlSugar.DbType newDbType, string connectionString)
        {
            try
            {
                // 1. 先确认连接测试成功（同时完成建库建表）
                if (!SqlSugarDatabaseHelper.TestConnection(newDbType, connectionString, out var error))
                    return (false, 0, $"连接测试失败，未保存：{error}");

                // 2. 读取旧配置
                var store = new AppConfigStore();
                var appConfig = store.Load();
                var clientConfig = appConfig.Config.FirstOrDefault(c => c.WechatNickName == nickName);
                var oldDb = clientConfig?.Database;

                // 3. 若旧库与新库不同，且旧库有数据，则导入新库
                int imported = 0;
                if (oldDb != null
                    && !string.IsNullOrWhiteSpace(oldDb.ConnectionString)
                    && SqlSugarDatabaseHelper.TryParseDbType(oldDb.Type, out var oldDbType)
                    && !(oldDbType == newDbType && string.Equals(oldDb.ConnectionString, connectionString, StringComparison.OrdinalIgnoreCase)))
                {
                    imported = SqlSugarDatabaseHelper.ImportMessages(oldDbType, oldDb.ConnectionString, newDbType, connectionString);
                }

                // 4. 保存新配置到 App.json
                if (clientConfig == null)
                {
                    clientConfig = new ClientConfig { WechatNickName = nickName };
                    appConfig.Config.Add(clientConfig);
                }
                clientConfig.Database = new DatabaseConfig { Type = newDbType.ToString(), ConnectionString = connectionString };
                store.Save(appConfig);

                return (true, imported, null);
            }
            catch (Exception ex)
            {
                return (false, 0, $"保存配置失败：{ex.Message}");
            }
        }

        /// <summary>刷新占位，后续接入具体数据库 Provider 后实现。</summary>
        private void RefreshMessages()
        {
            AntdUI.Notification.info(this, "提示", "刷新功能开发中，敬请期待。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
        }

        /// <summary>批量删除勾选的消息。</summary>
        private void DeleteSelectedMessages()
        {
            var selected = messageTable.Rows.Cast<DataRow>().Where(r => (bool)r["Selected"]).ToList();
            if (selected.Count == 0)
            {
                AntdUI.Notification.info(this, "提示", "请先在表格中选择要删除的消息。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                return;
            }

            foreach (var row in selected)
            {
                messageTable.Rows.Remove(row);
            }
            ReloadTable();
        }
    }
}
