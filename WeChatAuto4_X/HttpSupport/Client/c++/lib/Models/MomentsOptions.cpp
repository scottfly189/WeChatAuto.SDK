

#include "MomentsOptions.h"

using namespace Tiny;

MomentsOptions::MomentsOptions()
{
	atUsrs = std::list<std::string>();
	labels = std::list<std::string>();
	isCloseMoments = bool(false);
}

MomentsOptions::MomentsOptions(std::string jsonString)
{
	this->fromJson(jsonString);
}

MomentsOptions::~MomentsOptions()
{

}

void
MomentsOptions::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *atUsrsKey = "atUsrs";

    if(object.has_key(atUsrsKey))
    {
        bourne::json value = object[atUsrsKey];


        std::list<std::string> atUsrs_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            atUsrs_list.push_back(element);
        }
        atUsrs = atUsrs_list;


    }

    const char *labelsKey = "labels";

    if(object.has_key(labelsKey))
    {
        bourne::json value = object[labelsKey];


        std::list<std::string> labels_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            labels_list.push_back(element);
        }
        labels = labels_list;


    }

    const char *isCloseMomentsKey = "isCloseMoments";

    if(object.has_key(isCloseMomentsKey))
    {
        bourne::json value = object[isCloseMomentsKey];



        jsonToValue(&isCloseMoments, value, "bool");


    }


}

bourne::json
MomentsOptions::toJson()
{
    bourne::json object = bourne::json::object();




    std::list<std::string> atUsrs_list = getAtUsrs();
    bourne::json atUsrs_arr = bourne::json::array();

    for(auto& var : atUsrs_list)
    {
        atUsrs_arr.append(var);
    }
    object["atUsrs"] = atUsrs_arr;








    std::list<std::string> labels_list = getLabels();
    bourne::json labels_arr = bourne::json::array();

    for(auto& var : labels_list)
    {
        labels_arr.append(var);
    }
    object["labels"] = labels_arr;









    object["isCloseMoments"] = isIsCloseMoments();



    return object;

}

std::list<std::string>
MomentsOptions::getAtUsrs()
{
	return atUsrs;
}

void
MomentsOptions::setAtUsrs(std::list<std::string> atUsrs)
{
	this->atUsrs = atUsrs;
}

std::list<std::string>
MomentsOptions::getLabels()
{
	return labels;
}

void
MomentsOptions::setLabels(std::list<std::string> labels)
{
	this->labels = labels;
}

bool
MomentsOptions::isIsCloseMoments()
{
	return isCloseMoments;
}

void
MomentsOptions::setIsCloseMoments(bool isCloseMoments)
{
	this->isCloseMoments = isCloseMoments;
}



