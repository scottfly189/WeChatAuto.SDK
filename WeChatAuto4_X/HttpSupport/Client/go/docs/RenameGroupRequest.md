# RenameGroupRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**OldGroupName** | Pointer to **NullableString** |  | [optional] 
**NewGroupName** | Pointer to **NullableString** |  | [optional] 

## Methods

### NewRenameGroupRequest

`func NewRenameGroupRequest() *RenameGroupRequest`

NewRenameGroupRequest instantiates a new RenameGroupRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewRenameGroupRequestWithDefaults

`func NewRenameGroupRequestWithDefaults() *RenameGroupRequest`

NewRenameGroupRequestWithDefaults instantiates a new RenameGroupRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetOldGroupName

`func (o *RenameGroupRequest) GetOldGroupName() string`

GetOldGroupName returns the OldGroupName field if non-nil, zero value otherwise.

### GetOldGroupNameOk

`func (o *RenameGroupRequest) GetOldGroupNameOk() (*string, bool)`

GetOldGroupNameOk returns a tuple with the OldGroupName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetOldGroupName

`func (o *RenameGroupRequest) SetOldGroupName(v string)`

SetOldGroupName sets OldGroupName field to given value.

### HasOldGroupName

`func (o *RenameGroupRequest) HasOldGroupName() bool`

HasOldGroupName returns a boolean if a field has been set.

### SetOldGroupNameNil

`func (o *RenameGroupRequest) SetOldGroupNameNil(b bool)`

 SetOldGroupNameNil sets the value for OldGroupName to be an explicit nil

### UnsetOldGroupName
`func (o *RenameGroupRequest) UnsetOldGroupName()`

UnsetOldGroupName ensures that no value is present for OldGroupName, not even an explicit nil
### GetNewGroupName

`func (o *RenameGroupRequest) GetNewGroupName() string`

GetNewGroupName returns the NewGroupName field if non-nil, zero value otherwise.

### GetNewGroupNameOk

`func (o *RenameGroupRequest) GetNewGroupNameOk() (*string, bool)`

GetNewGroupNameOk returns a tuple with the NewGroupName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetNewGroupName

`func (o *RenameGroupRequest) SetNewGroupName(v string)`

SetNewGroupName sets NewGroupName field to given value.

### HasNewGroupName

`func (o *RenameGroupRequest) HasNewGroupName() bool`

HasNewGroupName returns a boolean if a field has been set.

### SetNewGroupNameNil

`func (o *RenameGroupRequest) SetNewGroupNameNil(b bool)`

 SetNewGroupNameNil sets the value for NewGroupName to be an explicit nil

### UnsetNewGroupName
`func (o *RenameGroupRequest) UnsetNewGroupName()`

UnsetNewGroupName ensures that no value is present for NewGroupName, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


