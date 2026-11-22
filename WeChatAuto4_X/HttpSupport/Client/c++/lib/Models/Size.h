
/*
 * Size.h
 *
 * 
 */

#ifndef TINY_CPP_CLIENT_Size_H_
#define TINY_CPP_CLIENT_Size_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 
 *
 *  \ingroup Models
 *
 */

class Size{
public:

    /*! \brief Constructor.
	 */
    Size();
    Size(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~Size();


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
	int getWidth();

	/*! \brief Set 
	 */
	void setWidth(int width);
	/*! \brief Get 
	 */
	int getHeight();

	/*! \brief Set 
	 */
	void setHeight(int height);


    private:
    bool isEmpty{};
    int width{};
    int height{};
};
}

#endif /* TINY_CPP_CLIENT_Size_H_ */
