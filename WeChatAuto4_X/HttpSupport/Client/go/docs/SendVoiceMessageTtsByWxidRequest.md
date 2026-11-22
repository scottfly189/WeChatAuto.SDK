# SendVoiceMessageTtsByWxidRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**UserName** | Pointer to **NullableString** |  | [optional] 
**ApiKey** | Pointer to **NullableString** |  | [optional] 
**Message** | Pointer to **NullableString** |  | [optional] 
**Options** | Pointer to [**VoiceOptions**](VoiceOptions.md) |  | [optional] 
**OptimizeWithLlm** | Pointer to **bool** |  | [optional] 

## Methods

### NewSendVoiceMessageTtsByWxidRequest

`func NewSendVoiceMessageTtsByWxidRequest() *SendVoiceMessageTtsByWxidRequest`

NewSendVoiceMessageTtsByWxidRequest instantiates a new SendVoiceMessageTtsByWxidRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSendVoiceMessageTtsByWxidRequestWithDefaults

`func NewSendVoiceMessageTtsByWxidRequestWithDefaults() *SendVoiceMessageTtsByWxidRequest`

NewSendVoiceMessageTtsByWxidRequestWithDefaults instantiates a new SendVoiceMessageTtsByWxidRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetUserName

`func (o *SendVoiceMessageTtsByWxidRequest) GetUserName() string`

GetUserName returns the UserName field if non-nil, zero value otherwise.

### GetUserNameOk

`func (o *SendVoiceMessageTtsByWxidRequest) GetUserNameOk() (*string, bool)`

GetUserNameOk returns a tuple with the UserName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetUserName

`func (o *SendVoiceMessageTtsByWxidRequest) SetUserName(v string)`

SetUserName sets UserName field to given value.

### HasUserName

`func (o *SendVoiceMessageTtsByWxidRequest) HasUserName() bool`

HasUserName returns a boolean if a field has been set.

### SetUserNameNil

`func (o *SendVoiceMessageTtsByWxidRequest) SetUserNameNil(b bool)`

 SetUserNameNil sets the value for UserName to be an explicit nil

### UnsetUserName
`func (o *SendVoiceMessageTtsByWxidRequest) UnsetUserName()`

UnsetUserName ensures that no value is present for UserName, not even an explicit nil
### GetApiKey

`func (o *SendVoiceMessageTtsByWxidRequest) GetApiKey() string`

GetApiKey returns the ApiKey field if non-nil, zero value otherwise.

### GetApiKeyOk

`func (o *SendVoiceMessageTtsByWxidRequest) GetApiKeyOk() (*string, bool)`

GetApiKeyOk returns a tuple with the ApiKey field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetApiKey

`func (o *SendVoiceMessageTtsByWxidRequest) SetApiKey(v string)`

SetApiKey sets ApiKey field to given value.

### HasApiKey

`func (o *SendVoiceMessageTtsByWxidRequest) HasApiKey() bool`

HasApiKey returns a boolean if a field has been set.

### SetApiKeyNil

`func (o *SendVoiceMessageTtsByWxidRequest) SetApiKeyNil(b bool)`

 SetApiKeyNil sets the value for ApiKey to be an explicit nil

### UnsetApiKey
`func (o *SendVoiceMessageTtsByWxidRequest) UnsetApiKey()`

UnsetApiKey ensures that no value is present for ApiKey, not even an explicit nil
### GetMessage

`func (o *SendVoiceMessageTtsByWxidRequest) GetMessage() string`

GetMessage returns the Message field if non-nil, zero value otherwise.

### GetMessageOk

`func (o *SendVoiceMessageTtsByWxidRequest) GetMessageOk() (*string, bool)`

GetMessageOk returns a tuple with the Message field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMessage

`func (o *SendVoiceMessageTtsByWxidRequest) SetMessage(v string)`

SetMessage sets Message field to given value.

### HasMessage

`func (o *SendVoiceMessageTtsByWxidRequest) HasMessage() bool`

HasMessage returns a boolean if a field has been set.

### SetMessageNil

`func (o *SendVoiceMessageTtsByWxidRequest) SetMessageNil(b bool)`

 SetMessageNil sets the value for Message to be an explicit nil

### UnsetMessage
`func (o *SendVoiceMessageTtsByWxidRequest) UnsetMessage()`

UnsetMessage ensures that no value is present for Message, not even an explicit nil
### GetOptions

`func (o *SendVoiceMessageTtsByWxidRequest) GetOptions() VoiceOptions`

GetOptions returns the Options field if non-nil, zero value otherwise.

### GetOptionsOk

`func (o *SendVoiceMessageTtsByWxidRequest) GetOptionsOk() (*VoiceOptions, bool)`

GetOptionsOk returns a tuple with the Options field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetOptions

`func (o *SendVoiceMessageTtsByWxidRequest) SetOptions(v VoiceOptions)`

SetOptions sets Options field to given value.

### HasOptions

`func (o *SendVoiceMessageTtsByWxidRequest) HasOptions() bool`

HasOptions returns a boolean if a field has been set.

### GetOptimizeWithLlm

`func (o *SendVoiceMessageTtsByWxidRequest) GetOptimizeWithLlm() bool`

GetOptimizeWithLlm returns the OptimizeWithLlm field if non-nil, zero value otherwise.

### GetOptimizeWithLlmOk

`func (o *SendVoiceMessageTtsByWxidRequest) GetOptimizeWithLlmOk() (*bool, bool)`

GetOptimizeWithLlmOk returns a tuple with the OptimizeWithLlm field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetOptimizeWithLlm

`func (o *SendVoiceMessageTtsByWxidRequest) SetOptimizeWithLlm(v bool)`

SetOptimizeWithLlm sets OptimizeWithLlm field to given value.

### HasOptimizeWithLlm

`func (o *SendVoiceMessageTtsByWxidRequest) HasOptimizeWithLlm() bool`

HasOptimizeWithLlm returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


