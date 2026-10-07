using System;
using System.Drawing;
using System.Windows.Forms;

namespace wechatbot
{
    public partial class FlowerSidePane : AntdUI.Window
    {
        private bool _isDragging;
        private Point _dragStartScreenPos;

        public FlowerSidePane()
        {
            InitializeComponent();
            AttachDragEvents();
            InitMenu();
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

        private void InitMenu()
        {
            // 在此初始化菜单或保留为空以消除编译错误
            ddConfig.Items.Clear();
            ddConfig.Items.AddRange(new AntdUI.SelectItem[]
            {
                new AntdUI.SelectItem("好友接入")
                {
                    IconSvg = "TeamOutlined",
                },
                new AntdUI.SelectItem("定时消息")
                {
                    IconSvg = "CommentOutlined",
                },
                new AntdUI.SelectItem("消息转发")
                {
                    IconSvg = "SendOutlined",
                },
                new AntdUI.SelectItem("语音/克隆")
                {
                    IconSvg = "SoundOutlined",
                },
                new AntdUI.SelectItem("配置")
                {
                    IconSvg = "SettingOutlined",
                },
            });
        }
    }
}
