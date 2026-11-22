
#include "ChangeGroupNickNameRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_ChangeGroupNickNameRequest_group_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "group_name", "hello"
    };

    ChangeGroupNickNameRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getGroupName().c_str());






}


void test_ChangeGroupNickNameRequest_nick_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "nick_name", "hello"
    };

    ChangeGroupNickNameRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getNickName().c_str());






}



void test_ChangeGroupNickNameRequest_group_name_is_converted_to_json()
{

    bourne::json input =
    {
        "group_name", "hello"
    };

    ChangeGroupNickNameRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["group_name"] == output["group_name"]);



}


void test_ChangeGroupNickNameRequest_nick_name_is_converted_to_json()
{

    bourne::json input =
    {
        "nick_name", "hello"
    };

    ChangeGroupNickNameRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["nick_name"] == output["nick_name"]);



}


