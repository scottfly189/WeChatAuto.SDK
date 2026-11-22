# ChatRefer

消息引用

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**var_date** | **date** | 日期,如果不设置则不进行日期筛选 | [optional] 
**message** | [**ChatSimpleMessage**](ChatSimpleMessage.md) |  | [optional] 
**is_close_search_win** | **bool** | 是否关闭查找窗口，默认是关闭，如果设置为false,则不关闭查找窗口，速度会略快，但需要自行关闭查找窗口. | [optional] 

## Example

```python
from wechatauto.models.chat_refer import ChatRefer

# TODO update the JSON string below
json = "{}"
# create an instance of ChatRefer from a JSON string
chat_refer_instance = ChatRefer.from_json(json)
# print the JSON string representation of the object
print(ChatRefer.to_json())

# convert the object into a dict
chat_refer_dict = chat_refer_instance.to_dict()
# create an instance of ChatRefer from a dict
chat_refer_from_dict = ChatRefer.from_dict(chat_refer_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


