
#include "SendVoiceMessageByWxidRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SendVoiceMessageByWxidRequest_user_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "user_name", "hello"
    };

    SendVoiceMessageByWxidRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getUserName().c_str());






}


void test_SendVoiceMessageByWxidRequest_file_path_is_assigned_from_json()
{


    bourne::json input =
    {
        "file_path", "hello"
    };

    SendVoiceMessageByWxidRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getFilePath().c_str());






}



void test_SendVoiceMessageByWxidRequest_user_name_is_converted_to_json()
{

    bourne::json input =
    {
        "user_name", "hello"
    };

    SendVoiceMessageByWxidRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["user_name"] == output["user_name"]);



}


void test_SendVoiceMessageByWxidRequest_file_path_is_converted_to_json()
{

    bourne::json input =
    {
        "file_path", "hello"
    };

    SendVoiceMessageByWxidRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["file_path"] == output["file_path"]);



}


