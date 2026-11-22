

#include "UpdateGroupNoticeRequest.h"

using namespace Tiny;

UpdateGroupNoticeRequest::UpdateGroupNoticeRequest()
{
	group_name = std::string();
	group_notice = std::string();
}

UpdateGroupNoticeRequest::UpdateGroupNoticeRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

UpdateGroupNoticeRequest::~UpdateGroupNoticeRequest()
{

}

void
UpdateGroupNoticeRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *group_nameKey = "group_name";

    if(object.has_key(group_nameKey))
    {
        bourne::json value = object[group_nameKey];



        jsonToValue(&group_name, value, "std::string");


    }

    const char *group_noticeKey = "group_notice";

    if(object.has_key(group_noticeKey))
    {
        bourne::json value = object[group_noticeKey];



        jsonToValue(&group_notice, value, "std::string");


    }


}

bourne::json
UpdateGroupNoticeRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["group_name"] = getGroupName();






    object["group_notice"] = getGroupNotice();



    return object;

}

std::string
UpdateGroupNoticeRequest::getGroupName()
{
	return group_name;
}

void
UpdateGroupNoticeRequest::setGroupName(std::string group_name)
{
	this->group_name = group_name;
}

std::string
UpdateGroupNoticeRequest::getGroupNotice()
{
	return group_notice;
}

void
UpdateGroupNoticeRequest::setGroupNotice(std::string group_notice)
{
	this->group_notice = group_notice;
}



