# AddGroupMemberToFriendsRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**GroupName** | Pointer to **NullableString** |  | [optional] 
**Members** | Pointer to **[]string** |  | [optional] 
**Options** | Pointer to [**AddFriendsOptions**](AddFriendsOptions.md) |  | [optional] 

## Methods

### NewAddGroupMemberToFriendsRequest

`func NewAddGroupMemberToFriendsRequest() *AddGroupMemberToFriendsRequest`

NewAddGroupMemberToFriendsRequest instantiates a new AddGroupMemberToFriendsRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewAddGroupMemberToFriendsRequestWithDefaults

`func NewAddGroupMemberToFriendsRequestWithDefaults() *AddGroupMemberToFriendsRequest`

NewAddGroupMemberToFriendsRequestWithDefaults instantiates a new AddGroupMemberToFriendsRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetGroupName

`func (o *AddGroupMemberToFriendsRequest) GetGroupName() string`

GetGroupName returns the GroupName field if non-nil, zero value otherwise.

### GetGroupNameOk

`func (o *AddGroupMemberToFriendsRequest) GetGroupNameOk() (*string, bool)`

GetGroupNameOk returns a tuple with the GroupName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetGroupName

`func (o *AddGroupMemberToFriendsRequest) SetGroupName(v string)`

SetGroupName sets GroupName field to given value.

### HasGroupName

`func (o *AddGroupMemberToFriendsRequest) HasGroupName() bool`

HasGroupName returns a boolean if a field has been set.

### SetGroupNameNil

`func (o *AddGroupMemberToFriendsRequest) SetGroupNameNil(b bool)`

 SetGroupNameNil sets the value for GroupName to be an explicit nil

### UnsetGroupName
`func (o *AddGroupMemberToFriendsRequest) UnsetGroupName()`

UnsetGroupName ensures that no value is present for GroupName, not even an explicit nil
### GetMembers

`func (o *AddGroupMemberToFriendsRequest) GetMembers() []string`

GetMembers returns the Members field if non-nil, zero value otherwise.

### GetMembersOk

`func (o *AddGroupMemberToFriendsRequest) GetMembersOk() (*[]string, bool)`

GetMembersOk returns a tuple with the Members field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMembers

`func (o *AddGroupMemberToFriendsRequest) SetMembers(v []string)`

SetMembers sets Members field to given value.

### HasMembers

`func (o *AddGroupMemberToFriendsRequest) HasMembers() bool`

HasMembers returns a boolean if a field has been set.

### SetMembersNil

`func (o *AddGroupMemberToFriendsRequest) SetMembersNil(b bool)`

 SetMembersNil sets the value for Members to be an explicit nil

### UnsetMembers
`func (o *AddGroupMemberToFriendsRequest) UnsetMembers()`

UnsetMembers ensures that no value is present for Members, not even an explicit nil
### GetOptions

`func (o *AddGroupMemberToFriendsRequest) GetOptions() AddFriendsOptions`

GetOptions returns the Options field if non-nil, zero value otherwise.

### GetOptionsOk

`func (o *AddGroupMemberToFriendsRequest) GetOptionsOk() (*AddFriendsOptions, bool)`

GetOptionsOk returns a tuple with the Options field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetOptions

`func (o *AddGroupMemberToFriendsRequest) SetOptions(v AddFriendsOptions)`

SetOptions sets Options field to given value.

### HasOptions

`func (o *AddGroupMemberToFriendsRequest) HasOptions() bool`

HasOptions returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


