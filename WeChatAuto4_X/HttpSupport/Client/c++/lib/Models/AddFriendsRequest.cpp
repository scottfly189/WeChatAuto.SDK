

#include "AddFriendsRequest.h"

using namespace Tiny;

AddFriendsRequest::AddFriendsRequest()
{
	friends = std::list<std::string>();
	options = AddFriendsOptions();
}

AddFriendsRequest::AddFriendsRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

AddFriendsRequest::~AddFriendsRequest()
{

}

void
AddFriendsRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *friendsKey = "friends";

    if(object.has_key(friendsKey))
    {
        bourne::json value = object[friendsKey];


        std::list<std::string> friends_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            friends_list.push_back(element);
        }
        friends = friends_list;


    }

    const char *optionsKey = "options";

    if(object.has_key(optionsKey))
    {
        bourne::json value = object[optionsKey];




        AddFriendsOptions* obj = &options;
		obj->fromJson(value.dump());

    }


}

bourne::json
AddFriendsRequest::toJson()
{
    bourne::json object = bourne::json::object();




    std::list<std::string> friends_list = getFriends();
    bourne::json friends_arr = bourne::json::array();

    for(auto& var : friends_list)
    {
        friends_arr.append(var);
    }
    object["friends"] = friends_arr;










	object["options"] = getOptions().toJson();


    return object;

}

std::list<std::string>
AddFriendsRequest::getFriends()
{
	return friends;
}

void
AddFriendsRequest::setFriends(std::list<std::string> friends)
{
	this->friends = friends;
}

AddFriendsOptions
AddFriendsRequest::getOptions()
{
	return options;
}

void
AddFriendsRequest::setOptions(AddFriendsOptions options)
{
	this->options = options;
}



