using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms; // 引用 System.Windows.Forms.dll

public static class InputHelper
{
    #region Win32 structs & imports

    [StructLayout(LayoutKind.Sequential)]
    struct INPUT
    {
        public uint type;
        public InputUnion U;
    }

    [StructLayout(LayoutKind.Explicit)]
    struct InputUnion
    {
        [FieldOffset(0)] public MOUSEINPUT mi;
        [FieldOffset(0)] public KEYBDINPUT ki;
        // HARDWAREINPUT omitted
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    const uint INPUT_MOUSE = 0;
    const uint INPUT_KEYBOARD = 1;

    const uint MOUSEEVENTF_MOVE = 0x0001;
    const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    const uint MOUSEEVENTF_LEFTUP = 0x0004;
    const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

    const uint KEYEVENTF_KEYDOWN = 0x0000;
    const uint KEYEVENTF_KEYUP = 0x0002;
    // const uint KEYEVENTF_SCANCODE = 0x0008;

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    #endregion

    /// <summary>
    /// 将指定像素坐标转换为 SendInput 的绝对坐标 (0..65535)
    /// 考虑多显示器时按坐标所属屏幕做换算（使用 Screen.FromPoint）。
    /// </summary>
    static (int absX, int absY) ToAbsoluteCoordinates(int pixelX, int pixelY)
    {
        var pt = new Point(pixelX, pixelY);
        var screen = Screen.FromPoint(pt);
        var bounds = screen.Bounds; // 注意 bounds.Left 可能不为0（多显示器）
        // 在该屏幕上的相对像素坐标
        double relX = pixelX - bounds.Left;
        double relY = pixelY - bounds.Top;
        double absX = relX * (65535.0 / bounds.Width);
        double absY = relY * (65535.0 / bounds.Height);
        // Clamp & round
        int ax = Math.Max(0, Math.Min(65535, (int)Math.Round(absX)));
        int ay = Math.Max(0, Math.Min(65535, (int)Math.Round(absY)));
        return (ax, ay);
    }

    /// <summary>
    /// 在像素坐标 (x,y) 执行一次鼠标左键点击（使用 SendInput）。
    /// x,y 是屏幕像素坐标（可来自你获取到的按钮坐标）。
    /// </summary>
    public static void LeftClickAt(int x, int y)
    {
        var (ax, ay) = ToAbsoluteCoordinates(x, y);

        // Move (absolute), LeftDown, LeftUp
        INPUT[] inputs = new INPUT[3];

        inputs[0].type = INPUT_MOUSE;
        inputs[0].U.mi.dx = ax;
        inputs[0].U.mi.dy = ay;
        inputs[0].U.mi.mouseData = 0;
        inputs[0].U.mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE;
        inputs[0].U.mi.time = 0;
        inputs[0].U.mi.dwExtraInfo = IntPtr.Zero;

        inputs[1].type = INPUT_MOUSE;
        inputs[1].U.mi.dx = 0;
        inputs[1].U.mi.dy = 0;
        inputs[1].U.mi.mouseData = 0;
        inputs[1].U.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
        inputs[1].U.mi.time = 0;
        inputs[1].U.mi.dwExtraInfo = IntPtr.Zero;

        inputs[2].type = INPUT_MOUSE;
        inputs[2].U.mi.dx = 0;
        inputs[2].U.mi.dy = 0;
        inputs[2].U.mi.mouseData = 0;
        inputs[2].U.mi.dwFlags = MOUSEEVENTF_LEFTUP;
        inputs[2].U.mi.time = 0;
        inputs[2].U.mi.dwExtraInfo = IntPtr.Zero;

        uint sent = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        if (sent != inputs.Length)
        {
            throw new InvalidOperationException("SendInput failed to send all inputs. Error: " + Marshal.GetLastWin32Error());
        }
    }

    /// <summary>
    /// 使用 SendInput 发送一个回车（Enter）按键事件
    /// </summary>
    public static void SendEnter()
    {
        const ushort VK_RETURN = 0x0D;

        INPUT[] inputs = new INPUT[2];

        inputs[0].type = INPUT_KEYBOARD;
        inputs[0].U.ki.wVk = VK_RETURN;
        inputs[0].U.ki.wScan = 0;
        inputs[0].U.ki.dwFlags = KEYEVENTF_KEYDOWN;
        inputs[0].U.ki.time = 0;
        inputs[0].U.ki.dwExtraInfo = IntPtr.Zero;

        inputs[1].type = INPUT_KEYBOARD;
        inputs[1].U.ki.wVk = VK_RETURN;
        inputs[1].U.ki.wScan = 0;
        inputs[1].U.ki.dwFlags = KEYEVENTF_KEYUP;
        inputs[1].U.ki.time = 0;
        inputs[1].U.ki.dwExtraInfo = IntPtr.Zero;

        uint sent = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        if (sent != inputs.Length)
        {
            throw new InvalidOperationException("SendInput failed to send key inputs. Error: " + Marshal.GetLastWin32Error());
        }
    }

    // 可选：更健壮的封装，带重试与小延迟
    public static void LeftClickAtSafe(int x, int y, int preDelayMs = 50, int postDelayMs = 50)
    {
        Thread.Sleep(preDelayMs);
        LeftClickAt(x, y);
        Thread.Sleep(postDelayMs);
    }
}

