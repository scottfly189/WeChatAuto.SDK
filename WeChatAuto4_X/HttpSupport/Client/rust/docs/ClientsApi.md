# \ClientsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**clients_click_notify_icon**](ClientsApi.md#clients_click_notify_icon) | **POST** /api/v1/clients/{clientId}/notify-icon/click | 点击任务栏微信图标，按索引或微信名称定位。
[**clients_get_all**](ClientsApi.md#clients_get_all) | **GET** /api/v1/clients | 获取全部微信客户端的概要信息列表。
[**clients_get_info**](ClientsApi.md#clients_get_info) | **GET** /api/v1/clients/{clientId} | 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。
[**clients_get_owner_info**](ClientsApi.md#clients_get_owner_info) | **GET** /api/v1/clients/{clientId}/owner-info | 获取本微信的个人信息（头像文件位置、wxid、昵称）。
[**clients_get_storage_root**](ClientsApi.md#clients_get_storage_root) | **GET** /api/v1/clients/{clientId}/storage-root | 通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。
[**clients_get_version**](ClientsApi.md#clients_get_version) | **GET** /api/v1/clients/{clientId}/version | 获取本微信的版本号。
[**clients_save_avatar**](ClientsApi.md#clients_save_avatar) | **POST** /api/v1/clients/{clientId}/avatar | 保存本微信头像到指定路径。



## clients_click_notify_icon

> clients_click_notify_icon(client_id, notify_icon_click_request)
点击任务栏微信图标，按索引或微信名称定位。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**notify_icon_click_request** | Option<[**NotifyIconClickRequest**](NotifyIconClickRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## clients_get_all

> clients_get_all()
获取全部微信客户端的概要信息列表。

### Parameters

This endpoint does not need any parameter.

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## clients_get_info

> clients_get_info(client_id)
获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。

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


## clients_get_owner_info

> clients_get_owner_info(client_id)
获取本微信的个人信息（头像文件位置、wxid、昵称）。

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


## clients_get_storage_root

> clients_get_storage_root(client_id)
通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。

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


## clients_get_version

> clients_get_version(client_id)
获取本微信的版本号。

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


## clients_save_avatar

> clients_save_avatar(client_id, save_avatar_request)
保存本微信头像到指定路径。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**save_avatar_request** | Option<[**SaveAvatarRequest**](SaveAvatarRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

