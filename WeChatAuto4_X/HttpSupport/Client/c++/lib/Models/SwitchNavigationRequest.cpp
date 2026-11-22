

#include "SwitchNavigationRequest.h"

using namespace Tiny;

SwitchNavigationRequest::SwitchNavigationRequest()
{
	navigation_type = NavigationType();
}

SwitchNavigationRequest::SwitchNavigationRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SwitchNavigationRequest::~SwitchNavigationRequest()
{

}

void
SwitchNavigationRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *navigation_typeKey = "navigation_type";

    if(object.has_key(navigation_typeKey))
    {
        bourne::json value = object[navigation_typeKey];




        NavigationType* obj = &navigation_type;
		obj->fromJson(value.dump());

    }


}

bourne::json
SwitchNavigationRequest::toJson()
{
    bourne::json object = bourne::json::object();






	object["navigation_type"] = getNavigationType().toJson();


    return object;

}

NavigationType
SwitchNavigationRequest::getNavigationType()
{
	return navigation_type;
}

void
SwitchNavigationRequest::setNavigationType(NavigationType navigation_type)
{
	this->navigation_type = navigation_type;
}



