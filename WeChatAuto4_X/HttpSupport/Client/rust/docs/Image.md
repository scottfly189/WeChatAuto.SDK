# Image

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**tag** | Option<**serde_json::Value**> |  | [optional]
**physical_dimension** | Option<[**models::SizeF**](SizeF.md)> |  | [optional]
**size** | Option<[**models::Size**](Size.md)> |  | [optional]
**width** | Option<**i32**> |  | [optional][readonly]
**height** | Option<**i32**> |  | [optional][readonly]
**horizontal_resolution** | Option<**f32**> |  | [optional][readonly]
**vertical_resolution** | Option<**f32**> |  | [optional][readonly]
**flags** | Option<**i32**> |  | [optional][readonly]
**raw_format** | Option<[**models::ImageFormat**](ImageFormat.md)> |  | [optional]
**pixel_format** | Option<[**models::PixelFormat**](PixelFormat.md)> |  | [optional]
**property_id_list** | Option<**Vec<i32>**> |  | [optional][readonly]
**property_items** | Option<[**Vec<models::PropertyItem>**](PropertyItem.md)> |  | [optional][readonly]
**palette** | Option<[**models::ColorPalette**](ColorPalette.md)> |  | [optional]
**frame_dimensions_list** | Option<**Vec<uuid::Uuid>**> |  | [optional][readonly]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


