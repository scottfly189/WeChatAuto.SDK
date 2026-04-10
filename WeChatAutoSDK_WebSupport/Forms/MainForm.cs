using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using WeChatAutoSDK_WebSupport.Utils;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Components;
using WeChatAuto.Services;

namespace WeChatAutoSDK_WebSupport
{
    public partial class MainForm : AntdUI.Window
    {
        private CancellationTokenSource? webCts;
        private WebApplication? app;
        private WeChatClientFactory? clientFactory;
        private Dictionary<string, WeChatClient>? WeChatClientList;

        public MainForm()
        {
            InitializeComponent();
            InitData();
            BindEvents();
        }

        private void InitData()
        {
            
        }


        private void BindEvents()
        {
            this.btnStart.Click += BtnStart_Click;
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            webCts?.Cancel();
            clientFactory?.Dispose(); //释放.
        }

        private async void MainForm_Load(object? sender, EventArgs e)
        {


            await InitWeChatAutoSDK();
        }

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
            foreach(var item in WeChatClientList)
            {
                var weixinName = item.Key;
                var client = item.Value;
                await _CheckAvator(item.Key,item.Value);
            }
        }

        private async Task _CheckAvator(string? name,WeChatClient? client)
        {
            string path = Path.Combine(AppContext.BaseDirectory,"Assets");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path,name!);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path,$"avator.png");
            if (File.Exists(path))
                return;
            //获得头像.
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
                //this.btnStatus.IconHoverSvg = "PauseCircleOutlined";
                this.btnStatus.Loading = true;
                try
                {
                    await StartWebApiAsync();
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
                this.btnStart.Text = "开始";
                this.btnStart.Type = AntdUI.TTypeMini.Primary;
                this.btnStatus.IconSvg = "PlayCircleOutlined";
                this.btnStatus.IconHoverSvg = "PlayCircleOutlined";
                this.btnStatus.Loading = false;
            }
        }
    }
}
