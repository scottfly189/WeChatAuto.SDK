using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using System.Linq;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 会话列表
    /// </summary>
    public class Conversations
    {
        private List<ListBoxItem> _Conversations = new List<ListBoxItem>();
        public Conversations(Window window)
        {
            RefreshList(window);
        }
        public void RefreshList(Window window)
        {
            var list = window.FindFirstByXPath("/Pane[2]/Pane/Pane[1]/Pane[2]/Pane/Pane/Pane/List").AsListBox();
            var listItems = list.FindAllChildren();
            foreach (var listItem in listItems)
            {
                _Conversations.Add(listItem.AsListBoxItem());
            }
        }
        /// <summary>
        /// 搜索会话
        /// </summary>
        /// <param name="name">会话名称</param>
        /// <returns>是否找到</returns>
        public bool SearchConversation(string name)
        {
            var conversation = _Conversations.FirstOrDefault(c => c.Name.Contains(name));
            if (conversation != null)
            {
                conversation.Focus();
                conversation.Select();  //可能有问题
                return true;
            }
            return false;
        }
    }
}