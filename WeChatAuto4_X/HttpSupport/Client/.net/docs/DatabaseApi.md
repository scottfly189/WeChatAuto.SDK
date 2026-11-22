# WeChatAuto.Client.Api.DatabaseApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DatabaseGetContact**](DatabaseApi.md#databasegetcontact) | **GET** /api/v1/clients/{clientId}/database/contacts/{userName} | 按 username（wxid / 群 id）取一条联系人。 |
| [**DatabaseGetContactByShowName**](DatabaseApi.md#databasegetcontactbyshowname) | **GET** /api/v1/clients/{clientId}/database/contacts/by-show-name/first | 按显示名取第一条联系人（重名时取 UserName 升序第一条）。 |
| [**DatabaseGetContacts**](DatabaseApi.md#databasegetcontacts) | **GET** /api/v1/clients/{clientId}/database/contacts | 获取当前微信号全部联系人（不含已删除）。 |
| [**DatabaseGetContactsByShowName**](DatabaseApi.md#databasegetcontactsbyshowname) | **GET** /api/v1/clients/{clientId}/database/contacts/by-show-name | 按显示名取联系人；重名时返回多个。 |
| [**DatabaseGetMaxSortSeq**](DatabaseApi.md#databasegetmaxsortseq) | **GET** /api/v1/clients/{clientId}/database/messages/max-sort-seq | 取当前微信号消息的最大 SortSeq（增量水位）。 |
| [**DatabaseGetMessages**](DatabaseApi.md#databasegetmessages) | **GET** /api/v1/clients/{clientId}/database/messages | 取指定会话最近 N 条消息（从新到旧）。 |
| [**DatabaseGetNewMessages**](DatabaseApi.md#databasegetnewmessages) | **GET** /api/v1/clients/{clientId}/database/messages/new | 取 SortSeq 大于指定值的新消息（从旧到新）。 |
| [**DatabaseGetRecentMessages**](DatabaseApi.md#databasegetrecentmessages) | **GET** /api/v1/clients/{clientId}/database/messages/recent | 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。 |
| [**DatabaseGetRoomByShowName**](DatabaseApi.md#databasegetroombyshowname) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name/first | 按群的显示名取第一条群。 |
| [**DatabaseGetRoomMembers**](DatabaseApi.md#databasegetroommembers) | **GET** /api/v1/clients/{clientId}/database/rooms/{roomUsername}/members | 按群 username 取成员列表。 |
| [**DatabaseGetRoomMembersByShowName**](DatabaseApi.md#databasegetroommembersbyshowname) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name/members | 按群的显示名取成员列表；重名时取第一条群。 |
| [**DatabaseGetRooms**](DatabaseApi.md#databasegetrooms) | **GET** /api/v1/clients/{clientId}/database/rooms | 获取当前微信号全部群聊。 |
| [**DatabaseGetRoomsByLocalType**](DatabaseApi.md#databasegetroomsbylocaltype) | **GET** /api/v1/clients/{clientId}/database/rooms/by-local-type | 按联系人 LocalType 过滤群聊（本机观察群多为 2）。 |
| [**DatabaseGetRoomsByShowName**](DatabaseApi.md#databasegetroomsbyshowname) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name | 按群的显示名取群；重名时返回多个。 |
| [**DatabaseGetSnsPost**](DatabaseApi.md#databasegetsnspost) | **GET** /api/v1/clients/{clientId}/database/sns/posts/{tid} | 按 tid 取一条朋友圈（含媒体、点赞、评论）。 |
| [**DatabaseGetSnsPosts**](DatabaseApi.md#databasegetsnsposts) | **GET** /api/v1/clients/{clientId}/database/sns/posts | 取最近 N 条朋友圈（含媒体、点赞、评论）。 |
| [**DatabaseGetSnsPostsByDate**](DatabaseApi.md#databasegetsnspostsbydate) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-date | 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。 |
| [**DatabaseGetSnsPostsByShowName**](DatabaseApi.md#databasegetsnspostsbyshowname) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-show-name | 按发布者显示名取朋友圈；重名会一并返回。 |
| [**DatabaseGetSnsPostsByUserName**](DatabaseApi.md#databasegetsnspostsbyusername) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-user | 取某个发布者（真实 UserName / wxid）的朋友圈。 |
| [**DatabaseQuery**](DatabaseApi.md#databasequery) | **POST** /api/v1/clients/{clientId}/database/query | 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。 |
| [**DatabaseSearchAllMessages**](DatabaseApi.md#databasesearchallmessages) | **GET** /api/v1/clients/{clientId}/database/messages/search-all | 按正文关键字跨所有微信号搜索消息。 |
| [**DatabaseSearchContacts**](DatabaseApi.md#databasesearchcontacts) | **GET** /api/v1/clients/{clientId}/database/contacts/search | 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。 |
| [**DatabaseSearchMessages**](DatabaseApi.md#databasesearchmessages) | **GET** /api/v1/clients/{clientId}/database/messages/search | 按正文关键字搜索当前微信号消息。 |

<a id="databasegetcontact"></a>
# **DatabaseGetContact**
> void DatabaseGetContact (string clientId, string userName)

按 username（wxid / 群 id）取一条联系人。

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
    public class DatabaseGetContactExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var userName = "userName_example";  // string | 

            try
            {
                // 按 username（wxid / 群 id）取一条联系人。
                apiInstance.DatabaseGetContact(clientId, userName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetContact: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetContactWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按 username（wxid / 群 id）取一条联系人。
    apiInstance.DatabaseGetContactWithHttpInfo(clientId, userName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetContactWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **userName** | **string** |  |  |

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

<a id="databasegetcontactbyshowname"></a>
# **DatabaseGetContactByShowName**
> void DatabaseGetContactByShowName (string clientId, string? showName = null)

按显示名取第一条联系人（重名时取 UserName 升序第一条）。

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
    public class DatabaseGetContactByShowNameExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var showName = "showName_example";  // string? |  (optional) 

            try
            {
                // 按显示名取第一条联系人（重名时取 UserName 升序第一条）。
                apiInstance.DatabaseGetContactByShowName(clientId, showName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetContactByShowName: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetContactByShowNameWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按显示名取第一条联系人（重名时取 UserName 升序第一条）。
    apiInstance.DatabaseGetContactByShowNameWithHttpInfo(clientId, showName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetContactByShowNameWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **showName** | **string?** |  | [optional]  |

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

<a id="databasegetcontacts"></a>
# **DatabaseGetContacts**
> void DatabaseGetContacts (string clientId)

获取当前微信号全部联系人（不含已删除）。

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
    public class DatabaseGetContactsExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取当前微信号全部联系人（不含已删除）。
                apiInstance.DatabaseGetContacts(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetContacts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetContactsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取当前微信号全部联系人（不含已删除）。
    apiInstance.DatabaseGetContactsWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetContactsWithHttpInfo: " + e.Message);
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

<a id="databasegetcontactsbyshowname"></a>
# **DatabaseGetContactsByShowName**
> void DatabaseGetContactsByShowName (string clientId, string? showName = null)

按显示名取联系人；重名时返回多个。

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
    public class DatabaseGetContactsByShowNameExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var showName = "showName_example";  // string? |  (optional) 

            try
            {
                // 按显示名取联系人；重名时返回多个。
                apiInstance.DatabaseGetContactsByShowName(clientId, showName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetContactsByShowName: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetContactsByShowNameWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按显示名取联系人；重名时返回多个。
    apiInstance.DatabaseGetContactsByShowNameWithHttpInfo(clientId, showName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetContactsByShowNameWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **showName** | **string?** |  | [optional]  |

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

<a id="databasegetmaxsortseq"></a>
# **DatabaseGetMaxSortSeq**
> void DatabaseGetMaxSortSeq (string clientId)

取当前微信号消息的最大 SortSeq（增量水位）。

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
    public class DatabaseGetMaxSortSeqExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 取当前微信号消息的最大 SortSeq（增量水位）。
                apiInstance.DatabaseGetMaxSortSeq(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetMaxSortSeq: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetMaxSortSeqWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 取当前微信号消息的最大 SortSeq（增量水位）。
    apiInstance.DatabaseGetMaxSortSeqWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetMaxSortSeqWithHttpInfo: " + e.Message);
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

<a id="databasegetmessages"></a>
# **DatabaseGetMessages**
> void DatabaseGetMessages (string clientId, string? chatUsername = null, int? count = null)

取指定会话最近 N 条消息（从新到旧）。

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
    public class DatabaseGetMessagesExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var chatUsername = "chatUsername_example";  // string? |  (optional) 
            var count = 50;  // int? |  (optional)  (default to 50)

            try
            {
                // 取指定会话最近 N 条消息（从新到旧）。
                apiInstance.DatabaseGetMessages(clientId, chatUsername, count);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetMessages: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetMessagesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 取指定会话最近 N 条消息（从新到旧）。
    apiInstance.DatabaseGetMessagesWithHttpInfo(clientId, chatUsername, count);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetMessagesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **chatUsername** | **string?** |  | [optional]  |
| **count** | **int?** |  | [optional] [default to 50] |

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

<a id="databasegetnewmessages"></a>
# **DatabaseGetNewMessages**
> void DatabaseGetNewMessages (string clientId, long? afterSortSeq = null)

取 SortSeq 大于指定值的新消息（从旧到新）。

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
    public class DatabaseGetNewMessagesExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var afterSortSeq = 789L;  // long? |  (optional) 

            try
            {
                // 取 SortSeq 大于指定值的新消息（从旧到新）。
                apiInstance.DatabaseGetNewMessages(clientId, afterSortSeq);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetNewMessages: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetNewMessagesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 取 SortSeq 大于指定值的新消息（从旧到新）。
    apiInstance.DatabaseGetNewMessagesWithHttpInfo(clientId, afterSortSeq);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetNewMessagesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **afterSortSeq** | **long?** |  | [optional]  |

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

<a id="databasegetrecentmessages"></a>
# **DatabaseGetRecentMessages**
> void DatabaseGetRecentMessages (string clientId, string? chatUsername = null, int? count = null)

取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。

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
    public class DatabaseGetRecentMessagesExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var chatUsername = "chatUsername_example";  // string? |  (optional) 
            var count = 50;  // int? |  (optional)  (default to 50)

            try
            {
                // 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。
                apiInstance.DatabaseGetRecentMessages(clientId, chatUsername, count);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetRecentMessages: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetRecentMessagesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。
    apiInstance.DatabaseGetRecentMessagesWithHttpInfo(clientId, chatUsername, count);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetRecentMessagesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **chatUsername** | **string?** |  | [optional]  |
| **count** | **int?** |  | [optional] [default to 50] |

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

<a id="databasegetroombyshowname"></a>
# **DatabaseGetRoomByShowName**
> void DatabaseGetRoomByShowName (string clientId, string? showName = null)

按群的显示名取第一条群。

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
    public class DatabaseGetRoomByShowNameExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var showName = "showName_example";  // string? |  (optional) 

            try
            {
                // 按群的显示名取第一条群。
                apiInstance.DatabaseGetRoomByShowName(clientId, showName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomByShowName: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetRoomByShowNameWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按群的显示名取第一条群。
    apiInstance.DatabaseGetRoomByShowNameWithHttpInfo(clientId, showName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomByShowNameWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **showName** | **string?** |  | [optional]  |

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

<a id="databasegetroommembers"></a>
# **DatabaseGetRoomMembers**
> void DatabaseGetRoomMembers (string clientId, string roomUsername)

按群 username 取成员列表。

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
    public class DatabaseGetRoomMembersExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var roomUsername = "roomUsername_example";  // string | 

            try
            {
                // 按群 username 取成员列表。
                apiInstance.DatabaseGetRoomMembers(clientId, roomUsername);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetRoomMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按群 username 取成员列表。
    apiInstance.DatabaseGetRoomMembersWithHttpInfo(clientId, roomUsername);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **roomUsername** | **string** |  |  |

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

<a id="databasegetroommembersbyshowname"></a>
# **DatabaseGetRoomMembersByShowName**
> void DatabaseGetRoomMembersByShowName (string clientId, string? showName = null)

按群的显示名取成员列表；重名时取第一条群。

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
    public class DatabaseGetRoomMembersByShowNameExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var showName = "showName_example";  // string? |  (optional) 

            try
            {
                // 按群的显示名取成员列表；重名时取第一条群。
                apiInstance.DatabaseGetRoomMembersByShowName(clientId, showName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomMembersByShowName: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetRoomMembersByShowNameWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按群的显示名取成员列表；重名时取第一条群。
    apiInstance.DatabaseGetRoomMembersByShowNameWithHttpInfo(clientId, showName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomMembersByShowNameWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **showName** | **string?** |  | [optional]  |

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

<a id="databasegetrooms"></a>
# **DatabaseGetRooms**
> void DatabaseGetRooms (string clientId)

获取当前微信号全部群聊。

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
    public class DatabaseGetRoomsExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 

            try
            {
                // 获取当前微信号全部群聊。
                apiInstance.DatabaseGetRooms(clientId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetRooms: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetRoomsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 获取当前微信号全部群聊。
    apiInstance.DatabaseGetRoomsWithHttpInfo(clientId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomsWithHttpInfo: " + e.Message);
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

<a id="databasegetroomsbylocaltype"></a>
# **DatabaseGetRoomsByLocalType**
> void DatabaseGetRoomsByLocalType (string clientId, int? localType = null)

按联系人 LocalType 过滤群聊（本机观察群多为 2）。

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
    public class DatabaseGetRoomsByLocalTypeExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var localType = 56;  // int? |  (optional) 

            try
            {
                // 按联系人 LocalType 过滤群聊（本机观察群多为 2）。
                apiInstance.DatabaseGetRoomsByLocalType(clientId, localType);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomsByLocalType: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetRoomsByLocalTypeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按联系人 LocalType 过滤群聊（本机观察群多为 2）。
    apiInstance.DatabaseGetRoomsByLocalTypeWithHttpInfo(clientId, localType);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomsByLocalTypeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **localType** | **int?** |  | [optional]  |

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

<a id="databasegetroomsbyshowname"></a>
# **DatabaseGetRoomsByShowName**
> void DatabaseGetRoomsByShowName (string clientId, string? showName = null)

按群的显示名取群；重名时返回多个。

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
    public class DatabaseGetRoomsByShowNameExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var showName = "showName_example";  // string? |  (optional) 

            try
            {
                // 按群的显示名取群；重名时返回多个。
                apiInstance.DatabaseGetRoomsByShowName(clientId, showName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomsByShowName: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetRoomsByShowNameWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按群的显示名取群；重名时返回多个。
    apiInstance.DatabaseGetRoomsByShowNameWithHttpInfo(clientId, showName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetRoomsByShowNameWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **showName** | **string?** |  | [optional]  |

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

<a id="databasegetsnspost"></a>
# **DatabaseGetSnsPost**
> void DatabaseGetSnsPost (string clientId, long tid)

按 tid 取一条朋友圈（含媒体、点赞、评论）。

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
    public class DatabaseGetSnsPostExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var tid = 789L;  // long | 

            try
            {
                // 按 tid 取一条朋友圈（含媒体、点赞、评论）。
                apiInstance.DatabaseGetSnsPost(clientId, tid);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetSnsPost: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetSnsPostWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按 tid 取一条朋友圈（含媒体、点赞、评论）。
    apiInstance.DatabaseGetSnsPostWithHttpInfo(clientId, tid);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetSnsPostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **tid** | **long** |  |  |

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

<a id="databasegetsnsposts"></a>
# **DatabaseGetSnsPosts**
> void DatabaseGetSnsPosts (string clientId, int? limit = null)

取最近 N 条朋友圈（含媒体、点赞、评论）。

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
    public class DatabaseGetSnsPostsExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var limit = 20;  // int? |  (optional)  (default to 20)

            try
            {
                // 取最近 N 条朋友圈（含媒体、点赞、评论）。
                apiInstance.DatabaseGetSnsPosts(clientId, limit);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetSnsPosts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetSnsPostsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 取最近 N 条朋友圈（含媒体、点赞、评论）。
    apiInstance.DatabaseGetSnsPostsWithHttpInfo(clientId, limit);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetSnsPostsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **limit** | **int?** |  | [optional] [default to 20] |

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

<a id="databasegetsnspostsbydate"></a>
# **DatabaseGetSnsPostsByDate**
> void DatabaseGetSnsPostsByDate (string clientId, DateTime? date = null)

取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。

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
    public class DatabaseGetSnsPostsByDateExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var date = DateTime.Parse("2013-10-20T19:20:30+01:00");  // DateTime? |  (optional) 

            try
            {
                // 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。
                apiInstance.DatabaseGetSnsPostsByDate(clientId, date);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetSnsPostsByDate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetSnsPostsByDateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。
    apiInstance.DatabaseGetSnsPostsByDateWithHttpInfo(clientId, date);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetSnsPostsByDateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **date** | **DateTime?** |  | [optional]  |

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

<a id="databasegetsnspostsbyshowname"></a>
# **DatabaseGetSnsPostsByShowName**
> void DatabaseGetSnsPostsByShowName (string clientId, string? showName = null)

按发布者显示名取朋友圈；重名会一并返回。

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
    public class DatabaseGetSnsPostsByShowNameExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var showName = "showName_example";  // string? |  (optional) 

            try
            {
                // 按发布者显示名取朋友圈；重名会一并返回。
                apiInstance.DatabaseGetSnsPostsByShowName(clientId, showName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetSnsPostsByShowName: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetSnsPostsByShowNameWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按发布者显示名取朋友圈；重名会一并返回。
    apiInstance.DatabaseGetSnsPostsByShowNameWithHttpInfo(clientId, showName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetSnsPostsByShowNameWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **showName** | **string?** |  | [optional]  |

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

<a id="databasegetsnspostsbyusername"></a>
# **DatabaseGetSnsPostsByUserName**
> void DatabaseGetSnsPostsByUserName (string clientId, string? userName = null)

取某个发布者（真实 UserName / wxid）的朋友圈。

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
    public class DatabaseGetSnsPostsByUserNameExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var userName = "userName_example";  // string? |  (optional) 

            try
            {
                // 取某个发布者（真实 UserName / wxid）的朋友圈。
                apiInstance.DatabaseGetSnsPostsByUserName(clientId, userName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseGetSnsPostsByUserName: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseGetSnsPostsByUserNameWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 取某个发布者（真实 UserName / wxid）的朋友圈。
    apiInstance.DatabaseGetSnsPostsByUserNameWithHttpInfo(clientId, userName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseGetSnsPostsByUserNameWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **userName** | **string?** |  | [optional]  |

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

<a id="databasequery"></a>
# **DatabaseQuery**
> void DatabaseQuery (string clientId, DatabaseQueryRequest? databaseQueryRequest = null)

执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。

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
    public class DatabaseQueryExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var databaseQueryRequest = new DatabaseQueryRequest?(); // DatabaseQueryRequest? |  (optional) 

            try
            {
                // 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。
                apiInstance.DatabaseQuery(clientId, databaseQueryRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseQuery: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseQueryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。
    apiInstance.DatabaseQueryWithHttpInfo(clientId, databaseQueryRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseQueryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **databaseQueryRequest** | [**DatabaseQueryRequest?**](DatabaseQueryRequest?.md) |  | [optional]  |

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

<a id="databasesearchallmessages"></a>
# **DatabaseSearchAllMessages**
> void DatabaseSearchAllMessages (string clientId, string? keyword = null, string? chatUsername = null)

按正文关键字跨所有微信号搜索消息。

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
    public class DatabaseSearchAllMessagesExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var keyword = "keyword_example";  // string? |  (optional) 
            var chatUsername = "chatUsername_example";  // string? |  (optional) 

            try
            {
                // 按正文关键字跨所有微信号搜索消息。
                apiInstance.DatabaseSearchAllMessages(clientId, keyword, chatUsername);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseSearchAllMessages: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSearchAllMessagesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按正文关键字跨所有微信号搜索消息。
    apiInstance.DatabaseSearchAllMessagesWithHttpInfo(clientId, keyword, chatUsername);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseSearchAllMessagesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **keyword** | **string?** |  | [optional]  |
| **chatUsername** | **string?** |  | [optional]  |

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

<a id="databasesearchcontacts"></a>
# **DatabaseSearchContacts**
> void DatabaseSearchContacts (string clientId, string? keyword = null)

按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。

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
    public class DatabaseSearchContactsExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var keyword = "keyword_example";  // string? |  (optional) 

            try
            {
                // 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。
                apiInstance.DatabaseSearchContacts(clientId, keyword);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseSearchContacts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSearchContactsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。
    apiInstance.DatabaseSearchContactsWithHttpInfo(clientId, keyword);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseSearchContactsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **keyword** | **string?** |  | [optional]  |

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

<a id="databasesearchmessages"></a>
# **DatabaseSearchMessages**
> void DatabaseSearchMessages (string clientId, string? keyword = null, string? chatUsername = null)

按正文关键字搜索当前微信号消息。

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
    public class DatabaseSearchMessagesExample
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
            var apiInstance = new DatabaseApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | 
            var keyword = "keyword_example";  // string? |  (optional) 
            var chatUsername = "chatUsername_example";  // string? |  (optional) 

            try
            {
                // 按正文关键字搜索当前微信号消息。
                apiInstance.DatabaseSearchMessages(clientId, keyword, chatUsername);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseApi.DatabaseSearchMessages: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSearchMessagesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // 按正文关键字搜索当前微信号消息。
    apiInstance.DatabaseSearchMessagesWithHttpInfo(clientId, keyword, chatUsername);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseApi.DatabaseSearchMessagesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** |  |  |
| **keyword** | **string?** |  | [optional]  |
| **chatUsername** | **string?** |  | [optional]  |

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

