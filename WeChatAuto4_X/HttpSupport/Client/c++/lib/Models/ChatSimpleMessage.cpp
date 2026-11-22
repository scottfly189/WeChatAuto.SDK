

#include "ChatSimpleMessage.h"

using namespace Tiny;

ChatSimpleMessage::ChatSimpleMessage()
{
	who = std::string();
	message = std::string();
	sendDateTime = std::string();
	dateTime = std::string();
	uniqueString = std::string();
}

ChatSimpleMessage::ChatSimpleMessage(std::string jsonString)
{
	this->fromJson(jsonString);
}

ChatSimpleMessage::~ChatSimpleMessage()
{

}

void
ChatSimpleMessage::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


    }

    const char *messageKey = "message";

    if(object.has_key(messageKey))
    {
        bourne::json value = object[messageKey];



        jsonToValue(&message, value, "std::string");


    }

    const char *sendDateTimeKey = "sendDateTime";

    if(object.has_key(sendDateTimeKey))
    {
        bourne::json value = object[sendDateTimeKey];



        jsonToValue(&sendDateTime, value, "std::string");


    }

    const char *dateTimeKey = "dateTime";

    if(object.has_key(dateTimeKey))
    {
        bourne::json value = object[dateTimeKey];



        jsonToValue(&dateTime, value, "std::string");


    }

    const char *uniqueStringKey = "uniqueString";

    if(object.has_key(uniqueStringKey))
    {
        bourne::json value = object[uniqueStringKey];



        jsonToValue(&uniqueString, value, "std::string");


    }


}

bourne::json
ChatSimpleMessage::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();






    object["message"] = getMessage();






    object["sendDateTime"] = getSendDateTime();






    object["dateTime"] = getDateTime();






    object["uniqueString"] = getUniqueString();



    return object;

}

std::string
ChatSimpleMessage::getWho()
{
	return who;
}

void
ChatSimpleMessage::setWho(std::string who)
{
	this->who = who;
}

std::string
ChatSimpleMessage::getMessage()
{
	return message;
}

void
ChatSimpleMessage::setMessage(std::string message)
{
	this->message = message;
}

std::string
ChatSimpleMessage::getSendDateTime()
{
	return sendDateTime;
}

void
ChatSimpleMessage::setSendDateTime(std::string sendDateTime)
{
	this->sendDateTime = sendDateTime;
}

std::string
ChatSimpleMessage::getDateTime()
{
	return dateTime;
}

void
ChatSimpleMessage::setDateTime(std::string dateTime)
{
	this->dateTime = dateTime;
}

std::string
ChatSimpleMessage::getUniqueString()
{
	return uniqueString;
}

void
ChatSimpleMessage::setUniqueString(std::string uniqueString)
{
	this->uniqueString = uniqueString;
}



