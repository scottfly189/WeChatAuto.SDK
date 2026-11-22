

#include "Size.h"

using namespace Tiny;

Size::Size()
{
	isEmpty = bool(false);
	width = int(0);
	height = int(0);
}

Size::Size(std::string jsonString)
{
	this->fromJson(jsonString);
}

Size::~Size()
{

}

void
Size::fromJson(std::string jsonObj)
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



        jsonToValue(&width, value, "int");


    }

    const char *heightKey = "height";

    if(object.has_key(heightKey))
    {
        bourne::json value = object[heightKey];



        jsonToValue(&height, value, "int");


    }


}

bourne::json
Size::toJson()
{
    bourne::json object = bourne::json::object();





    object["isEmpty"] = isIsEmpty();






    object["width"] = getWidth();






    object["height"] = getHeight();



    return object;

}

bool
Size::isIsEmpty()
{
	return isEmpty;
}

void
Size::setIsEmpty(bool isEmpty)
{
	this->isEmpty = isEmpty;
}

int
Size::getWidth()
{
	return width;
}

void
Size::setWidth(int width)
{
	this->width = width;
}

int
Size::getHeight()
{
	return height;
}

void
Size::setHeight(int height)
{
	this->height = height;
}



