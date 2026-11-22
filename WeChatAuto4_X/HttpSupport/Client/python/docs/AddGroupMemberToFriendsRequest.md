# AddGroupMemberToFriendsRequest

从群聊中添加好友。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**group_name** | **str** |  | [optional] 
**members** | **List[str]** |  | [optional] 
**options** | [**AddFriendsOptions**](AddFriendsOptions.md) |  | [optional] 

## Example

```python
from wechatauto.models.add_group_member_to_friends_request import AddGroupMemberToFriendsRequest

# TODO update the JSON string below
json = "{}"
# create an instance of AddGroupMemberToFriendsRequest from a JSON string
add_group_member_to_friends_request_instance = AddGroupMemberToFriendsRequest.from_json(json)
# print the JSON string representation of the object
print(AddGroupMemberToFriendsRequest.to_json())

# convert the object into a dict
add_group_member_to_friends_request_dict = add_group_member_to_friends_request_instance.to_dict()
# create an instance of AddGroupMemberToFriendsRequest from a dict
add_group_member_to_friends_request_from_dict = AddGroupMemberToFriendsRequest.from_dict(add_group_member_to_friends_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


