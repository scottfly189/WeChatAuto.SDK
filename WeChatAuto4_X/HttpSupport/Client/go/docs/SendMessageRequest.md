# SendMessageRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**Message** | Pointer to **NullableString** |  | [optional] 
**AtUsers** | Pointer to **[]string** | 被 @ 的好友列表。 | [optional] 
**Refer** | Pointer to [**ChatRefer**](ChatRefer.md) |  | [optional] 

## Methods

### NewSendMessageRequest

`func NewSendMessageRequest() *SendMessageRequest`

NewSendMessageRequest instantiates a new SendMessageRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSendMessageRequestWithDefaults

`func NewSendMessageRequestWithDefaults() *SendMessageRequest`

NewSendMessageRequestWithDefaults instantiates a new SendMessageRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *SendMessageRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *SendMessageRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *SendMessageRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *SendMessageRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *SendMessageRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *SendMessageRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetMessage

`func (o *SendMessageRequest) GetMessage() string`

GetMessage returns the Message field if non-nil, zero value otherwise.

### GetMessageOk

`func (o *SendMessageRequest) GetMessageOk() (*string, bool)`

GetMessageOk returns a tuple with the Message field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMessage

`func (o *SendMessageRequest) SetMessage(v string)`

SetMessage sets Message field to given value.

### HasMessage

`func (o *SendMessageRequest) HasMessage() bool`

HasMessage returns a boolean if a field has been set.

### SetMessageNil

`func (o *SendMessageRequest) SetMessageNil(b bool)`

 SetMessageNil sets the value for Message to be an explicit nil

### UnsetMessage
`func (o *SendMessageRequest) UnsetMessage()`

UnsetMessage ensures that no value is present for Message, not even an explicit nil
### GetAtUsers

`func (o *SendMessageRequest) GetAtUsers() []string`

GetAtUsers returns the AtUsers field if non-nil, zero value otherwise.

### GetAtUsersOk

`func (o *SendMessageRequest) GetAtUsersOk() (*[]string, bool)`

GetAtUsersOk returns a tuple with the AtUsers field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetAtUsers

`func (o *SendMessageRequest) SetAtUsers(v []string)`

SetAtUsers sets AtUsers field to given value.

### HasAtUsers

`func (o *SendMessageRequest) HasAtUsers() bool`

HasAtUsers returns a boolean if a field has been set.

### SetAtUsersNil

`func (o *SendMessageRequest) SetAtUsersNil(b bool)`

 SetAtUsersNil sets the value for AtUsers to be an explicit nil

### UnsetAtUsers
`func (o *SendMessageRequest) UnsetAtUsers()`

UnsetAtUsers ensures that no value is present for AtUsers, not even an explicit nil
### GetRefer

`func (o *SendMessageRequest) GetRefer() ChatRefer`

GetRefer returns the Refer field if non-nil, zero value otherwise.

### GetReferOk

`func (o *SendMessageRequest) GetReferOk() (*ChatRefer, bool)`

GetReferOk returns a tuple with the Refer field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetRefer

`func (o *SendMessageRequest) SetRefer(v ChatRefer)`

SetRefer sets Refer field to given value.

### HasRefer

`func (o *SendMessageRequest) HasRefer() bool`

HasRefer returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


