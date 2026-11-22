

#include "SendMessageByWxidRequest.h"

using namespace Tiny;

SendMessageByWxidRequest::SendMessageByWxidRequest()
{
	user_name = std::string();
	message = std::string();
	at_users = std::list<std::string>();
	refer = ChatRefer();
}

SendMessageByWxidRequest::SendMessageByWxidRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendMessageByWxidRequest::~SendMessageByWxidRequest()
{

}

void
SendMessageByWxidRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *user_nameKey = "user_name";

    if(object.has_key(user_nameKey))
    {
        bourne::json value = object[user_nameKey];



        jsonToValue(&user_name, value, "std::string");


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
SendMessageByWxidRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["user_name"] = getUserName();






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
SendMessageByWxidRequest::getUserName()
{
	return user_name;
}

void
SendMessageByWxidRequest::setUserName(std::string user_name)
{
	this->user_name = user_name;
}

std::string
SendMessageByWxidRequest::getMessage()
{
	return message;
}

void
SendMessageByWxidRequest::setMessage(std::string message)
{
	this->message = message;
}

std::list<std::string>
SendMessageByWxidRequest::getAtUsers()
{
	return at_users;
}

void
SendMessageByWxidRequest::setAtUsers(std::list<std::string> at_users)
{
	this->at_users = at_users;
}

ChatRefer
SendMessageByWxidRequest::getRefer()
{
	return refer;
}

void
SendMessageByWxidRequest::setRefer(ChatRefer refer)
{
	this->refer = refer;
}



