

#include "PassedAllNewFriendRequest.h"

using namespace Tiny;

PassedAllNewFriendRequest::PassedAllNewFriendRequest()
{
	keyword = std::list<std::string>();
	suffix = std::string();
	label = std::string();
	passed_delete = bool(false);
	keyword_rules = null<FriendKeywordRuleRequest>();
}

PassedAllNewFriendRequest::PassedAllNewFriendRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

PassedAllNewFriendRequest::~PassedAllNewFriendRequest()
{

}

void
PassedAllNewFriendRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *keywordKey = "keyword";

    if(object.has_key(keywordKey))
    {
        bourne::json value = object[keywordKey];


        std::list<std::string> keyword_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            keyword_list.push_back(element);
        }
        keyword = keyword_list;


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

    const char *passed_deleteKey = "passed_delete";

    if(object.has_key(passed_deleteKey))
    {
        bourne::json value = object[passed_deleteKey];



        jsonToValue(&passed_delete, value, "bool");


    }

    const char *keyword_rulesKey = "keyword_rules";

    if(object.has_key(keyword_rulesKey))
    {
        bourne::json value = object[keyword_rulesKey];


    }


}

bourne::json
PassedAllNewFriendRequest::toJson()
{
    bourne::json object = bourne::json::object();




    std::list<std::string> keyword_list = getKeyword();
    bourne::json keyword_arr = bourne::json::array();

    for(auto& var : keyword_list)
    {
        keyword_arr.append(var);
    }
    object["keyword"] = keyword_arr;









    object["suffix"] = getSuffix();






    object["label"] = getLabel();






    object["passed_delete"] = isPassedDelete();






    return object;

}

std::list<std::string>
PassedAllNewFriendRequest::getKeyword()
{
	return keyword;
}

void
PassedAllNewFriendRequest::setKeyword(std::list<std::string> keyword)
{
	this->keyword = keyword;
}

std::string
PassedAllNewFriendRequest::getSuffix()
{
	return suffix;
}

void
PassedAllNewFriendRequest::setSuffix(std::string suffix)
{
	this->suffix = suffix;
}

std::string
PassedAllNewFriendRequest::getLabel()
{
	return label;
}

void
PassedAllNewFriendRequest::setLabel(std::string label)
{
	this->label = label;
}

bool
PassedAllNewFriendRequest::isPassedDelete()
{
	return passed_delete;
}

void
PassedAllNewFriendRequest::setPassedDelete(bool passed_delete)
{
	this->passed_delete = passed_delete;
}

std::map<std::string, FriendKeywordRuleRequest>
PassedAllNewFriendRequest::getKeywordRules()
{
	return keyword_rules;
}

void
PassedAllNewFriendRequest::setKeywordRules(std::map<std::string, FriendKeywordRuleRequest> keyword_rules)
{
	this->keyword_rules = keyword_rules;
}



