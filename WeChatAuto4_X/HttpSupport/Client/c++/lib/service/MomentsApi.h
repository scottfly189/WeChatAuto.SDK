#ifndef TINY_CPP_CLIENT_MomentsApi_H_
#define TINY_CPP_CLIENT_MomentsApi_H_


#include "Response.h"
#include "Arduino.h"
#include "Service.h"
#include "Helpers.h"
#include <list>

#include "AddMomentsRequest.h"
#include "RemoveMomentsRequest.h"

namespace Tiny {

/**
 *  Class 
 * Generated with openapi::tiny-cpp-client
 */

class MomentsApi : public Service {
public:
    MomentsApi() = default;

    virtual ~MomentsApi();

    /**
    * 发朋友圈（图文，多图需在同一目录）。.
    *
    * 
    * \param clientId  *Required*
    * \param addMomentsRequest 
    */
    Response<
            String
        >
    momentsAdd(
            
            std::string clientId
            , 
            
            AddMomentsRequest addMomentsRequest
            
    );
    /**
    * 关闭朋友圈。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    momentsClose(
            
            std::string clientId
            
    );
    /**
    * 打开朋友圈（已打开则提到最前）。.
    *
    * 
    * \param clientId  *Required*
    */
    Response<
            String
        >
    momentsOpen(
            
            std::string clientId
            
    );
    /**
    * 删除自己发的朋友圈（按文字内容）。.
    *
    * 
    * \param clientId  *Required*
    * \param removeMomentsRequest 
    */
    Response<
            String
        >
    momentsRemove(
            
            std::string clientId
            , 
            
            RemoveMomentsRequest removeMomentsRequest
            
    );
}; 

} 

#endif /* TINY_CPP_CLIENT_MomentsApi_H_ */