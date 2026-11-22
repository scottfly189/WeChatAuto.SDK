
#include "SendVoiceMessageTtsByWxidRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SendVoiceMessageTtsByWxidRequest_user_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "user_name", "hello"
    };

    SendVoiceMessageTtsByWxidRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getUserName().c_str());






}


void test_SendVoiceMessageTtsByWxidRequest_api_key_is_assigned_from_json()
{


    bourne::json input =
    {
        "api_key", "hello"
    };

    SendVoiceMessageTtsByWxidRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getApiKey().c_str());






}


void test_SendVoiceMessageTtsByWxidRequest_message_is_assigned_from_json()
{


    bourne::json input =
    {
        "message", "hello"
    };

    SendVoiceMessageTtsByWxidRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getMessage().c_str());






}



void test_SendVoiceMessageTtsByWxidRequest_optimize_with_llm_is_assigned_from_json()
{




    bourne::json input =
    {
        "optimize_with_llm", true
    };

    SendVoiceMessageTtsByWxidRequest obj(input.dump());

    TEST_ASSERT(true == obj.isOptimizeWithLlm());




}



void test_SendVoiceMessageTtsByWxidRequest_user_name_is_converted_to_json()
{

    bourne::json input =
    {
        "user_name", "hello"
    };

    SendVoiceMessageTtsByWxidRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["user_name"] == output["user_name"]);



}


void test_SendVoiceMessageTtsByWxidRequest_api_key_is_converted_to_json()
{

    bourne::json input =
    {
        "api_key", "hello"
    };

    SendVoiceMessageTtsByWxidRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["api_key"] == output["api_key"]);



}


void test_SendVoiceMessageTtsByWxidRequest_message_is_converted_to_json()
{

    bourne::json input =
    {
        "message", "hello"
    };

    SendVoiceMessageTtsByWxidRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["message"] == output["message"]);



}



void test_SendVoiceMessageTtsByWxidRequest_optimize_with_llm_is_converted_to_json()
{


    bourne::json input =
    {
        "optimize_with_llm", true
    };

    SendVoiceMessageTtsByWxidRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["optimize_with_llm"] == output["optimize_with_llm"]);


}


