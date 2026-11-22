# UpdateGroupNoticeRequest

更新群公告。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**group_name** | **str** |  | [optional] 
**group_notice** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.update_group_notice_request import UpdateGroupNoticeRequest

# TODO update the JSON string below
json = "{}"
# create an instance of UpdateGroupNoticeRequest from a JSON string
update_group_notice_request_instance = UpdateGroupNoticeRequest.from_json(json)
# print the JSON string representation of the object
print(UpdateGroupNoticeRequest.to_json())

# convert the object into a dict
update_group_notice_request_dict = update_group_notice_request_instance.to_dict()
# create an instance of UpdateGroupNoticeRequest from a dict
update_group_notice_request_from_dict = UpdateGroupNoticeRequest.from_dict(update_group_notice_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


