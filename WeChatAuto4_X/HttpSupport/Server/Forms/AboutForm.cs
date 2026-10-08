namespace Server.Forms
{
    public partial class AboutForm : AntdUI.Window
    {
        public AboutForm()
        {
            InitializeComponent();
            btnOk.Click += (s, e) => Close();
        }
    }
}
