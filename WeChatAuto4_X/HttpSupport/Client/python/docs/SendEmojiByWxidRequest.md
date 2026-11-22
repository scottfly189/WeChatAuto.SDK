# SendEmojiByWxidRequest

按联系人 UserName 发送表情。emoji 与 index 二选一（优先 index）。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**user_name** | **str** |  | [optional] 
**emoji** | **str** |  | [optional] 
**index** | **int** |  | [optional] 
**at_users** | **List[str]** |  | [optional] 

## Example

```python
from wechatauto.models.send_emoji_by_wxid_request import SendEmojiByWxidRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SendEmojiByWxidRequest from a JSON string
send_emoji_by_wxid_request_instance = SendEmojiByWxidRequest.from_json(json)
# print the JSON string representation of the object
print(SendEmojiByWxidRequest.to_json())

# convert the object into a dict
send_emoji_by_wxid_request_dict = send_emoji_by_wxid_request_instance.to_dict()
# create an instance of SendEmojiByWxidRequest from a dict
send_emoji_by_wxid_request_from_dict = SendEmojiByWxidRequest.from_dict(send_emoji_by_wxid_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


