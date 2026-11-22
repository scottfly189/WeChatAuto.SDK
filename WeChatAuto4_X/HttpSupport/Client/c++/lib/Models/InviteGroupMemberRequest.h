
/*
 * InviteGroupMemberRequest.h
 *
 * 邀请群成员（外部群）。
 */

#ifndef TINY_CPP_CLIENT_InviteGroupMemberRequest_H_
#define TINY_CPP_CLIENT_InviteGroupMemberRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include <list>

namespace Tiny {


/*! \brief 邀请群成员（外部群）。
 *
 *  \ingroup Models
 *
 */

class InviteGroupMemberRequest{
public:

    /*! \brief Constructor.
	 */
    InviteGroupMemberRequest();
    InviteGroupMemberRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~InviteGroupMemberRequest();


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
	std::string getInviteReason();

	/*! \brief Set 
	 */
	void setInviteReason(std::string invite_reason);


    private:
    std::string group_name{};
    std::list<std::string> members;
    std::string invite_reason{};
};
}

#endif /* TINY_CPP_CLIENT_InviteGroupMemberRequest_H_ */
