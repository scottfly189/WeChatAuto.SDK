
#include "DatabaseQueryRequest.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_DatabaseQueryRequest_sql_is_assigned_from_json()
{


    bourne::json input =
    {
        "sql", "hello"
    };

    DatabaseQueryRequest obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getSql().c_str());






}



void test_DatabaseQueryRequest_sql_is_converted_to_json()
{

    bourne::json input =
    {
        "sql", "hello"
    };

    DatabaseQueryRequest obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["sql"] == output["sql"]);



}


