
/*
 * SwitchNavigationRequest.h
 *
 * 切换/关闭导航栏窗口。
 */

#ifndef TINY_CPP_CLIENT_SwitchNavigationRequest_H_
#define TINY_CPP_CLIENT_SwitchNavigationRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "NavigationType.h"

namespace Tiny {


/*! \brief 切换/关闭导航栏窗口。
 *
 *  \ingroup Models
 *
 */

class SwitchNavigationRequest{
public:

    /*! \brief Constructor.
	 */
    SwitchNavigationRequest();
    SwitchNavigationRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SwitchNavigationRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	NavigationType getNavigationType();

	/*! \brief Set 
	 */
	void setNavigationType(NavigationType navigation_type);


    private:
    NavigationType navigation_type;
};
}

#endif /* TINY_CPP_CLIENT_SwitchNavigationRequest_H_ */
