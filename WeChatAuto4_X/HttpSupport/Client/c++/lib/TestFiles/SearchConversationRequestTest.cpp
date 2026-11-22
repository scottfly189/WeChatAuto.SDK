
#include "SearchConversationRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_SearchConversationRequest_who_is_assigned_from_json()
{


    bourne::json input =
    {
        "who", "hello"
    };

    SearchConversationRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWho().c_str());






}



void test_SearchConversationRequest_who_is_converted_to_json()
{

    bourne::json input =
    {
        "who", "hello"
    };

    SearchConversationRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["who"] == output["who"]);



}


