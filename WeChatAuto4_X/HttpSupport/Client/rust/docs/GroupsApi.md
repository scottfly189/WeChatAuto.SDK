# \GroupsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**groups_add_members**](GroupsApi.md#groups_add_members) | **POST** /api/v1/clients/{clientId}/groups/members | 添加群聊成员（适用于自有群）。
[**groups_add_members_to_friends**](GroupsApi.md#groups_add_members_to_friends) | **POST** /api/v1/clients/{clientId}/groups/add-members-to-friends | 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。
[**groups_change_memo**](GroupsApi.md#groups_change_memo) | **PUT** /api/v1/clients/{clientId}/groups/memo | 修改群备注（仅自己可见）。
[**groups_change_name**](GroupsApi.md#groups_change_name) | **PUT** /api/v1/clients/{clientId}/groups/name | 修改群名（适用于自有群）。
[**groups_change_nick_name**](GroupsApi.md#groups_change_nick_name) | **PUT** /api/v1/clients/{clientId}/groups/nickname | 修改自己在群中的昵称。
[**groups_create**](GroupsApi.md#groups_create) | **POST** /api/v1/clients/{clientId}/groups | 创建群聊，已存在则打开群聊。
[**groups_get_members**](GroupsApi.md#groups_get_members) | **GET** /api/v1/clients/{clientId}/groups/members | 获取群聊成员列表。
[**groups_get_owner**](GroupsApi.md#groups_get_owner) | **GET** /api/v1/clients/{clientId}/groups/{groupName}/owner | 获取群主昵称。
[**groups_invite**](GroupsApi.md#groups_invite) | **POST** /api/v1/clients/{clientId}/groups/invite | 邀请群聊成员（适用于外部群）。
[**groups_is_owner**](GroupsApi.md#groups_is_owner) | **GET** /api/v1/clients/{clientId}/groups/{groupName}/is-owner | 判断群聊是否为自有群。
[**groups_quit**](GroupsApi.md#groups_quit) | **DELETE** /api/v1/clients/{clientId}/groups/{groupName} | 退出群聊。
[**groups_remove_members**](GroupsApi.md#groups_remove_members) | **DELETE** /api/v1/clients/{clientId}/groups/members | 移除群聊成员（适用于自有群）。
[**groups_update_notice**](GroupsApi.md#groups_update_notice) | **PUT** /api/v1/clients/{clientId}/groups/notice | 更新群公告（仅适用于自有群）。



## groups_add_members

> groups_add_members(client_id, group_member_request)
添加群聊成员（适用于自有群）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**group_member_request** | Option<[**GroupMemberRequest**](GroupMemberRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_add_members_to_friends

> groups_add_members_to_friends(client_id, add_group_member_to_friends_request)
把群聊成员加为自己的好友（适用于外部群，注意微信风控）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**add_group_member_to_friends_request** | Option<[**AddGroupMemberToFriendsRequest**](AddGroupMemberToFriendsRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_change_memo

> groups_change_memo(client_id, change_group_memo_request)
修改群备注（仅自己可见）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**change_group_memo_request** | Option<[**ChangeGroupMemoRequest**](ChangeGroupMemoRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_change_name

> groups_change_name(client_id, rename_group_request)
修改群名（适用于自有群）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**rename_group_request** | Option<[**RenameGroupRequest**](RenameGroupRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_change_nick_name

> groups_change_nick_name(client_id, change_group_nick_name_request)
修改自己在群中的昵称。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**change_group_nick_name_request** | Option<[**ChangeGroupNickNameRequest**](ChangeGroupNickNameRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_create

> groups_create(client_id, create_group_request)
创建群聊，已存在则打开群聊。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**create_group_request** | Option<[**CreateGroupRequest**](CreateGroupRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_get_members

> groups_get_members(client_id, group_name)
获取群聊成员列表。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**group_name** | Option<**String**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_get_owner

> groups_get_owner(client_id, group_name)
获取群主昵称。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**group_name** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_invite

> groups_invite(client_id, invite_group_member_request)
邀请群聊成员（适用于外部群）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**invite_group_member_request** | Option<[**InviteGroupMemberRequest**](InviteGroupMemberRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_is_owner

> groups_is_owner(client_id, group_name)
判断群聊是否为自有群。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**group_name** | **String** |  | [required] |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_quit

> groups_quit(client_id, group_name, clear_history)
退出群聊。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**group_name** | **String** |  | [required] |
**clear_history** | Option<**bool**> |  |  |[default to true]

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_remove_members

> groups_remove_members(client_id, group_member_request)
移除群聊成员（适用于自有群）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**group_member_request** | Option<[**GroupMemberRequest**](GroupMemberRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## groups_update_notice

> groups_update_notice(client_id, update_group_notice_request)
更新群公告（仅适用于自有群）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**update_group_notice_request** | Option<[**UpdateGroupNoticeRequest**](UpdateGroupNoticeRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

