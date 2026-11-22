

#include "SendFileRequest.h"

using namespace Tiny;

SendFileRequest::SendFileRequest()
{
	who = std::string();
	files = std::list<std::string>();
}

SendFileRequest::SendFileRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendFileRequest::~SendFileRequest()
{

}

void
SendFileRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


    }

    const char *filesKey = "files";

    if(object.has_key(filesKey))
    {
        bourne::json value = object[filesKey];


        std::list<std::string> files_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            files_list.push_back(element);
        }
        files = files_list;


    }


}

bourne::json
SendFileRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();





    std::list<std::string> files_list = getFiles();
    bourne::json files_arr = bourne::json::array();

    for(auto& var : files_list)
    {
        files_arr.append(var);
    }
    object["files"] = files_arr;






    return object;

}

std::string
SendFileRequest::getWho()
{
	return who;
}

void
SendFileRequest::setWho(std::string who)
{
	this->who = who;
}

std::list<std::string>
SendFileRequest::getFiles()
{
	return files;
}

void
SendFileRequest::setFiles(std::list<std::string> files)
{
	this->files = files;
}



