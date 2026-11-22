#ifndef TINY_CPP_CLIENT_WindowApi_H_
#define TINY_CPP_CLIENT_WindowApi_H_


#include "Response.h"
#include "Arduino.h"
#include "Service.h"
#include "Helpers.h"
#include <list>

#include "SubWindowRequest.h"

namespace Tiny {

/**
 *  Class 
 * Generated with openapi::tiny-cpp-client
 */

class WindowApi : public Service {
public:
    WindowApi() = default;

    virtual ~WindowApi();

    /**
    * 关闭本微信窗口。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    windowClose(
            
            std::string clientId
            
    );
    /**
    * 关闭查询窗口（已打开则关闭）。.
    *
    * 
    * \param clientId  *Required*
    * \param subWindowRequest 
    */
    Response<
            String
        >
    windowCloseSearchWindow(
            
            std::string clientId
            , 
            
            SubWindowRequest subWindowRequest
            
    );
    /**
    * 关闭指定对象的子窗口（已存在则关闭）。.
    *
    * 
    * \param clientId  *Required*
    * \param subWindowRequest 
    */
    Response<
            String
        >
    windowCloseSubWindow(
            
            std::string clientId
            , 
            
            SubWindowRequest subWindowRequest
            
    );
    /**
    * 使主窗口获取焦点。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    windowFocus(
            
            std::string clientId
            
    );
    /**
    * 获取本微信窗口句柄。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    windowGetHandler(
            
            std::string clientId
            
    );
    /**
    * 获取本微信窗口进程 ID。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    windowGetProcessId(
            
            std::string clientId
            
    );
    /**
    * 最大化微信窗口。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    windowMaximize(
            
            std::string clientId
            
    );
    /**
    * 打开本微信窗口。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    windowOpen(
            
            std::string clientId
            
    );
    /**
    * 打开指定对象的子窗口。.
    *
    * 
    * \param clientId  *Required*
    * \param subWindowRequest 
    */
    Response<
            String
        >
    windowOpenSubWindow(
            
            std::string clientId
            , 
            
            SubWindowRequest subWindowRequest
            
    );
    /**
    * 置顶微信窗口。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    windowPin(
            
            std::string clientId
            
    );
    /**
    * 还原微信窗口。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    windowRestore(
            
            std::string clientId
            
    );
    /**
    * 取消置顶微信窗口。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    windowUnpin(
            
            std::string clientId
            
    );
}; 

} 

#endif /* TINY_CPP_CLIENT_WindowApi_H_ */