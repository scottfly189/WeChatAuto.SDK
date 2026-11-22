# ForwardSingleMessageRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**Message** | Pointer to **NullableString** |  | [optional] 
**ChatSimpleMessage** | Pointer to [**ChatSimpleMessage**](ChatSimpleMessage.md) |  | [optional] 
**To** | Pointer to **[]string** |  | [optional] 
**PrevScrollNumber** | Pointer to **int32** |  | [optional] 

## Methods

### NewForwardSingleMessageRequest

`func NewForwardSingleMessageRequest() *ForwardSingleMessageRequest`

NewForwardSingleMessageRequest instantiates a new ForwardSingleMessageRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewForwardSingleMessageRequestWithDefaults

`func NewForwardSingleMessageRequestWithDefaults() *ForwardSingleMessageRequest`

NewForwardSingleMessageRequestWithDefaults instantiates a new ForwardSingleMessageRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *ForwardSingleMessageRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *ForwardSingleMessageRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *ForwardSingleMessageRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *ForwardSingleMessageRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *ForwardSingleMessageRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *ForwardSingleMessageRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetMessage

`func (o *ForwardSingleMessageRequest) GetMessage() string`

GetMessage returns the Message field if non-nil, zero value otherwise.

### GetMessageOk

`func (o *ForwardSingleMessageRequest) GetMessageOk() (*string, bool)`

GetMessageOk returns a tuple with the Message field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMessage

`func (o *ForwardSingleMessageRequest) SetMessage(v string)`

SetMessage sets Message field to given value.

### HasMessage

`func (o *ForwardSingleMessageRequest) HasMessage() bool`

HasMessage returns a boolean if a field has been set.

### SetMessageNil

`func (o *ForwardSingleMessageRequest) SetMessageNil(b bool)`

 SetMessageNil sets the value for Message to be an explicit nil

### UnsetMessage
`func (o *ForwardSingleMessageRequest) UnsetMessage()`

UnsetMessage ensures that no value is present for Message, not even an explicit nil
### GetChatSimpleMessage

`func (o *ForwardSingleMessageRequest) GetChatSimpleMessage() ChatSimpleMessage`

GetChatSimpleMessage returns the ChatSimpleMessage field if non-nil, zero value otherwise.

### GetChatSimpleMessageOk

`func (o *ForwardSingleMessageRequest) GetChatSimpleMessageOk() (*ChatSimpleMessage, bool)`

GetChatSimpleMessageOk returns a tuple with the ChatSimpleMessage field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetChatSimpleMessage

`func (o *ForwardSingleMessageRequest) SetChatSimpleMessage(v ChatSimpleMessage)`

SetChatSimpleMessage sets ChatSimpleMessage field to given value.

### HasChatSimpleMessage

`func (o *ForwardSingleMessageRequest) HasChatSimpleMessage() bool`

HasChatSimpleMessage returns a boolean if a field has been set.

### GetTo

`func (o *ForwardSingleMessageRequest) GetTo() []string`

GetTo returns the To field if non-nil, zero value otherwise.

### GetToOk

`func (o *ForwardSingleMessageRequest) GetToOk() (*[]string, bool)`

GetToOk returns a tuple with the To field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetTo

`func (o *ForwardSingleMessageRequest) SetTo(v []string)`

SetTo sets To field to given value.

### HasTo

`func (o *ForwardSingleMessageRequest) HasTo() bool`

HasTo returns a boolean if a field has been set.

### SetToNil

`func (o *ForwardSingleMessageRequest) SetToNil(b bool)`

 SetToNil sets the value for To to be an explicit nil

### UnsetTo
`func (o *ForwardSingleMessageRequest) UnsetTo()`

UnsetTo ensures that no value is present for To, not even an explicit nil
### GetPrevScrollNumber

`func (o *ForwardSingleMessageRequest) GetPrevScrollNumber() int32`

GetPrevScrollNumber returns the PrevScrollNumber field if non-nil, zero value otherwise.

### GetPrevScrollNumberOk

`func (o *ForwardSingleMessageRequest) GetPrevScrollNumberOk() (*int32, bool)`

GetPrevScrollNumberOk returns a tuple with the PrevScrollNumber field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetPrevScrollNumber

`func (o *ForwardSingleMessageRequest) SetPrevScrollNumber(v int32)`

SetPrevScrollNumber sets PrevScrollNumber field to given value.

### HasPrevScrollNumber

`func (o *ForwardSingleMessageRequest) HasPrevScrollNumber() bool`

HasPrevScrollNumber returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


