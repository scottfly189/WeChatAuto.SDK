

#include "SendEmojiRequest.h"

using namespace Tiny;

SendEmojiRequest::SendEmojiRequest()
{
	who = std::string();
	emoji = std::string();
	index = int(0);
	at_users = std::list<std::string>();
}

SendEmojiRequest::SendEmojiRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendEmojiRequest::~SendEmojiRequest()
{

}

void
SendEmojiRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


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
SendEmojiRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();






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
SendEmojiRequest::getWho()
{
	return who;
}

void
SendEmojiRequest::setWho(std::string who)
{
	this->who = who;
}

std::string
SendEmojiRequest::getEmoji()
{
	return emoji;
}

void
SendEmojiRequest::setEmoji(std::string emoji)
{
	this->emoji = emoji;
}

int
SendEmojiRequest::getIndex()
{
	return index;
}

void
SendEmojiRequest::setIndex(int index)
{
	this->index = index;
}

std::list<std::string>
SendEmojiRequest::getAtUsers()
{
	return at_users;
}

void
SendEmojiRequest::setAtUsers(std::list<std::string> at_users)
{
	this->at_users = at_users;
}



