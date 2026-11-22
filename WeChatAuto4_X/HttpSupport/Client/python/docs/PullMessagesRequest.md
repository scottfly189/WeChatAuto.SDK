# PullMessagesRequest

轮询拉取消息。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**max_fetch_number** | **int** |  | [optional] 
**include_system_message** | **bool** |  | [optional] 

## Example

```python
from wechatauto.models.pull_messages_request import PullMessagesRequest

# TODO update the JSON string below
json = "{}"
# create an instance of PullMessagesRequest from a JSON string
pull_messages_request_instance = PullMessagesRequest.from_json(json)
# print the JSON string representation of the object
print(PullMessagesRequest.to_json())

# convert the object into a dict
pull_messages_request_dict = pull_messages_request_instance.to_dict()
# create an instance of PullMessagesRequest from a dict
pull_messages_request_from_dict = PullMessagesRequest.from_dict(pull_messages_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


