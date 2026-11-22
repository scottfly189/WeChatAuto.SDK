# \DatabaseApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**database_get_contact**](DatabaseApi.md#database_get_contact) | **GET** /api/v1/clients/{clientId}/database/contacts/{userName} | 按 username（wxid / 群 id）取一条联系人。
[**database_get_contact_by_show_name**](DatabaseApi.md#database_get_contact_by_show_name) | **GET** /api/v1/clients/{clientId}/database/contacts/by-show-name/first | 按显示名取第一条联系人（重名时取 UserName 升序第一条）。
[**database_get_contacts**](DatabaseApi.md#database_get_contacts) | **GET** /api/v1/clients/{clientId}/database/contacts | 获取当前微信号全部联系人（不含已删除）。
[**database_get_contacts_by_show_name**](DatabaseApi.md#database_get_contacts_by_show_name) | **GET** /api/v1/clients/{clientId}/database/contacts/by-show-name | 按显示名取联系人；重名时返回多个。
[**database_get_max_sort_seq**](DatabaseApi.md#database_get_max_sort_seq) | **GET** /api/v1/clients/{clientId}/database/messages/max-sort-seq | 取当前微信号消息的最大 SortSeq（增量水位）。
[**database_get_messages**](DatabaseApi.md#database_get_messages) | **GET** /api/v1/clients/{clientId}/database/messages | 取指定会话最近 N 条消息（从新到旧）。
[**database_get_new_messages**](DatabaseApi.md#database_get_new_messages) | **GET** /api/v1/clients/{clientId}/database/messages/new | 取 SortSeq 大于指定值的新消息（从旧到新）。
[**database_get_recent_messages**](DatabaseApi.md#database_get_recent_messages) | **GET** /api/v1/clients/{clientId}/database/messages/recent | 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。
[**database_get_room_by_show_name**](DatabaseApi.md#database_get_room_by_show_name) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name/first | 按群的显示名取第一条群。
[**database_get_room_members**](DatabaseApi.md#database_get_room_members) | **GET** /api/v1/clients/{clientId}/database/rooms/{roomUsername}/members | 按群 username 取成员列表。
[**database_get_room_members_by_show_name**](DatabaseApi.md#database_get_room_members_by_show_name) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name/members | 按群的显示名取成员列表；重名时取第一条群。
[**database_get_rooms**](DatabaseApi.md#database_get_rooms) | **GET** /api/v1/clients/{clientId}/database/rooms | 获取当前微信号全部群聊。
[**database_get_rooms_by_local_type**](DatabaseApi.md#database_get_rooms_by_local_type) | **GET** /api/v1/clients/{clientId}/database/rooms/by-local-type | 按联系人 LocalType 过滤群聊（本机观察群多为 2）。
[**database_get_rooms_by_show_name**](DatabaseApi.md#database_get_rooms_by_show_name) | **GET** /api/v1/clients/{clientId}/database/rooms/by-show-name | 按群的显示名取群；重名时返回多个。
[**database_get_sns_post**](DatabaseApi.md#database_get_sns_post) | **GET** /api/v1/clients/{clientId}/database/sns/posts/{tid} | 按 tid 取一条朋友圈（含媒体、点赞、评论）。
[**database_get_sns_posts**](DatabaseApi.md#database_get_sns_posts) | **GET** /api/v1/clients/{clientId}/database/sns/posts | 取最近 N 条朋友圈（含媒体、点赞、评论）。
[**database_get_sns_posts_by_date**](DatabaseApi.md#database_get_sns_posts_by_date) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-date | 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。
[**database_get_sns_posts_by_show_name**](DatabaseApi.md#database_get_sns_posts_by_show_name) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-show-name | 按发布者显示名取朋友圈；重名会一并返回。
[**database_get_sns_posts_by_user_name**](DatabaseApi.md#database_get_sns_posts_by_user_name) | **GET** /api/v1/clients/{clientId}/database/sns/posts/by-user | 取某个发布者（真实 UserName / wxid）的朋友圈。
[**database_query**](DatabaseApi.md#database_query) | **POST** /api/v1/clients/{clientId}/database/query | 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。
[**database_search_all_messages**](DatabaseApi.md#database_search_all_messages) | **GET** /api/v1/clients/{clientId}/database/messages/search-all | 按正文关键字跨所有微信号搜索消息。
[**database_search_contacts**](DatabaseApi.md#database_search_contacts) | **GET** /api/v1/clients/{clientId}/database/contacts/search | 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。
[**database_search_messages**](DatabaseApi.md#database_search_messages) | **GET** /api/v1/clients/{clientId}/database/messages/search | 按正文关键字搜索当前微信号消息。



## database_get_contact

> database_get_contact(client_id, user_name)
按 username（wxid / 群 id）取一条联系人。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**user_name** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_contact_by_show_name

> database_get_contact_by_show_name(client_id, show_name)
按显示名取第一条联系人（重名时取 UserName 升序第一条）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**show_name** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_contacts

> database_get_contacts(client_id)
获取当前微信号全部联系人（不含已删除）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_contacts_by_show_name

> database_get_contacts_by_show_name(client_id, show_name)
按显示名取联系人；重名时返回多个。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**show_name** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_max_sort_seq

> database_get_max_sort_seq(client_id)
取当前微信号消息的最大 SortSeq（增量水位）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_messages

> database_get_messages(client_id, chat_username, count)
取指定会话最近 N 条消息（从新到旧）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**chat_username** | Option<**String**> |  |  |
**count** | Option<**i32**> |  |  |[default to 50]

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_new_messages

> database_get_new_messages(client_id, after_sort_seq)
取 SortSeq 大于指定值的新消息（从旧到新）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**after_sort_seq** | Option<**i64**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_recent_messages

> database_get_recent_messages(client_id, chat_username, count)
取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**chat_username** | Option<**String**> |  |  |
**count** | Option<**i32**> |  |  |[default to 50]

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_room_by_show_name

> database_get_room_by_show_name(client_id, show_name)
按群的显示名取第一条群。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**show_name** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_room_members

> database_get_room_members(client_id, room_username)
按群 username 取成员列表。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**room_username** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_room_members_by_show_name

> database_get_room_members_by_show_name(client_id, show_name)
按群的显示名取成员列表；重名时取第一条群。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**show_name** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_rooms

> database_get_rooms(client_id)
获取当前微信号全部群聊。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_rooms_by_local_type

> database_get_rooms_by_local_type(client_id, local_type)
按联系人 LocalType 过滤群聊（本机观察群多为 2）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**local_type** | Option<**i32**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_rooms_by_show_name

> database_get_rooms_by_show_name(client_id, show_name)
按群的显示名取群；重名时返回多个。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**show_name** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_sns_post

> database_get_sns_post(client_id, tid)
按 tid 取一条朋友圈（含媒体、点赞、评论）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**tid** | **i64** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_sns_posts

> database_get_sns_posts(client_id, limit)
取最近 N 条朋友圈（含媒体、点赞、评论）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**limit** | Option<**i32**> |  |  |[default to 20]

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_sns_posts_by_date

> database_get_sns_posts_by_date(client_id, date)
取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**date** | Option<**chrono::DateTime<chrono::FixedOffset>**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_sns_posts_by_show_name

> database_get_sns_posts_by_show_name(client_id, show_name)
按发布者显示名取朋友圈；重名会一并返回。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**show_name** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_get_sns_posts_by_user_name

> database_get_sns_posts_by_user_name(client_id, user_name)
取某个发布者（真实 UserName / wxid）的朋友圈。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**user_name** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_query

> database_query(client_id, database_query_request)
执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**database_query_request** | Option<[**DatabaseQueryRequest**](DatabaseQueryRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_search_all_messages

> database_search_all_messages(client_id, keyword, chat_username)
按正文关键字跨所有微信号搜索消息。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**keyword** | Option<**String**> |  |  |
**chat_username** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_search_contacts

> database_search_contacts(client_id, keyword)
按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**keyword** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## database_search_messages

> database_search_messages(client_id, keyword, chat_username)
按正文关键字搜索当前微信号消息。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**keyword** | Option<**String**> |  |  |
**chat_username** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

