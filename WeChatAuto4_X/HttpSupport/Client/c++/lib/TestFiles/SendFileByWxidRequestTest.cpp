
#include "SendFileByWxidRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SendFileByWxidRequest_user_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "user_name", "hello"
    };

    SendFileByWxidRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getUserName().c_str());






}




void test_SendFileByWxidRequest_user_name_is_converted_to_json()
{

    bourne::json input =
    {
        "user_name", "hello"
    };

    SendFileByWxidRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["user_name"] == output["user_name"]);



}



