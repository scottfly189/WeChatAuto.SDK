

#include "SetDoNotDisturbRequest.h"

using namespace Tiny;

SetDoNotDisturbRequest::SetDoNotDisturbRequest()
{
	who = std::string();
	setting = bool(false);
}

SetDoNotDisturbRequest::SetDoNotDisturbRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SetDoNotDisturbRequest::~SetDoNotDisturbRequest()
{

}

void
SetDoNotDisturbRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


    }

    const char *settingKey = "setting";

    if(object.has_key(settingKey))
    {
        bourne::json value = object[settingKey];



        jsonToValue(&setting, value, "bool");


    }


}

bourne::json
SetDoNotDisturbRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();






    object["setting"] = isSetting();



    return object;

}

std::string
SetDoNotDisturbRequest::getWho()
{
	return who;
}

void
SetDoNotDisturbRequest::setWho(std::string who)
{
	this->who = who;
}

bool
SetDoNotDisturbRequest::isSetting()
{
	return setting;
}

void
SetDoNotDisturbRequest::setSetting(bool setting)
{
	this->setting = setting;
}



