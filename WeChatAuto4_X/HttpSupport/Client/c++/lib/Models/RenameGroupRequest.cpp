

#include "RenameGroupRequest.h"

using namespace Tiny;

RenameGroupRequest::RenameGroupRequest()
{
	old_group_name = std::string();
	new_group_name = std::string();
}

RenameGroupRequest::RenameGroupRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

RenameGroupRequest::~RenameGroupRequest()
{

}

void
RenameGroupRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *old_group_nameKey = "old_group_name";

    if(object.has_key(old_group_nameKey))
    {
        bourne::json value = object[old_group_nameKey];



        jsonToValue(&old_group_name, value, "std::string");


    }

    const char *new_group_nameKey = "new_group_name";

    if(object.has_key(new_group_nameKey))
    {
        bourne::json value = object[new_group_nameKey];



        jsonToValue(&new_group_name, value, "std::string");


    }


}

bourne::json
RenameGroupRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["old_group_name"] = getOldGroupName();






    object["new_group_name"] = getNewGroupName();



    return object;

}

std::string
RenameGroupRequest::getOldGroupName()
{
	return old_group_name;
}

void
RenameGroupRequest::setOldGroupName(std::string old_group_name)
{
	this->old_group_name = old_group_name;
}

std::string
RenameGroupRequest::getNewGroupName()
{
	return new_group_name;
}

void
RenameGroupRequest::setNewGroupName(std::string new_group_name)
{
	this->new_group_name = new_group_name;
}



