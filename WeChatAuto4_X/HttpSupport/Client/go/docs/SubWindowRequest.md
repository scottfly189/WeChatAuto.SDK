# SubWindowRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 

## Methods

### NewSubWindowRequest

`func NewSubWindowRequest() *SubWindowRequest`

NewSubWindowRequest instantiates a new SubWindowRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewSubWindowRequestWithDefaults

`func NewSubWindowRequestWithDefaults() *SubWindowRequest`

NewSubWindowRequestWithDefaults instantiates a new SubWindowRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *SubWindowRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *SubWindowRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *SubWindowRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *SubWindowRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *SubWindowRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *SubWindowRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


