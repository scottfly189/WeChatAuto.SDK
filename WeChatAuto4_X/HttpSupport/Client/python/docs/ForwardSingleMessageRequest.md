# ForwardSingleMessageRequest

转发单条消息（message 与 chat_simple_message 二选一）。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**message** | **str** |  | [optional] 
**chat_simple_message** | [**ChatSimpleMessage**](ChatSimpleMessage.md) |  | [optional] 
**to** | **List[str]** |  | [optional] 
**prev_scroll_number** | **int** |  | [optional] 

## Example

```python
from wechatauto.models.forward_single_message_request import ForwardSingleMessageRequest

# TODO update the JSON string below
json = "{}"
# create an instance of ForwardSingleMessageRequest from a JSON string
forward_single_message_request_instance = ForwardSingleMessageRequest.from_json(json)
# print the JSON string representation of the object
print(ForwardSingleMessageRequest.to_json())

# convert the object into a dict
forward_single_message_request_dict = forward_single_message_request_instance.to_dict()
# create an instance of ForwardSingleMessageRequest from a dict
forward_single_message_request_from_dict = ForwardSingleMessageRequest.from_dict(forward_single_message_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


