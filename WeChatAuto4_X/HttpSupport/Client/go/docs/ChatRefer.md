# ChatRefer

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Date** | Pointer to **string** | 日期,如果不设置则不进行日期筛选 | [optional] 
**Message** | Pointer to [**ChatSimpleMessage**](ChatSimpleMessage.md) |  | [optional] 
**IsCloseSearchWin** | Pointer to **bool** | 是否关闭查找窗口，默认是关闭，如果设置为false,则不关闭查找窗口，速度会略快，但需要自行关闭查找窗口. | [optional] 

## Methods

### NewChatRefer

`func NewChatRefer() *ChatRefer`

NewChatRefer instantiates a new ChatRefer object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewChatReferWithDefaults

`func NewChatReferWithDefaults() *ChatRefer`

NewChatReferWithDefaults instantiates a new ChatRefer object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetDate

`func (o *ChatRefer) GetDate() string`

GetDate returns the Date field if non-nil, zero value otherwise.

### GetDateOk

`func (o *ChatRefer) GetDateOk() (*string, bool)`

GetDateOk returns a tuple with the Date field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetDate

`func (o *ChatRefer) SetDate(v string)`

SetDate sets Date field to given value.

### HasDate

`func (o *ChatRefer) HasDate() bool`

HasDate returns a boolean if a field has been set.

### GetMessage

`func (o *ChatRefer) GetMessage() ChatSimpleMessage`

GetMessage returns the Message field if non-nil, zero value otherwise.

### GetMessageOk

`func (o *ChatRefer) GetMessageOk() (*ChatSimpleMessage, bool)`

GetMessageOk returns a tuple with the Message field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMessage

`func (o *ChatRefer) SetMessage(v ChatSimpleMessage)`

SetMessage sets Message field to given value.

### HasMessage

`func (o *ChatRefer) HasMessage() bool`

HasMessage returns a boolean if a field has been set.

### GetIsCloseSearchWin

`func (o *ChatRefer) GetIsCloseSearchWin() bool`

GetIsCloseSearchWin returns the IsCloseSearchWin field if non-nil, zero value otherwise.

### GetIsCloseSearchWinOk

`func (o *ChatRefer) GetIsCloseSearchWinOk() (*bool, bool)`

GetIsCloseSearchWinOk returns a tuple with the IsCloseSearchWin field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIsCloseSearchWin

`func (o *ChatRefer) SetIsCloseSearchWin(v bool)`

SetIsCloseSearchWin sets IsCloseSearchWin field to given value.

### HasIsCloseSearchWin

`func (o *ChatRefer) HasIsCloseSearchWin() bool`

HasIsCloseSearchWin returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


