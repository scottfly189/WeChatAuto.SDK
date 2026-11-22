#ifndef TINY_CPP_CLIENT_DatabaseApi_H_
#define TINY_CPP_CLIENT_DatabaseApi_H_


#include "Response.h"
#include "Arduino.h"
#include "Service.h"
#include "Helpers.h"
#include <list>

#include "DatabaseQueryRequest.h"

namespace Tiny {

/**
 *  Class 
 * Generated with openapi::tiny-cpp-client
 */

class DatabaseApi : public Service {
public:
    DatabaseApi() = default;

    virtual ~DatabaseApi();

    /**
    * 按 username（wxid / 群 id）取一条联系人。.
    *
    * 
    * \param clientId  *Required*
    * \param userName  *Required*
    */
    Response<
            String
        >
    databaseGetContact(
            
            std::string clientId
            , 
            
            std::string userName
            
    );
    /**
    * 按显示名取第一条联系人（重名时取 UserName 升序第一条）。.
    *
    * 
    * \param clientId  *Required*
    * \param showName 
    */
    Response<
            String
        >
    databaseGetContactByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
    );
    /**
    * 获取当前微信号全部联系人（不含已删除）。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    databaseGetContacts(
            
            std::string clientId
            
    );
    /**
    * 按显示名取联系人；重名时返回多个。.
    *
    * 
    * \param clientId  *Required*
    * \param showName 
    */
    Response<
            String
        >
    databaseGetContactsByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
    );
    /**
    * 取当前微信号消息的最大 SortSeq（增量水位）。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    databaseGetMaxSortSeq(
            
            std::string clientId
            
    );
    /**
    * 取指定会话最近 N 条消息（从新到旧）。.
    *
    * 
    * \param clientId  *Required*
    * \param chatUsername 
    * \param count 
    */
    Response<
            String
        >
    databaseGetMessages(
            
            std::string clientId
            , 
            
            std::string chatUsername
            , 
            
            int count
            
    );
    /**
    * 取 SortSeq 大于指定值的新消息（从旧到新）。.
    *
    * 
    * \param clientId  *Required*
    * \param afterSortSeq 
    */
    Response<
            String
        >
    databaseGetNewMessages(
            
            std::string clientId
            , 
            
            long afterSortSeq
            
    );
    /**
    * 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。.
    *
    * 
    * \param clientId  *Required*
    * \param chatUsername 
    * \param count 
    */
    Response<
            String
        >
    databaseGetRecentMessages(
            
            std::string clientId
            , 
            
            std::string chatUsername
            , 
            
            int count
            
    );
    /**
    * 按群的显示名取第一条群。.
    *
    * 
    * \param clientId  *Required*
    * \param showName 
    */
    Response<
            String
        >
    databaseGetRoomByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
    );
    /**
    * 按群 username 取成员列表。.
    *
    * 
    * \param clientId  *Required*
    * \param roomUsername  *Required*
    */
    Response<
            String
        >
    databaseGetRoomMembers(
            
            std::string clientId
            , 
            
            std::string roomUsername
            
    );
    /**
    * 按群的显示名取成员列表；重名时取第一条群。.
    *
    * 
    * \param clientId  *Required*
    * \param showName 
    */
    Response<
            String
        >
    databaseGetRoomMembersByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
    );
    /**
    * 获取当前微信号全部群聊。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    databaseGetRooms(
            
            std::string clientId
            
    );
    /**
    * 按联系人 LocalType 过滤群聊（本机观察群多为 2）。.
    *
    * 
    * \param clientId  *Required*
    * \param localType 
    */
    Response<
            String
        >
    databaseGetRoomsByLocalType(
            
            std::string clientId
            , 
            
            int localType
            
    );
    /**
    * 按群的显示名取群；重名时返回多个。.
    *
    * 
    * \param clientId  *Required*
    * \param showName 
    */
    Response<
            String
        >
    databaseGetRoomsByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
    );
    /**
    * 按 tid 取一条朋友圈（含媒体、点赞、评论）。.
    *
    * 
    * \param clientId  *Required*
    * \param tid  *Required*
    */
    Response<
            String
        >
    databaseGetSnsPost(
            
            std::string clientId
            , 
            
            long tid
            
    );
    /**
    * 取最近 N 条朋友圈（含媒体、点赞、评论）。.
    *
    * 
    * \param clientId  *Required*
    * \param limit 
    */
    Response<
            String
        >
    databaseGetSnsPosts(
            
            std::string clientId
            , 
            
            int limit
            
    );
    /**
    * 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。.
    *
    * 
    * \param clientId  *Required*
    * \param date 
    */
    Response<
            String
        >
    databaseGetSnsPostsByDate(
            
            std::string clientId
            , 
            
            std::string date
            
    );
    /**
    * 按发布者显示名取朋友圈；重名会一并返回。.
    *
    * 
    * \param clientId  *Required*
    * \param showName 
    */
    Response<
            String
        >
    databaseGetSnsPostsByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
    );
    /**
    * 取某个发布者（真实 UserName / wxid）的朋友圈。.
    *
    * 
    * \param clientId  *Required*
    * \param userName 
    */
    Response<
            String
        >
    databaseGetSnsPostsByUserName(
            
            std::string clientId
            , 
            
            std::string userName
            
    );
    /**
    * 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。.
    *
    * 
    * \param clientId  *Required*
    * \param databaseQueryRequest 
    */
    Response<
            String
        >
    databaseQuery(
            
            std::string clientId
            , 
            
            DatabaseQueryRequest databaseQueryRequest
            
    );
    /**
    * 按正文关键字跨所有微信号搜索消息。.
    *
    * 
    * \param clientId  *Required*
    * \param keyword 
    * \param chatUsername 
    */
    Response<
            String
        >
    databaseSearchAllMessages(
            
            std::string clientId
            , 
            
            std::string keyword
            , 
            
            std::string chatUsername
            
    );
    /**
    * 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。.
    *
    * 
    * \param clientId  *Required*
    * \param keyword 
    */
    Response<
            String
        >
    databaseSearchContacts(
            
            std::string clientId
            , 
            
            std::string keyword
            
    );
    /**
    * 按正文关键字搜索当前微信号消息。.
    *
    * 
    * \param clientId  *Required*
    * \param keyword 
    * \param chatUsername 
    */
    Response<
            String
        >
    databaseSearchMessages(
            
            std::string clientId
            , 
            
            std::string keyword
            , 
            
            std::string chatUsername
            
    );
}; 

} 

#endif /* TINY_CPP_CLIENT_DatabaseApi_H_ */