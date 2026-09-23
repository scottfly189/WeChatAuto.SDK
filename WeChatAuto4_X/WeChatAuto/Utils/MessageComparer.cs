using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using FlaUI.Core.AutomationElements;
using WeChatAuto.Models;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 消息是否相等比较器
    /// </summary>
    public class MessageComparer : IEqualityComparer<SimpleMessageBubble>
    {
        public bool Equals(SimpleMessageBubble x, SimpleMessageBubble y)
        {
            if (x == null || y == null)
                return false;
            //由于图片或者文件、视频等在微信消息中太容易重复，所以增加FilePath做为判断条件.
            x.FilePath = string.IsNullOrWhiteSpace(x.FilePath) ? "" : x.FilePath;
            y.FilePath = string.IsNullOrWhiteSpace(y.FilePath) ? "" : y.FilePath;
            if (x.Message == y.Message &&
                x.Who == y.Who &&
                x.MessageType == y.MessageType &&
                x.UIClassName.Equals(y.UIClassName) &&
                x.FilePath.Equals(y.FilePath))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode([DisallowNull] SimpleMessageBubble obj)
        {
            return $"{obj.Who}-{obj.Message}-{obj.MessageType.ToString()}-{obj.UIClassName}-{(string.IsNullOrWhiteSpace(obj.FilePath) ? "" : obj.FilePath)}".GetHashCode();
        }
    }
}