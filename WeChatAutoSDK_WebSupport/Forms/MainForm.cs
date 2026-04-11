using FlaUI.Core.AutomationElements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Components;
using WeChatAuto.Services;
using WeChatAutoSDK_WebSupport.Properties;
using WeChatAutoSDK_WebSupport.Utils;

namespace WeChatAutoSDK_WebSupport
{
    public partial class MainForm : AntdUI.Window
    {
        private CancellationTokenSource? webCts;
        private WebApplication? app;
        private WeChatClientFactory? clientFactory;
        private Dictionary<string, WeChatClient>? WeChatClientList;
        private Dictionary<string, WxAttachHelper> embedList = new Dictionary<string, WxAttachHelper>();

        public MainForm()
        {
            InitializeComponent();
            InitData();
            BindEvents();
        }

        private void InitData()
        {
            LogsHelper.LogInfo("---- 正在启动WechatAutoSDK Web Support中 ----");
            pnlAvator.Controls.Clear();
        }


        private void BindEvents()
        {
            this.btnStart.Click += BtnStart_Click;
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
            this.Shown += MainForm_Shown;
        }

        private async void MainForm_Shown(object? sender, EventArgs e)
        {
            LogsHelper.LogInfo("开始初始化WeChatAuto.SDK框架");
            await InitWeChatAutoSDK();
            if (tabsWX.Pages.Count > 0)
            {
                tabsWX.SelectedTab = tabsWX.Pages[1];
            }
            LogsHelper.LogInfo("WeChatAuto.SDK框架初始化完成");
        }
        /// <summary>
        /// 退出时释放对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SetEmbedWinNormal();
            webCts?.Cancel();
            clientFactory?.Dispose(); //释放.
            foreach (var helper in embedList.Values)
            {
                helper.Dispose();
            }
            LogsHelper.Dispose();
        }

        private void SetEmbedWinNormal()
        {
            for (int i = 1; i < tabsWX.Pages.Count; i++)
            {
                var page = tabsWX.Pages[i];
                tabsWX.SelectedTab = page;
                var name = page.Text;
                if (WeChatClientList!.ContainsKey(name))
                {
                    var client = WeChatClientList[name];
                    client.WxMainWindow.WindowRestore();
                }
            }
        }

        /// <summary>
        /// 初始化微信数据,包括头像和标签页等.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="nickName"></param>
        /// <param name="client"></param>
        private async Task InitWxData(string path, string nickName, WeChatClient? client)
        {
            _InitAvator(path, nickName);
            await _AttachWx(path, nickName, client);

        }
        //生成标签页,并绑定事件,点击标签页时显示对应的微信界面.
        //并且微信直接绑定在标签页上.
        private async Task _AttachWx(string path, string nickName, WeChatClient? client)
        {
            tabsWX.SuspendLayout();
            AntdUI.TabPage page = new AntdUI.TabPage();
            page.Name = $"page_{nickName}";
            page.Text = nickName;
            page.Dock = DockStyle.Fill;
            tabsWX.Controls.Add(page);
            tabsWX.Pages.Add(page);

            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BorderStyle = BorderStyle.None;
            page.Controls.Add(panel);

            tabsWX.SelectedTab = page;

            tabsWX.ResumeLayout();

            var win = client!.WxMainWindow.SelfWindow;
            var handler = win.Properties.NativeWindowHandle;
            WxAttachHelper helper = new WxAttachHelper(panel, handler);
            embedList.Add(nickName, helper);
            var result = helper.StartAndEmbed();
            await Task.Yield();
            client!.WxMainWindow.WindowMax();
        }

        private void _InitAvator(string path, string nickName)
        {
            pnlAvator.SuspendLayout();
            AntdUI.Avatar avator = new AntdUI.Avatar();
            avator.Image = Image.FromFile(path);
            avator.ImageFit = AntdUI.TFit.Fill;
            avator.Name = nickName;
            avator.Radius = 6;
            avator.Cursor = Cursors.Hand;
            avator.Size = new Size(64, 64);
            avator.Text = "";
            avator.Click += Avator_Click;
            pnlAvator.Controls.Add(avator);
            pnlAvator.ResumeLayout();
        }

        private void Avator_Click(object? sender, EventArgs e)
        {
            var avator = sender as AntdUI.Avatar;
            var name = avator!.Name;
            this.tabsWX.SelectTab($"page_{name}");
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            LogsHelper.RegisterConsume(log =>
            {
                Action action = () =>
                {
                    if (txtLog.Lines.Length > 10000)
                    {
                        var list = txtLog.Lines.ToList();
                        list.RemoveAt(0);
                        txtLog.Lines = list.ToArray();
                    }
                    txtLog.AppendText(log+Environment.NewLine);
                };
                if (txtLog.InvokeRequired)
                {
                    this.BeginInvoke(action);
                }
                else
                {
                    action();
                }
            });
        }
        /// <summary>
        /// 初始化微信自动化SDK,获得微信客户端列表,如果没有则提示用户打开微信PC客户端
        /// 第一次打开会自动获取微信头像并保存在本地,下次打开直接从本地加载头像,提高速度.如果微信头像发生变化,可以删除本地头像文件,重新获取.
        /// </summary>
        /// <returns></returns>
        private async Task InitWeChatAutoSDK()
        {
            var serviceProvider = WeAutomation.Initialize(options =>
            {
                options.DebugMode = true;
            });
            clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
            WeChatClientList = clientFactory.WeChatClientList;
            if (WeChatClientList.Count == 0)
            {
                AntdUI.Modal.open(new AntdUI.Modal.Config(this, "警告", "你必须首先打开微信PC客户端", AntdUI.TType.Error)
                {
                    BtnHeight = 0,
                });
                this.Close();
                return;
            }
            foreach (var item in WeChatClientList)
            {
                var weixinName = item.Key;
                var client = item.Value;
                await _CheckAvator(item.Key, item.Value);
            }
        }

        private async Task _CheckAvator(string? name, WeChatClient? client)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Assets");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, name!);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, $"avator.png");
            if (File.Exists(path))
            {
                await InitWxData(path, name!, client);
                return;
            }

            //获得头像.
            await client!.SaveOwnerAvator(path);
            await InitWxData(path, name!, client);
        }

        private Task StartWebApiAsync()
        {
            webCts = new CancellationTokenSource();

            var builder = WebApplication.CreateBuilder();
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5000);
            });
            app = builder.Build();
            app.MapGet("/api/test", () => "OK");
            app.MapGet("/", () => "hello world!");
            
            LogsHelper.LogInfo("Web API started on http://localhost:5000");
            foreach(var url in app.Urls)
            {
                LogsHelper.LogInfo($"Listening on {url}");
            }

            return app!.StartAsync(webCts.Token);
        }

        private Task StopWebApiAsync()
        {
            webCts?.Cancel();
            return app!.StopAsync();
        }

        private async void BtnStart_Click(object? sender, EventArgs e)
        {
            if (this.btnStart.Text!.Equals("开始"))
            {
                this.btnStart.Text = "停止";
                this.btnStart.Type = AntdUI.TTypeMini.Success;
                this.btnStatus.IconSvg = "PauseCircleOutlined";
                this.btnStatus.Loading = true;
                try
                {
                    LogsHelper.LogInfo("正在启动Web API...");
                    await StartWebApiAsync();
                    LogsHelper.LogInfo("Web API启动完成");
                }
                catch (OperationCanceledException)
                {

                }
                catch (Exception ex)
                {
                    LogsHelper.LogError(ex);
                }
            }
            else
            {
                await StopWebApiAsync();
                await Task.Delay(1000);
                if (app != null)
                {
                    await app.DisposeAsync();
                }
                LogsHelper.LogInfo("Web API已停止");
                this.btnStart.Text = "开始";
                this.btnStart.Type = AntdUI.TTypeMini.Primary;
                this.btnStatus.IconSvg = "PlayCircleOutlined";
                this.btnStatus.IconHoverSvg = "PlayCircleOutlined";
                this.btnStatus.Loading = false;
            }
        }
    }
}
