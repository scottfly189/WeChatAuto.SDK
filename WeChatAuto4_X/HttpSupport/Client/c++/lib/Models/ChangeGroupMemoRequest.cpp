

#include "ChangeGroupMemoRequest.h"

using namespace Tiny;

ChangeGroupMemoRequest::ChangeGroupMemoRequest()
{
	group_name = std::string();
	new_memo = std::string();
}

ChangeGroupMemoRequest::ChangeGroupMemoRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

ChangeGroupMemoRequest::~ChangeGroupMemoRequest()
{

}

void
ChangeGroupMemoRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *group_nameKey = "group_name";

    if(object.has_key(group_nameKey))
    {
        bourne::json value = object[group_nameKey];



        jsonToValue(&group_name, value, "std::string");


    }

    const char *new_memoKey = "new_memo";

    if(object.has_key(new_memoKey))
    {
        bourne::json value = object[new_memoKey];



        jsonToValue(&new_memo, value, "std::string");


    }


}

bourne::json
ChangeGroupMemoRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["group_name"] = getGroupName();






    object["new_memo"] = getNewMemo();



    return object;

}

std::string
ChangeGroupMemoRequest::getGroupName()
{
	return group_name;
}

void
ChangeGroupMemoRequest::setGroupName(std::string group_name)
{
	this->group_name = group_name;
}

std::string
ChangeGroupMemoRequest::getNewMemo()
{
	return new_memo;
}

void
ChangeGroupMemoRequest::setNewMemo(std::string new_memo)
{
	this->new_memo = new_memo;
}



