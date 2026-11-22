
#include "ChangeGroupMemoRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_ChangeGroupMemoRequest_group_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "group_name", "hello"
    };

    ChangeGroupMemoRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getGroupName().c_str());






}


void test_ChangeGroupMemoRequest_new_memo_is_assigned_from_json()
{


    bourne::json input =
    {
        "new_memo", "hello"
    };

    ChangeGroupMemoRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getNewMemo().c_str());






}



void test_ChangeGroupMemoRequest_group_name_is_converted_to_json()
{

    bourne::json input =
    {
        "group_name", "hello"
    };

    ChangeGroupMemoRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["group_name"] == output["group_name"]);



}


void test_ChangeGroupMemoRequest_new_memo_is_converted_to_json()
{

    bourne::json input =
    {
        "new_memo", "hello"
    };

    ChangeGroupMemoRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["new_memo"] == output["new_memo"]);



}


