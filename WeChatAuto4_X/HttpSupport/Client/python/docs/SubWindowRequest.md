# SubWindowRequest

打开/关闭指定好友或群的子窗口。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.sub_window_request import SubWindowRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SubWindowRequest from a JSON string
sub_window_request_instance = SubWindowRequest.from_json(json)
# print the JSON string representation of the object
print(SubWindowRequest.to_json())

# convert the object into a dict
sub_window_request_dict = sub_window_request_instance.to_dict()
# create an instance of SubWindowRequest from a dict
sub_window_request_from_dict = SubWindowRequest.from_dict(sub_window_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


