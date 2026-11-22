# ChangeGroupMemoRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**GroupName** | Pointer to **NullableString** |  | [optional] 
**NewMemo** | Pointer to **NullableString** |  | [optional] 

## Methods

### NewChangeGroupMemoRequest

`func NewChangeGroupMemoRequest() *ChangeGroupMemoRequest`

NewChangeGroupMemoRequest instantiates a new ChangeGroupMemoRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewChangeGroupMemoRequestWithDefaults

`func NewChangeGroupMemoRequestWithDefaults() *ChangeGroupMemoRequest`

NewChangeGroupMemoRequestWithDefaults instantiates a new ChangeGroupMemoRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetGroupName

`func (o *ChangeGroupMemoRequest) GetGroupName() string`

GetGroupName returns the GroupName field if non-nil, zero value otherwise.

### GetGroupNameOk

`func (o *ChangeGroupMemoRequest) GetGroupNameOk() (*string, bool)`

GetGroupNameOk returns a tuple with the GroupName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetGroupName

`func (o *ChangeGroupMemoRequest) SetGroupName(v string)`

SetGroupName sets GroupName field to given value.

### HasGroupName

`func (o *ChangeGroupMemoRequest) HasGroupName() bool`

HasGroupName returns a boolean if a field has been set.

### SetGroupNameNil

`func (o *ChangeGroupMemoRequest) SetGroupNameNil(b bool)`

 SetGroupNameNil sets the value for GroupName to be an explicit nil

### UnsetGroupName
`func (o *ChangeGroupMemoRequest) UnsetGroupName()`

UnsetGroupName ensures that no value is present for GroupName, not even an explicit nil
### GetNewMemo

`func (o *ChangeGroupMemoRequest) GetNewMemo() string`

GetNewMemo returns the NewMemo field if non-nil, zero value otherwise.

### GetNewMemoOk

`func (o *ChangeGroupMemoRequest) GetNewMemoOk() (*string, bool)`

GetNewMemoOk returns a tuple with the NewMemo field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetNewMemo

`func (o *ChangeGroupMemoRequest) SetNewMemo(v string)`

SetNewMemo sets NewMemo field to given value.

### HasNewMemo

`func (o *ChangeGroupMemoRequest) HasNewMemo() bool`

HasNewMemo returns a boolean if a field has been set.

### SetNewMemoNil

`func (o *ChangeGroupMemoRequest) SetNewMemoNil(b bool)`

 SetNewMemoNil sets the value for NewMemo to be an explicit nil

### UnsetNewMemo
`func (o *ChangeGroupMemoRequest) UnsetNewMemo()`

UnsetNewMemo ensures that no value is present for NewMemo, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


