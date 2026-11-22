# SendVoiceMessageTtsRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**ApiKey** | Pointer to **NullableString** |  | [optional] 
**Message** | Pointer to **NullableString** |  | [optional] 
**Options** | Pointer to [**VoiceOptions**](VoiceOptions.md) |  | [optional] 
**OptimizeWithLlm** | Pointer to **bool** |  | [optional] 

## Methods

### NewSendVoiceMessageTtsRequest

`func NewSendVoiceMessageTtsRequest() *SendVoiceMessageTtsRequest`

NewSendVoiceMessageTtsRequest instantiates a new SendVoiceMessageTtsRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSendVoiceMessageTtsRequestWithDefaults

`func NewSendVoiceMessageTtsRequestWithDefaults() *SendVoiceMessageTtsRequest`

NewSendVoiceMessageTtsRequestWithDefaults instantiates a new SendVoiceMessageTtsRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *SendVoiceMessageTtsRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *SendVoiceMessageTtsRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *SendVoiceMessageTtsRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *SendVoiceMessageTtsRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *SendVoiceMessageTtsRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *SendVoiceMessageTtsRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetApiKey

`func (o *SendVoiceMessageTtsRequest) GetApiKey() string`

GetApiKey returns the ApiKey field if non-nil, zero value otherwise.

### GetApiKeyOk

`func (o *SendVoiceMessageTtsRequest) GetApiKeyOk() (*string, bool)`

GetApiKeyOk returns a tuple with the ApiKey field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetApiKey

`func (o *SendVoiceMessageTtsRequest) SetApiKey(v string)`

SetApiKey sets ApiKey field to given value.

### HasApiKey

`func (o *SendVoiceMessageTtsRequest) HasApiKey() bool`

HasApiKey returns a boolean if a field has been set.

### SetApiKeyNil

`func (o *SendVoiceMessageTtsRequest) SetApiKeyNil(b bool)`

 SetApiKeyNil sets the value for ApiKey to be an explicit nil

### UnsetApiKey
`func (o *SendVoiceMessageTtsRequest) UnsetApiKey()`

UnsetApiKey ensures that no value is present for ApiKey, not even an explicit nil
### GetMessage

`func (o *SendVoiceMessageTtsRequest) GetMessage() string`

GetMessage returns the Message field if non-nil, zero value otherwise.

### GetMessageOk

`func (o *SendVoiceMessageTtsRequest) GetMessageOk() (*string, bool)`

GetMessageOk returns a tuple with the Message field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMessage

`func (o *SendVoiceMessageTtsRequest) SetMessage(v string)`

SetMessage sets Message field to given value.

### HasMessage

`func (o *SendVoiceMessageTtsRequest) HasMessage() bool`

HasMessage returns a boolean if a field has been set.

### SetMessageNil

`func (o *SendVoiceMessageTtsRequest) SetMessageNil(b bool)`

 SetMessageNil sets the value for Message to be an explicit nil

### UnsetMessage
`func (o *SendVoiceMessageTtsRequest) UnsetMessage()`

UnsetMessage ensures that no value is present for Message, not even an explicit nil
### GetOptions

`func (o *SendVoiceMessageTtsRequest) GetOptions() VoiceOptions`

GetOptions returns the Options field if non-nil, zero value otherwise.

### GetOptionsOk

`func (o *SendVoiceMessageTtsRequest) GetOptionsOk() (*VoiceOptions, bool)`

GetOptionsOk returns a tuple with the Options field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetOptions

`func (o *SendVoiceMessageTtsRequest) SetOptions(v VoiceOptions)`

SetOptions sets Options field to given value.

### HasOptions

`func (o *SendVoiceMessageTtsRequest) HasOptions() bool`

HasOptions returns a boolean if a field has been set.

### GetOptimizeWithLlm

`func (o *SendVoiceMessageTtsRequest) GetOptimizeWithLlm() bool`

GetOptimizeWithLlm returns the OptimizeWithLlm field if non-nil, zero value otherwise.

### GetOptimizeWithLlmOk

`func (o *SendVoiceMessageTtsRequest) GetOptimizeWithLlmOk() (*bool, bool)`

GetOptimizeWithLlmOk returns a tuple with the OptimizeWithLlm field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetOptimizeWithLlm

`func (o *SendVoiceMessageTtsRequest) SetOptimizeWithLlm(v bool)`

SetOptimizeWithLlm sets OptimizeWithLlm field to given value.

### HasOptimizeWithLlm

`func (o *SendVoiceMessageTtsRequest) HasOptimizeWithLlm() bool`

HasOptimizeWithLlm returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


