# MomentsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**momentsAdd**](MomentsApi.md#momentsAdd) | **POST** /api/v1/clients/{clientId}/moments/add | 发朋友圈（图文，多图需在同一目录）。 |
| [**momentsClose**](MomentsApi.md#momentsClose) | **POST** /api/v1/clients/{clientId}/moments/close | 关闭朋友圈。 |
| [**momentsOpen**](MomentsApi.md#momentsOpen) | **POST** /api/v1/clients/{clientId}/moments/open | 打开朋友圈（已打开则提到最前）。 |
| [**momentsRemove**](MomentsApi.md#momentsRemove) | **POST** /api/v1/clients/{clientId}/moments/remove | 删除自己发的朋友圈（按文字内容）。 |


<a id="momentsAdd"></a>
# **momentsAdd**
> momentsAdd(clientId, addMomentsRequest)

发朋友圈（图文，多图需在同一目录）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MomentsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MomentsApi apiInstance = new MomentsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    AddMomentsRequest addMomentsRequest = new AddMomentsRequest(); // AddMomentsRequest | 
    try {
      apiInstance.momentsAdd(clientId, addMomentsRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MomentsApi#momentsAdd");
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
| **addMomentsRequest** | [**AddMomentsRequest**](AddMomentsRequest.md)|  | [optional] |

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

<a id="momentsClose"></a>
# **momentsClose**
> momentsClose(clientId)

关闭朋友圈。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MomentsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MomentsApi apiInstance = new MomentsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.momentsClose(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling MomentsApi#momentsClose");
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

<a id="momentsOpen"></a>
# **momentsOpen**
> momentsOpen(clientId)

打开朋友圈（已打开则提到最前）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MomentsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MomentsApi apiInstance = new MomentsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.momentsOpen(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling MomentsApi#momentsOpen");
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

<a id="momentsRemove"></a>
# **momentsRemove**
> momentsRemove(clientId, removeMomentsRequest)

删除自己发的朋友圈（按文字内容）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.MomentsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    MomentsApi apiInstance = new MomentsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    RemoveMomentsRequest removeMomentsRequest = new RemoveMomentsRequest(); // RemoveMomentsRequest | 
    try {
      apiInstance.momentsRemove(clientId, removeMomentsRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling MomentsApi#momentsRemove");
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
| **removeMomentsRequest** | [**RemoveMomentsRequest**](RemoveMomentsRequest.md)|  | [optional] |

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

