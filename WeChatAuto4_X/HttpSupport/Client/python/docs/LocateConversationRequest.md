# LocateConversationRequest

定位会话。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**title** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.locate_conversation_request import LocateConversationRequest

# TODO update the JSON string below
json = "{}"
# create an instance of LocateConversationRequest from a JSON string
locate_conversation_request_instance = LocateConversationRequest.from_json(json)
# print the JSON string representation of the object
print(LocateConversationRequest.to_json())

# convert the object into a dict
locate_conversation_request_dict = locate_conversation_request_instance.to_dict()
# create an instance of LocateConversationRequest from a dict
locate_conversation_request_from_dict = LocateConversationRequest.from_dict(locate_conversation_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


