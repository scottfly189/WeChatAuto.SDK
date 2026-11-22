# \NavigationAPI

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**NavigationClose**](NavigationAPI.md#NavigationClose) | **Post** /api/v1/clients/{clientId}/navigation/close | 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。
[**NavigationSwitch**](NavigationAPI.md#NavigationSwitch) | **Post** /api/v1/clients/{clientId}/navigation/switch | 切换导航栏。



## NavigationClose

> NavigationClose(ctx, clientId).SwitchNavigationRequest(switchNavigationRequest).Execute()

关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。

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
	switchNavigationRequest := *openapiclient.NewSwitchNavigationRequest() // SwitchNavigationRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.NavigationAPI.NavigationClose(context.Background(), clientId).SwitchNavigationRequest(switchNavigationRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `NavigationAPI.NavigationClose``: %v\n", err)
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

Other parameters are passed through a pointer to a apiNavigationCloseRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **switchNavigationRequest** | [**SwitchNavigationRequest**](SwitchNavigationRequest.md) |  | 

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


## NavigationSwitch

> NavigationSwitch(ctx, clientId).SwitchNavigationRequest(switchNavigationRequest).Execute()

切换导航栏。

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
	switchNavigationRequest := *openapiclient.NewSwitchNavigationRequest() // SwitchNavigationRequest |  (optional)

	configuration := openapiclient.NewConfiguration()
	apiClient := openapiclient.NewAPIClient(configuration)
	r, err := apiClient.NavigationAPI.NavigationSwitch(context.Background(), clientId).SwitchNavigationRequest(switchNavigationRequest).Execute()
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error when calling `NavigationAPI.NavigationSwitch``: %v\n", err)
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

Other parameters are passed through a pointer to a apiNavigationSwitchRequest struct via the builder pattern


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **switchNavigationRequest** | [**SwitchNavigationRequest**](SwitchNavigationRequest.md) |  | 

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

