

#include "SendVoiceMessageByWxidRequest.h"

using namespace Tiny;

SendVoiceMessageByWxidRequest::SendVoiceMessageByWxidRequest()
{
	user_name = std::string();
	file_path = std::string();
}

SendVoiceMessageByWxidRequest::SendVoiceMessageByWxidRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendVoiceMessageByWxidRequest::~SendVoiceMessageByWxidRequest()
{

}

void
SendVoiceMessageByWxidRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *user_nameKey = "user_name";

    if(object.has_key(user_nameKey))
    {
        bourne::json value = object[user_nameKey];



        jsonToValue(&user_name, value, "std::string");


    }

    const char *file_pathKey = "file_path";

    if(object.has_key(file_pathKey))
    {
        bourne::json value = object[file_pathKey];



        jsonToValue(&file_path, value, "std::string");


    }


}

bourne::json
SendVoiceMessageByWxidRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["user_name"] = getUserName();






    object["file_path"] = getFilePath();



    return object;

}

std::string
SendVoiceMessageByWxidRequest::getUserName()
{
	return user_name;
}

void
SendVoiceMessageByWxidRequest::setUserName(std::string user_name)
{
	this->user_name = user_name;
}

std::string
SendVoiceMessageByWxidRequest::getFilePath()
{
	return file_path;
}

void
SendVoiceMessageByWxidRequest::setFilePath(std::string file_path)
{
	this->file_path = file_path;
}



