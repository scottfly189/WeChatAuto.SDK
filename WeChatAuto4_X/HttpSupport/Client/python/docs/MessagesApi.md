# wechatauto.MessagesApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**messages_clone_voice**](MessagesApi.md#messages_clone_voice) | **POST** /api/v1/clients/{clientId}/messages/clone-voice | 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。
[**messages_focus_input**](MessagesApi.md#messages_focus_input) | **POST** /api/v1/clients/{clientId}/messages/focus-input | 聚焦当前窗口的消息输入区。
[**messages_forward_multiple**](MessagesApi.md#messages_forward_multiple) | **POST** /api/v1/clients/{clientId}/messages/forward-multiple | 转发多条消息（默认合并转发）。
[**messages_forward_single**](MessagesApi.md#messages_forward_single) | **POST** /api/v1/clients/{clientId}/messages/forward-single | 转发单条消息（message 与 chat_simple_message 二选一）。
[**messages_get_history**](MessagesApi.md#messages_get_history) | **GET** /api/v1/clients/{clientId}/messages/history | 获取聊天历史（支持按日期或时间段查询）。
[**messages_get_only_title**](MessagesApi.md#messages_get_only_title) | **GET** /api/v1/clients/{clientId}/messages/only-title | 获取当前聊天窗口的标题名称。
[**messages_get_title**](MessagesApi.md#messages_get_title) | **GET** /api/v1/clients/{clientId}/messages/title | 获取当前聊天窗口的标题对象。
[**messages_pull**](MessagesApi.md#messages_pull) | **POST** /api/v1/clients/{clientId}/messages/pull | 轮询拉取指定会话的新消息（用于自己实现轮询）。
[**messages_reference**](MessagesApi.md#messages_reference) | **POST** /api/v1/clients/{clientId}/messages/reference | 引用当前窗口的好友消息。
[**messages_reference_last**](MessagesApi.md#messages_reference_last) | **POST** /api/v1/clients/{clientId}/messages/reference-last | 引用当前窗口最后一条消息。
[**messages_send**](MessagesApi.md#messages_send) | **POST** /api/v1/clients/{clientId}/messages/send | 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。
[**messages_send_and_close**](MessagesApi.md#messages_send_and_close) | **POST** /api/v1/clients/{clientId}/messages/send-and-close | 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。
[**messages_send_by_wxid**](MessagesApi.md#messages_send_by_wxid) | **POST** /api/v1/clients/{clientId}/messages/send-by-wxid | 按联系人 UserName（wxid / 群 id）发送文本。
[**messages_send_emoji**](MessagesApi.md#messages_send_emoji) | **POST** /api/v1/clients/{clientId}/messages/send-emoji | 发送表情（按名称/描述/索引）。
[**messages_send_emoji_and_close**](MessagesApi.md#messages_send_emoji_and_close) | **POST** /api/v1/clients/{clientId}/messages/send-emoji-and-close | 发送表情后关闭微信窗口（按名称/描述/索引）。
[**messages_send_emoji_by_wxid**](MessagesApi.md#messages_send_emoji_by_wxid) | **POST** /api/v1/clients/{clientId}/messages/send-emoji-by-wxid | 按联系人 UserName（wxid / 群 id）发送表情。
[**messages_send_file**](MessagesApi.md#messages_send_file) | **POST** /api/v1/clients/{clientId}/messages/send-file | 发送文件。
[**messages_send_file_and_close**](MessagesApi.md#messages_send_file_and_close) | **POST** /api/v1/clients/{clientId}/messages/send-file-and-close | 发送文件后关闭微信窗口。
[**messages_send_file_by_wxid**](MessagesApi.md#messages_send_file_by_wxid) | **POST** /api/v1/clients/{clientId}/messages/send-file-by-wxid | 按联系人 UserName（wxid / 群 id）发送文件。
[**messages_send_video_chat**](MessagesApi.md#messages_send_video_chat) | **POST** /api/v1/clients/{clientId}/messages/send-video-chat | 发起单人视频聊天。
[**messages_send_voice_chat**](MessagesApi.md#messages_send_voice_chat) | **POST** /api/v1/clients/{clientId}/messages/send-voice-chat | 发起单人语音聊天。
[**messages_send_voice_chats**](MessagesApi.md#messages_send_voice_chats) | **POST** /api/v1/clients/{clientId}/messages/send-voice-chats | 发起多人语音聊天（群聊）。
[**messages_send_voice_message**](MessagesApi.md#messages_send_voice_message) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message | 发送语音消息（需虚拟声卡 Cable input/Cable output）。
[**messages_send_voice_message_and_close**](MessagesApi.md#messages_send_voice_message_and_close) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-and-close | 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。
[**messages_send_voice_message_by_wxid**](MessagesApi.md#messages_send_voice_message_by_wxid) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-by-wxid | 按联系人 UserName（wxid / 群 id）发送语音文件。
[**messages_send_voice_message_tts**](MessagesApi.md#messages_send_voice_message_tts) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts | 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。
[**messages_send_voice_message_tts_and_close**](MessagesApi.md#messages_send_voice_message_tts_and_close) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts-and-close | 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。
[**messages_send_voice_message_tts_by_wxid**](MessagesApi.md#messages_send_voice_message_tts_by_wxid) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts-by-wxid | 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。
[**messages_tap**](MessagesApi.md#messages_tap) | **POST** /api/v1/clients/{clientId}/messages/tap | 拍一拍当前聊天窗口的好友。


# **messages_clone_voice**
> messages_clone_voice(client_id, clone_voice_request=clone_voice_request)

声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.clone_voice_request import CloneVoiceRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    clone_voice_request = wechatauto.CloneVoiceRequest() # CloneVoiceRequest |  (optional)

    try:
        # 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。
        api_instance.messages_clone_voice(client_id, clone_voice_request=clone_voice_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_clone_voice: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **clone_voice_request** | [**CloneVoiceRequest**](CloneVoiceRequest.md)|  | [optional] 

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

# **messages_focus_input**
> messages_focus_input(client_id)

聚焦当前窗口的消息输入区。

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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 聚焦当前窗口的消息输入区。
        api_instance.messages_focus_input(client_id)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_focus_input: %s\n" % e)
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

# **messages_forward_multiple**
> messages_forward_multiple(client_id, forward_multiple_message_request=forward_multiple_message_request)

转发多条消息（默认合并转发）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.forward_multiple_message_request import ForwardMultipleMessageRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    forward_multiple_message_request = wechatauto.ForwardMultipleMessageRequest() # ForwardMultipleMessageRequest |  (optional)

    try:
        # 转发多条消息（默认合并转发）。
        api_instance.messages_forward_multiple(client_id, forward_multiple_message_request=forward_multiple_message_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_forward_multiple: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **forward_multiple_message_request** | [**ForwardMultipleMessageRequest**](ForwardMultipleMessageRequest.md)|  | [optional] 

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

# **messages_forward_single**
> messages_forward_single(client_id, forward_single_message_request=forward_single_message_request)

转发单条消息（message 与 chat_simple_message 二选一）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.forward_single_message_request import ForwardSingleMessageRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    forward_single_message_request = wechatauto.ForwardSingleMessageRequest() # ForwardSingleMessageRequest |  (optional)

    try:
        # 转发单条消息（message 与 chat_simple_message 二选一）。
        api_instance.messages_forward_single(client_id, forward_single_message_request=forward_single_message_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_forward_single: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **forward_single_message_request** | [**ForwardSingleMessageRequest**](ForwardSingleMessageRequest.md)|  | [optional] 

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

# **messages_get_history**
> messages_get_history(client_id, who=who, var_date=var_date, start_date=start_date, end_date=end_date)

获取聊天历史（支持按日期或时间段查询）。

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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    who = 'who_example' # str |  (optional)
    var_date = '2013-10-20T19:20:30+01:00' # datetime |  (optional)
    start_date = '2013-10-20T19:20:30+01:00' # datetime |  (optional)
    end_date = '2013-10-20T19:20:30+01:00' # datetime |  (optional)

    try:
        # 获取聊天历史（支持按日期或时间段查询）。
        api_instance.messages_get_history(client_id, who=who, var_date=var_date, start_date=start_date, end_date=end_date)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_get_history: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **who** | **str**|  | [optional] 
 **var_date** | **datetime**|  | [optional] 
 **start_date** | **datetime**|  | [optional] 
 **end_date** | **datetime**|  | [optional] 

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

# **messages_get_only_title**
> messages_get_only_title(client_id)

获取当前聊天窗口的标题名称。

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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 获取当前聊天窗口的标题名称。
        api_instance.messages_get_only_title(client_id)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_get_only_title: %s\n" % e)
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

# **messages_get_title**
> messages_get_title(client_id)

获取当前聊天窗口的标题对象。

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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 获取当前聊天窗口的标题对象。
        api_instance.messages_get_title(client_id)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_get_title: %s\n" % e)
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

# **messages_pull**
> messages_pull(client_id, pull_messages_request=pull_messages_request)

轮询拉取指定会话的新消息（用于自己实现轮询）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.pull_messages_request import PullMessagesRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    pull_messages_request = wechatauto.PullMessagesRequest() # PullMessagesRequest |  (optional)

    try:
        # 轮询拉取指定会话的新消息（用于自己实现轮询）。
        api_instance.messages_pull(client_id, pull_messages_request=pull_messages_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_pull: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **pull_messages_request** | [**PullMessagesRequest**](PullMessagesRequest.md)|  | [optional] 

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

# **messages_reference**
> messages_reference(client_id, reference_request=reference_request)

引用当前窗口的好友消息。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.reference_request import ReferenceRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    reference_request = wechatauto.ReferenceRequest() # ReferenceRequest |  (optional)

    try:
        # 引用当前窗口的好友消息。
        api_instance.messages_reference(client_id, reference_request=reference_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_reference: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **reference_request** | [**ReferenceRequest**](ReferenceRequest.md)|  | [optional] 

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

# **messages_reference_last**
> messages_reference_last(client_id)

引用当前窗口最后一条消息。

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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 引用当前窗口最后一条消息。
        api_instance.messages_reference_last(client_id)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_reference_last: %s\n" % e)
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

# **messages_send**
> messages_send(client_id, send_message_request=send_message_request)

发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_message_request import SendMessageRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_message_request = wechatauto.SendMessageRequest() # SendMessageRequest |  (optional)

    try:
        # 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。
        api_instance.messages_send(client_id, send_message_request=send_message_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_message_request** | [**SendMessageRequest**](SendMessageRequest.md)|  | [optional] 

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

# **messages_send_and_close**
> messages_send_and_close(client_id, send_message_request=send_message_request)

发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_message_request import SendMessageRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_message_request = wechatauto.SendMessageRequest() # SendMessageRequest |  (optional)

    try:
        # 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。
        api_instance.messages_send_and_close(client_id, send_message_request=send_message_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_and_close: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_message_request** | [**SendMessageRequest**](SendMessageRequest.md)|  | [optional] 

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

# **messages_send_by_wxid**
> messages_send_by_wxid(client_id, send_message_by_wxid_request=send_message_by_wxid_request)

按联系人 UserName（wxid / 群 id）发送文本。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_message_by_wxid_request import SendMessageByWxidRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_message_by_wxid_request = wechatauto.SendMessageByWxidRequest() # SendMessageByWxidRequest |  (optional)

    try:
        # 按联系人 UserName（wxid / 群 id）发送文本。
        api_instance.messages_send_by_wxid(client_id, send_message_by_wxid_request=send_message_by_wxid_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_by_wxid: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_message_by_wxid_request** | [**SendMessageByWxidRequest**](SendMessageByWxidRequest.md)|  | [optional] 

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

# **messages_send_emoji**
> messages_send_emoji(client_id, send_emoji_request=send_emoji_request)

发送表情（按名称/描述/索引）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_emoji_request import SendEmojiRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_emoji_request = wechatauto.SendEmojiRequest() # SendEmojiRequest |  (optional)

    try:
        # 发送表情（按名称/描述/索引）。
        api_instance.messages_send_emoji(client_id, send_emoji_request=send_emoji_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_emoji: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_emoji_request** | [**SendEmojiRequest**](SendEmojiRequest.md)|  | [optional] 

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

# **messages_send_emoji_and_close**
> messages_send_emoji_and_close(client_id, send_emoji_request=send_emoji_request)

发送表情后关闭微信窗口（按名称/描述/索引）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_emoji_request import SendEmojiRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_emoji_request = wechatauto.SendEmojiRequest() # SendEmojiRequest |  (optional)

    try:
        # 发送表情后关闭微信窗口（按名称/描述/索引）。
        api_instance.messages_send_emoji_and_close(client_id, send_emoji_request=send_emoji_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_emoji_and_close: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_emoji_request** | [**SendEmojiRequest**](SendEmojiRequest.md)|  | [optional] 

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

# **messages_send_emoji_by_wxid**
> messages_send_emoji_by_wxid(client_id, send_emoji_by_wxid_request=send_emoji_by_wxid_request)

按联系人 UserName（wxid / 群 id）发送表情。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_emoji_by_wxid_request import SendEmojiByWxidRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_emoji_by_wxid_request = wechatauto.SendEmojiByWxidRequest() # SendEmojiByWxidRequest |  (optional)

    try:
        # 按联系人 UserName（wxid / 群 id）发送表情。
        api_instance.messages_send_emoji_by_wxid(client_id, send_emoji_by_wxid_request=send_emoji_by_wxid_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_emoji_by_wxid: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_emoji_by_wxid_request** | [**SendEmojiByWxidRequest**](SendEmojiByWxidRequest.md)|  | [optional] 

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

# **messages_send_file**
> messages_send_file(client_id, send_file_request=send_file_request)

发送文件。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_file_request import SendFileRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_file_request = wechatauto.SendFileRequest() # SendFileRequest |  (optional)

    try:
        # 发送文件。
        api_instance.messages_send_file(client_id, send_file_request=send_file_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_file: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_file_request** | [**SendFileRequest**](SendFileRequest.md)|  | [optional] 

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

# **messages_send_file_and_close**
> messages_send_file_and_close(client_id, send_file_request=send_file_request)

发送文件后关闭微信窗口。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_file_request import SendFileRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_file_request = wechatauto.SendFileRequest() # SendFileRequest |  (optional)

    try:
        # 发送文件后关闭微信窗口。
        api_instance.messages_send_file_and_close(client_id, send_file_request=send_file_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_file_and_close: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_file_request** | [**SendFileRequest**](SendFileRequest.md)|  | [optional] 

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

# **messages_send_file_by_wxid**
> messages_send_file_by_wxid(client_id, send_file_by_wxid_request=send_file_by_wxid_request)

按联系人 UserName（wxid / 群 id）发送文件。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_file_by_wxid_request import SendFileByWxidRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_file_by_wxid_request = wechatauto.SendFileByWxidRequest() # SendFileByWxidRequest |  (optional)

    try:
        # 按联系人 UserName（wxid / 群 id）发送文件。
        api_instance.messages_send_file_by_wxid(client_id, send_file_by_wxid_request=send_file_by_wxid_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_file_by_wxid: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_file_by_wxid_request** | [**SendFileByWxidRequest**](SendFileByWxidRequest.md)|  | [optional] 

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

# **messages_send_video_chat**
> messages_send_video_chat(client_id, who_request=who_request)

发起单人视频聊天。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.who_request import WhoRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    who_request = wechatauto.WhoRequest() # WhoRequest |  (optional)

    try:
        # 发起单人视频聊天。
        api_instance.messages_send_video_chat(client_id, who_request=who_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_video_chat: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **who_request** | [**WhoRequest**](WhoRequest.md)|  | [optional] 

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

# **messages_send_voice_chat**
> messages_send_voice_chat(client_id, who_request=who_request)

发起单人语音聊天。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.who_request import WhoRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    who_request = wechatauto.WhoRequest() # WhoRequest |  (optional)

    try:
        # 发起单人语音聊天。
        api_instance.messages_send_voice_chat(client_id, who_request=who_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_voice_chat: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **who_request** | [**WhoRequest**](WhoRequest.md)|  | [optional] 

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

# **messages_send_voice_chats**
> messages_send_voice_chats(client_id, send_voice_chats_request=send_voice_chats_request)

发起多人语音聊天（群聊）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_voice_chats_request import SendVoiceChatsRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_voice_chats_request = wechatauto.SendVoiceChatsRequest() # SendVoiceChatsRequest |  (optional)

    try:
        # 发起多人语音聊天（群聊）。
        api_instance.messages_send_voice_chats(client_id, send_voice_chats_request=send_voice_chats_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_voice_chats: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_voice_chats_request** | [**SendVoiceChatsRequest**](SendVoiceChatsRequest.md)|  | [optional] 

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

# **messages_send_voice_message**
> messages_send_voice_message(client_id, send_voice_message_request=send_voice_message_request)

发送语音消息（需虚拟声卡 Cable input/Cable output）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_voice_message_request import SendVoiceMessageRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_voice_message_request = wechatauto.SendVoiceMessageRequest() # SendVoiceMessageRequest |  (optional)

    try:
        # 发送语音消息（需虚拟声卡 Cable input/Cable output）。
        api_instance.messages_send_voice_message(client_id, send_voice_message_request=send_voice_message_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_voice_message: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_voice_message_request** | [**SendVoiceMessageRequest**](SendVoiceMessageRequest.md)|  | [optional] 

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

# **messages_send_voice_message_and_close**
> messages_send_voice_message_and_close(client_id, send_voice_message_request=send_voice_message_request)

发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_voice_message_request import SendVoiceMessageRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_voice_message_request = wechatauto.SendVoiceMessageRequest() # SendVoiceMessageRequest |  (optional)

    try:
        # 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。
        api_instance.messages_send_voice_message_and_close(client_id, send_voice_message_request=send_voice_message_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_voice_message_and_close: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_voice_message_request** | [**SendVoiceMessageRequest**](SendVoiceMessageRequest.md)|  | [optional] 

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

# **messages_send_voice_message_by_wxid**
> messages_send_voice_message_by_wxid(client_id, send_voice_message_by_wxid_request=send_voice_message_by_wxid_request)

按联系人 UserName（wxid / 群 id）发送语音文件。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_voice_message_by_wxid_request import SendVoiceMessageByWxidRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_voice_message_by_wxid_request = wechatauto.SendVoiceMessageByWxidRequest() # SendVoiceMessageByWxidRequest |  (optional)

    try:
        # 按联系人 UserName（wxid / 群 id）发送语音文件。
        api_instance.messages_send_voice_message_by_wxid(client_id, send_voice_message_by_wxid_request=send_voice_message_by_wxid_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_voice_message_by_wxid: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_voice_message_by_wxid_request** | [**SendVoiceMessageByWxidRequest**](SendVoiceMessageByWxidRequest.md)|  | [optional] 

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

# **messages_send_voice_message_tts**
> messages_send_voice_message_tts(client_id, send_voice_message_tts_request=send_voice_message_tts_request)

文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_voice_message_tts_request import SendVoiceMessageTtsRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_voice_message_tts_request = wechatauto.SendVoiceMessageTtsRequest() # SendVoiceMessageTtsRequest |  (optional)

    try:
        # 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。
        api_instance.messages_send_voice_message_tts(client_id, send_voice_message_tts_request=send_voice_message_tts_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_voice_message_tts: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_voice_message_tts_request** | [**SendVoiceMessageTtsRequest**](SendVoiceMessageTtsRequest.md)|  | [optional] 

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

# **messages_send_voice_message_tts_and_close**
> messages_send_voice_message_tts_and_close(client_id, send_voice_message_tts_request=send_voice_message_tts_request)

文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_voice_message_tts_request import SendVoiceMessageTtsRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_voice_message_tts_request = wechatauto.SendVoiceMessageTtsRequest() # SendVoiceMessageTtsRequest |  (optional)

    try:
        # 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。
        api_instance.messages_send_voice_message_tts_and_close(client_id, send_voice_message_tts_request=send_voice_message_tts_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_voice_message_tts_and_close: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_voice_message_tts_request** | [**SendVoiceMessageTtsRequest**](SendVoiceMessageTtsRequest.md)|  | [optional] 

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

# **messages_send_voice_message_tts_by_wxid**
> messages_send_voice_message_tts_by_wxid(client_id, send_voice_message_tts_by_wxid_request=send_voice_message_tts_by_wxid_request)

按联系人 UserName（wxid / 群 id）用 TTS 发送语音。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.send_voice_message_tts_by_wxid_request import SendVoiceMessageTtsByWxidRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    send_voice_message_tts_by_wxid_request = wechatauto.SendVoiceMessageTtsByWxidRequest() # SendVoiceMessageTtsByWxidRequest |  (optional)

    try:
        # 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。
        api_instance.messages_send_voice_message_tts_by_wxid(client_id, send_voice_message_tts_by_wxid_request=send_voice_message_tts_by_wxid_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_send_voice_message_tts_by_wxid: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **send_voice_message_tts_by_wxid_request** | [**SendVoiceMessageTtsByWxidRequest**](SendVoiceMessageTtsByWxidRequest.md)|  | [optional] 

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

# **messages_tap**
> messages_tap(client_id, tap_request=tap_request)

拍一拍当前聊天窗口的好友。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.tap_request import TapRequest
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
    api_instance = wechatauto.MessagesApi(api_client)
    client_id = 'client_id_example' # str | 
    tap_request = wechatauto.TapRequest() # TapRequest |  (optional)

    try:
        # 拍一拍当前聊天窗口的好友。
        api_instance.messages_tap(client_id, tap_request=tap_request)
    except Exception as e:
        print("Exception when calling MessagesApi->messages_tap: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **tap_request** | [**TapRequest**](TapRequest.md)|  | [optional] 

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

