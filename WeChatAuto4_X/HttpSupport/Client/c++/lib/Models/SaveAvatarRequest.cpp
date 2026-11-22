

#include "SaveAvatarRequest.h"

using namespace Tiny;

SaveAvatarRequest::SaveAvatarRequest()
{
	path = std::string();
}

SaveAvatarRequest::SaveAvatarRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SaveAvatarRequest::~SaveAvatarRequest()
{

}

void
SaveAvatarRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *pathKey = "path";

    if(object.has_key(pathKey))
    {
        bourne::json value = object[pathKey];



        jsonToValue(&path, value, "std::string");


    }


}

bourne::json
SaveAvatarRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["path"] = getPath();



    return object;

}

std::string
SaveAvatarRequest::getPath()
{
	return path;
}

void
SaveAvatarRequest::setPath(std::string path)
{
	this->path = path;
}



