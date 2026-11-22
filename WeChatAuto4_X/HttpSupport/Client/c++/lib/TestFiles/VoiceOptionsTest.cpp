
#include "VoiceOptions.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_VoiceOptions_voice_is_assigned_from_json()
{


    bourne::json input =
    {
        "voice", "hello"
    };

    VoiceOptions obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getVoice().c_str());






}


void test_VoiceOptions_model_is_assigned_from_json()
{


    bourne::json input =
    {
        "model", "hello"
    };

    VoiceOptions obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getModel().c_str());






}


void test_VoiceOptions_languageType_is_assigned_from_json()
{


    bourne::json input =
    {
        "languageType", "hello"
    };

    VoiceOptions obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getLanguageType().c_str());






}


void test_VoiceOptions_instructions_is_assigned_from_json()
{


    bourne::json input =
    {
        "instructions", "hello"
    };

    VoiceOptions obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getInstructions().c_str());






}


void test_VoiceOptions_volume_is_assigned_from_json()
{
    bourne::json input =
    {
        "volume", 1
    };

    VoiceOptions obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getVolume());








}


void test_VoiceOptions_rate_is_assigned_from_json()
{








    bourne::json input =
    {
        "rate", 1.0
    };

    VoiceOptions obj(input.dump());

    TEST_ASSERT_EQUAL_FLOAT(1.0, obj.getRate());
}


void test_VoiceOptions_pitch_is_assigned_from_json()
{








    bourne::json input =
    {
        "pitch", 1.0
    };

    VoiceOptions obj(input.dump());

    TEST_ASSERT_EQUAL_FLOAT(1.0, obj.getPitch());
}


void test_VoiceOptions_seed_is_assigned_from_json()
{
    bourne::json input =
    {
        "seed", 1
    };

    VoiceOptions obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getSeed());








}


void test_VoiceOptions_optimizeInstructions_is_assigned_from_json()
{




    bourne::json input =
    {
        "optimizeInstructions", true
    };

    VoiceOptions obj(input.dump());

    TEST_ASSERT(true == obj.isOptimizeInstructions());




}


void test_VoiceOptions_isStream_is_assigned_from_json()
{




    bourne::json input =
    {
        "isStream", true
    };

    VoiceOptions obj(input.dump());

    TEST_ASSERT(true == obj.isIsStream());




}



void test_VoiceOptions_voice_is_converted_to_json()
{

    bourne::json input =
    {
        "voice", "hello"
    };

    VoiceOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["voice"] == output["voice"]);



}


void test_VoiceOptions_model_is_converted_to_json()
{

    bourne::json input =
    {
        "model", "hello"
    };

    VoiceOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["model"] == output["model"]);



}


void test_VoiceOptions_languageType_is_converted_to_json()
{

    bourne::json input =
    {
        "languageType", "hello"
    };

    VoiceOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["languageType"] == output["languageType"]);



}


void test_VoiceOptions_instructions_is_converted_to_json()
{

    bourne::json input =
    {
        "instructions", "hello"
    };

    VoiceOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["instructions"] == output["instructions"]);



}


void test_VoiceOptions_volume_is_converted_to_json()
{
    bourne::json input =
    {
        "volume", 1
    };

    VoiceOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["volume"] == output["volume"]);




}


void test_VoiceOptions_rate_is_converted_to_json()
{




    bourne::json input =
    {
        "rate", 1.0
    };

    VoiceOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["rate"] == output["rate"]);
}


void test_VoiceOptions_pitch_is_converted_to_json()
{




    bourne::json input =
    {
        "pitch", 1.0
    };

    VoiceOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["pitch"] == output["pitch"]);
}


void test_VoiceOptions_seed_is_converted_to_json()
{
    bourne::json input =
    {
        "seed", 1
    };

    VoiceOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["seed"] == output["seed"]);




}


void test_VoiceOptions_optimizeInstructions_is_converted_to_json()
{


    bourne::json input =
    {
        "optimizeInstructions", true
    };

    VoiceOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["optimizeInstructions"] == output["optimizeInstructions"]);


}


void test_VoiceOptions_isStream_is_converted_to_json()
{


    bourne::json input =
    {
        "isStream", true
    };

    VoiceOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["isStream"] == output["isStream"]);


}


