

#include "TapRequest.h"

using namespace Tiny;

TapRequest::TapRequest()
{
	who = std::string();
	prev_scroll_number = int(0);
}

TapRequest::TapRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

TapRequest::~TapRequest()
{

}

void
TapRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


    }

    const char *prev_scroll_numberKey = "prev_scroll_number";

    if(object.has_key(prev_scroll_numberKey))
    {
        bourne::json value = object[prev_scroll_numberKey];



        jsonToValue(&prev_scroll_number, value, "int");


    }


}

bourne::json
TapRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();






    object["prev_scroll_number"] = getPrevScrollNumber();



    return object;

}

std::string
TapRequest::getWho()
{
	return who;
}

void
TapRequest::setWho(std::string who)
{
	this->who = who;
}

int
TapRequest::getPrevScrollNumber()
{
	return prev_scroll_number;
}

void
TapRequest::setPrevScrollNumber(int prev_scroll_number)
{
	this->prev_scroll_number = prev_scroll_number;
}



