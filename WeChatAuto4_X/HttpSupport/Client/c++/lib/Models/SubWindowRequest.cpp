

#include "SubWindowRequest.h"

using namespace Tiny;

SubWindowRequest::SubWindowRequest()
{
	who = std::string();
}

SubWindowRequest::SubWindowRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SubWindowRequest::~SubWindowRequest()
{

}

void
SubWindowRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


    }


}

bourne::json
SubWindowRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();



    return object;

}

std::string
SubWindowRequest::getWho()
{
	return who;
}

void
SubWindowRequest::setWho(std::string who)
{
	this->who = who;
}



