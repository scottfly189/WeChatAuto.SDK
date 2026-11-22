# RenameGroupRequest

修改群名。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**old_group_name** | **str** |  | [optional] 
**new_group_name** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.rename_group_request import RenameGroupRequest

# TODO update the JSON string below
json = "{}"
# create an instance of RenameGroupRequest from a JSON string
rename_group_request_instance = RenameGroupRequest.from_json(json)
# print the JSON string representation of the object
print(RenameGroupRequest.to_json())

# convert the object into a dict
rename_group_request_dict = rename_group_request_instance.to_dict()
# create an instance of RenameGroupRequest from a dict
rename_group_request_from_dict = RenameGroupRequest.from_dict(rename_group_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


