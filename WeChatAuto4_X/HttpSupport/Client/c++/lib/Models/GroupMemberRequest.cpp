

#include "GroupMemberRequest.h"

using namespace Tiny;

GroupMemberRequest::GroupMemberRequest()
{
	group_name = std::string();
	members = std::list<std::string>();
}

GroupMemberRequest::GroupMemberRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

GroupMemberRequest::~GroupMemberRequest()
{

}

void
GroupMemberRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *group_nameKey = "group_name";

    if(object.has_key(group_nameKey))
    {
        bourne::json value = object[group_nameKey];



        jsonToValue(&group_name, value, "std::string");


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
GroupMemberRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["group_name"] = getGroupName();





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
GroupMemberRequest::getGroupName()
{
	return group_name;
}

void
GroupMemberRequest::setGroupName(std::string group_name)
{
	this->group_name = group_name;
}

std::list<std::string>
GroupMemberRequest::getMembers()
{
	return members;
}

void
GroupMemberRequest::setMembers(std::list<std::string> members)
{
	this->members = members;
}



