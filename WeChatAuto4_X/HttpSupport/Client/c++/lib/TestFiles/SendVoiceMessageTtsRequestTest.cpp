
#include "SendVoiceMessageTtsRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SendVoiceMessageTtsRequest_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "who", "hello"
    };

    SendVoiceMessageTtsRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWho().c_str());






}


void test_SendVoiceMessageTtsRequest_api_key_is_assigned_from_json()
{


    bourne::json input =
    {
        "api_key", "hello"
    };

    SendVoiceMessageTtsRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getApiKey().c_str());






}


void test_SendVoiceMessageTtsRequest_message_is_assigned_from_json()
{


    bourne::json input =
    {
        "message", "hello"
    };

    SendVoiceMessageTtsRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getMessage().c_str());






}



void test_SendVoiceMessageTtsRequest_optimize_with_llm_is_assigned_from_json()
{




    bourne::json input =
    {
        "optimize_with_llm", true
    };

    SendVoiceMessageTtsRequest obj(input.dump());

    TEST_ASSERT(true == obj.isOptimizeWithLlm());




}



void test_SendVoiceMessageTtsRequest_who_is_converted_to_json()
{

    bourne::json input =
    {
        "who", "hello"
    };

    SendVoiceMessageTtsRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["who"] == output["who"]);



}


void test_SendVoiceMessageTtsRequest_api_key_is_converted_to_json()
{

    bourne::json input =
    {
        "api_key", "hello"
    };

    SendVoiceMessageTtsRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["api_key"] == output["api_key"]);



}


void test_SendVoiceMessageTtsRequest_message_is_converted_to_json()
{

    bourne::json input =
    {
        "message", "hello"
    };

    SendVoiceMessageTtsRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["message"] == output["message"]);



}



void test_SendVoiceMessageTtsRequest_optimize_with_llm_is_converted_to_json()
{


    bourne::json input =
    {
        "optimize_with_llm", true
    };

    SendVoiceMessageTtsRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["optimize_with_llm"] == output["optimize_with_llm"]);


}


