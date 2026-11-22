
/*
 * NotifyIconClickRequest.h
 *
 * 点击任务栏微信图标。index 与 wechat_name 二选一。
 */

#ifndef TINY_CPP_CLIENT_NotifyIconClickRequest_H_
#define TINY_CPP_CLIENT_NotifyIconClickRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 点击任务栏微信图标。index 与 wechat_name 二选一。
 *
 *  \ingroup Models
 *
 */

class NotifyIconClickRequest{
public:

    /*! \brief Constructor.
	 */
    NotifyIconClickRequest();
    NotifyIconClickRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~NotifyIconClickRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	int getIndex();

	/*! \brief Set 
	 */
	void setIndex(int index);
	/*! \brief Get 
	 */
	std::string getWechatName();

	/*! \brief Set 
	 */
	void setWechatName(std::string wechat_name);


    private:
    int index{};
    std::string wechat_name{};
};
}

#endif /* TINY_CPP_CLIENT_NotifyIconClickRequest_H_ */
