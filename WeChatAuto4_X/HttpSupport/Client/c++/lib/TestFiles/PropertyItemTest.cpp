
#include "PropertyItem.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_PropertyItem_id_is_assigned_from_json()
{
    bourne::json input =
    {
        "id", 1
    };

    PropertyItem obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getId());








}


void test_PropertyItem_len_is_assigned_from_json()
{
    bourne::json input =
    {
        "len", 1
    };

    PropertyItem obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getLen());








}


void test_PropertyItem_type_is_assigned_from_json()
{
    bourne::json input =
    {
        "type", 1
    };

    PropertyItem obj(input.dump());

    TEST_ASSERT_EQUAL_INT(1, obj.getType());








}




void test_PropertyItem_id_is_converted_to_json()
{
    bourne::json input =
    {
        "id", 1
    };

    PropertyItem obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["id"] == output["id"]);




}


void test_PropertyItem_len_is_converted_to_json()
{
    bourne::json input =
    {
        "len", 1
    };

    PropertyItem obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["len"] == output["len"]);




}


void test_PropertyItem_type_is_converted_to_json()
{
    bourne::json input =
    {
        "type", 1
    };

    PropertyItem obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["type"] == output["type"]);




}



