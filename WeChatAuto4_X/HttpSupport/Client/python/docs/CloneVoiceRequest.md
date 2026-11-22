# CloneVoiceRequest

声音复刻。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**api_key** | **str** |  | [optional] 
**audio_url** | **str** |  | [optional] 
**target_model** | **str** |  | [optional] 
**prefix** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.clone_voice_request import CloneVoiceRequest

# TODO update the JSON string below
json = "{}"
# create an instance of CloneVoiceRequest from a JSON string
clone_voice_request_instance = CloneVoiceRequest.from_json(json)
# print the JSON string representation of the object
print(CloneVoiceRequest.to_json())

# convert the object into a dict
clone_voice_request_dict = clone_voice_request_instance.to_dict()
# create an instance of CloneVoiceRequest from a dict
clone_voice_request_from_dict = CloneVoiceRequest.from_dict(clone_voice_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


