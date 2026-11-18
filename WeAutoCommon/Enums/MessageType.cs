namespace WeAutoCommon.Enums
{
    /// <summary>
    /// 聊天消息类型
    /// </summary>
    public enum MessageType
    {
        系统,
        文字,
        图片,
        语音,
        视频,
        文件,
        聊天记录,
        位置,
        个人名片,
        视频号,
        视频号直播,
        红包,  
        微信转账,
        小程序,
        链接,
        表情,
        动画表情,
        时间,    //不需要处理
        引用,  //不需要，原样返回使用者，由使用者自行分析
        笔记,
        拍一拍,
        撤回消息,
        语音通话,
        其他,
        None,   //无
    }
}