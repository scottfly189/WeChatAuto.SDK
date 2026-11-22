# WeChatAuto.Client.Api.GroupsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GroupsAddMembers**](GroupsApi.md#groupsaddmembers) | **POST** /api/v1/clients/{clientId}/groups/members | 添加群聊成员（适用于自有群）。 |
| [**GroupsAddMembersToFriends**](GroupsApi.md#groupsaddmemberstofriends) | **POST** /api/v1/clients/{clientId}/groups/add-members-to-friends | 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。 |
| [**GroupsChangeMemo**](GroupsApi.md#groupschangememo) | **PUT** /api/v1/clients/{clientId}/groups/memo | 修改群备注（仅自己可见）。 |
| [**GroupsChangeName**](GroupsApi.md#groupschangename) | **PUT** /api/v1/clients/{clientId}/groups/name | 修改群名（适用于自有群）。 |
| [**GroupsChangeNickName**](GroupsApi.md#groupschangenickname) | **PUT** /api/v1/clients/{clientId}/groups/nickname | 修改自己在群中的昵称。 |
| [**GroupsCreate**](GroupsApi.md#groupscreate) | **POST** /api/v1/clients/{clientId}/groups | 创建群聊，已存在则打开群聊。 |
| [**GroupsGetMembers**](GroupsApi.md#groupsgetmembers) | **GET** /api/v1/clients/{clientId}/groups/members | 获取群聊成员列表。 |
| [**GroupsGetOwner**](GroupsApi.md#groupsgetowner) | **GET** /api/v1/clients/{clientId}/groups/{groupName}/owner | 获取群主昵称。 |
| [**GroupsInvite**](GroupsApi.md#groupsinvite) | **POST** /api/v1/clients/{clientId}/groups/invite | 邀请群聊成员（适用于外部群）。 |
| [**GroupsIsOwner**](GroupsApi.md#groupsisowner) | **GET** /api/v1/clients/{clientId}/groups/{groupName}/is-owner | 判断群聊是否为自有群。 |
| [**GroupsQuit**](GroupsApi.md#groupsquit) | **DELETE** /api/v1/clients/{clientId}/groups/{groupName} | 退出群聊。 |
| [**GroupsRemoveMembers**](GroupsApi.md#groupsremovemembers) | **DELETE** /api/v1/clients/{clientId}/groups/members | 移除群聊成员（适用于自有群）。 |
| [**GroupsUpdateNotice**](GroupsApi.md#groupsupdatenotice) | **PUT** /api/v1/clients/{clientId}/groups/notice | 更新群公告（仅适用于自有群）。 |

<a id="groupsaddmembers"></a>
# **GroupsAddMembers**
> void GroupsAddMembers (string clientId, GroupMemberRequest? groupMemberRequest = null)

添加群聊成员（适用于自有群）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsAddMembersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var groupMemberRequest = new GroupMemberRequest?(); // GroupMemberRequest? |  (optional) 

            try
            {
                // 添加群聊成员（适用于自有群）。
                apiInstance.GroupsAddMembers(clientId, groupMemberRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsAddMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsAddMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 添加群聊成员（适用于自有群）。
    apiInstance.GroupsAddMembersWithHttpInfo(clientId, groupMemberRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsAddMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **groupMemberRequest** | [**GroupMemberRequest?**](GroupMemberRequest?.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsaddmemberstofriends"></a>
# **GroupsAddMembersToFriends**
> void GroupsAddMembersToFriends (string clientId, AddGroupMemberToFriendsRequest? addGroupMemberToFriendsRequest = null)

把群聊成员加为自己的好友（适用于外部群，注意微信风控）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsAddMembersToFriendsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var addGroupMemberToFriendsRequest = new AddGroupMemberToFriendsRequest?(); // AddGroupMemberToFriendsRequest? |  (optional) 

            try
            {
                // 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。
                apiInstance.GroupsAddMembersToFriends(clientId, addGroupMemberToFriendsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsAddMembersToFriends: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsAddMembersToFriendsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。
    apiInstance.GroupsAddMembersToFriendsWithHttpInfo(clientId, addGroupMemberToFriendsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsAddMembersToFriendsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **addGroupMemberToFriendsRequest** | [**AddGroupMemberToFriendsRequest?**](AddGroupMemberToFriendsRequest?.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupschangememo"></a>
# **GroupsChangeMemo**
> void GroupsChangeMemo (string clientId, ChangeGroupMemoRequest? changeGroupMemoRequest = null)

修改群备注（仅自己可见）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsChangeMemoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var changeGroupMemoRequest = new ChangeGroupMemoRequest?(); // ChangeGroupMemoRequest? |  (optional) 

            try
            {
                // 修改群备注（仅自己可见）。
                apiInstance.GroupsChangeMemo(clientId, changeGroupMemoRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsChangeMemo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsChangeMemoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 修改群备注（仅自己可见）。
    apiInstance.GroupsChangeMemoWithHttpInfo(clientId, changeGroupMemoRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsChangeMemoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **changeGroupMemoRequest** | [**ChangeGroupMemoRequest?**](ChangeGroupMemoRequest?.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupschangename"></a>
# **GroupsChangeName**
> void GroupsChangeName (string clientId, RenameGroupRequest? renameGroupRequest = null)

修改群名（适用于自有群）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsChangeNameExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var renameGroupRequest = new RenameGroupRequest?(); // RenameGroupRequest? |  (optional) 

            try
            {
                // 修改群名（适用于自有群）。
                apiInstance.GroupsChangeName(clientId, renameGroupRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsChangeName: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsChangeNameWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 修改群名（适用于自有群）。
    apiInstance.GroupsChangeNameWithHttpInfo(clientId, renameGroupRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsChangeNameWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **renameGroupRequest** | [**RenameGroupRequest?**](RenameGroupRequest?.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupschangenickname"></a>
# **GroupsChangeNickName**
> void GroupsChangeNickName (string clientId, ChangeGroupNickNameRequest? changeGroupNickNameRequest = null)

修改自己在群中的昵称。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsChangeNickNameExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var changeGroupNickNameRequest = new ChangeGroupNickNameRequest?(); // ChangeGroupNickNameRequest? |  (optional) 

            try
            {
                // 修改自己在群中的昵称。
                apiInstance.GroupsChangeNickName(clientId, changeGroupNickNameRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsChangeNickName: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsChangeNickNameWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 修改自己在群中的昵称。
    apiInstance.GroupsChangeNickNameWithHttpInfo(clientId, changeGroupNickNameRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsChangeNickNameWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **changeGroupNickNameRequest** | [**ChangeGroupNickNameRequest?**](ChangeGroupNickNameRequest?.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupscreate"></a>
# **GroupsCreate**
> void GroupsCreate (string clientId, CreateGroupRequest? createGroupRequest = null)

创建群聊，已存在则打开群聊。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var createGroupRequest = new CreateGroupRequest?(); // CreateGroupRequest? |  (optional) 

            try
            {
                // 创建群聊，已存在则打开群聊。
                apiInstance.GroupsCreate(clientId, createGroupRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 创建群聊，已存在则打开群聊。
    apiInstance.GroupsCreateWithHttpInfo(clientId, createGroupRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **createGroupRequest** | [**CreateGroupRequest?**](CreateGroupRequest?.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsgetmembers"></a>
# **GroupsGetMembers**
> void GroupsGetMembers (string clientId, string? groupName = null)

获取群聊成员列表。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsGetMembersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var groupName = "groupName_example";  // string? |  (optional) 

            try
            {
                // 获取群聊成员列表。
                apiInstance.GroupsGetMembers(clientId, groupName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsGetMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsGetMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取群聊成员列表。
    apiInstance.GroupsGetMembersWithHttpInfo(clientId, groupName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsGetMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **groupName** | **string?** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsgetowner"></a>
# **GroupsGetOwner**
> void GroupsGetOwner (string clientId, string groupName)

获取群主昵称。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsGetOwnerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var groupName = "groupName_example";  // string | 

            try
            {
                // 获取群主昵称。
                apiInstance.GroupsGetOwner(clientId, groupName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsGetOwner: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsGetOwnerWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取群主昵称。
    apiInstance.GroupsGetOwnerWithHttpInfo(clientId, groupName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsGetOwnerWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **groupName** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsinvite"></a>
# **GroupsInvite**
> void GroupsInvite (string clientId, InviteGroupMemberRequest? inviteGroupMemberRequest = null)

邀请群聊成员（适用于外部群）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsInviteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var inviteGroupMemberRequest = new InviteGroupMemberRequest?(); // InviteGroupMemberRequest? |  (optional) 

            try
            {
                // 邀请群聊成员（适用于外部群）。
                apiInstance.GroupsInvite(clientId, inviteGroupMemberRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsInvite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsInviteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 邀请群聊成员（适用于外部群）。
    apiInstance.GroupsInviteWithHttpInfo(clientId, inviteGroupMemberRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsInviteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **inviteGroupMemberRequest** | [**InviteGroupMemberRequest?**](InviteGroupMemberRequest?.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsisowner"></a>
# **GroupsIsOwner**
> void GroupsIsOwner (string clientId, string groupName)

判断群聊是否为自有群。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsIsOwnerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var groupName = "groupName_example";  // string | 

            try
            {
                // 判断群聊是否为自有群。
                apiInstance.GroupsIsOwner(clientId, groupName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsIsOwner: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsIsOwnerWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 判断群聊是否为自有群。
    apiInstance.GroupsIsOwnerWithHttpInfo(clientId, groupName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsIsOwnerWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **groupName** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsquit"></a>
# **GroupsQuit**
> void GroupsQuit (string clientId, string groupName, bool? clearHistory = null)

退出群聊。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsQuitExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var groupName = "groupName_example";  // string | 
            var clearHistory = true;  // bool? |  (optional)  (default to true)

            try
            {
                // 退出群聊。
                apiInstance.GroupsQuit(clientId, groupName, clearHistory);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsQuit: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsQuitWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 退出群聊。
    apiInstance.GroupsQuitWithHttpInfo(clientId, groupName, clearHistory);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsQuitWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **groupName** | **string** |  |  |
| **clearHistory** | **bool?** |  | [optional] [default to true] |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsremovemembers"></a>
# **GroupsRemoveMembers**
> void GroupsRemoveMembers (string clientId, GroupMemberRequest? groupMemberRequest = null)

移除群聊成员（适用于自有群）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsRemoveMembersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var groupMemberRequest = new GroupMemberRequest?(); // GroupMemberRequest? |  (optional) 

            try
            {
                // 移除群聊成员（适用于自有群）。
                apiInstance.GroupsRemoveMembers(clientId, groupMemberRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsRemoveMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsRemoveMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 移除群聊成员（适用于自有群）。
    apiInstance.GroupsRemoveMembersWithHttpInfo(clientId, groupMemberRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsRemoveMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **groupMemberRequest** | [**GroupMemberRequest?**](GroupMemberRequest?.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsupdatenotice"></a>
# **GroupsUpdateNotice**
> void GroupsUpdateNotice (string clientId, UpdateGroupNoticeRequest? updateGroupNoticeRequest = null)

更新群公告（仅适用于自有群）。

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace Example
{
    public class GroupsUpdateNoticeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: ApiKey
            config.AddApiKey("X-API-Key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("X-API-Key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GroupsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var updateGroupNoticeRequest = new UpdateGroupNoticeRequest?(); // UpdateGroupNoticeRequest? |  (optional) 

            try
            {
                // 更新群公告（仅适用于自有群）。
                apiInstance.GroupsUpdateNotice(clientId, updateGroupNoticeRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsUpdateNotice: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsUpdateNoticeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 更新群公告（仅适用于自有群）。
    apiInstance.GroupsUpdateNoticeWithHttpInfo(clientId, updateGroupNoticeRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsUpdateNoticeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **updateGroupNoticeRequest** | [**UpdateGroupNoticeRequest?**](UpdateGroupNoticeRequest?.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

