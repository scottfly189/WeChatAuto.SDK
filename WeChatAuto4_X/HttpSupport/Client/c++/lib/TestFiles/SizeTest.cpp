
#include "Size.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_Size_isEmpty_is_assigned_from_json()
{




    bourne::json input =
    {
        "isEmpty", true
    };

    Size obj(input.dump());

    TEST_ASSERT(true == obj.isIsEmpty());




}


void test_Size_width_is_assigned_from_json()
{
    bourne::json input =
    {
        "width", 1
    };

    Size obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getWidth());








}


void test_Size_height_is_assigned_from_json()
{
    bourne::json input =
    {
        "height", 1
    };

    Size obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getHeight());








}



void test_Size_isEmpty_is_converted_to_json()
{


    bourne::json input =
    {
        "isEmpty", true
    };

    Size obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["isEmpty"] == output["isEmpty"]);


}


void test_Size_width_is_converted_to_json()
{
    bourne::json input =
    {
        "width", 1
    };

    Size obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["width"] == output["width"]);




}


void test_Size_height_is_converted_to_json()
{
    bourne::json input =
    {
        "height", 1
    };

    Size obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["height"] == output["height"]);




}


