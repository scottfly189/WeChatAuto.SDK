# SendVoiceChatsRequest

发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**partner** | **List[str]** |  | [optional] 

## Example

```python
from wechatauto.models.send_voice_chats_request import SendVoiceChatsRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SendVoiceChatsRequest from a JSON string
send_voice_chats_request_instance = SendVoiceChatsRequest.from_json(json)
# print the JSON string representation of the object
print(SendVoiceChatsRequest.to_json())

# convert the object into a dict
send_voice_chats_request_dict = send_voice_chats_request_instance.to_dict()
# create an instance of SendVoiceChatsRequest from a dict
send_voice_chats_request_from_dict = SendVoiceChatsRequest.from_dict(send_voice_chats_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


