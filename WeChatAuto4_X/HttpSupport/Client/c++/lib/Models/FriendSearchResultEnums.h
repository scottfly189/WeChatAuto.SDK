
/*
 * FriendSearchResultEnums.h
 *
 * 通过wxid查询，或者通过手机号查询结果枚举
 */

#ifndef TINY_CPP_CLIENT_FriendSearchResultEnums_H_
#define TINY_CPP_CLIENT_FriendSearchResultEnums_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 通过wxid查询，或者通过手机号查询结果枚举
 *
 *  \ingroup Models
 *
 */

class FriendSearchResultEnums{
public:

    /*! \brief Constructor.
	 */
    FriendSearchResultEnums();
    FriendSearchResultEnums(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~FriendSearchResultEnums();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);



    private:
};
}

#endif /* TINY_CPP_CLIENT_FriendSearchResultEnums_H_ */
