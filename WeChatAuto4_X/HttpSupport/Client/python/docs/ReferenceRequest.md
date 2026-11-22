# ReferenceRequest

引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**message** | [**ChatSimpleMessage**](ChatSimpleMessage.md) |  | [optional] 
**who** | **str** |  | [optional] 
**message_content** | **str** |  | [optional] 
**prev_scroll_number** | **int** |  | [optional] 

## Example

```python
from wechatauto.models.reference_request import ReferenceRequest

# TODO update the JSON string below
json = "{}"
# create an instance of ReferenceRequest from a JSON string
reference_request_instance = ReferenceRequest.from_json(json)
# print the JSON string representation of the object
print(ReferenceRequest.to_json())

# convert the object into a dict
reference_request_dict = reference_request_instance.to_dict()
# create an instance of ReferenceRequest from a dict
reference_request_from_dict = ReferenceRequest.from_dict(reference_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


