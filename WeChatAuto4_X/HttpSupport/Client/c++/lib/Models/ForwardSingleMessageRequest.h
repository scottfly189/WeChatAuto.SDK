
/*
 * ForwardSingleMessageRequest.h
 *
 * 转发单条消息（message 与 chat_simple_message 二选一）。
 */

#ifndef TINY_CPP_CLIENT_ForwardSingleMessageRequest_H_
#define TINY_CPP_CLIENT_ForwardSingleMessageRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "ChatSimpleMessage.h"
#include <list>

namespace Tiny {


/*! \brief 转发单条消息（message 与 chat_simple_message 二选一）。
 *
 *  \ingroup Models
 *
 */

class ForwardSingleMessageRequest{
public:

    /*! \brief Constructor.
	 */
    ForwardSingleMessageRequest();
    ForwardSingleMessageRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~ForwardSingleMessageRequest();


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
	/*! \brief Get 
	 */
	ChatSimpleMessage getChatSimpleMessage();

	/*! \brief Set 
	 */
	void setChatSimpleMessage(ChatSimpleMessage chat_simple_message);
	/*! \brief Get 
	 */
	std::list<std::string> getTo();

	/*! \brief Set 
	 */
	void setTo(std::list<std::string> to);
	/*! \brief Get 
	 */
	int getPrevScrollNumber();

	/*! \brief Set 
	 */
	void setPrevScrollNumber(int prev_scroll_number);


    private:
    std::string who{};
    std::string message{};
    ChatSimpleMessage chat_simple_message;
    std::list<std::string> to;
    int prev_scroll_number{};
};
}

#endif /* TINY_CPP_CLIENT_ForwardSingleMessageRequest_H_ */
