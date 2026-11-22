

#include "ColorPalette.h"

using namespace Tiny;

ColorPalette::ColorPalette()
{
	flags = int(0);
	entries = std::list<Color>();
}

ColorPalette::ColorPalette(std::string jsonString)
{
	this->fromJson(jsonString);
}

ColorPalette::~ColorPalette()
{

}

void
ColorPalette::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *flagsKey = "flags";

    if(object.has_key(flagsKey))
    {
        bourne::json value = object[flagsKey];



        jsonToValue(&flags, value, "int");


    }

    const char *entriesKey = "entries";

    if(object.has_key(entriesKey))
    {
        bourne::json value = object[entriesKey];


        std::list<Color> entries_list;
        Color element;
        for(auto& var : value.array_range())
        {


            element.fromJson(var.dump());

            entries_list.push_back(element);
        }
        entries = entries_list;


    }


}

bourne::json
ColorPalette::toJson()
{
    bourne::json object = bourne::json::object();





    object["flags"] = getFlags();





    std::list<Color> entries_list = getEntries();
    bourne::json entries_arr = bourne::json::array();

    for(auto& var : entries_list)
    {
        Color obj = var;
        entries_arr.append(obj.toJson());
    }
    object["entries"] = entries_arr;




    return object;

}

int
ColorPalette::getFlags()
{
	return flags;
}

void
ColorPalette::setFlags(int flags)
{
	this->flags = flags;
}

std::list<Color>
ColorPalette::getEntries()
{
	return entries;
}

void
ColorPalette::setEntries(std::list<Color> entries)
{
	this->entries = entries;
}



