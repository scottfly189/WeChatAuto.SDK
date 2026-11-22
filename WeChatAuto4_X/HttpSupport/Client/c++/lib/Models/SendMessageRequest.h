
/*
 * SendMessageRequest.h
 *
 * 发送文本消息。who 为空时发送给当前焦点聊天窗口。
 */

#ifndef TINY_CPP_CLIENT_SendMessageRequest_H_
#define TINY_CPP_CLIENT_SendMessageRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "ChatRefer.h"
#include <list>

namespace Tiny {


/*! \brief 发送文本消息。who 为空时发送给当前焦点聊天窗口。
 *
 *  \ingroup Models
 *
 */

class SendMessageRequest{
public:

    /*! \brief Constructor.
	 */
    SendMessageRequest();
    SendMessageRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendMessageRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getWho();

	/*! \brief Set 
	 */
	void setWho(std::string who);
	/*! \brief Get 
	 */
	std::string getMessage();

	/*! \brief Set 
	 */
	void setMessage(std::string message);
	/*! \brief Get 被 @ 的好友列表。
	 */
	std::list<std::string> getAtUsers();

	/*! \brief Set 被 @ 的好友列表。
	 */
	void setAtUsers(std::list<std::string> at_users);
	/*! \brief Get 
	 */
	ChatRefer getRefer();

	/*! \brief Set 
	 */
	void setRefer(ChatRefer refer);


    private:
    std::string who{};
    std::string message{};
    std::list<std::string> at_users;
    ChatRefer refer;
};
}

#endif /* TINY_CPP_CLIENT_SendMessageRequest_H_ */
