
#include "SendEmojiRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SendEmojiRequest_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "who", "hello"
    };

    SendEmojiRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWho().c_str());






}


void test_SendEmojiRequest_emoji_is_assigned_from_json()
{


    bourne::json input =
    {
        "emoji", "hello"
    };

    SendEmojiRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getEmoji().c_str());






}


void test_SendEmojiRequest_index_is_assigned_from_json()
{
    bourne::json input =
    {
        "index", 1
    };

    SendEmojiRequest obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getIndex());








}




void test_SendEmojiRequest_who_is_converted_to_json()
{

    bourne::json input =
    {
        "who", "hello"
    };

    SendEmojiRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["who"] == output["who"]);



}


void test_SendEmojiRequest_emoji_is_converted_to_json()
{

    bourne::json input =
    {
        "emoji", "hello"
    };

    SendEmojiRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["emoji"] == output["emoji"]);



}


void test_SendEmojiRequest_index_is_converted_to_json()
{
    bourne::json input =
    {
        "index", 1
    };

    SendEmojiRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["index"] == output["index"]);




}



