using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using OneOf;

namespace WeAutoCommon.Enums
{
    /// <summary>
    /// 通过wxid查询，或者通过手机号查询结果枚举
    /// </summary>
    public enum FriendSearchResultEnums
    {
        [Description("已是好友")]
        Friend = 0,
        [Description("不允许被查询，或者通过手机号查询不到")]
        No_Find = 1,
        [Description("能查到，但不是好友")]
        NotFriend = 2,
    }
}
