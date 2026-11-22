
/*
 * AddFriendsOptions.h
 *
 * 增加朋友选项
 */

#ifndef TINY_CPP_CLIENT_AddFriendsOptions_H_
#define TINY_CPP_CLIENT_AddFriendsOptions_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 增加朋友选项
 *
 *  \ingroup Models
 *
 */

class AddFriendsOptions{
public:

    /*! \brief Constructor.
	 */
    AddFriendsOptions();
    AddFriendsOptions(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~AddFriendsOptions();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 间隔时间,以秒为单位，默认为三秒，如果担心风控，可以把此时间设置长一点
	 */
	int getIntervalTime();

	/*! \brief Set 间隔时间,以秒为单位，默认为三秒，如果担心风控，可以把此时间设置长一点
	 */
	void setIntervalTime(int intervalTime);
	/*! \brief Get 是否关闭增加朋友窗口,默认关闭，可以设置为false不关闭
	 */
	bool isIsCloseWin();

	/*! \brief Set 是否关闭增加朋友窗口,默认关闭，可以设置为false不关闭
	 */
	void setIsCloseWin(bool isCloseWin);
	/*! \brief Get 加好友时打招呼内容,如果为空，则保持微信默认
	 */
	std::string getSayHi();

	/*! \brief Set 加好友时打招呼内容,如果为空，则保持微信默认
	 */
	void setSayHi(std::string sayHi);
	/*! \brief Get 备注后缀，如：设置suffix为\"test\",则备注为:xxxx_test,可以未来以备注的后缀来区分不同的好友分类
	 */
	std::string getSuffix();

	/*! \brief Set 备注后缀，如：设置suffix为\"test\",则备注为:xxxx_test,可以未来以备注的后缀来区分不同的好友分类
	 */
	void setSuffix(std::string suffix);
	/*! \brief Get 设置标签
	 */
	std::string getLabel();

	/*! \brief Set 设置标签
	 */
	void setLabel(std::string label);


    private:
    int intervalTime{};
    bool isCloseWin{};
    std::string sayHi{};
    std::string suffix{};
    std::string label{};
};
}

#endif /* TINY_CPP_CLIENT_AddFriendsOptions_H_ */
