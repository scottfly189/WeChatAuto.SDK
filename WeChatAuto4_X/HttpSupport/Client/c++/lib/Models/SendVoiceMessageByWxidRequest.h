
/*
 * SendVoiceMessageByWxidRequest.h
 *
 * 按联系人 UserName 发送语音文件。
 */

#ifndef TINY_CPP_CLIENT_SendVoiceMessageByWxidRequest_H_
#define TINY_CPP_CLIENT_SendVoiceMessageByWxidRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 按联系人 UserName 发送语音文件。
 *
 *  \ingroup Models
 *
 */

class SendVoiceMessageByWxidRequest{
public:

    /*! \brief Constructor.
	 */
    SendVoiceMessageByWxidRequest();
    SendVoiceMessageByWxidRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendVoiceMessageByWxidRequest();


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
	std::string getFilePath();

	/*! \brief Set 
	 */
	void setFilePath(std::string file_path);


    private:
    std::string user_name{};
    std::string file_path{};
};
}

#endif /* TINY_CPP_CLIENT_SendVoiceMessageByWxidRequest_H_ */
