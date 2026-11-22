

#include "InviteGroupMemberRequest.h"

using namespace Tiny;

InviteGroupMemberRequest::InviteGroupMemberRequest()
{
	group_name = std::string();
	members = std::list<std::string>();
	invite_reason = std::string();
}

InviteGroupMemberRequest::InviteGroupMemberRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

InviteGroupMemberRequest::~InviteGroupMemberRequest()
{

}

void
InviteGroupMemberRequest::fromJson(std::string jsonObj)
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

    const char *invite_reasonKey = "invite_reason";

    if(object.has_key(invite_reasonKey))
    {
        bourne::json value = object[invite_reasonKey];



        jsonToValue(&invite_reason, value, "std::string");


    }


}

bourne::json
InviteGroupMemberRequest::toJson()
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









    object["invite_reason"] = getInviteReason();



    return object;

}

std::string
InviteGroupMemberRequest::getGroupName()
{
	return group_name;
}

void
InviteGroupMemberRequest::setGroupName(std::string group_name)
{
	this->group_name = group_name;
}

std::list<std::string>
InviteGroupMemberRequest::getMembers()
{
	return members;
}

void
InviteGroupMemberRequest::setMembers(std::list<std::string> members)
{
	this->members = members;
}

std::string
InviteGroupMemberRequest::getInviteReason()
{
	return invite_reason;
}

void
InviteGroupMemberRequest::setInviteReason(std::string invite_reason)
{
	this->invite_reason = invite_reason;
}



