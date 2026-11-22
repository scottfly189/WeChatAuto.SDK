
/*
 * SearchConversationRequest.h
 *
 * 搜索会话。
 */

#ifndef TINY_CPP_CLIENT_SearchConversationRequest_H_
#define TINY_CPP_CLIENT_SearchConversationRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 搜索会话。
 *
 *  \ingroup Models
 *
 */

class SearchConversationRequest{
public:

    /*! \brief Constructor.
	 */
    SearchConversationRequest();
    SearchConversationRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SearchConversationRequest();


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


    private:
    std::string who{};
};
}

#endif /* TINY_CPP_CLIENT_SearchConversationRequest_H_ */
