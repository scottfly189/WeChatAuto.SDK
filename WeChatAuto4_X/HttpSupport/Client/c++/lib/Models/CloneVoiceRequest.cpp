

#include "CloneVoiceRequest.h"

using namespace Tiny;

CloneVoiceRequest::CloneVoiceRequest()
{
	api_key = std::string();
	audio_url = std::string();
	target_model = std::string();
	prefix = std::string();
}

CloneVoiceRequest::CloneVoiceRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

CloneVoiceRequest::~CloneVoiceRequest()
{

}

void
CloneVoiceRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *api_keyKey = "api_key";

    if(object.has_key(api_keyKey))
    {
        bourne::json value = object[api_keyKey];



        jsonToValue(&api_key, value, "std::string");


    }

    const char *audio_urlKey = "audio_url";

    if(object.has_key(audio_urlKey))
    {
        bourne::json value = object[audio_urlKey];



        jsonToValue(&audio_url, value, "std::string");


    }

    const char *target_modelKey = "target_model";

    if(object.has_key(target_modelKey))
    {
        bourne::json value = object[target_modelKey];



        jsonToValue(&target_model, value, "std::string");


    }

    const char *prefixKey = "prefix";

    if(object.has_key(prefixKey))
    {
        bourne::json value = object[prefixKey];



        jsonToValue(&prefix, value, "std::string");


    }


}

bourne::json
CloneVoiceRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["api_key"] = getApiKey();






    object["audio_url"] = getAudioUrl();






    object["target_model"] = getTargetModel();






    object["prefix"] = getPrefix();



    return object;

}

std::string
CloneVoiceRequest::getApiKey()
{
	return api_key;
}

void
CloneVoiceRequest::setApiKey(std::string api_key)
{
	this->api_key = api_key;
}

std::string
CloneVoiceRequest::getAudioUrl()
{
	return audio_url;
}

void
CloneVoiceRequest::setAudioUrl(std::string audio_url)
{
	this->audio_url = audio_url;
}

std::string
CloneVoiceRequest::getTargetModel()
{
	return target_model;
}

void
CloneVoiceRequest::setTargetModel(std::string target_model)
{
	this->target_model = target_model;
}

std::string
CloneVoiceRequest::getPrefix()
{
	return prefix;
}

void
CloneVoiceRequest::setPrefix(std::string prefix)
{
	this->prefix = prefix;
}



