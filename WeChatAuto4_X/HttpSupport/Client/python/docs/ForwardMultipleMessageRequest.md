# ForwardMultipleMessageRequest

转发多条消息。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**to** | **List[str]** |  | [optional] 
**f_type** | [**ForwardMessageTypeEnums**](ForwardMessageTypeEnums.md) |  | [optional] 
**row_count** | **int** |  | [optional] 

## Example

```python
from wechatauto.models.forward_multiple_message_request import ForwardMultipleMessageRequest

# TODO update the JSON string below
json = "{}"
# create an instance of ForwardMultipleMessageRequest from a JSON string
forward_multiple_message_request_instance = ForwardMultipleMessageRequest.from_json(json)
# print the JSON string representation of the object
print(ForwardMultipleMessageRequest.to_json())

# convert the object into a dict
forward_multiple_message_request_dict = forward_multiple_message_request_instance.to_dict()
# create an instance of ForwardMultipleMessageRequest from a dict
forward_multiple_message_request_from_dict = ForwardMultipleMessageRequest.from_dict(forward_multiple_message_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


