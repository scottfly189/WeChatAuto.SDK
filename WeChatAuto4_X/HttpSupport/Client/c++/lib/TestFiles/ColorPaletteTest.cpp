
#include "ColorPalette.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_ColorPalette_flags_is_assigned_from_json()
{
    bourne::json input =
    {
        "flags", 1
    };

    ColorPalette obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getFlags());








}




void test_ColorPalette_flags_is_converted_to_json()
{
    bourne::json input =
    {
        "flags", 1
    };

    ColorPalette obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["flags"] == output["flags"]);




}



