
/*
 * SaveAvatarRequest.h
 *
 * 保存头像到指定路径。
 */

#ifndef TINY_CPP_CLIENT_SaveAvatarRequest_H_
#define TINY_CPP_CLIENT_SaveAvatarRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 保存头像到指定路径。
 *
 *  \ingroup Models
 *
 */

class SaveAvatarRequest{
public:

    /*! \brief Constructor.
	 */
    SaveAvatarRequest();
    SaveAvatarRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SaveAvatarRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getPath();

	/*! \brief Set 
	 */
	void setPath(std::string path);


    private:
    std::string path{};
};
}

#endif /* TINY_CPP_CLIENT_SaveAvatarRequest_H_ */
