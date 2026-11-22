
/*
 * SendVoiceMessageTtsByWxidRequest.h
 *
 * 按联系人 UserName 用 TTS 发送语音。
 */

#ifndef TINY_CPP_CLIENT_SendVoiceMessageTtsByWxidRequest_H_
#define TINY_CPP_CLIENT_SendVoiceMessageTtsByWxidRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "VoiceOptions.h"

namespace Tiny {


/*! \brief 按联系人 UserName 用 TTS 发送语音。
 *
 *  \ingroup Models
 *
 */

class SendVoiceMessageTtsByWxidRequest{
public:

    /*! \brief Constructor.
	 */
    SendVoiceMessageTtsByWxidRequest();
    SendVoiceMessageTtsByWxidRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendVoiceMessageTtsByWxidRequest();


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
	std::string getApiKey();

	/*! \brief Set 
	 */
	void setApiKey(std::string api_key);
	/*! \brief Get 
	 */
	std::string getMessage();

	/*! \brief Set 
	 */
	void setMessage(std::string message);
	/*! \brief Get 
	 */
	VoiceOptions getOptions();

	/*! \brief Set 
	 */
	void setOptions(VoiceOptions options);
	/*! \brief Get 
	 */
	bool isOptimizeWithLlm();

	/*! \brief Set 
	 */
	void setOptimizeWithLlm(bool optimize_with_llm);


    private:
    std::string user_name{};
    std::string api_key{};
    std::string message{};
    VoiceOptions options;
    bool optimize_with_llm{};
};
}

#endif /* TINY_CPP_CLIENT_SendVoiceMessageTtsByWxidRequest_H_ */
