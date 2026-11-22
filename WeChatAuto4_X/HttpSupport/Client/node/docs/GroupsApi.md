# wechatauto-client.GroupsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**groupsAddMembers**](GroupsApi.md#groupsAddMembers) | **POST** /api/v1/clients/{clientId}/groups/members | 添加群聊成员（适用于自有群）。
[**groupsAddMembersToFriends**](GroupsApi.md#groupsAddMembersToFriends) | **POST** /api/v1/clients/{clientId}/groups/add-members-to-friends | 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。
[**groupsChangeMemo**](GroupsApi.md#groupsChangeMemo) | **PUT** /api/v1/clients/{clientId}/groups/memo | 修改群备注（仅自己可见）。
[**groupsChangeName**](GroupsApi.md#groupsChangeName) | **PUT** /api/v1/clients/{clientId}/groups/name | 修改群名（适用于自有群）。
[**groupsChangeNickName**](GroupsApi.md#groupsChangeNickName) | **PUT** /api/v1/clients/{clientId}/groups/nickname | 修改自己在群中的昵称。
[**groupsCreate**](GroupsApi.md#groupsCreate) | **POST** /api/v1/clients/{clientId}/groups | 创建群聊，已存在则打开群聊。
[**groupsGetMembers**](GroupsApi.md#groupsGetMembers) | **GET** /api/v1/clients/{clientId}/groups/members | 获取群聊成员列表。
[**groupsGetOwner**](GroupsApi.md#groupsGetOwner) | **GET** /api/v1/clients/{clientId}/groups/{groupName}/owner | 获取群主昵称。
[**groupsInvite**](GroupsApi.md#groupsInvite) | **POST** /api/v1/clients/{clientId}/groups/invite | 邀请群聊成员（适用于外部群）。
[**groupsIsOwner**](GroupsApi.md#groupsIsOwner) | **GET** /api/v1/clients/{clientId}/groups/{groupName}/is-owner | 判断群聊是否为自有群。
[**groupsQuit**](GroupsApi.md#groupsQuit) | **DELETE** /api/v1/clients/{clientId}/groups/{groupName} | 退出群聊。
[**groupsRemoveMembers**](GroupsApi.md#groupsRemoveMembers) | **DELETE** /api/v1/clients/{clientId}/groups/members | 移除群聊成员（适用于自有群）。
[**groupsUpdateNotice**](GroupsApi.md#groupsUpdateNotice) | **PUT** /api/v1/clients/{clientId}/groups/notice | 更新群公告（仅适用于自有群）。



## groupsAddMembers

> groupsAddMembers(clientId, opts)

添加群聊成员（适用于自有群）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'groupMemberRequest': new wechatauto-client.GroupMemberRequest() // GroupMemberRequest | 
};
apiInstance.groupsAddMembers(clientId, opts, (error, data, response) => {
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
 **groupMemberRequest** | [**GroupMemberRequest**](GroupMemberRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## groupsAddMembersToFriends

> groupsAddMembersToFriends(clientId, opts)

把群聊成员加为自己的好友（适用于外部群，注意微信风控）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'addGroupMemberToFriendsRequest': new wechatauto-client.AddGroupMemberToFriendsRequest() // AddGroupMemberToFriendsRequest | 
};
apiInstance.groupsAddMembersToFriends(clientId, opts, (error, data, response) => {
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
 **addGroupMemberToFriendsRequest** | [**AddGroupMemberToFriendsRequest**](AddGroupMemberToFriendsRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## groupsChangeMemo

> groupsChangeMemo(clientId, opts)

修改群备注（仅自己可见）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'changeGroupMemoRequest': new wechatauto-client.ChangeGroupMemoRequest() // ChangeGroupMemoRequest | 
};
apiInstance.groupsChangeMemo(clientId, opts, (error, data, response) => {
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
 **changeGroupMemoRequest** | [**ChangeGroupMemoRequest**](ChangeGroupMemoRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## groupsChangeName

> groupsChangeName(clientId, opts)

修改群名（适用于自有群）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'renameGroupRequest': new wechatauto-client.RenameGroupRequest() // RenameGroupRequest | 
};
apiInstance.groupsChangeName(clientId, opts, (error, data, response) => {
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
 **renameGroupRequest** | [**RenameGroupRequest**](RenameGroupRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## groupsChangeNickName

> groupsChangeNickName(clientId, opts)

修改自己在群中的昵称。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'changeGroupNickNameRequest': new wechatauto-client.ChangeGroupNickNameRequest() // ChangeGroupNickNameRequest | 
};
apiInstance.groupsChangeNickName(clientId, opts, (error, data, response) => {
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
 **changeGroupNickNameRequest** | [**ChangeGroupNickNameRequest**](ChangeGroupNickNameRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## groupsCreate

> groupsCreate(clientId, opts)

创建群聊，已存在则打开群聊。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'createGroupRequest': new wechatauto-client.CreateGroupRequest() // CreateGroupRequest | 
};
apiInstance.groupsCreate(clientId, opts, (error, data, response) => {
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
 **createGroupRequest** | [**CreateGroupRequest**](CreateGroupRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## groupsGetMembers

> groupsGetMembers(clientId, opts)

获取群聊成员列表。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'groupName': "groupName_example" // String | 
};
apiInstance.groupsGetMembers(clientId, opts, (error, data, response) => {
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
 **groupName** | **String**|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## groupsGetOwner

> groupsGetOwner(clientId, groupName)

获取群主昵称。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let groupName = "groupName_example"; // String | 
apiInstance.groupsGetOwner(clientId, groupName, (error, data, response) => {
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
 **groupName** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## groupsInvite

> groupsInvite(clientId, opts)

邀请群聊成员（适用于外部群）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'inviteGroupMemberRequest': new wechatauto-client.InviteGroupMemberRequest() // InviteGroupMemberRequest | 
};
apiInstance.groupsInvite(clientId, opts, (error, data, response) => {
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
 **inviteGroupMemberRequest** | [**InviteGroupMemberRequest**](InviteGroupMemberRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## groupsIsOwner

> groupsIsOwner(clientId, groupName)

判断群聊是否为自有群。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let groupName = "groupName_example"; // String | 
apiInstance.groupsIsOwner(clientId, groupName, (error, data, response) => {
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
 **groupName** | **String**|  | 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## groupsQuit

> groupsQuit(clientId, groupName, opts)

退出群聊。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let groupName = "groupName_example"; // String | 
let opts = {
  'clearHistory': true // Boolean | 
};
apiInstance.groupsQuit(clientId, groupName, opts, (error, data, response) => {
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
 **groupName** | **String**|  | 
 **clearHistory** | **Boolean**|  | [optional] [default to true]

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## groupsRemoveMembers

> groupsRemoveMembers(clientId, opts)

移除群聊成员（适用于自有群）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'groupMemberRequest': new wechatauto-client.GroupMemberRequest() // GroupMemberRequest | 
};
apiInstance.groupsRemoveMembers(clientId, opts, (error, data, response) => {
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
 **groupMemberRequest** | [**GroupMemberRequest**](GroupMemberRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined


## groupsUpdateNotice

> groupsUpdateNotice(clientId, opts)

更新群公告（仅适用于自有群）。

### Example

```javascript
import wechatauto-client from 'wechatauto_sdk';
let defaultClient = wechatauto-client.ApiClient.instance;
// Configure API key authorization: ApiKey
let ApiKey = defaultClient.authentications['ApiKey'];
ApiKey.apiKey = 'YOUR API KEY';
// Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
//ApiKey.apiKeyPrefix = 'Token';

let apiInstance = new wechatauto-client.GroupsApi();
let clientId = "clientId_example"; // String | 
let opts = {
  'updateGroupNoticeRequest': new wechatauto-client.UpdateGroupNoticeRequest() // UpdateGroupNoticeRequest | 
};
apiInstance.groupsUpdateNotice(clientId, opts, (error, data, response) => {
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
 **updateGroupNoticeRequest** | [**UpdateGroupNoticeRequest**](UpdateGroupNoticeRequest.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

