# WeChatAuto.Client.Api.FriendsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**FriendsAdd**](FriendsApi.md#friendsadd) | **POST** /api/v1/clients/{clientId}/friends/add | 通过手机号码/微信号查找并添加好友。 |
| [**FriendsAddOrUpdateFriend**](FriendsApi.md#friendsaddorupdatefriend) | **POST** /api/v1/clients/{clientId}/friends/cache | 手动新增或修改缓存中的好友对象。 |
| [**FriendsCloseAddWindow**](FriendsApi.md#friendscloseaddwindow) | **POST** /api/v1/clients/{clientId}/friends/add-window/close | 关闭新增朋友窗口。 |
| [**FriendsGetAllFriendNames**](FriendsApi.md#friendsgetallfriendnames) | **GET** /api/v1/clients/{clientId}/friends/names | 获取所有好友名称列表（有备注优先备注名）。 |
| [**FriendsGetAllFriends**](FriendsApi.md#friendsgetallfriends) | **GET** /api/v1/clients/{clientId}/friends | 获取所有好友的信息列表（默认优先从缓存读取）。 |
| [**FriendsGetFriendByWxidFromCache**](FriendsApi.md#friendsgetfriendbywxidfromcache) | **GET** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存获取好友信息。 |
| [**FriendsGetFriendFromCache**](FriendsApi.md#friendsgetfriendfromcache) | **GET** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存获取单个好友信息。 |
| [**FriendsGetFriendListFromCache**](FriendsApi.md#friendsgetfriendlistfromcache) | **GET** /api/v1/clients/{clientId}/friends/cache | 获取缓存中的全部好友信息列表。 |
| [**FriendsGetFriendsFromCache**](FriendsApi.md#friendsgetfriendsfromcache) | **GET** /api/v1/clients/{clientId}/friends/cache/by-name/{who} | 按名称从缓存获取好友列表（适用于昵称重复场景）。 |
| [**FriendsOpenAddWindow**](FriendsApi.md#friendsopenaddwindow) | **POST** /api/v1/clients/{clientId}/friends/add-window/open | 打开新增朋友窗口。 |
| [**FriendsPassAll**](FriendsApi.md#friendspassall) | **POST** /api/v1/clients/{clientId}/friends/pass-all | 立即批量通过当前待处理的新好友申请（不启动持续监听）。 |
| [**FriendsPauseAutoAccept**](FriendsApi.md#friendspauseautoaccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/pause | 暂停新好友自动通过监听。 |
| [**FriendsRemoveFriend**](FriendsApi.md#friendsremovefriend) | **DELETE** /api/v1/clients/{clientId}/friends/{nickName} | 从通讯录移除好友。 |
| [**FriendsRemoveFriendByWxidFromCache**](FriendsApi.md#friendsremovefriendbywxidfromcache) | **DELETE** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存移除好友。 |
| [**FriendsRemoveFriendFromCache**](FriendsApi.md#friendsremovefriendfromcache) | **DELETE** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存移除好友。 |
| [**FriendsResumeAutoAccept**](FriendsApi.md#friendsresumeautoaccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/resume | 恢复新好友自动通过监听。 |
| [**FriendsStartAutoAccept**](FriendsApi.md#friendsstartautoaccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/start | 启动新好友自动通过监听（按关键词/后缀/标签规则）。 |
| [**FriendsStopAutoAccept**](FriendsApi.md#friendsstopautoaccept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/stop | 停止新好友自动通过监听。 |

<a id="friendsadd"></a>
# **FriendsAdd**
> void FriendsAdd (string clientId, AddFriendsRequest? addFriendsRequest = null)

通过手机号码/微信号查找并添加好友。

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
    public class FriendsAddExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var addFriendsRequest = new AddFriendsRequest?(); // AddFriendsRequest? |  (optional) 

            try
            {
                // 通过手机号码/微信号查找并添加好友。
                apiInstance.FriendsAdd(clientId, addFriendsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsAdd: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsAddWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 通过手机号码/微信号查找并添加好友。
    apiInstance.FriendsAddWithHttpInfo(clientId, addFriendsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsAddWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **addFriendsRequest** | [**AddFriendsRequest?**](AddFriendsRequest?.md) |  | [optional]  |

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

<a id="friendsaddorupdatefriend"></a>
# **FriendsAddOrUpdateFriend**
> void FriendsAddOrUpdateFriend (string clientId, FriendInfo? friendInfo = null)

手动新增或修改缓存中的好友对象。

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
    public class FriendsAddOrUpdateFriendExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var friendInfo = new FriendInfo?(); // FriendInfo? |  (optional) 

            try
            {
                // 手动新增或修改缓存中的好友对象。
                apiInstance.FriendsAddOrUpdateFriend(clientId, friendInfo);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsAddOrUpdateFriend: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsAddOrUpdateFriendWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 手动新增或修改缓存中的好友对象。
    apiInstance.FriendsAddOrUpdateFriendWithHttpInfo(clientId, friendInfo);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsAddOrUpdateFriendWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **friendInfo** | [**FriendInfo?**](FriendInfo?.md) |  | [optional]  |

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

<a id="friendscloseaddwindow"></a>
# **FriendsCloseAddWindow**
> void FriendsCloseAddWindow (string clientId)

关闭新增朋友窗口。

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
    public class FriendsCloseAddWindowExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 关闭新增朋友窗口。
                apiInstance.FriendsCloseAddWindow(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsCloseAddWindow: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsCloseAddWindowWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 关闭新增朋友窗口。
    apiInstance.FriendsCloseAddWindowWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsCloseAddWindowWithHttpInfo: " + e.Message);
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

<a id="friendsgetallfriendnames"></a>
# **FriendsGetAllFriendNames**
> void FriendsGetAllFriendNames (string clientId)

获取所有好友名称列表（有备注优先备注名）。

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
    public class FriendsGetAllFriendNamesExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取所有好友名称列表（有备注优先备注名）。
                apiInstance.FriendsGetAllFriendNames(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsGetAllFriendNames: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsGetAllFriendNamesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取所有好友名称列表（有备注优先备注名）。
    apiInstance.FriendsGetAllFriendNamesWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsGetAllFriendNamesWithHttpInfo: " + e.Message);
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

<a id="friendsgetallfriends"></a>
# **FriendsGetAllFriends**
> void FriendsGetAllFriends (string clientId, bool? fromCache = null)

获取所有好友的信息列表（默认优先从缓存读取）。

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
    public class FriendsGetAllFriendsExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var fromCache = true;  // bool? |  (optional)  (default to true)

            try
            {
                // 获取所有好友的信息列表（默认优先从缓存读取）。
                apiInstance.FriendsGetAllFriends(clientId, fromCache);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsGetAllFriends: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsGetAllFriendsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取所有好友的信息列表（默认优先从缓存读取）。
    apiInstance.FriendsGetAllFriendsWithHttpInfo(clientId, fromCache);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsGetAllFriendsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **fromCache** | **bool?** |  | [optional] [default to true] |

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

<a id="friendsgetfriendbywxidfromcache"></a>
# **FriendsGetFriendByWxidFromCache**
> void FriendsGetFriendByWxidFromCache (string clientId, string wxid)

按 wxid 从缓存获取好友信息。

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
    public class FriendsGetFriendByWxidFromCacheExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var wxid = "wxid_example";  // string | 

            try
            {
                // 按 wxid 从缓存获取好友信息。
                apiInstance.FriendsGetFriendByWxidFromCache(clientId, wxid);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsGetFriendByWxidFromCache: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsGetFriendByWxidFromCacheWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按 wxid 从缓存获取好友信息。
    apiInstance.FriendsGetFriendByWxidFromCacheWithHttpInfo(clientId, wxid);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsGetFriendByWxidFromCacheWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **wxid** | **string** |  |  |

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

<a id="friendsgetfriendfromcache"></a>
# **FriendsGetFriendFromCache**
> void FriendsGetFriendFromCache (string clientId, string who)

按名称从缓存获取单个好友信息。

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
    public class FriendsGetFriendFromCacheExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var who = "who_example";  // string | 

            try
            {
                // 按名称从缓存获取单个好友信息。
                apiInstance.FriendsGetFriendFromCache(clientId, who);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsGetFriendFromCache: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsGetFriendFromCacheWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按名称从缓存获取单个好友信息。
    apiInstance.FriendsGetFriendFromCacheWithHttpInfo(clientId, who);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsGetFriendFromCacheWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **who** | **string** |  |  |

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

<a id="friendsgetfriendlistfromcache"></a>
# **FriendsGetFriendListFromCache**
> void FriendsGetFriendListFromCache (string clientId)

获取缓存中的全部好友信息列表。

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
    public class FriendsGetFriendListFromCacheExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取缓存中的全部好友信息列表。
                apiInstance.FriendsGetFriendListFromCache(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsGetFriendListFromCache: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsGetFriendListFromCacheWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取缓存中的全部好友信息列表。
    apiInstance.FriendsGetFriendListFromCacheWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsGetFriendListFromCacheWithHttpInfo: " + e.Message);
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

<a id="friendsgetfriendsfromcache"></a>
# **FriendsGetFriendsFromCache**
> void FriendsGetFriendsFromCache (string clientId, string who)

按名称从缓存获取好友列表（适用于昵称重复场景）。

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
    public class FriendsGetFriendsFromCacheExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var who = "who_example";  // string | 

            try
            {
                // 按名称从缓存获取好友列表（适用于昵称重复场景）。
                apiInstance.FriendsGetFriendsFromCache(clientId, who);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsGetFriendsFromCache: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsGetFriendsFromCacheWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按名称从缓存获取好友列表（适用于昵称重复场景）。
    apiInstance.FriendsGetFriendsFromCacheWithHttpInfo(clientId, who);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsGetFriendsFromCacheWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **who** | **string** |  |  |

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

<a id="friendsopenaddwindow"></a>
# **FriendsOpenAddWindow**
> void FriendsOpenAddWindow (string clientId)

打开新增朋友窗口。

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
    public class FriendsOpenAddWindowExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 打开新增朋友窗口。
                apiInstance.FriendsOpenAddWindow(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsOpenAddWindow: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsOpenAddWindowWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 打开新增朋友窗口。
    apiInstance.FriendsOpenAddWindowWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsOpenAddWindowWithHttpInfo: " + e.Message);
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

<a id="friendspassall"></a>
# **FriendsPassAll**
> void FriendsPassAll (string clientId, PassedAllNewFriendRequest? passedAllNewFriendRequest = null)

立即批量通过当前待处理的新好友申请（不启动持续监听）。

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
    public class FriendsPassAllExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var passedAllNewFriendRequest = new PassedAllNewFriendRequest?(); // PassedAllNewFriendRequest? |  (optional) 

            try
            {
                // 立即批量通过当前待处理的新好友申请（不启动持续监听）。
                apiInstance.FriendsPassAll(clientId, passedAllNewFriendRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsPassAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsPassAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 立即批量通过当前待处理的新好友申请（不启动持续监听）。
    apiInstance.FriendsPassAllWithHttpInfo(clientId, passedAllNewFriendRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsPassAllWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **passedAllNewFriendRequest** | [**PassedAllNewFriendRequest?**](PassedAllNewFriendRequest?.md) |  | [optional]  |

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

<a id="friendspauseautoaccept"></a>
# **FriendsPauseAutoAccept**
> void FriendsPauseAutoAccept (string clientId)

暂停新好友自动通过监听。

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
    public class FriendsPauseAutoAcceptExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 暂停新好友自动通过监听。
                apiInstance.FriendsPauseAutoAccept(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsPauseAutoAccept: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsPauseAutoAcceptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 暂停新好友自动通过监听。
    apiInstance.FriendsPauseAutoAcceptWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsPauseAutoAcceptWithHttpInfo: " + e.Message);
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

<a id="friendsremovefriend"></a>
# **FriendsRemoveFriend**
> void FriendsRemoveFriend (string clientId, string nickName)

从通讯录移除好友。

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
    public class FriendsRemoveFriendExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var nickName = "nickName_example";  // string | 

            try
            {
                // 从通讯录移除好友。
                apiInstance.FriendsRemoveFriend(clientId, nickName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsRemoveFriend: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsRemoveFriendWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 从通讯录移除好友。
    apiInstance.FriendsRemoveFriendWithHttpInfo(clientId, nickName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsRemoveFriendWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **nickName** | **string** |  |  |

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

<a id="friendsremovefriendbywxidfromcache"></a>
# **FriendsRemoveFriendByWxidFromCache**
> void FriendsRemoveFriendByWxidFromCache (string clientId, string wxid)

按 wxid 从缓存移除好友。

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
    public class FriendsRemoveFriendByWxidFromCacheExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var wxid = "wxid_example";  // string | 

            try
            {
                // 按 wxid 从缓存移除好友。
                apiInstance.FriendsRemoveFriendByWxidFromCache(clientId, wxid);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsRemoveFriendByWxidFromCache: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsRemoveFriendByWxidFromCacheWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按 wxid 从缓存移除好友。
    apiInstance.FriendsRemoveFriendByWxidFromCacheWithHttpInfo(clientId, wxid);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsRemoveFriendByWxidFromCacheWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **wxid** | **string** |  |  |

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

<a id="friendsremovefriendfromcache"></a>
# **FriendsRemoveFriendFromCache**
> void FriendsRemoveFriendFromCache (string clientId, string who)

按名称从缓存移除好友。

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
    public class FriendsRemoveFriendFromCacheExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var who = "who_example";  // string | 

            try
            {
                // 按名称从缓存移除好友。
                apiInstance.FriendsRemoveFriendFromCache(clientId, who);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsRemoveFriendFromCache: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsRemoveFriendFromCacheWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按名称从缓存移除好友。
    apiInstance.FriendsRemoveFriendFromCacheWithHttpInfo(clientId, who);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsRemoveFriendFromCacheWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **who** | **string** |  |  |

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

<a id="friendsresumeautoaccept"></a>
# **FriendsResumeAutoAccept**
> void FriendsResumeAutoAccept (string clientId)

恢复新好友自动通过监听。

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
    public class FriendsResumeAutoAcceptExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 恢复新好友自动通过监听。
                apiInstance.FriendsResumeAutoAccept(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsResumeAutoAccept: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsResumeAutoAcceptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 恢复新好友自动通过监听。
    apiInstance.FriendsResumeAutoAcceptWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsResumeAutoAcceptWithHttpInfo: " + e.Message);
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

<a id="friendsstartautoaccept"></a>
# **FriendsStartAutoAccept**
> void FriendsStartAutoAccept (string clientId, PassedAllNewFriendRequest? passedAllNewFriendRequest = null)

启动新好友自动通过监听（按关键词/后缀/标签规则）。

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
    public class FriendsStartAutoAcceptExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var passedAllNewFriendRequest = new PassedAllNewFriendRequest?(); // PassedAllNewFriendRequest? |  (optional) 

            try
            {
                // 启动新好友自动通过监听（按关键词/后缀/标签规则）。
                apiInstance.FriendsStartAutoAccept(clientId, passedAllNewFriendRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsStartAutoAccept: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsStartAutoAcceptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 启动新好友自动通过监听（按关键词/后缀/标签规则）。
    apiInstance.FriendsStartAutoAcceptWithHttpInfo(clientId, passedAllNewFriendRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsStartAutoAcceptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **passedAllNewFriendRequest** | [**PassedAllNewFriendRequest?**](PassedAllNewFriendRequest?.md) |  | [optional]  |

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

<a id="friendsstopautoaccept"></a>
# **FriendsStopAutoAccept**
> void FriendsStopAutoAccept (string clientId)

停止新好友自动通过监听。

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
    public class FriendsStopAutoAcceptExample
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
            var apiInstance = new FriendsApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 停止新好友自动通过监听。
                apiInstance.FriendsStopAutoAccept(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FriendsApi.FriendsStopAutoAccept: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FriendsStopAutoAcceptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 停止新好友自动通过监听。
    apiInstance.FriendsStopAutoAcceptWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FriendsApi.FriendsStopAutoAcceptWithHttpInfo: " + e.Message);
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

