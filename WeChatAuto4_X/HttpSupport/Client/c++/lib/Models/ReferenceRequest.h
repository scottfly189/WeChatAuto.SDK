
/*
 * ReferenceRequest.h
 *
 * 引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。
 */

#ifndef TINY_CPP_CLIENT_ReferenceRequest_H_
#define TINY_CPP_CLIENT_ReferenceRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "ChatSimpleMessage.h"

namespace Tiny {


/*! \brief 引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。
 *
 *  \ingroup Models
 *
 */

class ReferenceRequest{
public:

    /*! \brief Constructor.
	 */
    ReferenceRequest();
    ReferenceRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~ReferenceRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	ChatSimpleMessage getMessage();

	/*! \brief Set 
	 */
	void setMessage(ChatSimpleMessage message);
	/*! \brief Get 
	 */
	std::string getWho();

	/*! \brief Set 
	 */
	void setWho(std::string who);
	/*! \brief Get 
	 */
	std::string getMessageContent();

	/*! \brief Set 
	 */
	void setMessageContent(std::string message_content);
	/*! \brief Get 
	 */
	int getPrevScrollNumber();

	/*! \brief Set 
	 */
	void setPrevScrollNumber(int prev_scroll_number);


    private:
    ChatSimpleMessage message;
    std::string who{};
    std::string message_content{};
    int prev_scroll_number{};
};
}

#endif /* TINY_CPP_CLIENT_ReferenceRequest_H_ */
