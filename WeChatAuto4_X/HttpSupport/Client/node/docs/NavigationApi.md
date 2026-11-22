# wechatauto-client.NavigationApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**navigationClose**](NavigationApi.md#navigationClose) | **POST** /api/v1/clients/{clientId}/navigation/close | 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。
[**navigationSwitch**](NavigationApi.md#navigationSwitch) | **POST** /api/v1/clients/{clientId}/navigation/switch | 切换导航栏。



## navigationClose

> navigationClose(clientId, opts)

关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.NavigationApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'switchNavigationRequest': new wechatauto-client.SwitchNavigationRequest() // SwitchNavigationRequest | 
};
apiInstance.navigationClose(clientId, opts, (error, data, response) => {
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
 **switchNavigationRequest** | [**SwitchNavigationRequest**](SwitchNavigationRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## navigationSwitch

> navigationSwitch(clientId, opts)

切换导航栏。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.NavigationApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'switchNavigationRequest': new wechatauto-client.SwitchNavigationRequest() // SwitchNavigationRequest | 
};
apiInstance.navigationSwitch(clientId, opts, (error, data, response) => {
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
 **switchNavigationRequest** | [**SwitchNavigationRequest**](SwitchNavigationRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

