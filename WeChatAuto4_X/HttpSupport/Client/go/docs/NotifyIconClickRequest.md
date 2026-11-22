# NotifyIconClickRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Index** | Pointer to **NullableInt32** |  | [optional] 
**WechatName** | Pointer to **NullableString** |  | [optional] 

## Methods

### NewNotifyIconClickRequest

`func NewNotifyIconClickRequest() *NotifyIconClickRequest`

NewNotifyIconClickRequest instantiates a new NotifyIconClickRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewNotifyIconClickRequestWithDefaults

`func NewNotifyIconClickRequestWithDefaults() *NotifyIconClickRequest`

NewNotifyIconClickRequestWithDefaults instantiates a new NotifyIconClickRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetIndex

`func (o *NotifyIconClickRequest) GetIndex() int32`

GetIndex returns the Index field if non-nil, zero value otherwise.

### GetIndexOk

`func (o *NotifyIconClickRequest) GetIndexOk() (*int32, bool)`

GetIndexOk returns a tuple with the Index field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIndex

`func (o *NotifyIconClickRequest) SetIndex(v int32)`

SetIndex sets Index field to given value.

### HasIndex

`func (o *NotifyIconClickRequest) HasIndex() bool`

HasIndex returns a boolean if a field has been set.

### SetIndexNil

`func (o *NotifyIconClickRequest) SetIndexNil(b bool)`

 SetIndexNil sets the value for Index to be an explicit nil

### UnsetIndex
`func (o *NotifyIconClickRequest) UnsetIndex()`

UnsetIndex ensures that no value is present for Index, not even an explicit nil
### GetWechatName

`func (o *NotifyIconClickRequest) GetWechatName() string`

GetWechatName returns the WechatName field if non-nil, zero value otherwise.

### GetWechatNameOk

`func (o *NotifyIconClickRequest) GetWechatNameOk() (*string, bool)`

GetWechatNameOk returns a tuple with the WechatName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWechatName

`func (o *NotifyIconClickRequest) SetWechatName(v string)`

SetWechatName sets WechatName field to given value.

### HasWechatName

`func (o *NotifyIconClickRequest) HasWechatName() bool`

HasWechatName returns a boolean if a field has been set.

### SetWechatNameNil

`func (o *NotifyIconClickRequest) SetWechatNameNil(b bool)`

 SetWechatNameNil sets the value for WechatName to be an explicit nil

### UnsetWechatName
`func (o *NotifyIconClickRequest) UnsetWechatName()`

UnsetWechatName ensures that no value is present for WechatName, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


