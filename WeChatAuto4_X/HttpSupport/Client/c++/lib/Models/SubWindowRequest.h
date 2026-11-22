
/*
 * SubWindowRequest.h
 *
 * 打开/关闭指定好友或群的子窗口。
 */

#ifndef TINY_CPP_CLIENT_SubWindowRequest_H_
#define TINY_CPP_CLIENT_SubWindowRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 打开/关闭指定好友或群的子窗口。
 *
 *  \ingroup Models
 *
 */

class SubWindowRequest{
public:

    /*! \brief Constructor.
	 */
    SubWindowRequest();
    SubWindowRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SubWindowRequest();


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


    private:
    std::string who{};
};
}

#endif /* TINY_CPP_CLIENT_SubWindowRequest_H_ */
