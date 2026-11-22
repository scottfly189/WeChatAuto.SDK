# wechatauto.GroupsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**groups_add_members**](GroupsApi.md#groups_add_members) | **POST** /api/v1/clients/{clientId}/groups/members | 添加群聊成员（适用于自有群）。
[**groups_add_members_to_friends**](GroupsApi.md#groups_add_members_to_friends) | **POST** /api/v1/clients/{clientId}/groups/add-members-to-friends | 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。
[**groups_change_memo**](GroupsApi.md#groups_change_memo) | **PUT** /api/v1/clients/{clientId}/groups/memo | 修改群备注（仅自己可见）。
[**groups_change_name**](GroupsApi.md#groups_change_name) | **PUT** /api/v1/clients/{clientId}/groups/name | 修改群名（适用于自有群）。
[**groups_change_nick_name**](GroupsApi.md#groups_change_nick_name) | **PUT** /api/v1/clients/{clientId}/groups/nickname | 修改自己在群中的昵称。
[**groups_create**](GroupsApi.md#groups_create) | **POST** /api/v1/clients/{clientId}/groups | 创建群聊，已存在则打开群聊。
[**groups_get_members**](GroupsApi.md#groups_get_members) | **GET** /api/v1/clients/{clientId}/groups/members | 获取群聊成员列表。
[**groups_get_owner**](GroupsApi.md#groups_get_owner) | **GET** /api/v1/clients/{clientId}/groups/{groupName}/owner | 获取群主昵称。
[**groups_invite**](GroupsApi.md#groups_invite) | **POST** /api/v1/clients/{clientId}/groups/invite | 邀请群聊成员（适用于外部群）。
[**groups_is_owner**](GroupsApi.md#groups_is_owner) | **GET** /api/v1/clients/{clientId}/groups/{groupName}/is-owner | 判断群聊是否为自有群。
[**groups_quit**](GroupsApi.md#groups_quit) | **DELETE** /api/v1/clients/{clientId}/groups/{groupName} | 退出群聊。
[**groups_remove_members**](GroupsApi.md#groups_remove_members) | **DELETE** /api/v1/clients/{clientId}/groups/members | 移除群聊成员（适用于自有群）。
[**groups_update_notice**](GroupsApi.md#groups_update_notice) | **PUT** /api/v1/clients/{clientId}/groups/notice | 更新群公告（仅适用于自有群）。


# **groups_add_members**
> groups_add_members(client_id, group_member_request=group_member_request)

添加群聊成员（适用于自有群）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.group_member_request import GroupMemberRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    group_member_request = wechatauto.GroupMemberRequest() # GroupMemberRequest |  (optional)

    try:
        # 添加群聊成员（适用于自有群）。
        api_instance.groups_add_members(client_id, group_member_request=group_member_request)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_add_members: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **group_member_request** | [**GroupMemberRequest**](GroupMemberRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_add_members_to_friends**
> groups_add_members_to_friends(client_id, add_group_member_to_friends_request=add_group_member_to_friends_request)

把群聊成员加为自己的好友（适用于外部群，注意微信风控）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.add_group_member_to_friends_request import AddGroupMemberToFriendsRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    add_group_member_to_friends_request = wechatauto.AddGroupMemberToFriendsRequest() # AddGroupMemberToFriendsRequest |  (optional)

    try:
        # 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。
        api_instance.groups_add_members_to_friends(client_id, add_group_member_to_friends_request=add_group_member_to_friends_request)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_add_members_to_friends: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **add_group_member_to_friends_request** | [**AddGroupMemberToFriendsRequest**](AddGroupMemberToFriendsRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_change_memo**
> groups_change_memo(client_id, change_group_memo_request=change_group_memo_request)

修改群备注（仅自己可见）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.change_group_memo_request import ChangeGroupMemoRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    change_group_memo_request = wechatauto.ChangeGroupMemoRequest() # ChangeGroupMemoRequest |  (optional)

    try:
        # 修改群备注（仅自己可见）。
        api_instance.groups_change_memo(client_id, change_group_memo_request=change_group_memo_request)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_change_memo: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **change_group_memo_request** | [**ChangeGroupMemoRequest**](ChangeGroupMemoRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_change_name**
> groups_change_name(client_id, rename_group_request=rename_group_request)

修改群名（适用于自有群）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.rename_group_request import RenameGroupRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    rename_group_request = wechatauto.RenameGroupRequest() # RenameGroupRequest |  (optional)

    try:
        # 修改群名（适用于自有群）。
        api_instance.groups_change_name(client_id, rename_group_request=rename_group_request)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_change_name: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **rename_group_request** | [**RenameGroupRequest**](RenameGroupRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_change_nick_name**
> groups_change_nick_name(client_id, change_group_nick_name_request=change_group_nick_name_request)

修改自己在群中的昵称。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.change_group_nick_name_request import ChangeGroupNickNameRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    change_group_nick_name_request = wechatauto.ChangeGroupNickNameRequest() # ChangeGroupNickNameRequest |  (optional)

    try:
        # 修改自己在群中的昵称。
        api_instance.groups_change_nick_name(client_id, change_group_nick_name_request=change_group_nick_name_request)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_change_nick_name: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **change_group_nick_name_request** | [**ChangeGroupNickNameRequest**](ChangeGroupNickNameRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_create**
> groups_create(client_id, create_group_request=create_group_request)

创建群聊，已存在则打开群聊。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.create_group_request import CreateGroupRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    create_group_request = wechatauto.CreateGroupRequest() # CreateGroupRequest |  (optional)

    try:
        # 创建群聊，已存在则打开群聊。
        api_instance.groups_create(client_id, create_group_request=create_group_request)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_create: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **create_group_request** | [**CreateGroupRequest**](CreateGroupRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_get_members**
> groups_get_members(client_id, group_name=group_name)

获取群聊成员列表。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    group_name = 'group_name_example' # str |  (optional)

    try:
        # 获取群聊成员列表。
        api_instance.groups_get_members(client_id, group_name=group_name)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_get_members: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **group_name** | **str**|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_get_owner**
> groups_get_owner(client_id, group_name)

获取群主昵称。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    group_name = 'group_name_example' # str | 

    try:
        # 获取群主昵称。
        api_instance.groups_get_owner(client_id, group_name)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_get_owner: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **group_name** | **str**|  | 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_invite**
> groups_invite(client_id, invite_group_member_request=invite_group_member_request)

邀请群聊成员（适用于外部群）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.invite_group_member_request import InviteGroupMemberRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    invite_group_member_request = wechatauto.InviteGroupMemberRequest() # InviteGroupMemberRequest |  (optional)

    try:
        # 邀请群聊成员（适用于外部群）。
        api_instance.groups_invite(client_id, invite_group_member_request=invite_group_member_request)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_invite: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **invite_group_member_request** | [**InviteGroupMemberRequest**](InviteGroupMemberRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_is_owner**
> groups_is_owner(client_id, group_name)

判断群聊是否为自有群。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    group_name = 'group_name_example' # str | 

    try:
        # 判断群聊是否为自有群。
        api_instance.groups_is_owner(client_id, group_name)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_is_owner: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **group_name** | **str**|  | 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_quit**
> groups_quit(client_id, group_name, clear_history=clear_history)

退出群聊。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    group_name = 'group_name_example' # str | 
    clear_history = True # bool |  (optional) (default to True)

    try:
        # 退出群聊。
        api_instance.groups_quit(client_id, group_name, clear_history=clear_history)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_quit: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **group_name** | **str**|  | 
 **clear_history** | **bool**|  | [optional] [default to True]

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_remove_members**
> groups_remove_members(client_id, group_member_request=group_member_request)

移除群聊成员（适用于自有群）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.group_member_request import GroupMemberRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    group_member_request = wechatauto.GroupMemberRequest() # GroupMemberRequest |  (optional)

    try:
        # 移除群聊成员（适用于自有群）。
        api_instance.groups_remove_members(client_id, group_member_request=group_member_request)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_remove_members: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **group_member_request** | [**GroupMemberRequest**](GroupMemberRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **groups_update_notice**
> groups_update_notice(client_id, update_group_notice_request=update_group_notice_request)

更新群公告（仅适用于自有群）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.update_group_notice_request import UpdateGroupNoticeRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.GroupsApi(api_client)
    client_id = 'client_id_example' # str | 
    update_group_notice_request = wechatauto.UpdateGroupNoticeRequest() # UpdateGroupNoticeRequest |  (optional)

    try:
        # 更新群公告（仅适用于自有群）。
        api_instance.groups_update_notice(client_id, update_group_notice_request=update_group_notice_request)
    except Exception as e:
        print("Exception when calling GroupsApi->groups_update_notice: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **update_group_notice_request** | [**UpdateGroupNoticeRequest**](UpdateGroupNoticeRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

