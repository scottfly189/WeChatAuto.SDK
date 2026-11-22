# WeChatAuto.Client.Api.NavigationApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**NavigationClose**](NavigationApi.md#navigationclose) | **POST** /api/v1/clients/{clientId}/navigation/close | 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。 |
| [**NavigationSwitch**](NavigationApi.md#navigationswitch) | **POST** /api/v1/clients/{clientId}/navigation/switch | 切换导航栏。 |

<a id="navigationclose"></a>
# **NavigationClose**
> void NavigationClose (string clientId, SwitchNavigationRequest? switchNavigationRequest = null)

关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。

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
    public class NavigationCloseExample
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
            var apiInstance = new NavigationApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var switchNavigationRequest = new SwitchNavigationRequest?(); // SwitchNavigationRequest? |  (optional) 

            try
            {
                // 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。
                apiInstance.NavigationClose(clientId, switchNavigationRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling NavigationApi.NavigationClose: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NavigationCloseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。
    apiInstance.NavigationCloseWithHttpInfo(clientId, switchNavigationRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling NavigationApi.NavigationCloseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **switchNavigationRequest** | [**SwitchNavigationRequest?**](SwitchNavigationRequest?.md) |  | [optional]  |

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

<a id="navigationswitch"></a>
# **NavigationSwitch**
> void NavigationSwitch (string clientId, SwitchNavigationRequest? switchNavigationRequest = null)

切换导航栏。

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
    public class NavigationSwitchExample
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
            var apiInstance = new NavigationApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var switchNavigationRequest = new SwitchNavigationRequest?(); // SwitchNavigationRequest? |  (optional) 

            try
            {
                // 切换导航栏。
                apiInstance.NavigationSwitch(clientId, switchNavigationRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling NavigationApi.NavigationSwitch: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NavigationSwitchWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 切换导航栏。
    apiInstance.NavigationSwitchWithHttpInfo(clientId, switchNavigationRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling NavigationApi.NavigationSwitchWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **switchNavigationRequest** | [**SwitchNavigationRequest?**](SwitchNavigationRequest?.md) |  | [optional]  |

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

