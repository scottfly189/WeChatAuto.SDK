

#include "ForwardMessageTypeEnums.h"

using namespace Tiny;

ForwardMessageTypeEnums::ForwardMessageTypeEnums()
{
}

ForwardMessageTypeEnums::ForwardMessageTypeEnums(std::string jsonString)
{
	this->fromJson(jsonString);
}

ForwardMessageTypeEnums::~ForwardMessageTypeEnums()
{

}

void
ForwardMessageTypeEnums::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);


}

bourne::json
ForwardMessageTypeEnums::toJson()
{
    bourne::json object = bourne::json::object();


    return object;

}



