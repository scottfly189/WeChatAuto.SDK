# Image

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Tag** | Pointer to **interface{}** |  | [optional] 
**PhysicalDimension** | Pointer to [**SizeF**](SizeF.md) |  | [optional] 
**Size** | Pointer to [**Size**](Size.md) |  | [optional] 
**Width** | Pointer to **int32** |  | [optional] [readonly] 
**Height** | Pointer to **int32** |  | [optional] [readonly] 
**HorizontalResolution** | Pointer to **float32** |  | [optional] [readonly] 
**VerticalResolution** | Pointer to **float32** |  | [optional] [readonly] 
**Flags** | Pointer to **int32** |  | [optional] [readonly] 
**RawFormat** | Pointer to [**ImageFormat**](ImageFormat.md) |  | [optional] 
**PixelFormat** | Pointer to [**PixelFormat**](PixelFormat.md) |  | [optional] 
**PropertyIdList** | Pointer to **[]int32** |  | [optional] [readonly] 
**PropertyItems** | Pointer to [**[]PropertyItem**](PropertyItem.md) |  | [optional] [readonly] 
**Palette** | Pointer to [**ColorPalette**](ColorPalette.md) |  | [optional] 
**FrameDimensionsList** | Pointer to **[]string** |  | [optional] [readonly] 

## Methods

### NewImage

`func NewImage() *Image`

NewImage instantiates a new Image object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewImageWithDefaults

`func NewImageWithDefaults() *Image`

NewImageWithDefaults instantiates a new Image object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetTag

`func (o *Image) GetTag() interface{}`

GetTag returns the Tag field if non-nil, zero value otherwise.

### GetTagOk

`func (o *Image) GetTagOk() (*interface{}, bool)`

GetTagOk returns a tuple with the Tag field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetTag

`func (o *Image) SetTag(v interface{})`

SetTag sets Tag field to given value.

### HasTag

`func (o *Image) HasTag() bool`

HasTag returns a boolean if a field has been set.

### SetTagNil

`func (o *Image) SetTagNil(b bool)`

 SetTagNil sets the value for Tag to be an explicit nil

### UnsetTag
`func (o *Image) UnsetTag()`

UnsetTag ensures that no value is present for Tag, not even an explicit nil
### GetPhysicalDimension

`func (o *Image) GetPhysicalDimension() SizeF`

GetPhysicalDimension returns the PhysicalDimension field if non-nil, zero value otherwise.

### GetPhysicalDimensionOk

`func (o *Image) GetPhysicalDimensionOk() (*SizeF, bool)`

GetPhysicalDimensionOk returns a tuple with the PhysicalDimension field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetPhysicalDimension

`func (o *Image) SetPhysicalDimension(v SizeF)`

SetPhysicalDimension sets PhysicalDimension field to given value.

### HasPhysicalDimension

`func (o *Image) HasPhysicalDimension() bool`

HasPhysicalDimension returns a boolean if a field has been set.

### GetSize

`func (o *Image) GetSize() Size`

GetSize returns the Size field if non-nil, zero value otherwise.

### GetSizeOk

`func (o *Image) GetSizeOk() (*Size, bool)`

GetSizeOk returns a tuple with the Size field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSize

`func (o *Image) SetSize(v Size)`

SetSize sets Size field to given value.

### HasSize

`func (o *Image) HasSize() bool`

HasSize returns a boolean if a field has been set.

### GetWidth

`func (o *Image) GetWidth() int32`

GetWidth returns the Width field if non-nil, zero value otherwise.

### GetWidthOk

`func (o *Image) GetWidthOk() (*int32, bool)`

GetWidthOk returns a tuple with the Width field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWidth

`func (o *Image) SetWidth(v int32)`

SetWidth sets Width field to given value.

### HasWidth

`func (o *Image) HasWidth() bool`

HasWidth returns a boolean if a field has been set.

### GetHeight

`func (o *Image) GetHeight() int32`

GetHeight returns the Height field if non-nil, zero value otherwise.

### GetHeightOk

`func (o *Image) GetHeightOk() (*int32, bool)`

GetHeightOk returns a tuple with the Height field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetHeight

`func (o *Image) SetHeight(v int32)`

SetHeight sets Height field to given value.

### HasHeight

`func (o *Image) HasHeight() bool`

HasHeight returns a boolean if a field has been set.

### GetHorizontalResolution

`func (o *Image) GetHorizontalResolution() float32`

GetHorizontalResolution returns the HorizontalResolution field if non-nil, zero value otherwise.

### GetHorizontalResolutionOk

`func (o *Image) GetHorizontalResolutionOk() (*float32, bool)`

GetHorizontalResolutionOk returns a tuple with the HorizontalResolution field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetHorizontalResolution

`func (o *Image) SetHorizontalResolution(v float32)`

SetHorizontalResolution sets HorizontalResolution field to given value.

### HasHorizontalResolution

`func (o *Image) HasHorizontalResolution() bool`

HasHorizontalResolution returns a boolean if a field has been set.

### GetVerticalResolution

`func (o *Image) GetVerticalResolution() float32`

GetVerticalResolution returns the VerticalResolution field if non-nil, zero value otherwise.

### GetVerticalResolutionOk

`func (o *Image) GetVerticalResolutionOk() (*float32, bool)`

GetVerticalResolutionOk returns a tuple with the VerticalResolution field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetVerticalResolution

`func (o *Image) SetVerticalResolution(v float32)`

SetVerticalResolution sets VerticalResolution field to given value.

### HasVerticalResolution

`func (o *Image) HasVerticalResolution() bool`

HasVerticalResolution returns a boolean if a field has been set.

### GetFlags

`func (o *Image) GetFlags() int32`

GetFlags returns the Flags field if non-nil, zero value otherwise.

### GetFlagsOk

`func (o *Image) GetFlagsOk() (*int32, bool)`

GetFlagsOk returns a tuple with the Flags field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetFlags

`func (o *Image) SetFlags(v int32)`

SetFlags sets Flags field to given value.

### HasFlags

`func (o *Image) HasFlags() bool`

HasFlags returns a boolean if a field has been set.

### GetRawFormat

`func (o *Image) GetRawFormat() ImageFormat`

GetRawFormat returns the RawFormat field if non-nil, zero value otherwise.

### GetRawFormatOk

`func (o *Image) GetRawFormatOk() (*ImageFormat, bool)`

GetRawFormatOk returns a tuple with the RawFormat field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetRawFormat

`func (o *Image) SetRawFormat(v ImageFormat)`

SetRawFormat sets RawFormat field to given value.

### HasRawFormat

`func (o *Image) HasRawFormat() bool`

HasRawFormat returns a boolean if a field has been set.

### GetPixelFormat

`func (o *Image) GetPixelFormat() PixelFormat`

GetPixelFormat returns the PixelFormat field if non-nil, zero value otherwise.

### GetPixelFormatOk

`func (o *Image) GetPixelFormatOk() (*PixelFormat, bool)`

GetPixelFormatOk returns a tuple with the PixelFormat field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetPixelFormat

`func (o *Image) SetPixelFormat(v PixelFormat)`

SetPixelFormat sets PixelFormat field to given value.

### HasPixelFormat

`func (o *Image) HasPixelFormat() bool`

HasPixelFormat returns a boolean if a field has been set.

### GetPropertyIdList

`func (o *Image) GetPropertyIdList() []int32`

GetPropertyIdList returns the PropertyIdList field if non-nil, zero value otherwise.

### GetPropertyIdListOk

`func (o *Image) GetPropertyIdListOk() (*[]int32, bool)`

GetPropertyIdListOk returns a tuple with the PropertyIdList field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetPropertyIdList

`func (o *Image) SetPropertyIdList(v []int32)`

SetPropertyIdList sets PropertyIdList field to given value.

### HasPropertyIdList

`func (o *Image) HasPropertyIdList() bool`

HasPropertyIdList returns a boolean if a field has been set.

### SetPropertyIdListNil

`func (o *Image) SetPropertyIdListNil(b bool)`

 SetPropertyIdListNil sets the value for PropertyIdList to be an explicit nil

### UnsetPropertyIdList
`func (o *Image) UnsetPropertyIdList()`

UnsetPropertyIdList ensures that no value is present for PropertyIdList, not even an explicit nil
### GetPropertyItems

`func (o *Image) GetPropertyItems() []PropertyItem`

GetPropertyItems returns the PropertyItems field if non-nil, zero value otherwise.

### GetPropertyItemsOk

`func (o *Image) GetPropertyItemsOk() (*[]PropertyItem, bool)`

GetPropertyItemsOk returns a tuple with the PropertyItems field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetPropertyItems

`func (o *Image) SetPropertyItems(v []PropertyItem)`

SetPropertyItems sets PropertyItems field to given value.

### HasPropertyItems

`func (o *Image) HasPropertyItems() bool`

HasPropertyItems returns a boolean if a field has been set.

### SetPropertyItemsNil

`func (o *Image) SetPropertyItemsNil(b bool)`

 SetPropertyItemsNil sets the value for PropertyItems to be an explicit nil

### UnsetPropertyItems
`func (o *Image) UnsetPropertyItems()`

UnsetPropertyItems ensures that no value is present for PropertyItems, not even an explicit nil
### GetPalette

`func (o *Image) GetPalette() ColorPalette`

GetPalette returns the Palette field if non-nil, zero value otherwise.

### GetPaletteOk

`func (o *Image) GetPaletteOk() (*ColorPalette, bool)`

GetPaletteOk returns a tuple with the Palette field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetPalette

`func (o *Image) SetPalette(v ColorPalette)`

SetPalette sets Palette field to given value.

### HasPalette

`func (o *Image) HasPalette() bool`

HasPalette returns a boolean if a field has been set.

### GetFrameDimensionsList

`func (o *Image) GetFrameDimensionsList() []string`

GetFrameDimensionsList returns the FrameDimensionsList field if non-nil, zero value otherwise.

### GetFrameDimensionsListOk

`func (o *Image) GetFrameDimensionsListOk() (*[]string, bool)`

GetFrameDimensionsListOk returns a tuple with the FrameDimensionsList field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetFrameDimensionsList

`func (o *Image) SetFrameDimensionsList(v []string)`

SetFrameDimensionsList sets FrameDimensionsList field to given value.

### HasFrameDimensionsList

`func (o *Image) HasFrameDimensionsList() bool`

HasFrameDimensionsList returns a boolean if a field has been set.

### SetFrameDimensionsListNil

`func (o *Image) SetFrameDimensionsListNil(b bool)`

 SetFrameDimensionsListNil sets the value for FrameDimensionsList to be an explicit nil

### UnsetFrameDimensionsList
`func (o *Image) UnsetFrameDimensionsList()`

UnsetFrameDimensionsList ensures that no value is present for FrameDimensionsList, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


