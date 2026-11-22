
/*
 * AddGroupMemberToFriendsRequest.h
 *
 * 从群聊中添加好友。
 */

#ifndef TINY_CPP_CLIENT_AddGroupMemberToFriendsRequest_H_
#define TINY_CPP_CLIENT_AddGroupMemberToFriendsRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "AddFriendsOptions.h"
#include <list>

namespace Tiny {


/*! \brief 从群聊中添加好友。
 *
 *  \ingroup Models
 *
 */

class AddGroupMemberToFriendsRequest{
public:

    /*! \brief Constructor.
	 */
    AddGroupMemberToFriendsRequest();
    AddGroupMemberToFriendsRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~AddGroupMemberToFriendsRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getGroupName();

	/*! \brief Set 
	 */
	void setGroupName(std::string group_name);
	/*! \brief Get 
	 */
	std::list<std::string> getMembers();

	/*! \brief Set 
	 */
	void setMembers(std::list<std::string> members);
	/*! \brief Get 
	 */
	AddFriendsOptions getOptions();

	/*! \brief Set 
	 */
	void setOptions(AddFriendsOptions options);


    private:
    std::string group_name{};
    std::list<std::string> members;
    AddFriendsOptions options;
};
}

#endif /* TINY_CPP_CLIENT_AddGroupMemberToFriendsRequest_H_ */
