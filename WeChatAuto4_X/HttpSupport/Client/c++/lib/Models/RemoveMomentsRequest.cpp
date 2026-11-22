

#include "RemoveMomentsRequest.h"

using namespace Tiny;

RemoveMomentsRequest::RemoveMomentsRequest()
{
	content = std::string();
}

RemoveMomentsRequest::RemoveMomentsRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

RemoveMomentsRequest::~RemoveMomentsRequest()
{

}

void
RemoveMomentsRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *contentKey = "content";

    if(object.has_key(contentKey))
    {
        bourne::json value = object[contentKey];



        jsonToValue(&content, value, "std::string");


    }


}

bourne::json
RemoveMomentsRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["content"] = getContent();



    return object;

}

std::string
RemoveMomentsRequest::getContent()
{
	return content;
}

void
RemoveMomentsRequest::setContent(std::string content)
{
	this->content = content;
}



