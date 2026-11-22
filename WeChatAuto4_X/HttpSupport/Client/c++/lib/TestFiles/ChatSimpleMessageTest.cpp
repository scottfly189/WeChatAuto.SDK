
#include "ChatSimpleMessage.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_ChatSimpleMessage_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "who", "hello"
    };

    ChatSimpleMessage obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWho().c_str());






}


void test_ChatSimpleMessage_message_is_assigned_from_json()
{


    bourne::json input =
    {
        "message", "hello"
    };

    ChatSimpleMessage obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getMessage().c_str());






}


void test_ChatSimpleMessage_sendDateTime_is_assigned_from_json()
{


    bourne::json input =
    {
        "sendDateTime", "hello"
    };

    ChatSimpleMessage obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getSendDateTime().c_str());






}


void test_ChatSimpleMessage_dateTime_is_assigned_from_json()
{








}


void test_ChatSimpleMessage_uniqueString_is_assigned_from_json()
{


    bourne::json input =
    {
        "uniqueString", "hello"
    };

    ChatSimpleMessage obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getUniqueString().c_str());






}



void test_ChatSimpleMessage_who_is_converted_to_json()
{

    bourne::json input =
    {
        "who", "hello"
    };

    ChatSimpleMessage obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["who"] == output["who"]);



}


void test_ChatSimpleMessage_message_is_converted_to_json()
{

    bourne::json input =
    {
        "message", "hello"
    };

    ChatSimpleMessage obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["message"] == output["message"]);



}


void test_ChatSimpleMessage_sendDateTime_is_converted_to_json()
{

    bourne::json input =
    {
        "sendDateTime", "hello"
    };

    ChatSimpleMessage obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["sendDateTime"] == output["sendDateTime"]);



}


void test_ChatSimpleMessage_dateTime_is_converted_to_json()
{




}


void test_ChatSimpleMessage_uniqueString_is_converted_to_json()
{

    bourne::json input =
    {
        "uniqueString", "hello"
    };

    ChatSimpleMessage obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["uniqueString"] == output["uniqueString"]);



}


