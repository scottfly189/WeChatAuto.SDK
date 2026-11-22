

#include "NavigationType.h"

using namespace Tiny;

NavigationType::NavigationType()
{
}

NavigationType::NavigationType(std::string jsonString)
{
	this->fromJson(jsonString);
}

NavigationType::~NavigationType()
{

}

void
NavigationType::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);


}

bourne::json
NavigationType::toJson()
{
    bourne::json object = bourne::json::object();


    return object;

}



