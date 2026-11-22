

#include "DatabaseQueryRequest.h"

using namespace Tiny;

DatabaseQueryRequest::DatabaseQueryRequest()
{
	sql = std::string();
}

DatabaseQueryRequest::DatabaseQueryRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

DatabaseQueryRequest::~DatabaseQueryRequest()
{

}

void
DatabaseQueryRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *sqlKey = "sql";

    if(object.has_key(sqlKey))
    {
        bourne::json value = object[sqlKey];



        jsonToValue(&sql, value, "std::string");


    }


}

bourne::json
DatabaseQueryRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["sql"] = getSql();



    return object;

}

std::string
DatabaseQueryRequest::getSql()
{
	return sql;
}

void
DatabaseQueryRequest::setSql(std::string sql)
{
	this->sql = sql;
}



