using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
    public class Search
    {
        private TextBox _SearchEdit;
        public Search(Window window)
        {
            _SearchEdit = window.FindFirstByXPath("/Pane[2]/Pane/Pane[1]/Pane[1]/Pane[1]/Pane/Edit").AsTextBox();
        }

        public void SearchSomthing(string text)
        {
            _SearchEdit.Enter(text);
        }
    }
}