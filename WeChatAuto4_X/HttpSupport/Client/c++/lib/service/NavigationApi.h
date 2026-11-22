#ifndef TINY_CPP_CLIENT_NavigationApi_H_
#define TINY_CPP_CLIENT_NavigationApi_H_


#include "Response.h"
#include "Arduino.h"
#include "Service.h"
#include "Helpers.h"
#include <list>

#include "SwitchNavigationRequest.h"

namespace Tiny {

/**
 *  Class 
 * Generated with openapi::tiny-cpp-client
 */

class NavigationApi : public Service {
public:
    NavigationApi() = default;

    virtual ~NavigationApi();

    /**
    * 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。.
    *
    * 
    * \param clientId  *Required*
    * \param switchNavigationRequest 
    */
    Response<
            String
        >
    navigationClose(
            
            std::string clientId
            , 
            
            SwitchNavigationRequest switchNavigationRequest
            
    );
    /**
    * 切换导航栏。.
    *
    * 
    * \param clientId  *Required*
    * \param switchNavigationRequest 
    */
    Response<
            String
        >
    navigationSwitch(
            
            std::string clientId
            , 
            
            SwitchNavigationRequest switchNavigationRequest
            
    );
}; 

} 

#endif /* TINY_CPP_CLIENT_NavigationApi_H_ */