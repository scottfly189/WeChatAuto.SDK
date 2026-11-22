# ForwardMultipleMessageRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Who** | Pointer to **NullableString** |  | [optional] 
**To** | Pointer to **[]string** |  | [optional] 
**FType** | Pointer to [**ForwardMessageTypeEnums**](ForwardMessageTypeEnums.md) |  | [optional] 
**RowCount** | Pointer to **int32** |  | [optional] 

## Methods

### NewForwardMultipleMessageRequest

`func NewForwardMultipleMessageRequest() *ForwardMultipleMessageRequest`

NewForwardMultipleMessageRequest instantiates a new ForwardMultipleMessageRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewForwardMultipleMessageRequestWithDefaults

`func NewForwardMultipleMessageRequestWithDefaults() *ForwardMultipleMessageRequest`

NewForwardMultipleMessageRequestWithDefaults instantiates a new ForwardMultipleMessageRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetWho

`func (o *ForwardMultipleMessageRequest) GetWho() string`

GetWho returns the Who field if non-nil, zero value otherwise.

### GetWhoOk

`func (o *ForwardMultipleMessageRequest) GetWhoOk() (*string, bool)`

GetWhoOk returns a tuple with the Who field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWho

`func (o *ForwardMultipleMessageRequest) SetWho(v string)`

SetWho sets Who field to given value.

### HasWho

`func (o *ForwardMultipleMessageRequest) HasWho() bool`

HasWho returns a boolean if a field has been set.

### SetWhoNil

`func (o *ForwardMultipleMessageRequest) SetWhoNil(b bool)`

 SetWhoNil sets the value for Who to be an explicit nil

### UnsetWho
`func (o *ForwardMultipleMessageRequest) UnsetWho()`

UnsetWho ensures that no value is present for Who, not even an explicit nil
### GetTo

`func (o *ForwardMultipleMessageRequest) GetTo() []string`

GetTo returns the To field if non-nil, zero value otherwise.

### GetToOk

`func (o *ForwardMultipleMessageRequest) GetToOk() (*[]string, bool)`

GetToOk returns a tuple with the To field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetTo

`func (o *ForwardMultipleMessageRequest) SetTo(v []string)`

SetTo sets To field to given value.

### HasTo

`func (o *ForwardMultipleMessageRequest) HasTo() bool`

HasTo returns a boolean if a field has been set.

### SetToNil

`func (o *ForwardMultipleMessageRequest) SetToNil(b bool)`

 SetToNil sets the value for To to be an explicit nil

### UnsetTo
`func (o *ForwardMultipleMessageRequest) UnsetTo()`

UnsetTo ensures that no value is present for To, not even an explicit nil
### GetFType

`func (o *ForwardMultipleMessageRequest) GetFType() ForwardMessageTypeEnums`

GetFType returns the FType field if non-nil, zero value otherwise.

### GetFTypeOk

`func (o *ForwardMultipleMessageRequest) GetFTypeOk() (*ForwardMessageTypeEnums, bool)`

GetFTypeOk returns a tuple with the FType field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetFType

`func (o *ForwardMultipleMessageRequest) SetFType(v ForwardMessageTypeEnums)`

SetFType sets FType field to given value.

### HasFType

`func (o *ForwardMultipleMessageRequest) HasFType() bool`

HasFType returns a boolean if a field has been set.

### GetRowCount

`func (o *ForwardMultipleMessageRequest) GetRowCount() int32`

GetRowCount returns the RowCount field if non-nil, zero value otherwise.

### GetRowCountOk

`func (o *ForwardMultipleMessageRequest) GetRowCountOk() (*int32, bool)`

GetRowCountOk returns a tuple with the RowCount field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetRowCount

`func (o *ForwardMultipleMessageRequest) SetRowCount(v int32)`

SetRowCount sets RowCount field to given value.

### HasRowCount

`func (o *ForwardMultipleMessageRequest) HasRowCount() bool`

HasRowCount returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


