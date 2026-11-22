# RemoveMomentsRequest

删除自己发的朋友圈（按文字内容）。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**content** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.remove_moments_request import RemoveMomentsRequest

# TODO update the JSON string below
json = "{}"
# create an instance of RemoveMomentsRequest from a JSON string
remove_moments_request_instance = RemoveMomentsRequest.from_json(json)
# print the JSON string representation of the object
print(RemoveMomentsRequest.to_json())

# convert the object into a dict
remove_moments_request_dict = remove_moments_request_instance.to_dict()
# create an instance of RemoveMomentsRequest from a dict
remove_moments_request_from_dict = RemoveMomentsRequest.from_dict(remove_moments_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


