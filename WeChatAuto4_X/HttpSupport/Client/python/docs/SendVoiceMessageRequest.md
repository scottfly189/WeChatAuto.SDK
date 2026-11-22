# SendVoiceMessageRequest

发送语音消息。who 为空时发送给当前焦点聊天窗口。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**file_path** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.send_voice_message_request import SendVoiceMessageRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SendVoiceMessageRequest from a JSON string
send_voice_message_request_instance = SendVoiceMessageRequest.from_json(json)
# print the JSON string representation of the object
print(SendVoiceMessageRequest.to_json())

# convert the object into a dict
send_voice_message_request_dict = send_voice_message_request_instance.to_dict()
# create an instance of SendVoiceMessageRequest from a dict
send_voice_message_request_from_dict = SendVoiceMessageRequest.from_dict(send_voice_message_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


