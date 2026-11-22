# WeChatAuto.Client.Api.ConversationsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ConversationsGetAllConversations**](ConversationsApi.md#conversationsgetallconversations) | **GET** /api/v1/clients/{clientId}/conversations | 获取会话列表所有会话的标题。 |
| [**ConversationsGetVisibleConversations**](ConversationsApi.md#conversationsgetvisibleconversations) | **GET** /api/v1/clients/{clientId}/conversations/visible | 获取可见会话列表（含名称、未读消息数、头像等）。 |
| [**ConversationsGetVisibleTitles**](ConversationsApi.md#conversationsgetvisibletitles) | **GET** /api/v1/clients/{clientId}/conversations/visible-titles | 获取会话列表可见会话的标题。 |
| [**ConversationsLocate**](ConversationsApi.md#conversationslocate) | **POST** /api/v1/clients/{clientId}/conversations/locate | 定位会话，滚动会话列表使指定会话可见。 |
| [**ConversationsSearch**](ConversationsApi.md#conversationssearch) | **POST** /api/v1/clients/{clientId}/conversations/search | 搜索好友/群聊。 |
| [**ConversationsSetDoNotDisturb**](ConversationsApi.md#conversationssetdonotdisturb) | **POST** /api/v1/clients/{clientId}/conversations/set-do-not-disturb | 设置会话消息免打扰。 |
| [**ConversationsSetTopMost**](ConversationsApi.md#conversationssettopmost) | **POST** /api/v1/clients/{clientId}/conversations/set-top-most | 设置会话置顶。 |

<a id="conversationsgetallconversations"></a>
# **ConversationsGetAllConversations**
> void ConversationsGetAllConversations (string clientId)

获取会话列表所有会话的标题。

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
    public class ConversationsGetAllConversationsExample
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
            var apiInstance = new ConversationsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取会话列表所有会话的标题。
                apiInstance.ConversationsGetAllConversations(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConversationsApi.ConversationsGetAllConversations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConversationsGetAllConversationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取会话列表所有会话的标题。
    apiInstance.ConversationsGetAllConversationsWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConversationsApi.ConversationsGetAllConversationsWithHttpInfo: " + e.Message);
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

<a id="conversationsgetvisibleconversations"></a>
# **ConversationsGetVisibleConversations**
> void ConversationsGetVisibleConversations (string clientId)

获取可见会话列表（含名称、未读消息数、头像等）。

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
    public class ConversationsGetVisibleConversationsExample
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
            var apiInstance = new ConversationsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取可见会话列表（含名称、未读消息数、头像等）。
                apiInstance.ConversationsGetVisibleConversations(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConversationsApi.ConversationsGetVisibleConversations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConversationsGetVisibleConversationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取可见会话列表（含名称、未读消息数、头像等）。
    apiInstance.ConversationsGetVisibleConversationsWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConversationsApi.ConversationsGetVisibleConversationsWithHttpInfo: " + e.Message);
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

<a id="conversationsgetvisibletitles"></a>
# **ConversationsGetVisibleTitles**
> void ConversationsGetVisibleTitles (string clientId)

获取会话列表可见会话的标题。

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
    public class ConversationsGetVisibleTitlesExample
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
            var apiInstance = new ConversationsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取会话列表可见会话的标题。
                apiInstance.ConversationsGetVisibleTitles(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConversationsApi.ConversationsGetVisibleTitles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConversationsGetVisibleTitlesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取会话列表可见会话的标题。
    apiInstance.ConversationsGetVisibleTitlesWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConversationsApi.ConversationsGetVisibleTitlesWithHttpInfo: " + e.Message);
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

<a id="conversationslocate"></a>
# **ConversationsLocate**
> void ConversationsLocate (string clientId, LocateConversationRequest? locateConversationRequest = null)

定位会话，滚动会话列表使指定会话可见。

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
    public class ConversationsLocateExample
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
            var apiInstance = new ConversationsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var locateConversationRequest = new LocateConversationRequest?(); // LocateConversationRequest? |  (optional) 

            try
            {
                // 定位会话，滚动会话列表使指定会话可见。
                apiInstance.ConversationsLocate(clientId, locateConversationRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConversationsApi.ConversationsLocate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConversationsLocateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 定位会话，滚动会话列表使指定会话可见。
    apiInstance.ConversationsLocateWithHttpInfo(clientId, locateConversationRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConversationsApi.ConversationsLocateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **locateConversationRequest** | [**LocateConversationRequest?**](LocateConversationRequest?.md) |  | [optional]  |

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

<a id="conversationssearch"></a>
# **ConversationsSearch**
> void ConversationsSearch (string clientId, SearchConversationRequest? searchConversationRequest = null)

搜索好友/群聊。

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
    public class ConversationsSearchExample
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
            var apiInstance = new ConversationsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var searchConversationRequest = new SearchConversationRequest?(); // SearchConversationRequest? |  (optional) 

            try
            {
                // 搜索好友/群聊。
                apiInstance.ConversationsSearch(clientId, searchConversationRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConversationsApi.ConversationsSearch: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConversationsSearchWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 搜索好友/群聊。
    apiInstance.ConversationsSearchWithHttpInfo(clientId, searchConversationRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConversationsApi.ConversationsSearchWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **searchConversationRequest** | [**SearchConversationRequest?**](SearchConversationRequest?.md) |  | [optional]  |

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

<a id="conversationssetdonotdisturb"></a>
# **ConversationsSetDoNotDisturb**
> void ConversationsSetDoNotDisturb (string clientId, SetDoNotDisturbRequest? setDoNotDisturbRequest = null)

设置会话消息免打扰。

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
    public class ConversationsSetDoNotDisturbExample
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
            var apiInstance = new ConversationsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var setDoNotDisturbRequest = new SetDoNotDisturbRequest?(); // SetDoNotDisturbRequest? |  (optional) 

            try
            {
                // 设置会话消息免打扰。
                apiInstance.ConversationsSetDoNotDisturb(clientId, setDoNotDisturbRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConversationsApi.ConversationsSetDoNotDisturb: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConversationsSetDoNotDisturbWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 设置会话消息免打扰。
    apiInstance.ConversationsSetDoNotDisturbWithHttpInfo(clientId, setDoNotDisturbRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConversationsApi.ConversationsSetDoNotDisturbWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **setDoNotDisturbRequest** | [**SetDoNotDisturbRequest?**](SetDoNotDisturbRequest?.md) |  | [optional]  |

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

<a id="conversationssettopmost"></a>
# **ConversationsSetTopMost**
> void ConversationsSetTopMost (string clientId, SetTopMostRequest? setTopMostRequest = null)

设置会话置顶。

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
    public class ConversationsSetTopMostExample
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
            var apiInstance = new ConversationsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var setTopMostRequest = new SetTopMostRequest?(); // SetTopMostRequest? |  (optional) 

            try
            {
                // 设置会话置顶。
                apiInstance.ConversationsSetTopMost(clientId, setTopMostRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConversationsApi.ConversationsSetTopMost: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConversationsSetTopMostWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 设置会话置顶。
    apiInstance.ConversationsSetTopMostWithHttpInfo(clientId, setTopMostRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConversationsApi.ConversationsSetTopMostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **setTopMostRequest** | [**SetTopMostRequest?**](SetTopMostRequest?.md) |  | [optional]  |

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

