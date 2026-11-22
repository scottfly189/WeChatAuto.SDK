# TapRequest

拍一拍。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**prev_scroll_number** | **int** |  | [optional] 

## Example

```python
from wechatauto.models.tap_request import TapRequest

# TODO update the JSON string below
json = "{}"
# create an instance of TapRequest from a JSON string
tap_request_instance = TapRequest.from_json(json)
# print the JSON string representation of the object
print(TapRequest.to_json())

# convert the object into a dict
tap_request_dict = tap_request_instance.to_dict()
# create an instance of TapRequest from a dict
tap_request_from_dict = TapRequest.from_dict(tap_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


