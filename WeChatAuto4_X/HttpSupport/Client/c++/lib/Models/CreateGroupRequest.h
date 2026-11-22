
/*
 * CreateGroupRequest.h
 *
 * 创建群聊。
 */

#ifndef TINY_CPP_CLIENT_CreateGroupRequest_H_
#define TINY_CPP_CLIENT_CreateGroupRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include <list>

namespace Tiny {


/*! \brief 创建群聊。
 *
 *  \ingroup Models
 *
 */

class CreateGroupRequest{
public:

    /*! \brief Constructor.
	 */
    CreateGroupRequest();
    CreateGroupRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~CreateGroupRequest();


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
	std::string getFirstWho();

	/*! \brief Set 
	 */
	void setFirstWho(std::string first_who);
	/*! \brief Get 
	 */
	std::list<std::string> getMembers();

	/*! \brief Set 
	 */
	void setMembers(std::list<std::string> members);


    private:
    std::string group_name{};
    std::string first_who{};
    std::list<std::string> members;
};
}

#endif /* TINY_CPP_CLIENT_CreateGroupRequest_H_ */
