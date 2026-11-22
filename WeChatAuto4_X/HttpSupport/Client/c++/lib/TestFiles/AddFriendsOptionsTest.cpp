
#include "AddFriendsOptions.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_AddFriendsOptions_intervalTime_is_assigned_from_json()
{
    bourne::json input =
    {
        "intervalTime", 1
    };

    AddFriendsOptions obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getIntervalTime());








}


void test_AddFriendsOptions_isCloseWin_is_assigned_from_json()
{




    bourne::json input =
    {
        "isCloseWin", true
    };

    AddFriendsOptions obj(input.dump());

    TEST_ASSERT(true == obj.isIsCloseWin());




}


void test_AddFriendsOptions_sayHi_is_assigned_from_json()
{


    bourne::json input =
    {
        "sayHi", "hello"
    };

    AddFriendsOptions obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getSayHi().c_str());






}


void test_AddFriendsOptions_suffix_is_assigned_from_json()
{


    bourne::json input =
    {
        "suffix", "hello"
    };

    AddFriendsOptions obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getSuffix().c_str());






}


void test_AddFriendsOptions_label_is_assigned_from_json()
{


    bourne::json input =
    {
        "label", "hello"
    };

    AddFriendsOptions obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getLabel().c_str());






}



void test_AddFriendsOptions_intervalTime_is_converted_to_json()
{
    bourne::json input =
    {
        "intervalTime", 1
    };

    AddFriendsOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["intervalTime"] == output["intervalTime"]);




}


void test_AddFriendsOptions_isCloseWin_is_converted_to_json()
{


    bourne::json input =
    {
        "isCloseWin", true
    };

    AddFriendsOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["isCloseWin"] == output["isCloseWin"]);


}


void test_AddFriendsOptions_sayHi_is_converted_to_json()
{

    bourne::json input =
    {
        "sayHi", "hello"
    };

    AddFriendsOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["sayHi"] == output["sayHi"]);



}


void test_AddFriendsOptions_suffix_is_converted_to_json()
{

    bourne::json input =
    {
        "suffix", "hello"
    };

    AddFriendsOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["suffix"] == output["suffix"]);



}


void test_AddFriendsOptions_label_is_converted_to_json()
{

    bourne::json input =
    {
        "label", "hello"
    };

    AddFriendsOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["label"] == output["label"]);



}


