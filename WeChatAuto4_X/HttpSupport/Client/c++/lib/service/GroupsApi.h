#ifndef TINY_CPP_CLIENT_GroupsApi_H_
#define TINY_CPP_CLIENT_GroupsApi_H_


#include "Response.h"
#include "Arduino.h"
#include "Service.h"
#include "Helpers.h"
#include <list>

#include "AddGroupMemberToFriendsRequest.h"
#include "ChangeGroupMemoRequest.h"
#include "ChangeGroupNickNameRequest.h"
#include "CreateGroupRequest.h"
#include "GroupMemberRequest.h"
#include "InviteGroupMemberRequest.h"
#include "RenameGroupRequest.h"
#include "UpdateGroupNoticeRequest.h"

namespace Tiny {

/**
 *  Class 
 * Generated with openapi::tiny-cpp-client
 */

class GroupsApi : public Service {
public:
    GroupsApi() = default;

    virtual ~GroupsApi();

    /**
    * 添加群聊成员（适用于自有群）。.
    *
    * 
    * \param clientId  *Required*
    * \param groupMemberRequest 
    */
    Response<
            String
        >
    groupsAddMembers(
            
            std::string clientId
            , 
            
            GroupMemberRequest groupMemberRequest
            
    );
    /**
    * 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。.
    *
    * 
    * \param clientId  *Required*
    * \param addGroupMemberToFriendsRequest 
    */
    Response<
            String
        >
    groupsAddMembersToFriends(
            
            std::string clientId
            , 
            
            AddGroupMemberToFriendsRequest addGroupMemberToFriendsRequest
            
    );
    /**
    * 修改群备注（仅自己可见）。.
    *
    * 
    * \param clientId  *Required*
    * \param changeGroupMemoRequest 
    */
    Response<
            String
        >
    groupsChangeMemo(
            
            std::string clientId
            , 
            
            ChangeGroupMemoRequest changeGroupMemoRequest
            
    );
    /**
    * 修改群名（适用于自有群）。.
    *
    * 
    * \param clientId  *Required*
    * \param renameGroupRequest 
    */
    Response<
            String
        >
    groupsChangeName(
            
            std::string clientId
            , 
            
            RenameGroupRequest renameGroupRequest
            
    );
    /**
    * 修改自己在群中的昵称。.
    *
    * 
    * \param clientId  *Required*
    * \param changeGroupNickNameRequest 
    */
    Response<
            String
        >
    groupsChangeNickName(
            
            std::string clientId
            , 
            
            ChangeGroupNickNameRequest changeGroupNickNameRequest
            
    );
    /**
    * 创建群聊，已存在则打开群聊。.
    *
    * 
    * \param clientId  *Required*
    * \param createGroupRequest 
    */
    Response<
            String
        >
    groupsCreate(
            
            std::string clientId
            , 
            
            CreateGroupRequest createGroupRequest
            
    );
    /**
    * 获取群聊成员列表。.
    *
    * 
    * \param clientId  *Required*
    * \param groupName 
    */
    Response<
            String
        >
    groupsGetMembers(
            
            std::string clientId
            , 
            
            std::string groupName
            
    );
    /**
    * 获取群主昵称。.
    *
    * 
    * \param clientId  *Required*
    * \param groupName  *Required*
    */
    Response<
            String
        >
    groupsGetOwner(
            
            std::string clientId
            , 
            
            std::string groupName
            
    );
    /**
    * 邀请群聊成员（适用于外部群）。.
    *
    * 
    * \param clientId  *Required*
    * \param inviteGroupMemberRequest 
    */
    Response<
            String
        >
    groupsInvite(
            
            std::string clientId
            , 
            
            InviteGroupMemberRequest inviteGroupMemberRequest
            
    );
    /**
    * 判断群聊是否为自有群。.
    *
    * 
    * \param clientId  *Required*
    * \param groupName  *Required*
    */
    Response<
            String
        >
    groupsIsOwner(
            
            std::string clientId
            , 
            
            std::string groupName
            
    );
    /**
    * 退出群聊。.
    *
    * 
    * \param clientId  *Required*
    * \param groupName  *Required*
    * \param clearHistory 
    */
    Response<
            String
        >
    groupsQuit(
            
            std::string clientId
            , 
            
            std::string groupName
            , 
            
            bool clearHistory
            
    );
    /**
    * 移除群聊成员（适用于自有群）。.
    *
    * 
    * \param clientId  *Required*
    * \param groupMemberRequest 
    */
    Response<
            String
        >
    groupsRemoveMembers(
            
            std::string clientId
            , 
            
            GroupMemberRequest groupMemberRequest
            
    );
    /**
    * 更新群公告（仅适用于自有群）。.
    *
    * 
    * \param clientId  *Required*
    * \param updateGroupNoticeRequest 
    */
    Response<
            String
        >
    groupsUpdateNotice(
            
            std::string clientId
            , 
            
            UpdateGroupNoticeRequest updateGroupNoticeRequest
            
    );
}; 

} 

#endif /* TINY_CPP_CLIENT_GroupsApi_H_ */