
/*
 * SendFileRequest.h
 *
 * 发送文件。who 为空时发送给当前焦点聊天窗口。
 */

#ifndef TINY_CPP_CLIENT_SendFileRequest_H_
#define TINY_CPP_CLIENT_SendFileRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include <list>

namespace Tiny {


/*! \brief 发送文件。who 为空时发送给当前焦点聊天窗口。
 *
 *  \ingroup Models
 *
 */

class SendFileRequest{
public:

    /*! \brief Constructor.
	 */
    SendFileRequest();
    SendFileRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SendFileRequest();


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
	std::list<std::string> getFiles();

	/*! \brief Set 
	 */
	void setFiles(std::list<std::string> files);


    private:
    std::string who{};
    std::list<std::string> files;
};
}

#endif /* TINY_CPP_CLIENT_SendFileRequest_H_ */
