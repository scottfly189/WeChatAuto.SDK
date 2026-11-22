# WhoRequest

仅携带 who（可为空，作用于当前焦点窗口）的请求。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.who_request import WhoRequest

# TODO update the JSON string below
json = "{}"
# create an instance of WhoRequest from a JSON string
who_request_instance = WhoRequest.from_json(json)
# print the JSON string representation of the object
print(WhoRequest.to_json())

# convert the object into a dict
who_request_dict = who_request_instance.to_dict()
# create an instance of WhoRequest from a dict
who_request_from_dict = WhoRequest.from_dict(who_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


