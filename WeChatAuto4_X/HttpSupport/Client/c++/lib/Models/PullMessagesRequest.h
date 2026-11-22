
/*
 * PullMessagesRequest.h
 *
 * 轮询拉取消息。
 */

#ifndef TINY_CPP_CLIENT_PullMessagesRequest_H_
#define TINY_CPP_CLIENT_PullMessagesRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 轮询拉取消息。
 *
 *  \ingroup Models
 *
 */

class PullMessagesRequest{
public:

    /*! \brief Constructor.
	 */
    PullMessagesRequest();
    PullMessagesRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~PullMessagesRequest();


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
	int getMaxFetchNumber();

	/*! \brief Set 
	 */
	void setMaxFetchNumber(int max_fetch_number);
	/*! \brief Get 
	 */
	bool isIncludeSystemMessage();

	/*! \brief Set 
	 */
	void setIncludeSystemMessage(bool include_system_message);


    private:
    std::string who{};
    int max_fetch_number{};
    bool include_system_message{};
};
}

#endif /* TINY_CPP_CLIENT_PullMessagesRequest_H_ */
