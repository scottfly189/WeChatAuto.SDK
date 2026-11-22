
#include "NotifyIconClickRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_NotifyIconClickRequest_index_is_assigned_from_json()
{
    bourne::json input =
    {
        "index", 1
    };

    NotifyIconClickRequest obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getIndex());








}


void test_NotifyIconClickRequest_wechat_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "wechat_name", "hello"
    };

    NotifyIconClickRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWechatName().c_str());






}



void test_NotifyIconClickRequest_index_is_converted_to_json()
{
    bourne::json input =
    {
        "index", 1
    };

    NotifyIconClickRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["index"] == output["index"]);




}


void test_NotifyIconClickRequest_wechat_name_is_converted_to_json()
{

    bourne::json input =
    {
        "wechat_name", "hello"
    };

    NotifyIconClickRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["wechat_name"] == output["wechat_name"]);



}


