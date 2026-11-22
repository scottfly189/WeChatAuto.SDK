# wechatauto.DatabaseApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**database_get_contact**](DatabaseApi.md#database_get_contact) | **GET** /api/v1/clients/{clientId}/database/contacts/{userName} | 按 username（wxid / 群 id）取一条联系人。
[**database_get_contact_by_show_name**](DatabaseApi.md#database_get_contact_by_show_name) | **GET** /api/v1/clients/{clientId}/database/contacts/by-show-name/first | 按显示名取第一条联系人（重名时取 UserName 升序第一条）。
[**database_get_contacts**](DatabaseApi.md#database_get_contacts) | **GET** /api/v1/clients/{clientId}/database/contacts | 获取当前微信号全部联系人（不含已删除）。
[**database_get_contacts_by_show_name**](DatabaseApi.md#database_get_contacts_by_show_name) | **GET** /api/v1/clients/{clientId}/database/contacts/by-show-name | 按显示名取联系人；重名时返回多个。
[**database_get_max_sort_seq**](DatabaseApi.md#database_get_max_sort_seq) | **GET** /api/v1/clients/{clientId}/database/messages/max-sort-seq | 取当前微信号消息的最大 SortSeq（增量水位）。
[**database_get_messages**](DatabaseApi.md#database_get_messages) | **GET** /api/v1/clients/{clientId}/database/messages | 取指定会话最近 N 条消息（从新到旧）。
[**database_get_new_messages**](DatabaseApi.md#database_get_new_messages) | **GET** /api/v1/clients/{clientId}/database/messages/new | 取 SortSeq 大于指定值的新消息（从旧到新）。
[**database_get_recent_messages**](DatabaseApi.md#database_get_recent_messages) | **GET** /api/v1/clients/{clientId}/database/messages/recent | 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。
[**database_get_room_by_show_name**](DatabaseApi.md#database_get_room_by_show_name) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name/first | 按群的显示名取第一条群。
[**database_get_room_members**](DatabaseApi.md#database_get_room_members) | **GET** /api/v1/clients/{clientId}/database/rooms/{roomUsername}/members | 按群 username 取成员列表。
[**database_get_room_members_by_show_name**](DatabaseApi.md#database_get_room_members_by_show_name) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name/members | 按群的显示名取成员列表；重名时取第一条群。
[**database_get_rooms**](DatabaseApi.md#database_get_rooms) | **GET** /api/v1/clients/{clientId}/database/rooms | 获取当前微信号全部群聊。
[**database_get_rooms_by_local_type**](DatabaseApi.md#database_get_rooms_by_local_type) | **GET** /api/v1/clients/{clientId}/database/rooms/by-local-type | 按联系人 LocalType 过滤群聊（本机观察群多为 2）。
[**database_get_rooms_by_show_name**](DatabaseApi.md#database_get_rooms_by_show_name) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name | 按群的显示名取群；重名时返回多个。
[**database_get_sns_post**](DatabaseApi.md#database_get_sns_post) | **GET** /api/v1/clients/{clientId}/database/sns/posts/{tid} | 按 tid 取一条朋友圈（含媒体、点赞、评论）。
[**database_get_sns_posts**](DatabaseApi.md#database_get_sns_posts) | **GET** /api/v1/clients/{clientId}/database/sns/posts | 取最近 N 条朋友圈（含媒体、点赞、评论）。
[**database_get_sns_posts_by_date**](DatabaseApi.md#database_get_sns_posts_by_date) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-date | 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。
[**database_get_sns_posts_by_show_name**](DatabaseApi.md#database_get_sns_posts_by_show_name) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-show-name | 按发布者显示名取朋友圈；重名会一并返回。
[**database_get_sns_posts_by_user_name**](DatabaseApi.md#database_get_sns_posts_by_user_name) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-user | 取某个发布者（真实 UserName / wxid）的朋友圈。
[**database_query**](DatabaseApi.md#database_query) | **POST** /api/v1/clients/{clientId}/database/query | 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。
[**database_search_all_messages**](DatabaseApi.md#database_search_all_messages) | **GET** /api/v1/clients/{clientId}/database/messages/search-all | 按正文关键字跨所有微信号搜索消息。
[**database_search_contacts**](DatabaseApi.md#database_search_contacts) | **GET** /api/v1/clients/{clientId}/database/contacts/search | 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。
[**database_search_messages**](DatabaseApi.md#database_search_messages) | **GET** /api/v1/clients/{clientId}/database/messages/search | 按正文关键字搜索当前微信号消息。


# **database_get_contact**
> database_get_contact(client_id, user_name)

按 username（wxid / 群 id）取一条联系人。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    user_name = 'user_name_example' # str | 

    try:
        # 按 username（wxid / 群 id）取一条联系人。
        api_instance.database_get_contact(client_id, user_name)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_contact: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **user_name** | **str**|  | 

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

# **database_get_contact_by_show_name**
> database_get_contact_by_show_name(client_id, show_name=show_name)

按显示名取第一条联系人（重名时取 UserName 升序第一条）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    show_name = 'show_name_example' # str |  (optional)

    try:
        # 按显示名取第一条联系人（重名时取 UserName 升序第一条）。
        api_instance.database_get_contact_by_show_name(client_id, show_name=show_name)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_contact_by_show_name: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **show_name** | **str**|  | [optional] 

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

# **database_get_contacts**
> database_get_contacts(client_id)

获取当前微信号全部联系人（不含已删除）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 获取当前微信号全部联系人（不含已删除）。
        api_instance.database_get_contacts(client_id)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_contacts: %s\n" % e)
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

# **database_get_contacts_by_show_name**
> database_get_contacts_by_show_name(client_id, show_name=show_name)

按显示名取联系人；重名时返回多个。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    show_name = 'show_name_example' # str |  (optional)

    try:
        # 按显示名取联系人；重名时返回多个。
        api_instance.database_get_contacts_by_show_name(client_id, show_name=show_name)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_contacts_by_show_name: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **show_name** | **str**|  | [optional] 

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

# **database_get_max_sort_seq**
> database_get_max_sort_seq(client_id)

取当前微信号消息的最大 SortSeq（增量水位）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 取当前微信号消息的最大 SortSeq（增量水位）。
        api_instance.database_get_max_sort_seq(client_id)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_max_sort_seq: %s\n" % e)
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

# **database_get_messages**
> database_get_messages(client_id, chat_username=chat_username, count=count)

取指定会话最近 N 条消息（从新到旧）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    chat_username = 'chat_username_example' # str |  (optional)
    count = 50 # int |  (optional) (default to 50)

    try:
        # 取指定会话最近 N 条消息（从新到旧）。
        api_instance.database_get_messages(client_id, chat_username=chat_username, count=count)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_messages: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **chat_username** | **str**|  | [optional] 
 **count** | **int**|  | [optional] [default to 50]

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

# **database_get_new_messages**
> database_get_new_messages(client_id, after_sort_seq=after_sort_seq)

取 SortSeq 大于指定值的新消息（从旧到新）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    after_sort_seq = 56 # int |  (optional)

    try:
        # 取 SortSeq 大于指定值的新消息（从旧到新）。
        api_instance.database_get_new_messages(client_id, after_sort_seq=after_sort_seq)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_new_messages: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **after_sort_seq** | **int**|  | [optional] 

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

# **database_get_recent_messages**
> database_get_recent_messages(client_id, chat_username=chat_username, count=count)

取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    chat_username = 'chat_username_example' # str |  (optional)
    count = 50 # int |  (optional) (default to 50)

    try:
        # 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。
        api_instance.database_get_recent_messages(client_id, chat_username=chat_username, count=count)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_recent_messages: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **chat_username** | **str**|  | [optional] 
 **count** | **int**|  | [optional] [default to 50]

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

# **database_get_room_by_show_name**
> database_get_room_by_show_name(client_id, show_name=show_name)

按群的显示名取第一条群。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    show_name = 'show_name_example' # str |  (optional)

    try:
        # 按群的显示名取第一条群。
        api_instance.database_get_room_by_show_name(client_id, show_name=show_name)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_room_by_show_name: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **show_name** | **str**|  | [optional] 

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

# **database_get_room_members**
> database_get_room_members(client_id, room_username)

按群 username 取成员列表。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    room_username = 'room_username_example' # str | 

    try:
        # 按群 username 取成员列表。
        api_instance.database_get_room_members(client_id, room_username)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_room_members: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **room_username** | **str**|  | 

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

# **database_get_room_members_by_show_name**
> database_get_room_members_by_show_name(client_id, show_name=show_name)

按群的显示名取成员列表；重名时取第一条群。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    show_name = 'show_name_example' # str |  (optional)

    try:
        # 按群的显示名取成员列表；重名时取第一条群。
        api_instance.database_get_room_members_by_show_name(client_id, show_name=show_name)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_room_members_by_show_name: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **show_name** | **str**|  | [optional] 

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

# **database_get_rooms**
> database_get_rooms(client_id)

获取当前微信号全部群聊。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 获取当前微信号全部群聊。
        api_instance.database_get_rooms(client_id)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_rooms: %s\n" % e)
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

# **database_get_rooms_by_local_type**
> database_get_rooms_by_local_type(client_id, local_type=local_type)

按联系人 LocalType 过滤群聊（本机观察群多为 2）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    local_type = 56 # int |  (optional)

    try:
        # 按联系人 LocalType 过滤群聊（本机观察群多为 2）。
        api_instance.database_get_rooms_by_local_type(client_id, local_type=local_type)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_rooms_by_local_type: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **local_type** | **int**|  | [optional] 

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

# **database_get_rooms_by_show_name**
> database_get_rooms_by_show_name(client_id, show_name=show_name)

按群的显示名取群；重名时返回多个。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    show_name = 'show_name_example' # str |  (optional)

    try:
        # 按群的显示名取群；重名时返回多个。
        api_instance.database_get_rooms_by_show_name(client_id, show_name=show_name)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_rooms_by_show_name: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **show_name** | **str**|  | [optional] 

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

# **database_get_sns_post**
> database_get_sns_post(client_id, tid)

按 tid 取一条朋友圈（含媒体、点赞、评论）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    tid = 56 # int | 

    try:
        # 按 tid 取一条朋友圈（含媒体、点赞、评论）。
        api_instance.database_get_sns_post(client_id, tid)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_sns_post: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **tid** | **int**|  | 

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

# **database_get_sns_posts**
> database_get_sns_posts(client_id, limit=limit)

取最近 N 条朋友圈（含媒体、点赞、评论）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    limit = 20 # int |  (optional) (default to 20)

    try:
        # 取最近 N 条朋友圈（含媒体、点赞、评论）。
        api_instance.database_get_sns_posts(client_id, limit=limit)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_sns_posts: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **limit** | **int**|  | [optional] [default to 20]

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

# **database_get_sns_posts_by_date**
> database_get_sns_posts_by_date(client_id, var_date=var_date)

取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    var_date = '2013-10-20T19:20:30+01:00' # datetime |  (optional)

    try:
        # 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。
        api_instance.database_get_sns_posts_by_date(client_id, var_date=var_date)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_sns_posts_by_date: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **var_date** | **datetime**|  | [optional] 

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

# **database_get_sns_posts_by_show_name**
> database_get_sns_posts_by_show_name(client_id, show_name=show_name)

按发布者显示名取朋友圈；重名会一并返回。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    show_name = 'show_name_example' # str |  (optional)

    try:
        # 按发布者显示名取朋友圈；重名会一并返回。
        api_instance.database_get_sns_posts_by_show_name(client_id, show_name=show_name)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_sns_posts_by_show_name: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **show_name** | **str**|  | [optional] 

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

# **database_get_sns_posts_by_user_name**
> database_get_sns_posts_by_user_name(client_id, user_name=user_name)

取某个发布者（真实 UserName / wxid）的朋友圈。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    user_name = 'user_name_example' # str |  (optional)

    try:
        # 取某个发布者（真实 UserName / wxid）的朋友圈。
        api_instance.database_get_sns_posts_by_user_name(client_id, user_name=user_name)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_get_sns_posts_by_user_name: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **user_name** | **str**|  | [optional] 

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

# **database_query**
> database_query(client_id, database_query_request=database_query_request)

执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.database_query_request import DatabaseQueryRequest
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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    database_query_request = wechatauto.DatabaseQueryRequest() # DatabaseQueryRequest |  (optional)

    try:
        # 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。
        api_instance.database_query(client_id, database_query_request=database_query_request)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_query: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **database_query_request** | [**DatabaseQueryRequest**](DatabaseQueryRequest.md)|  | [optional] 

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

# **database_search_all_messages**
> database_search_all_messages(client_id, keyword=keyword, chat_username=chat_username)

按正文关键字跨所有微信号搜索消息。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    keyword = 'keyword_example' # str |  (optional)
    chat_username = 'chat_username_example' # str |  (optional)

    try:
        # 按正文关键字跨所有微信号搜索消息。
        api_instance.database_search_all_messages(client_id, keyword=keyword, chat_username=chat_username)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_search_all_messages: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **keyword** | **str**|  | [optional] 
 **chat_username** | **str**|  | [optional] 

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

# **database_search_contacts**
> database_search_contacts(client_id, keyword=keyword)

按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    keyword = 'keyword_example' # str |  (optional)

    try:
        # 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。
        api_instance.database_search_contacts(client_id, keyword=keyword)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_search_contacts: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **keyword** | **str**|  | [optional] 

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

# **database_search_messages**
> database_search_messages(client_id, keyword=keyword, chat_username=chat_username)

按正文关键字搜索当前微信号消息。

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
    api_instance = wechatauto.DatabaseApi(api_client)
    client_id = 'client_id_example' # str | 
    keyword = 'keyword_example' # str |  (optional)
    chat_username = 'chat_username_example' # str |  (optional)

    try:
        # 按正文关键字搜索当前微信号消息。
        api_instance.database_search_messages(client_id, keyword=keyword, chat_username=chat_username)
    except Exception as e:
        print("Exception when calling DatabaseApi->database_search_messages: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **keyword** | **str**|  | [optional] 
 **chat_username** | **str**|  | [optional] 

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

