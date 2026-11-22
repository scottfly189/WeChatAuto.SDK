

#include "AddFriendsOptions.h"

using namespace Tiny;

AddFriendsOptions::AddFriendsOptions()
{
	intervalTime = int(0);
	isCloseWin = bool(false);
	sayHi = std::string();
	suffix = std::string();
	label = std::string();
}

AddFriendsOptions::AddFriendsOptions(std::string jsonString)
{
	this->fromJson(jsonString);
}

AddFriendsOptions::~AddFriendsOptions()
{

}

void
AddFriendsOptions::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *intervalTimeKey = "intervalTime";

    if(object.has_key(intervalTimeKey))
    {
        bourne::json value = object[intervalTimeKey];



        jsonToValue(&intervalTime, value, "int");


    }

    const char *isCloseWinKey = "isCloseWin";

    if(object.has_key(isCloseWinKey))
    {
        bourne::json value = object[isCloseWinKey];



        jsonToValue(&isCloseWin, value, "bool");


    }

    const char *sayHiKey = "sayHi";

    if(object.has_key(sayHiKey))
    {
        bourne::json value = object[sayHiKey];



        jsonToValue(&sayHi, value, "std::string");


    }

    const char *suffixKey = "suffix";

    if(object.has_key(suffixKey))
    {
        bourne::json value = object[suffixKey];



        jsonToValue(&suffix, value, "std::string");


    }

    const char *labelKey = "label";

    if(object.has_key(labelKey))
    {
        bourne::json value = object[labelKey];



        jsonToValue(&label, value, "std::string");


    }


}

bourne::json
AddFriendsOptions::toJson()
{
    bourne::json object = bourne::json::object();





    object["intervalTime"] = getIntervalTime();






    object["isCloseWin"] = isIsCloseWin();






    object["sayHi"] = getSayHi();






    object["suffix"] = getSuffix();






    object["label"] = getLabel();



    return object;

}

int
AddFriendsOptions::getIntervalTime()
{
	return intervalTime;
}

void
AddFriendsOptions::setIntervalTime(int intervalTime)
{
	this->intervalTime = intervalTime;
}

bool
AddFriendsOptions::isIsCloseWin()
{
	return isCloseWin;
}

void
AddFriendsOptions::setIsCloseWin(bool isCloseWin)
{
	this->isCloseWin = isCloseWin;
}

std::string
AddFriendsOptions::getSayHi()
{
	return sayHi;
}

void
AddFriendsOptions::setSayHi(std::string sayHi)
{
	this->sayHi = sayHi;
}

std::string
AddFriendsOptions::getSuffix()
{
	return suffix;
}

void
AddFriendsOptions::setSuffix(std::string suffix)
{
	this->suffix = suffix;
}

std::string
AddFriendsOptions::getLabel()
{
	return label;
}

void
AddFriendsOptions::setLabel(std::string label)
{
	this->label = label;
}



