
/*
 * TapRequest.h
 *
 * 拍一拍。
 */

#ifndef TINY_CPP_CLIENT_TapRequest_H_
#define TINY_CPP_CLIENT_TapRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 拍一拍。
 *
 *  \ingroup Models
 *
 */

class TapRequest{
public:

    /*! \brief Constructor.
	 */
    TapRequest();
    TapRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~TapRequest();


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
	int getPrevScrollNumber();

	/*! \brief Set 
	 */
	void setPrevScrollNumber(int prev_scroll_number);


    private:
    std::string who{};
    int prev_scroll_number{};
};
}

#endif /* TINY_CPP_CLIENT_TapRequest_H_ */
