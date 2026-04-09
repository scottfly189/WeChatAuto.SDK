using System;
using System.Collections.Generic;
using System.Linq;

namespace WeAutoCommon.Models
{
    /// <summary>
    /// 个人信息
    /// </summary>
    public class FriendInfo
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 微信ID
        /// </summary>
        public string WxId { get; set; }

        public override string ToString()
        {
            return $"NickName={NickName} - WxId={WxId}";
        }
    }
}