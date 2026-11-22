# PullMessagesRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**MaxFetchNumber** | Pointer to **int32** |  | [optional] 
**IncludeSystemMessage** | Pointer to **bool** |  | [optional] 

## Methods

### NewPullMessagesRequest

`func NewPullMessagesRequest() *PullMessagesRequest`

NewPullMessagesRequest instantiates a new PullMessagesRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewPullMessagesRequestWithDefaults

`func NewPullMessagesRequestWithDefaults() *PullMessagesRequest`

NewPullMessagesRequestWithDefaults instantiates a new PullMessagesRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *PullMessagesRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *PullMessagesRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *PullMessagesRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *PullMessagesRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *PullMessagesRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *PullMessagesRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetMaxFetchNumber

`func (o *PullMessagesRequest) GetMaxFetchNumber() int32`

GetMaxFetchNumber returns the MaxFetchNumber field if non-nil, zero value otherwise.

### GetMaxFetchNumberOk

`func (o *PullMessagesRequest) GetMaxFetchNumberOk() (*int32, bool)`

GetMaxFetchNumberOk returns a tuple with the MaxFetchNumber field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMaxFetchNumber

`func (o *PullMessagesRequest) SetMaxFetchNumber(v int32)`

SetMaxFetchNumber sets MaxFetchNumber field to given value.

### HasMaxFetchNumber

`func (o *PullMessagesRequest) HasMaxFetchNumber() bool`

HasMaxFetchNumber returns a boolean if a field has been set.

### GetIncludeSystemMessage

`func (o *PullMessagesRequest) GetIncludeSystemMessage() bool`

GetIncludeSystemMessage returns the IncludeSystemMessage field if non-nil, zero value otherwise.

### GetIncludeSystemMessageOk

`func (o *PullMessagesRequest) GetIncludeSystemMessageOk() (*bool, bool)`

GetIncludeSystemMessageOk returns a tuple with the IncludeSystemMessage field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIncludeSystemMessage

`func (o *PullMessagesRequest) SetIncludeSystemMessage(v bool)`

SetIncludeSystemMessage sets IncludeSystemMessage field to given value.

### HasIncludeSystemMessage

`func (o *PullMessagesRequest) HasIncludeSystemMessage() bool`

HasIncludeSystemMessage returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


