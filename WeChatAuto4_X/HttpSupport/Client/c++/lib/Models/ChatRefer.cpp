

#include "ChatRefer.h"

using namespace Tiny;

ChatRefer::ChatRefer()
{
	date = std::string();
	message = ChatSimpleMessage();
	isCloseSearchWin = bool(false);
}

ChatRefer::ChatRefer(std::string jsonString)
{
	this->fromJson(jsonString);
}

ChatRefer::~ChatRefer()
{

}

void
ChatRefer::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *dateKey = "date";

    if(object.has_key(dateKey))
    {
        bourne::json value = object[dateKey];




        Date* obj = &date;
		obj->fromJson(value.dump());

    }

    const char *messageKey = "message";

    if(object.has_key(messageKey))
    {
        bourne::json value = object[messageKey];




        ChatSimpleMessage* obj = &message;
		obj->fromJson(value.dump());

    }

    const char *isCloseSearchWinKey = "isCloseSearchWin";

    if(object.has_key(isCloseSearchWinKey))
    {
        bourne::json value = object[isCloseSearchWinKey];



        jsonToValue(&isCloseSearchWin, value, "bool");


    }


}

bourne::json
ChatRefer::toJson()
{
    bourne::json object = bourne::json::object();






	object["date"] = getDate().toJson();






	object["message"] = getMessage().toJson();





    object["isCloseSearchWin"] = isIsCloseSearchWin();



    return object;

}

Date
ChatRefer::getDate()
{
	return date;
}

void
ChatRefer::setDate(Date date)
{
	this->date = date;
}

ChatSimpleMessage
ChatRefer::getMessage()
{
	return message;
}

void
ChatRefer::setMessage(ChatSimpleMessage message)
{
	this->message = message;
}

bool
ChatRefer::isIsCloseSearchWin()
{
	return isCloseSearchWin;
}

void
ChatRefer::setIsCloseSearchWin(bool isCloseSearchWin)
{
	this->isCloseSearchWin = isCloseSearchWin;
}



