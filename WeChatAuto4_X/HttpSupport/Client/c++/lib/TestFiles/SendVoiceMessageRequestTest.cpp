
#include "SendVoiceMessageRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SendVoiceMessageRequest_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "who", "hello"
    };

    SendVoiceMessageRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWho().c_str());






}


void test_SendVoiceMessageRequest_file_path_is_assigned_from_json()
{


    bourne::json input =
    {
        "file_path", "hello"
    };

    SendVoiceMessageRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getFilePath().c_str());






}



void test_SendVoiceMessageRequest_who_is_converted_to_json()
{

    bourne::json input =
    {
        "who", "hello"
    };

    SendVoiceMessageRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["who"] == output["who"]);



}


void test_SendVoiceMessageRequest_file_path_is_converted_to_json()
{

    bourne::json input =
    {
        "file_path", "hello"
    };

    SendVoiceMessageRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["file_path"] == output["file_path"]);



}


