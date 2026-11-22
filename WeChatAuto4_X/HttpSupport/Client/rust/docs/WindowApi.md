# \WindowApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**window_close**](WindowApi.md#window_close) | **POST** /api/v1/clients/{clientId}/window/close | 关闭本微信窗口。
[**window_close_search_window**](WindowApi.md#window_close_search_window) | **POST** /api/v1/clients/{clientId}/window/close-search-window | 关闭查询窗口（已打开则关闭）。
[**window_close_sub_window**](WindowApi.md#window_close_sub_window) | **POST** /api/v1/clients/{clientId}/window/close-sub-window | 关闭指定对象的子窗口（已存在则关闭）。
[**window_focus**](WindowApi.md#window_focus) | **POST** /api/v1/clients/{clientId}/window/focus | 使主窗口获取焦点。
[**window_get_handler**](WindowApi.md#window_get_handler) | **GET** /api/v1/clients/{clientId}/window/handler | 获取本微信窗口句柄。
[**window_get_process_id**](WindowApi.md#window_get_process_id) | **GET** /api/v1/clients/{clientId}/window/process-id | 获取本微信窗口进程 ID。
[**window_maximize**](WindowApi.md#window_maximize) | **POST** /api/v1/clients/{clientId}/window/maximize | 最大化微信窗口。
[**window_open**](WindowApi.md#window_open) | **POST** /api/v1/clients/{clientId}/window/open | 打开本微信窗口。
[**window_open_sub_window**](WindowApi.md#window_open_sub_window) | **POST** /api/v1/clients/{clientId}/window/open-sub-window | 打开指定对象的子窗口。
[**window_pin**](WindowApi.md#window_pin) | **POST** /api/v1/clients/{clientId}/window/pin | 置顶微信窗口。
[**window_restore**](WindowApi.md#window_restore) | **POST** /api/v1/clients/{clientId}/window/restore | 还原微信窗口。
[**window_unpin**](WindowApi.md#window_unpin) | **POST** /api/v1/clients/{clientId}/window/unpin | 取消置顶微信窗口。



## window_close

> window_close(client_id)
关闭本微信窗口。

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


## window_close_search_window

> window_close_search_window(client_id, sub_window_request)
关闭查询窗口（已打开则关闭）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**sub_window_request** | Option<[**SubWindowRequest**](SubWindowRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## window_close_sub_window

> window_close_sub_window(client_id, sub_window_request)
关闭指定对象的子窗口（已存在则关闭）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**sub_window_request** | Option<[**SubWindowRequest**](SubWindowRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## window_focus

> window_focus(client_id)
使主窗口获取焦点。

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


## window_get_handler

> window_get_handler(client_id)
获取本微信窗口句柄。

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


## window_get_process_id

> window_get_process_id(client_id)
获取本微信窗口进程 ID。

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


## window_maximize

> window_maximize(client_id)
最大化微信窗口。

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


## window_open

> window_open(client_id)
打开本微信窗口。

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


## window_open_sub_window

> window_open_sub_window(client_id, sub_window_request)
打开指定对象的子窗口。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**sub_window_request** | Option<[**SubWindowRequest**](SubWindowRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## window_pin

> window_pin(client_id)
置顶微信窗口。

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


## window_restore

> window_restore(client_id)
还原微信窗口。

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


## window_unpin

> window_unpin(client_id)
取消置顶微信窗口。

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

