#ifndef TINY_CPP_CLIENT_MessagesApi_H_
#define TINY_CPP_CLIENT_MessagesApi_H_


#include "Response.h"
#include "Arduino.h"
#include "Service.h"
#include "Helpers.h"
#include <list>

#include "CloneVoiceRequest.h"
#include "ForwardMultipleMessageRequest.h"
#include "ForwardSingleMessageRequest.h"
#include "PullMessagesRequest.h"
#include "ReferenceRequest.h"
#include "SendEmojiByWxidRequest.h"
#include "SendEmojiRequest.h"
#include "SendFileByWxidRequest.h"
#include "SendFileRequest.h"
#include "SendMessageByWxidRequest.h"
#include "SendMessageRequest.h"
#include "SendVoiceChatsRequest.h"
#include "SendVoiceMessageByWxidRequest.h"
#include "SendVoiceMessageRequest.h"
#include "SendVoiceMessageTtsByWxidRequest.h"
#include "SendVoiceMessageTtsRequest.h"
#include "TapRequest.h"
#include "WhoRequest.h"

namespace Tiny {

/**
 *  Class 
 * Generated with openapi::tiny-cpp-client
 */

class MessagesApi : public Service {
public:
    MessagesApi() = default;

    virtual ~MessagesApi();

    /**
    * 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。.
    *
    * 
    * \param clientId  *Required*
    * \param cloneVoiceRequest 
    */
    Response<
            String
        >
    messagesCloneVoice(
            
            std::string clientId
            , 
            
            CloneVoiceRequest cloneVoiceRequest
            
    );
    /**
    * 聚焦当前窗口的消息输入区。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    messagesFocusInput(
            
            std::string clientId
            
    );
    /**
    * 转发多条消息（默认合并转发）。.
    *
    * 
    * \param clientId  *Required*
    * \param forwardMultipleMessageRequest 
    */
    Response<
            String
        >
    messagesForwardMultiple(
            
            std::string clientId
            , 
            
            ForwardMultipleMessageRequest forwardMultipleMessageRequest
            
    );
    /**
    * 转发单条消息（message 与 chat_simple_message 二选一）。.
    *
    * 
    * \param clientId  *Required*
    * \param forwardSingleMessageRequest 
    */
    Response<
            String
        >
    messagesForwardSingle(
            
            std::string clientId
            , 
            
            ForwardSingleMessageRequest forwardSingleMessageRequest
            
    );
    /**
    * 获取聊天历史（支持按日期或时间段查询）。.
    *
    * 
    * \param clientId  *Required*
    * \param who 
    * \param date 
    * \param startDate 
    * \param endDate 
    */
    Response<
            String
        >
    messagesGetHistory(
            
            std::string clientId
            , 
            
            std::string who
            , 
            
            std::string date
            , 
            
            std::string startDate
            , 
            
            std::string endDate
            
    );
    /**
    * 获取当前聊天窗口的标题名称。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    messagesGetOnlyTitle(
            
            std::string clientId
            
    );
    /**
    * 获取当前聊天窗口的标题对象。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    messagesGetTitle(
            
            std::string clientId
            
    );
    /**
    * 轮询拉取指定会话的新消息（用于自己实现轮询）。.
    *
    * 
    * \param clientId  *Required*
    * \param pullMessagesRequest 
    */
    Response<
            String
        >
    messagesPull(
            
            std::string clientId
            , 
            
            PullMessagesRequest pullMessagesRequest
            
    );
    /**
    * 引用当前窗口的好友消息。.
    *
    * 
    * \param clientId  *Required*
    * \param referenceRequest 
    */
    Response<
            String
        >
    messagesReference(
            
            std::string clientId
            , 
            
            ReferenceRequest referenceRequest
            
    );
    /**
    * 引用当前窗口最后一条消息。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    messagesReferenceLast(
            
            std::string clientId
            
    );
    /**
    * 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。.
    *
    * 
    * \param clientId  *Required*
    * \param sendMessageRequest 
    */
    Response<
            String
        >
    messagesSend(
            
            std::string clientId
            , 
            
            SendMessageRequest sendMessageRequest
            
    );
    /**
    * 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。.
    *
    * 
    * \param clientId  *Required*
    * \param sendMessageRequest 
    */
    Response<
            String
        >
    messagesSendAndClose(
            
            std::string clientId
            , 
            
            SendMessageRequest sendMessageRequest
            
    );
    /**
    * 按联系人 UserName（wxid / 群 id）发送文本。.
    *
    * 
    * \param clientId  *Required*
    * \param sendMessageByWxidRequest 
    */
    Response<
            String
        >
    messagesSendByWxid(
            
            std::string clientId
            , 
            
            SendMessageByWxidRequest sendMessageByWxidRequest
            
    );
    /**
    * 发送表情（按名称/描述/索引）。.
    *
    * 
    * \param clientId  *Required*
    * \param sendEmojiRequest 
    */
    Response<
            String
        >
    messagesSendEmoji(
            
            std::string clientId
            , 
            
            SendEmojiRequest sendEmojiRequest
            
    );
    /**
    * 发送表情后关闭微信窗口（按名称/描述/索引）。.
    *
    * 
    * \param clientId  *Required*
    * \param sendEmojiRequest 
    */
    Response<
            String
        >
    messagesSendEmojiAndClose(
            
            std::string clientId
            , 
            
            SendEmojiRequest sendEmojiRequest
            
    );
    /**
    * 按联系人 UserName（wxid / 群 id）发送表情。.
    *
    * 
    * \param clientId  *Required*
    * \param sendEmojiByWxidRequest 
    */
    Response<
            String
        >
    messagesSendEmojiByWxid(
            
            std::string clientId
            , 
            
            SendEmojiByWxidRequest sendEmojiByWxidRequest
            
    );
    /**
    * 发送文件。.
    *
    * 
    * \param clientId  *Required*
    * \param sendFileRequest 
    */
    Response<
            String
        >
    messagesSendFile(
            
            std::string clientId
            , 
            
            SendFileRequest sendFileRequest
            
    );
    /**
    * 发送文件后关闭微信窗口。.
    *
    * 
    * \param clientId  *Required*
    * \param sendFileRequest 
    */
    Response<
            String
        >
    messagesSendFileAndClose(
            
            std::string clientId
            , 
            
            SendFileRequest sendFileRequest
            
    );
    /**
    * 按联系人 UserName（wxid / 群 id）发送文件。.
    *
    * 
    * \param clientId  *Required*
    * \param sendFileByWxidRequest 
    */
    Response<
            String
        >
    messagesSendFileByWxid(
            
            std::string clientId
            , 
            
            SendFileByWxidRequest sendFileByWxidRequest
            
    );
    /**
    * 发起单人视频聊天。.
    *
    * 
    * \param clientId  *Required*
    * \param whoRequest 
    */
    Response<
            String
        >
    messagesSendVideoChat(
            
            std::string clientId
            , 
            
            WhoRequest whoRequest
            
    );
    /**
    * 发起单人语音聊天。.
    *
    * 
    * \param clientId  *Required*
    * \param whoRequest 
    */
    Response<
            String
        >
    messagesSendVoiceChat(
            
            std::string clientId
            , 
            
            WhoRequest whoRequest
            
    );
    /**
    * 发起多人语音聊天（群聊）。.
    *
    * 
    * \param clientId  *Required*
    * \param sendVoiceChatsRequest 
    */
    Response<
            String
        >
    messagesSendVoiceChats(
            
            std::string clientId
            , 
            
            SendVoiceChatsRequest sendVoiceChatsRequest
            
    );
    /**
    * 发送语音消息（需虚拟声卡 Cable input/Cable output）。.
    *
    * 
    * \param clientId  *Required*
    * \param sendVoiceMessageRequest 
    */
    Response<
            String
        >
    messagesSendVoiceMessage(
            
            std::string clientId
            , 
            
            SendVoiceMessageRequest sendVoiceMessageRequest
            
    );
    /**
    * 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。.
    *
    * 
    * \param clientId  *Required*
    * \param sendVoiceMessageRequest 
    */
    Response<
            String
        >
    messagesSendVoiceMessageAndClose(
            
            std::string clientId
            , 
            
            SendVoiceMessageRequest sendVoiceMessageRequest
            
    );
    /**
    * 按联系人 UserName（wxid / 群 id）发送语音文件。.
    *
    * 
    * \param clientId  *Required*
    * \param sendVoiceMessageByWxidRequest 
    */
    Response<
            String
        >
    messagesSendVoiceMessageByWxid(
            
            std::string clientId
            , 
            
            SendVoiceMessageByWxidRequest sendVoiceMessageByWxidRequest
            
    );
    /**
    * 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。.
    *
    * 
    * \param clientId  *Required*
    * \param sendVoiceMessageTtsRequest 
    */
    Response<
            String
        >
    messagesSendVoiceMessageTts(
            
            std::string clientId
            , 
            
            SendVoiceMessageTtsRequest sendVoiceMessageTtsRequest
            
    );
    /**
    * 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。.
    *
    * 
    * \param clientId  *Required*
    * \param sendVoiceMessageTtsRequest 
    */
    Response<
            String
        >
    messagesSendVoiceMessageTtsAndClose(
            
            std::string clientId
            , 
            
            SendVoiceMessageTtsRequest sendVoiceMessageTtsRequest
            
    );
    /**
    * 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。.
    *
    * 
    * \param clientId  *Required*
    * \param sendVoiceMessageTtsByWxidRequest 
    */
    Response<
            String
        >
    messagesSendVoiceMessageTtsByWxid(
            
            std::string clientId
            , 
            
            SendVoiceMessageTtsByWxidRequest sendVoiceMessageTtsByWxidRequest
            
    );
    /**
    * 拍一拍当前聊天窗口的好友。.
    *
    * 
    * \param clientId  *Required*
    * \param tapRequest 
    */
    Response<
            String
        >
    messagesTap(
            
            std::string clientId
            , 
            
            TapRequest tapRequest
            
    );
}; 

} 

#endif /* TINY_CPP_CLIENT_MessagesApi_H_ */