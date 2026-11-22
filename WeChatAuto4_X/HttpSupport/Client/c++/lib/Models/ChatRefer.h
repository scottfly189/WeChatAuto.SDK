
/*
 * ChatRefer.h
 *
 * 消息引用
 */

#ifndef TINY_CPP_CLIENT_ChatRefer_H_
#define TINY_CPP_CLIENT_ChatRefer_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "ChatSimpleMessage.h"
#include "Date.h"

namespace Tiny {


/*! \brief 消息引用
 *
 *  \ingroup Models
 *
 */

class ChatRefer{
public:

    /*! \brief Constructor.
	 */
    ChatRefer();
    ChatRefer(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~ChatRefer();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 日期,如果不设置则不进行日期筛选
	 */
	Date getDate();

	/*! \brief Set 日期,如果不设置则不进行日期筛选
	 */
	void setDate(Date date);
	/*! \brief Get 
	 */
	ChatSimpleMessage getMessage();

	/*! \brief Set 
	 */
	void setMessage(ChatSimpleMessage message);
	/*! \brief Get 是否关闭查找窗口，默认是关闭，如果设置为false,则不关闭查找窗口，速度会略快，但需要自行关闭查找窗口.
	 */
	bool isIsCloseSearchWin();

	/*! \brief Set 是否关闭查找窗口，默认是关闭，如果设置为false,则不关闭查找窗口，速度会略快，但需要自行关闭查找窗口.
	 */
	void setIsCloseSearchWin(bool isCloseSearchWin);


    private:
    Date date;
    ChatSimpleMessage message;
    bool isCloseSearchWin{};
};
}

#endif /* TINY_CPP_CLIENT_ChatRefer_H_ */
