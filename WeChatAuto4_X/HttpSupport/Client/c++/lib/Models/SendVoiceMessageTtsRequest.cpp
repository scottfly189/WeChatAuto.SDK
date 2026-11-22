

#include "SendVoiceMessageTtsRequest.h"

using namespace Tiny;

SendVoiceMessageTtsRequest::SendVoiceMessageTtsRequest()
{
	who = std::string();
	api_key = std::string();
	message = std::string();
	options = VoiceOptions();
	optimize_with_llm = bool(false);
}

SendVoiceMessageTtsRequest::SendVoiceMessageTtsRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendVoiceMessageTtsRequest::~SendVoiceMessageTtsRequest()
{

}

void
SendVoiceMessageTtsRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


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
SendVoiceMessageTtsRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();






    object["api_key"] = getApiKey();






    object["message"] = getMessage();







	object["options"] = getOptions().toJson();





    object["optimize_with_llm"] = isOptimizeWithLlm();



    return object;

}

std::string
SendVoiceMessageTtsRequest::getWho()
{
	return who;
}

void
SendVoiceMessageTtsRequest::setWho(std::string who)
{
	this->who = who;
}

std::string
SendVoiceMessageTtsRequest::getApiKey()
{
	return api_key;
}

void
SendVoiceMessageTtsRequest::setApiKey(std::string api_key)
{
	this->api_key = api_key;
}

std::string
SendVoiceMessageTtsRequest::getMessage()
{
	return message;
}

void
SendVoiceMessageTtsRequest::setMessage(std::string message)
{
	this->message = message;
}

VoiceOptions
SendVoiceMessageTtsRequest::getOptions()
{
	return options;
}

void
SendVoiceMessageTtsRequest::setOptions(VoiceOptions options)
{
	this->options = options;
}

bool
SendVoiceMessageTtsRequest::isOptimizeWithLlm()
{
	return optimize_with_llm;
}

void
SendVoiceMessageTtsRequest::setOptimizeWithLlm(bool optimize_with_llm)
{
	this->optimize_with_llm = optimize_with_llm;
}



