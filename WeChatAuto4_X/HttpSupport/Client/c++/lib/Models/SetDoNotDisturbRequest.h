
/*
 * SetDoNotDisturbRequest.h
 *
 * 设置会话免打扰。who 为空时作用于当前窗口。
 */

#ifndef TINY_CPP_CLIENT_SetDoNotDisturbRequest_H_
#define TINY_CPP_CLIENT_SetDoNotDisturbRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 设置会话免打扰。who 为空时作用于当前窗口。
 *
 *  \ingroup Models
 *
 */

class SetDoNotDisturbRequest{
public:

    /*! \brief Constructor.
	 */
    SetDoNotDisturbRequest();
    SetDoNotDisturbRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SetDoNotDisturbRequest();


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
	bool isSetting();

	/*! \brief Set 
	 */
	void setSetting(bool setting);


    private:
    std::string who{};
    bool setting{};
};
}

#endif /* TINY_CPP_CLIENT_SetDoNotDisturbRequest_H_ */
