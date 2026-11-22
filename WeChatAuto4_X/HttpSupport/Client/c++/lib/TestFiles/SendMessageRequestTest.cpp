
#include "SendMessageRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SendMessageRequest_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "who", "hello"
    };

    SendMessageRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWho().c_str());






}


void test_SendMessageRequest_message_is_assigned_from_json()
{


    bourne::json input =
    {
        "message", "hello"
    };

    SendMessageRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getMessage().c_str());






}





void test_SendMessageRequest_who_is_converted_to_json()
{

    bourne::json input =
    {
        "who", "hello"
    };

    SendMessageRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["who"] == output["who"]);



}


void test_SendMessageRequest_message_is_converted_to_json()
{

    bourne::json input =
    {
        "message", "hello"
    };

    SendMessageRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["message"] == output["message"]);



}




