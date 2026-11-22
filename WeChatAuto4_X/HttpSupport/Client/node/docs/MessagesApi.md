# wechatauto-client.MessagesApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**messagesCloneVoice**](MessagesApi.md#messagesCloneVoice) | **POST** /api/v1/clients/{clientId}/messages/clone-voice | 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。
[**messagesFocusInput**](MessagesApi.md#messagesFocusInput) | **POST** /api/v1/clients/{clientId}/messages/focus-input | 聚焦当前窗口的消息输入区。
[**messagesForwardMultiple**](MessagesApi.md#messagesForwardMultiple) | **POST** /api/v1/clients/{clientId}/messages/forward-multiple | 转发多条消息（默认合并转发）。
[**messagesForwardSingle**](MessagesApi.md#messagesForwardSingle) | **POST** /api/v1/clients/{clientId}/messages/forward-single | 转发单条消息（message 与 chat_simple_message 二选一）。
[**messagesGetHistory**](MessagesApi.md#messagesGetHistory) | **GET** /api/v1/clients/{clientId}/messages/history | 获取聊天历史（支持按日期或时间段查询）。
[**messagesGetOnlyTitle**](MessagesApi.md#messagesGetOnlyTitle) | **GET** /api/v1/clients/{clientId}/messages/only-title | 获取当前聊天窗口的标题名称。
[**messagesGetTitle**](MessagesApi.md#messagesGetTitle) | **GET** /api/v1/clients/{clientId}/messages/title | 获取当前聊天窗口的标题对象。
[**messagesPull**](MessagesApi.md#messagesPull) | **POST** /api/v1/clients/{clientId}/messages/pull | 轮询拉取指定会话的新消息（用于自己实现轮询）。
[**messagesReference**](MessagesApi.md#messagesReference) | **POST** /api/v1/clients/{clientId}/messages/reference | 引用当前窗口的好友消息。
[**messagesReferenceLast**](MessagesApi.md#messagesReferenceLast) | **POST** /api/v1/clients/{clientId}/messages/reference-last | 引用当前窗口最后一条消息。
[**messagesSend**](MessagesApi.md#messagesSend) | **POST** /api/v1/clients/{clientId}/messages/send | 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。
[**messagesSendAndClose**](MessagesApi.md#messagesSendAndClose) | **POST** /api/v1/clients/{clientId}/messages/send-and-close | 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。
[**messagesSendByWxid**](MessagesApi.md#messagesSendByWxid) | **POST** /api/v1/clients/{clientId}/messages/send-by-wxid | 按联系人 UserName（wxid / 群 id）发送文本。
[**messagesSendEmoji**](MessagesApi.md#messagesSendEmoji) | **POST** /api/v1/clients/{clientId}/messages/send-emoji | 发送表情（按名称/描述/索引）。
[**messagesSendEmojiAndClose**](MessagesApi.md#messagesSendEmojiAndClose) | **POST** /api/v1/clients/{clientId}/messages/send-emoji-and-close | 发送表情后关闭微信窗口（按名称/描述/索引）。
[**messagesSendEmojiByWxid**](MessagesApi.md#messagesSendEmojiByWxid) | **POST** /api/v1/clients/{clientId}/messages/send-emoji-by-wxid | 按联系人 UserName（wxid / 群 id）发送表情。
[**messagesSendFile**](MessagesApi.md#messagesSendFile) | **POST** /api/v1/clients/{clientId}/messages/send-file | 发送文件。
[**messagesSendFileAndClose**](MessagesApi.md#messagesSendFileAndClose) | **POST** /api/v1/clients/{clientId}/messages/send-file-and-close | 发送文件后关闭微信窗口。
[**messagesSendFileByWxid**](MessagesApi.md#messagesSendFileByWxid) | **POST** /api/v1/clients/{clientId}/messages/send-file-by-wxid | 按联系人 UserName（wxid / 群 id）发送文件。
[**messagesSendVideoChat**](MessagesApi.md#messagesSendVideoChat) | **POST** /api/v1/clients/{clientId}/messages/send-video-chat | 发起单人视频聊天。
[**messagesSendVoiceChat**](MessagesApi.md#messagesSendVoiceChat) | **POST** /api/v1/clients/{clientId}/messages/send-voice-chat | 发起单人语音聊天。
[**messagesSendVoiceChats**](MessagesApi.md#messagesSendVoiceChats) | **POST** /api/v1/clients/{clientId}/messages/send-voice-chats | 发起多人语音聊天（群聊）。
[**messagesSendVoiceMessage**](MessagesApi.md#messagesSendVoiceMessage) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message | 发送语音消息（需虚拟声卡 Cable input/Cable output）。
[**messagesSendVoiceMessageAndClose**](MessagesApi.md#messagesSendVoiceMessageAndClose) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-and-close | 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。
[**messagesSendVoiceMessageByWxid**](MessagesApi.md#messagesSendVoiceMessageByWxid) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-by-wxid | 按联系人 UserName（wxid / 群 id）发送语音文件。
[**messagesSendVoiceMessageTts**](MessagesApi.md#messagesSendVoiceMessageTts) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts | 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。
[**messagesSendVoiceMessageTtsAndClose**](MessagesApi.md#messagesSendVoiceMessageTtsAndClose) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts-and-close | 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。
[**messagesSendVoiceMessageTtsByWxid**](MessagesApi.md#messagesSendVoiceMessageTtsByWxid) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts-by-wxid | 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。
[**messagesTap**](MessagesApi.md#messagesTap) | **POST** /api/v1/clients/{clientId}/messages/tap | 拍一拍当前聊天窗口的好友。



## messagesCloneVoice

> messagesCloneVoice(clientId, opts)

声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'cloneVoiceRequest': new wechatauto-client.CloneVoiceRequest() // CloneVoiceRequest | 
};
apiInstance.messagesCloneVoice(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **cloneVoiceRequest** | [**CloneVoiceRequest**](CloneVoiceRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesFocusInput

> messagesFocusInput(clientId)

聚焦当前窗口的消息输入区。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
apiInstance.messagesFocusInput(clientId, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## messagesForwardMultiple

> messagesForwardMultiple(clientId, opts)

转发多条消息（默认合并转发）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'forwardMultipleMessageRequest': new wechatauto-client.ForwardMultipleMessageRequest() // ForwardMultipleMessageRequest | 
};
apiInstance.messagesForwardMultiple(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **forwardMultipleMessageRequest** | [**ForwardMultipleMessageRequest**](ForwardMultipleMessageRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesForwardSingle

> messagesForwardSingle(clientId, opts)

转发单条消息（message 与 chat_simple_message 二选一）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'forwardSingleMessageRequest': new wechatauto-client.ForwardSingleMessageRequest() // ForwardSingleMessageRequest | 
};
apiInstance.messagesForwardSingle(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **forwardSingleMessageRequest** | [**ForwardSingleMessageRequest**](ForwardSingleMessageRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesGetHistory

> messagesGetHistory(clientId, opts)

获取聊天历史（支持按日期或时间段查询）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'who': "who_example", // String | 
  'date': new Date("2013-10-20T19:20:30+01:00"), // Date | 
  'startDate': new Date("2013-10-20T19:20:30+01:00"), // Date | 
  'endDate': new Date("2013-10-20T19:20:30+01:00") // Date | 
};
apiInstance.messagesGetHistory(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **who** | **String**|  | [optional] 
 **date** | **Date**|  | [optional] 
 **startDate** | **Date**|  | [optional] 
 **endDate** | **Date**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## messagesGetOnlyTitle

> messagesGetOnlyTitle(clientId)

获取当前聊天窗口的标题名称。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
apiInstance.messagesGetOnlyTitle(clientId, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## messagesGetTitle

> messagesGetTitle(clientId)

获取当前聊天窗口的标题对象。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
apiInstance.messagesGetTitle(clientId, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## messagesPull

> messagesPull(clientId, opts)

轮询拉取指定会话的新消息（用于自己实现轮询）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'pullMessagesRequest': new wechatauto-client.PullMessagesRequest() // PullMessagesRequest | 
};
apiInstance.messagesPull(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **pullMessagesRequest** | [**PullMessagesRequest**](PullMessagesRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesReference

> messagesReference(clientId, opts)

引用当前窗口的好友消息。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'referenceRequest': new wechatauto-client.ReferenceRequest() // ReferenceRequest | 
};
apiInstance.messagesReference(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **referenceRequest** | [**ReferenceRequest**](ReferenceRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesReferenceLast

> messagesReferenceLast(clientId)

引用当前窗口最后一条消息。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
apiInstance.messagesReferenceLast(clientId, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## messagesSend

> messagesSend(clientId, opts)

发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendMessageRequest': new wechatauto-client.SendMessageRequest() // SendMessageRequest | 
};
apiInstance.messagesSend(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendMessageRequest** | [**SendMessageRequest**](SendMessageRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendAndClose

> messagesSendAndClose(clientId, opts)

发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendMessageRequest': new wechatauto-client.SendMessageRequest() // SendMessageRequest | 
};
apiInstance.messagesSendAndClose(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendMessageRequest** | [**SendMessageRequest**](SendMessageRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendByWxid

> messagesSendByWxid(clientId, opts)

按联系人 UserName（wxid / 群 id）发送文本。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendMessageByWxidRequest': new wechatauto-client.SendMessageByWxidRequest() // SendMessageByWxidRequest | 
};
apiInstance.messagesSendByWxid(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendMessageByWxidRequest** | [**SendMessageByWxidRequest**](SendMessageByWxidRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendEmoji

> messagesSendEmoji(clientId, opts)

发送表情（按名称/描述/索引）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendEmojiRequest': new wechatauto-client.SendEmojiRequest() // SendEmojiRequest | 
};
apiInstance.messagesSendEmoji(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendEmojiRequest** | [**SendEmojiRequest**](SendEmojiRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendEmojiAndClose

> messagesSendEmojiAndClose(clientId, opts)

发送表情后关闭微信窗口（按名称/描述/索引）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendEmojiRequest': new wechatauto-client.SendEmojiRequest() // SendEmojiRequest | 
};
apiInstance.messagesSendEmojiAndClose(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendEmojiRequest** | [**SendEmojiRequest**](SendEmojiRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendEmojiByWxid

> messagesSendEmojiByWxid(clientId, opts)

按联系人 UserName（wxid / 群 id）发送表情。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendEmojiByWxidRequest': new wechatauto-client.SendEmojiByWxidRequest() // SendEmojiByWxidRequest | 
};
apiInstance.messagesSendEmojiByWxid(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendEmojiByWxidRequest** | [**SendEmojiByWxidRequest**](SendEmojiByWxidRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendFile

> messagesSendFile(clientId, opts)

发送文件。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendFileRequest': new wechatauto-client.SendFileRequest() // SendFileRequest | 
};
apiInstance.messagesSendFile(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendFileRequest** | [**SendFileRequest**](SendFileRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendFileAndClose

> messagesSendFileAndClose(clientId, opts)

发送文件后关闭微信窗口。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendFileRequest': new wechatauto-client.SendFileRequest() // SendFileRequest | 
};
apiInstance.messagesSendFileAndClose(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendFileRequest** | [**SendFileRequest**](SendFileRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendFileByWxid

> messagesSendFileByWxid(clientId, opts)

按联系人 UserName（wxid / 群 id）发送文件。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendFileByWxidRequest': new wechatauto-client.SendFileByWxidRequest() // SendFileByWxidRequest | 
};
apiInstance.messagesSendFileByWxid(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendFileByWxidRequest** | [**SendFileByWxidRequest**](SendFileByWxidRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendVideoChat

> messagesSendVideoChat(clientId, opts)

发起单人视频聊天。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'whoRequest': new wechatauto-client.WhoRequest() // WhoRequest | 
};
apiInstance.messagesSendVideoChat(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **whoRequest** | [**WhoRequest**](WhoRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendVoiceChat

> messagesSendVoiceChat(clientId, opts)

发起单人语音聊天。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'whoRequest': new wechatauto-client.WhoRequest() // WhoRequest | 
};
apiInstance.messagesSendVoiceChat(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **whoRequest** | [**WhoRequest**](WhoRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendVoiceChats

> messagesSendVoiceChats(clientId, opts)

发起多人语音聊天（群聊）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendVoiceChatsRequest': new wechatauto-client.SendVoiceChatsRequest() // SendVoiceChatsRequest | 
};
apiInstance.messagesSendVoiceChats(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendVoiceChatsRequest** | [**SendVoiceChatsRequest**](SendVoiceChatsRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendVoiceMessage

> messagesSendVoiceMessage(clientId, opts)

发送语音消息（需虚拟声卡 Cable input/Cable output）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendVoiceMessageRequest': new wechatauto-client.SendVoiceMessageRequest() // SendVoiceMessageRequest | 
};
apiInstance.messagesSendVoiceMessage(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendVoiceMessageRequest** | [**SendVoiceMessageRequest**](SendVoiceMessageRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendVoiceMessageAndClose

> messagesSendVoiceMessageAndClose(clientId, opts)

发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendVoiceMessageRequest': new wechatauto-client.SendVoiceMessageRequest() // SendVoiceMessageRequest | 
};
apiInstance.messagesSendVoiceMessageAndClose(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendVoiceMessageRequest** | [**SendVoiceMessageRequest**](SendVoiceMessageRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendVoiceMessageByWxid

> messagesSendVoiceMessageByWxid(clientId, opts)

按联系人 UserName（wxid / 群 id）发送语音文件。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendVoiceMessageByWxidRequest': new wechatauto-client.SendVoiceMessageByWxidRequest() // SendVoiceMessageByWxidRequest | 
};
apiInstance.messagesSendVoiceMessageByWxid(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendVoiceMessageByWxidRequest** | [**SendVoiceMessageByWxidRequest**](SendVoiceMessageByWxidRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendVoiceMessageTts

> messagesSendVoiceMessageTts(clientId, opts)

文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendVoiceMessageTtsRequest': new wechatauto-client.SendVoiceMessageTtsRequest() // SendVoiceMessageTtsRequest | 
};
apiInstance.messagesSendVoiceMessageTts(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendVoiceMessageTtsRequest** | [**SendVoiceMessageTtsRequest**](SendVoiceMessageTtsRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendVoiceMessageTtsAndClose

> messagesSendVoiceMessageTtsAndClose(clientId, opts)

文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendVoiceMessageTtsRequest': new wechatauto-client.SendVoiceMessageTtsRequest() // SendVoiceMessageTtsRequest | 
};
apiInstance.messagesSendVoiceMessageTtsAndClose(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendVoiceMessageTtsRequest** | [**SendVoiceMessageTtsRequest**](SendVoiceMessageTtsRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesSendVoiceMessageTtsByWxid

> messagesSendVoiceMessageTtsByWxid(clientId, opts)

按联系人 UserName（wxid / 群 id）用 TTS 发送语音。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'sendVoiceMessageTtsByWxidRequest': new wechatauto-client.SendVoiceMessageTtsByWxidRequest() // SendVoiceMessageTtsByWxidRequest | 
};
apiInstance.messagesSendVoiceMessageTtsByWxid(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **sendVoiceMessageTtsByWxidRequest** | [**SendVoiceMessageTtsByWxidRequest**](SendVoiceMessageTtsByWxidRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## messagesTap

> messagesTap(clientId, opts)

拍一拍当前聊天窗口的好友。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MessagesApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'tapRequest': new wechatauto-client.TapRequest() // TapRequest | 
};
apiInstance.messagesTap(clientId, opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **String**|  | 
 **tapRequest** | [**TapRequest**](TapRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

