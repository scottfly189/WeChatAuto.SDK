
/*
 * Image.h
 *
 * 
 */

#ifndef TINY_CPP_CLIENT_Image_H_
#define TINY_CPP_CLIENT_Image_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "AnyType.h"
#include "ColorPalette.h"
#include "ImageFormat.h"
#include "PixelFormat.h"
#include "PropertyItem.h"
#include "Size.h"
#include "SizeF.h"
#include <list>

namespace Tiny {


/*! \brief 
 *
 *  \ingroup Models
 *
 */

class Image{
public:

    /*! \brief Constructor.
	 */
    Image();
    Image(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~Image();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	AnyType getTag();

	/*! \brief Set 
	 */
	void setTag(AnyType tag);
	/*! \brief Get 
	 */
	SizeF getPhysicalDimension();

	/*! \brief Set 
	 */
	void setPhysicalDimension(SizeF physicalDimension);
	/*! \brief Get 
	 */
	Size getSize();

	/*! \brief Set 
	 */
	void setSize(Size size);
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
	/*! \brief Get 
	 */
	float getHorizontalResolution();

	/*! \brief Set 
	 */
	void setHorizontalResolution(float horizontalResolution);
	/*! \brief Get 
	 */
	float getVerticalResolution();

	/*! \brief Set 
	 */
	void setVerticalResolution(float verticalResolution);
	/*! \brief Get 
	 */
	int getFlags();

	/*! \brief Set 
	 */
	void setFlags(int flags);
	/*! \brief Get 
	 */
	ImageFormat getRawFormat();

	/*! \brief Set 
	 */
	void setRawFormat(ImageFormat rawFormat);
	/*! \brief Get 
	 */
	PixelFormat getPixelFormat();

	/*! \brief Set 
	 */
	void setPixelFormat(PixelFormat pixelFormat);
	/*! \brief Get 
	 */
	std::list<int> getPropertyIdList();

	/*! \brief Set 
	 */
	void setPropertyIdList(std::list<int> propertyIdList);
	/*! \brief Get 
	 */
	std::list<PropertyItem> getPropertyItems();

	/*! \brief Set 
	 */
	void setPropertyItems(std::list<PropertyItem> propertyItems);
	/*! \brief Get 
	 */
	ColorPalette getPalette();

	/*! \brief Set 
	 */
	void setPalette(ColorPalette palette);
	/*! \brief Get 
	 */
	std::list<std::string> getFrameDimensionsList();

	/*! \brief Set 
	 */
	void setFrameDimensionsList(std::list<std::string> frameDimensionsList);


    private:
    AnyType tag;
    SizeF physicalDimension;
    Size size;
    int width{};
    int height{};
    float horizontalResolution{};
    float verticalResolution{};
    int flags{};
    ImageFormat rawFormat;
    PixelFormat pixelFormat;
    std::list<int> propertyIdList;
    std::list<PropertyItem> propertyItems;
    ColorPalette palette;
    std::list<std::string> frameDimensionsList;
};
}

#endif /* TINY_CPP_CLIENT_Image_H_ */
