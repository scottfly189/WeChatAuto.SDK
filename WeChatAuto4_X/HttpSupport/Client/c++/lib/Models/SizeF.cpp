

#include "SizeF.h"

using namespace Tiny;

SizeF::SizeF()
{
	isEmpty = bool(false);
	width = float(0);
	height = float(0);
}

SizeF::SizeF(std::string jsonString)
{
	this->fromJson(jsonString);
}

SizeF::~SizeF()
{

}

void
SizeF::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *isEmptyKey = "isEmpty";

    if(object.has_key(isEmptyKey))
    {
        bourne::json value = object[isEmptyKey];



        jsonToValue(&isEmpty, value, "bool");


    }

    const char *widthKey = "width";

    if(object.has_key(widthKey))
    {
        bourne::json value = object[widthKey];



        jsonToValue(&width, value, "float");


    }

    const char *heightKey = "height";

    if(object.has_key(heightKey))
    {
        bourne::json value = object[heightKey];



        jsonToValue(&height, value, "float");


    }


}

bourne::json
SizeF::toJson()
{
    bourne::json object = bourne::json::object();





    object["isEmpty"] = isIsEmpty();






    object["width"] = getWidth();






    object["height"] = getHeight();



    return object;

}

bool
SizeF::isIsEmpty()
{
	return isEmpty;
}

void
SizeF::setIsEmpty(bool isEmpty)
{
	this->isEmpty = isEmpty;
}

float
SizeF::getWidth()
{
	return width;
}

void
SizeF::setWidth(float width)
{
	this->width = width;
}

float
SizeF::getHeight()
{
	return height;
}

void
SizeF::setHeight(float height)
{
	this->height = height;
}



