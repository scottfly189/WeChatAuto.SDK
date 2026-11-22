# DatabaseQueryRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Sql** | Pointer to **NullableString** |  | [optional] 

## Methods

### NewDatabaseQueryRequest

`func NewDatabaseQueryRequest() *DatabaseQueryRequest`

NewDatabaseQueryRequest instantiates a new DatabaseQueryRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewDatabaseQueryRequestWithDefaults

`func NewDatabaseQueryRequestWithDefaults() *DatabaseQueryRequest`

NewDatabaseQueryRequestWithDefaults instantiates a new DatabaseQueryRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetSql

`func (o *DatabaseQueryRequest) GetSql() string`

GetSql returns the Sql field if non-nil, zero value otherwise.

### GetSqlOk

`func (o *DatabaseQueryRequest) GetSqlOk() (*string, bool)`

GetSqlOk returns a tuple with the Sql field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSql

`func (o *DatabaseQueryRequest) SetSql(v string)`

SetSql sets Sql field to given value.

### HasSql

`func (o *DatabaseQueryRequest) HasSql() bool`

HasSql returns a boolean if a field has been set.

### SetSqlNil

`func (o *DatabaseQueryRequest) SetSqlNil(b bool)`

 SetSqlNil sets the value for Sql to be an explicit nil

### UnsetSql
`func (o *DatabaseQueryRequest) UnsetSql()`

UnsetSql ensures that no value is present for Sql, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


