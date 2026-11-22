
/*
 * ImageFormat.h
 *
 * 
 */

#ifndef TINY_CPP_CLIENT_ImageFormat_H_
#define TINY_CPP_CLIENT_ImageFormat_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 
 *
 *  \ingroup Models
 *
 */

class ImageFormat{
public:

    /*! \brief Constructor.
	 */
    ImageFormat();
    ImageFormat(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~ImageFormat();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getGuid();

	/*! \brief Set 
	 */
	void setGuid(std::string guid);


    private:
    std::string guid{};
};
}

#endif /* TINY_CPP_CLIENT_ImageFormat_H_ */
