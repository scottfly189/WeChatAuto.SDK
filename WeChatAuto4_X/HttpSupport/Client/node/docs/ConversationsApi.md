# wechatauto-client.ConversationsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**conversationsGetAllConversations**](ConversationsApi.md#conversationsGetAllConversations) | **GET** /api/v1/clients/{clientId}/conversations | 获取会话列表所有会话的标题。
[**conversationsGetVisibleConversations**](ConversationsApi.md#conversationsGetVisibleConversations) | **GET** /api/v1/clients/{clientId}/conversations/visible | 获取可见会话列表（含名称、未读消息数、头像等）。
[**conversationsGetVisibleTitles**](ConversationsApi.md#conversationsGetVisibleTitles) | **GET** /api/v1/clients/{clientId}/conversations/visible-titles | 获取会话列表可见会话的标题。
[**conversationsLocate**](ConversationsApi.md#conversationsLocate) | **POST** /api/v1/clients/{clientId}/conversations/locate | 定位会话，滚动会话列表使指定会话可见。
[**conversationsSearch**](ConversationsApi.md#conversationsSearch) | **POST** /api/v1/clients/{clientId}/conversations/search | 搜索好友/群聊。
[**conversationsSetDoNotDisturb**](ConversationsApi.md#conversationsSetDoNotDisturb) | **POST** /api/v1/clients/{clientId}/conversations/set-do-not-disturb | 设置会话消息免打扰。
[**conversationsSetTopMost**](ConversationsApi.md#conversationsSetTopMost) | **POST** /api/v1/clients/{clientId}/conversations/set-top-most | 设置会话置顶。



## conversationsGetAllConversations

> conversationsGetAllConversations(clientId)

获取会话列表所有会话的标题。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ConversationsApi();
let clientId = "clientId_example"; // String | 
apiInstance.conversationsGetAllConversations(clientId, (error, data, response) => {
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


## conversationsGetVisibleConversations

> conversationsGetVisibleConversations(clientId)

获取可见会话列表（含名称、未读消息数、头像等）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ConversationsApi();
let clientId = "clientId_example"; // String | 
apiInstance.conversationsGetVisibleConversations(clientId, (error, data, response) => {
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


## conversationsGetVisibleTitles

> conversationsGetVisibleTitles(clientId)

获取会话列表可见会话的标题。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ConversationsApi();
let clientId = "clientId_example"; // String | 
apiInstance.conversationsGetVisibleTitles(clientId, (error, data, response) => {
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


## conversationsLocate

> conversationsLocate(clientId, opts)

定位会话，滚动会话列表使指定会话可见。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ConversationsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'locateConversationRequest': new wechatauto-client.LocateConversationRequest() // LocateConversationRequest | 
};
apiInstance.conversationsLocate(clientId, opts, (error, data, response) => {
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
 **locateConversationRequest** | [**LocateConversationRequest**](LocateConversationRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## conversationsSearch

> conversationsSearch(clientId, opts)

搜索好友/群聊。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ConversationsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'searchConversationRequest': new wechatauto-client.SearchConversationRequest() // SearchConversationRequest | 
};
apiInstance.conversationsSearch(clientId, opts, (error, data, response) => {
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
 **searchConversationRequest** | [**SearchConversationRequest**](SearchConversationRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## conversationsSetDoNotDisturb

> conversationsSetDoNotDisturb(clientId, opts)

设置会话消息免打扰。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ConversationsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'setDoNotDisturbRequest': new wechatauto-client.SetDoNotDisturbRequest() // SetDoNotDisturbRequest | 
};
apiInstance.conversationsSetDoNotDisturb(clientId, opts, (error, data, response) => {
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
 **setDoNotDisturbRequest** | [**SetDoNotDisturbRequest**](SetDoNotDisturbRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## conversationsSetTopMost

> conversationsSetTopMost(clientId, opts)

设置会话置顶。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.ConversationsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'setTopMostRequest': new wechatauto-client.SetTopMostRequest() // SetTopMostRequest | 
};
apiInstance.conversationsSetTopMost(clientId, opts, (error, data, response) => {
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
 **setTopMostRequest** | [**SetTopMostRequest**](SetTopMostRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

