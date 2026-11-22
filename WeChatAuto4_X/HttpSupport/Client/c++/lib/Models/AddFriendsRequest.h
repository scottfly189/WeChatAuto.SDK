
/*
 * AddFriendsRequest.h
 *
 * 通过手机号码或微信号添加好友。
 */

#ifndef TINY_CPP_CLIENT_AddFriendsRequest_H_
#define TINY_CPP_CLIENT_AddFriendsRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "AddFriendsOptions.h"
#include <list>

namespace Tiny {


/*! \brief 通过手机号码或微信号添加好友。
 *
 *  \ingroup Models
 *
 */

class AddFriendsRequest{
public:

    /*! \brief Constructor.
	 */
    AddFriendsRequest();
    AddFriendsRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~AddFriendsRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::list<std::string> getFriends();

	/*! \brief Set 
	 */
	void setFriends(std::list<std::string> friends);
	/*! \brief Get 
	 */
	AddFriendsOptions getOptions();

	/*! \brief Set 
	 */
	void setOptions(AddFriendsOptions options);


    private:
    std::list<std::string> friends;
    AddFriendsOptions options;
};
}

#endif /* TINY_CPP_CLIENT_AddFriendsRequest_H_ */
