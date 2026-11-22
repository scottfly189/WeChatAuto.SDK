
#include "PullMessagesRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_PullMessagesRequest_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "who", "hello"
    };

    PullMessagesRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWho().c_str());






}


void test_PullMessagesRequest_max_fetch_number_is_assigned_from_json()
{
    bourne::json input =
    {
        "max_fetch_number", 1
    };

    PullMessagesRequest obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getMaxFetchNumber());








}


void test_PullMessagesRequest_include_system_message_is_assigned_from_json()
{




    bourne::json input =
    {
        "include_system_message", true
    };

    PullMessagesRequest obj(input.dump());

    TEST_ASSERT(true == obj.isIncludeSystemMessage());




}



void test_PullMessagesRequest_who_is_converted_to_json()
{

    bourne::json input =
    {
        "who", "hello"
    };

    PullMessagesRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["who"] == output["who"]);



}


void test_PullMessagesRequest_max_fetch_number_is_converted_to_json()
{
    bourne::json input =
    {
        "max_fetch_number", 1
    };

    PullMessagesRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["max_fetch_number"] == output["max_fetch_number"]);




}


void test_PullMessagesRequest_include_system_message_is_converted_to_json()
{


    bourne::json input =
    {
        "include_system_message", true
    };

    PullMessagesRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["include_system_message"] == output["include_system_message"]);


}


