# \ConversationsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**conversations_get_all_conversations**](ConversationsApi.md#conversations_get_all_conversations) | **GET** /api/v1/clients/{clientId}/conversations | 获取会话列表所有会话的标题。
[**conversations_get_visible_conversations**](ConversationsApi.md#conversations_get_visible_conversations) | **GET** /api/v1/clients/{clientId}/conversations/visible | 获取可见会话列表（含名称、未读消息数、头像等）。
[**conversations_get_visible_titles**](ConversationsApi.md#conversations_get_visible_titles) | **GET** /api/v1/clients/{clientId}/conversations/visible-titles | 获取会话列表可见会话的标题。
[**conversations_locate**](ConversationsApi.md#conversations_locate) | **POST** /api/v1/clients/{clientId}/conversations/locate | 定位会话，滚动会话列表使指定会话可见。
[**conversations_search**](ConversationsApi.md#conversations_search) | **POST** /api/v1/clients/{clientId}/conversations/search | 搜索好友/群聊。
[**conversations_set_do_not_disturb**](ConversationsApi.md#conversations_set_do_not_disturb) | **POST** /api/v1/clients/{clientId}/conversations/set-do-not-disturb | 设置会话消息免打扰。
[**conversations_set_top_most**](ConversationsApi.md#conversations_set_top_most) | **POST** /api/v1/clients/{clientId}/conversations/set-top-most | 设置会话置顶。



## conversations_get_all_conversations

> conversations_get_all_conversations(client_id)
获取会话列表所有会话的标题。

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


## conversations_get_visible_conversations

> conversations_get_visible_conversations(client_id)
获取可见会话列表（含名称、未读消息数、头像等）。

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


## conversations_get_visible_titles

> conversations_get_visible_titles(client_id)
获取会话列表可见会话的标题。

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


## conversations_locate

> conversations_locate(client_id, locate_conversation_request)
定位会话，滚动会话列表使指定会话可见。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**locate_conversation_request** | Option<[**LocateConversationRequest**](LocateConversationRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## conversations_search

> conversations_search(client_id, search_conversation_request)
搜索好友/群聊。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**search_conversation_request** | Option<[**SearchConversationRequest**](SearchConversationRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## conversations_set_do_not_disturb

> conversations_set_do_not_disturb(client_id, set_do_not_disturb_request)
设置会话消息免打扰。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**set_do_not_disturb_request** | Option<[**SetDoNotDisturbRequest**](SetDoNotDisturbRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## conversations_set_top_most

> conversations_set_top_most(client_id, set_top_most_request)
设置会话置顶。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**set_top_most_request** | Option<[**SetTopMostRequest**](SetTopMostRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

