
/*
 * FriendKeywordRuleRequest.h
 *
 * 单个关键词对应的备注后缀和标签。
 */

#ifndef TINY_CPP_CLIENT_FriendKeywordRuleRequest_H_
#define TINY_CPP_CLIENT_FriendKeywordRuleRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 单个关键词对应的备注后缀和标签。
 *
 *  \ingroup Models
 *
 */

class FriendKeywordRuleRequest{
public:

    /*! \brief Constructor.
	 */
    FriendKeywordRuleRequest();
    FriendKeywordRuleRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~FriendKeywordRuleRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getSuffix();

	/*! \brief Set 
	 */
	void setSuffix(std::string suffix);
	/*! \brief Get 
	 */
	std::string getLabel();

	/*! \brief Set 
	 */
	void setLabel(std::string label);


    private:
    std::string suffix{};
    std::string label{};
};
}

#endif /* TINY_CPP_CLIENT_FriendKeywordRuleRequest_H_ */
