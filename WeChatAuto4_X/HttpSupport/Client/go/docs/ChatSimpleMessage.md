# ChatSimpleMessage

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** | 微信名称 | [optional] 
**Message** | Pointer to **NullableString** | 消息 | [optional] 
**SendDateTime** | Pointer to **NullableString** | 消息日期 | [optional] 
**DateTime** | Pointer to **time.Time** | 消息日期,日期时间格式 | [optional] 
**UniqueString** | Pointer to **NullableString** | 唯一字符串 | [optional] 

## Methods

### NewChatSimpleMessage

`func NewChatSimpleMessage() *ChatSimpleMessage`

NewChatSimpleMessage instantiates a new ChatSimpleMessage object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewChatSimpleMessageWithDefaults

`func NewChatSimpleMessageWithDefaults() *ChatSimpleMessage`

NewChatSimpleMessageWithDefaults instantiates a new ChatSimpleMessage object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *ChatSimpleMessage) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *ChatSimpleMessage) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *ChatSimpleMessage) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *ChatSimpleMessage) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *ChatSimpleMessage) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *ChatSimpleMessage) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetMessage

`func (o *ChatSimpleMessage) GetMessage() string`

GetMessage returns the Message field if non-nil, zero value otherwise.

### GetMessageOk

`func (o *ChatSimpleMessage) GetMessageOk() (*string, bool)`

GetMessageOk returns a tuple with the Message field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMessage

`func (o *ChatSimpleMessage) SetMessage(v string)`

SetMessage sets Message field to given value.

### HasMessage

`func (o *ChatSimpleMessage) HasMessage() bool`

HasMessage returns a boolean if a field has been set.

### SetMessageNil

`func (o *ChatSimpleMessage) SetMessageNil(b bool)`

 SetMessageNil sets the value for Message to be an explicit nil

### UnsetMessage
`func (o *ChatSimpleMessage) UnsetMessage()`

UnsetMessage ensures that no value is present for Message, not even an explicit nil
### GetSendDateTime

`func (o *ChatSimpleMessage) GetSendDateTime() string`

GetSendDateTime returns the SendDateTime field if non-nil, zero value otherwise.

### GetSendDateTimeOk

`func (o *ChatSimpleMessage) GetSendDateTimeOk() (*string, bool)`

GetSendDateTimeOk returns a tuple with the SendDateTime field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSendDateTime

`func (o *ChatSimpleMessage) SetSendDateTime(v string)`

SetSendDateTime sets SendDateTime field to given value.

### HasSendDateTime

`func (o *ChatSimpleMessage) HasSendDateTime() bool`

HasSendDateTime returns a boolean if a field has been set.

### SetSendDateTimeNil

`func (o *ChatSimpleMessage) SetSendDateTimeNil(b bool)`

 SetSendDateTimeNil sets the value for SendDateTime to be an explicit nil

### UnsetSendDateTime
`func (o *ChatSimpleMessage) UnsetSendDateTime()`

UnsetSendDateTime ensures that no value is present for SendDateTime, not even an explicit nil
### GetDateTime

`func (o *ChatSimpleMessage) GetDateTime() time.Time`

GetDateTime returns the DateTime field if non-nil, zero value otherwise.

### GetDateTimeOk

`func (o *ChatSimpleMessage) GetDateTimeOk() (*time.Time, bool)`

GetDateTimeOk returns a tuple with the DateTime field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetDateTime

`func (o *ChatSimpleMessage) SetDateTime(v time.Time)`

SetDateTime sets DateTime field to given value.

### HasDateTime

`func (o *ChatSimpleMessage) HasDateTime() bool`

HasDateTime returns a boolean if a field has been set.

### GetUniqueString

`func (o *ChatSimpleMessage) GetUniqueString() string`

GetUniqueString returns the UniqueString field if non-nil, zero value otherwise.

### GetUniqueStringOk

`func (o *ChatSimpleMessage) GetUniqueStringOk() (*string, bool)`

GetUniqueStringOk returns a tuple with the UniqueString field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetUniqueString

`func (o *ChatSimpleMessage) SetUniqueString(v string)`

SetUniqueString sets UniqueString field to given value.

### HasUniqueString

`func (o *ChatSimpleMessage) HasUniqueString() bool`

HasUniqueString returns a boolean if a field has been set.

### SetUniqueStringNil

`func (o *ChatSimpleMessage) SetUniqueStringNil(b bool)`

 SetUniqueStringNil sets the value for UniqueString to be an explicit nil

### UnsetUniqueString
`func (o *ChatSimpleMessage) UnsetUniqueString()`

UnsetUniqueString ensures that no value is present for UniqueString, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


