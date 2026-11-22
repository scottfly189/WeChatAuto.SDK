# NavigationApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**navigationClose**](NavigationApi.md#navigationClose) | **POST** /api/v1/clients/{clientId}/navigation/close | 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。 |
| [**navigationSwitch**](NavigationApi.md#navigationSwitch) | **POST** /api/v1/clients/{clientId}/navigation/switch | 切换导航栏。 |


<a id="navigationClose"></a>
# **navigationClose**
> navigationClose(clientId, switchNavigationRequest)

关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.NavigationApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    NavigationApi apiInstance = new NavigationApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SwitchNavigationRequest switchNavigationRequest = new SwitchNavigationRequest(); // SwitchNavigationRequest | 
    try {
      apiInstance.navigationClose(clientId, switchNavigationRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling NavigationApi#navigationClose");
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
| **switchNavigationRequest** | [**SwitchNavigationRequest**](SwitchNavigationRequest.md)|  | [optional] |

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

<a id="navigationSwitch"></a>
# **navigationSwitch**
> navigationSwitch(clientId, switchNavigationRequest)

切换导航栏。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.NavigationApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    NavigationApi apiInstance = new NavigationApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SwitchNavigationRequest switchNavigationRequest = new SwitchNavigationRequest(); // SwitchNavigationRequest | 
    try {
      apiInstance.navigationSwitch(clientId, switchNavigationRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling NavigationApi#navigationSwitch");
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
| **switchNavigationRequest** | [**SwitchNavigationRequest**](SwitchNavigationRequest.md)|  | [optional] |

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

