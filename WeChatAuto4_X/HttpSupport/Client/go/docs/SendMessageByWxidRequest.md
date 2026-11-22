# SendMessageByWxidRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**UserName** | Pointer to **NullableString** |  | [optional] 
**Message** | Pointer to **NullableString** |  | [optional] 
**AtUsers** | Pointer to **[]string** |  | [optional] 
**Refer** | Pointer to [**ChatRefer**](ChatRefer.md) |  | [optional] 

## Methods

### NewSendMessageByWxidRequest

`func NewSendMessageByWxidRequest() *SendMessageByWxidRequest`

NewSendMessageByWxidRequest instantiates a new SendMessageByWxidRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSendMessageByWxidRequestWithDefaults

`func NewSendMessageByWxidRequestWithDefaults() *SendMessageByWxidRequest`

NewSendMessageByWxidRequestWithDefaults instantiates a new SendMessageByWxidRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetUserName

`func (o *SendMessageByWxidRequest) GetUserName() string`

GetUserName returns the UserName field if non-nil, zero value otherwise.

### GetUserNameOk

`func (o *SendMessageByWxidRequest) GetUserNameOk() (*string, bool)`

GetUserNameOk returns a tuple with the UserName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetUserName

`func (o *SendMessageByWxidRequest) SetUserName(v string)`

SetUserName sets UserName field to given value.

### HasUserName

`func (o *SendMessageByWxidRequest) HasUserName() bool`

HasUserName returns a boolean if a field has been set.

### SetUserNameNil

`func (o *SendMessageByWxidRequest) SetUserNameNil(b bool)`

 SetUserNameNil sets the value for UserName to be an explicit nil

### UnsetUserName
`func (o *SendMessageByWxidRequest) UnsetUserName()`

UnsetUserName ensures that no value is present for UserName, not even an explicit nil
### GetMessage

`func (o *SendMessageByWxidRequest) GetMessage() string`

GetMessage returns the Message field if non-nil, zero value otherwise.

### GetMessageOk

`func (o *SendMessageByWxidRequest) GetMessageOk() (*string, bool)`

GetMessageOk returns a tuple with the Message field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMessage

`func (o *SendMessageByWxidRequest) SetMessage(v string)`

SetMessage sets Message field to given value.

### HasMessage

`func (o *SendMessageByWxidRequest) HasMessage() bool`

HasMessage returns a boolean if a field has been set.

### SetMessageNil

`func (o *SendMessageByWxidRequest) SetMessageNil(b bool)`

 SetMessageNil sets the value for Message to be an explicit nil

### UnsetMessage
`func (o *SendMessageByWxidRequest) UnsetMessage()`

UnsetMessage ensures that no value is present for Message, not even an explicit nil
### GetAtUsers

`func (o *SendMessageByWxidRequest) GetAtUsers() []string`

GetAtUsers returns the AtUsers field if non-nil, zero value otherwise.

### GetAtUsersOk

`func (o *SendMessageByWxidRequest) GetAtUsersOk() (*[]string, bool)`

GetAtUsersOk returns a tuple with the AtUsers field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetAtUsers

`func (o *SendMessageByWxidRequest) SetAtUsers(v []string)`

SetAtUsers sets AtUsers field to given value.

### HasAtUsers

`func (o *SendMessageByWxidRequest) HasAtUsers() bool`

HasAtUsers returns a boolean if a field has been set.

### SetAtUsersNil

`func (o *SendMessageByWxidRequest) SetAtUsersNil(b bool)`

 SetAtUsersNil sets the value for AtUsers to be an explicit nil

### UnsetAtUsers
`func (o *SendMessageByWxidRequest) UnsetAtUsers()`

UnsetAtUsers ensures that no value is present for AtUsers, not even an explicit nil
### GetRefer

`func (o *SendMessageByWxidRequest) GetRefer() ChatRefer`

GetRefer returns the Refer field if non-nil, zero value otherwise.

### GetReferOk

`func (o *SendMessageByWxidRequest) GetReferOk() (*ChatRefer, bool)`

GetReferOk returns a tuple with the Refer field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetRefer

`func (o *SendMessageByWxidRequest) SetRefer(v ChatRefer)`

SetRefer sets Refer field to given value.

### HasRefer

`func (o *SendMessageByWxidRequest) HasRefer() bool`

HasRefer returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


