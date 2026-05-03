using System;
using FlaUI.Core.Capturing;
using System.IO;
using WeChatAuto.Services;
using System.Linq;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 截图工具类
    /// </summary>
    public class WeChatCaptureImage
    {
        private string _Path { get; set; } = "";
        private bool _UseOverlay { get; set; } = true;
        private object lockObject = new object();
        public WeChatCaptureImage(string path, bool useOverlay = true)
        {
            _Path = path;
            _UseOverlay = useOverlay;
        }
        /// <summary>
        /// 截图UI
        /// </summary>
        /// <param name="fileName">文件名,默认不指定,如果不指定,则使用Capture_20251109_123456.png作为文件名</param>
        /// <returns>截图文件路径</returns>
        public string CaptureUI(string fileName = "")
        {
            lock (lockObject)
            {
                var _fileName = fileName;
                var rootPath = Path.Combine(WeAutomation.Config.CaptureUIPath, DateTime.Now.ToString("yyyy_MM_dd"));
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    _fileName = $"Capture_{DateTime.Now.ToString("HHmmss_fff")}.png";
                }
                var filePath = Path.Combine(rootPath, _fileName);
                var image = Capture.Screen();
                if (_UseOverlay)
                {
                    var mouseOverlay = new MouseOverlay(image);
                    var infoOverlay = new InfoOverlay(image);
                    image.ApplyOverlays(mouseOverlay, infoOverlay);
                }

                image.ToFile(filePath);
                return filePath;
            }
        }
    }
}