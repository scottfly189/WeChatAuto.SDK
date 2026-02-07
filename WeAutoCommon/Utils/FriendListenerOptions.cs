namespace WeAutoCommon.Utils
{
    public class FriendListenerOptions
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 添加好友成功后是否删除好友申请按钮，默认删除
        /// </summary>
        public bool IsDelet { get; set; } = true;
    }
}