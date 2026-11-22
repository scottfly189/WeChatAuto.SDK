
/*
 * PassedAllNewFriendRequest.h
 *
 * 批量通过新好友申请 / 启动自动通过监听。
 */

#ifndef TINY_CPP_CLIENT_PassedAllNewFriendRequest_H_
#define TINY_CPP_CLIENT_PassedAllNewFriendRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "FriendKeywordRuleRequest.h"
#include <list>
#include <map>

namespace Tiny {


/*! \brief 批量通过新好友申请 / 启动自动通过监听。
 *
 *  \ingroup Models
 *
 */

class PassedAllNewFriendRequest{
public:

    /*! \brief Constructor.
	 */
    PassedAllNewFriendRequest();
    PassedAllNewFriendRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~PassedAllNewFriendRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::list<std::string> getKeyword();

	/*! \brief Set 
	 */
	void setKeyword(std::list<std::string> keyword);
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
	/*! \brief Get 
	 */
	bool isPassedDelete();

	/*! \brief Set 
	 */
	void setPassedDelete(bool passed_delete);
	/*! \brief Get 
	 */
	std::map<std::string, FriendKeywordRuleRequest> getKeywordRules();

	/*! \brief Set 
	 */
	void setKeywordRules(std::map<std::string, FriendKeywordRuleRequest> keyword_rules);


    private:
    std::list<std::string> keyword;
    std::string suffix{};
    std::string label{};
    bool passed_delete{};
    std::map<std::string, FriendKeywordRuleRequest> keyword_rules;
};
}

#endif /* TINY_CPP_CLIENT_PassedAllNewFriendRequest_H_ */
