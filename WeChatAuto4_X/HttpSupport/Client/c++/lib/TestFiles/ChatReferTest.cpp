
#include "ChatRefer.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"





void test_ChatRefer_isCloseSearchWin_is_assigned_from_json()
{




    bourne::json input =
    {
        "isCloseSearchWin", true
    };

    ChatRefer obj(input.dump());

    TEST_ASSERT(true == obj.isIsCloseSearchWin());




}





void test_ChatRefer_isCloseSearchWin_is_converted_to_json()
{


    bourne::json input =
    {
        "isCloseSearchWin", true
    };

    ChatRefer obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["isCloseSearchWin"] == output["isCloseSearchWin"]);


}


