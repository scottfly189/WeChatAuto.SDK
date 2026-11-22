

#include "PullMessagesRequest.h"

using namespace Tiny;

PullMessagesRequest::PullMessagesRequest()
{
	who = std::string();
	max_fetch_number = int(0);
	include_system_message = bool(false);
}

PullMessagesRequest::PullMessagesRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

PullMessagesRequest::~PullMessagesRequest()
{

}

void
PullMessagesRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


    }

    const char *max_fetch_numberKey = "max_fetch_number";

    if(object.has_key(max_fetch_numberKey))
    {
        bourne::json value = object[max_fetch_numberKey];



        jsonToValue(&max_fetch_number, value, "int");


    }

    const char *include_system_messageKey = "include_system_message";

    if(object.has_key(include_system_messageKey))
    {
        bourne::json value = object[include_system_messageKey];



        jsonToValue(&include_system_message, value, "bool");


    }


}

bourne::json
PullMessagesRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();






    object["max_fetch_number"] = getMaxFetchNumber();






    object["include_system_message"] = isIncludeSystemMessage();



    return object;

}

std::string
PullMessagesRequest::getWho()
{
	return who;
}

void
PullMessagesRequest::setWho(std::string who)
{
	this->who = who;
}

int
PullMessagesRequest::getMaxFetchNumber()
{
	return max_fetch_number;
}

void
PullMessagesRequest::setMaxFetchNumber(int max_fetch_number)
{
	this->max_fetch_number = max_fetch_number;
}

bool
PullMessagesRequest::isIncludeSystemMessage()
{
	return include_system_message;
}

void
PullMessagesRequest::setIncludeSystemMessage(bool include_system_message)
{
	this->include_system_message = include_system_message;
}



