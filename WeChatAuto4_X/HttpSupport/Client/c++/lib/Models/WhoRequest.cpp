

#include "WhoRequest.h"

using namespace Tiny;

WhoRequest::WhoRequest()
{
	who = std::string();
}

WhoRequest::WhoRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

WhoRequest::~WhoRequest()
{

}

void
WhoRequest::fromJson(std::string jsonObj)
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
WhoRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();



    return object;

}

std::string
WhoRequest::getWho()
{
	return who;
}

void
WhoRequest::setWho(std::string who)
{
	this->who = who;
}



