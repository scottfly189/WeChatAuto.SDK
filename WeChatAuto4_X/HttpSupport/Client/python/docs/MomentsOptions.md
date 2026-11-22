# MomentsOptions

朋友圈选项

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**at_usrs** | **List[str]** | 朋友圈被@的好友 | [optional] 
**labels** | **List[str]** | 朋友圈哪些设定的标签可以看，如果没有设置标签，则全部可见. | [optional] 
**is_close_moments** | **bool** | 是否执行操作后关闭朋友圈,默认关闭，也可以设置为False,然后使用者可以手动关闭 | [optional] 

## Example

```python
from wechatauto.models.moments_options import MomentsOptions

# TODO update the JSON string below
json = "{}"
# create an instance of MomentsOptions from a JSON string
moments_options_instance = MomentsOptions.from_json(json)
# print the JSON string representation of the object
print(MomentsOptions.to_json())

# convert the object into a dict
moments_options_dict = moments_options_instance.to_dict()
# create an instance of MomentsOptions from a dict
moments_options_from_dict = MomentsOptions.from_dict(moments_options_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


