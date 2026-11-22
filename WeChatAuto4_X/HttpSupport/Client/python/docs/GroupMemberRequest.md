# GroupMemberRequest

添加/移除群成员。group_name 为空时作用于当前焦点群聊。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**group_name** | **str** |  | [optional] 
**members** | **List[str]** |  | [optional] 

## Example

```python
from wechatauto.models.group_member_request import GroupMemberRequest

# TODO update the JSON string below
json = "{}"
# create an instance of GroupMemberRequest from a JSON string
group_member_request_instance = GroupMemberRequest.from_json(json)
# print the JSON string representation of the object
print(GroupMemberRequest.to_json())

# convert the object into a dict
group_member_request_dict = group_member_request_instance.to_dict()
# create an instance of GroupMemberRequest from a dict
group_member_request_from_dict = GroupMemberRequest.from_dict(group_member_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


