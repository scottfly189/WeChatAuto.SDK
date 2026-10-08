using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 赛博朋克风格的屏幕提示框。
    /// <para>
    /// 用于在耗时操作（例如强开 UI Tree 扫描 weixin.dll）期间，在屏幕上绘制一个
    /// 无边框、置顶、不抢焦点、不进入任务栏的半透明 HUD 提示框。
    /// </para>
    /// <para>
    /// 提示框运行在独立的 STA 后台线程中，拥有自己的消息循环，因此不会阻塞调用方，
    /// 也不会与 UIA 自动化线程相互干扰。
    /// </para>
    /// 用法：
    /// <![CDATA[
    /// using (CyberOverlay.Show($"正在强开 pid={pid} 的微信 UI Tree 中..."))
    /// {
    ///     // 执行耗时操作
    /// }
    /// ]]>
    /// </summary>
    public sealed class CyberOverlay : IDisposable
    {
        private readonly Thread _uiThread;
        private OverlayForm _form;
        private readonly ManualResetEventSlim _ready = new ManualResetEventSlim(false);
        private int _closed;

        private CyberOverlay(string message)
        {
            _uiThread = new Thread(() => RunMessageLoop(message))
            {
                IsBackground = true,
                Name = "wechatauto.sdk.cyber-overlay"
            };
            _uiThread.SetApartmentState(ApartmentState.STA);
            _uiThread.Start();
            // 等待窗口真正显示；超时则降级为不显示，避免卡住调用方。
            _ready.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// 在屏幕上显示一个赛博朋克风格的提示框。
        /// </summary>
        /// <param name="message">要显示的提示文字。</param>
        /// <returns>提示框控制器，释放或调用 <see cref="Close"/> 即可关闭。</returns>
        public static CyberOverlay Show(string message)
        {
            return new CyberOverlay(message);
        }

        /// <summary>
        /// 更新提示框中的文字（线程安全，可跨线程调用）。
        /// </summary>
        public void UpdateMessage(string message)
        {
            var form = _form;
            if (form == null || form.IsDisposed)
            {
                return;
            }
            try
            {
                if (form.InvokeRequired)
                {
                    form.BeginInvoke(new Action(() => form.SetMessage(message)));
                }
                else
                {
                    form.SetMessage(message);
                }
            }
            catch
            {
                // 窗口可能刚好被关闭，忽略跨线程调度异常。
            }
        }

        /// <summary>
        /// 关闭提示框。
        /// </summary>
        public void Close()
        {
            if (Interlocked.Exchange(ref _closed, 1) == 1)
            {
                return;
            }

            var form = _form;
            if (form != null && !form.IsDisposed)
            {
                try
                {
                    if (form.InvokeRequired)
                    {
                        form.BeginInvoke(new Action(() => form.Close()));
                    }
                    else
                    {
                        form.Close();
                    }
                }
                catch
                {
                    // 忽略关闭过程中的异常。
                }
            }

            // 如果窗口尚未创建完成，解除 Show 的等待，并让窗口线程自行退出。
            _ready.Set();

            if (_uiThread != null && _uiThread.IsAlive &&
                _uiThread.ManagedThreadId != Thread.CurrentThread.ManagedThreadId)
            {
                _uiThread.Join(TimeSpan.FromSeconds(2));
            }
        }

        public void Dispose()
        {
            Close();
        }

        private void RunMessageLoop(string message)
        {
            try
            {
                using (var form = new OverlayForm(message, _ready, () => Volatile.Read(ref _closed) == 1))
                {
                    _form = form;
                    if (Volatile.Read(ref _closed) == 1)
                    {
                        return;
                    }
                    Application.Run(form);
                }
            }
            catch
            {
                _ready.Set();
            }
            finally
            {
                _form = null;
            }
        }

        /// <summary>
        /// 赛博朋克 HUD 提示框窗体。
        /// </summary>
        private sealed class OverlayForm : Form
        {
            private const int WS_EX_TOPMOST = 0x00000008;
            private const int WS_EX_TOOLWINDOW = 0x00000080;
            private const int WS_EX_NOACTIVATE = 0x08000000;

            private static readonly Color BgColor = Color.FromArgb(10, 14, 22);
            private static readonly Color NeonCyan = Color.FromArgb(0, 255, 224);
            private static readonly Color NeonMagenta = Color.FromArgb(255, 0, 200);
            private static readonly Color DimCyan = Color.FromArgb(0, 170, 190);
            private static readonly Color DimText = Color.FromArgb(120, 150, 175);

            private readonly ManualResetEventSlim _ready;
            private readonly Func<bool> _shouldClose;
            private readonly Font _titleFont;
            private readonly Font _messageFont;
            private readonly Font _decoFont;
            private readonly System.Windows.Forms.Timer _timer;
            private readonly Random _rand = new Random();

            private string _message;
            private int _tick;
            private string _flicker = string.Empty;

            public OverlayForm(string message, ManualResetEventSlim ready, Func<bool> shouldClose)
            {
                _message = message;
                _ready = ready;
                _shouldClose = shouldClose;

                _titleFont = new Font("Consolas", 9f, FontStyle.Bold, GraphicsUnit.Point);
                _messageFont = new Font("Microsoft YaHei UI", 13f, FontStyle.Bold, GraphicsUnit.Point);
                _decoFont = new Font("Consolas", 8f, FontStyle.Regular, GraphicsUnit.Point);

                FormBorderStyle = FormBorderStyle.None;
                ControlBox = false;
                MinimizeBox = false;
                MaximizeBox = false;
                ShowInTaskbar = false;
                StartPosition = FormStartPosition.Manual;
                TopMost = true;
                BackColor = BgColor;
                Opacity = 0.96;

                float scale = DeviceDpi / 96f;
                Size = new Size((int)(620 * scale), (int)(168 * scale));

                var workArea = Screen.PrimaryScreen.WorkingArea;
                Location = new Point(
                    workArea.Left + (workArea.Width - Width) / 2,
                    workArea.Top + (workArea.Height - Height) / 3);

                SetStyle(
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw,
                    true);

                _timer = new System.Windows.Forms.Timer { Interval = 33 };
                _timer.Tick += (_, _) => { _tick++; Invalidate(); };
            }

            /// <summary>窗口显示时不抢占焦点。</summary>
            protected override bool ShowWithoutActivation => true;

            protected override CreateParams CreateParams
            {
                get
                {
                    var cp = base.CreateParams;
                    cp.ExStyle |= WS_EX_TOPMOST | WS_EX_TOOLWINDOW | WS_EX_NOACTIVATE;
                    return cp;
                }
            }

            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                Region = BuildRoundedRegion(new Rectangle(Point.Empty, Size), 14);
            }

            protected override void OnShown(EventArgs e)
            {
                base.OnShown(e);
                if (_shouldClose())
                {
                    Close();
                    return;
                }
                _timer.Start();
                _ready.Set();
            }

            public void SetMessage(string message)
            {
                _message = message;
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                try
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    var rect = new Rectangle(Point.Empty, Size);
                    PaintBackground(g, rect);
                    PaintBorderAndCorners(g, rect);
                    PaintHeader(g, rect);
                    PaintMessage(g, rect);
                    PaintScanline(g, rect);
                    PaintProgress(g, rect);
                    PaintDecoration(g, rect);
                }
                catch
                {
                    // 绘制失败不影响主流程，静默忽略。
                }
            }

            private void PaintBackground(Graphics g, Rectangle rect)
            {
                using (var path = RoundedRect(rect, 14))
                using (var brush = new SolidBrush(BgColor))
                {
                    g.FillPath(brush, path);
                }
            }

            private void PaintBorderAndCorners(Graphics g, Rectangle rect)
            {
                // 外发光：由外向内多描几圈，营造霓虹光晕。
                for (int i = 3; i >= 1; i--)
                {
                    int alpha = 18 * (4 - i);
                    using (var pen = new Pen(Color.FromArgb(alpha, NeonCyan), i * 2f))
                    using (var path = RoundedRect(rect, 14))
                    {
                        g.DrawPath(pen, path);
                    }
                }
                using (var border = new Pen(NeonCyan, 1.5f))
                using (var path = RoundedRect(rect, 14))
                {
                    g.DrawPath(border, path);
                }

                // 四角 HUD 括号。inset 略大于圆角半径，避免被圆角 Region 裁切。
                const int len = 16;
                const int inset = 15;
                using (var pen = new Pen(NeonMagenta, 2.5f))
                {
                    int x0 = inset, x1 = rect.Width - inset;
                    int y0 = inset, y1 = rect.Height - inset;
                    // 左上
                    g.DrawLines(pen, new[] { new Point(x0, y0 + len), new Point(x0, y0), new Point(x0 + len, y0) });
                    // 右上
                    g.DrawLines(pen, new[] { new Point(x1 - len, y0), new Point(x1, y0), new Point(x1, y0 + len) });
                    // 左下
                    g.DrawLines(pen, new[] { new Point(x0, y1 - len), new Point(x0, y1), new Point(x0 + len, y1) });
                    // 右下
                    g.DrawLines(pen, new[] { new Point(x1 - len, y1), new Point(x1, y1), new Point(x1, y1 - len) });
                }
            }

            private void PaintHeader(Graphics g, Rectangle rect)
            {
                float scale = DeviceDpi / 96f;
                int pad = (int)(18 * scale);
                int y = (int)(14 * scale);

                using (var brush = new SolidBrush(DimCyan))
                {
                    g.DrawString("WECHATAUTO.SDK // UI TREE ACTIVATOR", _titleFont, brush, pad, y);
                }

                // 右上角闪烁方块游标。
                string cursor = (_tick / 10) % 2 == 0 ? "█" : "░";
                var size = g.MeasureString(cursor, _titleFont);
                using (var brush = new SolidBrush(NeonCyan))
                {
                    g.DrawString(cursor, _titleFont, brush, rect.Width - pad - size.Width, y);
                }
            }

            private void PaintMessage(Graphics g, Rectangle rect)
            {
                float scale = DeviceDpi / 96f;
                int pad = (int)(18 * scale);
                var y = rect.Height / 2f;

                // 主提示文字 + 轻微品红辉光。
                using (var glow = new SolidBrush(Color.FromArgb(70, NeonMagenta)))
                {
                    g.DrawString(_message, _messageFont, glow, pad + 1.5f, y - 1);
                }
                using (var brush = new SolidBrush(NeonCyan))
                {
                    g.DrawString(_message, _messageFont, brush, pad, y - 3);
                }
            }

            private void PaintScanline(Graphics g, Rectangle rect)
            {
                // 自上而下的扫描亮线。
                int y = (_tick * 3) % Math.Max(1, rect.Height);
                using (var brush = new LinearGradientBrush(
                    new Rectangle(0, y, rect.Width, 2),
                    Color.FromArgb(0, NeonCyan),
                    Color.FromArgb(40, NeonCyan),
                    LinearGradientMode.Horizontal))
                {
                    g.FillRectangle(brush, 0, y, rect.Width, 2);
                }
            }

            private void PaintProgress(Graphics g, Rectangle rect)
            {
                float scale = DeviceDpi / 96f;
                int pad = (int)(18 * scale);
                int barY = rect.Height - (int)(26 * scale);
                int barH = (int)(4 * scale);
                int barW = rect.Width - pad * 2;

                using (var track = new Pen(Color.FromArgb(60, DimText), barH))
                {
                    g.DrawLine(track, pad, barY, pad + barW, barY);
                }

                // 不确定进度的来回扫描段。
                int segW = Math.Max(24, barW / 5);
                int cycle = barW - segW;
                int pos = _tick % (2 * cycle);
                if (pos > cycle)
                {
                    pos = 2 * cycle - pos;
                }
                using (var glow = new Pen(Color.FromArgb(60, NeonCyan), barH + 4))
                using (var seg = new Pen(NeonCyan, barH))
                {
                    g.DrawLine(glow, pad + pos, barY, pad + pos + segW, barY);
                    g.DrawLine(seg, pad + pos, barY, pad + pos + segW, barY);
                }
            }

            private void PaintDecoration(Graphics g, Rectangle rect)
            {
                // 右下角随机十六进制闪烁，营造“矩阵”感。
                if (_tick % 6 == 0)
                {
                    var chars = new char[12];
                    for (int i = 0; i < chars.Length; i++)
                    {
                        int v = _rand.Next(16);
                        chars[i] = v < 10 ? (char)('0' + v) : (char)('A' + v - 10);
                    }
                    _flicker = new string(chars);
                }
                float scale = DeviceDpi / 96f;
                int pad = (int)(16 * scale);
                var size = g.MeasureString(_flicker, _decoFont);
                using (var brush = new SolidBrush(Color.FromArgb(90, DimText)))
                {
                    g.DrawString(_flicker, _decoFont, brush, rect.Width - pad - size.Width, rect.Height - (int)(44 * scale));
                }
            }

            private static Region BuildRoundedRegion(Rectangle rect, int radius)
            {
                using (var path = RoundedRect(rect, radius))
                {
                    return new Region(path);
                }
            }

            private static GraphicsPath RoundedRect(Rectangle rect, int radius)
            {
                var path = new GraphicsPath();
                int d = radius * 2;
                path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                return path;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _timer?.Dispose();
                    _titleFont?.Dispose();
                    _messageFont?.Dispose();
                    _decoFont?.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
}
