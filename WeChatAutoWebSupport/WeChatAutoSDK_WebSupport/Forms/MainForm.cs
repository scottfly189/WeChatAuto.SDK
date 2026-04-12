using AntdUI;
using FlaUI.Core.AutomationElements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Components;
using WeChatAuto.Services;
using WeChatAutoSDK_WebSupport.Enums;
using WeChatAutoSDK_WebSupport.Models;
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
        private CancellationTokenSource channelCts = new CancellationTokenSource();

        public MainForm()
        {
            InitializeComponent();
            InitData();
            BindEvents();
        }

        private void InitData()
        {
            LogsHelper.LogInfo("正在启动WechatAutoSDK Web Support中");
            pnlAvator.Controls.Clear();

            SetButtonTips();
        }

        private void SetButtonTips()
        {
            AntdUI.TooltipComponent tooltip = new AntdUI.TooltipComponent()
            {
                Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134))),
            };
            tooltip.SetTip(this.btnTopMost, "置顶窗口");
            tooltip.SetTip(this.btnCopy, "复制日志");
            tooltip.SetTip(this.btnClear, "清空日志");
            tooltip.SetTip(this.btnHelp, "帮助");
        }

        private void BindEvents()
        {
            this.btnStart.Click += BtnStart_Click;
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
            this.Shown += MainForm_Shown;
            this.btnCopy.Click += BtnCopy_Click;
            this.btnClear.Click += BtnClear_Click;
            this.btnTopMost.Click += BtnTopMost_Click;
        }

        private void BtnTopMost_Click(object? sender, EventArgs e)
        {
            if (this.TopMost)
            {
                this.TopMost = false;
                this.btnTopMost.IconSvg = "TagOutlined";
            }
            else
            {
                this.TopMost = true;
                this.btnTopMost.IconSvg = "TagsOutlined";
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            var dlg = AntdUI.Modal.open(new AntdUI.Modal.Config(this, "警告", "你要清空日志吗？", TType.Warn));
            if (dlg == DialogResult.OK)
            {
                this.txtLog.Clear();
            }
        }

        private void BtnCopy_Click(object? sender, EventArgs e)
        {
            Clipboard.SetText(txtLog.Text);
            AntdUI.Message.success(this, "服务器日志成功复制到剪切板", autoClose: 3);
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
            channelCts?.Cancel();
            clientFactory?.Dispose(); //释放.
            foreach (var helper in embedList.Values)
            {
                helper.Dispose();
            }
            LogsHelper.Dispose();
            WeChatAgent.Dispose();
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
            System.Windows.Forms.Panel panel = __AddNewPage__(nickName);

            var win = client!.WxMainWindow.SelfWindow;
            var handler = win.Properties.NativeWindowHandle;
            WxAttachHelper helper = new WxAttachHelper(panel, handler);
            embedList.Add(nickName, helper);
            var result = helper.StartAndEmbed();
            await Task.Yield();
            client!.WxMainWindow.WindowMax();
        }

        private System.Windows.Forms.Panel __AddNewPage__(string nickName)
        {
            tabsWX.SuspendLayout();
            AntdUI.TabPage page = new AntdUI.TabPage();
            page.Name = $"page_{nickName}";
            page.Text = nickName;
            page.Dock = DockStyle.Fill;
            tabsWX.Controls.Add(page);
            tabsWX.Pages.Add(page);

            System.Windows.Forms.Panel panel = new();
            panel.Dock = DockStyle.Fill;
            panel.BorderStyle = BorderStyle.None;
            page.Controls.Add(panel);

            tabsWX.SelectedTab = page;

            tabsWX.ResumeLayout();
            return panel;
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

        private async void MainForm_Load(object? sender, EventArgs e)
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
                    txtLog.AppendText(log + Environment.NewLine);
                    txtLog.ScrollToCaret();
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

            await WeChatAgent.RegisterConsumeAction(AutomationActionCore, EndCallBack, channelCts.Token);
        }
        /// <summary>
        /// 防风控的自动化核心操作
        /// </summary>
        /// <param name="action">自动化操作对象<see cref="AutomationAction"/></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<AutomationResult> AutomationActionCore(AutomationAction action, CancellationToken token)
        {
            AutomationResult result = new AutomationResult();
            result.Success = true;
            result.StartTime = DateTime.Now;
            result.MessageId = action.MessageId;
            switch (action.ActionType)
            {
                case ActionTypeEnum.SendMessage:
                    await SendMessageCore(action, result);
                    break;
                case ActionTypeEnum.SendFile:
                    await SendFileCore(action, result);
                    break;
                default:
                    break;
            }
            result.EndTime = DateTime.Now;
            return result;
        }

        private async Task SendMessageCore(AutomationAction action, AutomationResult result)
        {
            Action<string> uiAction = (page) =>
            {
                tabsWX.SelectTab(page);
            };
            var nickeName = action.From;
            var message = action.Payload!.ToString();
            message = message!.Replace("\\r", "\r").Replace("\\n", "\n");
            var who = action.To;
            try
            {
                var client = WeChatClientList![nickeName];
                _ = client == null ? throw new Exception($"传入的from={nickeName}不正确，或者微信客户端没有打开！") : client;
                var pageName = $"page_{nickeName}";
                if (this.tabsWX.InvokeRequired)
                {
                    this.Invoke(uiAction, pageName);
                }
                else
                {
                    uiAction(pageName);
                }
                LogsHelper.LogInfo($"准备发送消息: from={nickeName}, to={who}, message={message}, messageId={action.MessageId},reqest_method={action.Method}, reqest_url={action.Url}");
                await Task.Delay(Random.Shared.Next(500, 1000));
                await client.SendWho(who, message, isOpenChat: false);
                LogsHelper.LogInfo($"消息发送结束: from={nickeName}, to={who}, message={message}, messageId={action.MessageId},reqest_method={action.Method}, reqest_url={action.Url}");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.DescriptionIfError = ex.ToString();
            }
        }

        private async Task SendFileCore(AutomationAction action, AutomationResult result)
        {
            Action<string> uiAction = (page) =>
            {
                tabsWX.SelectTab(page);
            };
            var path = Path.Combine(AppContext.BaseDirectory, "Temp");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var nickeName = action.From;
            (string fileName, byte[] content) fileInfo = ((string fileName, byte[] content))action.Payload!;
            var who = action.To;
            try
            {
                var client = WeChatClientList![nickeName];
                _ = client == null ? throw new Exception($"传入的from={nickeName}不正确，或者微信客户端没有打开！") : client;
                var pageName = $"page_{nickeName}";
                if (this.tabsWX.InvokeRequired)
                {
                    this.Invoke(uiAction, pageName);
                }
                else
                {
                    uiAction(pageName);
                }

                path = Path.Combine(path, fileInfo.fileName);
                await File.WriteAllBytesAsync(path, fileInfo.content);
                await Task.Delay(Random.Shared.Next(200, 1000));
                LogsHelper.LogInfo($"准备发送消息: from={nickeName}, to={who}, clientsuggestionFileName={fileInfo.fileName}, messageId={action.MessageId},reqest_method={action.Method}, reqest_url={action.Url}");
                await client.SendFile(who, path, isOpenChat: false);
                LogsHelper.LogInfo($"消息发送结束: from={nickeName}, to={who}, clientsuggestionFileName={fileInfo.fileName}, messageId={action.MessageId},reqest_method={action.Method}, reqest_url={action.Url}");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.DescriptionIfError = ex.ToString();
            }

        }



        /// <summary>
        /// 你应该在此处实现自动化操作完成后的回调逻辑,比如:把自动化结果上传你的服务器<see cref="AutomationResult"/>对象.
        /// </summary>
        /// <param name="result">自动化结束后返回的结果对象，如果你需要处理结果，可以在此方法中实现<see cref="AutomationResult"/></param>
        private void EndCallBack(AutomationResult result)
        {
            var messageId = result.MessageId;
            //这里根据messageId对应你的业务逻辑进行处理,比如:把自动化结果上传你的服务器等.
            //可以在此处设置断点，查看result的值.
            // ... 请根据自己的业务需求实现此处逻辑
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
                options.DebugMode = false;
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
        /// <summary>
        /// 启动web api服务.
        /// </summary>
        /// <returns></returns>
        private Task StartWebApiAsync()
        {
            webCts = new CancellationTokenSource();

            var builder = WebApplication.CreateBuilder();
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5000);
            });
            ConfigServices(builder);
            app = builder.Build();
            ConfigWebApp(app);
            MapUIAutomation(app);

            LogsHelper.LogInfo("Web API started on http://localhost:5000");

            return app!.StartAsync(webCts.Token);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigServices(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
        /// <summary>
        /// 配置web应用
        /// </summary>
        /// <param name="app"></param>
        private void ConfigWebApp(WebApplication app)
        {
            app.UseCors("AllowAll"); //允许跨域访问
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        /// <summary>
        /// 接收请求，进行自动化操作.
        /// </summary>
        private void MapUIAutomation(WebApplication app)
        {
            app.MapGet("/", () => "hello world!");
            app.MapGet("/api/v1/message", async (string from, string to, string message, string messageId, HttpContext context) => await __MessageSendAction(from, to, message, messageId, context));
            app.MapPost("/api/v1/message", async (AutomationMessage message, HttpContext context) =>
            {
                await __MessageSendAction(message.From, message.To, message.Message, message.MessageId, context);
            });
            app.MapPost("/api/v1/file", async (AutomationFile file, HttpContext context) => await __FileSendAction(file, context));
        }
        /// <summary>
        /// 发送图片、视频等文件内容.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task __FileSendAction(AutomationFile file, HttpContext context)
        {
            LogsHelper.LogInfo($"收到发送文件请求: from={file.From}, to={file.To}, fileName={file.ClientSuggestionFileName}, messageId={file.MessageId},url={context?.Request?.Path},method={context?.Request?.Method},已放入运行管道!");
            (string fileName, byte[] content) fileInfo = (file.ClientSuggestionFileName, file.FileContent);
            AutomationAction action = new AutomationAction
            {
                From = file.From,
                To = file.To,
                Payload = fileInfo,
                ActionType = ActionTypeEnum.SendFile,
                MessageId = file.MessageId,
                Url = context?.Request?.Path,
                Method = context?.Request?.Method,
            };
            await WeChatAgent.WriteAsync(action, channelCts.Token);
        }

        /// <summary>
        /// 发送文字消息的Action
        /// </summary>
        /// <param name="from">发送的微信昵称</param>
        /// <param name="to">发送给谁 - 好友或者群聊昵称</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        private async Task __MessageSendAction(string from, string to, string message, string? messageId, HttpContext? context)
        {
            LogsHelper.LogInfo($"收到发送消息请求: from={from}, to={to}, message={message}, messageId={messageId},url={context?.Request?.Path},method={context?.Request?.Method},已放入运行管道!");
            AutomationAction action = new AutomationAction
            {
                From = from,
                To = to,
                Payload = message,
                ActionType = ActionTypeEnum.SendMessage,
                MessageId = messageId,
                Url = context?.Request?.Path,
                Method = context?.Request?.Method,
            };
            await WeChatAgent.WriteAsync(action, channelCts.Token);
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
