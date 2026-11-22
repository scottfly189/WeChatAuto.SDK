# CreateGroupRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**GroupName** | Pointer to **NullableString** |  | [optional] 
**FirstWho** | Pointer to **NullableString** |  | [optional] 
**Members** | Pointer to **[]string** |  | [optional] 

## Methods

### NewCreateGroupRequest

`func NewCreateGroupRequest() *CreateGroupRequest`

NewCreateGroupRequest instantiates a new CreateGroupRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewCreateGroupRequestWithDefaults

`func NewCreateGroupRequestWithDefaults() *CreateGroupRequest`

NewCreateGroupRequestWithDefaults instantiates a new CreateGroupRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetGroupName

`func (o *CreateGroupRequest) GetGroupName() string`

GetGroupName returns the GroupName field if non-nil, zero value otherwise.

### GetGroupNameOk

`func (o *CreateGroupRequest) GetGroupNameOk() (*string, bool)`

GetGroupNameOk returns a tuple with the GroupName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetGroupName

`func (o *CreateGroupRequest) SetGroupName(v string)`

SetGroupName sets GroupName field to given value.

### HasGroupName

`func (o *CreateGroupRequest) HasGroupName() bool`

HasGroupName returns a boolean if a field has been set.

### SetGroupNameNil

`func (o *CreateGroupRequest) SetGroupNameNil(b bool)`

 SetGroupNameNil sets the value for GroupName to be an explicit nil

### UnsetGroupName
`func (o *CreateGroupRequest) UnsetGroupName()`

UnsetGroupName ensures that no value is present for GroupName, not even an explicit nil
### GetFirstWho

`func (o *CreateGroupRequest) GetFirstWho() string`

GetFirstWho returns the FirstWho field if non-nil, zero value otherwise.

### GetFirstWhoOk

`func (o *CreateGroupRequest) GetFirstWhoOk() (*string, bool)`

GetFirstWhoOk returns a tuple with the FirstWho field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetFirstWho

`func (o *CreateGroupRequest) SetFirstWho(v string)`

SetFirstWho sets FirstWho field to given value.

### HasFirstWho

`func (o *CreateGroupRequest) HasFirstWho() bool`

HasFirstWho returns a boolean if a field has been set.

### SetFirstWhoNil

`func (o *CreateGroupRequest) SetFirstWhoNil(b bool)`

 SetFirstWhoNil sets the value for FirstWho to be an explicit nil

### UnsetFirstWho
`func (o *CreateGroupRequest) UnsetFirstWho()`

UnsetFirstWho ensures that no value is present for FirstWho, not even an explicit nil
### GetMembers

`func (o *CreateGroupRequest) GetMembers() []string`

GetMembers returns the Members field if non-nil, zero value otherwise.

### GetMembersOk

`func (o *CreateGroupRequest) GetMembersOk() (*[]string, bool)`

GetMembersOk returns a tuple with the Members field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMembers

`func (o *CreateGroupRequest) SetMembers(v []string)`

SetMembers sets Members field to given value.

### HasMembers

`func (o *CreateGroupRequest) HasMembers() bool`

HasMembers returns a boolean if a field has been set.

### SetMembersNil

`func (o *CreateGroupRequest) SetMembersNil(b bool)`

 SetMembersNil sets the value for Members to be an explicit nil

### UnsetMembers
`func (o *CreateGroupRequest) UnsetMembers()`

UnsetMembers ensures that no value is present for Members, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


