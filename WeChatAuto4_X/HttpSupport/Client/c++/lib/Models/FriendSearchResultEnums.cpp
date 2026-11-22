

#include "FriendSearchResultEnums.h"

using namespace Tiny;

FriendSearchResultEnums::FriendSearchResultEnums()
{
}

FriendSearchResultEnums::FriendSearchResultEnums(std::string jsonString)
{
	this->fromJson(jsonString);
}

FriendSearchResultEnums::~FriendSearchResultEnums()
{

}

void
FriendSearchResultEnums::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);


}

bourne::json
FriendSearchResultEnums::toJson()
{
    bourne::json object = bourne::json::object();


    return object;

}



