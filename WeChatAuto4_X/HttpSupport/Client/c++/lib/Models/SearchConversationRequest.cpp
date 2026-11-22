

#include "SearchConversationRequest.h"

using namespace Tiny;

SearchConversationRequest::SearchConversationRequest()
{
	who = std::string();
}

SearchConversationRequest::SearchConversationRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SearchConversationRequest::~SearchConversationRequest()
{

}

void
SearchConversationRequest::fromJson(std::string jsonObj)
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
SearchConversationRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();



    return object;

}

std::string
SearchConversationRequest::getWho()
{
	return who;
}

void
SearchConversationRequest::setWho(std::string who)
{
	this->who = who;
}



