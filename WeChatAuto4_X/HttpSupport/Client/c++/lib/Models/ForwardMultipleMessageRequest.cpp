

#include "ForwardMultipleMessageRequest.h"

using namespace Tiny;

ForwardMultipleMessageRequest::ForwardMultipleMessageRequest()
{
	who = std::string();
	to = std::list<std::string>();
	f_type = ForwardMessageTypeEnums();
	row_count = int(0);
}

ForwardMultipleMessageRequest::ForwardMultipleMessageRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

ForwardMultipleMessageRequest::~ForwardMultipleMessageRequest()
{

}

void
ForwardMultipleMessageRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


    }

    const char *toKey = "to";

    if(object.has_key(toKey))
    {
        bourne::json value = object[toKey];


        std::list<std::string> to_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            to_list.push_back(element);
        }
        to = to_list;


    }

    const char *f_typeKey = "f_type";

    if(object.has_key(f_typeKey))
    {
        bourne::json value = object[f_typeKey];




        ForwardMessageTypeEnums* obj = &f_type;
		obj->fromJson(value.dump());

    }

    const char *row_countKey = "row_count";

    if(object.has_key(row_countKey))
    {
        bourne::json value = object[row_countKey];



        jsonToValue(&row_count, value, "int");


    }


}

bourne::json
ForwardMultipleMessageRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();





    std::list<std::string> to_list = getTo();
    bourne::json to_arr = bourne::json::array();

    for(auto& var : to_list)
    {
        to_arr.append(var);
    }
    object["to"] = to_arr;










	object["f_type"] = getFType().toJson();





    object["row_count"] = getRowCount();



    return object;

}

std::string
ForwardMultipleMessageRequest::getWho()
{
	return who;
}

void
ForwardMultipleMessageRequest::setWho(std::string who)
{
	this->who = who;
}

std::list<std::string>
ForwardMultipleMessageRequest::getTo()
{
	return to;
}

void
ForwardMultipleMessageRequest::setTo(std::list<std::string> to)
{
	this->to = to;
}

ForwardMessageTypeEnums
ForwardMultipleMessageRequest::getFType()
{
	return f_type;
}

void
ForwardMultipleMessageRequest::setFType(ForwardMessageTypeEnums f_type)
{
	this->f_type = f_type;
}

int
ForwardMultipleMessageRequest::getRowCount()
{
	return row_count;
}

void
ForwardMultipleMessageRequest::setRowCount(int row_count)
{
	this->row_count = row_count;
}



