# SendVoiceChatsRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**Partner** | Pointer to **[]string** |  | [optional] 

## Methods

### NewSendVoiceChatsRequest

`func NewSendVoiceChatsRequest() *SendVoiceChatsRequest`

NewSendVoiceChatsRequest instantiates a new SendVoiceChatsRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSendVoiceChatsRequestWithDefaults

`func NewSendVoiceChatsRequestWithDefaults() *SendVoiceChatsRequest`

NewSendVoiceChatsRequestWithDefaults instantiates a new SendVoiceChatsRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *SendVoiceChatsRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *SendVoiceChatsRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *SendVoiceChatsRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *SendVoiceChatsRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *SendVoiceChatsRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *SendVoiceChatsRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetPartner

`func (o *SendVoiceChatsRequest) GetPartner() []string`

GetPartner returns the Partner field if non-nil, zero value otherwise.

### GetPartnerOk

`func (o *SendVoiceChatsRequest) GetPartnerOk() (*[]string, bool)`

GetPartnerOk returns a tuple with the Partner field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetPartner

`func (o *SendVoiceChatsRequest) SetPartner(v []string)`

SetPartner sets Partner field to given value.

### HasPartner

`func (o *SendVoiceChatsRequest) HasPartner() bool`

HasPartner returns a boolean if a field has been set.

### SetPartnerNil

`func (o *SendVoiceChatsRequest) SetPartnerNil(b bool)`

 SetPartnerNil sets the value for Partner to be an explicit nil

### UnsetPartner
`func (o *SendVoiceChatsRequest) UnsetPartner()`

UnsetPartner ensures that no value is present for Partner, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


