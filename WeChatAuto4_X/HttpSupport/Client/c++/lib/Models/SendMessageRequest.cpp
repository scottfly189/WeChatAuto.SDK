

#include "SendMessageRequest.h"

using namespace Tiny;

SendMessageRequest::SendMessageRequest()
{
	who = std::string();
	message = std::string();
	at_users = std::list<std::string>();
	refer = ChatRefer();
}

SendMessageRequest::SendMessageRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendMessageRequest::~SendMessageRequest()
{

}

void
SendMessageRequest::fromJson(std::string jsonObj)
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

    const char *at_usersKey = "at_users";

    if(object.has_key(at_usersKey))
    {
        bourne::json value = object[at_usersKey];


        std::list<std::string> at_users_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            at_users_list.push_back(element);
        }
        at_users = at_users_list;


    }

    const char *referKey = "refer";

    if(object.has_key(referKey))
    {
        bourne::json value = object[referKey];




        ChatRefer* obj = &refer;
		obj->fromJson(value.dump());

    }


}

bourne::json
SendMessageRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();






    object["message"] = getMessage();





    std::list<std::string> at_users_list = getAtUsers();
    bourne::json at_users_arr = bourne::json::array();

    for(auto& var : at_users_list)
    {
        at_users_arr.append(var);
    }
    object["at_users"] = at_users_arr;










	object["refer"] = getRefer().toJson();


    return object;

}

std::string
SendMessageRequest::getWho()
{
	return who;
}

void
SendMessageRequest::setWho(std::string who)
{
	this->who = who;
}

std::string
SendMessageRequest::getMessage()
{
	return message;
}

void
SendMessageRequest::setMessage(std::string message)
{
	this->message = message;
}

std::list<std::string>
SendMessageRequest::getAtUsers()
{
	return at_users;
}

void
SendMessageRequest::setAtUsers(std::list<std::string> at_users)
{
	this->at_users = at_users;
}

ChatRefer
SendMessageRequest::getRefer()
{
	return refer;
}

void
SendMessageRequest::setRefer(ChatRefer refer)
{
	this->refer = refer;
}



