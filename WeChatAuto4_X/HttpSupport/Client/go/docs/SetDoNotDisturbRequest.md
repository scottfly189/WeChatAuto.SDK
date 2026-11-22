# SetDoNotDisturbRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**Setting** | Pointer to **bool** |  | [optional] 

## Methods

### NewSetDoNotDisturbRequest

`func NewSetDoNotDisturbRequest() *SetDoNotDisturbRequest`

NewSetDoNotDisturbRequest instantiates a new SetDoNotDisturbRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSetDoNotDisturbRequestWithDefaults

`func NewSetDoNotDisturbRequestWithDefaults() *SetDoNotDisturbRequest`

NewSetDoNotDisturbRequestWithDefaults instantiates a new SetDoNotDisturbRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *SetDoNotDisturbRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *SetDoNotDisturbRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *SetDoNotDisturbRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *SetDoNotDisturbRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *SetDoNotDisturbRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *SetDoNotDisturbRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetSetting

`func (o *SetDoNotDisturbRequest) GetSetting() bool`

GetSetting returns the Setting field if non-nil, zero value otherwise.

### GetSettingOk

`func (o *SetDoNotDisturbRequest) GetSettingOk() (*bool, bool)`

GetSettingOk returns a tuple with the Setting field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSetting

`func (o *SetDoNotDisturbRequest) SetSetting(v bool)`

SetSetting sets Setting field to given value.

### HasSetting

`func (o *SetDoNotDisturbRequest) HasSetting() bool`

HasSetting returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


