# AddFriendsOptions

增加朋友选项

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**interval_time** | **int** | 间隔时间,以秒为单位，默认为三秒，如果担心风控，可以把此时间设置长一点 | [optional] 
**is_close_win** | **bool** | 是否关闭增加朋友窗口,默认关闭，可以设置为false不关闭 | [optional] 
**say_hi** | **str** | 加好友时打招呼内容,如果为空，则保持微信默认 | [optional] 
**suffix** | **str** | 备注后缀，如：设置suffix为\&quot;test\&quot;,则备注为:xxxx_test,可以未来以备注的后缀来区分不同的好友分类 | [optional] 
**label** | **str** | 设置标签 | [optional] 

## Example

```python
from wechatauto.models.add_friends_options import AddFriendsOptions

# TODO update the JSON string below
json = "{}"
# create an instance of AddFriendsOptions from a JSON string
add_friends_options_instance = AddFriendsOptions.from_json(json)
# print the JSON string representation of the object
print(AddFriendsOptions.to_json())

# convert the object into a dict
add_friends_options_dict = add_friends_options_instance.to_dict()
# create an instance of AddFriendsOptions from a dict
add_friends_options_from_dict = AddFriendsOptions.from_dict(add_friends_options_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


