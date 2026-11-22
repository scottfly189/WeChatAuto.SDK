# SendEmojiRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**Emoji** | Pointer to **NullableString** | 表情名称或描述。 | [optional] 
**Index** | Pointer to **NullableInt32** | 表情索引。 | [optional] 
**AtUsers** | Pointer to **[]string** |  | [optional] 

## Methods

### NewSendEmojiRequest

`func NewSendEmojiRequest() *SendEmojiRequest`

NewSendEmojiRequest instantiates a new SendEmojiRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSendEmojiRequestWithDefaults

`func NewSendEmojiRequestWithDefaults() *SendEmojiRequest`

NewSendEmojiRequestWithDefaults instantiates a new SendEmojiRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *SendEmojiRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *SendEmojiRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *SendEmojiRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *SendEmojiRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *SendEmojiRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *SendEmojiRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetEmoji

`func (o *SendEmojiRequest) GetEmoji() string`

GetEmoji returns the Emoji field if non-nil, zero value otherwise.

### GetEmojiOk

`func (o *SendEmojiRequest) GetEmojiOk() (*string, bool)`

GetEmojiOk returns a tuple with the Emoji field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetEmoji

`func (o *SendEmojiRequest) SetEmoji(v string)`

SetEmoji sets Emoji field to given value.

### HasEmoji

`func (o *SendEmojiRequest) HasEmoji() bool`

HasEmoji returns a boolean if a field has been set.

### SetEmojiNil

`func (o *SendEmojiRequest) SetEmojiNil(b bool)`

 SetEmojiNil sets the value for Emoji to be an explicit nil

### UnsetEmoji
`func (o *SendEmojiRequest) UnsetEmoji()`

UnsetEmoji ensures that no value is present for Emoji, not even an explicit nil
### GetIndex

`func (o *SendEmojiRequest) GetIndex() int32`

GetIndex returns the Index field if non-nil, zero value otherwise.

### GetIndexOk

`func (o *SendEmojiRequest) GetIndexOk() (*int32, bool)`

GetIndexOk returns a tuple with the Index field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIndex

`func (o *SendEmojiRequest) SetIndex(v int32)`

SetIndex sets Index field to given value.

### HasIndex

`func (o *SendEmojiRequest) HasIndex() bool`

HasIndex returns a boolean if a field has been set.

### SetIndexNil

`func (o *SendEmojiRequest) SetIndexNil(b bool)`

 SetIndexNil sets the value for Index to be an explicit nil

### UnsetIndex
`func (o *SendEmojiRequest) UnsetIndex()`

UnsetIndex ensures that no value is present for Index, not even an explicit nil
### GetAtUsers

`func (o *SendEmojiRequest) GetAtUsers() []string`

GetAtUsers returns the AtUsers field if non-nil, zero value otherwise.

### GetAtUsersOk

`func (o *SendEmojiRequest) GetAtUsersOk() (*[]string, bool)`

GetAtUsersOk returns a tuple with the AtUsers field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetAtUsers

`func (o *SendEmojiRequest) SetAtUsers(v []string)`

SetAtUsers sets AtUsers field to given value.

### HasAtUsers

`func (o *SendEmojiRequest) HasAtUsers() bool`

HasAtUsers returns a boolean if a field has been set.

### SetAtUsersNil

`func (o *SendEmojiRequest) SetAtUsersNil(b bool)`

 SetAtUsersNil sets the value for AtUsers to be an explicit nil

### UnsetAtUsers
`func (o *SendEmojiRequest) UnsetAtUsers()`

UnsetAtUsers ensures that no value is present for AtUsers, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


