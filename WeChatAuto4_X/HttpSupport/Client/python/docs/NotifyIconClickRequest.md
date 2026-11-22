# NotifyIconClickRequest

点击任务栏微信图标。index 与 wechat_name 二选一。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**index** | **int** |  | [optional] 
**wechat_name** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.notify_icon_click_request import NotifyIconClickRequest

# TODO update the JSON string below
json = "{}"
# create an instance of NotifyIconClickRequest from a JSON string
notify_icon_click_request_instance = NotifyIconClickRequest.from_json(json)
# print the JSON string representation of the object
print(NotifyIconClickRequest.to_json())

# convert the object into a dict
notify_icon_click_request_dict = notify_icon_click_request_instance.to_dict()
# create an instance of NotifyIconClickRequest from a dict
notify_icon_click_request_from_dict = NotifyIconClickRequest.from_dict(notify_icon_click_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


