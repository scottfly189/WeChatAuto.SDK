# \FriendsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**friends_add**](FriendsApi.md#friends_add) | **POST** /api/v1/clients/{clientId}/friends/add | 通过手机号码/微信号查找并添加好友。
[**friends_add_or_update_friend**](FriendsApi.md#friends_add_or_update_friend) | **POST** /api/v1/clients/{clientId}/friends/cache | 手动新增或修改缓存中的好友对象。
[**friends_close_add_window**](FriendsApi.md#friends_close_add_window) | **POST** /api/v1/clients/{clientId}/friends/add-window/close | 关闭新增朋友窗口。
[**friends_get_all_friend_names**](FriendsApi.md#friends_get_all_friend_names) | **GET** /api/v1/clients/{clientId}/friends/names | 获取所有好友名称列表（有备注优先备注名）。
[**friends_get_all_friends**](FriendsApi.md#friends_get_all_friends) | **GET** /api/v1/clients/{clientId}/friends | 获取所有好友的信息列表（默认优先从缓存读取）。
[**friends_get_friend_by_wxid_from_cache**](FriendsApi.md#friends_get_friend_by_wxid_from_cache) | **GET** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存获取好友信息。
[**friends_get_friend_from_cache**](FriendsApi.md#friends_get_friend_from_cache) | **GET** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存获取单个好友信息。
[**friends_get_friend_list_from_cache**](FriendsApi.md#friends_get_friend_list_from_cache) | **GET** /api/v1/clients/{clientId}/friends/cache | 获取缓存中的全部好友信息列表。
[**friends_get_friends_from_cache**](FriendsApi.md#friends_get_friends_from_cache) | **GET** /api/v1/clients/{clientId}/friends/cache/by-name/{who} | 按名称从缓存获取好友列表（适用于昵称重复场景）。
[**friends_open_add_window**](FriendsApi.md#friends_open_add_window) | **POST** /api/v1/clients/{clientId}/friends/add-window/open | 打开新增朋友窗口。
[**friends_pass_all**](FriendsApi.md#friends_pass_all) | **POST** /api/v1/clients/{clientId}/friends/pass-all | 立即批量通过当前待处理的新好友申请（不启动持续监听）。
[**friends_pause_auto_accept**](FriendsApi.md#friends_pause_auto_accept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/pause | 暂停新好友自动通过监听。
[**friends_remove_friend**](FriendsApi.md#friends_remove_friend) | **DELETE** /api/v1/clients/{clientId}/friends/{nickName} | 从通讯录移除好友。
[**friends_remove_friend_by_wxid_from_cache**](FriendsApi.md#friends_remove_friend_by_wxid_from_cache) | **DELETE** /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存移除好友。
[**friends_remove_friend_from_cache**](FriendsApi.md#friends_remove_friend_from_cache) | **DELETE** /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存移除好友。
[**friends_resume_auto_accept**](FriendsApi.md#friends_resume_auto_accept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/resume | 恢复新好友自动通过监听。
[**friends_start_auto_accept**](FriendsApi.md#friends_start_auto_accept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/start | 启动新好友自动通过监听（按关键词/后缀/标签规则）。
[**friends_stop_auto_accept**](FriendsApi.md#friends_stop_auto_accept) | **POST** /api/v1/clients/{clientId}/friends/auto-accept/stop | 停止新好友自动通过监听。



## friends_add

> friends_add(client_id, add_friends_request)
通过手机号码/微信号查找并添加好友。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**add_friends_request** | Option<[**AddFriendsRequest**](AddFriendsRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_add_or_update_friend

> friends_add_or_update_friend(client_id, friend_info)
手动新增或修改缓存中的好友对象。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**friend_info** | Option<[**FriendInfo**](FriendInfo.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_close_add_window

> friends_close_add_window(client_id)
关闭新增朋友窗口。

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


## friends_get_all_friend_names

> friends_get_all_friend_names(client_id)
获取所有好友名称列表（有备注优先备注名）。

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


## friends_get_all_friends

> friends_get_all_friends(client_id, from_cache)
获取所有好友的信息列表（默认优先从缓存读取）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**from_cache** | Option<**bool**> |  |  |[default to true]

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_get_friend_by_wxid_from_cache

> friends_get_friend_by_wxid_from_cache(client_id, wxid)
按 wxid 从缓存获取好友信息。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**wxid** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_get_friend_from_cache

> friends_get_friend_from_cache(client_id, who)
按名称从缓存获取单个好友信息。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**who** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_get_friend_list_from_cache

> friends_get_friend_list_from_cache(client_id)
获取缓存中的全部好友信息列表。

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


## friends_get_friends_from_cache

> friends_get_friends_from_cache(client_id, who)
按名称从缓存获取好友列表（适用于昵称重复场景）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**who** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_open_add_window

> friends_open_add_window(client_id)
打开新增朋友窗口。

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


## friends_pass_all

> friends_pass_all(client_id, passed_all_new_friend_request)
立即批量通过当前待处理的新好友申请（不启动持续监听）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**passed_all_new_friend_request** | Option<[**PassedAllNewFriendRequest**](PassedAllNewFriendRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_pause_auto_accept

> friends_pause_auto_accept(client_id)
暂停新好友自动通过监听。

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


## friends_remove_friend

> friends_remove_friend(client_id, nick_name)
从通讯录移除好友。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**nick_name** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_remove_friend_by_wxid_from_cache

> friends_remove_friend_by_wxid_from_cache(client_id, wxid)
按 wxid 从缓存移除好友。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**wxid** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_remove_friend_from_cache

> friends_remove_friend_from_cache(client_id, who)
按名称从缓存移除好友。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**who** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_resume_auto_accept

> friends_resume_auto_accept(client_id)
恢复新好友自动通过监听。

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


## friends_start_auto_accept

> friends_start_auto_accept(client_id, passed_all_new_friend_request)
启动新好友自动通过监听（按关键词/后缀/标签规则）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**passed_all_new_friend_request** | Option<[**PassedAllNewFriendRequest**](PassedAllNewFriendRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## friends_stop_auto_accept

> friends_stop_auto_accept(client_id)
停止新好友自动通过监听。

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

