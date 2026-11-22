
#include "SaveAvatarRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SaveAvatarRequest_path_is_assigned_from_json()
{


    bourne::json input =
    {
        "path", "hello"
    };

    SaveAvatarRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getPath().c_str());






}



void test_SaveAvatarRequest_path_is_converted_to_json()
{

    bourne::json input =
    {
        "path", "hello"
    };

    SaveAvatarRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["path"] == output["path"]);



}


