# ColorPalette


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**flags** | **int** |  | [optional] [readonly] 
**entries** | [**List[Color]**](Color.md) |  | [optional] [readonly] 

## Example

```python
from wechatauto.models.color_palette import ColorPalette

# TODO update the JSON string below
json = "{}"
# create an instance of ColorPalette from a JSON string
color_palette_instance = ColorPalette.from_json(json)
# print the JSON string representation of the object
print(ColorPalette.to_json())

# convert the object into a dict
color_palette_dict = color_palette_instance.to_dict()
# create an instance of ColorPalette from a dict
color_palette_from_dict = ColorPalette.from_dict(color_palette_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


