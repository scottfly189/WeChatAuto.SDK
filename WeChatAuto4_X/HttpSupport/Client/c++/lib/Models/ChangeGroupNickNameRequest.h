
/*
 * ChangeGroupNickNameRequest.h
 *
 * 修改自己在群中的昵称。
 */

#ifndef TINY_CPP_CLIENT_ChangeGroupNickNameRequest_H_
#define TINY_CPP_CLIENT_ChangeGroupNickNameRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 修改自己在群中的昵称。
 *
 *  \ingroup Models
 *
 */

class ChangeGroupNickNameRequest{
public:

    /*! \brief Constructor.
	 */
    ChangeGroupNickNameRequest();
    ChangeGroupNickNameRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~ChangeGroupNickNameRequest();


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
	std::string getNickName();

	/*! \brief Set 
	 */
	void setNickName(std::string nick_name);


    private:
    std::string group_name{};
    std::string nick_name{};
};
}

#endif /* TINY_CPP_CLIENT_ChangeGroupNickNameRequest_H_ */
