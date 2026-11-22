

#include "Color.h"

using namespace Tiny;

Color::Color()
{
	r = int(0);
	g = int(0);
	b = int(0);
	a = int(0);
	isKnownColor = bool(false);
	isEmpty = bool(false);
	isNamedColor = bool(false);
	isSystemColor = bool(false);
	name = std::string();
}

Color::Color(std::string jsonString)
{
	this->fromJson(jsonString);
}

Color::~Color()
{

}

void
Color::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *rKey = "r";

    if(object.has_key(rKey))
    {
        bourne::json value = object[rKey];



        jsonToValue(&r, value, "int");


    }

    const char *gKey = "g";

    if(object.has_key(gKey))
    {
        bourne::json value = object[gKey];



        jsonToValue(&g, value, "int");


    }

    const char *bKey = "b";

    if(object.has_key(bKey))
    {
        bourne::json value = object[bKey];



        jsonToValue(&b, value, "int");


    }

    const char *aKey = "a";

    if(object.has_key(aKey))
    {
        bourne::json value = object[aKey];



        jsonToValue(&a, value, "int");


    }

    const char *isKnownColorKey = "isKnownColor";

    if(object.has_key(isKnownColorKey))
    {
        bourne::json value = object[isKnownColorKey];



        jsonToValue(&isKnownColor, value, "bool");


    }

    const char *isEmptyKey = "isEmpty";

    if(object.has_key(isEmptyKey))
    {
        bourne::json value = object[isEmptyKey];



        jsonToValue(&isEmpty, value, "bool");


    }

    const char *isNamedColorKey = "isNamedColor";

    if(object.has_key(isNamedColorKey))
    {
        bourne::json value = object[isNamedColorKey];



        jsonToValue(&isNamedColor, value, "bool");


    }

    const char *isSystemColorKey = "isSystemColor";

    if(object.has_key(isSystemColorKey))
    {
        bourne::json value = object[isSystemColorKey];



        jsonToValue(&isSystemColor, value, "bool");


    }

    const char *nameKey = "name";

    if(object.has_key(nameKey))
    {
        bourne::json value = object[nameKey];



        jsonToValue(&name, value, "std::string");


    }


}

bourne::json
Color::toJson()
{
    bourne::json object = bourne::json::object();





    object["r"] = getR();






    object["g"] = getG();






    object["b"] = getB();






    object["a"] = getA();






    object["isKnownColor"] = isIsKnownColor();






    object["isEmpty"] = isIsEmpty();






    object["isNamedColor"] = isIsNamedColor();






    object["isSystemColor"] = isIsSystemColor();






    object["name"] = getName();



    return object;

}

int
Color::getR()
{
	return r;
}

void
Color::setR(int r)
{
	this->r = r;
}

int
Color::getG()
{
	return g;
}

void
Color::setG(int g)
{
	this->g = g;
}

int
Color::getB()
{
	return b;
}

void
Color::setB(int b)
{
	this->b = b;
}

int
Color::getA()
{
	return a;
}

void
Color::setA(int a)
{
	this->a = a;
}

bool
Color::isIsKnownColor()
{
	return isKnownColor;
}

void
Color::setIsKnownColor(bool isKnownColor)
{
	this->isKnownColor = isKnownColor;
}

bool
Color::isIsEmpty()
{
	return isEmpty;
}

void
Color::setIsEmpty(bool isEmpty)
{
	this->isEmpty = isEmpty;
}

bool
Color::isIsNamedColor()
{
	return isNamedColor;
}

void
Color::setIsNamedColor(bool isNamedColor)
{
	this->isNamedColor = isNamedColor;
}

bool
Color::isIsSystemColor()
{
	return isSystemColor;
}

void
Color::setIsSystemColor(bool isSystemColor)
{
	this->isSystemColor = isSystemColor;
}

std::string
Color::getName()
{
	return name;
}

void
Color::setName(std::string name)
{
	this->name = name;
}



