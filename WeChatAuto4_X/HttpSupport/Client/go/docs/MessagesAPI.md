# \MessagesAPI

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**MessagesCloneVoice**](MessagesAPI.md#MessagesCloneVoice) | **Post** /api/v1/clients/{clientId}/messages/clone-voice | 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。
[**MessagesFocusInput**](MessagesAPI.md#MessagesFocusInput) | **Post** /api/v1/clients/{clientId}/messages/focus-input | 聚焦当前窗口的消息输入区。
[**MessagesForwardMultiple**](MessagesAPI.md#MessagesForwardMultiple) | **Post** /api/v1/clients/{clientId}/messages/forward-multiple | 转发多条消息（默认合并转发）。
[**MessagesForwardSingle**](MessagesAPI.md#MessagesForwardSingle) | **Post** /api/v1/clients/{clientId}/messages/forward-single | 转发单条消息（message 与 chat_simple_message 二选一）。
[**MessagesGetHistory**](MessagesAPI.md#MessagesGetHistory) | **Get** /api/v1/clients/{clientId}/messages/history | 获取聊天历史（支持按日期或时间段查询）。
[**MessagesGetOnlyTitle**](MessagesAPI.md#MessagesGetOnlyTitle) | **Get** /api/v1/clients/{clientId}/messages/only-title | 获取当前聊天窗口的标题名称。
[**MessagesGetTitle**](MessagesAPI.md#MessagesGetTitle) | **Get** /api/v1/clients/{clientId}/messages/title | 获取当前聊天窗口的标题对象。
[**MessagesPull**](MessagesAPI.md#MessagesPull) | **Post** /api/v1/clients/{clientId}/messages/pull | 轮询拉取指定会话的新消息（用于自己实现轮询）。
[**MessagesReference**](MessagesAPI.md#MessagesReference) | **Post** /api/v1/clients/{clientId}/messages/reference | 引用当前窗口的好友消息。
[**MessagesReferenceLast**](MessagesAPI.md#MessagesReferenceLast) | **Post** /api/v1/clients/{clientId}/messages/reference-last | 引用当前窗口最后一条消息。
[**MessagesSend**](MessagesAPI.md#MessagesSend) | **Post** /api/v1/clients/{clientId}/messages/send | 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。
[**MessagesSendAndClose**](MessagesAPI.md#MessagesSendAndClose) | **Post** /api/v1/clients/{clientId}/messages/send-and-close | 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。
[**MessagesSendByWxid**](MessagesAPI.md#MessagesSendByWxid) | **Post** /api/v1/clients/{clientId}/messages/send-by-wxid | 按联系人 UserName（wxid / 群 id）发送文本。
[**MessagesSendEmoji**](MessagesAPI.md#MessagesSendEmoji) | **Post** /api/v1/clients/{clientId}/messages/send-emoji | 发送表情（按名称/描述/索引）。
[**MessagesSendEmojiAndClose**](MessagesAPI.md#MessagesSendEmojiAndClose) | **Post** /api/v1/clients/{clientId}/messages/send-emoji-and-close | 发送表情后关闭微信窗口（按名称/描述/索引）。
[**MessagesSendEmojiByWxid**](MessagesAPI.md#MessagesSendEmojiByWxid) | **Post** /api/v1/clients/{clientId}/messages/send-emoji-by-wxid | 按联系人 UserName（wxid / 群 id）发送表情。
[**MessagesSendFile**](MessagesAPI.md#MessagesSendFile) | **Post** /api/v1/clients/{clientId}/messages/send-file | 发送文件。
[**MessagesSendFileAndClose**](MessagesAPI.md#MessagesSendFileAndClose) | **Post** /api/v1/clients/{clientId}/messages/send-file-and-close | 发送文件后关闭微信窗口。
[**MessagesSendFileByWxid**](MessagesAPI.md#MessagesSendFileByWxid) | **Post** /api/v1/clients/{clientId}/messages/send-file-by-wxid | 按联系人 UserName（wxid / 群 id）发送文件。
[**MessagesSendVideoChat**](MessagesAPI.md#MessagesSendVideoChat) | **Post** /api/v1/clients/{clientId}/messages/send-video-chat | 发起单人视频聊天。
[**MessagesSendVoiceChat**](MessagesAPI.md#MessagesSendVoiceChat) | **Post** /api/v1/clients/{clientId}/messages/send-voice-chat | 发起单人语音聊天。
[**MessagesSendVoiceChats**](MessagesAPI.md#MessagesSendVoiceChats) | **Post** /api/v1/clients/{clientId}/messages/send-voice-chats | 发起多人语音聊天（群聊）。
[**MessagesSendVoiceMessage**](MessagesAPI.md#MessagesSendVoiceMessage) | **Post** /api/v1/clients/{clientId}/messages/send-voice-message | 发送语音消息（需虚拟声卡 Cable input/Cable output）。
[**MessagesSendVoiceMessageAndClose**](MessagesAPI.md#MessagesSendVoiceMessageAndClose) | **Post** /api/v1/clients/{clientId}/messages/send-voice-message-and-close | 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。
[**MessagesSendVoiceMessageByWxid**](MessagesAPI.md#MessagesSendVoiceMessageByWxid) | **Post** /api/v1/clients/{clientId}/messages/send-voice-message-by-wxid | 按联系人 UserName（wxid / 群 id）发送语音文件。
[**MessagesSendVoiceMessageTts**](MessagesAPI.md#MessagesSendVoiceMessageTts) | **Post** /api/v1/clients/{clientId}/messages/send-voice-message-tts | 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。
[**MessagesSendVoiceMessageTtsAndClose**](MessagesAPI.md#MessagesSendVoiceMessageTtsAndClose) | **Post** /api/v1/clients/{clientId}/messages/send-voice-message-tts-and-close | 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。
[**MessagesSendVoiceMessageTtsByWxid**](MessagesAPI.md#MessagesSendVoiceMessageTtsByWxid) | **Post** /api/v1/clients/{clientId}/messages/send-voice-message-tts-by-wxid | 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。
[**MessagesTap**](MessagesAPI.md#MessagesTap) | **Post** /api/v1/clients/{clientId}/messages/tap | 拍一拍当前聊天窗口的好友。



## MessagesCloneVoice

> MessagesCloneVoice(ctx, clientId).CloneVoiceRequest(cloneVoiceRequest).Execute()

声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	cloneVoiceRequest := *openapiclient.NewCloneVoiceRequest() // CloneVoiceRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesCloneVoice(context.Background(), clientId).CloneVoiceRequest(cloneVoiceRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesCloneVoice``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesCloneVoiceRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **cloneVoiceRequest** | [**CloneVoiceRequest**](CloneVoiceRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesFocusInput

> MessagesFocusInput(ctx, clientId).Execute()

聚焦当前窗口的消息输入区。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesFocusInput(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesFocusInput``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesFocusInputRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------


### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesForwardMultiple

> MessagesForwardMultiple(ctx, clientId).ForwardMultipleMessageRequest(forwardMultipleMessageRequest).Execute()

转发多条消息（默认合并转发）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	forwardMultipleMessageRequest := *openapiclient.NewForwardMultipleMessageRequest() // ForwardMultipleMessageRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesForwardMultiple(context.Background(), clientId).ForwardMultipleMessageRequest(forwardMultipleMessageRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesForwardMultiple``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesForwardMultipleRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **forwardMultipleMessageRequest** | [**ForwardMultipleMessageRequest**](ForwardMultipleMessageRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesForwardSingle

> MessagesForwardSingle(ctx, clientId).ForwardSingleMessageRequest(forwardSingleMessageRequest).Execute()

转发单条消息（message 与 chat_simple_message 二选一）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	forwardSingleMessageRequest := *openapiclient.NewForwardSingleMessageRequest() // ForwardSingleMessageRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesForwardSingle(context.Background(), clientId).ForwardSingleMessageRequest(forwardSingleMessageRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesForwardSingle``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesForwardSingleRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **forwardSingleMessageRequest** | [**ForwardSingleMessageRequest**](ForwardSingleMessageRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesGetHistory

> MessagesGetHistory(ctx, clientId).Who(who).Date(date).StartDate(startDate).EndDate(endDate).Execute()

获取聊天历史（支持按日期或时间段查询）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
    "time"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	who := "who_example" // string |  (optional)
	date := time.Now() // time.Time |  (optional)
	startDate := time.Now() // time.Time |  (optional)
	endDate := time.Now() // time.Time |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesGetHistory(context.Background(), clientId).Who(who).Date(date).StartDate(startDate).EndDate(endDate).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesGetHistory``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesGetHistoryRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **who** | **string** |  | 
 **date** | **time.Time** |  | 
 **startDate** | **time.Time** |  | 
 **endDate** | **time.Time** |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesGetOnlyTitle

> MessagesGetOnlyTitle(ctx, clientId).Execute()

获取当前聊天窗口的标题名称。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesGetOnlyTitle(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesGetOnlyTitle``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesGetOnlyTitleRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------


### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesGetTitle

> MessagesGetTitle(ctx, clientId).Execute()

获取当前聊天窗口的标题对象。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesGetTitle(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesGetTitle``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesGetTitleRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------


### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesPull

> MessagesPull(ctx, clientId).PullMessagesRequest(pullMessagesRequest).Execute()

轮询拉取指定会话的新消息（用于自己实现轮询）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	pullMessagesRequest := *openapiclient.NewPullMessagesRequest() // PullMessagesRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesPull(context.Background(), clientId).PullMessagesRequest(pullMessagesRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesPull``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesPullRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **pullMessagesRequest** | [**PullMessagesRequest**](PullMessagesRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesReference

> MessagesReference(ctx, clientId).ReferenceRequest(referenceRequest).Execute()

引用当前窗口的好友消息。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	referenceRequest := *openapiclient.NewReferenceRequest() // ReferenceRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesReference(context.Background(), clientId).ReferenceRequest(referenceRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesReference``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesReferenceRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **referenceRequest** | [**ReferenceRequest**](ReferenceRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesReferenceLast

> MessagesReferenceLast(ctx, clientId).Execute()

引用当前窗口最后一条消息。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesReferenceLast(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesReferenceLast``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesReferenceLastRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------


### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSend

> MessagesSend(ctx, clientId).SendMessageRequest(sendMessageRequest).Execute()

发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendMessageRequest := *openapiclient.NewSendMessageRequest() // SendMessageRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSend(context.Background(), clientId).SendMessageRequest(sendMessageRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSend``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendMessageRequest** | [**SendMessageRequest**](SendMessageRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendAndClose

> MessagesSendAndClose(ctx, clientId).SendMessageRequest(sendMessageRequest).Execute()

发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendMessageRequest := *openapiclient.NewSendMessageRequest() // SendMessageRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendAndClose(context.Background(), clientId).SendMessageRequest(sendMessageRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendAndClose``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendAndCloseRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendMessageRequest** | [**SendMessageRequest**](SendMessageRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendByWxid

> MessagesSendByWxid(ctx, clientId).SendMessageByWxidRequest(sendMessageByWxidRequest).Execute()

按联系人 UserName（wxid / 群 id）发送文本。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendMessageByWxidRequest := *openapiclient.NewSendMessageByWxidRequest() // SendMessageByWxidRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendByWxid(context.Background(), clientId).SendMessageByWxidRequest(sendMessageByWxidRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendByWxid``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendByWxidRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendMessageByWxidRequest** | [**SendMessageByWxidRequest**](SendMessageByWxidRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendEmoji

> MessagesSendEmoji(ctx, clientId).SendEmojiRequest(sendEmojiRequest).Execute()

发送表情（按名称/描述/索引）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendEmojiRequest := *openapiclient.NewSendEmojiRequest() // SendEmojiRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendEmoji(context.Background(), clientId).SendEmojiRequest(sendEmojiRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendEmoji``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendEmojiRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendEmojiRequest** | [**SendEmojiRequest**](SendEmojiRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendEmojiAndClose

> MessagesSendEmojiAndClose(ctx, clientId).SendEmojiRequest(sendEmojiRequest).Execute()

发送表情后关闭微信窗口（按名称/描述/索引）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendEmojiRequest := *openapiclient.NewSendEmojiRequest() // SendEmojiRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendEmojiAndClose(context.Background(), clientId).SendEmojiRequest(sendEmojiRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendEmojiAndClose``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendEmojiAndCloseRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendEmojiRequest** | [**SendEmojiRequest**](SendEmojiRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendEmojiByWxid

> MessagesSendEmojiByWxid(ctx, clientId).SendEmojiByWxidRequest(sendEmojiByWxidRequest).Execute()

按联系人 UserName（wxid / 群 id）发送表情。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendEmojiByWxidRequest := *openapiclient.NewSendEmojiByWxidRequest() // SendEmojiByWxidRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendEmojiByWxid(context.Background(), clientId).SendEmojiByWxidRequest(sendEmojiByWxidRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendEmojiByWxid``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendEmojiByWxidRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendEmojiByWxidRequest** | [**SendEmojiByWxidRequest**](SendEmojiByWxidRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendFile

> MessagesSendFile(ctx, clientId).SendFileRequest(sendFileRequest).Execute()

发送文件。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendFileRequest := *openapiclient.NewSendFileRequest() // SendFileRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendFile(context.Background(), clientId).SendFileRequest(sendFileRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendFile``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendFileRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendFileRequest** | [**SendFileRequest**](SendFileRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendFileAndClose

> MessagesSendFileAndClose(ctx, clientId).SendFileRequest(sendFileRequest).Execute()

发送文件后关闭微信窗口。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendFileRequest := *openapiclient.NewSendFileRequest() // SendFileRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendFileAndClose(context.Background(), clientId).SendFileRequest(sendFileRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendFileAndClose``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendFileAndCloseRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendFileRequest** | [**SendFileRequest**](SendFileRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendFileByWxid

> MessagesSendFileByWxid(ctx, clientId).SendFileByWxidRequest(sendFileByWxidRequest).Execute()

按联系人 UserName（wxid / 群 id）发送文件。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendFileByWxidRequest := *openapiclient.NewSendFileByWxidRequest() // SendFileByWxidRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendFileByWxid(context.Background(), clientId).SendFileByWxidRequest(sendFileByWxidRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendFileByWxid``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendFileByWxidRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendFileByWxidRequest** | [**SendFileByWxidRequest**](SendFileByWxidRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendVideoChat

> MessagesSendVideoChat(ctx, clientId).WhoRequest(whoRequest).Execute()

发起单人视频聊天。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	whoRequest := *openapiclient.NewWhoRequest() // WhoRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendVideoChat(context.Background(), clientId).WhoRequest(whoRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendVideoChat``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendVideoChatRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **whoRequest** | [**WhoRequest**](WhoRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendVoiceChat

> MessagesSendVoiceChat(ctx, clientId).WhoRequest(whoRequest).Execute()

发起单人语音聊天。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	whoRequest := *openapiclient.NewWhoRequest() // WhoRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendVoiceChat(context.Background(), clientId).WhoRequest(whoRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendVoiceChat``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendVoiceChatRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **whoRequest** | [**WhoRequest**](WhoRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendVoiceChats

> MessagesSendVoiceChats(ctx, clientId).SendVoiceChatsRequest(sendVoiceChatsRequest).Execute()

发起多人语音聊天（群聊）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendVoiceChatsRequest := *openapiclient.NewSendVoiceChatsRequest() // SendVoiceChatsRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendVoiceChats(context.Background(), clientId).SendVoiceChatsRequest(sendVoiceChatsRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendVoiceChats``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendVoiceChatsRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendVoiceChatsRequest** | [**SendVoiceChatsRequest**](SendVoiceChatsRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendVoiceMessage

> MessagesSendVoiceMessage(ctx, clientId).SendVoiceMessageRequest(sendVoiceMessageRequest).Execute()

发送语音消息（需虚拟声卡 Cable input/Cable output）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendVoiceMessageRequest := *openapiclient.NewSendVoiceMessageRequest() // SendVoiceMessageRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendVoiceMessage(context.Background(), clientId).SendVoiceMessageRequest(sendVoiceMessageRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendVoiceMessage``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendVoiceMessageRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendVoiceMessageRequest** | [**SendVoiceMessageRequest**](SendVoiceMessageRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendVoiceMessageAndClose

> MessagesSendVoiceMessageAndClose(ctx, clientId).SendVoiceMessageRequest(sendVoiceMessageRequest).Execute()

发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendVoiceMessageRequest := *openapiclient.NewSendVoiceMessageRequest() // SendVoiceMessageRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendVoiceMessageAndClose(context.Background(), clientId).SendVoiceMessageRequest(sendVoiceMessageRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendVoiceMessageAndClose``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendVoiceMessageAndCloseRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendVoiceMessageRequest** | [**SendVoiceMessageRequest**](SendVoiceMessageRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendVoiceMessageByWxid

> MessagesSendVoiceMessageByWxid(ctx, clientId).SendVoiceMessageByWxidRequest(sendVoiceMessageByWxidRequest).Execute()

按联系人 UserName（wxid / 群 id）发送语音文件。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendVoiceMessageByWxidRequest := *openapiclient.NewSendVoiceMessageByWxidRequest() // SendVoiceMessageByWxidRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendVoiceMessageByWxid(context.Background(), clientId).SendVoiceMessageByWxidRequest(sendVoiceMessageByWxidRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendVoiceMessageByWxid``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendVoiceMessageByWxidRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendVoiceMessageByWxidRequest** | [**SendVoiceMessageByWxidRequest**](SendVoiceMessageByWxidRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendVoiceMessageTts

> MessagesSendVoiceMessageTts(ctx, clientId).SendVoiceMessageTtsRequest(sendVoiceMessageTtsRequest).Execute()

文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendVoiceMessageTtsRequest := *openapiclient.NewSendVoiceMessageTtsRequest() // SendVoiceMessageTtsRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendVoiceMessageTts(context.Background(), clientId).SendVoiceMessageTtsRequest(sendVoiceMessageTtsRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendVoiceMessageTts``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendVoiceMessageTtsRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendVoiceMessageTtsRequest** | [**SendVoiceMessageTtsRequest**](SendVoiceMessageTtsRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendVoiceMessageTtsAndClose

> MessagesSendVoiceMessageTtsAndClose(ctx, clientId).SendVoiceMessageTtsRequest(sendVoiceMessageTtsRequest).Execute()

文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendVoiceMessageTtsRequest := *openapiclient.NewSendVoiceMessageTtsRequest() // SendVoiceMessageTtsRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendVoiceMessageTtsAndClose(context.Background(), clientId).SendVoiceMessageTtsRequest(sendVoiceMessageTtsRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendVoiceMessageTtsAndClose``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendVoiceMessageTtsAndCloseRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendVoiceMessageTtsRequest** | [**SendVoiceMessageTtsRequest**](SendVoiceMessageTtsRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesSendVoiceMessageTtsByWxid

> MessagesSendVoiceMessageTtsByWxid(ctx, clientId).SendVoiceMessageTtsByWxidRequest(sendVoiceMessageTtsByWxidRequest).Execute()

按联系人 UserName（wxid / 群 id）用 TTS 发送语音。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	sendVoiceMessageTtsByWxidRequest := *openapiclient.NewSendVoiceMessageTtsByWxidRequest() // SendVoiceMessageTtsByWxidRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesSendVoiceMessageTtsByWxid(context.Background(), clientId).SendVoiceMessageTtsByWxidRequest(sendVoiceMessageTtsByWxidRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesSendVoiceMessageTtsByWxid``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesSendVoiceMessageTtsByWxidRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **sendVoiceMessageTtsByWxidRequest** | [**SendVoiceMessageTtsByWxidRequest**](SendVoiceMessageTtsByWxidRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## MessagesTap

> MessagesTap(ctx, clientId).TapRequest(tapRequest).Execute()

拍一拍当前聊天窗口的好友。

### Example

```go
package main

import (
	"context"
	"fmt"
	"os"
	openapiclient "github.com/GIT_USER_ID/GIT_REPO_ID"
)

func main() {
	clientId := "clientId_example" // string | 
	tapRequest := *openapiclient.NewTapRequest() // TapRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.MessagesAPI.MessagesTap(context.Background(), clientId).TapRequest(tapRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `MessagesAPI.MessagesTap``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiMessagesTapRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **tapRequest** | [**TapRequest**](TapRequest.md) |  | 

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

