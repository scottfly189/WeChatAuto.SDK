namespace WxAutoCommon.Enums
{
    /// <summary>
    /// 会话类型
    /// </summary>
    public enum ConversationType
    {
        DontNeedKnown,  //不需要识别
        FileAssistant,  //文件传输助手
        Friend,         //好友
        Group,          //群聊
        OfficialAccount,//公众号
        MiniProgram,    //小程序
        Subscription,   //订阅号
        Service,        //服务号
        Company,        //企业号
        ServiceNotice,  //服务通知
        CollapsedGroupChat, //折叠的群聊
        WxPay,            //微信支付
        TxNew,            //腾讯新闻
    }
}