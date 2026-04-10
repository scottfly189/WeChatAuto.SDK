using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace WeChatAutoSDK_WebSupport
{
    public partial class MainForm : AntdUI.Window
    {
        public MainForm()
        {
            InitializeComponent();
            InitData();
            BindEvents();
            BindHttpListener();
        }

        private void InitData()
        {

        }

        private void BindEvents()
        {
            this.btnStart.Click += BtnStart_Click;
            this.Load += MainForm_Load;
        }

        private async void MainForm_Load(object? sender, EventArgs e)
        {
            await StartWebApiAsync();
        }

        private Task StartWebApiAsync()
        {
            var builder = WebApplication.CreateBuilder();
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5000);
            });
            var app = builder.Build();
            app.MapGet("/api/test", () => "OK");
            app.MapGet("/",()=>"hello world!");

            return app.StartAsync();
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            if (this.btnStart.Text!.Equals("开始"))
            {
                this.btnStart.Text = "停止";
                this.btnStart.Type = AntdUI.TTypeMini.Success;
                this.btnStatus.IconSvg = "PauseCircleOutlined";
                //this.btnStatus.IconHoverSvg = "PauseCircleOutlined";
                this.btnStatus.Loading = true;
            }
            else
            {
                this.btnStart.Text = "开始";
                this.btnStart.Type = AntdUI.TTypeMini.Primary;
                this.btnStatus.IconSvg = "PlayCircleOutlined";
                this.btnStatus.IconHoverSvg = "PlayCircleOutlined";
                this.btnStatus.Loading = false;
            }
        }

        private void BindHttpListener()
        {
            
        }
    }
}
