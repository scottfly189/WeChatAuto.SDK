
/*
 * SetTopMostRequest.h
 *
 * 设置会话置顶。who 为空时作用于当前窗口。
 */

#ifndef TINY_CPP_CLIENT_SetTopMostRequest_H_
#define TINY_CPP_CLIENT_SetTopMostRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 设置会话置顶。who 为空时作用于当前窗口。
 *
 *  \ingroup Models
 *
 */

class SetTopMostRequest{
public:

    /*! \brief Constructor.
	 */
    SetTopMostRequest();
    SetTopMostRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SetTopMostRequest();


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

#endif /* TINY_CPP_CLIENT_SetTopMostRequest_H_ */
