
#include "Image.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"






void test_Image_width_is_assigned_from_json()
{
    bourne::json input =
    {
        "width", 1
    };

    Image obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getWidth());








}


void test_Image_height_is_assigned_from_json()
{
    bourne::json input =
    {
        "height", 1
    };

    Image obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getHeight());








}


void test_Image_horizontalResolution_is_assigned_from_json()
{








    bourne::json input =
    {
        "horizontalResolution", 1.0
    };

    Image obj(input.dump());

    TEST_ASSERT_EQUAL_FLOAT(1.0, obj.getHorizontalResolution());
}


void test_Image_verticalResolution_is_assigned_from_json()
{








    bourne::json input =
    {
        "verticalResolution", 1.0
    };

    Image obj(input.dump());

    TEST_ASSERT_EQUAL_FLOAT(1.0, obj.getVerticalResolution());
}


void test_Image_flags_is_assigned_from_json()
{
    bourne::json input =
    {
        "flags", 1
    };

    Image obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getFlags());








}












void test_Image_width_is_converted_to_json()
{
    bourne::json input =
    {
        "width", 1
    };

    Image obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["width"] == output["width"]);




}


void test_Image_height_is_converted_to_json()
{
    bourne::json input =
    {
        "height", 1
    };

    Image obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["height"] == output["height"]);




}


void test_Image_horizontalResolution_is_converted_to_json()
{




    bourne::json input =
    {
        "horizontalResolution", 1.0
    };

    Image obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["horizontalResolution"] == output["horizontalResolution"]);
}


void test_Image_verticalResolution_is_converted_to_json()
{




    bourne::json input =
    {
        "verticalResolution", 1.0
    };

    Image obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["verticalResolution"] == output["verticalResolution"]);
}


void test_Image_flags_is_converted_to_json()
{
    bourne::json input =
    {
        "flags", 1
    };

    Image obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["flags"] == output["flags"]);




}








