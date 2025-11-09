using System.Diagnostics;
using FlaUI.Core.Capturing;
using WeChatAuto.Services;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WeChatAuto.Utils
{
    public class WeChatRecordVideo
    {
        public string TargetPath { get; set; }
        public VideoRecorder _VideoRecorder { get; set; } = null;


        public WeChatRecordVideo(string path)
        {
            TargetPath = path;
        }

        /// <summary>
        /// 录制视频
        /// 录制视频前，需要先下载ffmpeg(系统会自动下载ffmpeg,但是第一次会下载比较慢，请耐心等待)，并保存到当前目录下的ffmpeg文件夹
        /// </summary>
        /// <param name="fileName">文件名,默认不指定,如果不指定,则使用RecordVideo_20251109_123456.mp4作为文件名</param>
        /// <returns>视频文件路径</returns>
        public async Task<string> RecordVideo(string fileName = "")
        {
            var _fileName = fileName;
            var rootPath = Path.Combine(WeAutomation.Config.TargetVideoPath);
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            Trace.WriteLine($"开始录制视频,保存目录: {rootPath}");
            if (string.IsNullOrWhiteSpace(fileName))
            {
                _fileName = $"RecordVideo_{DateTime.Now.ToString("yyyy_MM_dd_HHmmss")}.mp4";
            }
            var filePath = Path.Combine(rootPath, _fileName);
            Trace.WriteLine($"开始录制视频,保存文件: {filePath}");
            var ffmpegPath = await VideoRecorder.DownloadFFMpeg(Path.Combine(Directory.GetCurrentDirectory(), "ffmpeg"));
            Trace.WriteLine($"ffmpeg路径: {ffmpegPath}");
            var video = new VideoRecorder(new VideoRecorderSettings { VideoQuality = 26, ffmpegPath = ffmpegPath, TargetVideoPath = filePath }, r =>
                {
                    var img = Capture.Screen();
                    var infoOverlay = new InfoOverlay(img);
                    var mouseOverlay = new MouseOverlay(img);
                    img.ApplyOverlays(mouseOverlay, infoOverlay);
                    return img;
                });
            _VideoRecorder = video;
            return filePath;
        }
    }
}