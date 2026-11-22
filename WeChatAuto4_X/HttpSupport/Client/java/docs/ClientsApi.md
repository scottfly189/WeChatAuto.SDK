# ClientsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**clientsClickNotifyIcon**](ClientsApi.md#clientsClickNotifyIcon) | **POST** /api/v1/clients/{clientId}/notify-icon/click | 点击任务栏微信图标，按索引或微信名称定位。 |
| [**clientsGetAll**](ClientsApi.md#clientsGetAll) | **GET** /api/v1/clients | 获取全部微信客户端的概要信息列表。 |
| [**clientsGetInfo**](ClientsApi.md#clientsGetInfo) | **GET** /api/v1/clients/{clientId} | 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。 |
| [**clientsGetOwnerInfo**](ClientsApi.md#clientsGetOwnerInfo) | **GET** /api/v1/clients/{clientId}/owner-info | 获取本微信的个人信息（头像文件位置、wxid、昵称）。 |
| [**clientsGetStorageRoot**](ClientsApi.md#clientsGetStorageRoot) | **GET** /api/v1/clients/{clientId}/storage-root | 通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。 |
| [**clientsGetVersion**](ClientsApi.md#clientsGetVersion) | **GET** /api/v1/clients/{clientId}/version | 获取本微信的版本号。 |
| [**clientsSaveAvatar**](ClientsApi.md#clientsSaveAvatar) | **POST** /api/v1/clients/{clientId}/avatar | 保存本微信头像到指定路径。 |


<a id="clientsClickNotifyIcon"></a>
# **clientsClickNotifyIcon**
> clientsClickNotifyIcon(clientId, notifyIconClickRequest)

点击任务栏微信图标，按索引或微信名称定位。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ClientsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ClientsApi apiInstance = new ClientsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    NotifyIconClickRequest notifyIconClickRequest = new NotifyIconClickRequest(); // NotifyIconClickRequest | 
    try {
      apiInstance.clientsClickNotifyIcon(clientId, notifyIconClickRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling ClientsApi#clientsClickNotifyIcon");
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
| **notifyIconClickRequest** | [**NotifyIconClickRequest**](NotifyIconClickRequest.md)|  | [optional] |

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

<a id="clientsGetAll"></a>
# **clientsGetAll**
> clientsGetAll()

获取全部微信客户端的概要信息列表。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ClientsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ClientsApi apiInstance = new ClientsApi(defaultClient);
    try {
      apiInstance.clientsGetAll();
    } catch (ApiException e) {
      System.err.println("Exception when calling ClientsApi#clientsGetAll");
      System.err.println("Status code: " + e.getCode());
      System.err.println("Reason: " + e.getResponseBody());
      System.err.println("Response headers: " + e.getResponseHeaders());
      e.printStackTrace();
    }
  }
}
```

### Parameters
This endpoint does not need any parameter.

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

<a id="clientsGetInfo"></a>
# **clientsGetInfo**
> clientsGetInfo(clientId)

获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ClientsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ClientsApi apiInstance = new ClientsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.clientsGetInfo(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling ClientsApi#clientsGetInfo");
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

<a id="clientsGetOwnerInfo"></a>
# **clientsGetOwnerInfo**
> clientsGetOwnerInfo(clientId)

获取本微信的个人信息（头像文件位置、wxid、昵称）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ClientsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ClientsApi apiInstance = new ClientsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.clientsGetOwnerInfo(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling ClientsApi#clientsGetOwnerInfo");
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

<a id="clientsGetStorageRoot"></a>
# **clientsGetStorageRoot**
> clientsGetStorageRoot(clientId)

通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ClientsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ClientsApi apiInstance = new ClientsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.clientsGetStorageRoot(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling ClientsApi#clientsGetStorageRoot");
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

<a id="clientsGetVersion"></a>
# **clientsGetVersion**
> clientsGetVersion(clientId)

获取本微信的版本号。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ClientsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ClientsApi apiInstance = new ClientsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    try {
      apiInstance.clientsGetVersion(clientId);
    } catch (ApiException e) {
      System.err.println("Exception when calling ClientsApi#clientsGetVersion");
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

<a id="clientsSaveAvatar"></a>
# **clientsSaveAvatar**
> clientsSaveAvatar(clientId, saveAvatarRequest)

保存本微信头像到指定路径。

### Example
```java
// Import classes:
import com.wechatauto.client.ApiClient;
import com.wechatauto.client.ApiException;
import com.wechatauto.client.Configuration;
import com.wechatauto.client.auth.*;
import com.wechatauto.client.models.*;
import com.wechatauto.client.api.ClientsApi;

public class Example {
  public static void main(String[] args) {
    ApiClient defaultClient = Configuration.getDefaultApiClient();
    defaultClient.setBasePath("http://localhost");
    
    // Configure API key authorization: ApiKey
    ApiKeyAuth ApiKey = (ApiKeyAuth) defaultClient.getAuthentication("ApiKey");
    ApiKey.setApiKey("YOUR API KEY");
    // Uncomment the following line to set a prefix for the API key, e.g. "Token" (defaults to null)
    //ApiKey.setApiKeyPrefix("Token");

    ClientsApi apiInstance = new ClientsApi(defaultClient);
    String clientId = "clientId_example"; // String | 
    SaveAvatarRequest saveAvatarRequest = new SaveAvatarRequest(); // SaveAvatarRequest | 
    try {
      apiInstance.clientsSaveAvatar(clientId, saveAvatarRequest);
    } catch (ApiException e) {
      System.err.println("Exception when calling ClientsApi#clientsSaveAvatar");
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
| **saveAvatarRequest** | [**SaveAvatarRequest**](SaveAvatarRequest.md)|  | [optional] |

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

