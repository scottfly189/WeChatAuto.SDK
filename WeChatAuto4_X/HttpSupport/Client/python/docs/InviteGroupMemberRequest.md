# InviteGroupMemberRequest

邀请群成员（外部群）。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**group_name** | **str** |  | [optional] 
**members** | **List[str]** |  | [optional] 
**invite_reason** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.invite_group_member_request import InviteGroupMemberRequest

# TODO update the JSON string below
json = "{}"
# create an instance of InviteGroupMemberRequest from a JSON string
invite_group_member_request_instance = InviteGroupMemberRequest.from_json(json)
# print the JSON string representation of the object
print(InviteGroupMemberRequest.to_json())

# convert the object into a dict
invite_group_member_request_dict = invite_group_member_request_instance.to_dict()
# create an instance of InviteGroupMemberRequest from a dict
invite_group_member_request_from_dict = InviteGroupMemberRequest.from_dict(invite_group_member_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


