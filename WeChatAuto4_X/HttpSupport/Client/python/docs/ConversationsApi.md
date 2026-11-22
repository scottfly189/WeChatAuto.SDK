# wechatauto.ConversationsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**conversations_get_all_conversations**](ConversationsApi.md#conversations_get_all_conversations) | **GET** /api/v1/clients/{clientId}/conversations | 获取会话列表所有会话的标题。
[**conversations_get_visible_conversations**](ConversationsApi.md#conversations_get_visible_conversations) | **GET** /api/v1/clients/{clientId}/conversations/visible | 获取可见会话列表（含名称、未读消息数、头像等）。
[**conversations_get_visible_titles**](ConversationsApi.md#conversations_get_visible_titles) | **GET** /api/v1/clients/{clientId}/conversations/visible-titles | 获取会话列表可见会话的标题。
[**conversations_locate**](ConversationsApi.md#conversations_locate) | **POST** /api/v1/clients/{clientId}/conversations/locate | 定位会话，滚动会话列表使指定会话可见。
[**conversations_search**](ConversationsApi.md#conversations_search) | **POST** /api/v1/clients/{clientId}/conversations/search | 搜索好友/群聊。
[**conversations_set_do_not_disturb**](ConversationsApi.md#conversations_set_do_not_disturb) | **POST** /api/v1/clients/{clientId}/conversations/set-do-not-disturb | 设置会话消息免打扰。
[**conversations_set_top_most**](ConversationsApi.md#conversations_set_top_most) | **POST** /api/v1/clients/{clientId}/conversations/set-top-most | 设置会话置顶。


# **conversations_get_all_conversations**
> conversations_get_all_conversations(client_id)

获取会话列表所有会话的标题。

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
    api_instance = wechatauto.ConversationsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 获取会话列表所有会话的标题。
        api_instance.conversations_get_all_conversations(client_id)
    except Exception as e:
        print("Exception when calling ConversationsApi->conversations_get_all_conversations: %s\n" % e)
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

# **conversations_get_visible_conversations**
> conversations_get_visible_conversations(client_id)

获取可见会话列表（含名称、未读消息数、头像等）。

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
    api_instance = wechatauto.ConversationsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 获取可见会话列表（含名称、未读消息数、头像等）。
        api_instance.conversations_get_visible_conversations(client_id)
    except Exception as e:
        print("Exception when calling ConversationsApi->conversations_get_visible_conversations: %s\n" % e)
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

# **conversations_get_visible_titles**
> conversations_get_visible_titles(client_id)

获取会话列表可见会话的标题。

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
    api_instance = wechatauto.ConversationsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 获取会话列表可见会话的标题。
        api_instance.conversations_get_visible_titles(client_id)
    except Exception as e:
        print("Exception when calling ConversationsApi->conversations_get_visible_titles: %s\n" % e)
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

# **conversations_locate**
> conversations_locate(client_id, locate_conversation_request=locate_conversation_request)

定位会话，滚动会话列表使指定会话可见。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.locate_conversation_request import LocateConversationRequest
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
    api_instance = wechatauto.ConversationsApi(api_client)
    client_id = 'client_id_example' # str | 
    locate_conversation_request = wechatauto.LocateConversationRequest() # LocateConversationRequest |  (optional)

    try:
        # 定位会话，滚动会话列表使指定会话可见。
        api_instance.conversations_locate(client_id, locate_conversation_request=locate_conversation_request)
    except Exception as e:
        print("Exception when calling ConversationsApi->conversations_locate: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **locate_conversation_request** | [**LocateConversationRequest**](LocateConversationRequest.md)|  | [optional] 

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

# **conversations_search**
> conversations_search(client_id, search_conversation_request=search_conversation_request)

搜索好友/群聊。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.search_conversation_request import SearchConversationRequest
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
    api_instance = wechatauto.ConversationsApi(api_client)
    client_id = 'client_id_example' # str | 
    search_conversation_request = wechatauto.SearchConversationRequest() # SearchConversationRequest |  (optional)

    try:
        # 搜索好友/群聊。
        api_instance.conversations_search(client_id, search_conversation_request=search_conversation_request)
    except Exception as e:
        print("Exception when calling ConversationsApi->conversations_search: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **search_conversation_request** | [**SearchConversationRequest**](SearchConversationRequest.md)|  | [optional] 

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

# **conversations_set_do_not_disturb**
> conversations_set_do_not_disturb(client_id, set_do_not_disturb_request=set_do_not_disturb_request)

设置会话消息免打扰。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.set_do_not_disturb_request import SetDoNotDisturbRequest
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
    api_instance = wechatauto.ConversationsApi(api_client)
    client_id = 'client_id_example' # str | 
    set_do_not_disturb_request = wechatauto.SetDoNotDisturbRequest() # SetDoNotDisturbRequest |  (optional)

    try:
        # 设置会话消息免打扰。
        api_instance.conversations_set_do_not_disturb(client_id, set_do_not_disturb_request=set_do_not_disturb_request)
    except Exception as e:
        print("Exception when calling ConversationsApi->conversations_set_do_not_disturb: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **set_do_not_disturb_request** | [**SetDoNotDisturbRequest**](SetDoNotDisturbRequest.md)|  | [optional] 

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

# **conversations_set_top_most**
> conversations_set_top_most(client_id, set_top_most_request=set_top_most_request)

设置会话置顶。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.set_top_most_request import SetTopMostRequest
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
    api_instance = wechatauto.ConversationsApi(api_client)
    client_id = 'client_id_example' # str | 
    set_top_most_request = wechatauto.SetTopMostRequest() # SetTopMostRequest |  (optional)

    try:
        # 设置会话置顶。
        api_instance.conversations_set_top_most(client_id, set_top_most_request=set_top_most_request)
    except Exception as e:
        print("Exception when calling ConversationsApi->conversations_set_top_most: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **set_top_most_request** | [**SetTopMostRequest**](SetTopMostRequest.md)|  | [optional] 

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

