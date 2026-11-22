# GroupsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**groupsAddMembers**](GroupsApi.md#groupsAddMembers) | **POST** /api/v1/clients/{clientId}/groups/members | 添加群聊成员（适用于自有群）。 |
| [**groupsAddMembersToFriends**](GroupsApi.md#groupsAddMembersToFriends) | **POST** /api/v1/clients/{clientId}/groups/add-members-to-friends | 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。 |
| [**groupsChangeMemo**](GroupsApi.md#groupsChangeMemo) | **PUT** /api/v1/clients/{clientId}/groups/memo | 修改群备注（仅自己可见）。 |
| [**groupsChangeName**](GroupsApi.md#groupsChangeName) | **PUT** /api/v1/clients/{clientId}/groups/name | 修改群名（适用于自有群）。 |
| [**groupsChangeNickName**](GroupsApi.md#groupsChangeNickName) | **PUT** /api/v1/clients/{clientId}/groups/nickname | 修改自己在群中的昵称。 |
| [**groupsCreate**](GroupsApi.md#groupsCreate) | **POST** /api/v1/clients/{clientId}/groups | 创建群聊，已存在则打开群聊。 |
| [**groupsGetMembers**](GroupsApi.md#groupsGetMembers) | **GET** /api/v1/clients/{clientId}/groups/members | 获取群聊成员列表。 |
| [**groupsGetOwner**](GroupsApi.md#groupsGetOwner) | **GET** /api/v1/clients/{clientId}/groups/{groupName}/owner | 获取群主昵称。 |
| [**groupsInvite**](GroupsApi.md#groupsInvite) | **POST** /api/v1/clients/{clientId}/groups/invite | 邀请群聊成员（适用于外部群）。 |
| [**groupsIsOwner**](GroupsApi.md#groupsIsOwner) | **GET** /api/v1/clients/{clientId}/groups/{groupName}/is-owner | 判断群聊是否为自有群。 |
| [**groupsQuit**](GroupsApi.md#groupsQuit) | **DELETE** /api/v1/clients/{clientId}/groups/{groupName} | 退出群聊。 |
| [**groupsRemoveMembers**](GroupsApi.md#groupsRemoveMembers) | **DELETE** /api/v1/clients/{clientId}/groups/members | 移除群聊成员（适用于自有群）。 |
| [**groupsUpdateNotice**](GroupsApi.md#groupsUpdateNotice) | **PUT** /api/v1/clients/{clientId}/groups/notice | 更新群公告（仅适用于自有群）。 |


<a id="groupsAddMembers"></a>
# **groupsAddMembers**
> groupsAddMembers(clientId, groupMemberRequest)

添加群聊成员（适用于自有群）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    GroupMemberRequest groupMemberRequest = new GroupMemberRequest(); // GroupMemberRequest | 
    try {
      apiInstance.groupsAddMembers(clientId, groupMemberRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsAddMembers");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **groupMemberRequest** | [**GroupMemberRequest**](GroupMemberRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsAddMembersToFriends"></a>
# **groupsAddMembersToFriends**
> groupsAddMembersToFriends(clientId, addGroupMemberToFriendsRequest)

把群聊成员加为自己的好友（适用于外部群，注意微信风控）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    AddGroupMemberToFriendsRequest addGroupMemberToFriendsRequest = new AddGroupMemberToFriendsRequest(); // AddGroupMemberToFriendsRequest | 
    try {
      apiInstance.groupsAddMembersToFriends(clientId, addGroupMemberToFriendsRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsAddMembersToFriends");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **addGroupMemberToFriendsRequest** | [**AddGroupMemberToFriendsRequest**](AddGroupMemberToFriendsRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsChangeMemo"></a>
# **groupsChangeMemo**
> groupsChangeMemo(clientId, changeGroupMemoRequest)

修改群备注（仅自己可见）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    ChangeGroupMemoRequest changeGroupMemoRequest = new ChangeGroupMemoRequest(); // ChangeGroupMemoRequest | 
    try {
      apiInstance.groupsChangeMemo(clientId, changeGroupMemoRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsChangeMemo");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **changeGroupMemoRequest** | [**ChangeGroupMemoRequest**](ChangeGroupMemoRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsChangeName"></a>
# **groupsChangeName**
> groupsChangeName(clientId, renameGroupRequest)

修改群名（适用于自有群）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    RenameGroupRequest renameGroupRequest = new RenameGroupRequest(); // RenameGroupRequest | 
    try {
      apiInstance.groupsChangeName(clientId, renameGroupRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsChangeName");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **renameGroupRequest** | [**RenameGroupRequest**](RenameGroupRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsChangeNickName"></a>
# **groupsChangeNickName**
> groupsChangeNickName(clientId, changeGroupNickNameRequest)

修改自己在群中的昵称。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    ChangeGroupNickNameRequest changeGroupNickNameRequest = new ChangeGroupNickNameRequest(); // ChangeGroupNickNameRequest | 
    try {
      apiInstance.groupsChangeNickName(clientId, changeGroupNickNameRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsChangeNickName");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **changeGroupNickNameRequest** | [**ChangeGroupNickNameRequest**](ChangeGroupNickNameRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsCreate"></a>
# **groupsCreate**
> groupsCreate(clientId, createGroupRequest)

创建群聊，已存在则打开群聊。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    CreateGroupRequest createGroupRequest = new CreateGroupRequest(); // CreateGroupRequest | 
    try {
      apiInstance.groupsCreate(clientId, createGroupRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsCreate");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **createGroupRequest** | [**CreateGroupRequest**](CreateGroupRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsGetMembers"></a>
# **groupsGetMembers**
> groupsGetMembers(clientId, groupName)

获取群聊成员列表。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String groupName = "groupName_example"; // String | 
    try {
      apiInstance.groupsGetMembers(clientId, groupName);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsGetMembers");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **groupName** | **String**|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsGetOwner"></a>
# **groupsGetOwner**
> groupsGetOwner(clientId, groupName)

获取群主昵称。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String groupName = "groupName_example"; // String | 
    try {
      apiInstance.groupsGetOwner(clientId, groupName);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsGetOwner");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **groupName** | **String**|  | |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsInvite"></a>
# **groupsInvite**
> groupsInvite(clientId, inviteGroupMemberRequest)

邀请群聊成员（适用于外部群）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    InviteGroupMemberRequest inviteGroupMemberRequest = new InviteGroupMemberRequest(); // InviteGroupMemberRequest | 
    try {
      apiInstance.groupsInvite(clientId, inviteGroupMemberRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsInvite");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **inviteGroupMemberRequest** | [**InviteGroupMemberRequest**](InviteGroupMemberRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsIsOwner"></a>
# **groupsIsOwner**
> groupsIsOwner(clientId, groupName)

判断群聊是否为自有群。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String groupName = "groupName_example"; // String | 
    try {
      apiInstance.groupsIsOwner(clientId, groupName);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsIsOwner");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **groupName** | **String**|  | |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsQuit"></a>
# **groupsQuit**
> groupsQuit(clientId, groupName, clearHistory)

退出群聊。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String groupName = "groupName_example"; // String | 
    Boolean clearHistory = true; // Boolean | 
    try {
      apiInstance.groupsQuit(clientId, groupName, clearHistory);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsQuit");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **groupName** | **String**|  | |
| **clearHistory** | **Boolean**|  | [optional] [default to true] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsRemoveMembers"></a>
# **groupsRemoveMembers**
> groupsRemoveMembers(clientId, groupMemberRequest)

移除群聊成员（适用于自有群）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    GroupMemberRequest groupMemberRequest = new GroupMemberRequest(); // GroupMemberRequest | 
    try {
      apiInstance.groupsRemoveMembers(clientId, groupMemberRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsRemoveMembers");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **groupMemberRequest** | [**GroupMemberRequest**](GroupMemberRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

<a id="groupsUpdateNotice"></a>
# **groupsUpdateNotice**
> groupsUpdateNotice(clientId, updateGroupNoticeRequest)

更新群公告（仅适用于自有群）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.GroupsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    GroupsApi apiInstance = new GroupsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    UpdateGroupNoticeRequest updateGroupNoticeRequest = new UpdateGroupNoticeRequest(); // UpdateGroupNoticeRequest | 
    try {
      apiInstance.groupsUpdateNotice(clientId, updateGroupNoticeRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling GroupsApi#groupsUpdateNotice");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters

| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **clientId** | **String**|  | |
| **updateGroupNoticeRequest** | [**UpdateGroupNoticeRequest**](UpdateGroupNoticeRequest.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

