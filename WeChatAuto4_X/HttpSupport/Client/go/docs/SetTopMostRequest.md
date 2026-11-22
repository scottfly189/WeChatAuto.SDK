# SetTopMostRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**Setting** | Pointer to **bool** |  | [optional] 

## Methods

### NewSetTopMostRequest

`func NewSetTopMostRequest() *SetTopMostRequest`

NewSetTopMostRequest instantiates a new SetTopMostRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSetTopMostRequestWithDefaults

`func NewSetTopMostRequestWithDefaults() *SetTopMostRequest`

NewSetTopMostRequestWithDefaults instantiates a new SetTopMostRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *SetTopMostRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *SetTopMostRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *SetTopMostRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *SetTopMostRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *SetTopMostRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *SetTopMostRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetSetting

`func (o *SetTopMostRequest) GetSetting() bool`

GetSetting returns the Setting field if non-nil, zero value otherwise.

### GetSettingOk

`func (o *SetTopMostRequest) GetSettingOk() (*bool, bool)`

GetSettingOk returns a tuple with the Setting field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSetting

`func (o *SetTopMostRequest) SetSetting(v bool)`

SetSetting sets Setting field to given value.

### HasSetting

`func (o *SetTopMostRequest) HasSetting() bool`

HasSetting returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


