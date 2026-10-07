using System;
using System.Drawing;
using System.Windows.Forms;
using WeChatAuto.Components;

namespace wechatbot
{
    public partial class SideBarForm : AntdUI.Window
    {
        private bool _isDragging;
        private Point _dragStartScreenPos;
        private readonly WeChatClient _client;

        public SideBarForm(WeChatClient client)
        {
            _client = client;
            InitializeComponent();
            AttachDragEvents();
        }

        /// <summary>
        ///  为 divider1 / divider2 绑定拖动 → 移动窗口事件。
        /// </summary>
        private void AttachDragEvents()
        {
            divider1.MouseDown += Divider_MouseDown;
            divider1.MouseMove += Divider_MouseMove;
            divider1.MouseUp += Divider_MouseUp;

            divider2.MouseDown += Divider_MouseDown;
            divider2.MouseMove += Divider_MouseMove;
            divider2.MouseUp += Divider_MouseUp;
        }

        private void Divider_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _isDragging = true;
            _dragStartScreenPos = Control.MousePosition;
        }

        private void Divider_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!_isDragging) return;

            Point now = Control.MousePosition;
            Location = new Point(
                Location.X + (now.X - _dragStartScreenPos.X),
                Location.Y + (now.Y - _dragStartScreenPos.Y));
            _dragStartScreenPos = now;
        }

        private void Divider_MouseUp(object? sender, MouseEventArgs e)
        {
            _isDragging = false;
        }
    }
}
