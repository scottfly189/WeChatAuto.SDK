# SendVoiceMessageTtsByWxidRequest

按联系人 UserName 用 TTS 发送语音。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**user_name** | **str** |  | [optional] 
**api_key** | **str** |  | [optional] 
**message** | **str** |  | [optional] 
**options** | [**VoiceOptions**](VoiceOptions.md) |  | [optional] 
**optimize_with_llm** | **bool** |  | [optional] 

## Example

```python
from wechatauto.models.send_voice_message_tts_by_wxid_request import SendVoiceMessageTtsByWxidRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SendVoiceMessageTtsByWxidRequest from a JSON string
send_voice_message_tts_by_wxid_request_instance = SendVoiceMessageTtsByWxidRequest.from_json(json)
# print the JSON string representation of the object
print(SendVoiceMessageTtsByWxidRequest.to_json())

# convert the object into a dict
send_voice_message_tts_by_wxid_request_dict = send_voice_message_tts_by_wxid_request_instance.to_dict()
# create an instance of SendVoiceMessageTtsByWxidRequest from a dict
send_voice_message_tts_by_wxid_request_from_dict = SendVoiceMessageTtsByWxidRequest.from_dict(send_voice_message_tts_by_wxid_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


