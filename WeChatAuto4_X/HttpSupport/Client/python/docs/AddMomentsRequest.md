# AddMomentsRequest

发朋友圈。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**image_files** | **List[str]** |  | [optional] 
**content** | **str** |  | [optional] 
**options** | [**MomentsOptions**](MomentsOptions.md) |  | [optional] 

## Example

```python
from wechatauto.models.add_moments_request import AddMomentsRequest

# TODO update the JSON string below
json = "{}"
# create an instance of AddMomentsRequest from a JSON string
add_moments_request_instance = AddMomentsRequest.from_json(json)
# print the JSON string representation of the object
print(AddMomentsRequest.to_json())

# convert the object into a dict
add_moments_request_dict = add_moments_request_instance.to_dict()
# create an instance of AddMomentsRequest from a dict
add_moments_request_from_dict = AddMomentsRequest.from_dict(add_moments_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


