
#include "ReferenceRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"




void test_ReferenceRequest_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "who", "hello"
    };

    ReferenceRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWho().c_str());






}


void test_ReferenceRequest_message_content_is_assigned_from_json()
{


    bourne::json input =
    {
        "message_content", "hello"
    };

    ReferenceRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getMessageContent().c_str());






}


void test_ReferenceRequest_prev_scroll_number_is_assigned_from_json()
{
    bourne::json input =
    {
        "prev_scroll_number", 1
    };

    ReferenceRequest obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getPrevScrollNumber());








}




void test_ReferenceRequest_who_is_converted_to_json()
{

    bourne::json input =
    {
        "who", "hello"
    };

    ReferenceRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["who"] == output["who"]);



}


void test_ReferenceRequest_message_content_is_converted_to_json()
{

    bourne::json input =
    {
        "message_content", "hello"
    };

    ReferenceRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["message_content"] == output["message_content"]);



}


void test_ReferenceRequest_prev_scroll_number_is_converted_to_json()
{
    bourne::json input =
    {
        "prev_scroll_number", 1
    };

    ReferenceRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["prev_scroll_number"] == output["prev_scroll_number"]);




}


