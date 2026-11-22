
#include "Color.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_Color_r_is_assigned_from_json()
{
    bourne::json input =
    {
        "r", 1
    };

    Color obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getR());








}


void test_Color_g_is_assigned_from_json()
{
    bourne::json input =
    {
        "g", 1
    };

    Color obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getG());








}


void test_Color_b_is_assigned_from_json()
{
    bourne::json input =
    {
        "b", 1
    };

    Color obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getB());








}


void test_Color_a_is_assigned_from_json()
{
    bourne::json input =
    {
        "a", 1
    };

    Color obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getA());








}


void test_Color_isKnownColor_is_assigned_from_json()
{




    bourne::json input =
    {
        "isKnownColor", true
    };

    Color obj(input.dump());

    TEST_ASSERT(true == obj.isIsKnownColor());




}


void test_Color_isEmpty_is_assigned_from_json()
{




    bourne::json input =
    {
        "isEmpty", true
    };

    Color obj(input.dump());

    TEST_ASSERT(true == obj.isIsEmpty());




}


void test_Color_isNamedColor_is_assigned_from_json()
{




    bourne::json input =
    {
        "isNamedColor", true
    };

    Color obj(input.dump());

    TEST_ASSERT(true == obj.isIsNamedColor());




}


void test_Color_isSystemColor_is_assigned_from_json()
{




    bourne::json input =
    {
        "isSystemColor", true
    };

    Color obj(input.dump());

    TEST_ASSERT(true == obj.isIsSystemColor());




}


void test_Color_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "name", "hello"
    };

    Color obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getName().c_str());






}



void test_Color_r_is_converted_to_json()
{
    bourne::json input =
    {
        "r", 1
    };

    Color obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["r"] == output["r"]);




}


void test_Color_g_is_converted_to_json()
{
    bourne::json input =
    {
        "g", 1
    };

    Color obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["g"] == output["g"]);




}


void test_Color_b_is_converted_to_json()
{
    bourne::json input =
    {
        "b", 1
    };

    Color obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["b"] == output["b"]);




}


void test_Color_a_is_converted_to_json()
{
    bourne::json input =
    {
        "a", 1
    };

    Color obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["a"] == output["a"]);




}


void test_Color_isKnownColor_is_converted_to_json()
{


    bourne::json input =
    {
        "isKnownColor", true
    };

    Color obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["isKnownColor"] == output["isKnownColor"]);


}


void test_Color_isEmpty_is_converted_to_json()
{


    bourne::json input =
    {
        "isEmpty", true
    };

    Color obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["isEmpty"] == output["isEmpty"]);


}


void test_Color_isNamedColor_is_converted_to_json()
{


    bourne::json input =
    {
        "isNamedColor", true
    };

    Color obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["isNamedColor"] == output["isNamedColor"]);


}


void test_Color_isSystemColor_is_converted_to_json()
{


    bourne::json input =
    {
        "isSystemColor", true
    };

    Color obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["isSystemColor"] == output["isSystemColor"]);


}


void test_Color_name_is_converted_to_json()
{

    bourne::json input =
    {
        "name", "hello"
    };

    Color obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["name"] == output["name"]);



}


