
/*
 * WhoRequest.h
 *
 * 仅携带 who（可为空，作用于当前焦点窗口）的请求。
 */

#ifndef TINY_CPP_CLIENT_WhoRequest_H_
#define TINY_CPP_CLIENT_WhoRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 仅携带 who（可为空，作用于当前焦点窗口）的请求。
 *
 *  \ingroup Models
 *
 */

class WhoRequest{
public:

    /*! \brief Constructor.
	 */
    WhoRequest();
    WhoRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~WhoRequest();


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

#endif /* TINY_CPP_CLIENT_WhoRequest_H_ */
