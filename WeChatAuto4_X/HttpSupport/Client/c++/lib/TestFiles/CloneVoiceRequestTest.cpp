
#include "CloneVoiceRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_CloneVoiceRequest_api_key_is_assigned_from_json()
{


    bourne::json input =
    {
        "api_key", "hello"
    };

    CloneVoiceRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getApiKey().c_str());






}


void test_CloneVoiceRequest_audio_url_is_assigned_from_json()
{


    bourne::json input =
    {
        "audio_url", "hello"
    };

    CloneVoiceRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getAudioUrl().c_str());






}


void test_CloneVoiceRequest_target_model_is_assigned_from_json()
{


    bourne::json input =
    {
        "target_model", "hello"
    };

    CloneVoiceRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getTargetModel().c_str());






}


void test_CloneVoiceRequest_prefix_is_assigned_from_json()
{


    bourne::json input =
    {
        "prefix", "hello"
    };

    CloneVoiceRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getPrefix().c_str());






}



void test_CloneVoiceRequest_api_key_is_converted_to_json()
{

    bourne::json input =
    {
        "api_key", "hello"
    };

    CloneVoiceRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["api_key"] == output["api_key"]);



}


void test_CloneVoiceRequest_audio_url_is_converted_to_json()
{

    bourne::json input =
    {
        "audio_url", "hello"
    };

    CloneVoiceRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["audio_url"] == output["audio_url"]);



}


void test_CloneVoiceRequest_target_model_is_converted_to_json()
{

    bourne::json input =
    {
        "target_model", "hello"
    };

    CloneVoiceRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["target_model"] == output["target_model"]);



}


void test_CloneVoiceRequest_prefix_is_converted_to_json()
{

    bourne::json input =
    {
        "prefix", "hello"
    };

    CloneVoiceRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["prefix"] == output["prefix"]);



}


