using System.Collections.Generic;
using System.Threading.Tasks;
using WeAutoCommon.Enums;

namespace WeAutoCommon.Models
{
    /// <summary>
    /// 标题信息
    /// </summary>
    public class HeaderInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 标题类型,<seealso cref="ChatType"/>
        /// </summary>
        public ChatType HeaderType { get; set; }
        /// <summary>
        /// 如果HeaderType是ChatType.群聊,则显示群聊人数数量，如果不是群聊，这里的数量恒为1
        /// </summary>
        public int ChatNumber { get; set; } = 1;
    }
}