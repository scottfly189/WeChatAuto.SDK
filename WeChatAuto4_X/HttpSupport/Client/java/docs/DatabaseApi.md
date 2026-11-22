# DatabaseApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**databaseGetContact**](DatabaseApi.md#databaseGetContact) | **GET** /api/v1/clients/{clientId}/database/contacts/{userName} | 按 username（wxid / 群 id）取一条联系人。 |
| [**databaseGetContactByShowName**](DatabaseApi.md#databaseGetContactByShowName) | **GET** /api/v1/clients/{clientId}/database/contacts/by-show-name/first | 按显示名取第一条联系人（重名时取 UserName 升序第一条）。 |
| [**databaseGetContacts**](DatabaseApi.md#databaseGetContacts) | **GET** /api/v1/clients/{clientId}/database/contacts | 获取当前微信号全部联系人（不含已删除）。 |
| [**databaseGetContactsByShowName**](DatabaseApi.md#databaseGetContactsByShowName) | **GET** /api/v1/clients/{clientId}/database/contacts/by-show-name | 按显示名取联系人；重名时返回多个。 |
| [**databaseGetMaxSortSeq**](DatabaseApi.md#databaseGetMaxSortSeq) | **GET** /api/v1/clients/{clientId}/database/messages/max-sort-seq | 取当前微信号消息的最大 SortSeq（增量水位）。 |
| [**databaseGetMessages**](DatabaseApi.md#databaseGetMessages) | **GET** /api/v1/clients/{clientId}/database/messages | 取指定会话最近 N 条消息（从新到旧）。 |
| [**databaseGetNewMessages**](DatabaseApi.md#databaseGetNewMessages) | **GET** /api/v1/clients/{clientId}/database/messages/new | 取 SortSeq 大于指定值的新消息（从旧到新）。 |
| [**databaseGetRecentMessages**](DatabaseApi.md#databaseGetRecentMessages) | **GET** /api/v1/clients/{clientId}/database/messages/recent | 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。 |
| [**databaseGetRoomByShowName**](DatabaseApi.md#databaseGetRoomByShowName) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name/first | 按群的显示名取第一条群。 |
| [**databaseGetRoomMembers**](DatabaseApi.md#databaseGetRoomMembers) | **GET** /api/v1/clients/{clientId}/database/rooms/{roomUsername}/members | 按群 username 取成员列表。 |
| [**databaseGetRoomMembersByShowName**](DatabaseApi.md#databaseGetRoomMembersByShowName) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name/members | 按群的显示名取成员列表；重名时取第一条群。 |
| [**databaseGetRooms**](DatabaseApi.md#databaseGetRooms) | **GET** /api/v1/clients/{clientId}/database/rooms | 获取当前微信号全部群聊。 |
| [**databaseGetRoomsByLocalType**](DatabaseApi.md#databaseGetRoomsByLocalType) | **GET** /api/v1/clients/{clientId}/database/rooms/by-local-type | 按联系人 LocalType 过滤群聊（本机观察群多为 2）。 |
| [**databaseGetRoomsByShowName**](DatabaseApi.md#databaseGetRoomsByShowName) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name | 按群的显示名取群；重名时返回多个。 |
| [**databaseGetSnsPost**](DatabaseApi.md#databaseGetSnsPost) | **GET** /api/v1/clients/{clientId}/database/sns/posts/{tid} | 按 tid 取一条朋友圈（含媒体、点赞、评论）。 |
| [**databaseGetSnsPosts**](DatabaseApi.md#databaseGetSnsPosts) | **GET** /api/v1/clients/{clientId}/database/sns/posts | 取最近 N 条朋友圈（含媒体、点赞、评论）。 |
| [**databaseGetSnsPostsByDate**](DatabaseApi.md#databaseGetSnsPostsByDate) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-date | 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。 |
| [**databaseGetSnsPostsByShowName**](DatabaseApi.md#databaseGetSnsPostsByShowName) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-show-name | 按发布者显示名取朋友圈；重名会一并返回。 |
| [**databaseGetSnsPostsByUserName**](DatabaseApi.md#databaseGetSnsPostsByUserName) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-user | 取某个发布者（真实 UserName / wxid）的朋友圈。 |
| [**databaseQuery**](DatabaseApi.md#databaseQuery) | **POST** /api/v1/clients/{clientId}/database/query | 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。 |
| [**databaseSearchAllMessages**](DatabaseApi.md#databaseSearchAllMessages) | **GET** /api/v1/clients/{clientId}/database/messages/search-all | 按正文关键字跨所有微信号搜索消息。 |
| [**databaseSearchContacts**](DatabaseApi.md#databaseSearchContacts) | **GET** /api/v1/clients/{clientId}/database/contacts/search | 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。 |
| [**databaseSearchMessages**](DatabaseApi.md#databaseSearchMessages) | **GET** /api/v1/clients/{clientId}/database/messages/search | 按正文关键字搜索当前微信号消息。 |


<a id="databaseGetContact"></a>
# **databaseGetContact**
> databaseGetContact(clientId, userName)

按 username（wxid / 群 id）取一条联系人。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String userName = "userName_example"; // String | 
    try {
      apiInstance.databaseGetContact(clientId, userName);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetContact");
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
| **userName** | **String**|  | |

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

<a id="databaseGetContactByShowName"></a>
# **databaseGetContactByShowName**
> databaseGetContactByShowName(clientId, showName)

按显示名取第一条联系人（重名时取 UserName 升序第一条）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String showName = "showName_example"; // String | 
    try {
      apiInstance.databaseGetContactByShowName(clientId, showName);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetContactByShowName");
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
| **showName** | **String**|  | [optional] |

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

<a id="databaseGetContacts"></a>
# **databaseGetContacts**
> databaseGetContacts(clientId)

获取当前微信号全部联系人（不含已删除）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.databaseGetContacts(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetContacts");
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

<a id="databaseGetContactsByShowName"></a>
# **databaseGetContactsByShowName**
> databaseGetContactsByShowName(clientId, showName)

按显示名取联系人；重名时返回多个。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String showName = "showName_example"; // String | 
    try {
      apiInstance.databaseGetContactsByShowName(clientId, showName);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetContactsByShowName");
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
| **showName** | **String**|  | [optional] |

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

<a id="databaseGetMaxSortSeq"></a>
# **databaseGetMaxSortSeq**
> databaseGetMaxSortSeq(clientId)

取当前微信号消息的最大 SortSeq（增量水位）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.databaseGetMaxSortSeq(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetMaxSortSeq");
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

<a id="databaseGetMessages"></a>
# **databaseGetMessages**
> databaseGetMessages(clientId, chatUsername, count)

取指定会话最近 N 条消息（从新到旧）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String chatUsername = "chatUsername_example"; // String | 
    Integer count = 50; // Integer | 
    try {
      apiInstance.databaseGetMessages(clientId, chatUsername, count);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetMessages");
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
| **chatUsername** | **String**|  | [optional] |
| **count** | **Integer**|  | [optional] [default to 50] |

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

<a id="databaseGetNewMessages"></a>
# **databaseGetNewMessages**
> databaseGetNewMessages(clientId, afterSortSeq)

取 SortSeq 大于指定值的新消息（从旧到新）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    Long afterSortSeq = 56L; // Long | 
    try {
      apiInstance.databaseGetNewMessages(clientId, afterSortSeq);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetNewMessages");
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
| **afterSortSeq** | **Long**|  | [optional] |

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

<a id="databaseGetRecentMessages"></a>
# **databaseGetRecentMessages**
> databaseGetRecentMessages(clientId, chatUsername, count)

取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String chatUsername = "chatUsername_example"; // String | 
    Integer count = 50; // Integer | 
    try {
      apiInstance.databaseGetRecentMessages(clientId, chatUsername, count);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetRecentMessages");
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
| **chatUsername** | **String**|  | [optional] |
| **count** | **Integer**|  | [optional] [default to 50] |

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

<a id="databaseGetRoomByShowName"></a>
# **databaseGetRoomByShowName**
> databaseGetRoomByShowName(clientId, showName)

按群的显示名取第一条群。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String showName = "showName_example"; // String | 
    try {
      apiInstance.databaseGetRoomByShowName(clientId, showName);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetRoomByShowName");
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
| **showName** | **String**|  | [optional] |

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

<a id="databaseGetRoomMembers"></a>
# **databaseGetRoomMembers**
> databaseGetRoomMembers(clientId, roomUsername)

按群 username 取成员列表。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String roomUsername = "roomUsername_example"; // String | 
    try {
      apiInstance.databaseGetRoomMembers(clientId, roomUsername);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetRoomMembers");
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
| **roomUsername** | **String**|  | |

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

<a id="databaseGetRoomMembersByShowName"></a>
# **databaseGetRoomMembersByShowName**
> databaseGetRoomMembersByShowName(clientId, showName)

按群的显示名取成员列表；重名时取第一条群。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String showName = "showName_example"; // String | 
    try {
      apiInstance.databaseGetRoomMembersByShowName(clientId, showName);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetRoomMembersByShowName");
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
| **showName** | **String**|  | [optional] |

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

<a id="databaseGetRooms"></a>
# **databaseGetRooms**
> databaseGetRooms(clientId)

获取当前微信号全部群聊。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.databaseGetRooms(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetRooms");
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

<a id="databaseGetRoomsByLocalType"></a>
# **databaseGetRoomsByLocalType**
> databaseGetRoomsByLocalType(clientId, localType)

按联系人 LocalType 过滤群聊（本机观察群多为 2）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    Integer localType = 56; // Integer | 
    try {
      apiInstance.databaseGetRoomsByLocalType(clientId, localType);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetRoomsByLocalType");
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
| **localType** | **Integer**|  | [optional] |

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

<a id="databaseGetRoomsByShowName"></a>
# **databaseGetRoomsByShowName**
> databaseGetRoomsByShowName(clientId, showName)

按群的显示名取群；重名时返回多个。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String showName = "showName_example"; // String | 
    try {
      apiInstance.databaseGetRoomsByShowName(clientId, showName);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetRoomsByShowName");
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
| **showName** | **String**|  | [optional] |

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

<a id="databaseGetSnsPost"></a>
# **databaseGetSnsPost**
> databaseGetSnsPost(clientId, tid)

按 tid 取一条朋友圈（含媒体、点赞、评论）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    Long tid = 56L; // Long | 
    try {
      apiInstance.databaseGetSnsPost(clientId, tid);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetSnsPost");
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
| **tid** | **Long**|  | |

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

<a id="databaseGetSnsPosts"></a>
# **databaseGetSnsPosts**
> databaseGetSnsPosts(clientId, limit)

取最近 N 条朋友圈（含媒体、点赞、评论）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    Integer limit = 20; // Integer | 
    try {
      apiInstance.databaseGetSnsPosts(clientId, limit);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetSnsPosts");
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
| **limit** | **Integer**|  | [optional] [default to 20] |

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

<a id="databaseGetSnsPostsByDate"></a>
# **databaseGetSnsPostsByDate**
> databaseGetSnsPostsByDate(clientId, date)

取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    OffsetDateTime date = OffsetDateTime.now(); // OffsetDateTime | 
    try {
      apiInstance.databaseGetSnsPostsByDate(clientId, date);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetSnsPostsByDate");
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
| **date** | **OffsetDateTime**|  | [optional] |

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

<a id="databaseGetSnsPostsByShowName"></a>
# **databaseGetSnsPostsByShowName**
> databaseGetSnsPostsByShowName(clientId, showName)

按发布者显示名取朋友圈；重名会一并返回。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String showName = "showName_example"; // String | 
    try {
      apiInstance.databaseGetSnsPostsByShowName(clientId, showName);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetSnsPostsByShowName");
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
| **showName** | **String**|  | [optional] |

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

<a id="databaseGetSnsPostsByUserName"></a>
# **databaseGetSnsPostsByUserName**
> databaseGetSnsPostsByUserName(clientId, userName)

取某个发布者（真实 UserName / wxid）的朋友圈。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String userName = "userName_example"; // String | 
    try {
      apiInstance.databaseGetSnsPostsByUserName(clientId, userName);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseGetSnsPostsByUserName");
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
| **userName** | **String**|  | [optional] |

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

<a id="databaseQuery"></a>
# **databaseQuery**
> databaseQuery(clientId, databaseQueryRequest)

执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    DatabaseQueryRequest databaseQueryRequest = new DatabaseQueryRequest(); // DatabaseQueryRequest | 
    try {
      apiInstance.databaseQuery(clientId, databaseQueryRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseQuery");
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
| **databaseQueryRequest** | [**DatabaseQueryRequest**](DatabaseQueryRequest.md)|  | [optional] |

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

<a id="databaseSearchAllMessages"></a>
# **databaseSearchAllMessages**
> databaseSearchAllMessages(clientId, keyword, chatUsername)

按正文关键字跨所有微信号搜索消息。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String keyword = "keyword_example"; // String | 
    String chatUsername = "chatUsername_example"; // String | 
    try {
      apiInstance.databaseSearchAllMessages(clientId, keyword, chatUsername);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseSearchAllMessages");
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
| **keyword** | **String**|  | [optional] |
| **chatUsername** | **String**|  | [optional] |

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

<a id="databaseSearchContacts"></a>
# **databaseSearchContacts**
> databaseSearchContacts(clientId, keyword)

按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String keyword = "keyword_example"; // String | 
    try {
      apiInstance.databaseSearchContacts(clientId, keyword);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseSearchContacts");
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
| **keyword** | **String**|  | [optional] |

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

<a id="databaseSearchMessages"></a>
# **databaseSearchMessages**
> databaseSearchMessages(clientId, keyword, chatUsername)

按正文关键字搜索当前微信号消息。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.DatabaseApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    DatabaseApi apiInstance = new DatabaseApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String keyword = "keyword_example"; // String | 
    String chatUsername = "chatUsername_example"; // String | 
    try {
      apiInstance.databaseSearchMessages(clientId, keyword, chatUsername);
    } catch (ApiException e) {
      System.err.println("Exception when calling DatabaseApi#databaseSearchMessages");
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
| **keyword** | **String**|  | [optional] |
| **chatUsername** | **String**|  | [optional] |

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

