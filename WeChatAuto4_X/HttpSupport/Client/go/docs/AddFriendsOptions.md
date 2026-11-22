# AddFriendsOptions

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**IntervalTime** | Pointer to **int32** | 间隔时间,以秒为单位，默认为三秒，如果担心风控，可以把此时间设置长一点 | [optional] 
**IsCloseWin** | Pointer to **bool** | 是否关闭增加朋友窗口,默认关闭，可以设置为false不关闭 | [optional] 
**SayHi** | Pointer to **NullableString** | 加好友时打招呼内容,如果为空，则保持微信默认 | [optional] 
**Suffix** | Pointer to **NullableString** | 备注后缀，如：设置suffix为\&quot;test\&quot;,则备注为:xxxx_test,可以未来以备注的后缀来区分不同的好友分类 | [optional] 
**Label** | Pointer to **NullableString** | 设置标签 | [optional] 

## Methods

### NewAddFriendsOptions

`func NewAddFriendsOptions() *AddFriendsOptions`

NewAddFriendsOptions instantiates a new AddFriendsOptions object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewAddFriendsOptionsWithDefaults

`func NewAddFriendsOptionsWithDefaults() *AddFriendsOptions`

NewAddFriendsOptionsWithDefaults instantiates a new AddFriendsOptions object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetIntervalTime

`func (o *AddFriendsOptions) GetIntervalTime() int32`

GetIntervalTime returns the IntervalTime field if non-nil, zero value otherwise.

### GetIntervalTimeOk

`func (o *AddFriendsOptions) GetIntervalTimeOk() (*int32, bool)`

GetIntervalTimeOk returns a tuple with the IntervalTime field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIntervalTime

`func (o *AddFriendsOptions) SetIntervalTime(v int32)`

SetIntervalTime sets IntervalTime field to given value.

### HasIntervalTime

`func (o *AddFriendsOptions) HasIntervalTime() bool`

HasIntervalTime returns a boolean if a field has been set.

### GetIsCloseWin

`func (o *AddFriendsOptions) GetIsCloseWin() bool`

GetIsCloseWin returns the IsCloseWin field if non-nil, zero value otherwise.

### GetIsCloseWinOk

`func (o *AddFriendsOptions) GetIsCloseWinOk() (*bool, bool)`

GetIsCloseWinOk returns a tuple with the IsCloseWin field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIsCloseWin

`func (o *AddFriendsOptions) SetIsCloseWin(v bool)`

SetIsCloseWin sets IsCloseWin field to given value.

### HasIsCloseWin

`func (o *AddFriendsOptions) HasIsCloseWin() bool`

HasIsCloseWin returns a boolean if a field has been set.

### GetSayHi

`func (o *AddFriendsOptions) GetSayHi() string`

GetSayHi returns the SayHi field if non-nil, zero value otherwise.

### GetSayHiOk

`func (o *AddFriendsOptions) GetSayHiOk() (*string, bool)`

GetSayHiOk returns a tuple with the SayHi field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSayHi

`func (o *AddFriendsOptions) SetSayHi(v string)`

SetSayHi sets SayHi field to given value.

### HasSayHi

`func (o *AddFriendsOptions) HasSayHi() bool`

HasSayHi returns a boolean if a field has been set.

### SetSayHiNil

`func (o *AddFriendsOptions) SetSayHiNil(b bool)`

 SetSayHiNil sets the value for SayHi to be an explicit nil

### UnsetSayHi
`func (o *AddFriendsOptions) UnsetSayHi()`

UnsetSayHi ensures that no value is present for SayHi, not even an explicit nil
### GetSuffix

`func (o *AddFriendsOptions) GetSuffix() string`

GetSuffix returns the Suffix field if non-nil, zero value otherwise.

### GetSuffixOk

`func (o *AddFriendsOptions) GetSuffixOk() (*string, bool)`

GetSuffixOk returns a tuple with the Suffix field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSuffix

`func (o *AddFriendsOptions) SetSuffix(v string)`

SetSuffix sets Suffix field to given value.

### HasSuffix

`func (o *AddFriendsOptions) HasSuffix() bool`

HasSuffix returns a boolean if a field has been set.

### SetSuffixNil

`func (o *AddFriendsOptions) SetSuffixNil(b bool)`

 SetSuffixNil sets the value for Suffix to be an explicit nil

### UnsetSuffix
`func (o *AddFriendsOptions) UnsetSuffix()`

UnsetSuffix ensures that no value is present for Suffix, not even an explicit nil
### GetLabel

`func (o *AddFriendsOptions) GetLabel() string`

GetLabel returns the Label field if non-nil, zero value otherwise.

### GetLabelOk

`func (o *AddFriendsOptions) GetLabelOk() (*string, bool)`

GetLabelOk returns a tuple with the Label field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetLabel

`func (o *AddFriendsOptions) SetLabel(v string)`

SetLabel sets Label field to given value.

### HasLabel

`func (o *AddFriendsOptions) HasLabel() bool`

HasLabel returns a boolean if a field has been set.

### SetLabelNil

`func (o *AddFriendsOptions) SetLabelNil(b bool)`

 SetLabelNil sets the value for Label to be an explicit nil

### UnsetLabel
`func (o *AddFriendsOptions) UnsetLabel()`

UnsetLabel ensures that no value is present for Label, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


