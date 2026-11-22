# SetDoNotDisturbRequest

设置会话免打扰。who 为空时作用于当前窗口。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**setting** | **bool** |  | [optional] 

## Example

```python
from wechatauto.models.set_do_not_disturb_request import SetDoNotDisturbRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SetDoNotDisturbRequest from a JSON string
set_do_not_disturb_request_instance = SetDoNotDisturbRequest.from_json(json)
# print the JSON string representation of the object
print(SetDoNotDisturbRequest.to_json())

# convert the object into a dict
set_do_not_disturb_request_dict = set_do_not_disturb_request_instance.to_dict()
# create an instance of SetDoNotDisturbRequest from a dict
set_do_not_disturb_request_from_dict = SetDoNotDisturbRequest.from_dict(set_do_not_disturb_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


