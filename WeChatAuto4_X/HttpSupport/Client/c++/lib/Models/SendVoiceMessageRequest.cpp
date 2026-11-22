

#include "SendVoiceMessageRequest.h"

using namespace Tiny;

SendVoiceMessageRequest::SendVoiceMessageRequest()
{
	who = std::string();
	file_path = std::string();
}

SendVoiceMessageRequest::SendVoiceMessageRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendVoiceMessageRequest::~SendVoiceMessageRequest()
{

}

void
SendVoiceMessageRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


    }

    const char *file_pathKey = "file_path";

    if(object.has_key(file_pathKey))
    {
        bourne::json value = object[file_pathKey];



        jsonToValue(&file_path, value, "std::string");


    }


}

bourne::json
SendVoiceMessageRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();






    object["file_path"] = getFilePath();



    return object;

}

std::string
SendVoiceMessageRequest::getWho()
{
	return who;
}

void
SendVoiceMessageRequest::setWho(std::string who)
{
	this->who = who;
}

std::string
SendVoiceMessageRequest::getFilePath()
{
	return file_path;
}

void
SendVoiceMessageRequest::setFilePath(std::string file_path)
{
	this->file_path = file_path;
}



