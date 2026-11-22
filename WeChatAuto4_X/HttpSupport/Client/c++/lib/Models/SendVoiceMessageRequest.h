
/*
 * SendVoiceMessageRequest.h
 *
 * 发送语音消息。who 为空时发送给当前焦点聊天窗口。
 */

#ifndef TINY_CPP_CLIENT_SendVoiceMessageRequest_H_
#define TINY_CPP_CLIENT_SendVoiceMessageRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 发送语音消息。who 为空时发送给当前焦点聊天窗口。
 *
 *  \ingroup Models
 *
 */

class SendVoiceMessageRequest{
public:

    /*! \brief Constructor.
	 */
    SendVoiceMessageRequest();
    SendVoiceMessageRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendVoiceMessageRequest();


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
	std::string getFilePath();

	/*! \brief Set 
	 */
	void setFilePath(std::string file_path);


    private:
    std::string who{};
    std::string file_path{};
};
}

#endif /* TINY_CPP_CLIENT_SendVoiceMessageRequest_H_ */
