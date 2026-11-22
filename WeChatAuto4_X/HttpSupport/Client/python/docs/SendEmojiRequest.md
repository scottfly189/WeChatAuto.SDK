# SendEmojiRequest

发送表情。emoji 与 index 二选一（优先使用 index）。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**emoji** | **str** | 表情名称或描述。 | [optional] 
**index** | **int** | 表情索引。 | [optional] 
**at_users** | **List[str]** |  | [optional] 

## Example

```python
from wechatauto.models.send_emoji_request import SendEmojiRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SendEmojiRequest from a JSON string
send_emoji_request_instance = SendEmojiRequest.from_json(json)
# print the JSON string representation of the object
print(SendEmojiRequest.to_json())

# convert the object into a dict
send_emoji_request_dict = send_emoji_request_instance.to_dict()
# create an instance of SendEmojiRequest from a dict
send_emoji_request_from_dict = SendEmojiRequest.from_dict(send_emoji_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


