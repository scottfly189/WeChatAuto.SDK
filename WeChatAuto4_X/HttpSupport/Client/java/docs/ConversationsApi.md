# ConversationsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**conversationsGetAllConversations**](ConversationsApi.md#conversationsGetAllConversations) | **GET** /api/v1/clients/{clientId}/conversations | 获取会话列表所有会话的标题。 |
| [**conversationsGetVisibleConversations**](ConversationsApi.md#conversationsGetVisibleConversations) | **GET** /api/v1/clients/{clientId}/conversations/visible | 获取可见会话列表（含名称、未读消息数、头像等）。 |
| [**conversationsGetVisibleTitles**](ConversationsApi.md#conversationsGetVisibleTitles) | **GET** /api/v1/clients/{clientId}/conversations/visible-titles | 获取会话列表可见会话的标题。 |
| [**conversationsLocate**](ConversationsApi.md#conversationsLocate) | **POST** /api/v1/clients/{clientId}/conversations/locate | 定位会话，滚动会话列表使指定会话可见。 |
| [**conversationsSearch**](ConversationsApi.md#conversationsSearch) | **POST** /api/v1/clients/{clientId}/conversations/search | 搜索好友/群聊。 |
| [**conversationsSetDoNotDisturb**](ConversationsApi.md#conversationsSetDoNotDisturb) | **POST** /api/v1/clients/{clientId}/conversations/set-do-not-disturb | 设置会话消息免打扰。 |
| [**conversationsSetTopMost**](ConversationsApi.md#conversationsSetTopMost) | **POST** /api/v1/clients/{clientId}/conversations/set-top-most | 设置会话置顶。 |


<a id="conversationsGetAllConversations"></a>
# **conversationsGetAllConversations**
> conversationsGetAllConversations(clientId)

获取会话列表所有会话的标题。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ConversationsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ConversationsApi apiInstance = new ConversationsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.conversationsGetAllConversations(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling ConversationsApi#conversationsGetAllConversations");
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

<a id="conversationsGetVisibleConversations"></a>
# **conversationsGetVisibleConversations**
> conversationsGetVisibleConversations(clientId)

获取可见会话列表（含名称、未读消息数、头像等）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ConversationsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ConversationsApi apiInstance = new ConversationsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.conversationsGetVisibleConversations(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling ConversationsApi#conversationsGetVisibleConversations");
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

<a id="conversationsGetVisibleTitles"></a>
# **conversationsGetVisibleTitles**
> conversationsGetVisibleTitles(clientId)

获取会话列表可见会话的标题。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ConversationsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ConversationsApi apiInstance = new ConversationsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.conversationsGetVisibleTitles(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling ConversationsApi#conversationsGetVisibleTitles");
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

<a id="conversationsLocate"></a>
# **conversationsLocate**
> conversationsLocate(clientId, locateConversationRequest)

定位会话，滚动会话列表使指定会话可见。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ConversationsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ConversationsApi apiInstance = new ConversationsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    LocateConversationRequest locateConversationRequest = new LocateConversationRequest(); // LocateConversationRequest | 
    try {
      apiInstance.conversationsLocate(clientId, locateConversationRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling ConversationsApi#conversationsLocate");
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
| **locateConversationRequest** | [**LocateConversationRequest**](LocateConversationRequest.md)|  | [optional] |

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

<a id="conversationsSearch"></a>
# **conversationsSearch**
> conversationsSearch(clientId, searchConversationRequest)

搜索好友/群聊。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ConversationsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ConversationsApi apiInstance = new ConversationsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SearchConversationRequest searchConversationRequest = new SearchConversationRequest(); // SearchConversationRequest | 
    try {
      apiInstance.conversationsSearch(clientId, searchConversationRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling ConversationsApi#conversationsSearch");
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
| **searchConversationRequest** | [**SearchConversationRequest**](SearchConversationRequest.md)|  | [optional] |

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

<a id="conversationsSetDoNotDisturb"></a>
# **conversationsSetDoNotDisturb**
> conversationsSetDoNotDisturb(clientId, setDoNotDisturbRequest)

设置会话消息免打扰。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ConversationsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ConversationsApi apiInstance = new ConversationsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SetDoNotDisturbRequest setDoNotDisturbRequest = new SetDoNotDisturbRequest(); // SetDoNotDisturbRequest | 
    try {
      apiInstance.conversationsSetDoNotDisturb(clientId, setDoNotDisturbRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling ConversationsApi#conversationsSetDoNotDisturb");
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
| **setDoNotDisturbRequest** | [**SetDoNotDisturbRequest**](SetDoNotDisturbRequest.md)|  | [optional] |

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

<a id="conversationsSetTopMost"></a>
# **conversationsSetTopMost**
> conversationsSetTopMost(clientId, setTopMostRequest)

设置会话置顶。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ConversationsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ConversationsApi apiInstance = new ConversationsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SetTopMostRequest setTopMostRequest = new SetTopMostRequest(); // SetTopMostRequest | 
    try {
      apiInstance.conversationsSetTopMost(clientId, setTopMostRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling ConversationsApi#conversationsSetTopMost");
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
| **setTopMostRequest** | [**SetTopMostRequest**](SetTopMostRequest.md)|  | [optional] |

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

