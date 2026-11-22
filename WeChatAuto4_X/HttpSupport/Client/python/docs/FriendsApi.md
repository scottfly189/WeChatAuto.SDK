# wechatauto.FriendsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**friends_add**](FriendsApi.md#friends_add) | **POST** /api/v1/clients/{clientId}/friends/add | 通过手机号码/微信号查找并添加好友。
[**friends_add_or_update_friend**](FriendsApi.md#friends_add_or_update_friend) | **POST** /api/v1/clients/{clientId}/friends/cache | 手动新增或修改缓存中的好友对象。
[**friends_close_add_window**](FriendsApi.md#friends_close_add_window) | **POST** /api/v1/clients/{clientId}/friends/add-window/close | 关闭新增朋友窗口。
[**friends_get_all_friend_names**](FriendsApi.md#friends_get_all_friend_names) | **GET** /api/v1/clients/{clientId}/friends/names | 获取所有好友名称列表（有备注优先备注名）。
[**friends_get_all_friends**](FriendsApi.md#friends_get_all_friends) | **GET** /api/v1/clients/{clientId}/friends | 获取所有好友的信息列表（默认优先从缓存读取）。
[**friends_get_friend_by_wxid_from_cache**](FriendsApi.md#friends_get_friend_by_wxid_from_cache) | **GET** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存获取好友信息。
[**friends_get_friend_from_cache**](FriendsApi.md#friends_get_friend_from_cache) | **GET** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存获取单个好友信息。
[**friends_get_friend_list_from_cache**](FriendsApi.md#friends_get_friend_list_from_cache) | **GET** /api/v1/clients/{clientId}/friends/cache | 获取缓存中的全部好友信息列表。
[**friends_get_friends_from_cache**](FriendsApi.md#friends_get_friends_from_cache) | **GET** /api/v1/clients/{clientId}/friends/cache/by-name/{who} | 按名称从缓存获取好友列表（适用于昵称重复场景）。
[**friends_open_add_window**](FriendsApi.md#friends_open_add_window) | **POST** /api/v1/clients/{clientId}/friends/add-window/open | 打开新增朋友窗口。
[**friends_pass_all**](FriendsApi.md#friends_pass_all) | **POST** /api/v1/clients/{clientId}/friends/pass-all | 立即批量通过当前待处理的新好友申请（不启动持续监听）。
[**friends_pause_auto_accept**](FriendsApi.md#friends_pause_auto_accept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/pause | 暂停新好友自动通过监听。
[**friends_remove_friend**](FriendsApi.md#friends_remove_friend) | **DELETE** /api/v1/clients/{clientId}/friends/{nickName} | 从通讯录移除好友。
[**friends_remove_friend_by_wxid_from_cache**](FriendsApi.md#friends_remove_friend_by_wxid_from_cache) | **DELETE** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存移除好友。
[**friends_remove_friend_from_cache**](FriendsApi.md#friends_remove_friend_from_cache) | **DELETE** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存移除好友。
[**friends_resume_auto_accept**](FriendsApi.md#friends_resume_auto_accept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/resume | 恢复新好友自动通过监听。
[**friends_start_auto_accept**](FriendsApi.md#friends_start_auto_accept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/start | 启动新好友自动通过监听（按关键词/后缀/标签规则）。
[**friends_stop_auto_accept**](FriendsApi.md#friends_stop_auto_accept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/stop | 停止新好友自动通过监听。


# **friends_add**
> friends_add(client_id, add_friends_request=add_friends_request)

通过手机号码/微信号查找并添加好友。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.add_friends_request import AddFriendsRequest
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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    add_friends_request = wechatauto.AddFriendsRequest() # AddFriendsRequest |  (optional)

    try:
        # 通过手机号码/微信号查找并添加好友。
        api_instance.friends_add(client_id, add_friends_request=add_friends_request)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_add: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **add_friends_request** | [**AddFriendsRequest**](AddFriendsRequest.md)|  | [optional] 

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

# **friends_add_or_update_friend**
> friends_add_or_update_friend(client_id, friend_info=friend_info)

手动新增或修改缓存中的好友对象。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.friend_info import FriendInfo
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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    friend_info = wechatauto.FriendInfo() # FriendInfo |  (optional)

    try:
        # 手动新增或修改缓存中的好友对象。
        api_instance.friends_add_or_update_friend(client_id, friend_info=friend_info)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_add_or_update_friend: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **friend_info** | [**FriendInfo**](FriendInfo.md)|  | [optional] 

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

# **friends_close_add_window**
> friends_close_add_window(client_id)

关闭新增朋友窗口。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 关闭新增朋友窗口。
        api_instance.friends_close_add_window(client_id)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_close_add_window: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 

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

# **friends_get_all_friend_names**
> friends_get_all_friend_names(client_id)

获取所有好友名称列表（有备注优先备注名）。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 获取所有好友名称列表（有备注优先备注名）。
        api_instance.friends_get_all_friend_names(client_id)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_get_all_friend_names: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 

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

# **friends_get_all_friends**
> friends_get_all_friends(client_id, from_cache=from_cache)

获取所有好友的信息列表（默认优先从缓存读取）。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    from_cache = True # bool |  (optional) (default to True)

    try:
        # 获取所有好友的信息列表（默认优先从缓存读取）。
        api_instance.friends_get_all_friends(client_id, from_cache=from_cache)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_get_all_friends: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **from_cache** | **bool**|  | [optional] [default to True]

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

# **friends_get_friend_by_wxid_from_cache**
> friends_get_friend_by_wxid_from_cache(client_id, wxid)

按 wxid 从缓存获取好友信息。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    wxid = 'wxid_example' # str | 

    try:
        # 按 wxid 从缓存获取好友信息。
        api_instance.friends_get_friend_by_wxid_from_cache(client_id, wxid)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_get_friend_by_wxid_from_cache: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **wxid** | **str**|  | 

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

# **friends_get_friend_from_cache**
> friends_get_friend_from_cache(client_id, who)

按名称从缓存获取单个好友信息。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    who = 'who_example' # str | 

    try:
        # 按名称从缓存获取单个好友信息。
        api_instance.friends_get_friend_from_cache(client_id, who)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_get_friend_from_cache: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **who** | **str**|  | 

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

# **friends_get_friend_list_from_cache**
> friends_get_friend_list_from_cache(client_id)

获取缓存中的全部好友信息列表。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 获取缓存中的全部好友信息列表。
        api_instance.friends_get_friend_list_from_cache(client_id)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_get_friend_list_from_cache: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 

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

# **friends_get_friends_from_cache**
> friends_get_friends_from_cache(client_id, who)

按名称从缓存获取好友列表（适用于昵称重复场景）。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    who = 'who_example' # str | 

    try:
        # 按名称从缓存获取好友列表（适用于昵称重复场景）。
        api_instance.friends_get_friends_from_cache(client_id, who)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_get_friends_from_cache: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **who** | **str**|  | 

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

# **friends_open_add_window**
> friends_open_add_window(client_id)

打开新增朋友窗口。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 打开新增朋友窗口。
        api_instance.friends_open_add_window(client_id)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_open_add_window: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 

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

# **friends_pass_all**
> friends_pass_all(client_id, passed_all_new_friend_request=passed_all_new_friend_request)

立即批量通过当前待处理的新好友申请（不启动持续监听）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.passed_all_new_friend_request import PassedAllNewFriendRequest
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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    passed_all_new_friend_request = wechatauto.PassedAllNewFriendRequest() # PassedAllNewFriendRequest |  (optional)

    try:
        # 立即批量通过当前待处理的新好友申请（不启动持续监听）。
        api_instance.friends_pass_all(client_id, passed_all_new_friend_request=passed_all_new_friend_request)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_pass_all: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **passed_all_new_friend_request** | [**PassedAllNewFriendRequest**](PassedAllNewFriendRequest.md)|  | [optional] 

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

# **friends_pause_auto_accept**
> friends_pause_auto_accept(client_id)

暂停新好友自动通过监听。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 暂停新好友自动通过监听。
        api_instance.friends_pause_auto_accept(client_id)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_pause_auto_accept: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 

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

# **friends_remove_friend**
> friends_remove_friend(client_id, nick_name)

从通讯录移除好友。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    nick_name = 'nick_name_example' # str | 

    try:
        # 从通讯录移除好友。
        api_instance.friends_remove_friend(client_id, nick_name)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_remove_friend: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **nick_name** | **str**|  | 

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

# **friends_remove_friend_by_wxid_from_cache**
> friends_remove_friend_by_wxid_from_cache(client_id, wxid)

按 wxid 从缓存移除好友。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    wxid = 'wxid_example' # str | 

    try:
        # 按 wxid 从缓存移除好友。
        api_instance.friends_remove_friend_by_wxid_from_cache(client_id, wxid)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_remove_friend_by_wxid_from_cache: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **wxid** | **str**|  | 

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

# **friends_remove_friend_from_cache**
> friends_remove_friend_from_cache(client_id, who)

按名称从缓存移除好友。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    who = 'who_example' # str | 

    try:
        # 按名称从缓存移除好友。
        api_instance.friends_remove_friend_from_cache(client_id, who)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_remove_friend_from_cache: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **who** | **str**|  | 

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

# **friends_resume_auto_accept**
> friends_resume_auto_accept(client_id)

恢复新好友自动通过监听。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 恢复新好友自动通过监听。
        api_instance.friends_resume_auto_accept(client_id)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_resume_auto_accept: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 

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

# **friends_start_auto_accept**
> friends_start_auto_accept(client_id, passed_all_new_friend_request=passed_all_new_friend_request)

启动新好友自动通过监听（按关键词/后缀/标签规则）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.passed_all_new_friend_request import PassedAllNewFriendRequest
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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 
    passed_all_new_friend_request = wechatauto.PassedAllNewFriendRequest() # PassedAllNewFriendRequest |  (optional)

    try:
        # 启动新好友自动通过监听（按关键词/后缀/标签规则）。
        api_instance.friends_start_auto_accept(client_id, passed_all_new_friend_request=passed_all_new_friend_request)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_start_auto_accept: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **passed_all_new_friend_request** | [**PassedAllNewFriendRequest**](PassedAllNewFriendRequest.md)|  | [optional] 

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

# **friends_stop_auto_accept**
> friends_stop_auto_accept(client_id)

停止新好友自动通过监听。

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
    api_instance = wechatauto.FriendsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 停止新好友自动通过监听。
        api_instance.friends_stop_auto_accept(client_id)
    except Exception as e:
        print("Exception when calling FriendsApi->friends_stop_auto_accept: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 

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

