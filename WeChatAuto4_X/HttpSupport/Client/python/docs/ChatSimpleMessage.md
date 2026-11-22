# ChatSimpleMessage


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** | 微信名称 | [optional] 
**message** | **str** | 消息 | [optional] 
**send_date_time** | **str** | 消息日期 | [optional] 
**date_time** | **datetime** | 消息日期,日期时间格式 | [optional] 
**unique_string** | **str** | 唯一字符串 | [optional] 

## Example

```python
from wechatauto.models.chat_simple_message import ChatSimpleMessage

# TODO update the JSON string below
json = "{}"
# create an instance of ChatSimpleMessage from a JSON string
chat_simple_message_instance = ChatSimpleMessage.from_json(json)
# print the JSON string representation of the object
print(ChatSimpleMessage.to_json())

# convert the object into a dict
chat_simple_message_dict = chat_simple_message_instance.to_dict()
# create an instance of ChatSimpleMessage from a dict
chat_simple_message_from_dict = ChatSimpleMessage.from_dict(chat_simple_message_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


