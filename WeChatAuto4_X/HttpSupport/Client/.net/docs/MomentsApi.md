# WeChatAuto.Client.Api.MomentsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**MomentsAdd**](MomentsApi.md#momentsadd) | **POST** /api/v1/clients/{clientId}/moments/add | 发朋友圈（图文，多图需在同一目录）。 |
| [**MomentsClose**](MomentsApi.md#momentsclose) | **POST** /api/v1/clients/{clientId}/moments/close | 关闭朋友圈。 |
| [**MomentsOpen**](MomentsApi.md#momentsopen) | **POST** /api/v1/clients/{clientId}/moments/open | 打开朋友圈（已打开则提到最前）。 |
| [**MomentsRemove**](MomentsApi.md#momentsremove) | **POST** /api/v1/clients/{clientId}/moments/remove | 删除自己发的朋友圈（按文字内容）。 |

<a id="momentsadd"></a>
# **MomentsAdd**
> void MomentsAdd (string clientId, AddMomentsRequest? addMomentsRequest = null)

发朋友圈（图文，多图需在同一目录）。

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
    public class MomentsAddExample
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
            var apiInstance = new MomentsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var addMomentsRequest = new AddMomentsRequest?(); // AddMomentsRequest? |  (optional) 

            try
            {
                // 发朋友圈（图文，多图需在同一目录）。
                apiInstance.MomentsAdd(clientId, addMomentsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MomentsApi.MomentsAdd: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MomentsAddWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 发朋友圈（图文，多图需在同一目录）。
    apiInstance.MomentsAddWithHttpInfo(clientId, addMomentsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MomentsApi.MomentsAddWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **addMomentsRequest** | [**AddMomentsRequest?**](AddMomentsRequest?.md) |  | [optional]  |

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

<a id="momentsclose"></a>
# **MomentsClose**
> void MomentsClose (string clientId)

关闭朋友圈。

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
    public class MomentsCloseExample
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
            var apiInstance = new MomentsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 关闭朋友圈。
                apiInstance.MomentsClose(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MomentsApi.MomentsClose: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MomentsCloseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 关闭朋友圈。
    apiInstance.MomentsCloseWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MomentsApi.MomentsCloseWithHttpInfo: " + e.Message);
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

<a id="momentsopen"></a>
# **MomentsOpen**
> void MomentsOpen (string clientId)

打开朋友圈（已打开则提到最前）。

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
    public class MomentsOpenExample
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
            var apiInstance = new MomentsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 打开朋友圈（已打开则提到最前）。
                apiInstance.MomentsOpen(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MomentsApi.MomentsOpen: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MomentsOpenWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 打开朋友圈（已打开则提到最前）。
    apiInstance.MomentsOpenWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MomentsApi.MomentsOpenWithHttpInfo: " + e.Message);
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

<a id="momentsremove"></a>
# **MomentsRemove**
> void MomentsRemove (string clientId, RemoveMomentsRequest? removeMomentsRequest = null)

删除自己发的朋友圈（按文字内容）。

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
    public class MomentsRemoveExample
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
            var apiInstance = new MomentsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var removeMomentsRequest = new RemoveMomentsRequest?(); // RemoveMomentsRequest? |  (optional) 

            try
            {
                // 删除自己发的朋友圈（按文字内容）。
                apiInstance.MomentsRemove(clientId, removeMomentsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MomentsApi.MomentsRemove: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MomentsRemoveWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 删除自己发的朋友圈（按文字内容）。
    apiInstance.MomentsRemoveWithHttpInfo(clientId, removeMomentsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling MomentsApi.MomentsRemoveWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **removeMomentsRequest** | [**RemoveMomentsRequest?**](RemoveMomentsRequest?.md) |  | [optional]  |

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

