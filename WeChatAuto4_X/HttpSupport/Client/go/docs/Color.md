# Color

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**R** | Pointer to **int32** |  | [optional] [readonly] 
**G** | Pointer to **int32** |  | [optional] [readonly] 
**B** | Pointer to **int32** |  | [optional] [readonly] 
**A** | Pointer to **int32** |  | [optional] [readonly] 
**IsKnownColor** | Pointer to **bool** |  | [optional] [readonly] 
**IsEmpty** | Pointer to **bool** |  | [optional] [readonly] 
**IsNamedColor** | Pointer to **bool** |  | [optional] [readonly] 
**IsSystemColor** | Pointer to **bool** |  | [optional] [readonly] 
**Name** | Pointer to **NullableString** |  | [optional] [readonly] 

## Methods

### NewColor

`func NewColor() *Color`

NewColor instantiates a new Color object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewColorWithDefaults

`func NewColorWithDefaults() *Color`

NewColorWithDefaults instantiates a new Color object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetR

`func (o *Color) GetR() int32`

GetR returns the R field if non-nil, zero value otherwise.

### GetROk

`func (o *Color) GetROk() (*int32, bool)`

GetROk returns a tuple with the R field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetR

`func (o *Color) SetR(v int32)`

SetR sets R field to given value.

### HasR

`func (o *Color) HasR() bool`

HasR returns a boolean if a field has been set.

### GetG

`func (o *Color) GetG() int32`

GetG returns the G field if non-nil, zero value otherwise.

### GetGOk

`func (o *Color) GetGOk() (*int32, bool)`

GetGOk returns a tuple with the G field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetG

`func (o *Color) SetG(v int32)`

SetG sets G field to given value.

### HasG

`func (o *Color) HasG() bool`

HasG returns a boolean if a field has been set.

### GetB

`func (o *Color) GetB() int32`

GetB returns the B field if non-nil, zero value otherwise.

### GetBOk

`func (o *Color) GetBOk() (*int32, bool)`

GetBOk returns a tuple with the B field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetB

`func (o *Color) SetB(v int32)`

SetB sets B field to given value.

### HasB

`func (o *Color) HasB() bool`

HasB returns a boolean if a field has been set.

### GetA

`func (o *Color) GetA() int32`

GetA returns the A field if non-nil, zero value otherwise.

### GetAOk

`func (o *Color) GetAOk() (*int32, bool)`

GetAOk returns a tuple with the A field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetA

`func (o *Color) SetA(v int32)`

SetA sets A field to given value.

### HasA

`func (o *Color) HasA() bool`

HasA returns a boolean if a field has been set.

### GetIsKnownColor

`func (o *Color) GetIsKnownColor() bool`

GetIsKnownColor returns the IsKnownColor field if non-nil, zero value otherwise.

### GetIsKnownColorOk

`func (o *Color) GetIsKnownColorOk() (*bool, bool)`

GetIsKnownColorOk returns a tuple with the IsKnownColor field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIsKnownColor

`func (o *Color) SetIsKnownColor(v bool)`

SetIsKnownColor sets IsKnownColor field to given value.

### HasIsKnownColor

`func (o *Color) HasIsKnownColor() bool`

HasIsKnownColor returns a boolean if a field has been set.

### GetIsEmpty

`func (o *Color) GetIsEmpty() bool`

GetIsEmpty returns the IsEmpty field if non-nil, zero value otherwise.

### GetIsEmptyOk

`func (o *Color) GetIsEmptyOk() (*bool, bool)`

GetIsEmptyOk returns a tuple with the IsEmpty field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIsEmpty

`func (o *Color) SetIsEmpty(v bool)`

SetIsEmpty sets IsEmpty field to given value.

### HasIsEmpty

`func (o *Color) HasIsEmpty() bool`

HasIsEmpty returns a boolean if a field has been set.

### GetIsNamedColor

`func (o *Color) GetIsNamedColor() bool`

GetIsNamedColor returns the IsNamedColor field if non-nil, zero value otherwise.

### GetIsNamedColorOk

`func (o *Color) GetIsNamedColorOk() (*bool, bool)`

GetIsNamedColorOk returns a tuple with the IsNamedColor field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIsNamedColor

`func (o *Color) SetIsNamedColor(v bool)`

SetIsNamedColor sets IsNamedColor field to given value.

### HasIsNamedColor

`func (o *Color) HasIsNamedColor() bool`

HasIsNamedColor returns a boolean if a field has been set.

### GetIsSystemColor

`func (o *Color) GetIsSystemColor() bool`

GetIsSystemColor returns the IsSystemColor field if non-nil, zero value otherwise.

### GetIsSystemColorOk

`func (o *Color) GetIsSystemColorOk() (*bool, bool)`

GetIsSystemColorOk returns a tuple with the IsSystemColor field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIsSystemColor

`func (o *Color) SetIsSystemColor(v bool)`

SetIsSystemColor sets IsSystemColor field to given value.

### HasIsSystemColor

`func (o *Color) HasIsSystemColor() bool`

HasIsSystemColor returns a boolean if a field has been set.

### GetName

`func (o *Color) GetName() string`

GetName returns the Name field if non-nil, zero value otherwise.

### GetNameOk

`func (o *Color) GetNameOk() (*string, bool)`

GetNameOk returns a tuple with the Name field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetName

`func (o *Color) SetName(v string)`

SetName sets Name field to given value.

### HasName

`func (o *Color) HasName() bool`

HasName returns a boolean if a field has been set.

### SetNameNil

`func (o *Color) SetNameNil(b bool)`

 SetNameNil sets the value for Name to be an explicit nil

### UnsetName
`func (o *Color) UnsetName()`

UnsetName ensures that no value is present for Name, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


