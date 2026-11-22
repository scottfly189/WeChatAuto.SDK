# ChangeGroupMemoRequest

修改群备注。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**group_name** | **str** |  | [optional] 
**new_memo** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.change_group_memo_request import ChangeGroupMemoRequest

# TODO update the JSON string below
json = "{}"
# create an instance of ChangeGroupMemoRequest from a JSON string
change_group_memo_request_instance = ChangeGroupMemoRequest.from_json(json)
# print the JSON string representation of the object
print(ChangeGroupMemoRequest.to_json())

# convert the object into a dict
change_group_memo_request_dict = change_group_memo_request_instance.to_dict()
# create an instance of ChangeGroupMemoRequest from a dict
change_group_memo_request_from_dict = ChangeGroupMemoRequest.from_dict(change_group_memo_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


