# WeChatAuto.Client.Api.ClientsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ClientsClickNotifyIcon**](ClientsApi.md#clientsclicknotifyicon) | **POST** /api/v1/clients/{clientId}/notify-icon/click | 点击任务栏微信图标，按索引或微信名称定位。 |
| [**ClientsGetAll**](ClientsApi.md#clientsgetall) | **GET** /api/v1/clients | 获取全部微信客户端的概要信息列表。 |
| [**ClientsGetInfo**](ClientsApi.md#clientsgetinfo) | **GET** /api/v1/clients/{clientId} | 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。 |
| [**ClientsGetOwnerInfo**](ClientsApi.md#clientsgetownerinfo) | **GET** /api/v1/clients/{clientId}/owner-info | 获取本微信的个人信息（头像文件位置、wxid、昵称）。 |
| [**ClientsGetStorageRoot**](ClientsApi.md#clientsgetstorageroot) | **GET** /api/v1/clients/{clientId}/storage-root | 通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。 |
| [**ClientsGetVersion**](ClientsApi.md#clientsgetversion) | **GET** /api/v1/clients/{clientId}/version | 获取本微信的版本号。 |
| [**ClientsSaveAvatar**](ClientsApi.md#clientssaveavatar) | **POST** /api/v1/clients/{clientId}/avatar | 保存本微信头像到指定路径。 |

<a id="clientsclicknotifyicon"></a>
# **ClientsClickNotifyIcon**
> void ClientsClickNotifyIcon (string clientId, NotifyIconClickRequest? notifyIconClickRequest = null)

点击任务栏微信图标，按索引或微信名称定位。

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
    public class ClientsClickNotifyIconExample
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
            var apiInstance = new ClientsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var notifyIconClickRequest = new NotifyIconClickRequest?(); // NotifyIconClickRequest? |  (optional) 

            try
            {
                // 点击任务栏微信图标，按索引或微信名称定位。
                apiInstance.ClientsClickNotifyIcon(clientId, notifyIconClickRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClientsApi.ClientsClickNotifyIcon: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClientsClickNotifyIconWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 点击任务栏微信图标，按索引或微信名称定位。
    apiInstance.ClientsClickNotifyIconWithHttpInfo(clientId, notifyIconClickRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClientsApi.ClientsClickNotifyIconWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **notifyIconClickRequest** | [**NotifyIconClickRequest?**](NotifyIconClickRequest?.md) |  | [optional]  |

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

<a id="clientsgetall"></a>
# **ClientsGetAll**
> void ClientsGetAll ()

获取全部微信客户端的概要信息列表。

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
    public class ClientsGetAllExample
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
            var apiInstance = new ClientsApi(httpClient, config, httpClientHandler);

            try
            {
                // 获取全部微信客户端的概要信息列表。
                apiInstance.ClientsGetAll();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClientsApi.ClientsGetAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClientsGetAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取全部微信客户端的概要信息列表。
    apiInstance.ClientsGetAllWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClientsApi.ClientsGetAllWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
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

<a id="clientsgetinfo"></a>
# **ClientsGetInfo**
> void ClientsGetInfo (string clientId)

获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。

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
    public class ClientsGetInfoExample
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
            var apiInstance = new ClientsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。
                apiInstance.ClientsGetInfo(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClientsApi.ClientsGetInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClientsGetInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。
    apiInstance.ClientsGetInfoWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClientsApi.ClientsGetInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |

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

<a id="clientsgetownerinfo"></a>
# **ClientsGetOwnerInfo**
> void ClientsGetOwnerInfo (string clientId)

获取本微信的个人信息（头像文件位置、wxid、昵称）。

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
    public class ClientsGetOwnerInfoExample
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
            var apiInstance = new ClientsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取本微信的个人信息（头像文件位置、wxid、昵称）。
                apiInstance.ClientsGetOwnerInfo(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClientsApi.ClientsGetOwnerInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClientsGetOwnerInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取本微信的个人信息（头像文件位置、wxid、昵称）。
    apiInstance.ClientsGetOwnerInfoWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClientsApi.ClientsGetOwnerInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |

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

<a id="clientsgetstorageroot"></a>
# **ClientsGetStorageRoot**
> void ClientsGetStorageRoot (string clientId)

通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。

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
    public class ClientsGetStorageRootExample
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
            var apiInstance = new ClientsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。
                apiInstance.ClientsGetStorageRoot(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClientsApi.ClientsGetStorageRoot: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClientsGetStorageRootWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。
    apiInstance.ClientsGetStorageRootWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClientsApi.ClientsGetStorageRootWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |

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

<a id="clientsgetversion"></a>
# **ClientsGetVersion**
> void ClientsGetVersion (string clientId)

获取本微信的版本号。

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
    public class ClientsGetVersionExample
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
            var apiInstance = new ClientsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取本微信的版本号。
                apiInstance.ClientsGetVersion(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClientsApi.ClientsGetVersion: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClientsGetVersionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取本微信的版本号。
    apiInstance.ClientsGetVersionWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClientsApi.ClientsGetVersionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |

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

<a id="clientssaveavatar"></a>
# **ClientsSaveAvatar**
> void ClientsSaveAvatar (string clientId, SaveAvatarRequest? saveAvatarRequest = null)

保存本微信头像到指定路径。

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
    public class ClientsSaveAvatarExample
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
            var apiInstance = new ClientsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var saveAvatarRequest = new SaveAvatarRequest?(); // SaveAvatarRequest? |  (optional) 

            try
            {
                // 保存本微信头像到指定路径。
                apiInstance.ClientsSaveAvatar(clientId, saveAvatarRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ClientsApi.ClientsSaveAvatar: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClientsSaveAvatarWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 保存本微信头像到指定路径。
    apiInstance.ClientsSaveAvatarWithHttpInfo(clientId, saveAvatarRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ClientsApi.ClientsSaveAvatarWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **saveAvatarRequest** | [**SaveAvatarRequest?**](SaveAvatarRequest?.md) |  | [optional]  |

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

