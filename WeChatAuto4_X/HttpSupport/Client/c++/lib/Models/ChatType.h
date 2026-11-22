
/*
 * ChatType.h
 *
 * 聊天类型
 */

#ifndef TINY_CPP_CLIENT_ChatType_H_
#define TINY_CPP_CLIENT_ChatType_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 聊天类型
 *
 *  \ingroup Models
 *
 */

class ChatType{
public:

    /*! \brief Constructor.
	 */
    ChatType();
    ChatType(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~ChatType();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);



    private:
};
}

#endif /* TINY_CPP_CLIENT_ChatType_H_ */
