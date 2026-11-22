#ifndef TINY_CPP_CLIENT_ClientsApi_H_
#define TINY_CPP_CLIENT_ClientsApi_H_


#include "Response.h"
#include "Arduino.h"
#include "Service.h"
#include "Helpers.h"
#include <list>

#include "NotifyIconClickRequest.h"
#include "SaveAvatarRequest.h"

namespace Tiny {

/**
 *  Class 
 * Generated with openapi::tiny-cpp-client
 */

class ClientsApi : public Service {
public:
    ClientsApi() = default;

    virtual ~ClientsApi();

    /**
    * 点击任务栏微信图标，按索引或微信名称定位。.
    *
    * 
    * \param clientId  *Required*
    * \param notifyIconClickRequest 
    */
    Response<
            String
        >
    clientsClickNotifyIcon(
            
            std::string clientId
            , 
            
            NotifyIconClickRequest notifyIconClickRequest
            
    );
    /**
    * 获取全部微信客户端的概要信息列表。.
    *
    * 
    */
    Response<
            String
        >
    clientsGetAll(
    );
    /**
    * 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    clientsGetInfo(
            
            std::string clientId
            
    );
    /**
    * 获取本微信的个人信息（头像文件位置、wxid、昵称）。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    clientsGetOwnerInfo(
            
            std::string clientId
            
    );
    /**
    * 通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    clientsGetStorageRoot(
            
            std::string clientId
            
    );
    /**
    * 获取本微信的版本号。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    clientsGetVersion(
            
            std::string clientId
            
    );
    /**
    * 保存本微信头像到指定路径。.
    *
    * 
    * \param clientId  *Required*
    * \param saveAvatarRequest 
    */
    Response<
            String
        >
    clientsSaveAvatar(
            
            std::string clientId
            , 
            
            SaveAvatarRequest saveAvatarRequest
            
    );
}; 

} 

#endif /* TINY_CPP_CLIENT_ClientsApi_H_ */