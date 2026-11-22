
/*
 * GroupMemberRequest.h
 *
 * 添加/移除群成员。group_name 为空时作用于当前焦点群聊。
 */

#ifndef TINY_CPP_CLIENT_GroupMemberRequest_H_
#define TINY_CPP_CLIENT_GroupMemberRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include <list>

namespace Tiny {


/*! \brief 添加/移除群成员。group_name 为空时作用于当前焦点群聊。
 *
 *  \ingroup Models
 *
 */

class GroupMemberRequest{
public:

    /*! \brief Constructor.
	 */
    GroupMemberRequest();
    GroupMemberRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~GroupMemberRequest();


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


    private:
    std::string group_name{};
    std::list<std::string> members;
};
}

#endif /* TINY_CPP_CLIENT_GroupMemberRequest_H_ */
