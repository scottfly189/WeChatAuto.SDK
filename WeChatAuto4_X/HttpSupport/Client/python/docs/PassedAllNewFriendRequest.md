# PassedAllNewFriendRequest

批量通过新好友申请 / 启动自动通过监听。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**keyword** | **List[str]** |  | [optional] 
**suffix** | **str** |  | [optional] 
**label** | **str** |  | [optional] 
**passed_delete** | **bool** |  | [optional] 
**keyword_rules** | [**Dict[str, FriendKeywordRuleRequest]**](FriendKeywordRuleRequest.md) |  | [optional] 

## Example

```python
from wechatauto.models.passed_all_new_friend_request import PassedAllNewFriendRequest

# TODO update the JSON string below
json = "{}"
# create an instance of PassedAllNewFriendRequest from a JSON string
passed_all_new_friend_request_instance = PassedAllNewFriendRequest.from_json(json)
# print the JSON string representation of the object
print(PassedAllNewFriendRequest.to_json())

# convert the object into a dict
passed_all_new_friend_request_dict = passed_all_new_friend_request_instance.to_dict()
# create an instance of PassedAllNewFriendRequest from a dict
passed_all_new_friend_request_from_dict = PassedAllNewFriendRequest.from_dict(passed_all_new_friend_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


