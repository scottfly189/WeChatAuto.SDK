# wechatauto-client.DatabaseApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**databaseGetContact**](DatabaseApi.md#databaseGetContact) | **GET** /api/v1/clients/{clientId}/database/contacts/{userName} | 按 username（wxid / 群 id）取一条联系人。
[**databaseGetContactByShowName**](DatabaseApi.md#databaseGetContactByShowName) | **GET** /api/v1/clients/{clientId}/database/contacts/by-show-name/first | 按显示名取第一条联系人（重名时取 UserName 升序第一条）。
[**databaseGetContacts**](DatabaseApi.md#databaseGetContacts) | **GET** /api/v1/clients/{clientId}/database/contacts | 获取当前微信号全部联系人（不含已删除）。
[**databaseGetContactsByShowName**](DatabaseApi.md#databaseGetContactsByShowName) | **GET** /api/v1/clients/{clientId}/database/contacts/by-show-name | 按显示名取联系人；重名时返回多个。
[**databaseGetMaxSortSeq**](DatabaseApi.md#databaseGetMaxSortSeq) | **GET** /api/v1/clients/{clientId}/database/messages/max-sort-seq | 取当前微信号消息的最大 SortSeq（增量水位）。
[**databaseGetMessages**](DatabaseApi.md#databaseGetMessages) | **GET** /api/v1/clients/{clientId}/database/messages | 取指定会话最近 N 条消息（从新到旧）。
[**databaseGetNewMessages**](DatabaseApi.md#databaseGetNewMessages) | **GET** /api/v1/clients/{clientId}/database/messages/new | 取 SortSeq 大于指定值的新消息（从旧到新）。
[**databaseGetRecentMessages**](DatabaseApi.md#databaseGetRecentMessages) | **GET** /api/v1/clients/{clientId}/database/messages/recent | 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。
[**databaseGetRoomByShowName**](DatabaseApi.md#databaseGetRoomByShowName) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name/first | 按群的显示名取第一条群。
[**databaseGetRoomMembers**](DatabaseApi.md#databaseGetRoomMembers) | **GET** /api/v1/clients/{clientId}/database/rooms/{roomUsername}/members | 按群 username 取成员列表。
[**databaseGetRoomMembersByShowName**](DatabaseApi.md#databaseGetRoomMembersByShowName) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name/members | 按群的显示名取成员列表；重名时取第一条群。
[**databaseGetRooms**](DatabaseApi.md#databaseGetRooms) | **GET** /api/v1/clients/{clientId}/database/rooms | 获取当前微信号全部群聊。
[**databaseGetRoomsByLocalType**](DatabaseApi.md#databaseGetRoomsByLocalType) | **GET** /api/v1/clients/{clientId}/database/rooms/by-local-type | 按联系人 LocalType 过滤群聊（本机观察群多为 2）。
[**databaseGetRoomsByShowName**](DatabaseApi.md#databaseGetRoomsByShowName) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name | 按群的显示名取群；重名时返回多个。
[**databaseGetSnsPost**](DatabaseApi.md#databaseGetSnsPost) | **GET** /api/v1/clients/{clientId}/database/sns/posts/{tid} | 按 tid 取一条朋友圈（含媒体、点赞、评论）。
[**databaseGetSnsPosts**](DatabaseApi.md#databaseGetSnsPosts) | **GET** /api/v1/clients/{clientId}/database/sns/posts | 取最近 N 条朋友圈（含媒体、点赞、评论）。
[**databaseGetSnsPostsByDate**](DatabaseApi.md#databaseGetSnsPostsByDate) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-date | 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。
[**databaseGetSnsPostsByShowName**](DatabaseApi.md#databaseGetSnsPostsByShowName) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-show-name | 按发布者显示名取朋友圈；重名会一并返回。
[**databaseGetSnsPostsByUserName**](DatabaseApi.md#databaseGetSnsPostsByUserName) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-user | 取某个发布者（真实 UserName / wxid）的朋友圈。
[**databaseQuery**](DatabaseApi.md#databaseQuery) | **POST** /api/v1/clients/{clientId}/database/query | 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。
[**databaseSearchAllMessages**](DatabaseApi.md#databaseSearchAllMessages) | **GET** /api/v1/clients/{clientId}/database/messages/search-all | 按正文关键字跨所有微信号搜索消息。
[**databaseSearchContacts**](DatabaseApi.md#databaseSearchContacts) | **GET** /api/v1/clients/{clientId}/database/contacts/search | 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。
[**databaseSearchMessages**](DatabaseApi.md#databaseSearchMessages) | **GET** /api/v1/clients/{clientId}/database/messages/search | 按正文关键字搜索当前微信号消息。



## databaseGetContact

> databaseGetContact(clientId, userName)

按 username（wxid / 群 id）取一条联系人。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let userName = "userName_example"; // String | 
apiInstance.databaseGetContact(clientId, userName, (error, data, response) => {
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
 **userName** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetContactByShowName

> databaseGetContactByShowName(clientId, opts)

按显示名取第一条联系人（重名时取 UserName 升序第一条）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'showName': "showName_example" // String | 
};
apiInstance.databaseGetContactByShowName(clientId, opts, (error, data, response) => {
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
 **showName** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetContacts

> databaseGetContacts(clientId)

获取当前微信号全部联系人（不含已删除）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
apiInstance.databaseGetContacts(clientId, (error, data, response) => {
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


## databaseGetContactsByShowName

> databaseGetContactsByShowName(clientId, opts)

按显示名取联系人；重名时返回多个。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'showName': "showName_example" // String | 
};
apiInstance.databaseGetContactsByShowName(clientId, opts, (error, data, response) => {
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
 **showName** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetMaxSortSeq

> databaseGetMaxSortSeq(clientId)

取当前微信号消息的最大 SortSeq（增量水位）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
apiInstance.databaseGetMaxSortSeq(clientId, (error, data, response) => {
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


## databaseGetMessages

> databaseGetMessages(clientId, opts)

取指定会话最近 N 条消息（从新到旧）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'chatUsername': "chatUsername_example", // String | 
  'count': 50 // Number | 
};
apiInstance.databaseGetMessages(clientId, opts, (error, data, response) => {
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
 **chatUsername** | **String**|  | [optional] 
 **count** | **Number**|  | [optional] [default to 50]

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetNewMessages

> databaseGetNewMessages(clientId, opts)

取 SortSeq 大于指定值的新消息（从旧到新）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'afterSortSeq': 789 // Number | 
};
apiInstance.databaseGetNewMessages(clientId, opts, (error, data, response) => {
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
 **afterSortSeq** | **Number**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetRecentMessages

> databaseGetRecentMessages(clientId, opts)

取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'chatUsername': "chatUsername_example", // String | 
  'count': 50 // Number | 
};
apiInstance.databaseGetRecentMessages(clientId, opts, (error, data, response) => {
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
 **chatUsername** | **String**|  | [optional] 
 **count** | **Number**|  | [optional] [default to 50]

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetRoomByShowName

> databaseGetRoomByShowName(clientId, opts)

按群的显示名取第一条群。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'showName': "showName_example" // String | 
};
apiInstance.databaseGetRoomByShowName(clientId, opts, (error, data, response) => {
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
 **showName** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetRoomMembers

> databaseGetRoomMembers(clientId, roomUsername)

按群 username 取成员列表。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let roomUsername = "roomUsername_example"; // String | 
apiInstance.databaseGetRoomMembers(clientId, roomUsername, (error, data, response) => {
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
 **roomUsername** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetRoomMembersByShowName

> databaseGetRoomMembersByShowName(clientId, opts)

按群的显示名取成员列表；重名时取第一条群。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'showName': "showName_example" // String | 
};
apiInstance.databaseGetRoomMembersByShowName(clientId, opts, (error, data, response) => {
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
 **showName** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetRooms

> databaseGetRooms(clientId)

获取当前微信号全部群聊。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
apiInstance.databaseGetRooms(clientId, (error, data, response) => {
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


## databaseGetRoomsByLocalType

> databaseGetRoomsByLocalType(clientId, opts)

按联系人 LocalType 过滤群聊（本机观察群多为 2）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'localType': 56 // Number | 
};
apiInstance.databaseGetRoomsByLocalType(clientId, opts, (error, data, response) => {
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
 **localType** | **Number**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetRoomsByShowName

> databaseGetRoomsByShowName(clientId, opts)

按群的显示名取群；重名时返回多个。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'showName': "showName_example" // String | 
};
apiInstance.databaseGetRoomsByShowName(clientId, opts, (error, data, response) => {
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
 **showName** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetSnsPost

> databaseGetSnsPost(clientId, tid)

按 tid 取一条朋友圈（含媒体、点赞、评论）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let tid = 789; // Number | 
apiInstance.databaseGetSnsPost(clientId, tid, (error, data, response) => {
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
 **tid** | **Number**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetSnsPosts

> databaseGetSnsPosts(clientId, opts)

取最近 N 条朋友圈（含媒体、点赞、评论）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'limit': 20 // Number | 
};
apiInstance.databaseGetSnsPosts(clientId, opts, (error, data, response) => {
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
 **limit** | **Number**|  | [optional] [default to 20]

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetSnsPostsByDate

> databaseGetSnsPostsByDate(clientId, opts)

取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'date': new Date("2013-10-20T19:20:30+01:00") // Date | 
};
apiInstance.databaseGetSnsPostsByDate(clientId, opts, (error, data, response) => {
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
 **date** | **Date**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetSnsPostsByShowName

> databaseGetSnsPostsByShowName(clientId, opts)

按发布者显示名取朋友圈；重名会一并返回。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'showName': "showName_example" // String | 
};
apiInstance.databaseGetSnsPostsByShowName(clientId, opts, (error, data, response) => {
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
 **showName** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseGetSnsPostsByUserName

> databaseGetSnsPostsByUserName(clientId, opts)

取某个发布者（真实 UserName / wxid）的朋友圈。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'userName': "userName_example" // String | 
};
apiInstance.databaseGetSnsPostsByUserName(clientId, opts, (error, data, response) => {
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
 **userName** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseQuery

> databaseQuery(clientId, opts)

执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'databaseQueryRequest': new wechatauto-client.DatabaseQueryRequest() // DatabaseQueryRequest | 
};
apiInstance.databaseQuery(clientId, opts, (error, data, response) => {
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
 **databaseQueryRequest** | [**DatabaseQueryRequest**](DatabaseQueryRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## databaseSearchAllMessages

> databaseSearchAllMessages(clientId, opts)

按正文关键字跨所有微信号搜索消息。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'keyword': "keyword_example", // String | 
  'chatUsername': "chatUsername_example" // String | 
};
apiInstance.databaseSearchAllMessages(clientId, opts, (error, data, response) => {
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
 **keyword** | **String**|  | [optional] 
 **chatUsername** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseSearchContacts

> databaseSearchContacts(clientId, opts)

按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'keyword': "keyword_example" // String | 
};
apiInstance.databaseSearchContacts(clientId, opts, (error, data, response) => {
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
 **keyword** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## databaseSearchMessages

> databaseSearchMessages(clientId, opts)

按正文关键字搜索当前微信号消息。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.DatabaseApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'keyword': "keyword_example", // String | 
  'chatUsername': "chatUsername_example" // String | 
};
apiInstance.databaseSearchMessages(clientId, opts, (error, data, response) => {
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
 **keyword** | **String**|  | [optional] 
 **chatUsername** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

