
#include "ForwardMultipleMessageRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_ForwardMultipleMessageRequest_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "who", "hello"
    };

    ForwardMultipleMessageRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWho().c_str());






}




void test_ForwardMultipleMessageRequest_row_count_is_assigned_from_json()
{
    bourne::json input =
    {
        "row_count", 1
    };

    ForwardMultipleMessageRequest obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getRowCount());








}



void test_ForwardMultipleMessageRequest_who_is_converted_to_json()
{

    bourne::json input =
    {
        "who", "hello"
    };

    ForwardMultipleMessageRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["who"] == output["who"]);



}




void test_ForwardMultipleMessageRequest_row_count_is_converted_to_json()
{
    bourne::json input =
    {
        "row_count", 1
    };

    ForwardMultipleMessageRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["row_count"] == output["row_count"]);




}


