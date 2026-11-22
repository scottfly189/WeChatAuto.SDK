
#include "FriendInfo.h"

using namespace Tiny;

#include <string>
#include <list>
#include <unity.h>
#include "bourne/json.hpp"



void test_FriendInfo_nickName_is_assigned_from_json()
{


    bourne::json input =
    {
        "nickName", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getNickName().c_str());






}


void test_FriendInfo_memoName_is_assigned_from_json()
{


    bourne::json input =
    {
        "memoName", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getMemoName().c_str());






}


void test_FriendInfo_area_is_assigned_from_json()
{


    bourne::json input =
    {
        "area", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getArea().c_str());






}



void test_FriendInfo_sameGroupNumber_is_assigned_from_json()
{


    bourne::json input =
    {
        "sameGroupNumber", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getSameGroupNumber().c_str());






}


void test_FriendInfo_signature_is_assigned_from_json()
{


    bourne::json input =
    {
        "signature", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getSignature().c_str());






}


void test_FriendInfo_source_is_assigned_from_json()
{


    bourne::json input =
    {
        "source", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getSource().c_str());






}


void test_FriendInfo_wxId_is_assigned_from_json()
{


    bourne::json input =
    {
        "wxId", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getWxId().c_str());






}


void test_FriendInfo_avatarPath_is_assigned_from_json()
{


    bourne::json input =
    {
        "avatarPath", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getAvatarPath().c_str());






}



void test_FriendInfo_avatarImageBase64_is_assigned_from_json()
{


    bourne::json input =
    {
        "avatarImageBase64", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getAvatarImageBase64().c_str());






}




void test_FriendInfo_addDateTime_is_assigned_from_json()
{


    bourne::json input =
    {
        "addDateTime", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getAddDateTime().c_str());






}


void test_FriendInfo_name_is_assigned_from_json()
{


    bourne::json input =
    {
        "name", "hello"
    };

    FriendInfo obj(input.dump());

    TEST_ASSERT_EQUAL_STRING("hello", obj.getName().c_str());






}



void test_FriendInfo_nickName_is_converted_to_json()
{

    bourne::json input =
    {
        "nickName", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["nickName"] == output["nickName"]);



}


void test_FriendInfo_memoName_is_converted_to_json()
{

    bourne::json input =
    {
        "memoName", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["memoName"] == output["memoName"]);



}


void test_FriendInfo_area_is_converted_to_json()
{

    bourne::json input =
    {
        "area", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["area"] == output["area"]);



}



void test_FriendInfo_sameGroupNumber_is_converted_to_json()
{

    bourne::json input =
    {
        "sameGroupNumber", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["sameGroupNumber"] == output["sameGroupNumber"]);



}


void test_FriendInfo_signature_is_converted_to_json()
{

    bourne::json input =
    {
        "signature", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["signature"] == output["signature"]);



}


void test_FriendInfo_source_is_converted_to_json()
{

    bourne::json input =
    {
        "source", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["source"] == output["source"]);



}


void test_FriendInfo_wxId_is_converted_to_json()
{

    bourne::json input =
    {
        "wxId", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["wxId"] == output["wxId"]);



}


void test_FriendInfo_avatarPath_is_converted_to_json()
{

    bourne::json input =
    {
        "avatarPath", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["avatarPath"] == output["avatarPath"]);



}



void test_FriendInfo_avatarImageBase64_is_converted_to_json()
{

    bourne::json input =
    {
        "avatarImageBase64", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["avatarImageBase64"] == output["avatarImageBase64"]);



}




void test_FriendInfo_addDateTime_is_converted_to_json()
{

    bourne::json input =
    {
        "addDateTime", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["addDateTime"] == output["addDateTime"]);



}


void test_FriendInfo_name_is_converted_to_json()
{

    bourne::json input =
    {
        "name", "hello"
    };

    FriendInfo obj(input.dump());

    bourne::json output = bourne::json::object();

    output = obj.toJson();

    TEST_ASSERT(input["name"] == output["name"]);



}


