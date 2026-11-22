# SendVoiceMessageRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**FilePath** | Pointer to **NullableString** |  | [optional] 

## Methods

### NewSendVoiceMessageRequest

`func NewSendVoiceMessageRequest() *SendVoiceMessageRequest`

NewSendVoiceMessageRequest instantiates a new SendVoiceMessageRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSendVoiceMessageRequestWithDefaults

`func NewSendVoiceMessageRequestWithDefaults() *SendVoiceMessageRequest`

NewSendVoiceMessageRequestWithDefaults instantiates a new SendVoiceMessageRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *SendVoiceMessageRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *SendVoiceMessageRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *SendVoiceMessageRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *SendVoiceMessageRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *SendVoiceMessageRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *SendVoiceMessageRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetFilePath

`func (o *SendVoiceMessageRequest) GetFilePath() string`

GetFilePath returns the FilePath field if non-nil, zero value otherwise.

### GetFilePathOk

`func (o *SendVoiceMessageRequest) GetFilePathOk() (*string, bool)`

GetFilePathOk returns a tuple with the FilePath field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetFilePath

`func (o *SendVoiceMessageRequest) SetFilePath(v string)`

SetFilePath sets FilePath field to given value.

### HasFilePath

`func (o *SendVoiceMessageRequest) HasFilePath() bool`

HasFilePath returns a boolean if a field has been set.

### SetFilePathNil

`func (o *SendVoiceMessageRequest) SetFilePathNil(b bool)`

 SetFilePathNil sets the value for FilePath to be an explicit nil

### UnsetFilePath
`func (o *SendVoiceMessageRequest) UnsetFilePath()`

UnsetFilePath ensures that no value is present for FilePath, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


