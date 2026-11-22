
#include "RenameGroupRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_RenameGroupRequest_old_group_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "old_group_name", "hello"
    };

    RenameGroupRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getOldGroupName().c_str());






}


void test_RenameGroupRequest_new_group_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "new_group_name", "hello"
    };

    RenameGroupRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getNewGroupName().c_str());






}



void test_RenameGroupRequest_old_group_name_is_converted_to_json()
{

    bourne::json input =
    {
        "old_group_name", "hello"
    };

    RenameGroupRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["old_group_name"] == output["old_group_name"]);



}


void test_RenameGroupRequest_new_group_name_is_converted_to_json()
{

    bourne::json input =
    {
        "new_group_name", "hello"
    };

    RenameGroupRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["new_group_name"] == output["new_group_name"]);



}


