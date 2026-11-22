# WeChatAuto.Client.Api.MessagesApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**MessagesCloneVoice**](MessagesApi.md#messagesclonevoice) | **POST** /api/v1/clients/{clientId}/messages/clone-voice | 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。 |
| [**MessagesFocusInput**](MessagesApi.md#messagesfocusinput) | **POST** /api/v1/clients/{clientId}/messages/focus-input | 聚焦当前窗口的消息输入区。 |
| [**MessagesForwardMultiple**](MessagesApi.md#messagesforwardmultiple) | **POST** /api/v1/clients/{clientId}/messages/forward-multiple | 转发多条消息（默认合并转发）。 |
| [**MessagesForwardSingle**](MessagesApi.md#messagesforwardsingle) | **POST** /api/v1/clients/{clientId}/messages/forward-single | 转发单条消息（message 与 chat_simple_message 二选一）。 |
| [**MessagesGetHistory**](MessagesApi.md#messagesgethistory) | **GET** /api/v1/clients/{clientId}/messages/history | 获取聊天历史（支持按日期或时间段查询）。 |
| [**MessagesGetOnlyTitle**](MessagesApi.md#messagesgetonlytitle) | **GET** /api/v1/clients/{clientId}/messages/only-title | 获取当前聊天窗口的标题名称。 |
| [**MessagesGetTitle**](MessagesApi.md#messagesgettitle) | **GET** /api/v1/clients/{clientId}/messages/title | 获取当前聊天窗口的标题对象。 |
| [**MessagesPull**](MessagesApi.md#messagespull) | **POST** /api/v1/clients/{clientId}/messages/pull | 轮询拉取指定会话的新消息（用于自己实现轮询）。 |
| [**MessagesReference**](MessagesApi.md#messagesreference) | **POST** /api/v1/clients/{clientId}/messages/reference | 引用当前窗口的好友消息。 |
| [**MessagesReferenceLast**](MessagesApi.md#messagesreferencelast) | **POST** /api/v1/clients/{clientId}/messages/reference-last | 引用当前窗口最后一条消息。 |
| [**MessagesSend**](MessagesApi.md#messagessend) | **POST** /api/v1/clients/{clientId}/messages/send | 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。 |
| [**MessagesSendAndClose**](MessagesApi.md#messagessendandclose) | **POST** /api/v1/clients/{clientId}/messages/send-and-close | 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。 |
| [**MessagesSendByWxid**](MessagesApi.md#messagessendbywxid) | **POST** /api/v1/clients/{clientId}/messages/send-by-wxid | 按联系人 UserName（wxid / 群 id）发送文本。 |
| [**MessagesSendEmoji**](MessagesApi.md#messagessendemoji) | **POST** /api/v1/clients/{clientId}/messages/send-emoji | 发送表情（按名称/描述/索引）。 |
| [**MessagesSendEmojiAndClose**](MessagesApi.md#messagessendemojiandclose) | **POST** /api/v1/clients/{clientId}/messages/send-emoji-and-close | 发送表情后关闭微信窗口（按名称/描述/索引）。 |
| [**MessagesSendEmojiByWxid**](MessagesApi.md#messagessendemojibywxid) | **POST** /api/v1/clients/{clientId}/messages/send-emoji-by-wxid | 按联系人 UserName（wxid / 群 id）发送表情。 |
| [**MessagesSendFile**](MessagesApi.md#messagessendfile) | **POST** /api/v1/clients/{clientId}/messages/send-file | 发送文件。 |
| [**MessagesSendFileAndClose**](MessagesApi.md#messagessendfileandclose) | **POST** /api/v1/clients/{clientId}/messages/send-file-and-close | 发送文件后关闭微信窗口。 |
| [**MessagesSendFileByWxid**](MessagesApi.md#messagessendfilebywxid) | **POST** /api/v1/clients/{clientId}/messages/send-file-by-wxid | 按联系人 UserName（wxid / 群 id）发送文件。 |
| [**MessagesSendVideoChat**](MessagesApi.md#messagessendvideochat) | **POST** /api/v1/clients/{clientId}/messages/send-video-chat | 发起单人视频聊天。 |
| [**MessagesSendVoiceChat**](MessagesApi.md#messagessendvoicechat) | **POST** /api/v1/clients/{clientId}/messages/send-voice-chat | 发起单人语音聊天。 |
| [**MessagesSendVoiceChats**](MessagesApi.md#messagessendvoicechats) | **POST** /api/v1/clients/{clientId}/messages/send-voice-chats | 发起多人语音聊天（群聊）。 |
| [**MessagesSendVoiceMessage**](MessagesApi.md#messagessendvoicemessage) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message | 发送语音消息（需虚拟声卡 Cable input/Cable output）。 |
| [**MessagesSendVoiceMessageAndClose**](MessagesApi.md#messagessendvoicemessageandclose) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-and-close | 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。 |
| [**MessagesSendVoiceMessageByWxid**](MessagesApi.md#messagessendvoicemessagebywxid) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-by-wxid | 按联系人 UserName（wxid / 群 id）发送语音文件。 |
| [**MessagesSendVoiceMessageTts**](MessagesApi.md#messagessendvoicemessagetts) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts | 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。 |
| [**MessagesSendVoiceMessageTtsAndClose**](MessagesApi.md#messagessendvoicemessagettsandclose) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts-and-close | 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。 |
| [**MessagesSendVoiceMessageTtsByWxid**](MessagesApi.md#messagessendvoicemessagettsbywxid) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts-by-wxid | 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。 |
| [**MessagesTap**](MessagesApi.md#messagestap) | **POST** /api/v1/clients/{clientId}/messages/tap | 拍一拍当前聊天窗口的好友。 |

<a id="messagesclonevoice"></a>
# **MessagesCloneVoice**
> void MessagesCloneVoice (string clientId, CloneVoiceRequest? cloneVoiceRequest = null)

声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesCloneVoiceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var cloneVoiceRequest = new CloneVoiceRequest?(); // CloneVoiceRequest? |  (optional) 

            try
            {
                // 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。
                apiInstance.MessagesCloneVoice(clientId, cloneVoiceRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesCloneVoice: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesCloneVoiceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。
    apiInstance.MessagesCloneVoiceWithHttpInfo(clientId, cloneVoiceRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesCloneVoiceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **cloneVoiceRequest** | [**CloneVoiceRequest?**](CloneVoiceRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagesfocusinput"></a>
# **MessagesFocusInput**
> void MessagesFocusInput (string clientId)

聚焦当前窗口的消息输入区。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesFocusInputExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 聚焦当前窗口的消息输入区。
                apiInstance.MessagesFocusInput(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesFocusInput: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesFocusInputWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 聚焦当前窗口的消息输入区。
    apiInstance.MessagesFocusInputWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesFocusInputWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagesforwardmultiple"></a>
# **MessagesForwardMultiple**
> void MessagesForwardMultiple (string clientId, ForwardMultipleMessageRequest? forwardMultipleMessageRequest = null)

转发多条消息（默认合并转发）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesForwardMultipleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var forwardMultipleMessageRequest = new ForwardMultipleMessageRequest?(); // ForwardMultipleMessageRequest? |  (optional) 

            try
            {
                // 转发多条消息（默认合并转发）。
                apiInstance.MessagesForwardMultiple(clientId, forwardMultipleMessageRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesForwardMultiple: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesForwardMultipleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 转发多条消息（默认合并转发）。
    apiInstance.MessagesForwardMultipleWithHttpInfo(clientId, forwardMultipleMessageRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesForwardMultipleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **forwardMultipleMessageRequest** | [**ForwardMultipleMessageRequest?**](ForwardMultipleMessageRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagesforwardsingle"></a>
# **MessagesForwardSingle**
> void MessagesForwardSingle (string clientId, ForwardSingleMessageRequest? forwardSingleMessageRequest = null)

转发单条消息（message 与 chat_simple_message 二选一）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesForwardSingleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var forwardSingleMessageRequest = new ForwardSingleMessageRequest?(); // ForwardSingleMessageRequest? |  (optional) 

            try
            {
                // 转发单条消息（message 与 chat_simple_message 二选一）。
                apiInstance.MessagesForwardSingle(clientId, forwardSingleMessageRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesForwardSingle: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesForwardSingleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 转发单条消息（message 与 chat_simple_message 二选一）。
    apiInstance.MessagesForwardSingleWithHttpInfo(clientId, forwardSingleMessageRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesForwardSingleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **forwardSingleMessageRequest** | [**ForwardSingleMessageRequest?**](ForwardSingleMessageRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagesgethistory"></a>
# **MessagesGetHistory**
> void MessagesGetHistory (string clientId, string? who = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null)

获取聊天历史（支持按日期或时间段查询）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesGetHistoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var who = "who_example";  // string? |  (optional) 
            var date = DateTime.Parse("2013-10-20T19:20:30+01:00");  // DateTime? |  (optional) 
            var startDate = DateTime.Parse("2013-10-20T19:20:30+01:00");  // DateTime? |  (optional) 
            var endDate = DateTime.Parse("2013-10-20T19:20:30+01:00");  // DateTime? |  (optional) 

            try
            {
                // 获取聊天历史（支持按日期或时间段查询）。
                apiInstance.MessagesGetHistory(clientId, who, date, startDate, endDate);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesGetHistory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesGetHistoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取聊天历史（支持按日期或时间段查询）。
    apiInstance.MessagesGetHistoryWithHttpInfo(clientId, who, date, startDate, endDate);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesGetHistoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **who** | **string?** |  | [optional]  |
| **date** | **DateTime?** |  | [optional]  |
| **startDate** | **DateTime?** |  | [optional]  |
| **endDate** | **DateTime?** |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagesgetonlytitle"></a>
# **MessagesGetOnlyTitle**
> void MessagesGetOnlyTitle (string clientId)

获取当前聊天窗口的标题名称。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesGetOnlyTitleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取当前聊天窗口的标题名称。
                apiInstance.MessagesGetOnlyTitle(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesGetOnlyTitle: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesGetOnlyTitleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取当前聊天窗口的标题名称。
    apiInstance.MessagesGetOnlyTitleWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesGetOnlyTitleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagesgettitle"></a>
# **MessagesGetTitle**
> void MessagesGetTitle (string clientId)

获取当前聊天窗口的标题对象。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesGetTitleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取当前聊天窗口的标题对象。
                apiInstance.MessagesGetTitle(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesGetTitle: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesGetTitleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取当前聊天窗口的标题对象。
    apiInstance.MessagesGetTitleWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesGetTitleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagespull"></a>
# **MessagesPull**
> void MessagesPull (string clientId, PullMessagesRequest? pullMessagesRequest = null)

轮询拉取指定会话的新消息（用于自己实现轮询）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesPullExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var pullMessagesRequest = new PullMessagesRequest?(); // PullMessagesRequest? |  (optional) 

            try
            {
                // 轮询拉取指定会话的新消息（用于自己实现轮询）。
                apiInstance.MessagesPull(clientId, pullMessagesRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesPull: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesPullWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 轮询拉取指定会话的新消息（用于自己实现轮询）。
    apiInstance.MessagesPullWithHttpInfo(clientId, pullMessagesRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesPullWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **pullMessagesRequest** | [**PullMessagesRequest?**](PullMessagesRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagesreference"></a>
# **MessagesReference**
> void MessagesReference (string clientId, ReferenceRequest? referenceRequest = null)

引用当前窗口的好友消息。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesReferenceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var referenceRequest = new ReferenceRequest?(); // ReferenceRequest? |  (optional) 

            try
            {
                // 引用当前窗口的好友消息。
                apiInstance.MessagesReference(clientId, referenceRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesReference: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesReferenceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 引用当前窗口的好友消息。
    apiInstance.MessagesReferenceWithHttpInfo(clientId, referenceRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesReferenceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **referenceRequest** | [**ReferenceRequest?**](ReferenceRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagesreferencelast"></a>
# **MessagesReferenceLast**
> void MessagesReferenceLast (string clientId)

引用当前窗口最后一条消息。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesReferenceLastExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 引用当前窗口最后一条消息。
                apiInstance.MessagesReferenceLast(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesReferenceLast: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesReferenceLastWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 引用当前窗口最后一条消息。
    apiInstance.MessagesReferenceLastWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesReferenceLastWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessend"></a>
# **MessagesSend**
> void MessagesSend (string clientId, SendMessageRequest? sendMessageRequest = null)

发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendMessageRequest = new SendMessageRequest?(); // SendMessageRequest? |  (optional) 

            try
            {
                // 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。
                apiInstance.MessagesSend(clientId, sendMessageRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSend: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。
    apiInstance.MessagesSendWithHttpInfo(clientId, sendMessageRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendMessageRequest** | [**SendMessageRequest?**](SendMessageRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendandclose"></a>
# **MessagesSendAndClose**
> void MessagesSendAndClose (string clientId, SendMessageRequest? sendMessageRequest = null)

发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendAndCloseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendMessageRequest = new SendMessageRequest?(); // SendMessageRequest? |  (optional) 

            try
            {
                // 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。
                apiInstance.MessagesSendAndClose(clientId, sendMessageRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendAndClose: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendAndCloseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。
    apiInstance.MessagesSendAndCloseWithHttpInfo(clientId, sendMessageRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendAndCloseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendMessageRequest** | [**SendMessageRequest?**](SendMessageRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendbywxid"></a>
# **MessagesSendByWxid**
> void MessagesSendByWxid (string clientId, SendMessageByWxidRequest? sendMessageByWxidRequest = null)

按联系人 UserName（wxid / 群 id）发送文本。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendByWxidExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendMessageByWxidRequest = new SendMessageByWxidRequest?(); // SendMessageByWxidRequest? |  (optional) 

            try
            {
                // 按联系人 UserName（wxid / 群 id）发送文本。
                apiInstance.MessagesSendByWxid(clientId, sendMessageByWxidRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendByWxid: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendByWxidWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按联系人 UserName（wxid / 群 id）发送文本。
    apiInstance.MessagesSendByWxidWithHttpInfo(clientId, sendMessageByWxidRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendByWxidWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendMessageByWxidRequest** | [**SendMessageByWxidRequest?**](SendMessageByWxidRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendemoji"></a>
# **MessagesSendEmoji**
> void MessagesSendEmoji (string clientId, SendEmojiRequest? sendEmojiRequest = null)

发送表情（按名称/描述/索引）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendEmojiExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendEmojiRequest = new SendEmojiRequest?(); // SendEmojiRequest? |  (optional) 

            try
            {
                // 发送表情（按名称/描述/索引）。
                apiInstance.MessagesSendEmoji(clientId, sendEmojiRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendEmoji: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendEmojiWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发送表情（按名称/描述/索引）。
    apiInstance.MessagesSendEmojiWithHttpInfo(clientId, sendEmojiRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendEmojiWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendEmojiRequest** | [**SendEmojiRequest?**](SendEmojiRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendemojiandclose"></a>
# **MessagesSendEmojiAndClose**
> void MessagesSendEmojiAndClose (string clientId, SendEmojiRequest? sendEmojiRequest = null)

发送表情后关闭微信窗口（按名称/描述/索引）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendEmojiAndCloseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendEmojiRequest = new SendEmojiRequest?(); // SendEmojiRequest? |  (optional) 

            try
            {
                // 发送表情后关闭微信窗口（按名称/描述/索引）。
                apiInstance.MessagesSendEmojiAndClose(clientId, sendEmojiRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendEmojiAndClose: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendEmojiAndCloseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发送表情后关闭微信窗口（按名称/描述/索引）。
    apiInstance.MessagesSendEmojiAndCloseWithHttpInfo(clientId, sendEmojiRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendEmojiAndCloseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendEmojiRequest** | [**SendEmojiRequest?**](SendEmojiRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendemojibywxid"></a>
# **MessagesSendEmojiByWxid**
> void MessagesSendEmojiByWxid (string clientId, SendEmojiByWxidRequest? sendEmojiByWxidRequest = null)

按联系人 UserName（wxid / 群 id）发送表情。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendEmojiByWxidExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendEmojiByWxidRequest = new SendEmojiByWxidRequest?(); // SendEmojiByWxidRequest? |  (optional) 

            try
            {
                // 按联系人 UserName（wxid / 群 id）发送表情。
                apiInstance.MessagesSendEmojiByWxid(clientId, sendEmojiByWxidRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendEmojiByWxid: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendEmojiByWxidWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按联系人 UserName（wxid / 群 id）发送表情。
    apiInstance.MessagesSendEmojiByWxidWithHttpInfo(clientId, sendEmojiByWxidRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendEmojiByWxidWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendEmojiByWxidRequest** | [**SendEmojiByWxidRequest?**](SendEmojiByWxidRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendfile"></a>
# **MessagesSendFile**
> void MessagesSendFile (string clientId, SendFileRequest? sendFileRequest = null)

发送文件。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendFileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendFileRequest = new SendFileRequest?(); // SendFileRequest? |  (optional) 

            try
            {
                // 发送文件。
                apiInstance.MessagesSendFile(clientId, sendFileRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendFile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendFileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发送文件。
    apiInstance.MessagesSendFileWithHttpInfo(clientId, sendFileRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendFileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendFileRequest** | [**SendFileRequest?**](SendFileRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendfileandclose"></a>
# **MessagesSendFileAndClose**
> void MessagesSendFileAndClose (string clientId, SendFileRequest? sendFileRequest = null)

发送文件后关闭微信窗口。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendFileAndCloseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendFileRequest = new SendFileRequest?(); // SendFileRequest? |  (optional) 

            try
            {
                // 发送文件后关闭微信窗口。
                apiInstance.MessagesSendFileAndClose(clientId, sendFileRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendFileAndClose: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendFileAndCloseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发送文件后关闭微信窗口。
    apiInstance.MessagesSendFileAndCloseWithHttpInfo(clientId, sendFileRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendFileAndCloseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendFileRequest** | [**SendFileRequest?**](SendFileRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendfilebywxid"></a>
# **MessagesSendFileByWxid**
> void MessagesSendFileByWxid (string clientId, SendFileByWxidRequest? sendFileByWxidRequest = null)

按联系人 UserName（wxid / 群 id）发送文件。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendFileByWxidExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendFileByWxidRequest = new SendFileByWxidRequest?(); // SendFileByWxidRequest? |  (optional) 

            try
            {
                // 按联系人 UserName（wxid / 群 id）发送文件。
                apiInstance.MessagesSendFileByWxid(clientId, sendFileByWxidRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendFileByWxid: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendFileByWxidWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按联系人 UserName（wxid / 群 id）发送文件。
    apiInstance.MessagesSendFileByWxidWithHttpInfo(clientId, sendFileByWxidRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendFileByWxidWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendFileByWxidRequest** | [**SendFileByWxidRequest?**](SendFileByWxidRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendvideochat"></a>
# **MessagesSendVideoChat**
> void MessagesSendVideoChat (string clientId, WhoRequest? whoRequest = null)

发起单人视频聊天。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendVideoChatExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var whoRequest = new WhoRequest?(); // WhoRequest? |  (optional) 

            try
            {
                // 发起单人视频聊天。
                apiInstance.MessagesSendVideoChat(clientId, whoRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendVideoChat: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendVideoChatWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发起单人视频聊天。
    apiInstance.MessagesSendVideoChatWithHttpInfo(clientId, whoRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendVideoChatWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **whoRequest** | [**WhoRequest?**](WhoRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendvoicechat"></a>
# **MessagesSendVoiceChat**
> void MessagesSendVoiceChat (string clientId, WhoRequest? whoRequest = null)

发起单人语音聊天。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendVoiceChatExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var whoRequest = new WhoRequest?(); // WhoRequest? |  (optional) 

            try
            {
                // 发起单人语音聊天。
                apiInstance.MessagesSendVoiceChat(clientId, whoRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceChat: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendVoiceChatWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发起单人语音聊天。
    apiInstance.MessagesSendVoiceChatWithHttpInfo(clientId, whoRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceChatWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **whoRequest** | [**WhoRequest?**](WhoRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendvoicechats"></a>
# **MessagesSendVoiceChats**
> void MessagesSendVoiceChats (string clientId, SendVoiceChatsRequest? sendVoiceChatsRequest = null)

发起多人语音聊天（群聊）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendVoiceChatsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendVoiceChatsRequest = new SendVoiceChatsRequest?(); // SendVoiceChatsRequest? |  (optional) 

            try
            {
                // 发起多人语音聊天（群聊）。
                apiInstance.MessagesSendVoiceChats(clientId, sendVoiceChatsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceChats: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendVoiceChatsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发起多人语音聊天（群聊）。
    apiInstance.MessagesSendVoiceChatsWithHttpInfo(clientId, sendVoiceChatsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceChatsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendVoiceChatsRequest** | [**SendVoiceChatsRequest?**](SendVoiceChatsRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendvoicemessage"></a>
# **MessagesSendVoiceMessage**
> void MessagesSendVoiceMessage (string clientId, SendVoiceMessageRequest? sendVoiceMessageRequest = null)

发送语音消息（需虚拟声卡 Cable input/Cable output）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendVoiceMessageExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendVoiceMessageRequest = new SendVoiceMessageRequest?(); // SendVoiceMessageRequest? |  (optional) 

            try
            {
                // 发送语音消息（需虚拟声卡 Cable input/Cable output）。
                apiInstance.MessagesSendVoiceMessage(clientId, sendVoiceMessageRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessage: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendVoiceMessageWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发送语音消息（需虚拟声卡 Cable input/Cable output）。
    apiInstance.MessagesSendVoiceMessageWithHttpInfo(clientId, sendVoiceMessageRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendVoiceMessageRequest** | [**SendVoiceMessageRequest?**](SendVoiceMessageRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendvoicemessageandclose"></a>
# **MessagesSendVoiceMessageAndClose**
> void MessagesSendVoiceMessageAndClose (string clientId, SendVoiceMessageRequest? sendVoiceMessageRequest = null)

发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendVoiceMessageAndCloseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendVoiceMessageRequest = new SendVoiceMessageRequest?(); // SendVoiceMessageRequest? |  (optional) 

            try
            {
                // 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。
                apiInstance.MessagesSendVoiceMessageAndClose(clientId, sendVoiceMessageRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageAndClose: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendVoiceMessageAndCloseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。
    apiInstance.MessagesSendVoiceMessageAndCloseWithHttpInfo(clientId, sendVoiceMessageRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageAndCloseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendVoiceMessageRequest** | [**SendVoiceMessageRequest?**](SendVoiceMessageRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendvoicemessagebywxid"></a>
# **MessagesSendVoiceMessageByWxid**
> void MessagesSendVoiceMessageByWxid (string clientId, SendVoiceMessageByWxidRequest? sendVoiceMessageByWxidRequest = null)

按联系人 UserName（wxid / 群 id）发送语音文件。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendVoiceMessageByWxidExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendVoiceMessageByWxidRequest = new SendVoiceMessageByWxidRequest?(); // SendVoiceMessageByWxidRequest? |  (optional) 

            try
            {
                // 按联系人 UserName（wxid / 群 id）发送语音文件。
                apiInstance.MessagesSendVoiceMessageByWxid(clientId, sendVoiceMessageByWxidRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageByWxid: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendVoiceMessageByWxidWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按联系人 UserName（wxid / 群 id）发送语音文件。
    apiInstance.MessagesSendVoiceMessageByWxidWithHttpInfo(clientId, sendVoiceMessageByWxidRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageByWxidWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendVoiceMessageByWxidRequest** | [**SendVoiceMessageByWxidRequest?**](SendVoiceMessageByWxidRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendvoicemessagetts"></a>
# **MessagesSendVoiceMessageTts**
> void MessagesSendVoiceMessageTts (string clientId, SendVoiceMessageTtsRequest? sendVoiceMessageTtsRequest = null)

文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendVoiceMessageTtsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendVoiceMessageTtsRequest = new SendVoiceMessageTtsRequest?(); // SendVoiceMessageTtsRequest? |  (optional) 

            try
            {
                // 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。
                apiInstance.MessagesSendVoiceMessageTts(clientId, sendVoiceMessageTtsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageTts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendVoiceMessageTtsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。
    apiInstance.MessagesSendVoiceMessageTtsWithHttpInfo(clientId, sendVoiceMessageTtsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageTtsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendVoiceMessageTtsRequest** | [**SendVoiceMessageTtsRequest?**](SendVoiceMessageTtsRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendvoicemessagettsandclose"></a>
# **MessagesSendVoiceMessageTtsAndClose**
> void MessagesSendVoiceMessageTtsAndClose (string clientId, SendVoiceMessageTtsRequest? sendVoiceMessageTtsRequest = null)

文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendVoiceMessageTtsAndCloseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendVoiceMessageTtsRequest = new SendVoiceMessageTtsRequest?(); // SendVoiceMessageTtsRequest? |  (optional) 

            try
            {
                // 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。
                apiInstance.MessagesSendVoiceMessageTtsAndClose(clientId, sendVoiceMessageTtsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageTtsAndClose: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendVoiceMessageTtsAndCloseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。
    apiInstance.MessagesSendVoiceMessageTtsAndCloseWithHttpInfo(clientId, sendVoiceMessageTtsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageTtsAndCloseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendVoiceMessageTtsRequest** | [**SendVoiceMessageTtsRequest?**](SendVoiceMessageTtsRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagessendvoicemessagettsbywxid"></a>
# **MessagesSendVoiceMessageTtsByWxid**
> void MessagesSendVoiceMessageTtsByWxid (string clientId, SendVoiceMessageTtsByWxidRequest? sendVoiceMessageTtsByWxidRequest = null)

按联系人 UserName（wxid / 群 id）用 TTS 发送语音。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesSendVoiceMessageTtsByWxidExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var sendVoiceMessageTtsByWxidRequest = new SendVoiceMessageTtsByWxidRequest?(); // SendVoiceMessageTtsByWxidRequest? |  (optional) 

            try
            {
                // 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。
                apiInstance.MessagesSendVoiceMessageTtsByWxid(clientId, sendVoiceMessageTtsByWxidRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageTtsByWxid: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesSendVoiceMessageTtsByWxidWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。
    apiInstance.MessagesSendVoiceMessageTtsByWxidWithHttpInfo(clientId, sendVoiceMessageTtsByWxidRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesSendVoiceMessageTtsByWxidWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **sendVoiceMessageTtsByWxidRequest** | [**SendVoiceMessageTtsByWxidRequest?**](SendVoiceMessageTtsByWxidRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="messagestap"></a>
# **MessagesTap**
> void MessagesTap (string clientId, TapRequest? tapRequest = null)

拍一拍当前聊天窗口的好友。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class MessagesTapExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MessagesApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var tapRequest = new TapRequest?(); // TapRequest? |  (optional) 

            try
            {
                // 拍一拍当前聊天窗口的好友。
                apiInstance.MessagesTap(clientId, tapRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MessagesApi.MessagesTap: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MessagesTapWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 拍一拍当前聊天窗口的好友。
    apiInstance.MessagesTapWithHttpInfo(clientId, tapRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MessagesApi.MessagesTapWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **tapRequest** | [**TapRequest?**](TapRequest?.md) |  | [optional]  |

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
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

