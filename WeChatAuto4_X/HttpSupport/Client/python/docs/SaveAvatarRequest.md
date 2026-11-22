# SaveAvatarRequest

保存头像到指定路径。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**path** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.save_avatar_request import SaveAvatarRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SaveAvatarRequest from a JSON string
save_avatar_request_instance = SaveAvatarRequest.from_json(json)
# print the JSON string representation of the object
print(SaveAvatarRequest.to_json())

# convert the object into a dict
save_avatar_request_dict = save_avatar_request_instance.to_dict()
# create an instance of SaveAvatarRequest from a dict
save_avatar_request_from_dict = SaveAvatarRequest.from_dict(save_avatar_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


