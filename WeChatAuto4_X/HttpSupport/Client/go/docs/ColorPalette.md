# ColorPalette

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Flags** | Pointer to **int32** |  | [optional] [readonly] 
**Entries** | Pointer to [**[]Color**](Color.md) |  | [optional] [readonly] 

## Methods

### NewColorPalette

`func NewColorPalette() *ColorPalette`

NewColorPalette instantiates a new ColorPalette object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewColorPaletteWithDefaults

`func NewColorPaletteWithDefaults() *ColorPalette`

NewColorPaletteWithDefaults instantiates a new ColorPalette object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetFlags

`func (o *ColorPalette) GetFlags() int32`

GetFlags returns the Flags field if non-nil, zero value otherwise.

### GetFlagsOk

`func (o *ColorPalette) GetFlagsOk() (*int32, bool)`

GetFlagsOk returns a tuple with the Flags field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetFlags

`func (o *ColorPalette) SetFlags(v int32)`

SetFlags sets Flags field to given value.

### HasFlags

`func (o *ColorPalette) HasFlags() bool`

HasFlags returns a boolean if a field has been set.

### GetEntries

`func (o *ColorPalette) GetEntries() []Color`

GetEntries returns the Entries field if non-nil, zero value otherwise.

### GetEntriesOk

`func (o *ColorPalette) GetEntriesOk() (*[]Color, bool)`

GetEntriesOk returns a tuple with the Entries field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetEntries

`func (o *ColorPalette) SetEntries(v []Color)`

SetEntries sets Entries field to given value.

### HasEntries

`func (o *ColorPalette) HasEntries() bool`

HasEntries returns a boolean if a field has been set.

### SetEntriesNil

`func (o *ColorPalette) SetEntriesNil(b bool)`

 SetEntriesNil sets the value for Entries to be an explicit nil

### UnsetEntries
`func (o *ColorPalette) UnsetEntries()`

UnsetEntries ensures that no value is present for Entries, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


