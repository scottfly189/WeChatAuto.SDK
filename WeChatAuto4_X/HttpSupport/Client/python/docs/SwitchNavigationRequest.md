# SwitchNavigationRequest

切换/关闭导航栏窗口。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**navigation_type** | [**NavigationType**](NavigationType.md) |  | [optional] 

## Example

```python
from wechatauto.models.switch_navigation_request import SwitchNavigationRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SwitchNavigationRequest from a JSON string
switch_navigation_request_instance = SwitchNavigationRequest.from_json(json)
# print the JSON string representation of the object
print(SwitchNavigationRequest.to_json())

# convert the object into a dict
switch_navigation_request_dict = switch_navigation_request_instance.to_dict()
# create an instance of SwitchNavigationRequest from a dict
switch_navigation_request_from_dict = SwitchNavigationRequest.from_dict(switch_navigation_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


