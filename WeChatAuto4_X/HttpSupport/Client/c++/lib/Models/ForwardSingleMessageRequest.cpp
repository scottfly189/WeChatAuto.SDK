

#include "ForwardSingleMessageRequest.h"

using namespace Tiny;

ForwardSingleMessageRequest::ForwardSingleMessageRequest()
{
	who = std::string();
	message = std::string();
	chat_simple_message = ChatSimpleMessage();
	to = std::list<std::string>();
	prev_scroll_number = int(0);
}

ForwardSingleMessageRequest::ForwardSingleMessageRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

ForwardSingleMessageRequest::~ForwardSingleMessageRequest()
{

}

void
ForwardSingleMessageRequest::fromJson(std::string jsonObj)
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

    const char *chat_simple_messageKey = "chat_simple_message";

    if(object.has_key(chat_simple_messageKey))
    {
        bourne::json value = object[chat_simple_messageKey];




        ChatSimpleMessage* obj = &chat_simple_message;
		obj->fromJson(value.dump());

    }

    const char *toKey = "to";

    if(object.has_key(toKey))
    {
        bourne::json value = object[toKey];


        std::list<std::string> to_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            to_list.push_back(element);
        }
        to = to_list;


    }

    const char *prev_scroll_numberKey = "prev_scroll_number";

    if(object.has_key(prev_scroll_numberKey))
    {
        bourne::json value = object[prev_scroll_numberKey];



        jsonToValue(&prev_scroll_number, value, "int");


    }


}

bourne::json
ForwardSingleMessageRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();






    object["message"] = getMessage();







	object["chat_simple_message"] = getChatSimpleMessage().toJson();




    std::list<std::string> to_list = getTo();
    bourne::json to_arr = bourne::json::array();

    for(auto& var : to_list)
    {
        to_arr.append(var);
    }
    object["to"] = to_arr;









    object["prev_scroll_number"] = getPrevScrollNumber();



    return object;

}

std::string
ForwardSingleMessageRequest::getWho()
{
	return who;
}

void
ForwardSingleMessageRequest::setWho(std::string who)
{
	this->who = who;
}

std::string
ForwardSingleMessageRequest::getMessage()
{
	return message;
}

void
ForwardSingleMessageRequest::setMessage(std::string message)
{
	this->message = message;
}

ChatSimpleMessage
ForwardSingleMessageRequest::getChatSimpleMessage()
{
	return chat_simple_message;
}

void
ForwardSingleMessageRequest::setChatSimpleMessage(ChatSimpleMessage chat_simple_message)
{
	this->chat_simple_message = chat_simple_message;
}

std::list<std::string>
ForwardSingleMessageRequest::getTo()
{
	return to;
}

void
ForwardSingleMessageRequest::setTo(std::list<std::string> to)
{
	this->to = to;
}

int
ForwardSingleMessageRequest::getPrevScrollNumber()
{
	return prev_scroll_number;
}

void
ForwardSingleMessageRequest::setPrevScrollNumber(int prev_scroll_number)
{
	this->prev_scroll_number = prev_scroll_number;
}



