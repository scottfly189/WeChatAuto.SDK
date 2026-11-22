

#include "FriendKeywordRuleRequest.h"

using namespace Tiny;

FriendKeywordRuleRequest::FriendKeywordRuleRequest()
{
	suffix = std::string();
	label = std::string();
}

FriendKeywordRuleRequest::FriendKeywordRuleRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

FriendKeywordRuleRequest::~FriendKeywordRuleRequest()
{

}

void
FriendKeywordRuleRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

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
FriendKeywordRuleRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["suffix"] = getSuffix();






    object["label"] = getLabel();



    return object;

}

std::string
FriendKeywordRuleRequest::getSuffix()
{
	return suffix;
}

void
FriendKeywordRuleRequest::setSuffix(std::string suffix)
{
	this->suffix = suffix;
}

std::string
FriendKeywordRuleRequest::getLabel()
{
	return label;
}

void
FriendKeywordRuleRequest::setLabel(std::string label)
{
	this->label = label;
}



