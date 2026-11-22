# PropertyItem

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | Pointer to **int32** |  | [optional] 
**Len** | Pointer to **int32** |  | [optional] 
**Type** | Pointer to **int32** |  | [optional] 
**Value** | Pointer to **NullableString** |  | [optional] 

## Methods

### NewPropertyItem

`func NewPropertyItem() *PropertyItem`

NewPropertyItem instantiates a new PropertyItem object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewPropertyItemWithDefaults

`func NewPropertyItemWithDefaults() *PropertyItem`

NewPropertyItemWithDefaults instantiates a new PropertyItem object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetId

`func (o *PropertyItem) GetId() int32`

GetId returns the Id field if non-nil, zero value otherwise.

### GetIdOk

`func (o *PropertyItem) GetIdOk() (*int32, bool)`

GetIdOk returns a tuple with the Id field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetId

`func (o *PropertyItem) SetId(v int32)`

SetId sets Id field to given value.

### HasId

`func (o *PropertyItem) HasId() bool`

HasId returns a boolean if a field has been set.

### GetLen

`func (o *PropertyItem) GetLen() int32`

GetLen returns the Len field if non-nil, zero value otherwise.

### GetLenOk

`func (o *PropertyItem) GetLenOk() (*int32, bool)`

GetLenOk returns a tuple with the Len field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetLen

`func (o *PropertyItem) SetLen(v int32)`

SetLen sets Len field to given value.

### HasLen

`func (o *PropertyItem) HasLen() bool`

HasLen returns a boolean if a field has been set.

### GetType

`func (o *PropertyItem) GetType() int32`

GetType returns the Type field if non-nil, zero value otherwise.

### GetTypeOk

`func (o *PropertyItem) GetTypeOk() (*int32, bool)`

GetTypeOk returns a tuple with the Type field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetType

`func (o *PropertyItem) SetType(v int32)`

SetType sets Type field to given value.

### HasType

`func (o *PropertyItem) HasType() bool`

HasType returns a boolean if a field has been set.

### GetValue

`func (o *PropertyItem) GetValue() string`

GetValue returns the Value field if non-nil, zero value otherwise.

### GetValueOk

`func (o *PropertyItem) GetValueOk() (*string, bool)`

GetValueOk returns a tuple with the Value field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetValue

`func (o *PropertyItem) SetValue(v string)`

SetValue sets Value field to given value.

### HasValue

`func (o *PropertyItem) HasValue() bool`

HasValue returns a boolean if a field has been set.

### SetValueNil

`func (o *PropertyItem) SetValueNil(b bool)`

 SetValueNil sets the value for Value to be an explicit nil

### UnsetValue
`func (o *PropertyItem) UnsetValue()`

UnsetValue ensures that no value is present for Value, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


