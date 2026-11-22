
/*
 * SendFileByWxidRequest.h
 *
 * 按联系人 UserName 发送文件。
 */

#ifndef TINY_CPP_CLIENT_SendFileByWxidRequest_H_
#define TINY_CPP_CLIENT_SendFileByWxidRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include <list>

namespace Tiny {


/*! \brief 按联系人 UserName 发送文件。
 *
 *  \ingroup Models
 *
 */

class SendFileByWxidRequest{
public:

    /*! \brief Constructor.
	 */
    SendFileByWxidRequest();
    SendFileByWxidRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendFileByWxidRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getUserName();

	/*! \brief Set 
	 */
	void setUserName(std::string user_name);
	/*! \brief Get 
	 */
	std::list<std::string> getFiles();

	/*! \brief Set 
	 */
	void setFiles(std::list<std::string> files);


    private:
    std::string user_name{};
    std::list<std::string> files;
};
}

#endif /* TINY_CPP_CLIENT_SendFileByWxidRequest_H_ */
