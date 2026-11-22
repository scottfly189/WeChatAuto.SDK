# SendFileByWxidRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**UserName** | Pointer to **NullableString** |  | [optional] 
**Files** | Pointer to **[]string** |  | [optional] 

## Methods

### NewSendFileByWxidRequest

`func NewSendFileByWxidRequest() *SendFileByWxidRequest`

NewSendFileByWxidRequest instantiates a new SendFileByWxidRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSendFileByWxidRequestWithDefaults

`func NewSendFileByWxidRequestWithDefaults() *SendFileByWxidRequest`

NewSendFileByWxidRequestWithDefaults instantiates a new SendFileByWxidRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetUserName

`func (o *SendFileByWxidRequest) GetUserName() string`

GetUserName returns the UserName field if non-nil, zero value otherwise.

### GetUserNameOk

`func (o *SendFileByWxidRequest) GetUserNameOk() (*string, bool)`

GetUserNameOk returns a tuple with the UserName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetUserName

`func (o *SendFileByWxidRequest) SetUserName(v string)`

SetUserName sets UserName field to given value.

### HasUserName

`func (o *SendFileByWxidRequest) HasUserName() bool`

HasUserName returns a boolean if a field has been set.

### SetUserNameNil

`func (o *SendFileByWxidRequest) SetUserNameNil(b bool)`

 SetUserNameNil sets the value for UserName to be an explicit nil

### UnsetUserName
`func (o *SendFileByWxidRequest) UnsetUserName()`

UnsetUserName ensures that no value is present for UserName, not even an explicit nil
### GetFiles

`func (o *SendFileByWxidRequest) GetFiles() []string`

GetFiles returns the Files field if non-nil, zero value otherwise.

### GetFilesOk

`func (o *SendFileByWxidRequest) GetFilesOk() (*[]string, bool)`

GetFilesOk returns a tuple with the Files field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetFiles

`func (o *SendFileByWxidRequest) SetFiles(v []string)`

SetFiles sets Files field to given value.

### HasFiles

`func (o *SendFileByWxidRequest) HasFiles() bool`

HasFiles returns a boolean if a field has been set.

### SetFilesNil

`func (o *SendFileByWxidRequest) SetFilesNil(b bool)`

 SetFilesNil sets the value for Files to be an explicit nil

### UnsetFiles
`func (o *SendFileByWxidRequest) UnsetFiles()`

UnsetFiles ensures that no value is present for Files, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


