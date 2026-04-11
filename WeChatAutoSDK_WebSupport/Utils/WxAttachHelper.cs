using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace WeChatAutoSDK_WebSupport.Utils
{
    /// <summary>
    /// 微信控件绑定器帮助类,提供一些静态方法来绑定微信到控件上.
    /// </summary>
    public class WxAttachHelper:IDisposable
    {
        private Panel? _container;
        private IntPtr WxHandel = IntPtr.Zero;

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private const int GWL_STYLE = -16;
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_MAXIMIZE = 0x01000000;
        private const int WS_BORDER = 0x00800000;
        private const int WS_THICKFRAME = 0x00040000; // 移除可调整边框
        public WxAttachHelper(Panel control, IntPtr wxHandel)
        {
            this._container = control;
            this.WxHandel = wxHandel;

            BindEvents();
        }
        /// <summary>
        /// 内嵌微信控件至指定的容器中，并调整大小以适应容器。
        /// </summary>
        /// <returns>true:如果成功嵌入微信控件;否则为false。</returns>
        public bool StartAndEmbed()
        {
            try
            {
                SetParent(WxHandel, _container!.Handle);
                SetWindowLong(WxHandel, GWL_STYLE, WS_VISIBLE);
                ResizeScrcpy();
                return true;
            }catch(Exception ex) 
            {
                LogsHelper.LogError(ex);
                return false;
            }
        }

        private void BindEvents()
        {
            _container!.SizeChanged += (s, e) => ResizeScrcpy();
        }

        public void ResizeScrcpy()
        {
            if (WxHandel != IntPtr.Zero)
            {
                // 将 scrcpy 铺满整个 Panel
                MoveWindow(WxHandel, 0, 0, _container!.Width, _container!.Height, true);
            }
        }

        public void Dispose()
        {
            SetParent(WxHandel, IntPtr.Zero);
        }
    }
}
