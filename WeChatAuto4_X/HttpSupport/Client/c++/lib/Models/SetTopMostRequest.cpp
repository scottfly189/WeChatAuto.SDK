

#include "SetTopMostRequest.h"

using namespace Tiny;

SetTopMostRequest::SetTopMostRequest()
{
	who = std::string();
	setting = bool(false);
}

SetTopMostRequest::SetTopMostRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SetTopMostRequest::~SetTopMostRequest()
{

}

void
SetTopMostRequest::fromJson(std::string jsonObj)
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
SetTopMostRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();






    object["setting"] = isSetting();



    return object;

}

std::string
SetTopMostRequest::getWho()
{
	return who;
}

void
SetTopMostRequest::setWho(std::string who)
{
	this->who = who;
}

bool
SetTopMostRequest::isSetting()
{
	return setting;
}

void
SetTopMostRequest::setSetting(bool setting)
{
	this->setting = setting;
}



