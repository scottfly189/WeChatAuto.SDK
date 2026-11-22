
#include "SizeF.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SizeF_isEmpty_is_assigned_from_json()
{




    bourne::json input =
    {
        "isEmpty", true
    };

    SizeF obj(input.dump());

    TEST_ASSERT(true == obj.isIsEmpty());




}


void test_SizeF_width_is_assigned_from_json()
{








    bourne::json input =
    {
        "width", 1.0
    };

    SizeF obj(input.dump());

    TEST_ASSERT_EQUAL_FLOAT(1.0, obj.getWidth());
}


void test_SizeF_height_is_assigned_from_json()
{








    bourne::json input =
    {
        "height", 1.0
    };

    SizeF obj(input.dump());

    TEST_ASSERT_EQUAL_FLOAT(1.0, obj.getHeight());
}



void test_SizeF_isEmpty_is_converted_to_json()
{


    bourne::json input =
    {
        "isEmpty", true
    };

    SizeF obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["isEmpty"] == output["isEmpty"]);


}


void test_SizeF_width_is_converted_to_json()
{




    bourne::json input =
    {
        "width", 1.0
    };

    SizeF obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["width"] == output["width"]);
}


void test_SizeF_height_is_converted_to_json()
{




    bourne::json input =
    {
        "height", 1.0
    };

    SizeF obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["height"] == output["height"]);
}


