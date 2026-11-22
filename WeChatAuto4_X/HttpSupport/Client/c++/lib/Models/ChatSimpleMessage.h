
/*
 * ChatSimpleMessage.h
 *
 * 
 */

#ifndef TINY_CPP_CLIENT_ChatSimpleMessage_H_
#define TINY_CPP_CLIENT_ChatSimpleMessage_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 
 *
 *  \ingroup Models
 *
 */

class ChatSimpleMessage{
public:

    /*! \brief Constructor.
	 */
    ChatSimpleMessage();
    ChatSimpleMessage(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~ChatSimpleMessage();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 微信名称
	 */
	std::string getWho();

	/*! \brief Set 微信名称
	 */
	void setWho(std::string who);
	/*! \brief Get 消息
	 */
	std::string getMessage();

	/*! \brief Set 消息
	 */
	void setMessage(std::string message);
	/*! \brief Get 消息日期
	 */
	std::string getSendDateTime();

	/*! \brief Set 消息日期
	 */
	void setSendDateTime(std::string sendDateTime);
	/*! \brief Get 消息日期,日期时间格式
	 */
	std::string getDateTime();

	/*! \brief Set 消息日期,日期时间格式
	 */
	void setDateTime(std::string dateTime);
	/*! \brief Get 唯一字符串
	 */
	std::string getUniqueString();

	/*! \brief Set 唯一字符串
	 */
	void setUniqueString(std::string uniqueString);


    private:
    std::string who{};
    std::string message{};
    std::string sendDateTime{};
    std::string dateTime{};
    std::string uniqueString{};
};
}

#endif /* TINY_CPP_CLIENT_ChatSimpleMessage_H_ */
