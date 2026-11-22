# \DatabaseAPI

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DatabaseGetContact**](DatabaseAPI.md#DatabaseGetContact) | **Get** /api/v1/clients/{clientId}/database/contacts/{userName} | 按 username（wxid / 群 id）取一条联系人。
[**DatabaseGetContactByShowName**](DatabaseAPI.md#DatabaseGetContactByShowName) | **Get** /api/v1/clients/{clientId}/database/contacts/by-show-name/first | 按显示名取第一条联系人（重名时取 UserName 升序第一条）。
[**DatabaseGetContacts**](DatabaseAPI.md#DatabaseGetContacts) | **Get** /api/v1/clients/{clientId}/database/contacts | 获取当前微信号全部联系人（不含已删除）。
[**DatabaseGetContactsByShowName**](DatabaseAPI.md#DatabaseGetContactsByShowName) | **Get** /api/v1/clients/{clientId}/database/contacts/by-show-name | 按显示名取联系人；重名时返回多个。
[**DatabaseGetMaxSortSeq**](DatabaseAPI.md#DatabaseGetMaxSortSeq) | **Get** /api/v1/clients/{clientId}/database/messages/max-sort-seq | 取当前微信号消息的最大 SortSeq（增量水位）。
[**DatabaseGetMessages**](DatabaseAPI.md#DatabaseGetMessages) | **Get** /api/v1/clients/{clientId}/database/messages | 取指定会话最近 N 条消息（从新到旧）。
[**DatabaseGetNewMessages**](DatabaseAPI.md#DatabaseGetNewMessages) | **Get** /api/v1/clients/{clientId}/database/messages/new | 取 SortSeq 大于指定值的新消息（从旧到新）。
[**DatabaseGetRecentMessages**](DatabaseAPI.md#DatabaseGetRecentMessages) | **Get** /api/v1/clients/{clientId}/database/messages/recent | 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。
[**DatabaseGetRoomByShowName**](DatabaseAPI.md#DatabaseGetRoomByShowName) | **Get** /api/v1/clients/{clientId}/database/rooms/by-show-name/first | 按群的显示名取第一条群。
[**DatabaseGetRoomMembers**](DatabaseAPI.md#DatabaseGetRoomMembers) | **Get** /api/v1/clients/{clientId}/database/rooms/{roomUsername}/members | 按群 username 取成员列表。
[**DatabaseGetRoomMembersByShowName**](DatabaseAPI.md#DatabaseGetRoomMembersByShowName) | **Get** /api/v1/clients/{clientId}/database/rooms/by-show-name/members | 按群的显示名取成员列表；重名时取第一条群。
[**DatabaseGetRooms**](DatabaseAPI.md#DatabaseGetRooms) | **Get** /api/v1/clients/{clientId}/database/rooms | 获取当前微信号全部群聊。
[**DatabaseGetRoomsByLocalType**](DatabaseAPI.md#DatabaseGetRoomsByLocalType) | **Get** /api/v1/clients/{clientId}/database/rooms/by-local-type | 按联系人 LocalType 过滤群聊（本机观察群多为 2）。
[**DatabaseGetRoomsByShowName**](DatabaseAPI.md#DatabaseGetRoomsByShowName) | **Get** /api/v1/clients/{clientId}/database/rooms/by-show-name | 按群的显示名取群；重名时返回多个。
[**DatabaseGetSnsPost**](DatabaseAPI.md#DatabaseGetSnsPost) | **Get** /api/v1/clients/{clientId}/database/sns/posts/{tid} | 按 tid 取一条朋友圈（含媒体、点赞、评论）。
[**DatabaseGetSnsPosts**](DatabaseAPI.md#DatabaseGetSnsPosts) | **Get** /api/v1/clients/{clientId}/database/sns/posts | 取最近 N 条朋友圈（含媒体、点赞、评论）。
[**DatabaseGetSnsPostsByDate**](DatabaseAPI.md#DatabaseGetSnsPostsByDate) | **Get** /api/v1/clients/{clientId}/database/sns/posts/by-date | 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。
[**DatabaseGetSnsPostsByShowName**](DatabaseAPI.md#DatabaseGetSnsPostsByShowName) | **Get** /api/v1/clients/{clientId}/database/sns/posts/by-show-name | 按发布者显示名取朋友圈；重名会一并返回。
[**DatabaseGetSnsPostsByUserName**](DatabaseAPI.md#DatabaseGetSnsPostsByUserName) | **Get** /api/v1/clients/{clientId}/database/sns/posts/by-user | 取某个发布者（真实 UserName / wxid）的朋友圈。
[**DatabaseQuery**](DatabaseAPI.md#DatabaseQuery) | **Post** /api/v1/clients/{clientId}/database/query | 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。
[**DatabaseSearchAllMessages**](DatabaseAPI.md#DatabaseSearchAllMessages) | **Get** /api/v1/clients/{clientId}/database/messages/search-all | 按正文关键字跨所有微信号搜索消息。
[**DatabaseSearchContacts**](DatabaseAPI.md#DatabaseSearchContacts) | **Get** /api/v1/clients/{clientId}/database/contacts/search | 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。
[**DatabaseSearchMessages**](DatabaseAPI.md#DatabaseSearchMessages) | **Get** /api/v1/clients/{clientId}/database/messages/search | 按正文关键字搜索当前微信号消息。



## DatabaseGetContact

> DatabaseGetContact(ctx, clientId, userName).Execute()

按 username（wxid / 群 id）取一条联系人。

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
	userName := "userName_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetContact(context.Background(), clientId, userName).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetContact``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 
**userName** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiDatabaseGetContactRequest struct via the builder pattern


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


## DatabaseGetContactByShowName

> DatabaseGetContactByShowName(ctx, clientId).ShowName(showName).Execute()

按显示名取第一条联系人（重名时取 UserName 升序第一条）。

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
	showName := "showName_example" // string |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetContactByShowName(context.Background(), clientId).ShowName(showName).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetContactByShowName``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetContactByShowNameRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **showName** | **string** |  | 

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


## DatabaseGetContacts

> DatabaseGetContacts(ctx, clientId).Execute()

获取当前微信号全部联系人（不含已删除）。

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
	r, err := apiClient.DatabaseAPI.DatabaseGetContacts(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetContacts``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetContactsRequest struct via the builder pattern


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


## DatabaseGetContactsByShowName

> DatabaseGetContactsByShowName(ctx, clientId).ShowName(showName).Execute()

按显示名取联系人；重名时返回多个。

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
	showName := "showName_example" // string |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetContactsByShowName(context.Background(), clientId).ShowName(showName).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetContactsByShowName``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetContactsByShowNameRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **showName** | **string** |  | 

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


## DatabaseGetMaxSortSeq

> DatabaseGetMaxSortSeq(ctx, clientId).Execute()

取当前微信号消息的最大 SortSeq（增量水位）。

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
	r, err := apiClient.DatabaseAPI.DatabaseGetMaxSortSeq(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetMaxSortSeq``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetMaxSortSeqRequest struct via the builder pattern


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


## DatabaseGetMessages

> DatabaseGetMessages(ctx, clientId).ChatUsername(chatUsername).Count(count).Execute()

取指定会话最近 N 条消息（从新到旧）。

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
	chatUsername := "chatUsername_example" // string |  (optional)
	count := int32(56) // int32 |  (optional) (default to 50)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetMessages(context.Background(), clientId).ChatUsername(chatUsername).Count(count).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetMessages``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetMessagesRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **chatUsername** | **string** |  | 
 **count** | **int32** |  | [default to 50]

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


## DatabaseGetNewMessages

> DatabaseGetNewMessages(ctx, clientId).AfterSortSeq(afterSortSeq).Execute()

取 SortSeq 大于指定值的新消息（从旧到新）。

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
	afterSortSeq := int64(789) // int64 |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetNewMessages(context.Background(), clientId).AfterSortSeq(afterSortSeq).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetNewMessages``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetNewMessagesRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **afterSortSeq** | **int64** |  | 

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


## DatabaseGetRecentMessages

> DatabaseGetRecentMessages(ctx, clientId).ChatUsername(chatUsername).Count(count).Execute()

取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。

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
	chatUsername := "chatUsername_example" // string |  (optional)
	count := int32(56) // int32 |  (optional) (default to 50)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetRecentMessages(context.Background(), clientId).ChatUsername(chatUsername).Count(count).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetRecentMessages``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetRecentMessagesRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **chatUsername** | **string** |  | 
 **count** | **int32** |  | [default to 50]

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


## DatabaseGetRoomByShowName

> DatabaseGetRoomByShowName(ctx, clientId).ShowName(showName).Execute()

按群的显示名取第一条群。

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
	showName := "showName_example" // string |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetRoomByShowName(context.Background(), clientId).ShowName(showName).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetRoomByShowName``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetRoomByShowNameRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **showName** | **string** |  | 

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


## DatabaseGetRoomMembers

> DatabaseGetRoomMembers(ctx, clientId, roomUsername).Execute()

按群 username 取成员列表。

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
	roomUsername := "roomUsername_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetRoomMembers(context.Background(), clientId, roomUsername).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetRoomMembers``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 
**roomUsername** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiDatabaseGetRoomMembersRequest struct via the builder pattern


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


## DatabaseGetRoomMembersByShowName

> DatabaseGetRoomMembersByShowName(ctx, clientId).ShowName(showName).Execute()

按群的显示名取成员列表；重名时取第一条群。

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
	showName := "showName_example" // string |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetRoomMembersByShowName(context.Background(), clientId).ShowName(showName).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetRoomMembersByShowName``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetRoomMembersByShowNameRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **showName** | **string** |  | 

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


## DatabaseGetRooms

> DatabaseGetRooms(ctx, clientId).Execute()

获取当前微信号全部群聊。

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
	r, err := apiClient.DatabaseAPI.DatabaseGetRooms(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetRooms``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetRoomsRequest struct via the builder pattern


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


## DatabaseGetRoomsByLocalType

> DatabaseGetRoomsByLocalType(ctx, clientId).LocalType(localType).Execute()

按联系人 LocalType 过滤群聊（本机观察群多为 2）。

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
	localType := int32(56) // int32 |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetRoomsByLocalType(context.Background(), clientId).LocalType(localType).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetRoomsByLocalType``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetRoomsByLocalTypeRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **localType** | **int32** |  | 

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


## DatabaseGetRoomsByShowName

> DatabaseGetRoomsByShowName(ctx, clientId).ShowName(showName).Execute()

按群的显示名取群；重名时返回多个。

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
	showName := "showName_example" // string |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetRoomsByShowName(context.Background(), clientId).ShowName(showName).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetRoomsByShowName``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetRoomsByShowNameRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **showName** | **string** |  | 

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


## DatabaseGetSnsPost

> DatabaseGetSnsPost(ctx, clientId, tid).Execute()

按 tid 取一条朋友圈（含媒体、点赞、评论）。

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
	tid := int64(789) // int64 | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetSnsPost(context.Background(), clientId, tid).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetSnsPost``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 
**tid** | **int64** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiDatabaseGetSnsPostRequest struct via the builder pattern


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


## DatabaseGetSnsPosts

> DatabaseGetSnsPosts(ctx, clientId).Limit(limit).Execute()

取最近 N 条朋友圈（含媒体、点赞、评论）。

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
	limit := int32(56) // int32 |  (optional) (default to 20)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetSnsPosts(context.Background(), clientId).Limit(limit).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetSnsPosts``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetSnsPostsRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **limit** | **int32** |  | [default to 20]

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


## DatabaseGetSnsPostsByDate

> DatabaseGetSnsPostsByDate(ctx, clientId).Date(date).Execute()

取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。

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
	date := time.Now() // time.Time |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetSnsPostsByDate(context.Background(), clientId).Date(date).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetSnsPostsByDate``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetSnsPostsByDateRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **date** | **time.Time** |  | 

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


## DatabaseGetSnsPostsByShowName

> DatabaseGetSnsPostsByShowName(ctx, clientId).ShowName(showName).Execute()

按发布者显示名取朋友圈；重名会一并返回。

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
	showName := "showName_example" // string |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetSnsPostsByShowName(context.Background(), clientId).ShowName(showName).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetSnsPostsByShowName``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetSnsPostsByShowNameRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **showName** | **string** |  | 

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


## DatabaseGetSnsPostsByUserName

> DatabaseGetSnsPostsByUserName(ctx, clientId).UserName(userName).Execute()

取某个发布者（真实 UserName / wxid）的朋友圈。

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
	userName := "userName_example" // string |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseGetSnsPostsByUserName(context.Background(), clientId).UserName(userName).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseGetSnsPostsByUserName``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseGetSnsPostsByUserNameRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **userName** | **string** |  | 

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


## DatabaseQuery

> DatabaseQuery(ctx, clientId).DatabaseQueryRequest(databaseQueryRequest).Execute()

执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。

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
	databaseQueryRequest := *openapiclient.NewDatabaseQueryRequest() // DatabaseQueryRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseQuery(context.Background(), clientId).DatabaseQueryRequest(databaseQueryRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseQuery``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseQueryRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **databaseQueryRequest** | [**DatabaseQueryRequest**](DatabaseQueryRequest.md) |  | 

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


## DatabaseSearchAllMessages

> DatabaseSearchAllMessages(ctx, clientId).Keyword(keyword).ChatUsername(chatUsername).Execute()

按正文关键字跨所有微信号搜索消息。

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
	keyword := "keyword_example" // string |  (optional)
	chatUsername := "chatUsername_example" // string |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseSearchAllMessages(context.Background(), clientId).Keyword(keyword).ChatUsername(chatUsername).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseSearchAllMessages``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseSearchAllMessagesRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **keyword** | **string** |  | 
 **chatUsername** | **string** |  | 

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


## DatabaseSearchContacts

> DatabaseSearchContacts(ctx, clientId).Keyword(keyword).Execute()

按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。

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
	keyword := "keyword_example" // string |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseSearchContacts(context.Background(), clientId).Keyword(keyword).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseSearchContacts``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseSearchContactsRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **keyword** | **string** |  | 

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


## DatabaseSearchMessages

> DatabaseSearchMessages(ctx, clientId).Keyword(keyword).ChatUsername(chatUsername).Execute()

按正文关键字搜索当前微信号消息。

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
	keyword := "keyword_example" // string |  (optional)
	chatUsername := "chatUsername_example" // string |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.DatabaseAPI.DatabaseSearchMessages(context.Background(), clientId).Keyword(keyword).ChatUsername(chatUsername).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `DatabaseAPI.DatabaseSearchMessages``: %v\n", err)
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

Other parameters are passed through a pointer to a apiDatabaseSearchMessagesRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **keyword** | **string** |  | 
 **chatUsername** | **string** |  | 

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

