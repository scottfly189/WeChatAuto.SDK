
/*
 * SendEmojiByWxidRequest.h
 *
 * 按联系人 UserName 发送表情。emoji 与 index 二选一（优先 index）。
 */

#ifndef TINY_CPP_CLIENT_SendEmojiByWxidRequest_H_
#define TINY_CPP_CLIENT_SendEmojiByWxidRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include <list>

namespace Tiny {


/*! \brief 按联系人 UserName 发送表情。emoji 与 index 二选一（优先 index）。
 *
 *  \ingroup Models
 *
 */

class SendEmojiByWxidRequest{
public:

    /*! \brief Constructor.
	 */
    SendEmojiByWxidRequest();
    SendEmojiByWxidRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendEmojiByWxidRequest();


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
	std::string getEmoji();

	/*! \brief Set 
	 */
	void setEmoji(std::string emoji);
	/*! \brief Get 
	 */
	int getIndex();

	/*! \brief Set 
	 */
	void setIndex(int index);
	/*! \brief Get 
	 */
	std::list<std::string> getAtUsers();

	/*! \brief Set 
	 */
	void setAtUsers(std::list<std::string> at_users);


    private:
    std::string user_name{};
    std::string emoji{};
    int index{};
    std::list<std::string> at_users;
};
}

#endif /* TINY_CPP_CLIENT_SendEmojiByWxidRequest_H_ */
