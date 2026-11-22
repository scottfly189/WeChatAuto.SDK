
/*
 * NavigationType.h
 *
 * 导航栏类型
 */

#ifndef TINY_CPP_CLIENT_NavigationType_H_
#define TINY_CPP_CLIENT_NavigationType_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 导航栏类型
 *
 *  \ingroup Models
 *
 */

class NavigationType{
public:

    /*! \brief Constructor.
	 */
    NavigationType();
    NavigationType(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~NavigationType();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);



    private:
};
}

#endif /* TINY_CPP_CLIENT_NavigationType_H_ */
