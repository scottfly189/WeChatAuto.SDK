namespace WxAutoCommon.Models
{
    public class ChatGroupOptions
    {
        /// <summary>
        /// 群聊名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 是否显示群组昵称
        /// </summary>
        public bool ShowGroupNickName { get; set; } = true;

        /// <summary>
        /// 是否免打扰
        /// </summary>
        public bool NoDisturb { get; set; } = false;
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool Top { get; set; } = false;
        /// <summary>
        /// 是否保存至通讯录
        /// </summary>
        public bool SaveToAddressBook { get; set; } = false;

        /// <summary>
        /// 群公告
        /// </summary>
        public string GroupNotice { get; set; }
        /// <summary>
        /// 我在群里的昵称
        /// </summary>
        public string MyGroupNickName { get; set; }
        /// <summary>
        /// 群备注
        /// </summary>
        public string GroupMemo { get; set; }

    }
}