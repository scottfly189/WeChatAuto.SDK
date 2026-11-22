

#include "NotifyIconClickRequest.h"

using namespace Tiny;

NotifyIconClickRequest::NotifyIconClickRequest()
{
	index = int(0);
	wechat_name = std::string();
}

NotifyIconClickRequest::NotifyIconClickRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

NotifyIconClickRequest::~NotifyIconClickRequest()
{

}

void
NotifyIconClickRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *indexKey = "index";

    if(object.has_key(indexKey))
    {
        bourne::json value = object[indexKey];



        jsonToValue(&index, value, "int");


    }

    const char *wechat_nameKey = "wechat_name";

    if(object.has_key(wechat_nameKey))
    {
        bourne::json value = object[wechat_nameKey];



        jsonToValue(&wechat_name, value, "std::string");


    }


}

bourne::json
NotifyIconClickRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["index"] = getIndex();






    object["wechat_name"] = getWechatName();



    return object;

}

int
NotifyIconClickRequest::getIndex()
{
	return index;
}

void
NotifyIconClickRequest::setIndex(int index)
{
	this->index = index;
}

std::string
NotifyIconClickRequest::getWechatName()
{
	return wechat_name;
}

void
NotifyIconClickRequest::setWechatName(std::string wechat_name)
{
	this->wechat_name = wechat_name;
}



