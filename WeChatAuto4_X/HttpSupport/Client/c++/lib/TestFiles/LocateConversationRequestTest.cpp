
#include "LocateConversationRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_LocateConversationRequest_title_is_assigned_from_json()
{


    bourne::json input =
    {
        "title", "hello"
    };

    LocateConversationRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getTitle().c_str());






}



void test_LocateConversationRequest_title_is_converted_to_json()
{

    bourne::json input =
    {
        "title", "hello"
    };

    LocateConversationRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["title"] == output["title"]);



}


