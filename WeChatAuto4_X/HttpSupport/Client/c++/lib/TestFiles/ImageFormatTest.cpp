
#include "ImageFormat.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_ImageFormat_guid_is_assigned_from_json()
{


    bourne::json input =
    {
        "guid", "hello"
    };

    ImageFormat obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getGuid().c_str());






}



void test_ImageFormat_guid_is_converted_to_json()
{

    bourne::json input =
    {
        "guid", "hello"
    };

    ImageFormat obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["guid"] == output["guid"]);



}


