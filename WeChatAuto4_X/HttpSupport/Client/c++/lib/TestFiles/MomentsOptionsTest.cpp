
#include "MomentsOptions.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"





void test_MomentsOptions_isCloseMoments_is_assigned_from_json()
{




    bourne::json input =
    {
        "isCloseMoments", true
    };

    MomentsOptions obj(input.dump());

    TEST_ASSERT(true == obj.isIsCloseMoments());




}





void test_MomentsOptions_isCloseMoments_is_converted_to_json()
{


    bourne::json input =
    {
        "isCloseMoments", true
    };

    MomentsOptions obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["isCloseMoments"] == output["isCloseMoments"]);


}


