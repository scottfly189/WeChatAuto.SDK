
/*
 * ChangeGroupMemoRequest.h
 *
 * 修改群备注。
 */

#ifndef TINY_CPP_CLIENT_ChangeGroupMemoRequest_H_
#define TINY_CPP_CLIENT_ChangeGroupMemoRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 修改群备注。
 *
 *  \ingroup Models
 *
 */

class ChangeGroupMemoRequest{
public:

    /*! \brief Constructor.
	 */
    ChangeGroupMemoRequest();
    ChangeGroupMemoRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~ChangeGroupMemoRequest();


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
	std::string getNewMemo();

	/*! \brief Set 
	 */
	void setNewMemo(std::string new_memo);


    private:
    std::string group_name{};
    std::string new_memo{};
};
}

#endif /* TINY_CPP_CLIENT_ChangeGroupMemoRequest_H_ */
