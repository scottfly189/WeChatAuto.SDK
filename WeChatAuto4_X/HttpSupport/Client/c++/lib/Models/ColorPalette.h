
/*
 * ColorPalette.h
 *
 * 
 */

#ifndef TINY_CPP_CLIENT_ColorPalette_H_
#define TINY_CPP_CLIENT_ColorPalette_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "Color.h"
#include <list>

namespace Tiny {


/*! \brief 
 *
 *  \ingroup Models
 *
 */

class ColorPalette{
public:

    /*! \brief Constructor.
	 */
    ColorPalette();
    ColorPalette(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~ColorPalette();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	int getFlags();

	/*! \brief Set 
	 */
	void setFlags(int flags);
	/*! \brief Get 
	 */
	std::list<Color> getEntries();

	/*! \brief Set 
	 */
	void setEntries(std::list<Color> entries);


    private:
    int flags{};
    std::list<Color> entries;
};
}

#endif /* TINY_CPP_CLIENT_ColorPalette_H_ */
