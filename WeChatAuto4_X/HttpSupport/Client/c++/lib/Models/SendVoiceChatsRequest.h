
/*
 * SendVoiceChatsRequest.h
 *
 * 发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。
 */

#ifndef TINY_CPP_CLIENT_SendVoiceChatsRequest_H_
#define TINY_CPP_CLIENT_SendVoiceChatsRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include <list>

namespace Tiny {


/*! \brief 发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。
 *
 *  \ingroup Models
 *
 */

class SendVoiceChatsRequest{
public:

    /*! \brief Constructor.
	 */
    SendVoiceChatsRequest();
    SendVoiceChatsRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendVoiceChatsRequest();


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
	std::list<std::string> getPartner();

	/*! \brief Set 
	 */
	void setPartner(std::list<std::string> partner);


    private:
    std::string who{};
    std::list<std::string> partner;
};
}

#endif /* TINY_CPP_CLIENT_SendVoiceChatsRequest_H_ */
