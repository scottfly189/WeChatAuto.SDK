
/*
 * PropertyItem.h
 *
 * 
 */

#ifndef TINY_CPP_CLIENT_PropertyItem_H_
#define TINY_CPP_CLIENT_PropertyItem_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "ByteArray.h"

namespace Tiny {


/*! \brief 
 *
 *  \ingroup Models
 *
 */

class PropertyItem{
public:

    /*! \brief Constructor.
	 */
    PropertyItem();
    PropertyItem(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~PropertyItem();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	int getId();

	/*! \brief Set 
	 */
	void setId(int id);
	/*! \brief Get 
	 */
	int getLen();

	/*! \brief Set 
	 */
	void setLen(int len);
	/*! \brief Get 
	 */
	int getType();

	/*! \brief Set 
	 */
	void setType(int type);
	/*! \brief Get 
	 */
	ByteArray getValue();

	/*! \brief Set 
	 */
	void setValue(ByteArray value);


    private:
    int id{};
    int len{};
    int type{};
    ByteArray value;
};
}

#endif /* TINY_CPP_CLIENT_PropertyItem_H_ */
