# WhoRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 

## Methods

### NewWhoRequest

`func NewWhoRequest() *WhoRequest`

NewWhoRequest instantiates a new WhoRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewWhoRequestWithDefaults

`func NewWhoRequestWithDefaults() *WhoRequest`

NewWhoRequestWithDefaults instantiates a new WhoRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *WhoRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *WhoRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *WhoRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *WhoRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *WhoRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *WhoRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


