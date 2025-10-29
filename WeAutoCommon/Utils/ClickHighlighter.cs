using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

/// <summary>
/// 点击高亮器
/// </summary>
public static class ClickHighlighter
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hwnd);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateSolidBrush(uint crColor);

    [DllImport("gdi32.dll")]
    private static extern bool Ellipse(IntPtr hdc, int left, int top, int right, int bottom);

    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    /// <summary>
    /// 显示当前鼠标点击
    /// </summary>
    public static void ShowClick()
    {
        ShowClick(Cursor.Position);
    }

    /// <summary>
    /// 显示当前鼠标点击
    /// </summary>
    /// <param name="position">坐标</param>
    /// <param name="radius">半径</param>
    /// <param name="duration">停留时间</param>
    public static void ShowClick(Point position, int radius = 20, int duration = 1000)
    {
        IntPtr desktopDC = GetDC(IntPtr.Zero);
        uint color = (uint)((255) | (0 << 8) | (0 << 16)); // 红色
        IntPtr brush = CreateSolidBrush(color);
        IntPtr old = SelectObject(desktopDC, brush);
        // 绘制圆形
        Ellipse(desktopDC,
            position.X - radius,
            position.Y - radius,
            position.X + radius,
            position.Y + radius);

        // 停留一会儿再清除
        Thread.Sleep(duration);

        // 删除 GDI 对象
        SelectObject(desktopDC, old);
        DeleteObject(brush);
        ReleaseDC(IntPtr.Zero, desktopDC);
    }
}
