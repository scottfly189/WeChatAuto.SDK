# SendEmojiByWxidRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**UserName** | Pointer to **NullableString** |  | [optional] 
**Emoji** | Pointer to **NullableString** |  | [optional] 
**Index** | Pointer to **NullableInt32** |  | [optional] 
**AtUsers** | Pointer to **[]string** |  | [optional] 

## Methods

### NewSendEmojiByWxidRequest

`func NewSendEmojiByWxidRequest() *SendEmojiByWxidRequest`

NewSendEmojiByWxidRequest instantiates a new SendEmojiByWxidRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSendEmojiByWxidRequestWithDefaults

`func NewSendEmojiByWxidRequestWithDefaults() *SendEmojiByWxidRequest`

NewSendEmojiByWxidRequestWithDefaults instantiates a new SendEmojiByWxidRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetUserName

`func (o *SendEmojiByWxidRequest) GetUserName() string`

GetUserName returns the UserName field if non-nil, zero value otherwise.

### GetUserNameOk

`func (o *SendEmojiByWxidRequest) GetUserNameOk() (*string, bool)`

GetUserNameOk returns a tuple with the UserName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetUserName

`func (o *SendEmojiByWxidRequest) SetUserName(v string)`

SetUserName sets UserName field to given value.

### HasUserName

`func (o *SendEmojiByWxidRequest) HasUserName() bool`

HasUserName returns a boolean if a field has been set.

### SetUserNameNil

`func (o *SendEmojiByWxidRequest) SetUserNameNil(b bool)`

 SetUserNameNil sets the value for UserName to be an explicit nil

### UnsetUserName
`func (o *SendEmojiByWxidRequest) UnsetUserName()`

UnsetUserName ensures that no value is present for UserName, not even an explicit nil
### GetEmoji

`func (o *SendEmojiByWxidRequest) GetEmoji() string`

GetEmoji returns the Emoji field if non-nil, zero value otherwise.

### GetEmojiOk

`func (o *SendEmojiByWxidRequest) GetEmojiOk() (*string, bool)`

GetEmojiOk returns a tuple with the Emoji field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetEmoji

`func (o *SendEmojiByWxidRequest) SetEmoji(v string)`

SetEmoji sets Emoji field to given value.

### HasEmoji

`func (o *SendEmojiByWxidRequest) HasEmoji() bool`

HasEmoji returns a boolean if a field has been set.

### SetEmojiNil

`func (o *SendEmojiByWxidRequest) SetEmojiNil(b bool)`

 SetEmojiNil sets the value for Emoji to be an explicit nil

### UnsetEmoji
`func (o *SendEmojiByWxidRequest) UnsetEmoji()`

UnsetEmoji ensures that no value is present for Emoji, not even an explicit nil
### GetIndex

`func (o *SendEmojiByWxidRequest) GetIndex() int32`

GetIndex returns the Index field if non-nil, zero value otherwise.

### GetIndexOk

`func (o *SendEmojiByWxidRequest) GetIndexOk() (*int32, bool)`

GetIndexOk returns a tuple with the Index field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIndex

`func (o *SendEmojiByWxidRequest) SetIndex(v int32)`

SetIndex sets Index field to given value.

### HasIndex

`func (o *SendEmojiByWxidRequest) HasIndex() bool`

HasIndex returns a boolean if a field has been set.

### SetIndexNil

`func (o *SendEmojiByWxidRequest) SetIndexNil(b bool)`

 SetIndexNil sets the value for Index to be an explicit nil

### UnsetIndex
`func (o *SendEmojiByWxidRequest) UnsetIndex()`

UnsetIndex ensures that no value is present for Index, not even an explicit nil
### GetAtUsers

`func (o *SendEmojiByWxidRequest) GetAtUsers() []string`

GetAtUsers returns the AtUsers field if non-nil, zero value otherwise.

### GetAtUsersOk

`func (o *SendEmojiByWxidRequest) GetAtUsersOk() (*[]string, bool)`

GetAtUsersOk returns a tuple with the AtUsers field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetAtUsers

`func (o *SendEmojiByWxidRequest) SetAtUsers(v []string)`

SetAtUsers sets AtUsers field to given value.

### HasAtUsers

`func (o *SendEmojiByWxidRequest) HasAtUsers() bool`

HasAtUsers returns a boolean if a field has been set.

### SetAtUsersNil

`func (o *SendEmojiByWxidRequest) SetAtUsersNil(b bool)`

 SetAtUsersNil sets the value for AtUsers to be an explicit nil

### UnsetAtUsers
`func (o *SendEmojiByWxidRequest) UnsetAtUsers()`

UnsetAtUsers ensures that no value is present for AtUsers, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


