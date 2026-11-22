
/*
 * SendMessageByWxidRequest.h
 *
 * 按联系人 UserName（wxid / 群 id）发送文本。user_name 必填。
 */

#ifndef TINY_CPP_CLIENT_SendMessageByWxidRequest_H_
#define TINY_CPP_CLIENT_SendMessageByWxidRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "ChatRefer.h"
#include <list>

namespace Tiny {


/*! \brief 按联系人 UserName（wxid / 群 id）发送文本。user_name 必填。
 *
 *  \ingroup Models
 *
 */

class SendMessageByWxidRequest{
public:

    /*! \brief Constructor.
	 */
    SendMessageByWxidRequest();
    SendMessageByWxidRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendMessageByWxidRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getUserName();

	/*! \brief Set 
	 */
	void setUserName(std::string user_name);
	/*! \brief Get 
	 */
	std::string getMessage();

	/*! \brief Set 
	 */
	void setMessage(std::string message);
	/*! \brief Get 
	 */
	std::list<std::string> getAtUsers();

	/*! \brief Set 
	 */
	void setAtUsers(std::list<std::string> at_users);
	/*! \brief Get 
	 */
	ChatRefer getRefer();

	/*! \brief Set 
	 */
	void setRefer(ChatRefer refer);


    private:
    std::string user_name{};
    std::string message{};
    std::list<std::string> at_users;
    ChatRefer refer;
};
}

#endif /* TINY_CPP_CLIENT_SendMessageByWxidRequest_H_ */
