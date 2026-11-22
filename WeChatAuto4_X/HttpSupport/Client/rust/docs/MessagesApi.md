# \MessagesApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**messages_clone_voice**](MessagesApi.md#messages_clone_voice) | **POST** /api/v1/clients/{clientId}/messages/clone-voice | 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。
[**messages_focus_input**](MessagesApi.md#messages_focus_input) | **POST** /api/v1/clients/{clientId}/messages/focus-input | 聚焦当前窗口的消息输入区。
[**messages_forward_multiple**](MessagesApi.md#messages_forward_multiple) | **POST** /api/v1/clients/{clientId}/messages/forward-multiple | 转发多条消息（默认合并转发）。
[**messages_forward_single**](MessagesApi.md#messages_forward_single) | **POST** /api/v1/clients/{clientId}/messages/forward-single | 转发单条消息（message 与 chat_simple_message 二选一）。
[**messages_get_history**](MessagesApi.md#messages_get_history) | **GET** /api/v1/clients/{clientId}/messages/history | 获取聊天历史（支持按日期或时间段查询）。
[**messages_get_only_title**](MessagesApi.md#messages_get_only_title) | **GET** /api/v1/clients/{clientId}/messages/only-title | 获取当前聊天窗口的标题名称。
[**messages_get_title**](MessagesApi.md#messages_get_title) | **GET** /api/v1/clients/{clientId}/messages/title | 获取当前聊天窗口的标题对象。
[**messages_pull**](MessagesApi.md#messages_pull) | **POST** /api/v1/clients/{clientId}/messages/pull | 轮询拉取指定会话的新消息（用于自己实现轮询）。
[**messages_reference**](MessagesApi.md#messages_reference) | **POST** /api/v1/clients/{clientId}/messages/reference | 引用当前窗口的好友消息。
[**messages_reference_last**](MessagesApi.md#messages_reference_last) | **POST** /api/v1/clients/{clientId}/messages/reference-last | 引用当前窗口最后一条消息。
[**messages_send**](MessagesApi.md#messages_send) | **POST** /api/v1/clients/{clientId}/messages/send | 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。
[**messages_send_and_close**](MessagesApi.md#messages_send_and_close) | **POST** /api/v1/clients/{clientId}/messages/send-and-close | 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。
[**messages_send_by_wxid**](MessagesApi.md#messages_send_by_wxid) | **POST** /api/v1/clients/{clientId}/messages/send-by-wxid | 按联系人 UserName（wxid / 群 id）发送文本。
[**messages_send_emoji**](MessagesApi.md#messages_send_emoji) | **POST** /api/v1/clients/{clientId}/messages/send-emoji | 发送表情（按名称/描述/索引）。
[**messages_send_emoji_and_close**](MessagesApi.md#messages_send_emoji_and_close) | **POST** /api/v1/clients/{clientId}/messages/send-emoji-and-close | 发送表情后关闭微信窗口（按名称/描述/索引）。
[**messages_send_emoji_by_wxid**](MessagesApi.md#messages_send_emoji_by_wxid) | **POST** /api/v1/clients/{clientId}/messages/send-emoji-by-wxid | 按联系人 UserName（wxid / 群 id）发送表情。
[**messages_send_file**](MessagesApi.md#messages_send_file) | **POST** /api/v1/clients/{clientId}/messages/send-file | 发送文件。
[**messages_send_file_and_close**](MessagesApi.md#messages_send_file_and_close) | **POST** /api/v1/clients/{clientId}/messages/send-file-and-close | 发送文件后关闭微信窗口。
[**messages_send_file_by_wxid**](MessagesApi.md#messages_send_file_by_wxid) | **POST** /api/v1/clients/{clientId}/messages/send-file-by-wxid | 按联系人 UserName（wxid / 群 id）发送文件。
[**messages_send_video_chat**](MessagesApi.md#messages_send_video_chat) | **POST** /api/v1/clients/{clientId}/messages/send-video-chat | 发起单人视频聊天。
[**messages_send_voice_chat**](MessagesApi.md#messages_send_voice_chat) | **POST** /api/v1/clients/{clientId}/messages/send-voice-chat | 发起单人语音聊天。
[**messages_send_voice_chats**](MessagesApi.md#messages_send_voice_chats) | **POST** /api/v1/clients/{clientId}/messages/send-voice-chats | 发起多人语音聊天（群聊）。
[**messages_send_voice_message**](MessagesApi.md#messages_send_voice_message) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message | 发送语音消息（需虚拟声卡 Cable input/Cable output）。
[**messages_send_voice_message_and_close**](MessagesApi.md#messages_send_voice_message_and_close) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-and-close | 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。
[**messages_send_voice_message_by_wxid**](MessagesApi.md#messages_send_voice_message_by_wxid) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-by-wxid | 按联系人 UserName（wxid / 群 id）发送语音文件。
[**messages_send_voice_message_tts**](MessagesApi.md#messages_send_voice_message_tts) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts | 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。
[**messages_send_voice_message_tts_and_close**](MessagesApi.md#messages_send_voice_message_tts_and_close) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts-and-close | 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。
[**messages_send_voice_message_tts_by_wxid**](MessagesApi.md#messages_send_voice_message_tts_by_wxid) | **POST** /api/v1/clients/{clientId}/messages/send-voice-message-tts-by-wxid | 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。
[**messages_tap**](MessagesApi.md#messages_tap) | **POST** /api/v1/clients/{clientId}/messages/tap | 拍一拍当前聊天窗口的好友。



## messages_clone_voice

> messages_clone_voice(client_id, clone_voice_request)
声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**clone_voice_request** | Option<[**CloneVoiceRequest**](CloneVoiceRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_focus_input

> messages_focus_input(client_id)
聚焦当前窗口的消息输入区。

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


## messages_forward_multiple

> messages_forward_multiple(client_id, forward_multiple_message_request)
转发多条消息（默认合并转发）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**forward_multiple_message_request** | Option<[**ForwardMultipleMessageRequest**](ForwardMultipleMessageRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_forward_single

> messages_forward_single(client_id, forward_single_message_request)
转发单条消息（message 与 chat_simple_message 二选一）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**forward_single_message_request** | Option<[**ForwardSingleMessageRequest**](ForwardSingleMessageRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_get_history

> messages_get_history(client_id, who, date, start_date, end_date)
获取聊天历史（支持按日期或时间段查询）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**who** | Option<**String**> |  |  |
**date** | Option<**chrono::DateTime<chrono::FixedOffset>**> |  |  |
**start_date** | Option<**chrono::DateTime<chrono::FixedOffset>**> |  |  |
**end_date** | Option<**chrono::DateTime<chrono::FixedOffset>**> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_get_only_title

> messages_get_only_title(client_id)
获取当前聊天窗口的标题名称。

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


## messages_get_title

> messages_get_title(client_id)
获取当前聊天窗口的标题对象。

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


## messages_pull

> messages_pull(client_id, pull_messages_request)
轮询拉取指定会话的新消息（用于自己实现轮询）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**pull_messages_request** | Option<[**PullMessagesRequest**](PullMessagesRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_reference

> messages_reference(client_id, reference_request)
引用当前窗口的好友消息。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**reference_request** | Option<[**ReferenceRequest**](ReferenceRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_reference_last

> messages_reference_last(client_id)
引用当前窗口最后一条消息。

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


## messages_send

> messages_send(client_id, send_message_request)
发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_message_request** | Option<[**SendMessageRequest**](SendMessageRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_and_close

> messages_send_and_close(client_id, send_message_request)
发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_message_request** | Option<[**SendMessageRequest**](SendMessageRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_by_wxid

> messages_send_by_wxid(client_id, send_message_by_wxid_request)
按联系人 UserName（wxid / 群 id）发送文本。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_message_by_wxid_request** | Option<[**SendMessageByWxidRequest**](SendMessageByWxidRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_emoji

> messages_send_emoji(client_id, send_emoji_request)
发送表情（按名称/描述/索引）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_emoji_request** | Option<[**SendEmojiRequest**](SendEmojiRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_emoji_and_close

> messages_send_emoji_and_close(client_id, send_emoji_request)
发送表情后关闭微信窗口（按名称/描述/索引）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_emoji_request** | Option<[**SendEmojiRequest**](SendEmojiRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_emoji_by_wxid

> messages_send_emoji_by_wxid(client_id, send_emoji_by_wxid_request)
按联系人 UserName（wxid / 群 id）发送表情。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_emoji_by_wxid_request** | Option<[**SendEmojiByWxidRequest**](SendEmojiByWxidRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_file

> messages_send_file(client_id, send_file_request)
发送文件。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_file_request** | Option<[**SendFileRequest**](SendFileRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_file_and_close

> messages_send_file_and_close(client_id, send_file_request)
发送文件后关闭微信窗口。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_file_request** | Option<[**SendFileRequest**](SendFileRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_file_by_wxid

> messages_send_file_by_wxid(client_id, send_file_by_wxid_request)
按联系人 UserName（wxid / 群 id）发送文件。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_file_by_wxid_request** | Option<[**SendFileByWxidRequest**](SendFileByWxidRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_video_chat

> messages_send_video_chat(client_id, who_request)
发起单人视频聊天。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**who_request** | Option<[**WhoRequest**](WhoRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_voice_chat

> messages_send_voice_chat(client_id, who_request)
发起单人语音聊天。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**who_request** | Option<[**WhoRequest**](WhoRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_voice_chats

> messages_send_voice_chats(client_id, send_voice_chats_request)
发起多人语音聊天（群聊）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_voice_chats_request** | Option<[**SendVoiceChatsRequest**](SendVoiceChatsRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_voice_message

> messages_send_voice_message(client_id, send_voice_message_request)
发送语音消息（需虚拟声卡 Cable input/Cable output）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_voice_message_request** | Option<[**SendVoiceMessageRequest**](SendVoiceMessageRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_voice_message_and_close

> messages_send_voice_message_and_close(client_id, send_voice_message_request)
发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_voice_message_request** | Option<[**SendVoiceMessageRequest**](SendVoiceMessageRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_voice_message_by_wxid

> messages_send_voice_message_by_wxid(client_id, send_voice_message_by_wxid_request)
按联系人 UserName（wxid / 群 id）发送语音文件。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_voice_message_by_wxid_request** | Option<[**SendVoiceMessageByWxidRequest**](SendVoiceMessageByWxidRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_voice_message_tts

> messages_send_voice_message_tts(client_id, send_voice_message_tts_request)
文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_voice_message_tts_request** | Option<[**SendVoiceMessageTtsRequest**](SendVoiceMessageTtsRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_voice_message_tts_and_close

> messages_send_voice_message_tts_and_close(client_id, send_voice_message_tts_request)
文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_voice_message_tts_request** | Option<[**SendVoiceMessageTtsRequest**](SendVoiceMessageTtsRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_send_voice_message_tts_by_wxid

> messages_send_voice_message_tts_by_wxid(client_id, send_voice_message_tts_by_wxid_request)
按联系人 UserName（wxid / 群 id）用 TTS 发送语音。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**send_voice_message_tts_by_wxid_request** | Option<[**SendVoiceMessageTtsByWxidRequest**](SendVoiceMessageTtsByWxidRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


## messages_tap

> messages_tap(client_id, tap_request)
拍一拍当前聊天窗口的好友。

### Parameters


Name | Type | Description  | Required | Notes
------------- | ------------- | ------------- | ------------- | -------------
**client_id** | **String** |  | [required] |
**tap_request** | Option<[**TapRequest**](TapRequest.md)> |  |  |

### Return type

 (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

