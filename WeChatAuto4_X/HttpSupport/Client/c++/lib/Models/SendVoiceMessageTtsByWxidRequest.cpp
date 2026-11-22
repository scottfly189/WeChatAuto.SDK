

#include "SendVoiceMessageTtsByWxidRequest.h"

using namespace Tiny;

SendVoiceMessageTtsByWxidRequest::SendVoiceMessageTtsByWxidRequest()
{
	user_name = std::string();
	api_key = std::string();
	message = std::string();
	options = VoiceOptions();
	optimize_with_llm = bool(false);
}

SendVoiceMessageTtsByWxidRequest::SendVoiceMessageTtsByWxidRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendVoiceMessageTtsByWxidRequest::~SendVoiceMessageTtsByWxidRequest()
{

}

void
SendVoiceMessageTtsByWxidRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *user_nameKey = "user_name";

    if(object.has_key(user_nameKey))
    {
        bourne::json value = object[user_nameKey];



        jsonToValue(&user_name, value, "std::string");


    }

    const char *api_keyKey = "api_key";

    if(object.has_key(api_keyKey))
    {
        bourne::json value = object[api_keyKey];



        jsonToValue(&api_key, value, "std::string");


    }

    const char *messageKey = "message";

    if(object.has_key(messageKey))
    {
        bourne::json value = object[messageKey];



        jsonToValue(&message, value, "std::string");


    }

    const char *optionsKey = "options";

    if(object.has_key(optionsKey))
    {
        bourne::json value = object[optionsKey];




        VoiceOptions* obj = &options;
		obj->fromJson(value.dump());

    }

    const char *optimize_with_llmKey = "optimize_with_llm";

    if(object.has_key(optimize_with_llmKey))
    {
        bourne::json value = object[optimize_with_llmKey];



        jsonToValue(&optimize_with_llm, value, "bool");


    }


}

bourne::json
SendVoiceMessageTtsByWxidRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["user_name"] = getUserName();






    object["api_key"] = getApiKey();






    object["message"] = getMessage();







	object["options"] = getOptions().toJson();





    object["optimize_with_llm"] = isOptimizeWithLlm();



    return object;

}

std::string
SendVoiceMessageTtsByWxidRequest::getUserName()
{
	return user_name;
}

void
SendVoiceMessageTtsByWxidRequest::setUserName(std::string user_name)
{
	this->user_name = user_name;
}

std::string
SendVoiceMessageTtsByWxidRequest::getApiKey()
{
	return api_key;
}

void
SendVoiceMessageTtsByWxidRequest::setApiKey(std::string api_key)
{
	this->api_key = api_key;
}

std::string
SendVoiceMessageTtsByWxidRequest::getMessage()
{
	return message;
}

void
SendVoiceMessageTtsByWxidRequest::setMessage(std::string message)
{
	this->message = message;
}

VoiceOptions
SendVoiceMessageTtsByWxidRequest::getOptions()
{
	return options;
}

void
SendVoiceMessageTtsByWxidRequest::setOptions(VoiceOptions options)
{
	this->options = options;
}

bool
SendVoiceMessageTtsByWxidRequest::isOptimizeWithLlm()
{
	return optimize_with_llm;
}

void
SendVoiceMessageTtsByWxidRequest::setOptimizeWithLlm(bool optimize_with_llm)
{
	this->optimize_with_llm = optimize_with_llm;
}



