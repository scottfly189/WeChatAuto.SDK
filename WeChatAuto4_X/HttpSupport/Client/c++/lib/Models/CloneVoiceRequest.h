
/*
 * CloneVoiceRequest.h
 *
 * 声音复刻。
 */

#ifndef TINY_CPP_CLIENT_CloneVoiceRequest_H_
#define TINY_CPP_CLIENT_CloneVoiceRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 声音复刻。
 *
 *  \ingroup Models
 *
 */

class CloneVoiceRequest{
public:

    /*! \brief Constructor.
	 */
    CloneVoiceRequest();
    CloneVoiceRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~CloneVoiceRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getApiKey();

	/*! \brief Set 
	 */
	void setApiKey(std::string api_key);
	/*! \brief Get 
	 */
	std::string getAudioUrl();

	/*! \brief Set 
	 */
	void setAudioUrl(std::string audio_url);
	/*! \brief Get 
	 */
	std::string getTargetModel();

	/*! \brief Set 
	 */
	void setTargetModel(std::string target_model);
	/*! \brief Get 
	 */
	std::string getPrefix();

	/*! \brief Set 
	 */
	void setPrefix(std::string prefix);


    private:
    std::string api_key{};
    std::string audio_url{};
    std::string target_model{};
    std::string prefix{};
};
}

#endif /* TINY_CPP_CLIENT_CloneVoiceRequest_H_ */
