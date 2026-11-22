
#include "CreateGroupRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_CreateGroupRequest_group_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "group_name", "hello"
    };

    CreateGroupRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getGroupName().c_str());






}


void test_CreateGroupRequest_first_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "first_who", "hello"
    };

    CreateGroupRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getFirstWho().c_str());






}




void test_CreateGroupRequest_group_name_is_converted_to_json()
{

    bourne::json input =
    {
        "group_name", "hello"
    };

    CreateGroupRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["group_name"] == output["group_name"]);



}


void test_CreateGroupRequest_first_who_is_converted_to_json()
{

    bourne::json input =
    {
        "first_who", "hello"
    };

    CreateGroupRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["first_who"] == output["first_who"]);



}



