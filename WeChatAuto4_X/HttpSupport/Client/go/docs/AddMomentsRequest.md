# AddMomentsRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ImageFiles** | Pointer to **[]string** |  | [optional] 
**Content** | Pointer to **NullableString** |  | [optional] 
**Options** | Pointer to [**MomentsOptions**](MomentsOptions.md) |  | [optional] 

## Methods

### NewAddMomentsRequest

`func NewAddMomentsRequest() *AddMomentsRequest`

NewAddMomentsRequest instantiates a new AddMomentsRequest object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewAddMomentsRequestWithDefaults

`func NewAddMomentsRequestWithDefaults() *AddMomentsRequest`

NewAddMomentsRequestWithDefaults instantiates a new AddMomentsRequest object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetImageFiles

`func (o *AddMomentsRequest) GetImageFiles() []string`

GetImageFiles returns the ImageFiles field if non-nil, zero value otherwise.

### GetImageFilesOk

`func (o *AddMomentsRequest) GetImageFilesOk() (*[]string, bool)`

GetImageFilesOk returns a tuple with the ImageFiles field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetImageFiles

`func (o *AddMomentsRequest) SetImageFiles(v []string)`

SetImageFiles sets ImageFiles field to given value.

### HasImageFiles

`func (o *AddMomentsRequest) HasImageFiles() bool`

HasImageFiles returns a boolean if a field has been set.

### SetImageFilesNil

`func (o *AddMomentsRequest) SetImageFilesNil(b bool)`

 SetImageFilesNil sets the value for ImageFiles to be an explicit nil

### UnsetImageFiles
`func (o *AddMomentsRequest) UnsetImageFiles()`

UnsetImageFiles ensures that no value is present for ImageFiles, not even an explicit nil
### GetContent

`func (o *AddMomentsRequest) GetContent() string`

GetContent returns the Content field if non-nil, zero value otherwise.

### GetContentOk

`func (o *AddMomentsRequest) GetContentOk() (*string, bool)`

GetContentOk returns a tuple with the Content field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetContent

`func (o *AddMomentsRequest) SetContent(v string)`

SetContent sets Content field to given value.

### HasContent

`func (o *AddMomentsRequest) HasContent() bool`

HasContent returns a boolean if a field has been set.

### SetContentNil

`func (o *AddMomentsRequest) SetContentNil(b bool)`

 SetContentNil sets the value for Content to be an explicit nil

### UnsetContent
`func (o *AddMomentsRequest) UnsetContent()`

UnsetContent ensures that no value is present for Content, not even an explicit nil
### GetOptions

`func (o *AddMomentsRequest) GetOptions() MomentsOptions`

GetOptions returns the Options field if non-nil, zero value otherwise.

### GetOptionsOk

`func (o *AddMomentsRequest) GetOptionsOk() (*MomentsOptions, bool)`

GetOptionsOk returns a tuple with the Options field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetOptions

`func (o *AddMomentsRequest) SetOptions(v MomentsOptions)`

SetOptions sets Options field to given value.

### HasOptions

`func (o *AddMomentsRequest) HasOptions() bool`

HasOptions returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


