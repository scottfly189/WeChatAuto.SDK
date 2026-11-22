# \MomentsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**moments_add**](MomentsApi.md#moments_add) | **POST** /api/v1/clients/{clientId}/moments/add | 发朋友圈（图文，多图需在同一目录）。
[**moments_close**](MomentsApi.md#moments_close) | **POST** /api/v1/clients/{clientId}/moments/close | 关闭朋友圈。
[**moments_open**](MomentsApi.md#moments_open) | **POST** /api/v1/clients/{clientId}/moments/open | 打开朋友圈（已打开则提到最前）。
[**moments_remove**](MomentsApi.md#moments_remove) | **POST** /api/v1/clients/{clientId}/moments/remove | 删除自己发的朋友圈（按文字内容）。



## moments_add

> moments_add(client_id, add_moments_request)
发朋友圈（图文，多图需在同一目录）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**add_moments_request** | Option<[**AddMomentsRequest**](AddMomentsRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## moments_close

> moments_close(client_id)
关闭朋友圈。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## moments_open

> moments_open(client_id)
打开朋友圈（已打开则提到最前）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## moments_remove

> moments_remove(client_id, remove_moments_request)
删除自己发的朋友圈（按文字内容）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**remove_moments_request** | Option<[**RemoveMomentsRequest**](RemoveMomentsRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

