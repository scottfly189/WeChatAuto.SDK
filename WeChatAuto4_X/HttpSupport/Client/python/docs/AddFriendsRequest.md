# AddFriendsRequest

通过手机号码或微信号添加好友。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**friends** | **List[str]** |  | [optional] 
**options** | [**AddFriendsOptions**](AddFriendsOptions.md) |  | [optional] 

## Example

```python
from wechatauto.models.add_friends_request import AddFriendsRequest

# TODO update the JSON string below
json = "{}"
# create an instance of AddFriendsRequest from a JSON string
add_friends_request_instance = AddFriendsRequest.from_json(json)
# print the JSON string representation of the object
print(AddFriendsRequest.to_json())

# convert the object into a dict
add_friends_request_dict = add_friends_request_instance.to_dict()
# create an instance of AddFriendsRequest from a dict
add_friends_request_from_dict = AddFriendsRequest.from_dict(add_friends_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


