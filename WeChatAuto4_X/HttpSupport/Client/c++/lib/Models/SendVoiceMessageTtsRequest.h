
/*
 * SendVoiceMessageTtsRequest.h
 *
 * 文字转语音发送。
 */

#ifndef TINY_CPP_CLIENT_SendVoiceMessageTtsRequest_H_
#define TINY_CPP_CLIENT_SendVoiceMessageTtsRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "VoiceOptions.h"

namespace Tiny {


/*! \brief 文字转语音发送。
 *
 *  \ingroup Models
 *
 */

class SendVoiceMessageTtsRequest{
public:

    /*! \brief Constructor.
	 */
    SendVoiceMessageTtsRequest();
    SendVoiceMessageTtsRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendVoiceMessageTtsRequest();


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
    std::string who{};
    std::string api_key{};
    std::string message{};
    VoiceOptions options;
    bool optimize_with_llm{};
};
}

#endif /* TINY_CPP_CLIENT_SendVoiceMessageTtsRequest_H_ */
