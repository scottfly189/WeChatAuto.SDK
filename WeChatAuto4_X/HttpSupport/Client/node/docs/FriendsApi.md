# wechatauto-client.FriendsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**friendsAdd**](FriendsApi.md#friendsAdd) | **POST** /api/v1/clients/{clientId}/friends/add | 通过手机号码/微信号查找并添加好友。
[**friendsAddOrUpdateFriend**](FriendsApi.md#friendsAddOrUpdateFriend) | **POST** /api/v1/clients/{clientId}/friends/cache | 手动新增或修改缓存中的好友对象。
[**friendsCloseAddWindow**](FriendsApi.md#friendsCloseAddWindow) | **POST** /api/v1/clients/{clientId}/friends/add-window/close | 关闭新增朋友窗口。
[**friendsGetAllFriendNames**](FriendsApi.md#friendsGetAllFriendNames) | **GET** /api/v1/clients/{clientId}/friends/names | 获取所有好友名称列表（有备注优先备注名）。
[**friendsGetAllFriends**](FriendsApi.md#friendsGetAllFriends) | **GET** /api/v1/clients/{clientId}/friends | 获取所有好友的信息列表（默认优先从缓存读取）。
[**friendsGetFriendByWxidFromCache**](FriendsApi.md#friendsGetFriendByWxidFromCache) | **GET** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存获取好友信息。
[**friendsGetFriendFromCache**](FriendsApi.md#friendsGetFriendFromCache) | **GET** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存获取单个好友信息。
[**friendsGetFriendListFromCache**](FriendsApi.md#friendsGetFriendListFromCache) | **GET** /api/v1/clients/{clientId}/friends/cache | 获取缓存中的全部好友信息列表。
[**friendsGetFriendsFromCache**](FriendsApi.md#friendsGetFriendsFromCache) | **GET** /api/v1/clients/{clientId}/friends/cache/by-name/{who} | 按名称从缓存获取好友列表（适用于昵称重复场景）。
[**friendsOpenAddWindow**](FriendsApi.md#friendsOpenAddWindow) | **POST** /api/v1/clients/{clientId}/friends/add-window/open | 打开新增朋友窗口。
[**friendsPassAll**](FriendsApi.md#friendsPassAll) | **POST** /api/v1/clients/{clientId}/friends/pass-all | 立即批量通过当前待处理的新好友申请（不启动持续监听）。
[**friendsPauseAutoAccept**](FriendsApi.md#friendsPauseAutoAccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/pause | 暂停新好友自动通过监听。
[**friendsRemoveFriend**](FriendsApi.md#friendsRemoveFriend) | **DELETE** /api/v1/clients/{clientId}/friends/{nickName} | 从通讯录移除好友。
[**friendsRemoveFriendByWxidFromCache**](FriendsApi.md#friendsRemoveFriendByWxidFromCache) | **DELETE** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存移除好友。
[**friendsRemoveFriendFromCache**](FriendsApi.md#friendsRemoveFriendFromCache) | **DELETE** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存移除好友。
[**friendsResumeAutoAccept**](FriendsApi.md#friendsResumeAutoAccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/resume | 恢复新好友自动通过监听。
[**friendsStartAutoAccept**](FriendsApi.md#friendsStartAutoAccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/start | 启动新好友自动通过监听（按关键词/后缀/标签规则）。
[**friendsStopAutoAccept**](FriendsApi.md#friendsStopAutoAccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/stop | 停止新好友自动通过监听。



## friendsAdd

> friendsAdd(clientId, opts)

通过手机号码/微信号查找并添加好友。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'addFriendsRequest': new wechatauto-client.AddFriendsRequest() // AddFriendsRequest | 
};
apiInstance.friendsAdd(clientId, opts, (error, data, response) => {
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
 **addFriendsRequest** | [**AddFriendsRequest**](AddFriendsRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## friendsAddOrUpdateFriend

> friendsAddOrUpdateFriend(clientId, opts)

手动新增或修改缓存中的好友对象。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'friendInfo': new wechatauto-client.FriendInfo() // FriendInfo | 
};
apiInstance.friendsAddOrUpdateFriend(clientId, opts, (error, data, response) => {
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
 **friendInfo** | [**FriendInfo**](FriendInfo.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## friendsCloseAddWindow

> friendsCloseAddWindow(clientId)

关闭新增朋友窗口。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
apiInstance.friendsCloseAddWindow(clientId, (error, data, response) => {
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


## friendsGetAllFriendNames

> friendsGetAllFriendNames(clientId)

获取所有好友名称列表（有备注优先备注名）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
apiInstance.friendsGetAllFriendNames(clientId, (error, data, response) => {
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


## friendsGetAllFriends

> friendsGetAllFriends(clientId, opts)

获取所有好友的信息列表（默认优先从缓存读取）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'fromCache': true // Boolean | 
};
apiInstance.friendsGetAllFriends(clientId, opts, (error, data, response) => {
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
 **fromCache** | **Boolean**|  | [optional] [default to true]

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## friendsGetFriendByWxidFromCache

> friendsGetFriendByWxidFromCache(clientId, wxid)

按 wxid 从缓存获取好友信息。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let wxid = "wxid_example"; // String | 
apiInstance.friendsGetFriendByWxidFromCache(clientId, wxid, (error, data, response) => {
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
 **wxid** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## friendsGetFriendFromCache

> friendsGetFriendFromCache(clientId, who)

按名称从缓存获取单个好友信息。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let who = "who_example"; // String | 
apiInstance.friendsGetFriendFromCache(clientId, who, (error, data, response) => {
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
 **who** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## friendsGetFriendListFromCache

> friendsGetFriendListFromCache(clientId)

获取缓存中的全部好友信息列表。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
apiInstance.friendsGetFriendListFromCache(clientId, (error, data, response) => {
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


## friendsGetFriendsFromCache

> friendsGetFriendsFromCache(clientId, who)

按名称从缓存获取好友列表（适用于昵称重复场景）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let who = "who_example"; // String | 
apiInstance.friendsGetFriendsFromCache(clientId, who, (error, data, response) => {
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
 **who** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## friendsOpenAddWindow

> friendsOpenAddWindow(clientId)

打开新增朋友窗口。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
apiInstance.friendsOpenAddWindow(clientId, (error, data, response) => {
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


## friendsPassAll

> friendsPassAll(clientId, opts)

立即批量通过当前待处理的新好友申请（不启动持续监听）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'passedAllNewFriendRequest': new wechatauto-client.PassedAllNewFriendRequest() // PassedAllNewFriendRequest | 
};
apiInstance.friendsPassAll(clientId, opts, (error, data, response) => {
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
 **passedAllNewFriendRequest** | [**PassedAllNewFriendRequest**](PassedAllNewFriendRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## friendsPauseAutoAccept

> friendsPauseAutoAccept(clientId)

暂停新好友自动通过监听。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
apiInstance.friendsPauseAutoAccept(clientId, (error, data, response) => {
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


## friendsRemoveFriend

> friendsRemoveFriend(clientId, nickName)

从通讯录移除好友。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let nickName = "nickName_example"; // String | 
apiInstance.friendsRemoveFriend(clientId, nickName, (error, data, response) => {
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
 **nickName** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## friendsRemoveFriendByWxidFromCache

> friendsRemoveFriendByWxidFromCache(clientId, wxid)

按 wxid 从缓存移除好友。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let wxid = "wxid_example"; // String | 
apiInstance.friendsRemoveFriendByWxidFromCache(clientId, wxid, (error, data, response) => {
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
 **wxid** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## friendsRemoveFriendFromCache

> friendsRemoveFriendFromCache(clientId, who)

按名称从缓存移除好友。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let who = "who_example"; // String | 
apiInstance.friendsRemoveFriendFromCache(clientId, who, (error, data, response) => {
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
 **who** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## friendsResumeAutoAccept

> friendsResumeAutoAccept(clientId)

恢复新好友自动通过监听。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
apiInstance.friendsResumeAutoAccept(clientId, (error, data, response) => {
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


## friendsStartAutoAccept

> friendsStartAutoAccept(clientId, opts)

启动新好友自动通过监听（按关键词/后缀/标签规则）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'passedAllNewFriendRequest': new wechatauto-client.PassedAllNewFriendRequest() // PassedAllNewFriendRequest | 
};
apiInstance.friendsStartAutoAccept(clientId, opts, (error, data, response) => {
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
 **passedAllNewFriendRequest** | [**PassedAllNewFriendRequest**](PassedAllNewFriendRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## friendsStopAutoAccept

> friendsStopAutoAccept(clientId)

停止新好友自动通过监听。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.FriendsApi();
let clientId = "clientId_example"; // String | 
apiInstance.friendsStopAutoAccept(clientId, (error, data, response) => {
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

