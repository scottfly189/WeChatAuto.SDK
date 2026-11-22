# FriendsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**friendsAdd**](FriendsApi.md#friendsAdd) | **POST** /api/v1/clients/{clientId}/friends/add | 通过手机号码/微信号查找并添加好友。 |
| [**friendsAddOrUpdateFriend**](FriendsApi.md#friendsAddOrUpdateFriend) | **POST** /api/v1/clients/{clientId}/friends/cache | 手动新增或修改缓存中的好友对象。 |
| [**friendsCloseAddWindow**](FriendsApi.md#friendsCloseAddWindow) | **POST** /api/v1/clients/{clientId}/friends/add-window/close | 关闭新增朋友窗口。 |
| [**friendsGetAllFriendNames**](FriendsApi.md#friendsGetAllFriendNames) | **GET** /api/v1/clients/{clientId}/friends/names | 获取所有好友名称列表（有备注优先备注名）。 |
| [**friendsGetAllFriends**](FriendsApi.md#friendsGetAllFriends) | **GET** /api/v1/clients/{clientId}/friends | 获取所有好友的信息列表（默认优先从缓存读取）。 |
| [**friendsGetFriendByWxidFromCache**](FriendsApi.md#friendsGetFriendByWxidFromCache) | **GET** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存获取好友信息。 |
| [**friendsGetFriendFromCache**](FriendsApi.md#friendsGetFriendFromCache) | **GET** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存获取单个好友信息。 |
| [**friendsGetFriendListFromCache**](FriendsApi.md#friendsGetFriendListFromCache) | **GET** /api/v1/clients/{clientId}/friends/cache | 获取缓存中的全部好友信息列表。 |
| [**friendsGetFriendsFromCache**](FriendsApi.md#friendsGetFriendsFromCache) | **GET** /api/v1/clients/{clientId}/friends/cache/by-name/{who} | 按名称从缓存获取好友列表（适用于昵称重复场景）。 |
| [**friendsOpenAddWindow**](FriendsApi.md#friendsOpenAddWindow) | **POST** /api/v1/clients/{clientId}/friends/add-window/open | 打开新增朋友窗口。 |
| [**friendsPassAll**](FriendsApi.md#friendsPassAll) | **POST** /api/v1/clients/{clientId}/friends/pass-all | 立即批量通过当前待处理的新好友申请（不启动持续监听）。 |
| [**friendsPauseAutoAccept**](FriendsApi.md#friendsPauseAutoAccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/pause | 暂停新好友自动通过监听。 |
| [**friendsRemoveFriend**](FriendsApi.md#friendsRemoveFriend) | **DELETE** /api/v1/clients/{clientId}/friends/{nickName} | 从通讯录移除好友。 |
| [**friendsRemoveFriendByWxidFromCache**](FriendsApi.md#friendsRemoveFriendByWxidFromCache) | **DELETE** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存移除好友。 |
| [**friendsRemoveFriendFromCache**](FriendsApi.md#friendsRemoveFriendFromCache) | **DELETE** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存移除好友。 |
| [**friendsResumeAutoAccept**](FriendsApi.md#friendsResumeAutoAccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/resume | 恢复新好友自动通过监听。 |
| [**friendsStartAutoAccept**](FriendsApi.md#friendsStartAutoAccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/start | 启动新好友自动通过监听（按关键词/后缀/标签规则）。 |
| [**friendsStopAutoAccept**](FriendsApi.md#friendsStopAutoAccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/stop | 停止新好友自动通过监听。 |


<a id="friendsAdd"></a>
# **friendsAdd**
> friendsAdd(clientId, addFriendsRequest)

通过手机号码/微信号查找并添加好友。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    AddFriendsRequest addFriendsRequest = new AddFriendsRequest(); // AddFriendsRequest | 
    try {
      apiInstance.friendsAdd(clientId, addFriendsRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsAdd");
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
| **addFriendsRequest** | [**AddFriendsRequest**](AddFriendsRequest.md)|  | [optional] |

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

<a id="friendsAddOrUpdateFriend"></a>
# **friendsAddOrUpdateFriend**
> friendsAddOrUpdateFriend(clientId, friendInfo)

手动新增或修改缓存中的好友对象。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    FriendInfo friendInfo = new FriendInfo(); // FriendInfo | 
    try {
      apiInstance.friendsAddOrUpdateFriend(clientId, friendInfo);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsAddOrUpdateFriend");
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
| **friendInfo** | [**FriendInfo**](FriendInfo.md)|  | [optional] |

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

<a id="friendsCloseAddWindow"></a>
# **friendsCloseAddWindow**
> friendsCloseAddWindow(clientId)

关闭新增朋友窗口。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.friendsCloseAddWindow(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsCloseAddWindow");
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

<a id="friendsGetAllFriendNames"></a>
# **friendsGetAllFriendNames**
> friendsGetAllFriendNames(clientId)

获取所有好友名称列表（有备注优先备注名）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.friendsGetAllFriendNames(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsGetAllFriendNames");
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

<a id="friendsGetAllFriends"></a>
# **friendsGetAllFriends**
> friendsGetAllFriends(clientId, fromCache)

获取所有好友的信息列表（默认优先从缓存读取）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    Boolean fromCache = true; // Boolean | 
    try {
      apiInstance.friendsGetAllFriends(clientId, fromCache);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsGetAllFriends");
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
| **fromCache** | **Boolean**|  | [optional] [default to true] |

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

<a id="friendsGetFriendByWxidFromCache"></a>
# **friendsGetFriendByWxidFromCache**
> friendsGetFriendByWxidFromCache(clientId, wxid)

按 wxid 从缓存获取好友信息。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String wxid = "wxid_example"; // String | 
    try {
      apiInstance.friendsGetFriendByWxidFromCache(clientId, wxid);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsGetFriendByWxidFromCache");
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
| **wxid** | **String**|  | |

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

<a id="friendsGetFriendFromCache"></a>
# **friendsGetFriendFromCache**
> friendsGetFriendFromCache(clientId, who)

按名称从缓存获取单个好友信息。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String who = "who_example"; // String | 
    try {
      apiInstance.friendsGetFriendFromCache(clientId, who);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsGetFriendFromCache");
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
| **who** | **String**|  | |

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

<a id="friendsGetFriendListFromCache"></a>
# **friendsGetFriendListFromCache**
> friendsGetFriendListFromCache(clientId)

获取缓存中的全部好友信息列表。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.friendsGetFriendListFromCache(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsGetFriendListFromCache");
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

<a id="friendsGetFriendsFromCache"></a>
# **friendsGetFriendsFromCache**
> friendsGetFriendsFromCache(clientId, who)

按名称从缓存获取好友列表（适用于昵称重复场景）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String who = "who_example"; // String | 
    try {
      apiInstance.friendsGetFriendsFromCache(clientId, who);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsGetFriendsFromCache");
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
| **who** | **String**|  | |

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

<a id="friendsOpenAddWindow"></a>
# **friendsOpenAddWindow**
> friendsOpenAddWindow(clientId)

打开新增朋友窗口。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.friendsOpenAddWindow(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsOpenAddWindow");
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

<a id="friendsPassAll"></a>
# **friendsPassAll**
> friendsPassAll(clientId, passedAllNewFriendRequest)

立即批量通过当前待处理的新好友申请（不启动持续监听）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    PassedAllNewFriendRequest passedAllNewFriendRequest = new PassedAllNewFriendRequest(); // PassedAllNewFriendRequest | 
    try {
      apiInstance.friendsPassAll(clientId, passedAllNewFriendRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsPassAll");
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
| **passedAllNewFriendRequest** | [**PassedAllNewFriendRequest**](PassedAllNewFriendRequest.md)|  | [optional] |

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

<a id="friendsPauseAutoAccept"></a>
# **friendsPauseAutoAccept**
> friendsPauseAutoAccept(clientId)

暂停新好友自动通过监听。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.friendsPauseAutoAccept(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsPauseAutoAccept");
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

<a id="friendsRemoveFriend"></a>
# **friendsRemoveFriend**
> friendsRemoveFriend(clientId, nickName)

从通讯录移除好友。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String nickName = "nickName_example"; // String | 
    try {
      apiInstance.friendsRemoveFriend(clientId, nickName);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsRemoveFriend");
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
| **nickName** | **String**|  | |

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

<a id="friendsRemoveFriendByWxidFromCache"></a>
# **friendsRemoveFriendByWxidFromCache**
> friendsRemoveFriendByWxidFromCache(clientId, wxid)

按 wxid 从缓存移除好友。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String wxid = "wxid_example"; // String | 
    try {
      apiInstance.friendsRemoveFriendByWxidFromCache(clientId, wxid);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsRemoveFriendByWxidFromCache");
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
| **wxid** | **String**|  | |

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

<a id="friendsRemoveFriendFromCache"></a>
# **friendsRemoveFriendFromCache**
> friendsRemoveFriendFromCache(clientId, who)

按名称从缓存移除好友。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    String who = "who_example"; // String | 
    try {
      apiInstance.friendsRemoveFriendFromCache(clientId, who);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsRemoveFriendFromCache");
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
| **who** | **String**|  | |

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

<a id="friendsResumeAutoAccept"></a>
# **friendsResumeAutoAccept**
> friendsResumeAutoAccept(clientId)

恢复新好友自动通过监听。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.friendsResumeAutoAccept(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsResumeAutoAccept");
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

<a id="friendsStartAutoAccept"></a>
# **friendsStartAutoAccept**
> friendsStartAutoAccept(clientId, passedAllNewFriendRequest)

启动新好友自动通过监听（按关键词/后缀/标签规则）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    PassedAllNewFriendRequest passedAllNewFriendRequest = new PassedAllNewFriendRequest(); // PassedAllNewFriendRequest | 
    try {
      apiInstance.friendsStartAutoAccept(clientId, passedAllNewFriendRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsStartAutoAccept");
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
| **passedAllNewFriendRequest** | [**PassedAllNewFriendRequest**](PassedAllNewFriendRequest.md)|  | [optional] |

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

<a id="friendsStopAutoAccept"></a>
# **friendsStopAutoAccept**
> friendsStopAutoAccept(clientId)

停止新好友自动通过监听。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.FriendsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    FriendsApi apiInstance = new FriendsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.friendsStopAutoAccept(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling FriendsApi#friendsStopAutoAccept");
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

