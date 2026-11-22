# AddFriendsRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Friends** | Pointer to **[]string** |  | [optional] 
**Options** | Pointer to [**AddFriendsOptions**](AddFriendsOptions.md) |  | [optional] 

## Methods

### NewAddFriendsRequest

`func NewAddFriendsRequest() *AddFriendsRequest`

NewAddFriendsRequest instantiates a new AddFriendsRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewAddFriendsRequestWithDefaults

`func NewAddFriendsRequestWithDefaults() *AddFriendsRequest`

NewAddFriendsRequestWithDefaults instantiates a new AddFriendsRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetFriends

`func (o *AddFriendsRequest) GetFriends() []string`

GetFriends returns the Friends field if non-nil, zero value otherwise.

### GetFriendsOk

`func (o *AddFriendsRequest) GetFriendsOk() (*[]string, bool)`

GetFriendsOk returns a tuple with the Friends field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetFriends

`func (o *AddFriendsRequest) SetFriends(v []string)`

SetFriends sets Friends field to given value.

### HasFriends

`func (o *AddFriendsRequest) HasFriends() bool`

HasFriends returns a boolean if a field has been set.

### SetFriendsNil

`func (o *AddFriendsRequest) SetFriendsNil(b bool)`

 SetFriendsNil sets the value for Friends to be an explicit nil

### UnsetFriends
`func (o *AddFriendsRequest) UnsetFriends()`

UnsetFriends ensures that no value is present for Friends, not even an explicit nil
### GetOptions

`func (o *AddFriendsRequest) GetOptions() AddFriendsOptions`

GetOptions returns the Options field if non-nil, zero value otherwise.

### GetOptionsOk

`func (o *AddFriendsRequest) GetOptionsOk() (*AddFriendsOptions, bool)`

GetOptionsOk returns a tuple with the Options field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetOptions

`func (o *AddFriendsRequest) SetOptions(v AddFriendsOptions)`

SetOptions sets Options field to given value.

### HasOptions

`func (o *AddFriendsRequest) HasOptions() bool`

HasOptions returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


