
#include "InviteGroupMemberRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_InviteGroupMemberRequest_group_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "group_name", "hello"
    };

    InviteGroupMemberRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getGroupName().c_str());






}



void test_InviteGroupMemberRequest_invite_reason_is_assigned_from_json()
{


    bourne::json input =
    {
        "invite_reason", "hello"
    };

    InviteGroupMemberRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getInviteReason().c_str());






}



void test_InviteGroupMemberRequest_group_name_is_converted_to_json()
{

    bourne::json input =
    {
        "group_name", "hello"
    };

    InviteGroupMemberRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["group_name"] == output["group_name"]);



}



void test_InviteGroupMemberRequest_invite_reason_is_converted_to_json()
{

    bourne::json input =
    {
        "invite_reason", "hello"
    };

    InviteGroupMemberRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["invite_reason"] == output["invite_reason"]);



}


