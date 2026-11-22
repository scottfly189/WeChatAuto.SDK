# \NavigationApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**navigation_close**](NavigationApi.md#navigation_close) | **POST** /api/v1/clients/{clientId}/navigation/close | 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。
[**navigation_switch**](NavigationApi.md#navigation_switch) | **POST** /api/v1/clients/{clientId}/navigation/switch | 切换导航栏。



## navigation_close

> navigation_close(client_id, switch_navigation_request)
关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**switch_navigation_request** | Option<[**SwitchNavigationRequest**](SwitchNavigationRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## navigation_switch

> navigation_switch(client_id, switch_navigation_request)
切换导航栏。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**switch_navigation_request** | Option<[**SwitchNavigationRequest**](SwitchNavigationRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

