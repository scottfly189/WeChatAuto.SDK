# MessagesApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**messagesCloneVoice**](MessagesApi.md#messagesCloneVoice) | **POST** /api/v1/clients/{clientId}/messages/clone-voice | 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。 |
| [**messagesFocusInput**](MessagesApi.md#messagesFocusInput) | **POST** /api/v1/clients/{clientId}/messages/focus-input | 聚焦当前窗口的消息输入区。 |
| [**messagesForwardMultiple**](MessagesApi.md#messagesForwardMultiple) | **POST** /api/v1/clients/{clientId}/messages/forward-multiple | 转发多条消息（默认合并转发）。 |
| [**messagesForwardSingle**](MessagesApi.md#messagesForwardSingle) | **POST** /api/v1/clients/{clientId}/messages/forward-single | 转发单条消息（message 与 chat_simple_message 二选一）。 |
| [**messagesGetHistory**](MessagesApi.md#messagesGetHistory) | **GET** /api/v1/clients/{clientId}/messages/history | 获取聊天历史（支持按日期或时间段查询）。 |
| [**messagesGetOnlyTitle**](MessagesApi.md#messagesGetOnlyTitle) | **GET** /api/v1/clients/{clientId}/messages/only-title | 获取当前聊天窗口的标题名称。 |
| [**messagesGetTitle**](MessagesApi.md#messagesGetTitle) | **GET** /api/v1/clients/{clientId}/messages/title | 获取当前聊天窗口的标题对象。 |
| [**messagesPull**](MessagesApi.md#messagesPull) | **POST** /api/v1/clients/{clientId}/messages/pull | 轮询拉取指定会话的新消息（用于自己实现轮询）。 |
| [**messagesReference**](MessagesApi.md#messagesReference) | **POST** /api/v1/clients/{clientId}/messages/reference | 引用当前窗口的好友消息。 |
| [**messagesReferenceLast**](MessagesApi.md#messagesReferenceLast) | **POST** /api/v1/clients/{clientId}/messages/reference-last | 引用当前窗口最后一条消息。 |
| [**messagesSend**](MessagesApi.md#messagesSend) | **POST** /api/v1/clients/{clientId}/messages/send | 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。 |
| [**messagesSendAndClose**](MessagesApi.md#messagesSendAndClose) | **POST** /api/v1/clients/{clientId}/messages/send-and-close | 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。 |
| [**messagesSendByWxid**](MessagesApi.md#messagesSendByWxid) | **POST** /api/v1/clients/{clientId}/messages/send-by-wxid | 按联系人 UserName（wxid / 群 id）发送文本。 |
| [**messagesSendEmoji**](MessagesApi.md#messagesSendEmoji) | **POST** /api/v1/clients/{clientId}/messages/send-emoji | 发送表情（按名称/描述/索引）。 |
| [**messagesSendEmojiAndClose**](MessagesApi.md#messagesSendEmojiAndClose) | **POST** /api/v1/clients/{clientId}/messages/send-emoji-and-close | 发送表情后关闭微信窗口（按名称/描述/索引）。 |
| [**messagesSendEmojiByWxid**](MessagesApi.md#messagesSendEmojiByWxid) | **POST** /api/v1/clients/{clientId}/messages/send-emoji-by-wxid | 按联系人 UserName（wxid / 群 id）发送表情。 |
| [**messagesSendFile**](MessagesApi.md#messagesSendFile) | **POST** /api/v1/clients/{clientId}/messages/send-file | 发送文件。 |
| [**messagesSendFileAndClose**](MessagesApi.md#messagesSendFileAndClose) | **POST** /api/v1/clients/{clientId}/messages/send-file-and-close | 发送文件后关闭微信窗口。 |
| [**messagesSendFileByWxid**](MessagesApi.md#messagesSendFileByWxid) | **POST** /api/v1/clients/{clientId}/messages/send-file-by-wxid | 按联系人 UserName（wxid / 群 id）发送文件。 |
| [**messagesSendVideoChat**](MessagesApi.md#messagesSendVideoChat) | **POST** /api/v1/clients/{clientId}/messages/send-video-chat | 发起单人视频聊天。 |
| [**messagesSendVoiceChat**](MessagesApi.md#messagesSendVoiceChat) | **POST** /api/v1/clients/{clientId}/messages/send-voice-chat | 发起单人语音聊天。 |
| [**messagesSendVoiceChats**](MessagesApi.md#messagesSendVoiceChats) | **POST** /api/v1/clients/{clientId}/messages/send-voice-chats | 发起多人语音聊天（群聊）。 |
| [**messagesSendVoiceMessage**](MessagesApi.md#messagesSendVoiceMessage) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message | 发送语音消息（需虚拟声卡 Cable input/Cable output）。 |
| [**messagesSendVoiceMessageAndClose**](MessagesApi.md#messagesSendVoiceMessageAndClose) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-and-close | 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。 |
| [**messagesSendVoiceMessageByWxid**](MessagesApi.md#messagesSendVoiceMessageByWxid) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-by-wxid | 按联系人 UserName（wxid / 群 id）发送语音文件。 |
| [**messagesSendVoiceMessageTts**](MessagesApi.md#messagesSendVoiceMessageTts) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts | 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。 |
| [**messagesSendVoiceMessageTtsAndClose**](MessagesApi.md#messagesSendVoiceMessageTtsAndClose) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts-and-close | 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。 |
| [**messagesSendVoiceMessageTtsByWxid**](MessagesApi.md#messagesSendVoiceMessageTtsByWxid) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts-by-wxid | 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。 |
| [**messagesTap**](MessagesApi.md#messagesTap) | **POST** /api/v1/clients/{clientId}/messages/tap | 拍一拍当前聊天窗口的好友。 |


<a id="messagesCloneVoice"></a>
# **messagesCloneVoice**
> messagesCloneVoice(clientId, cloneVoiceRequest)

声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    CloneVoiceRequest cloneVoiceRequest = new CloneVoiceRequest(); // CloneVoiceRequest | 
    try {
      apiInstance.messagesCloneVoice(clientId, cloneVoiceRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesCloneVoice");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **cloneVoiceRequest** | [**CloneVoiceRequest**](CloneVoiceRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesFocusInput"></a>
# **messagesFocusInput**
> messagesFocusInput(clientId)

聚焦当前窗口的消息输入区。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.messagesFocusInput(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesFocusInput");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesForwardMultiple"></a>
# **messagesForwardMultiple**
> messagesForwardMultiple(clientId, forwardMultipleMessageRequest)

转发多条消息（默认合并转发）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    ForwardMultipleMessageRequest forwardMultipleMessageRequest = new ForwardMultipleMessageRequest(); // ForwardMultipleMessageRequest | 
    try {
      apiInstance.messagesForwardMultiple(clientId, forwardMultipleMessageRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesForwardMultiple");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **forwardMultipleMessageRequest** | [**ForwardMultipleMessageRequest**](ForwardMultipleMessageRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesForwardSingle"></a>
# **messagesForwardSingle**
> messagesForwardSingle(clientId, forwardSingleMessageRequest)

转发单条消息（message 与 chat_simple_message 二选一）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    ForwardSingleMessageRequest forwardSingleMessageRequest = new ForwardSingleMessageRequest(); // ForwardSingleMessageRequest | 
    try {
      apiInstance.messagesForwardSingle(clientId, forwardSingleMessageRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesForwardSingle");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **forwardSingleMessageRequest** | [**ForwardSingleMessageRequest**](ForwardSingleMessageRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesGetHistory"></a>
# **messagesGetHistory**
> messagesGetHistory(clientId, who, date, startDate, endDate)

获取聊天历史（支持按日期或时间段查询）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String who = "who_example"; // String | 
    OffsetDateTime date = OffsetDateTime.now(); // OffsetDateTime | 
    OffsetDateTime startDate = OffsetDateTime.now(); // OffsetDateTime | 
    OffsetDateTime endDate = OffsetDateTime.now(); // OffsetDateTime | 
    try {
      apiInstance.messagesGetHistory(clientId, who, date, startDate, endDate);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesGetHistory");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **who** | **String**|  | [optional] |
| **date** | **OffsetDateTime**|  | [optional] |
| **startDate** | **OffsetDateTime**|  | [optional] |
| **endDate** | **OffsetDateTime**|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesGetOnlyTitle"></a>
# **messagesGetOnlyTitle**
> messagesGetOnlyTitle(clientId)

获取当前聊天窗口的标题名称。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.messagesGetOnlyTitle(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesGetOnlyTitle");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesGetTitle"></a>
# **messagesGetTitle**
> messagesGetTitle(clientId)

获取当前聊天窗口的标题对象。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.messagesGetTitle(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesGetTitle");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesPull"></a>
# **messagesPull**
> messagesPull(clientId, pullMessagesRequest)

轮询拉取指定会话的新消息（用于自己实现轮询）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    PullMessagesRequest pullMessagesRequest = new PullMessagesRequest(); // PullMessagesRequest | 
    try {
      apiInstance.messagesPull(clientId, pullMessagesRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesPull");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **pullMessagesRequest** | [**PullMessagesRequest**](PullMessagesRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesReference"></a>
# **messagesReference**
> messagesReference(clientId, referenceRequest)

引用当前窗口的好友消息。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    ReferenceRequest referenceRequest = new ReferenceRequest(); // ReferenceRequest | 
    try {
      apiInstance.messagesReference(clientId, referenceRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesReference");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **referenceRequest** | [**ReferenceRequest**](ReferenceRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesReferenceLast"></a>
# **messagesReferenceLast**
> messagesReferenceLast(clientId)

引用当前窗口最后一条消息。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.messagesReferenceLast(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesReferenceLast");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSend"></a>
# **messagesSend**
> messagesSend(clientId, sendMessageRequest)

发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendMessageRequest sendMessageRequest = new SendMessageRequest(); // SendMessageRequest | 
    try {
      apiInstance.messagesSend(clientId, sendMessageRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSend");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendMessageRequest** | [**SendMessageRequest**](SendMessageRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendAndClose"></a>
# **messagesSendAndClose**
> messagesSendAndClose(clientId, sendMessageRequest)

发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendMessageRequest sendMessageRequest = new SendMessageRequest(); // SendMessageRequest | 
    try {
      apiInstance.messagesSendAndClose(clientId, sendMessageRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendAndClose");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendMessageRequest** | [**SendMessageRequest**](SendMessageRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendByWxid"></a>
# **messagesSendByWxid**
> messagesSendByWxid(clientId, sendMessageByWxidRequest)

按联系人 UserName（wxid / 群 id）发送文本。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendMessageByWxidRequest sendMessageByWxidRequest = new SendMessageByWxidRequest(); // SendMessageByWxidRequest | 
    try {
      apiInstance.messagesSendByWxid(clientId, sendMessageByWxidRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendByWxid");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendMessageByWxidRequest** | [**SendMessageByWxidRequest**](SendMessageByWxidRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendEmoji"></a>
# **messagesSendEmoji**
> messagesSendEmoji(clientId, sendEmojiRequest)

发送表情（按名称/描述/索引）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendEmojiRequest sendEmojiRequest = new SendEmojiRequest(); // SendEmojiRequest | 
    try {
      apiInstance.messagesSendEmoji(clientId, sendEmojiRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendEmoji");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendEmojiRequest** | [**SendEmojiRequest**](SendEmojiRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendEmojiAndClose"></a>
# **messagesSendEmojiAndClose**
> messagesSendEmojiAndClose(clientId, sendEmojiRequest)

发送表情后关闭微信窗口（按名称/描述/索引）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendEmojiRequest sendEmojiRequest = new SendEmojiRequest(); // SendEmojiRequest | 
    try {
      apiInstance.messagesSendEmojiAndClose(clientId, sendEmojiRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendEmojiAndClose");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendEmojiRequest** | [**SendEmojiRequest**](SendEmojiRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendEmojiByWxid"></a>
# **messagesSendEmojiByWxid**
> messagesSendEmojiByWxid(clientId, sendEmojiByWxidRequest)

按联系人 UserName（wxid / 群 id）发送表情。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendEmojiByWxidRequest sendEmojiByWxidRequest = new SendEmojiByWxidRequest(); // SendEmojiByWxidRequest | 
    try {
      apiInstance.messagesSendEmojiByWxid(clientId, sendEmojiByWxidRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendEmojiByWxid");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendEmojiByWxidRequest** | [**SendEmojiByWxidRequest**](SendEmojiByWxidRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendFile"></a>
# **messagesSendFile**
> messagesSendFile(clientId, sendFileRequest)

发送文件。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendFileRequest sendFileRequest = new SendFileRequest(); // SendFileRequest | 
    try {
      apiInstance.messagesSendFile(clientId, sendFileRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendFile");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendFileRequest** | [**SendFileRequest**](SendFileRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendFileAndClose"></a>
# **messagesSendFileAndClose**
> messagesSendFileAndClose(clientId, sendFileRequest)

发送文件后关闭微信窗口。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendFileRequest sendFileRequest = new SendFileRequest(); // SendFileRequest | 
    try {
      apiInstance.messagesSendFileAndClose(clientId, sendFileRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendFileAndClose");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendFileRequest** | [**SendFileRequest**](SendFileRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendFileByWxid"></a>
# **messagesSendFileByWxid**
> messagesSendFileByWxid(clientId, sendFileByWxidRequest)

按联系人 UserName（wxid / 群 id）发送文件。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendFileByWxidRequest sendFileByWxidRequest = new SendFileByWxidRequest(); // SendFileByWxidRequest | 
    try {
      apiInstance.messagesSendFileByWxid(clientId, sendFileByWxidRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendFileByWxid");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendFileByWxidRequest** | [**SendFileByWxidRequest**](SendFileByWxidRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendVideoChat"></a>
# **messagesSendVideoChat**
> messagesSendVideoChat(clientId, whoRequest)

发起单人视频聊天。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    WhoRequest whoRequest = new WhoRequest(); // WhoRequest | 
    try {
      apiInstance.messagesSendVideoChat(clientId, whoRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendVideoChat");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **whoRequest** | [**WhoRequest**](WhoRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendVoiceChat"></a>
# **messagesSendVoiceChat**
> messagesSendVoiceChat(clientId, whoRequest)

发起单人语音聊天。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    WhoRequest whoRequest = new WhoRequest(); // WhoRequest | 
    try {
      apiInstance.messagesSendVoiceChat(clientId, whoRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendVoiceChat");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **whoRequest** | [**WhoRequest**](WhoRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendVoiceChats"></a>
# **messagesSendVoiceChats**
> messagesSendVoiceChats(clientId, sendVoiceChatsRequest)

发起多人语音聊天（群聊）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendVoiceChatsRequest sendVoiceChatsRequest = new SendVoiceChatsRequest(); // SendVoiceChatsRequest | 
    try {
      apiInstance.messagesSendVoiceChats(clientId, sendVoiceChatsRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendVoiceChats");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendVoiceChatsRequest** | [**SendVoiceChatsRequest**](SendVoiceChatsRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendVoiceMessage"></a>
# **messagesSendVoiceMessage**
> messagesSendVoiceMessage(clientId, sendVoiceMessageRequest)

发送语音消息（需虚拟声卡 Cable input/Cable output）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendVoiceMessageRequest sendVoiceMessageRequest = new SendVoiceMessageRequest(); // SendVoiceMessageRequest | 
    try {
      apiInstance.messagesSendVoiceMessage(clientId, sendVoiceMessageRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendVoiceMessage");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendVoiceMessageRequest** | [**SendVoiceMessageRequest**](SendVoiceMessageRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendVoiceMessageAndClose"></a>
# **messagesSendVoiceMessageAndClose**
> messagesSendVoiceMessageAndClose(clientId, sendVoiceMessageRequest)

发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendVoiceMessageRequest sendVoiceMessageRequest = new SendVoiceMessageRequest(); // SendVoiceMessageRequest | 
    try {
      apiInstance.messagesSendVoiceMessageAndClose(clientId, sendVoiceMessageRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendVoiceMessageAndClose");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendVoiceMessageRequest** | [**SendVoiceMessageRequest**](SendVoiceMessageRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendVoiceMessageByWxid"></a>
# **messagesSendVoiceMessageByWxid**
> messagesSendVoiceMessageByWxid(clientId, sendVoiceMessageByWxidRequest)

按联系人 UserName（wxid / 群 id）发送语音文件。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendVoiceMessageByWxidRequest sendVoiceMessageByWxidRequest = new SendVoiceMessageByWxidRequest(); // SendVoiceMessageByWxidRequest | 
    try {
      apiInstance.messagesSendVoiceMessageByWxid(clientId, sendVoiceMessageByWxidRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendVoiceMessageByWxid");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendVoiceMessageByWxidRequest** | [**SendVoiceMessageByWxidRequest**](SendVoiceMessageByWxidRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendVoiceMessageTts"></a>
# **messagesSendVoiceMessageTts**
> messagesSendVoiceMessageTts(clientId, sendVoiceMessageTtsRequest)

文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendVoiceMessageTtsRequest sendVoiceMessageTtsRequest = new SendVoiceMessageTtsRequest(); // SendVoiceMessageTtsRequest | 
    try {
      apiInstance.messagesSendVoiceMessageTts(clientId, sendVoiceMessageTtsRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendVoiceMessageTts");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendVoiceMessageTtsRequest** | [**SendVoiceMessageTtsRequest**](SendVoiceMessageTtsRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendVoiceMessageTtsAndClose"></a>
# **messagesSendVoiceMessageTtsAndClose**
> messagesSendVoiceMessageTtsAndClose(clientId, sendVoiceMessageTtsRequest)

文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendVoiceMessageTtsRequest sendVoiceMessageTtsRequest = new SendVoiceMessageTtsRequest(); // SendVoiceMessageTtsRequest | 
    try {
      apiInstance.messagesSendVoiceMessageTtsAndClose(clientId, sendVoiceMessageTtsRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendVoiceMessageTtsAndClose");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendVoiceMessageTtsRequest** | [**SendVoiceMessageTtsRequest**](SendVoiceMessageTtsRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesSendVoiceMessageTtsByWxid"></a>
# **messagesSendVoiceMessageTtsByWxid**
> messagesSendVoiceMessageTtsByWxid(clientId, sendVoiceMessageTtsByWxidRequest)

按联系人 UserName（wxid / 群 id）用 TTS 发送语音。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SendVoiceMessageTtsByWxidRequest sendVoiceMessageTtsByWxidRequest = new SendVoiceMessageTtsByWxidRequest(); // SendVoiceMessageTtsByWxidRequest | 
    try {
      apiInstance.messagesSendVoiceMessageTtsByWxid(clientId, sendVoiceMessageTtsByWxidRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesSendVoiceMessageTtsByWxid");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **sendVoiceMessageTtsByWxidRequest** | [**SendVoiceMessageTtsByWxidRequest**](SendVoiceMessageTtsByWxidRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="messagesTap"></a>
# **messagesTap**
> messagesTap(clientId, tapRequest)

拍一拍当前聊天窗口的好友。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MessagesApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MessagesApi apiInstance = new MessagesApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    TapRequest tapRequest = new TapRequest(); // TapRequest | 
    try {
      apiInstance.messagesTap(clientId, tapRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MessagesApi#messagesTap");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **tapRequest** | [**TapRequest**](TapRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

