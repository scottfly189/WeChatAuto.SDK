# \FriendsAPI

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**FriendsAdd**](FriendsAPI.md#FriendsAdd) | **Post** /api/v1/clients/{clientId}/friends/add | 通过手机号码/微信号查找并添加好友。
[**FriendsAddOrUpdateFriend**](FriendsAPI.md#FriendsAddOrUpdateFriend) | **Post** /api/v1/clients/{clientId}/friends/cache | 手动新增或修改缓存中的好友对象。
[**FriendsCloseAddWindow**](FriendsAPI.md#FriendsCloseAddWindow) | **Post** /api/v1/clients/{clientId}/friends/add-window/close | 关闭新增朋友窗口。
[**FriendsGetAllFriendNames**](FriendsAPI.md#FriendsGetAllFriendNames) | **Get** /api/v1/clients/{clientId}/friends/names | 获取所有好友名称列表（有备注优先备注名）。
[**FriendsGetAllFriends**](FriendsAPI.md#FriendsGetAllFriends) | **Get** /api/v1/clients/{clientId}/friends | 获取所有好友的信息列表（默认优先从缓存读取）。
[**FriendsGetFriendByWxidFromCache**](FriendsAPI.md#FriendsGetFriendByWxidFromCache) | **Get** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存获取好友信息。
[**FriendsGetFriendFromCache**](FriendsAPI.md#FriendsGetFriendFromCache) | **Get** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存获取单个好友信息。
[**FriendsGetFriendListFromCache**](FriendsAPI.md#FriendsGetFriendListFromCache) | **Get** /api/v1/clients/{clientId}/friends/cache | 获取缓存中的全部好友信息列表。
[**FriendsGetFriendsFromCache**](FriendsAPI.md#FriendsGetFriendsFromCache) | **Get** /api/v1/clients/{clientId}/friends/cache/by-name/{who} | 按名称从缓存获取好友列表（适用于昵称重复场景）。
[**FriendsOpenAddWindow**](FriendsAPI.md#FriendsOpenAddWindow) | **Post** /api/v1/clients/{clientId}/friends/add-window/open | 打开新增朋友窗口。
[**FriendsPassAll**](FriendsAPI.md#FriendsPassAll) | **Post** /api/v1/clients/{clientId}/friends/pass-all | 立即批量通过当前待处理的新好友申请（不启动持续监听）。
[**FriendsPauseAutoAccept**](FriendsAPI.md#FriendsPauseAutoAccept) | **Post** /api/v1/clients/{clientId}/friends/auto-accept/pause | 暂停新好友自动通过监听。
[**FriendsRemoveFriend**](FriendsAPI.md#FriendsRemoveFriend) | **Delete** /api/v1/clients/{clientId}/friends/{nickName} | 从通讯录移除好友。
[**FriendsRemoveFriendByWxidFromCache**](FriendsAPI.md#FriendsRemoveFriendByWxidFromCache) | **Delete** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存移除好友。
[**FriendsRemoveFriendFromCache**](FriendsAPI.md#FriendsRemoveFriendFromCache) | **Delete** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存移除好友。
[**FriendsResumeAutoAccept**](FriendsAPI.md#FriendsResumeAutoAccept) | **Post** /api/v1/clients/{clientId}/friends/auto-accept/resume | 恢复新好友自动通过监听。
[**FriendsStartAutoAccept**](FriendsAPI.md#FriendsStartAutoAccept) | **Post** /api/v1/clients/{clientId}/friends/auto-accept/start | 启动新好友自动通过监听（按关键词/后缀/标签规则）。
[**FriendsStopAutoAccept**](FriendsAPI.md#FriendsStopAutoAccept) | **Post** /api/v1/clients/{clientId}/friends/auto-accept/stop | 停止新好友自动通过监听。



## FriendsAdd

> FriendsAdd(ctx, clientId).AddFriendsRequest(addFriendsRequest).Execute()

通过手机号码/微信号查找并添加好友。

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
	addFriendsRequest := *openapiclient.NewAddFriendsRequest() // AddFriendsRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsAdd(context.Background(), clientId).AddFriendsRequest(addFriendsRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsAdd``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsAddRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **addFriendsRequest** | [**AddFriendsRequest**](AddFriendsRequest.md) |  | 

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


## FriendsAddOrUpdateFriend

> FriendsAddOrUpdateFriend(ctx, clientId).FriendInfo(friendInfo).Execute()

手动新增或修改缓存中的好友对象。

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
	friendInfo := *openapiclient.NewFriendInfo() // FriendInfo |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsAddOrUpdateFriend(context.Background(), clientId).FriendInfo(friendInfo).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsAddOrUpdateFriend``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsAddOrUpdateFriendRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **friendInfo** | [**FriendInfo**](FriendInfo.md) |  | 

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


## FriendsCloseAddWindow

> FriendsCloseAddWindow(ctx, clientId).Execute()

关闭新增朋友窗口。

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
	r, err := apiClient.FriendsAPI.FriendsCloseAddWindow(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsCloseAddWindow``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsCloseAddWindowRequest struct via the builder pattern


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


## FriendsGetAllFriendNames

> FriendsGetAllFriendNames(ctx, clientId).Execute()

获取所有好友名称列表（有备注优先备注名）。

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
	r, err := apiClient.FriendsAPI.FriendsGetAllFriendNames(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsGetAllFriendNames``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsGetAllFriendNamesRequest struct via the builder pattern


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


## FriendsGetAllFriends

> FriendsGetAllFriends(ctx, clientId).FromCache(fromCache).Execute()

获取所有好友的信息列表（默认优先从缓存读取）。

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
	fromCache := true // bool |  (optional) (default to true)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsGetAllFriends(context.Background(), clientId).FromCache(fromCache).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsGetAllFriends``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsGetAllFriendsRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **fromCache** | **bool** |  | [default to true]

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


## FriendsGetFriendByWxidFromCache

> FriendsGetFriendByWxidFromCache(ctx, clientId, wxid).Execute()

按 wxid 从缓存获取好友信息。

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
	wxid := "wxid_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsGetFriendByWxidFromCache(context.Background(), clientId, wxid).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsGetFriendByWxidFromCache``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 
**wxid** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiFriendsGetFriendByWxidFromCacheRequest struct via the builder pattern


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


## FriendsGetFriendFromCache

> FriendsGetFriendFromCache(ctx, clientId, who).Execute()

按名称从缓存获取单个好友信息。

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
	who := "who_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsGetFriendFromCache(context.Background(), clientId, who).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsGetFriendFromCache``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 
**who** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiFriendsGetFriendFromCacheRequest struct via the builder pattern


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


## FriendsGetFriendListFromCache

> FriendsGetFriendListFromCache(ctx, clientId).Execute()

获取缓存中的全部好友信息列表。

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
	r, err := apiClient.FriendsAPI.FriendsGetFriendListFromCache(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsGetFriendListFromCache``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsGetFriendListFromCacheRequest struct via the builder pattern


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


## FriendsGetFriendsFromCache

> FriendsGetFriendsFromCache(ctx, clientId, who).Execute()

按名称从缓存获取好友列表（适用于昵称重复场景）。

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
	who := "who_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsGetFriendsFromCache(context.Background(), clientId, who).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsGetFriendsFromCache``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 
**who** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiFriendsGetFriendsFromCacheRequest struct via the builder pattern


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


## FriendsOpenAddWindow

> FriendsOpenAddWindow(ctx, clientId).Execute()

打开新增朋友窗口。

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
	r, err := apiClient.FriendsAPI.FriendsOpenAddWindow(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsOpenAddWindow``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsOpenAddWindowRequest struct via the builder pattern


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


## FriendsPassAll

> FriendsPassAll(ctx, clientId).PassedAllNewFriendRequest(passedAllNewFriendRequest).Execute()

立即批量通过当前待处理的新好友申请（不启动持续监听）。

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
	passedAllNewFriendRequest := *openapiclient.NewPassedAllNewFriendRequest() // PassedAllNewFriendRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsPassAll(context.Background(), clientId).PassedAllNewFriendRequest(passedAllNewFriendRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsPassAll``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsPassAllRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **passedAllNewFriendRequest** | [**PassedAllNewFriendRequest**](PassedAllNewFriendRequest.md) |  | 

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


## FriendsPauseAutoAccept

> FriendsPauseAutoAccept(ctx, clientId).Execute()

暂停新好友自动通过监听。

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
	r, err := apiClient.FriendsAPI.FriendsPauseAutoAccept(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsPauseAutoAccept``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsPauseAutoAcceptRequest struct via the builder pattern


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


## FriendsRemoveFriend

> FriendsRemoveFriend(ctx, clientId, nickName).Execute()

从通讯录移除好友。

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
	nickName := "nickName_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsRemoveFriend(context.Background(), clientId, nickName).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsRemoveFriend``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 
**nickName** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiFriendsRemoveFriendRequest struct via the builder pattern


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


## FriendsRemoveFriendByWxidFromCache

> FriendsRemoveFriendByWxidFromCache(ctx, clientId, wxid).Execute()

按 wxid 从缓存移除好友。

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
	wxid := "wxid_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsRemoveFriendByWxidFromCache(context.Background(), clientId, wxid).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsRemoveFriendByWxidFromCache``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 
**wxid** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiFriendsRemoveFriendByWxidFromCacheRequest struct via the builder pattern


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


## FriendsRemoveFriendFromCache

> FriendsRemoveFriendFromCache(ctx, clientId, who).Execute()

按名称从缓存移除好友。

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
	who := "who_example" // string | 

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsRemoveFriendFromCache(context.Background(), clientId, who).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsRemoveFriendFromCache``: %v\n", err)
		fmt.Fprintf(os.Stderr, "Full HTTP response: %v\n", r)
	}
}
```

### Path Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
**ctx** | **context.Context** | context for authentication, logging, cancellation, deadlines, tracing, etc.
**clientId** | **string** |  | 
**who** | **string** |  | 

### Other Parameters

Other parameters are passed through a pointer to a apiFriendsRemoveFriendFromCacheRequest struct via the builder pattern


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


## FriendsResumeAutoAccept

> FriendsResumeAutoAccept(ctx, clientId).Execute()

恢复新好友自动通过监听。

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
	r, err := apiClient.FriendsAPI.FriendsResumeAutoAccept(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsResumeAutoAccept``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsResumeAutoAcceptRequest struct via the builder pattern


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


## FriendsStartAutoAccept

> FriendsStartAutoAccept(ctx, clientId).PassedAllNewFriendRequest(passedAllNewFriendRequest).Execute()

启动新好友自动通过监听（按关键词/后缀/标签规则）。

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
	passedAllNewFriendRequest := *openapiclient.NewPassedAllNewFriendRequest() // PassedAllNewFriendRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.FriendsAPI.FriendsStartAutoAccept(context.Background(), clientId).PassedAllNewFriendRequest(passedAllNewFriendRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsStartAutoAccept``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsStartAutoAcceptRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **passedAllNewFriendRequest** | [**PassedAllNewFriendRequest**](PassedAllNewFriendRequest.md) |  | 

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


## FriendsStopAutoAccept

> FriendsStopAutoAccept(ctx, clientId).Execute()

停止新好友自动通过监听。

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
	r, err := apiClient.FriendsAPI.FriendsStopAutoAccept(context.Background(), clientId).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `FriendsAPI.FriendsStopAutoAccept``: %v\n", err)
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

Other parameters are passed through a pointer to a apiFriendsStopAutoAcceptRequest struct via the builder pattern


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

