using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using WeAutoCommon.Simulator;

namespace WeChatAuto.Services
{
    /// <summary>
    /// 键鼠模拟器服务
    /// </summary>
    public static class KMSimulatorService
    {
        private static IntPtr _deviceData = IntPtr.Zero;
        public static IntPtr DeviceData => _deviceData;
        #region 设备初始化
        public static void Init(int deviceVID, int devicePID, string verifyUserData,int outputStringType=4)
        {
            CopyDllToCurrentDirectory();
            Thread.Sleep(600);
            var deviceId = SearchDevice(deviceVID, devicePID);
            OpenDevice(deviceId);
            VerifyUserData(verifyUserData);
            SetOutputMode(outputStringType);
            SetMouseMode(WeAutomation.Config.MouseMoveMode);
        }
        private static void VerifyUserData(string verifyUserData)
        {
            if (string.IsNullOrEmpty(verifyUserData))
            {
                throw new Exception("键鼠模拟器校验数据不能为空!");
            }
            if (Skm.HKMVerifyUserData2(_deviceData, verifyUserData, false) != Skm.HKMGetSerialNumber(_deviceData, false))
            {
                throw new Exception("键鼠模拟器校验数据错误!");
            }
        }
        /// <summary>
        /// 复制DLL到当前目录
        /// </summary>
        /// <exception cref="Exception"></exception>
        private static void CopyDllToCurrentDirectory()
        {
            var path = Environment.Is64BitProcess ? "x64" : "x86";
            path = Path.Combine(AppContext.BaseDirectory, path, "skm.dll");
            if (!File.Exists(path))
            {
                throw new Exception("未找到键鼠模拟器DLL文件,请检查文件是否存在!");
            }
            if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "skm.dll")))
            {
                File.Copy(path, Path.Combine(AppContext.BaseDirectory, "skm.dll"), true);
            }
        }

        /// <summary>
        /// 搜索设备
        /// </summary>
        /// <param name="deviceVID">设备VID</param>
        /// <param name="devicePID">设备PID</param>
        /// <returns>设备ID</returns>
        public static UInt32 SearchDevice(int deviceVID, int devicePID)
        {
            var deviceId = Skm.HKMSearchDevice((UInt32)deviceVID, (UInt32)devicePID, 0);
            if (deviceId == 0xFFFFFFFF)
            {
                throw new Exception("未找到键鼠模拟器设备,请检查设备是否连接好!");
            }

            return deviceId;
        }
        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="deviceID">设备ID</param>
        public static void OpenDevice(UInt32 deviceID)
        {
            _deviceData = Skm.HKMOpen(deviceID, 0);
            if (_deviceData == IntPtr.Zero)
            {
                throw new Exception("打开键鼠模拟器设备失败!");
            }
        }
        /// <summary>
        /// 设置鼠标移动模式
        /// </summary>
        /// <param name="mode"></param>
        private static void SetMouseMode(int mode)
        {
            Skm.HKMSetMode(_deviceData, 2, (uint)mode);
        }
        /// <summary>
        /// 设置鼠标移动模式
        /// </summary>
        private static void SetOutputMode(int outputStringType=4)
        {
            Skm.HKMSetMode(_deviceData, 4, (uint)outputStringType);
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public static void CloseDevice()
        {
            if (_deviceData == IntPtr.Zero)
            {
                return;
            }
            Skm.HKMClose(_deviceData);
            _deviceData = IntPtr.Zero;
        }
        /// <summary>
        /// 判断设备是否打开
        /// </summary>
        /// <returns>是否打开</returns>
        public static bool IsDeviceOpen() => Skm.HKMIsOpen(_deviceData, 0);

        #endregion

        /// <summary>
        /// 随机延时
        /// </summary>
        /// <param name="minTime">最小延时</param>
        /// <param name="maxTime">最大延时</param>
        public static void Delay(int minTime, int maxTime)
        {
            Skm.HKMDelayRnd(_deviceData, (UInt32)minTime, (UInt32)maxTime);
        }
        #region 键盘操作
        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <param name="str">字符串</param>
        public static void KeyPressString(string str)
        {
            Skm.HKMOutputString(_deviceData, str);
        }
        /// <summary>
        /// 清空输入框并输入字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="window">窗口</param>
        /// <param name="element">元素</param>
        public static void ClearAndTypeString(string str, Window window, AutomationElement element)
        {
            LeftClick(window, element);
            Delay(100, 300);
            KeyPress("Ctrl+A");
            Delay(100, 300);
            KeyPress("Backspace");
            Delay(100, 300);
            KeyPressString(str);
        }

        /// <summary>
        /// 按下回车键
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="element">元素</param>
        public static void Enter(Window window, AutomationElement element)
        {
            Delay(500, 1000);
            KeyPress("Enter");
        }
        /// <summary>
        /// 键盘按下
        /// </summary>
        /// <param name="key">按键</param>
        public static void KeyDown(string key)
        {
            Skm.HKMKeyDown(_deviceData, key);
        }
        /// <summary>
        /// 键盘弹起
        /// </summary>
        /// <param name="key">按键</param>
        public static void KeyUp(string key)
        {
            Skm.HKMKeyUp(_deviceData, key);
        }
        /// <summary>
        /// 键盘按键
        /// </summary>
        /// <param name="key">按键</param>
        public static void KeyPress(string key)
        {
            Skm.HKMKeyPress(_deviceData, key);
        }


        #endregion
        #region 鼠标操作
        /// <summary>
        /// 相对移动
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public static void MoveBy(int x, int y)
        {
            Skm.HKMMoveR(_deviceData, x, y);
        }

        /// <summary>
        /// 绝对移动
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public static void MoveTo(int x, int y)
        {
            Skm.HKMMoveTo(_deviceData, x, y);
        }
        /// <summary>
        /// 左键单击
        /// </summary>
        public static void LeftClick()
        {
            Skm.HKMLeftClick(_deviceData);
        }
        /// <summary>
        /// 左键单击
        /// </summary>
        /// <param name="point">坐标</param>
        public static void LeftClick(Point point)
        {
            LeftClick((int)point.X, (int)point.Y);
        }
        /// <summary>
        /// 左键单击
        /// 此方法dpi感知,并且加上元素范围内的随机偏移
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="element">元素</param>
        public static void LeftClick(Window window, AutomationElement element)
        {
            #region 版本一： 加了DPI感知，但是在进程中把dpi设置为System_DPI_Aware，导致点击不准确，但保留此版本，以备不时之需
            // double scale = GetScaleForWindow(window.Properties.NativeWindowHandle.Value);
            // int width = (int)(element.BoundingRectangle.Width * scale) - 2 * WeAutomation.Config.OffsetOfClick;
            // int height = (int)(element.BoundingRectangle.Height * scale) - 2 * WeAutomation.Config.OffsetOfClick;
            // var point = new Point();
            // if (width <= 0 || height <= 0)
            // {
            //     point = window.GetDpiAwarePoint(element);
            //     LeftClick(point);
            //     return;
            // }
            // var random = new Random();
            // var offsetX = random.Next(1, width + 1);
            // var offsetY = random.Next(1, height + 1);
            // point = new Point((int)(element.BoundingRectangle.Left * scale + WeAutomation.Config.OffsetOfClick + offsetX), (int)(element.BoundingRectangle.Top * scale + WeAutomation.Config.OffsetOfClick + offsetY));
            // LeftClick(point);
            #endregion

            #region 版本二： 不加DPI感知，因为进入口已经加上DPI感知，直接点击，测试通过，但是不优化，保留此版本，以备不时之需
            // double scale = 1;
            // int width = (int)(element.BoundingRectangle.Width * scale) - 2 * WeAutomation.Config.OffsetOfClick;
            // int height = (int)(element.BoundingRectangle.Height * scale) - 2 * WeAutomation.Config.OffsetOfClick;
            // var point = new Point();
            // if (width <= 0 || height <= 0)
            // {
            //     point = window.GetDpiAwarePoint(element);
            //     LeftClick(point);
            //     return;
            // }
            // var random = new Random();
            // var offsetX = random.Next(1, width + 1);
            // var offsetY = random.Next(1, height + 1);
            // point = new Point((int)(element.BoundingRectangle.Left * scale + WeAutomation.Config.OffsetOfClick + offsetX), (int)(element.BoundingRectangle.Top * scale + WeAutomation.Config.OffsetOfClick + offsetY));

            // LeftClick(point);
            #endregion
            //最新版本
            int width = element.BoundingRectangle.Width - 2 * WeAutomation.Config.OffsetOfClick;
            int height = element.BoundingRectangle.Height - 2 * WeAutomation.Config.OffsetOfClick;
            var point = element.BoundingRectangle.Center();
            if (width <= 0 || height <= 0)
            {
                LeftClick(point);
                return;
            }
            var random = new Random();
            var offsetX = random.Next(1, width + 1);
            var offsetY = random.Next(1, height + 1);
            var x = element.BoundingRectangle.Left + WeAutomation.Config.OffsetOfClick + offsetX;
            var y = element.BoundingRectangle.Top + WeAutomation.Config.OffsetOfClick + offsetY;
            if (x <= element.BoundingRectangle.Left || x >= element.BoundingRectangle.Right || y <= element.BoundingRectangle.Top || y >= element.BoundingRectangle.Bottom)
            {
                LeftClick(point);
            }
            else
            {
                point = new Point(x, y);
                LeftClick(point);
            }
        }
        /// <summary>
        /// 左键双击
        /// 此方法dpi感知,并且加上元素范围内的随机偏移
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="element">元素</param>
        public static void LeftDblClickWithDpiAware(Window window, AutomationElement element)
        {
            int width = element.BoundingRectangle.Width - 2 * WeAutomation.Config.OffsetOfClick;
            int height = element.BoundingRectangle.Height - 2 * WeAutomation.Config.OffsetOfClick;
            var point = element.BoundingRectangle.Center();
            if (width <= 0 || height <= 0)
            {
                LeftDoubleClick(point);
                return;
            }
            var random = new Random();
            var offsetX = random.Next(1, width + 1);
            var offsetY = random.Next(1, height + 1);
            var x = element.BoundingRectangle.Left + WeAutomation.Config.OffsetOfClick + offsetX;
            var y = element.BoundingRectangle.Top + WeAutomation.Config.OffsetOfClick + offsetY;
            if (x <= element.BoundingRectangle.Left || x >= element.BoundingRectangle.Right || y <= element.BoundingRectangle.Top || y >= element.BoundingRectangle.Bottom)
            {
                LeftDoubleClick(point);
            }
            else
            {
                point = new Point(x, y);
                LeftDoubleClick(point);
            }
        }
        /// <summary>
        /// 左键单击
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public static void LeftClick(int x, int y)
        {
            Skm.HKMMoveTo(_deviceData, x, y);
            Skm.HKMDelayRnd(_deviceData, 100, 150);
            Skm.HKMLeftClick(_deviceData);
        }
        /// <summary>
        /// 右键单击
        /// </summary>
        public static void RightClick()
        {
            Skm.HKMRightClick(_deviceData);
        }
        /// <summary>
        /// 右键单击
        /// </summary>
        /// <param name="point">坐标</param>
        public static void RightClick(Point point)
        {
            RightClick((int)point.X, (int)point.Y);
        }
        /// <summary>
        /// 右键单击
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public static void RightClick(int x, int y)
        {
            Skm.HKMMoveTo(_deviceData, x, y);
            Skm.HKMDelayRnd(_deviceData, 100, 150);
            Skm.HKMRightClick(_deviceData);
        }

        /// <summary>
        /// 右键单击
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="element">元素</param>
        public static void RightClick(Window window, AutomationElement element)
        {
            var point = element.BoundingRectangle.Center();
            RightClick(point);
        }

        /// <summary>
        /// 中键单击
        /// </summary>
        public static void MiddleClick()
        {
            Skm.HKMMiddleClick(_deviceData);
        }
        /// <summary>
        /// 中键单击
        /// </summary>
        /// <param name="point">坐标</param>
        public static void MiddleClick(Point point)
        {
            MiddleClick((int)point.X, (int)point.Y);
        }
        /// <summary>
        /// 中键单击
        /// 此方法dpi感知
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="element">元素</param>
        public static void MiddleClick(Window window, AutomationElement element)
        {
            var point = element.BoundingRectangle.Center();
            MiddleClick(point);
        }
        /// <summary>
        /// 中键单击
        /// 此方法dpi感知
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public static void MiddleClick(int x, int y)
        {
            Skm.HKMMoveTo(_deviceData, x, y);
            Skm.HKMDelayRnd(_deviceData, 100, 150);
            Skm.HKMMiddleClick(_deviceData);
        }

        /// <summary>
        /// 鼠标滚轮
        /// </summary>
        /// <param name="count">滚轮数量</param>
        public static void MouseWheel(int count = 3)
        {
            Skm.HKMMouseWheel(_deviceData, count);
        }
        /// <summary>
        /// 鼠标滚轮
        /// </summary>
        /// <param name="point">坐标</param>
        /// <param name="count">滚轮数量</param>
        public static void MouseWheel(Point point, int count = 3)
        {
            MouseWheel((int)point.X, (int)point.Y, count);
        }
        /// <summary>
        /// 鼠标滚轮
        /// 此方法dpi感知
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="element">元素</param>
        /// <param name="count">滚轮数量</param>
        public static void MouseWheel(Window window, AutomationElement element, int count = 3)
        {
            var point = element.BoundingRectangle.Center();
            MouseWheel(point, count);
        }
        /// <summary>
        /// 鼠标滚轮
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="count">滚轮数量</param>
        public static void MouseWheel(int x, int y, int count = 3)
        {
            Skm.HKMMoveTo(_deviceData, x, y);
            Skm.HKMDelayRnd(_deviceData, 100, 150);
            Skm.HKMMouseWheel(_deviceData, count);
        }
        /// <summary>
        /// 左键双击
        /// </summary>
        /// <param name="point"></param>
        public static void LeftDoubleClick(Point point)
        {
            Skm.HKMMoveTo(_deviceData, point.X, point.Y);
            Skm.HKMDelayRnd(_deviceData, 100, 150);
            Skm.HKMLeftDoubleClick(_deviceData);
        }
        /// <summary>
        /// 左键双击
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void LeftDoubleClick(int x, int y)
        {
            LeftDoubleClick(new Point(x, y));
        }
        #endregion

        // /// <summary>
        // /// 获取窗口缩放比例
        // /// </summary>
        // /// <param name="hwnd">窗口句柄</param>
        // /// <returns>缩放比例</returns>
        // public static double GetScaleForWindow(IntPtr hwnd)
        // {
        //     return DpiHelper.GetWindowDpi(hwnd) / 96.0;
        // }
    }
}