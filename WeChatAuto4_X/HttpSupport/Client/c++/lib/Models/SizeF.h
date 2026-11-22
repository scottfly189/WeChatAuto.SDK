
/*
 * SizeF.h
 *
 * 
 */

#ifndef TINY_CPP_CLIENT_SizeF_H_
#define TINY_CPP_CLIENT_SizeF_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 
 *
 *  \ingroup Models
 *
 */

class SizeF{
public:

    /*! \brief Constructor.
	 */
    SizeF();
    SizeF(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~SizeF();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	bool isIsEmpty();

	/*! \brief Set 
	 */
	void setIsEmpty(bool isEmpty);
	/*! \brief Get 
	 */
	float getWidth();

	/*! \brief Set 
	 */
	void setWidth(float width);
	/*! \brief Get 
	 */
	float getHeight();

	/*! \brief Set 
	 */
	void setHeight(float height);


    private:
    bool isEmpty{};
    float width{};
    float height{};
};
}

#endif /* TINY_CPP_CLIENT_SizeF_H_ */
