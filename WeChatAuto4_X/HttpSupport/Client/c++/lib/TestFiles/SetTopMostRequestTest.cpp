
#include "SetTopMostRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SetTopMostRequest_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "who", "hello"
    };

    SetTopMostRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWho().c_str());






}


void test_SetTopMostRequest_setting_is_assigned_from_json()
{




    bourne::json input =
    {
        "setting", true
    };

    SetTopMostRequest obj(input.dump());

    TEST_ASSERT(true == obj.isSetting());




}



void test_SetTopMostRequest_who_is_converted_to_json()
{

    bourne::json input =
    {
        "who", "hello"
    };

    SetTopMostRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["who"] == output["who"]);



}


void test_SetTopMostRequest_setting_is_converted_to_json()
{


    bourne::json input =
    {
        "setting", true
    };

    SetTopMostRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["setting"] == output["setting"]);


}


