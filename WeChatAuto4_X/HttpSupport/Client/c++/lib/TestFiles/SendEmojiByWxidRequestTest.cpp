
#include "SendEmojiByWxidRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SendEmojiByWxidRequest_user_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "user_name", "hello"
    };

    SendEmojiByWxidRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getUserName().c_str());






}


void test_SendEmojiByWxidRequest_emoji_is_assigned_from_json()
{


    bourne::json input =
    {
        "emoji", "hello"
    };

    SendEmojiByWxidRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getEmoji().c_str());






}


void test_SendEmojiByWxidRequest_index_is_assigned_from_json()
{
    bourne::json input =
    {
        "index", 1
    };

    SendEmojiByWxidRequest obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getIndex());








}




void test_SendEmojiByWxidRequest_user_name_is_converted_to_json()
{

    bourne::json input =
    {
        "user_name", "hello"
    };

    SendEmojiByWxidRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["user_name"] == output["user_name"]);



}


void test_SendEmojiByWxidRequest_emoji_is_converted_to_json()
{

    bourne::json input =
    {
        "emoji", "hello"
    };

    SendEmojiByWxidRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["emoji"] == output["emoji"]);



}


void test_SendEmojiByWxidRequest_index_is_converted_to_json()
{
    bourne::json input =
    {
        "index", 1
    };

    SendEmojiByWxidRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["index"] == output["index"]);




}



