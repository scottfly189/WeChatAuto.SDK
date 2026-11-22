

#include "ChatType.h"

using namespace Tiny;

ChatType::ChatType()
{
}

ChatType::ChatType(std::string jsonString)
{
	this->fromJson(jsonString);
}

ChatType::~ChatType()
{

}

void
ChatType::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);


}

bourne::json
ChatType::toJson()
{
    bourne::json object = bourne::json::object();


    return object;

}



