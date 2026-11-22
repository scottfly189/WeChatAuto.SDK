

#include "CreateGroupRequest.h"

using namespace Tiny;

CreateGroupRequest::CreateGroupRequest()
{
	group_name = std::string();
	first_who = std::string();
	members = std::list<std::string>();
}

CreateGroupRequest::CreateGroupRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

CreateGroupRequest::~CreateGroupRequest()
{

}

void
CreateGroupRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *group_nameKey = "group_name";

    if(object.has_key(group_nameKey))
    {
        bourne::json value = object[group_nameKey];



        jsonToValue(&group_name, value, "std::string");


    }

    const char *first_whoKey = "first_who";

    if(object.has_key(first_whoKey))
    {
        bourne::json value = object[first_whoKey];



        jsonToValue(&first_who, value, "std::string");


    }

    const char *membersKey = "members";

    if(object.has_key(membersKey))
    {
        bourne::json value = object[membersKey];


        std::list<std::string> members_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            members_list.push_back(element);
        }
        members = members_list;


    }


}

bourne::json
CreateGroupRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["group_name"] = getGroupName();






    object["first_who"] = getFirstWho();





    std::list<std::string> members_list = getMembers();
    bourne::json members_arr = bourne::json::array();

    for(auto& var : members_list)
    {
        members_arr.append(var);
    }
    object["members"] = members_arr;






    return object;

}

std::string
CreateGroupRequest::getGroupName()
{
	return group_name;
}

void
CreateGroupRequest::setGroupName(std::string group_name)
{
	this->group_name = group_name;
}

std::string
CreateGroupRequest::getFirstWho()
{
	return first_who;
}

void
CreateGroupRequest::setFirstWho(std::string first_who)
{
	this->first_who = first_who;
}

std::list<std::string>
CreateGroupRequest::getMembers()
{
	return members;
}

void
CreateGroupRequest::setMembers(std::list<std::string> members)
{
	this->members = members;
}



