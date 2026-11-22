# MomentsOptions

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AtUsrs** | Pointer to **[]string** | 朋友圈被@的好友 | [optional] 
**Labels** | Pointer to **[]string** | 朋友圈哪些设定的标签可以看，如果没有设置标签，则全部可见. | [optional] 
**IsCloseMoments** | Pointer to **bool** | 是否执行操作后关闭朋友圈,默认关闭，也可以设置为False,然后使用者可以手动关闭 | [optional] 

## Methods

### NewMomentsOptions

`func NewMomentsOptions() *MomentsOptions`

NewMomentsOptions instantiates a new MomentsOptions object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewMomentsOptionsWithDefaults

`func NewMomentsOptionsWithDefaults() *MomentsOptions`

NewMomentsOptionsWithDefaults instantiates a new MomentsOptions object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetAtUsrs

`func (o *MomentsOptions) GetAtUsrs() []string`

GetAtUsrs returns the AtUsrs field if non-nil, zero value otherwise.

### GetAtUsrsOk

`func (o *MomentsOptions) GetAtUsrsOk() (*[]string, bool)`

GetAtUsrsOk returns a tuple with the AtUsrs field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetAtUsrs

`func (o *MomentsOptions) SetAtUsrs(v []string)`

SetAtUsrs sets AtUsrs field to given value.

### HasAtUsrs

`func (o *MomentsOptions) HasAtUsrs() bool`

HasAtUsrs returns a boolean if a field has been set.

### SetAtUsrsNil

`func (o *MomentsOptions) SetAtUsrsNil(b bool)`

 SetAtUsrsNil sets the value for AtUsrs to be an explicit nil

### UnsetAtUsrs
`func (o *MomentsOptions) UnsetAtUsrs()`

UnsetAtUsrs ensures that no value is present for AtUsrs, not even an explicit nil
### GetLabels

`func (o *MomentsOptions) GetLabels() []string`

GetLabels returns the Labels field if non-nil, zero value otherwise.

### GetLabelsOk

`func (o *MomentsOptions) GetLabelsOk() (*[]string, bool)`

GetLabelsOk returns a tuple with the Labels field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetLabels

`func (o *MomentsOptions) SetLabels(v []string)`

SetLabels sets Labels field to given value.

### HasLabels

`func (o *MomentsOptions) HasLabels() bool`

HasLabels returns a boolean if a field has been set.

### SetLabelsNil

`func (o *MomentsOptions) SetLabelsNil(b bool)`

 SetLabelsNil sets the value for Labels to be an explicit nil

### UnsetLabels
`func (o *MomentsOptions) UnsetLabels()`

UnsetLabels ensures that no value is present for Labels, not even an explicit nil
### GetIsCloseMoments

`func (o *MomentsOptions) GetIsCloseMoments() bool`

GetIsCloseMoments returns the IsCloseMoments field if non-nil, zero value otherwise.

### GetIsCloseMomentsOk

`func (o *MomentsOptions) GetIsCloseMomentsOk() (*bool, bool)`

GetIsCloseMomentsOk returns a tuple with the IsCloseMoments field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIsCloseMoments

`func (o *MomentsOptions) SetIsCloseMoments(v bool)`

SetIsCloseMoments sets IsCloseMoments field to given value.

### HasIsCloseMoments

`func (o *MomentsOptions) HasIsCloseMoments() bool`

HasIsCloseMoments returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


