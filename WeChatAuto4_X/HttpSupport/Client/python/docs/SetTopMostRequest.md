# SetTopMostRequest

设置会话置顶。who 为空时作用于当前窗口。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**setting** | **bool** |  | [optional] 

## Example

```python
from wechatauto.models.set_top_most_request import SetTopMostRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SetTopMostRequest from a JSON string
set_top_most_request_instance = SetTopMostRequest.from_json(json)
# print the JSON string representation of the object
print(SetTopMostRequest.to_json())

# convert the object into a dict
set_top_most_request_dict = set_top_most_request_instance.to_dict()
# create an instance of SetTopMostRequest from a dict
set_top_most_request_from_dict = SetTopMostRequest.from_dict(set_top_most_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


