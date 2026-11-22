# wechatauto-client.MomentsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**momentsAdd**](MomentsApi.md#momentsAdd) | **POST** /api/v1/clients/{clientId}/moments/add | 发朋友圈（图文，多图需在同一目录）。
[**momentsClose**](MomentsApi.md#momentsClose) | **POST** /api/v1/clients/{clientId}/moments/close | 关闭朋友圈。
[**momentsOpen**](MomentsApi.md#momentsOpen) | **POST** /api/v1/clients/{clientId}/moments/open | 打开朋友圈（已打开则提到最前）。
[**momentsRemove**](MomentsApi.md#momentsRemove) | **POST** /api/v1/clients/{clientId}/moments/remove | 删除自己发的朋友圈（按文字内容）。



## momentsAdd

> momentsAdd(clientId, opts)

发朋友圈（图文，多图需在同一目录）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MomentsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'addMomentsRequest': new wechatauto-client.AddMomentsRequest() // AddMomentsRequest | 
};
apiInstance.momentsAdd(clientId, opts, (error, data, response) => {
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
 **addMomentsRequest** | [**AddMomentsRequest**](AddMomentsRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## momentsClose

> momentsClose(clientId)

关闭朋友圈。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MomentsApi();
let clientId = "clientId_example"; // String | 
apiInstance.momentsClose(clientId, (error, data, response) => {
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


## momentsOpen

> momentsOpen(clientId)

打开朋友圈（已打开则提到最前）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MomentsApi();
let clientId = "clientId_example"; // String | 
apiInstance.momentsOpen(clientId, (error, data, response) => {
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


## momentsRemove

> momentsRemove(clientId, opts)

删除自己发的朋友圈（按文字内容）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.MomentsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'removeMomentsRequest': new wechatauto-client.RemoveMomentsRequest() // RemoveMomentsRequest | 
};
apiInstance.momentsRemove(clientId, opts, (error, data, response) => {
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
 **removeMomentsRequest** | [**RemoveMomentsRequest**](RemoveMomentsRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

