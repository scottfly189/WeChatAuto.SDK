# ReferenceRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Message** | Pointer to [**ChatSimpleMessage**](ChatSimpleMessage.md) |  | [optional] 
**Who** | Pointer to **NullableString** |  | [optional] 
**MessageContent** | Pointer to **NullableString** |  | [optional] 
**PrevScrollNumber** | Pointer to **int32** |  | [optional] 

## Methods

### NewReferenceRequest

`func NewReferenceRequest() *ReferenceRequest`

NewReferenceRequest instantiates a new ReferenceRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewReferenceRequestWithDefaults

`func NewReferenceRequestWithDefaults() *ReferenceRequest`

NewReferenceRequestWithDefaults instantiates a new ReferenceRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetMessage

`func (o *ReferenceRequest) GetMessage() ChatSimpleMessage`

GetMessage returns the Message field if non-nil, zero value otherwise.

### GetMessageOk

`func (o *ReferenceRequest) GetMessageOk() (*ChatSimpleMessage, bool)`

GetMessageOk returns a tuple with the Message field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMessage

`func (o *ReferenceRequest) SetMessage(v ChatSimpleMessage)`

SetMessage sets Message field to given value.

### HasMessage

`func (o *ReferenceRequest) HasMessage() bool`

HasMessage returns a boolean if a field has been set.

### GetWho

`func (o *ReferenceRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *ReferenceRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *ReferenceRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *ReferenceRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *ReferenceRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *ReferenceRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetMessageContent

`func (o *ReferenceRequest) GetMessageContent() string`

GetMessageContent returns the MessageContent field if non-nil, zero value otherwise.

### GetMessageContentOk

`func (o *ReferenceRequest) GetMessageContentOk() (*string, bool)`

GetMessageContentOk returns a tuple with the MessageContent field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMessageContent

`func (o *ReferenceRequest) SetMessageContent(v string)`

SetMessageContent sets MessageContent field to given value.

### HasMessageContent

`func (o *ReferenceRequest) HasMessageContent() bool`

HasMessageContent returns a boolean if a field has been set.

### SetMessageContentNil

`func (o *ReferenceRequest) SetMessageContentNil(b bool)`

 SetMessageContentNil sets the value for MessageContent to be an explicit nil

### UnsetMessageContent
`func (o *ReferenceRequest) UnsetMessageContent()`

UnsetMessageContent ensures that no value is present for MessageContent, not even an explicit nil
### GetPrevScrollNumber

`func (o *ReferenceRequest) GetPrevScrollNumber() int32`

GetPrevScrollNumber returns the PrevScrollNumber field if non-nil, zero value otherwise.

### GetPrevScrollNumberOk

`func (o *ReferenceRequest) GetPrevScrollNumberOk() (*int32, bool)`

GetPrevScrollNumberOk returns a tuple with the PrevScrollNumber field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetPrevScrollNumber

`func (o *ReferenceRequest) SetPrevScrollNumber(v int32)`

SetPrevScrollNumber sets PrevScrollNumber field to given value.

### HasPrevScrollNumber

`func (o *ReferenceRequest) HasPrevScrollNumber() bool`

HasPrevScrollNumber returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


