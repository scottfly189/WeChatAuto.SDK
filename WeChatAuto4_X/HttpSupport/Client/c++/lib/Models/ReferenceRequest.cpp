

#include "ReferenceRequest.h"

using namespace Tiny;

ReferenceRequest::ReferenceRequest()
{
	message = ChatSimpleMessage();
	who = std::string();
	message_content = std::string();
	prev_scroll_number = int(0);
}

ReferenceRequest::ReferenceRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

ReferenceRequest::~ReferenceRequest()
{

}

void
ReferenceRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *messageKey = "message";

    if(object.has_key(messageKey))
    {
        bourne::json value = object[messageKey];




        ChatSimpleMessage* obj = &message;
		obj->fromJson(value.dump());

    }

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


    }

    const char *message_contentKey = "message_content";

    if(object.has_key(message_contentKey))
    {
        bourne::json value = object[message_contentKey];



        jsonToValue(&message_content, value, "std::string");


    }

    const char *prev_scroll_numberKey = "prev_scroll_number";

    if(object.has_key(prev_scroll_numberKey))
    {
        bourne::json value = object[prev_scroll_numberKey];



        jsonToValue(&prev_scroll_number, value, "int");


    }


}

bourne::json
ReferenceRequest::toJson()
{
    bourne::json object = bourne::json::object();






	object["message"] = getMessage().toJson();





    object["who"] = getWho();






    object["message_content"] = getMessageContent();






    object["prev_scroll_number"] = getPrevScrollNumber();



    return object;

}

ChatSimpleMessage
ReferenceRequest::getMessage()
{
	return message;
}

void
ReferenceRequest::setMessage(ChatSimpleMessage message)
{
	this->message = message;
}

std::string
ReferenceRequest::getWho()
{
	return who;
}

void
ReferenceRequest::setWho(std::string who)
{
	this->who = who;
}

std::string
ReferenceRequest::getMessageContent()
{
	return message_content;
}

void
ReferenceRequest::setMessageContent(std::string message_content)
{
	this->message_content = message_content;
}

int
ReferenceRequest::getPrevScrollNumber()
{
	return prev_scroll_number;
}

void
ReferenceRequest::setPrevScrollNumber(int prev_scroll_number)
{
	this->prev_scroll_number = prev_scroll_number;
}



