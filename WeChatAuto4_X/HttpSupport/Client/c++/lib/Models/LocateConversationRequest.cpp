

#include "LocateConversationRequest.h"

using namespace Tiny;

LocateConversationRequest::LocateConversationRequest()
{
	title = std::string();
}

LocateConversationRequest::LocateConversationRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

LocateConversationRequest::~LocateConversationRequest()
{

}

void
LocateConversationRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *titleKey = "title";

    if(object.has_key(titleKey))
    {
        bourne::json value = object[titleKey];



        jsonToValue(&title, value, "std::string");


    }


}

bourne::json
LocateConversationRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["title"] = getTitle();



    return object;

}

std::string
LocateConversationRequest::getTitle()
{
	return title;
}

void
LocateConversationRequest::setTitle(std::string title)
{
	this->title = title;
}



