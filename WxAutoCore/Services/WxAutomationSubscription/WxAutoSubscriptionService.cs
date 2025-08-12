using System.Collections;
using System.Collections.Generic;
using WxAutoCore.Interface;

namespace WxAutoCore.Services.WxAutomationSubscription
{
    /// <summary>
    /// 微信自动化订阅服务，用于订阅微信自动化服务
    /// </summary>
    public class WxAutoSubscriptionService
    {
        private readonly IList<IWxAuto> _wxAutoActions = new List<IWxAuto>();

        /// <summary>
        /// 获取微信自动化服务
        /// </summary>
        /// <returns></returns>
        public IList<IWxAuto> GetWxAuto()
        {
            return _wxAutoActions;
        }

        /// <summary>
        /// 添加微信自动化服务
        /// </summary>
        /// <param name="wxAuto"></param>
        public void AddWxAuto(IWxAuto wxAuto)
        {
            _wxAutoActions.Add(wxAuto);
        }

        /// <summary>
        /// 移除微信自动化服务
        /// </summary>
        /// <param name="wxAuto"></param>
        public void RemoveWxAuto(IWxAuto wxAuto)
        {
            _wxAutoActions.Remove(wxAuto);
        }
    }
}