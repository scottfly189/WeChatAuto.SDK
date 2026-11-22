# ChangeGroupNickNameRequest

修改自己在群中的昵称。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**group_name** | **str** |  | [optional] 
**nick_name** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.change_group_nick_name_request import ChangeGroupNickNameRequest

# TODO update the JSON string below
json = "{}"
# create an instance of ChangeGroupNickNameRequest from a JSON string
change_group_nick_name_request_instance = ChangeGroupNickNameRequest.from_json(json)
# print the JSON string representation of the object
print(ChangeGroupNickNameRequest.to_json())

# convert the object into a dict
change_group_nick_name_request_dict = change_group_nick_name_request_instance.to_dict()
# create an instance of ChangeGroupNickNameRequest from a dict
change_group_nick_name_request_from_dict = ChangeGroupNickNameRequest.from_dict(change_group_nick_name_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


