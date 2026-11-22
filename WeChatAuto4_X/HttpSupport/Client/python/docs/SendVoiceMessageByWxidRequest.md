# SendVoiceMessageByWxidRequest

按联系人 UserName 发送语音文件。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**user_name** | **str** |  | [optional] 
**file_path** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.send_voice_message_by_wxid_request import SendVoiceMessageByWxidRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SendVoiceMessageByWxidRequest from a JSON string
send_voice_message_by_wxid_request_instance = SendVoiceMessageByWxidRequest.from_json(json)
# print the JSON string representation of the object
print(SendVoiceMessageByWxidRequest.to_json())

# convert the object into a dict
send_voice_message_by_wxid_request_dict = send_voice_message_by_wxid_request_instance.to_dict()
# create an instance of SendVoiceMessageByWxidRequest from a dict
send_voice_message_by_wxid_request_from_dict = SendVoiceMessageByWxidRequest.from_dict(send_voice_message_by_wxid_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


