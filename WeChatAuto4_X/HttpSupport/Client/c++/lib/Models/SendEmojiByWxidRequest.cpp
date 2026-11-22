

#include "SendEmojiByWxidRequest.h"

using namespace Tiny;

SendEmojiByWxidRequest::SendEmojiByWxidRequest()
{
	user_name = std::string();
	emoji = std::string();
	index = int(0);
	at_users = std::list<std::string>();
}

SendEmojiByWxidRequest::SendEmojiByWxidRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendEmojiByWxidRequest::~SendEmojiByWxidRequest()
{

}

void
SendEmojiByWxidRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *user_nameKey = "user_name";

    if(object.has_key(user_nameKey))
    {
        bourne::json value = object[user_nameKey];



        jsonToValue(&user_name, value, "std::string");


    }

    const char *emojiKey = "emoji";

    if(object.has_key(emojiKey))
    {
        bourne::json value = object[emojiKey];



        jsonToValue(&emoji, value, "std::string");


    }

    const char *indexKey = "index";

    if(object.has_key(indexKey))
    {
        bourne::json value = object[indexKey];



        jsonToValue(&index, value, "int");


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


}

bourne::json
SendEmojiByWxidRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["user_name"] = getUserName();






    object["emoji"] = getEmoji();






    object["index"] = getIndex();





    std::list<std::string> at_users_list = getAtUsers();
    bourne::json at_users_arr = bourne::json::array();

    for(auto& var : at_users_list)
    {
        at_users_arr.append(var);
    }
    object["at_users"] = at_users_arr;






    return object;

}

std::string
SendEmojiByWxidRequest::getUserName()
{
	return user_name;
}

void
SendEmojiByWxidRequest::setUserName(std::string user_name)
{
	this->user_name = user_name;
}

std::string
SendEmojiByWxidRequest::getEmoji()
{
	return emoji;
}

void
SendEmojiByWxidRequest::setEmoji(std::string emoji)
{
	this->emoji = emoji;
}

int
SendEmojiByWxidRequest::getIndex()
{
	return index;
}

void
SendEmojiByWxidRequest::setIndex(int index)
{
	this->index = index;
}

std::list<std::string>
SendEmojiByWxidRequest::getAtUsers()
{
	return at_users;
}

void
SendEmojiByWxidRequest::setAtUsers(std::list<std::string> at_users)
{
	this->at_users = at_users;
}



