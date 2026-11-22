

#include "SendFileByWxidRequest.h"

using namespace Tiny;

SendFileByWxidRequest::SendFileByWxidRequest()
{
	user_name = std::string();
	files = std::list<std::string>();
}

SendFileByWxidRequest::SendFileByWxidRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendFileByWxidRequest::~SendFileByWxidRequest()
{

}

void
SendFileByWxidRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *user_nameKey = "user_name";

    if(object.has_key(user_nameKey))
    {
        bourne::json value = object[user_nameKey];



        jsonToValue(&user_name, value, "std::string");


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
SendFileByWxidRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["user_name"] = getUserName();





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
SendFileByWxidRequest::getUserName()
{
	return user_name;
}

void
SendFileByWxidRequest::setUserName(std::string user_name)
{
	this->user_name = user_name;
}

std::list<std::string>
SendFileByWxidRequest::getFiles()
{
	return files;
}

void
SendFileByWxidRequest::setFiles(std::list<std::string> files)
{
	this->files = files;
}



