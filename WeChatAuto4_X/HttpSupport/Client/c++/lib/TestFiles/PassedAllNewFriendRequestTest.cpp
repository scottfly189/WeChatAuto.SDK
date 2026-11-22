
#include "PassedAllNewFriendRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"




void test_PassedAllNewFriendRequest_suffix_is_assigned_from_json()
{


    bourne::json input =
    {
        "suffix", "hello"
    };

    PassedAllNewFriendRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getSuffix().c_str());






}


void test_PassedAllNewFriendRequest_label_is_assigned_from_json()
{


    bourne::json input =
    {
        "label", "hello"
    };

    PassedAllNewFriendRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getLabel().c_str());






}


void test_PassedAllNewFriendRequest_passed_delete_is_assigned_from_json()
{




    bourne::json input =
    {
        "passed_delete", true
    };

    PassedAllNewFriendRequest obj(input.dump());

    TEST_ASSERT(true == obj.isPassedDelete());




}





void test_PassedAllNewFriendRequest_suffix_is_converted_to_json()
{

    bourne::json input =
    {
        "suffix", "hello"
    };

    PassedAllNewFriendRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["suffix"] == output["suffix"]);



}


void test_PassedAllNewFriendRequest_label_is_converted_to_json()
{

    bourne::json input =
    {
        "label", "hello"
    };

    PassedAllNewFriendRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["label"] == output["label"]);



}


void test_PassedAllNewFriendRequest_passed_delete_is_converted_to_json()
{


    bourne::json input =
    {
        "passed_delete", true
    };

    PassedAllNewFriendRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["passed_delete"] == output["passed_delete"]);


}



