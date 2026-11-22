# Image


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**tag** | **object** |  | [optional] 
**physical_dimension** | [**SizeF**](SizeF.md) |  | [optional] 
**size** | [**Size**](Size.md) |  | [optional] 
**width** | **int** |  | [optional] [readonly] 
**height** | **int** |  | [optional] [readonly] 
**horizontal_resolution** | **float** |  | [optional] [readonly] 
**vertical_resolution** | **float** |  | [optional] [readonly] 
**flags** | **int** |  | [optional] [readonly] 
**raw_format** | [**ImageFormat**](ImageFormat.md) |  | [optional] 
**pixel_format** | [**PixelFormat**](PixelFormat.md) |  | [optional] 
**property_id_list** | **List[int]** |  | [optional] [readonly] 
**property_items** | [**List[PropertyItem]**](PropertyItem.md) |  | [optional] [readonly] 
**palette** | [**ColorPalette**](ColorPalette.md) |  | [optional] 
**frame_dimensions_list** | **List[UUID]** |  | [optional] [readonly] 

## Example

```python
from wechatauto.models.image import Image

# TODO update the JSON string below
json = "{}"
# create an instance of Image from a JSON string
image_instance = Image.from_json(json)
# print the JSON string representation of the object
print(Image.to_json())

# convert the object into a dict
image_dict = image_instance.to_dict()
# create an instance of Image from a dict
image_from_dict = Image.from_dict(image_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


