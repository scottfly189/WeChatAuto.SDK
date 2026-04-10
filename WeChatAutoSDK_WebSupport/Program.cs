using AntdUI;
using WeChatAutoSDK_WebSupport.Utils;

namespace WeChatAutoSDK_WebSupport
{
    internal static class Program
    {
        private static MainForm? mainWindow;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            //AntdUI.Config.DpiMode = DpiMode.Compatible;
            AntdUI.Config.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            AntdUI.Config.TextRenderingHighQuality = true;

            mainWindow = new MainForm();
            Application.Run(mainWindow);
        }

        // 捕获UI线程中的未处理异常
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (mainWindow == null)
                return;
            AntdUI.Notification.error(mainWindow, "未处理的UI线程异常", e.Exception.Message, autoClose: 3, align: AntdUI.TAlignFrom.TR);
            LogsHelper.LogError(e.Exception.ToString() ?? "未处理的UI线程异常");
            LogsHelper.LogError(e.Exception.StackTrace ?? "未处理的UI线程异常");
        }

        // 捕获非UI线程中的未处理异常
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (mainWindow == null)
                return;
            var errorInfo = e == null ? "未知异常" : e.ToString()!;
            AntdUI.Notification.error(mainWindow, "未处理的非UI线程异常", errorInfo, autoClose: 3, align: AntdUI.TAlignFrom.TR);
            if (e != null && e.ExceptionObject != null && e.ExceptionObject is Exception ex)
            {
                LogsHelper.LogError(ex.ToString() ?? "未处理的非UI线程异常");
            }
        }
    }
}