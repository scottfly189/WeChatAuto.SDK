

#include "AddMomentsRequest.h"

using namespace Tiny;

AddMomentsRequest::AddMomentsRequest()
{
	image_files = std::list<std::string>();
	content = std::string();
	options = MomentsOptions();
}

AddMomentsRequest::AddMomentsRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

AddMomentsRequest::~AddMomentsRequest()
{

}

void
AddMomentsRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *image_filesKey = "image_files";

    if(object.has_key(image_filesKey))
    {
        bourne::json value = object[image_filesKey];


        std::list<std::string> image_files_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            image_files_list.push_back(element);
        }
        image_files = image_files_list;


    }

    const char *contentKey = "content";

    if(object.has_key(contentKey))
    {
        bourne::json value = object[contentKey];



        jsonToValue(&content, value, "std::string");


    }

    const char *optionsKey = "options";

    if(object.has_key(optionsKey))
    {
        bourne::json value = object[optionsKey];




        MomentsOptions* obj = &options;
		obj->fromJson(value.dump());

    }


}

bourne::json
AddMomentsRequest::toJson()
{
    bourne::json object = bourne::json::object();




    std::list<std::string> image_files_list = getImageFiles();
    bourne::json image_files_arr = bourne::json::array();

    for(auto& var : image_files_list)
    {
        image_files_arr.append(var);
    }
    object["image_files"] = image_files_arr;









    object["content"] = getContent();







	object["options"] = getOptions().toJson();


    return object;

}

std::list<std::string>
AddMomentsRequest::getImageFiles()
{
	return image_files;
}

void
AddMomentsRequest::setImageFiles(std::list<std::string> image_files)
{
	this->image_files = image_files;
}

std::string
AddMomentsRequest::getContent()
{
	return content;
}

void
AddMomentsRequest::setContent(std::string content)
{
	this->content = content;
}

MomentsOptions
AddMomentsRequest::getOptions()
{
	return options;
}

void
AddMomentsRequest::setOptions(MomentsOptions options)
{
	this->options = options;
}



