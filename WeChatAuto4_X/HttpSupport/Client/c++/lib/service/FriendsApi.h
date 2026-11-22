#ifndef TINY_CPP_CLIENT_FriendsApi_H_
#define TINY_CPP_CLIENT_FriendsApi_H_


#include "Response.h"
#include "Arduino.h"
#include "Service.h"
#include "Helpers.h"
#include <list>

#include "AddFriendsRequest.h"
#include "FriendInfo.h"
#include "PassedAllNewFriendRequest.h"

namespace Tiny {

/**
 *  Class 
 * Generated with openapi::tiny-cpp-client
 */

class FriendsApi : public Service {
public:
    FriendsApi() = default;

    virtual ~FriendsApi();

    /**
    * 通过手机号码/微信号查找并添加好友。.
    *
    * 
    * \param clientId  *Required*
    * \param addFriendsRequest 
    */
    Response<
            String
        >
    friendsAdd(
            
            std::string clientId
            , 
            
            AddFriendsRequest addFriendsRequest
            
    );
    /**
    * 手动新增或修改缓存中的好友对象。.
    *
    * 
    * \param clientId  *Required*
    * \param friendInfo 
    */
    Response<
            String
        >
    friendsAddOrUpdateFriend(
            
            std::string clientId
            , 
            
            FriendInfo friendInfo
            
    );
    /**
    * 关闭新增朋友窗口。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    friendsCloseAddWindow(
            
            std::string clientId
            
    );
    /**
    * 获取所有好友名称列表（有备注优先备注名）。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    friendsGetAllFriendNames(
            
            std::string clientId
            
    );
    /**
    * 获取所有好友的信息列表（默认优先从缓存读取）。.
    *
    * 
    * \param clientId  *Required*
    * \param fromCache 
    */
    Response<
            String
        >
    friendsGetAllFriends(
            
            std::string clientId
            , 
            
            bool fromCache
            
    );
    /**
    * 按 wxid 从缓存获取好友信息。.
    *
    * 
    * \param clientId  *Required*
    * \param wxid  *Required*
    */
    Response<
            String
        >
    friendsGetFriendByWxidFromCache(
            
            std::string clientId
            , 
            
            std::string wxid
            
    );
    /**
    * 按名称从缓存获取单个好友信息。.
    *
    * 
    * \param clientId  *Required*
    * \param who  *Required*
    */
    Response<
            String
        >
    friendsGetFriendFromCache(
            
            std::string clientId
            , 
            
            std::string who
            
    );
    /**
    * 获取缓存中的全部好友信息列表。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    friendsGetFriendListFromCache(
            
            std::string clientId
            
    );
    /**
    * 按名称从缓存获取好友列表（适用于昵称重复场景）。.
    *
    * 
    * \param clientId  *Required*
    * \param who  *Required*
    */
    Response<
            String
        >
    friendsGetFriendsFromCache(
            
            std::string clientId
            , 
            
            std::string who
            
    );
    /**
    * 打开新增朋友窗口。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    friendsOpenAddWindow(
            
            std::string clientId
            
    );
    /**
    * 立即批量通过当前待处理的新好友申请（不启动持续监听）。.
    *
    * 
    * \param clientId  *Required*
    * \param passedAllNewFriendRequest 
    */
    Response<
            String
        >
    friendsPassAll(
            
            std::string clientId
            , 
            
            PassedAllNewFriendRequest passedAllNewFriendRequest
            
    );
    /**
    * 暂停新好友自动通过监听。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    friendsPauseAutoAccept(
            
            std::string clientId
            
    );
    /**
    * 从通讯录移除好友。.
    *
    * 
    * \param clientId  *Required*
    * \param nickName  *Required*
    */
    Response<
            String
        >
    friendsRemoveFriend(
            
            std::string clientId
            , 
            
            std::string nickName
            
    );
    /**
    * 按 wxid 从缓存移除好友。.
    *
    * 
    * \param clientId  *Required*
    * \param wxid  *Required*
    */
    Response<
            String
        >
    friendsRemoveFriendByWxidFromCache(
            
            std::string clientId
            , 
            
            std::string wxid
            
    );
    /**
    * 按名称从缓存移除好友。.
    *
    * 
    * \param clientId  *Required*
    * \param who  *Required*
    */
    Response<
            String
        >
    friendsRemoveFriendFromCache(
            
            std::string clientId
            , 
            
            std::string who
            
    );
    /**
    * 恢复新好友自动通过监听。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    friendsResumeAutoAccept(
            
            std::string clientId
            
    );
    /**
    * 启动新好友自动通过监听（按关键词/后缀/标签规则）。.
    *
    * 
    * \param clientId  *Required*
    * \param passedAllNewFriendRequest 
    */
    Response<
            String
        >
    friendsStartAutoAccept(
            
            std::string clientId
            , 
            
            PassedAllNewFriendRequest passedAllNewFriendRequest
            
    );
    /**
    * 停止新好友自动通过监听。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    friendsStopAutoAccept(
            
            std::string clientId
            
    );
}; 

} 

#endif /* TINY_CPP_CLIENT_FriendsApi_H_ */