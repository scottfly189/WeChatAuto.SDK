

#include "Image.h"

using namespace Tiny;

Image::Image()
{
	tag = null;
	physicalDimension = SizeF();
	size = Size();
	width = int(0);
	height = int(0);
	horizontalResolution = float(0);
	verticalResolution = float(0);
	flags = int(0);
	rawFormat = ImageFormat();
	pixelFormat = PixelFormat();
	propertyIdList = std::list<int>();
	propertyItems = std::list<PropertyItem>();
	palette = ColorPalette();
	frameDimensionsList = std::list<std::string>();
}

Image::Image(std::string jsonString)
{
	this->fromJson(jsonString);
}

Image::~Image()
{

}

void
Image::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *tagKey = "tag";

    if(object.has_key(tagKey))
    {
        bourne::json value = object[tagKey];




        AnyType* obj = &tag;
		obj->fromJson(value.dump());

    }

    const char *physicalDimensionKey = "physicalDimension";

    if(object.has_key(physicalDimensionKey))
    {
        bourne::json value = object[physicalDimensionKey];




        SizeF* obj = &physicalDimension;
		obj->fromJson(value.dump());

    }

    const char *sizeKey = "size";

    if(object.has_key(sizeKey))
    {
        bourne::json value = object[sizeKey];




        Size* obj = &size;
		obj->fromJson(value.dump());

    }

    const char *widthKey = "width";

    if(object.has_key(widthKey))
    {
        bourne::json value = object[widthKey];



        jsonToValue(&width, value, "int");


    }

    const char *heightKey = "height";

    if(object.has_key(heightKey))
    {
        bourne::json value = object[heightKey];



        jsonToValue(&height, value, "int");


    }

    const char *horizontalResolutionKey = "horizontalResolution";

    if(object.has_key(horizontalResolutionKey))
    {
        bourne::json value = object[horizontalResolutionKey];



        jsonToValue(&horizontalResolution, value, "float");


    }

    const char *verticalResolutionKey = "verticalResolution";

    if(object.has_key(verticalResolutionKey))
    {
        bourne::json value = object[verticalResolutionKey];



        jsonToValue(&verticalResolution, value, "float");


    }

    const char *flagsKey = "flags";

    if(object.has_key(flagsKey))
    {
        bourne::json value = object[flagsKey];



        jsonToValue(&flags, value, "int");


    }

    const char *rawFormatKey = "rawFormat";

    if(object.has_key(rawFormatKey))
    {
        bourne::json value = object[rawFormatKey];




        ImageFormat* obj = &rawFormat;
		obj->fromJson(value.dump());

    }

    const char *pixelFormatKey = "pixelFormat";

    if(object.has_key(pixelFormatKey))
    {
        bourne::json value = object[pixelFormatKey];




        PixelFormat* obj = &pixelFormat;
		obj->fromJson(value.dump());

    }

    const char *propertyIdListKey = "propertyIdList";

    if(object.has_key(propertyIdListKey))
    {
        bourne::json value = object[propertyIdListKey];


        std::list<int> propertyIdList_list;
        int element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "int");


            propertyIdList_list.push_back(element);
        }
        propertyIdList = propertyIdList_list;


    }

    const char *propertyItemsKey = "propertyItems";

    if(object.has_key(propertyItemsKey))
    {
        bourne::json value = object[propertyItemsKey];


        std::list<PropertyItem> propertyItems_list;
        PropertyItem element;
        for(auto& var : value.array_range())
        {


            element.fromJson(var.dump());

            propertyItems_list.push_back(element);
        }
        propertyItems = propertyItems_list;


    }

    const char *paletteKey = "palette";

    if(object.has_key(paletteKey))
    {
        bourne::json value = object[paletteKey];




        ColorPalette* obj = &palette;
		obj->fromJson(value.dump());

    }

    const char *frameDimensionsListKey = "frameDimensionsList";

    if(object.has_key(frameDimensionsListKey))
    {
        bourne::json value = object[frameDimensionsListKey];


        std::list<std::string> frameDimensionsList_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            frameDimensionsList_list.push_back(element);
        }
        frameDimensionsList = frameDimensionsList_list;


    }


}

bourne::json
Image::toJson()
{
    bourne::json object = bourne::json::object();






	object["tag"] = getTag().toJson();






	object["physicalDimension"] = getPhysicalDimension().toJson();






	object["size"] = getSize().toJson();





    object["width"] = getWidth();






    object["height"] = getHeight();






    object["horizontalResolution"] = getHorizontalResolution();






    object["verticalResolution"] = getVerticalResolution();






    object["flags"] = getFlags();







	object["rawFormat"] = getRawFormat().toJson();






	object["pixelFormat"] = getPixelFormat().toJson();




    std::list<int> propertyIdList_list = getPropertyIdList();
    bourne::json propertyIdList_arr = bourne::json::array();

    for(auto& var : propertyIdList_list)
    {
        propertyIdList_arr.append(var);
    }
    object["propertyIdList"] = propertyIdList_arr;








    std::list<PropertyItem> propertyItems_list = getPropertyItems();
    bourne::json propertyItems_arr = bourne::json::array();

    for(auto& var : propertyItems_list)
    {
        PropertyItem obj = var;
        propertyItems_arr.append(obj.toJson());
    }
    object["propertyItems"] = propertyItems_arr;








	object["palette"] = getPalette().toJson();




    std::list<std::string> frameDimensionsList_list = getFrameDimensionsList();
    bourne::json frameDimensionsList_arr = bourne::json::array();

    for(auto& var : frameDimensionsList_list)
    {
        frameDimensionsList_arr.append(var);
    }
    object["frameDimensionsList"] = frameDimensionsList_arr;






    return object;

}

AnyType
Image::getTag()
{
	return tag;
}

void
Image::setTag(AnyType tag)
{
	this->tag = tag;
}

SizeF
Image::getPhysicalDimension()
{
	return physicalDimension;
}

void
Image::setPhysicalDimension(SizeF physicalDimension)
{
	this->physicalDimension = physicalDimension;
}

Size
Image::getSize()
{
	return size;
}

void
Image::setSize(Size size)
{
	this->size = size;
}

int
Image::getWidth()
{
	return width;
}

void
Image::setWidth(int width)
{
	this->width = width;
}

int
Image::getHeight()
{
	return height;
}

void
Image::setHeight(int height)
{
	this->height = height;
}

float
Image::getHorizontalResolution()
{
	return horizontalResolution;
}

void
Image::setHorizontalResolution(float horizontalResolution)
{
	this->horizontalResolution = horizontalResolution;
}

float
Image::getVerticalResolution()
{
	return verticalResolution;
}

void
Image::setVerticalResolution(float verticalResolution)
{
	this->verticalResolution = verticalResolution;
}

int
Image::getFlags()
{
	return flags;
}

void
Image::setFlags(int flags)
{
	this->flags = flags;
}

ImageFormat
Image::getRawFormat()
{
	return rawFormat;
}

void
Image::setRawFormat(ImageFormat rawFormat)
{
	this->rawFormat = rawFormat;
}

PixelFormat
Image::getPixelFormat()
{
	return pixelFormat;
}

void
Image::setPixelFormat(PixelFormat pixelFormat)
{
	this->pixelFormat = pixelFormat;
}

std::list<int>
Image::getPropertyIdList()
{
	return propertyIdList;
}

void
Image::setPropertyIdList(std::list<int> propertyIdList)
{
	this->propertyIdList = propertyIdList;
}

std::list<PropertyItem>
Image::getPropertyItems()
{
	return propertyItems;
}

void
Image::setPropertyItems(std::list<PropertyItem> propertyItems)
{
	this->propertyItems = propertyItems;
}

ColorPalette
Image::getPalette()
{
	return palette;
}

void
Image::setPalette(ColorPalette palette)
{
	this->palette = palette;
}

std::list<std::string>
Image::getFrameDimensionsList()
{
	return frameDimensionsList;
}

void
Image::setFrameDimensionsList(std::list<std::string> frameDimensionsList)
{
	this->frameDimensionsList = frameDimensionsList;
}



