using System;
using System.Linq;
using System.Text.RegularExpressions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Models;

namespace WeChatAuto.Utils
{
    public class MomentsHelper
    {
        /// <summary>
        /// 解析朋友圈内容
        /// </summary>
        /// <param name="item">朋友圈内容列表项</param>
        /// <param name="nickName">我的昵称</param>
        /// <returns>朋友圈内容<see cref="MomentItem"/></returns>
        public MomentItem ParseMonentItem(ListBoxItem item, string nickName)
        {
            var monentItem = new MomentItem(nickName);
            var content = item.Name;
            var splitTempStr = content.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var title = splitTempStr[0].Trim();
            if (title.EndsWith(":"))
            {
                monentItem.Who = title.Substring(0, title.Length - 1);
            }
            else
            {
                monentItem.Who = title;
            }
            monentItem.Content = _GetContent(item);
            monentItem.Time = _GetTime(splitTempStr);
            monentItem.ListItemName = item.Name;
            _FetchLikes(monentItem, item);
            _FetchReplyItems(monentItem, item);


            return monentItem;
        }



        private void _FetchLikes(MomentItem monentItem, ListBoxItem item)
        {
            var xPath = "//Button[@Name='评论']";
            var button = item.FindFirstByXPath(xPath).AsButton();
            if (button != null)
            {
                var pane = button.GetParent().GetSibling(1);
                if (pane != null && pane.ControlType == ControlType.Pane)
                {
                    xPath = "/Pane[1]/Text";
                    var texts = pane.FindAllByXPath(xPath);
                    foreach (var text in texts)
                    {
                        monentItem.Likers.Add(text.Name.Trim());
                    }
                }
            }
        }

        private void _FetchReplyItems(MomentItem monentItem, ListBoxItem item)
        {
            var xPath = "//Button[@Name='评论']";
            var button = item.FindFirstByXPath(xPath).AsButton();
            if (button != null)
            {
                var pane = button.GetParent().GetSibling(1);
                if (pane != null && pane.ControlType == ControlType.Pane)
                {
                    xPath = "//List[@Name='评论']";
                    var listBox = pane.FindFirstByXPath(xPath)?.AsListBox();
                    if (listBox != null)
                    {
                        var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                        foreach (var subItem in items)
                        {
                            var historyCommentItem = new ReplyItem();
                            var splitSubItem = subItem.Name.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                            historyCommentItem.From = splitSubItem[0].Trim();
                            historyCommentItem.Content = splitSubItem[1].Trim();
                            if (splitSubItem[0].Trim().Contains("回复"))
                            {
                                splitSubItem = splitSubItem[0].Trim().Split(new string[] { "回复" }, StringSplitOptions.RemoveEmptyEntries);
                                historyCommentItem.From = splitSubItem[0].Trim();
                                historyCommentItem.ReplyTo = splitSubItem[1].Trim();
                            }
                            monentItem.ReplyItems.Add(historyCommentItem);
                        }
                    }
                }
            }
        }

        private string _GetTime(string[] content)
        {
            var time = "";
            content = content.Reverse().ToArray();
            foreach (var str in content)
            {
                if (str.Contains("秒"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("分钟"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("小时"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("天"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("月"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("日"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("年"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("刚刚"))
                {
                    time = str;
                    break;
                }
                if (Regex.IsMatch(str, @"(\d{1,2})月(\d{1,2})日"))
                {
                    time = str;
                    break;
                }
            }
            return time;
        }

        private string _GetContent(ListBoxItem item)
        {
            var pane = item.FindFirstByXPath("/Pane/Pane[1]/Pane[2]");
            var text = pane.FindFirstChild(cf => cf.ByControlType(ControlType.Text));
            if (text != null)
            {
                return text.Name.Trim();
            }
            return string.Empty;
        }
    }
}