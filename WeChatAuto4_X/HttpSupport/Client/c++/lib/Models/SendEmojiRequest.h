
/*
 * SendEmojiRequest.h
 *
 * 发送表情。emoji 与 index 二选一（优先使用 index）。
 */

#ifndef TINY_CPP_CLIENT_SendEmojiRequest_H_
#define TINY_CPP_CLIENT_SendEmojiRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include <list>

namespace Tiny {


/*! \brief 发送表情。emoji 与 index 二选一（优先使用 index）。
 *
 *  \ingroup Models
 *
 */

class SendEmojiRequest{
public:

    /*! \brief Constructor.
	 */
    SendEmojiRequest();
    SendEmojiRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendEmojiRequest();


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
	/*! \brief Get 表情名称或描述。
	 */
	std::string getEmoji();

	/*! \brief Set 表情名称或描述。
	 */
	void setEmoji(std::string emoji);
	/*! \brief Get 表情索引。
	 */
	int getIndex();

	/*! \brief Set 表情索引。
	 */
	void setIndex(int index);
	/*! \brief Get 
	 */
	std::list<std::string> getAtUsers();

	/*! \brief Set 
	 */
	void setAtUsers(std::list<std::string> at_users);


    private:
    std::string who{};
    std::string emoji{};
    int index{};
    std::list<std::string> at_users;
};
}

#endif /* TINY_CPP_CLIENT_SendEmojiRequest_H_ */
