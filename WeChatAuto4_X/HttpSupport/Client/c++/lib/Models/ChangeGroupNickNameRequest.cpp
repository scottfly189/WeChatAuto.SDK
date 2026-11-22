

#include "ChangeGroupNickNameRequest.h"

using namespace Tiny;

ChangeGroupNickNameRequest::ChangeGroupNickNameRequest()
{
	group_name = std::string();
	nick_name = std::string();
}

ChangeGroupNickNameRequest::ChangeGroupNickNameRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

ChangeGroupNickNameRequest::~ChangeGroupNickNameRequest()
{

}

void
ChangeGroupNickNameRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *group_nameKey = "group_name";

    if(object.has_key(group_nameKey))
    {
        bourne::json value = object[group_nameKey];



        jsonToValue(&group_name, value, "std::string");


    }

    const char *nick_nameKey = "nick_name";

    if(object.has_key(nick_nameKey))
    {
        bourne::json value = object[nick_nameKey];



        jsonToValue(&nick_name, value, "std::string");


    }


}

bourne::json
ChangeGroupNickNameRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["group_name"] = getGroupName();






    object["nick_name"] = getNickName();



    return object;

}

std::string
ChangeGroupNickNameRequest::getGroupName()
{
	return group_name;
}

void
ChangeGroupNickNameRequest::setGroupName(std::string group_name)
{
	this->group_name = group_name;
}

std::string
ChangeGroupNickNameRequest::getNickName()
{
	return nick_name;
}

void
ChangeGroupNickNameRequest::setNickName(std::string nick_name)
{
	this->nick_name = nick_name;
}



