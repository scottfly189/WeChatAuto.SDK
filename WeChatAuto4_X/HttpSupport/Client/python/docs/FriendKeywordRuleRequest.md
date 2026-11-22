# FriendKeywordRuleRequest

单个关键词对应的备注后缀和标签。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**suffix** | **str** |  | [optional] 
**label** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.friend_keyword_rule_request import FriendKeywordRuleRequest

# TODO update the JSON string below
json = "{}"
# create an instance of FriendKeywordRuleRequest from a JSON string
friend_keyword_rule_request_instance = FriendKeywordRuleRequest.from_json(json)
# print the JSON string representation of the object
print(FriendKeywordRuleRequest.to_json())

# convert the object into a dict
friend_keyword_rule_request_dict = friend_keyword_rule_request_instance.to_dict()
# create an instance of FriendKeywordRuleRequest from a dict
friend_keyword_rule_request_from_dict = FriendKeywordRuleRequest.from_dict(friend_keyword_rule_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


