using System.Collections.Generic;
using WxAutoCommon.Models;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 朋友圈上下文
    /// 最主要提供给终端用户使用，用于获取朋友圈内容列表，并进行点赞、回复评论等操作
    /// </summary>
    public class MomentsContext
    {
        private readonly AutoLogger<MomentsContext> _logger;
        private readonly List<MomentItem> _momentsList;
        public List<MomentItem> MomentsList => _momentsList;
        public MomentsContext(AutoLogger<MomentsContext> logger, List<MomentItem> momentsList)
        {
            this._logger = logger;
            this._momentsList = momentsList;
        }


    }
}