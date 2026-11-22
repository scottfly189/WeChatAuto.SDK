

#include "PixelFormat.h"

using namespace Tiny;

PixelFormat::PixelFormat()
{
}

PixelFormat::PixelFormat(std::string jsonString)
{
	this->fromJson(jsonString);
}

PixelFormat::~PixelFormat()
{

}

void
PixelFormat::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);


}

bourne::json
PixelFormat::toJson()
{
    bourne::json object = bourne::json::object();


    return object;

}



