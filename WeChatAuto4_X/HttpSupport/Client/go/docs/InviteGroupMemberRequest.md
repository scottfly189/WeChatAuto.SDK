# InviteGroupMemberRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**GroupName** | Pointer to **NullableString** |  | [optional] 
**Members** | Pointer to **[]string** |  | [optional] 
**InviteReason** | Pointer to **NullableString** |  | [optional] 

## Methods

### NewInviteGroupMemberRequest

`func NewInviteGroupMemberRequest() *InviteGroupMemberRequest`

NewInviteGroupMemberRequest instantiates a new InviteGroupMemberRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewInviteGroupMemberRequestWithDefaults

`func NewInviteGroupMemberRequestWithDefaults() *InviteGroupMemberRequest`

NewInviteGroupMemberRequestWithDefaults instantiates a new InviteGroupMemberRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetGroupName

`func (o *InviteGroupMemberRequest) GetGroupName() string`

GetGroupName returns the GroupName field if non-nil, zero value otherwise.

### GetGroupNameOk

`func (o *InviteGroupMemberRequest) GetGroupNameOk() (*string, bool)`

GetGroupNameOk returns a tuple with the GroupName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetGroupName

`func (o *InviteGroupMemberRequest) SetGroupName(v string)`

SetGroupName sets GroupName field to given value.

### HasGroupName

`func (o *InviteGroupMemberRequest) HasGroupName() bool`

HasGroupName returns a boolean if a field has been set.

### SetGroupNameNil

`func (o *InviteGroupMemberRequest) SetGroupNameNil(b bool)`

 SetGroupNameNil sets the value for GroupName to be an explicit nil

### UnsetGroupName
`func (o *InviteGroupMemberRequest) UnsetGroupName()`

UnsetGroupName ensures that no value is present for GroupName, not even an explicit nil
### GetMembers

`func (o *InviteGroupMemberRequest) GetMembers() []string`

GetMembers returns the Members field if non-nil, zero value otherwise.

### GetMembersOk

`func (o *InviteGroupMemberRequest) GetMembersOk() (*[]string, bool)`

GetMembersOk returns a tuple with the Members field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMembers

`func (o *InviteGroupMemberRequest) SetMembers(v []string)`

SetMembers sets Members field to given value.

### HasMembers

`func (o *InviteGroupMemberRequest) HasMembers() bool`

HasMembers returns a boolean if a field has been set.

### SetMembersNil

`func (o *InviteGroupMemberRequest) SetMembersNil(b bool)`

 SetMembersNil sets the value for Members to be an explicit nil

### UnsetMembers
`func (o *InviteGroupMemberRequest) UnsetMembers()`

UnsetMembers ensures that no value is present for Members, not even an explicit nil
### GetInviteReason

`func (o *InviteGroupMemberRequest) GetInviteReason() string`

GetInviteReason returns the InviteReason field if non-nil, zero value otherwise.

### GetInviteReasonOk

`func (o *InviteGroupMemberRequest) GetInviteReasonOk() (*string, bool)`

GetInviteReasonOk returns a tuple with the InviteReason field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetInviteReason

`func (o *InviteGroupMemberRequest) SetInviteReason(v string)`

SetInviteReason sets InviteReason field to given value.

### HasInviteReason

`func (o *InviteGroupMemberRequest) HasInviteReason() bool`

HasInviteReason returns a boolean if a field has been set.

### SetInviteReasonNil

`func (o *InviteGroupMemberRequest) SetInviteReasonNil(b bool)`

 SetInviteReasonNil sets the value for InviteReason to be an explicit nil

### UnsetInviteReason
`func (o *InviteGroupMemberRequest) UnsetInviteReason()`

UnsetInviteReason ensures that no value is present for InviteReason, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


