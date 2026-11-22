
/*
 * LocateConversationRequest.h
 *
 * 定位会话。
 */

#ifndef TINY_CPP_CLIENT_LocateConversationRequest_H_
#define TINY_CPP_CLIENT_LocateConversationRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 定位会话。
 *
 *  \ingroup Models
 *
 */

class LocateConversationRequest{
public:

    /*! \brief Constructor.
	 */
    LocateConversationRequest();
    LocateConversationRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~LocateConversationRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getTitle();

	/*! \brief Set 
	 */
	void setTitle(std::string title);


    private:
    std::string title{};
};
}

#endif /* TINY_CPP_CLIENT_LocateConversationRequest_H_ */
