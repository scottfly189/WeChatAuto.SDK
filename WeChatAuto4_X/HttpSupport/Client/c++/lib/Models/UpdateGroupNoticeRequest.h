
/*
 * UpdateGroupNoticeRequest.h
 *
 * 更新群公告。
 */

#ifndef TINY_CPP_CLIENT_UpdateGroupNoticeRequest_H_
#define TINY_CPP_CLIENT_UpdateGroupNoticeRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 更新群公告。
 *
 *  \ingroup Models
 *
 */

class UpdateGroupNoticeRequest{
public:

    /*! \brief Constructor.
	 */
    UpdateGroupNoticeRequest();
    UpdateGroupNoticeRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~UpdateGroupNoticeRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getGroupName();

	/*! \brief Set 
	 */
	void setGroupName(std::string group_name);
	/*! \brief Get 
	 */
	std::string getGroupNotice();

	/*! \brief Set 
	 */
	void setGroupNotice(std::string group_notice);


    private:
    std::string group_name{};
    std::string group_notice{};
};
}

#endif /* TINY_CPP_CLIENT_UpdateGroupNoticeRequest_H_ */
