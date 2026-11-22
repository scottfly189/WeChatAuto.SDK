

#include "PropertyItem.h"

using namespace Tiny;

PropertyItem::PropertyItem()
{
	id = int(0);
	len = int(0);
	type = int(0);
	value = std::string();
}

PropertyItem::PropertyItem(std::string jsonString)
{
	this->fromJson(jsonString);
}

PropertyItem::~PropertyItem()
{

}

void
PropertyItem::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *idKey = "id";

    if(object.has_key(idKey))
    {
        bourne::json value = object[idKey];



        jsonToValue(&id, value, "int");


    }

    const char *lenKey = "len";

    if(object.has_key(lenKey))
    {
        bourne::json value = object[lenKey];



        jsonToValue(&len, value, "int");


    }

    const char *typeKey = "type";

    if(object.has_key(typeKey))
    {
        bourne::json value = object[typeKey];



        jsonToValue(&type, value, "int");


    }

    const char *valueKey = "value";

    if(object.has_key(valueKey))
    {
        bourne::json value = object[valueKey];




        ByteArray* obj = &value;
		obj->fromJson(value.dump());

    }


}

bourne::json
PropertyItem::toJson()
{
    bourne::json object = bourne::json::object();





    object["id"] = getId();






    object["len"] = getLen();






    object["type"] = getType();







	object["value"] = getValue().toJson();


    return object;

}

int
PropertyItem::getId()
{
	return id;
}

void
PropertyItem::setId(int id)
{
	this->id = id;
}

int
PropertyItem::getLen()
{
	return len;
}

void
PropertyItem::setLen(int len)
{
	this->len = len;
}

int
PropertyItem::getType()
{
	return type;
}

void
PropertyItem::setType(int type)
{
	this->type = type;
}

ByteArray
PropertyItem::getValue()
{
	return value;
}

void
PropertyItem::setValue(ByteArray value)
{
	this->value = value;
}



