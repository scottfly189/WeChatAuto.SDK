# TapRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**PrevScrollNumber** | Pointer to **int32** |  | [optional] 

## Methods

### NewTapRequest

`func NewTapRequest() *TapRequest`

NewTapRequest instantiates a new TapRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewTapRequestWithDefaults

`func NewTapRequestWithDefaults() *TapRequest`

NewTapRequestWithDefaults instantiates a new TapRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *TapRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *TapRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *TapRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *TapRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *TapRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *TapRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetPrevScrollNumber

`func (o *TapRequest) GetPrevScrollNumber() int32`

GetPrevScrollNumber returns the PrevScrollNumber field if non-nil, zero value otherwise.

### GetPrevScrollNumberOk

`func (o *TapRequest) GetPrevScrollNumberOk() (*int32, bool)`

GetPrevScrollNumberOk returns a tuple with the PrevScrollNumber field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetPrevScrollNumber

`func (o *TapRequest) SetPrevScrollNumber(v int32)`

SetPrevScrollNumber sets PrevScrollNumber field to given value.

### HasPrevScrollNumber

`func (o *TapRequest) HasPrevScrollNumber() bool`

HasPrevScrollNumber returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


