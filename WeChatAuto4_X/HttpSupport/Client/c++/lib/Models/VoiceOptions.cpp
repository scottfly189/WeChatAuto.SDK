

#include "VoiceOptions.h"

using namespace Tiny;

VoiceOptions::VoiceOptions()
{
	voice = std::string();
	model = std::string();
	languageType = std::string();
	instructions = std::string();
	volume = int(0);
	rate = float(0);
	pitch = float(0);
	seed = int(0);
	optimizeInstructions = bool(false);
	isStream = bool(false);
}

VoiceOptions::VoiceOptions(std::string jsonString)
{
	this->fromJson(jsonString);
}

VoiceOptions::~VoiceOptions()
{

}

void
VoiceOptions::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *voiceKey = "voice";

    if(object.has_key(voiceKey))
    {
        bourne::json value = object[voiceKey];



        jsonToValue(&voice, value, "std::string");


    }

    const char *modelKey = "model";

    if(object.has_key(modelKey))
    {
        bourne::json value = object[modelKey];



        jsonToValue(&model, value, "std::string");


    }

    const char *languageTypeKey = "languageType";

    if(object.has_key(languageTypeKey))
    {
        bourne::json value = object[languageTypeKey];



        jsonToValue(&languageType, value, "std::string");


    }

    const char *instructionsKey = "instructions";

    if(object.has_key(instructionsKey))
    {
        bourne::json value = object[instructionsKey];



        jsonToValue(&instructions, value, "std::string");


    }

    const char *volumeKey = "volume";

    if(object.has_key(volumeKey))
    {
        bourne::json value = object[volumeKey];



        jsonToValue(&volume, value, "int");


    }

    const char *rateKey = "rate";

    if(object.has_key(rateKey))
    {
        bourne::json value = object[rateKey];



        jsonToValue(&rate, value, "float");


    }

    const char *pitchKey = "pitch";

    if(object.has_key(pitchKey))
    {
        bourne::json value = object[pitchKey];



        jsonToValue(&pitch, value, "float");


    }

    const char *seedKey = "seed";

    if(object.has_key(seedKey))
    {
        bourne::json value = object[seedKey];



        jsonToValue(&seed, value, "int");


    }

    const char *optimizeInstructionsKey = "optimizeInstructions";

    if(object.has_key(optimizeInstructionsKey))
    {
        bourne::json value = object[optimizeInstructionsKey];



        jsonToValue(&optimizeInstructions, value, "bool");


    }

    const char *isStreamKey = "isStream";

    if(object.has_key(isStreamKey))
    {
        bourne::json value = object[isStreamKey];



        jsonToValue(&isStream, value, "bool");


    }


}

bourne::json
VoiceOptions::toJson()
{
    bourne::json object = bourne::json::object();





    object["voice"] = getVoice();






    object["model"] = getModel();






    object["languageType"] = getLanguageType();






    object["instructions"] = getInstructions();






    object["volume"] = getVolume();






    object["rate"] = getRate();






    object["pitch"] = getPitch();






    object["seed"] = getSeed();






    object["optimizeInstructions"] = isOptimizeInstructions();






    object["isStream"] = isIsStream();



    return object;

}

std::string
VoiceOptions::getVoice()
{
	return voice;
}

void
VoiceOptions::setVoice(std::string voice)
{
	this->voice = voice;
}

std::string
VoiceOptions::getModel()
{
	return model;
}

void
VoiceOptions::setModel(std::string model)
{
	this->model = model;
}

std::string
VoiceOptions::getLanguageType()
{
	return languageType;
}

void
VoiceOptions::setLanguageType(std::string languageType)
{
	this->languageType = languageType;
}

std::string
VoiceOptions::getInstructions()
{
	return instructions;
}

void
VoiceOptions::setInstructions(std::string instructions)
{
	this->instructions = instructions;
}

int
VoiceOptions::getVolume()
{
	return volume;
}

void
VoiceOptions::setVolume(int volume)
{
	this->volume = volume;
}

float
VoiceOptions::getRate()
{
	return rate;
}

void
VoiceOptions::setRate(float rate)
{
	this->rate = rate;
}

float
VoiceOptions::getPitch()
{
	return pitch;
}

void
VoiceOptions::setPitch(float pitch)
{
	this->pitch = pitch;
}

int
VoiceOptions::getSeed()
{
	return seed;
}

void
VoiceOptions::setSeed(int seed)
{
	this->seed = seed;
}

bool
VoiceOptions::isOptimizeInstructions()
{
	return optimizeInstructions;
}

void
VoiceOptions::setOptimizeInstructions(bool optimizeInstructions)
{
	this->optimizeInstructions = optimizeInstructions;
}

bool
VoiceOptions::isIsStream()
{
	return isStream;
}

void
VoiceOptions::setIsStream(bool isStream)
{
	this->isStream = isStream;
}



