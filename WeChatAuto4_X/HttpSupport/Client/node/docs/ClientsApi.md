# wechatauto-client.ClientsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**clientsClickNotifyIcon**](ClientsApi.md#clientsClickNotifyIcon) | **POST** /api/v1/clients/{clientId}/notify-icon/click | 点击任务栏微信图标，按索引或微信名称定位。
[**clientsGetAll**](ClientsApi.md#clientsGetAll) | **GET** /api/v1/clients | 获取全部微信客户端的概要信息列表。
[**clientsGetInfo**](ClientsApi.md#clientsGetInfo) | **GET** /api/v1/clients/{clientId} | 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。
[**clientsGetOwnerInfo**](ClientsApi.md#clientsGetOwnerInfo) | **GET** /api/v1/clients/{clientId}/owner-info | 获取本微信的个人信息（头像文件位置、wxid、昵称）。
[**clientsGetStorageRoot**](ClientsApi.md#clientsGetStorageRoot) | **GET** /api/v1/clients/{clientId}/storage-root | 通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。
[**clientsGetVersion**](ClientsApi.md#clientsGetVersion) | **GET** /api/v1/clients/{clientId}/version | 获取本微信的版本号。
[**clientsSaveAvatar**](ClientsApi.md#clientsSaveAvatar) | **POST** /api/v1/clients/{clientId}/avatar | 保存本微信头像到指定路径。



## clientsClickNotifyIcon

> clientsClickNotifyIcon(clientId, opts)

点击任务栏微信图标，按索引或微信名称定位。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ClientsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'notifyIconClickRequest': new wechatauto-client.NotifyIconClickRequest() // NotifyIconClickRequest | 
};
apiInstance.clientsClickNotifyIcon(clientId, opts, (error, data, response) => {
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
 **notifyIconClickRequest** | [**NotifyIconClickRequest**](NotifyIconClickRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## clientsGetAll

> clientsGetAll()

获取全部微信客户端的概要信息列表。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ClientsApi();
apiInstance.clientsGetAll((error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters

This endpoint does not need any parameter.

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## clientsGetInfo

> clientsGetInfo(clientId)

获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ClientsApi();
let clientId = "clientId_example"; // String | 
apiInstance.clientsGetInfo(clientId, (error, data, response) => {
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


## clientsGetOwnerInfo

> clientsGetOwnerInfo(clientId)

获取本微信的个人信息（头像文件位置、wxid、昵称）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ClientsApi();
let clientId = "clientId_example"; // String | 
apiInstance.clientsGetOwnerInfo(clientId, (error, data, response) => {
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


## clientsGetStorageRoot

> clientsGetStorageRoot(clientId)

通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ClientsApi();
let clientId = "clientId_example"; // String | 
apiInstance.clientsGetStorageRoot(clientId, (error, data, response) => {
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


## clientsGetVersion

> clientsGetVersion(clientId)

获取本微信的版本号。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ClientsApi();
let clientId = "clientId_example"; // String | 
apiInstance.clientsGetVersion(clientId, (error, data, response) => {
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


## clientsSaveAvatar

> clientsSaveAvatar(clientId, opts)

保存本微信头像到指定路径。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ClientsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'saveAvatarRequest': new wechatauto-client.SaveAvatarRequest() // SaveAvatarRequest | 
};
apiInstance.clientsSaveAvatar(clientId, opts, (error, data, response) => {
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
 **saveAvatarRequest** | [**SaveAvatarRequest**](SaveAvatarRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

