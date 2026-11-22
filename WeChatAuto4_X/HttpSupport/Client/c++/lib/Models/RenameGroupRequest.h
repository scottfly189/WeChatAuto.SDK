
/*
 * RenameGroupRequest.h
 *
 * 修改群名。
 */

#ifndef TINY_CPP_CLIENT_RenameGroupRequest_H_
#define TINY_CPP_CLIENT_RenameGroupRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 修改群名。
 *
 *  \ingroup Models
 *
 */

class RenameGroupRequest{
public:

    /*! \brief Constructor.
	 */
    RenameGroupRequest();
    RenameGroupRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~RenameGroupRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getOldGroupName();

	/*! \brief Set 
	 */
	void setOldGroupName(std::string old_group_name);
	/*! \brief Get 
	 */
	std::string getNewGroupName();

	/*! \brief Set 
	 */
	void setNewGroupName(std::string new_group_name);


    private:
    std::string old_group_name{};
    std::string new_group_name{};
};
}

#endif /* TINY_CPP_CLIENT_RenameGroupRequest_H_ */
