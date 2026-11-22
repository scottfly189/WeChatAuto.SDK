#ifndef TINY_CPP_CLIENT_ConversationsApi_H_
#define TINY_CPP_CLIENT_ConversationsApi_H_


#include "Response.h"
#include "Arduino.h"
#include "Service.h"
#include "Helpers.h"
#include <list>

#include "LocateConversationRequest.h"
#include "SearchConversationRequest.h"
#include "SetDoNotDisturbRequest.h"
#include "SetTopMostRequest.h"

namespace Tiny {

/**
 *  Class 
 * Generated with openapi::tiny-cpp-client
 */

class ConversationsApi : public Service {
public:
    ConversationsApi() = default;

    virtual ~ConversationsApi();

    /**
    * 获取会话列表所有会话的标题。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    conversationsGetAllConversations(
            
            std::string clientId
            
    );
    /**
    * 获取可见会话列表（含名称、未读消息数、头像等）。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    conversationsGetVisibleConversations(
            
            std::string clientId
            
    );
    /**
    * 获取会话列表可见会话的标题。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    conversationsGetVisibleTitles(
            
            std::string clientId
            
    );
    /**
    * 定位会话，滚动会话列表使指定会话可见。.
    *
    * 
    * \param clientId  *Required*
    * \param locateConversationRequest 
    */
    Response<
            String
        >
    conversationsLocate(
            
            std::string clientId
            , 
            
            LocateConversationRequest locateConversationRequest
            
    );
    /**
    * 搜索好友/群聊。.
    *
    * 
    * \param clientId  *Required*
    * \param searchConversationRequest 
    */
    Response<
            String
        >
    conversationsSearch(
            
            std::string clientId
            , 
            
            SearchConversationRequest searchConversationRequest
            
    );
    /**
    * 设置会话消息免打扰。.
    *
    * 
    * \param clientId  *Required*
    * \param setDoNotDisturbRequest 
    */
    Response<
            String
        >
    conversationsSetDoNotDisturb(
            
            std::string clientId
            , 
            
            SetDoNotDisturbRequest setDoNotDisturbRequest
            
    );
    /**
    * 设置会话置顶。.
    *
    * 
    * \param clientId  *Required*
    * \param setTopMostRequest 
    */
    Response<
            String
        >
    conversationsSetTopMost(
            
            std::string clientId
            , 
            
            SetTopMostRequest setTopMostRequest
            
    );
}; 

} 

#endif /* TINY_CPP_CLIENT_ConversationsApi_H_ */