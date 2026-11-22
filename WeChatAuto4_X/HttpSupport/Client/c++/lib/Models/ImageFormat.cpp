

#include "ImageFormat.h"

using namespace Tiny;

ImageFormat::ImageFormat()
{
	guid = std::string();
}

ImageFormat::ImageFormat(std::string jsonString)
{
	this->fromJson(jsonString);
}

ImageFormat::~ImageFormat()
{

}

void
ImageFormat::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *guidKey = "guid";

    if(object.has_key(guidKey))
    {
        bourne::json value = object[guidKey];



        jsonToValue(&guid, value, "std::string");


    }


}

bourne::json
ImageFormat::toJson()
{
    bourne::json object = bourne::json::object();





    object["guid"] = getGuid();



    return object;

}

std::string
ImageFormat::getGuid()
{
	return guid;
}

void
ImageFormat::setGuid(std::string guid)
{
	this->guid = guid;
}



