using Server.WebApi.Models;
using Server.WebApi.Services;
using WeChatAuto.Components;

namespace Server.Forms
{
    public partial class MonitorConfigForm : AntdUI.Window
    {
        // 消息监听方式标识，写入 App.json 的 MessageMonitor.Type，与三个监听方式复选框一一对应。
        private const string MonitorTypeOpen = "Open";        // 开放式监听
        private const string MonitorTypePopup = "Popup";      // 弹窗式监听
        private const string MonitorTypePolling = "Polling";  // 轮询监听

        private readonly WeChatClient? client;
        private readonly AppConfigStore _store = new();

        public MonitorConfigForm(WeChatClient client)
        {
            this.client = client;
            InitializeComponent();
            InitData();
            InitEvents();
        }

        private void InitData()
        {
            pageHeader1.Text = "微信号 - " + client?.NickName;
            LoadExistingConfig();
        }

        private void InitEvents()
        {
            checkbox1.CheckedChanged += Checkbox_CheckedChanged;
            checkbox2.CheckedChanged += Checkbox_CheckedChanged;
            checkbox3.CheckedChanged += Checkbox_CheckedChanged;
        }

        /// <summary>三个监听方式复选框强制互斥：勾选其中一个时自动取消另外两个。</summary>
        private void Checkbox_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            if (sender == checkbox1 && checkbox1.Checked)
            {
                checkbox2.Checked = false;
                checkbox3.Checked = false;
            }
            else if (sender == checkbox2 && checkbox2.Checked)
            {
                checkbox1.Checked = false;
                checkbox3.Checked = false;
            }
            else if (sender == checkbox3 && checkbox3.Checked)
            {
                checkbox1.Checked = false;
                checkbox2.Checked = false;
            }
        }

        /// <summary>把已保存的消息监听配置回填到界面；无配置或类型未知时默认选中“开放式监听”。</summary>
        private void LoadExistingConfig()
        {
            try
            {
                if (string.IsNullOrEmpty(client?.NickName))
                {
                    checkbox1.Checked = true;
                    return;
                }

                var appConfig = _store.Load();
                var clientConfig = appConfig.Config.FirstOrDefault(c => c.WechatNickName == client!.NickName);
                ApplyMonitorType(clientConfig?.MessageMonitor?.Type);
            }
            catch
            {
                // 读取失败时保持默认，不影响界面展示
                checkbox1.Checked = true;
            }
        }

        /// <summary>根据保存的监听方式字符串回填三个复选框。</summary>
        private void ApplyMonitorType(string? type)
        {
            checkbox1.Checked = string.Equals(type, MonitorTypeOpen, StringComparison.OrdinalIgnoreCase);
            checkbox2.Checked = string.Equals(type, MonitorTypePopup, StringComparison.OrdinalIgnoreCase);
            checkbox3.Checked = string.Equals(type, MonitorTypePolling, StringComparison.OrdinalIgnoreCase);

            if (!checkbox1.Checked && !checkbox2.Checked && !checkbox3.Checked)
                checkbox1.Checked = true;
        }

        /// <summary>从三个复选框读取当前选中的监听方式（互斥后至多一个被选中；均未选中时默认开放式）。</summary>
        private string GetSelectedMonitorType()
        {
            if (checkbox3.Checked) return MonitorTypePolling;
            if (checkbox2.Checked) return MonitorTypePopup;
            return MonitorTypeOpen;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await SaveConfig();
        }

        /// <summary>保存消息监听配置到 App.json（后台执行，避免阻塞界面）。</summary>
        private async Task SaveConfig()
        {
            if (string.IsNullOrEmpty(client?.NickName))
            {
                AntdUI.Notification.info(this, "提示", "未获取到微信号，无法保存。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                return;
            }

            var nickName = client!.NickName;
            var type = GetSelectedMonitorType();

            btnMessageMonitorSave.Enabled = false;
            try
            {
                var error = await Task.Run(() => SaveCore(nickName, type));
                if (error == null)
                    AntdUI.Notification.success(this, "提示", "消息监听配置已保存。", autoClose: 3, align: AntdUI.TAlignFrom.Top);
                else
                    AntdUI.Notification.error(this, "提示", error, autoClose: 5, align: AntdUI.TAlignFrom.Top);
            }
            finally
            {
                btnMessageMonitorSave.Enabled = true;
            }
        }

        /// <summary>保存配置的后台逻辑，返回 null 表示成功，否则为错误信息。</summary>
        private string? SaveCore(string nickName, string type)
        {
            try
            {
                var appConfig = _store.Load();
                var clientConfig = appConfig.Config.FirstOrDefault(c => c.WechatNickName == nickName);
                if (clientConfig == null)
                {
                    clientConfig = new ClientConfig { WechatNickName = nickName };
                    appConfig.Config.Add(clientConfig);
                }

                clientConfig.MessageMonitor.Type = type;
                _store.Save(appConfig);
                return null;
            }
            catch (Exception ex)
            {
                return $"保存配置失败：{ex.Message}";
            }
        }

        private void pageOpen_Click(object sender, EventArgs e)
        {
        }
    }
}
