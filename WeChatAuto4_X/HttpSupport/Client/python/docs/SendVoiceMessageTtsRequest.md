# SendVoiceMessageTtsRequest

文字转语音发送。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**api_key** | **str** |  | [optional] 
**message** | **str** |  | [optional] 
**options** | [**VoiceOptions**](VoiceOptions.md) |  | [optional] 
**optimize_with_llm** | **bool** |  | [optional] 

## Example

```python
from wechatauto.models.send_voice_message_tts_request import SendVoiceMessageTtsRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SendVoiceMessageTtsRequest from a JSON string
send_voice_message_tts_request_instance = SendVoiceMessageTtsRequest.from_json(json)
# print the JSON string representation of the object
print(SendVoiceMessageTtsRequest.to_json())

# convert the object into a dict
send_voice_message_tts_request_dict = send_voice_message_tts_request_instance.to_dict()
# create an instance of SendVoiceMessageTtsRequest from a dict
send_voice_message_tts_request_from_dict = SendVoiceMessageTtsRequest.from_dict(send_voice_message_tts_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


