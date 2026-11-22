# FriendInfo

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**NickName** | Pointer to **NullableString** | 昵称 | [optional] 
**MemoName** | Pointer to **NullableString** | 备注名 | [optional] 
**Area** | Pointer to **NullableString** | 地区,建议仅供参考 | [optional] 
**Lable** | Pointer to **[]string** | 标签 | [optional] 
**SameGroupNumber** | Pointer to **NullableString** | 共同群数量 | [optional] 
**Signature** | Pointer to **NullableString** | 个性签名 | [optional] 
**Source** | Pointer to **NullableString** | 来源 | [optional] 
**WxId** | Pointer to **NullableString** | 微信ID | [optional] 
**AvatarPath** | Pointer to **NullableString** | 头像路径 | [optional] 
**AvatarImage** | Pointer to [**Image**](Image.md) |  | [optional] 
**AvatarImageBase64** | Pointer to **NullableString** |  | [optional] 
**FriendSearchResult** | Pointer to [**FriendSearchResultEnums**](FriendSearchResultEnums.md) |  | [optional] 
**ChatType** | Pointer to [**ChatType**](ChatType.md) |  | [optional] 
**AddDateTime** | Pointer to **NullableString** | 添加好友时间，&gt;4.x版本才有此属性 | [optional] 
**Name** | Pointer to **NullableString** | 微信中使用的名称  本质是: 如果昵称不等于备注名，则以备注名不为主. | [optional] [readonly] 

## Methods

### NewFriendInfo

`func NewFriendInfo() *FriendInfo`

NewFriendInfo instantiates a new FriendInfo object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewFriendInfoWithDefaults

`func NewFriendInfoWithDefaults() *FriendInfo`

NewFriendInfoWithDefaults instantiates a new FriendInfo object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetNickName

`func (o *FriendInfo) GetNickName() string`

GetNickName returns the NickName field if non-nil, zero value otherwise.

### GetNickNameOk

`func (o *FriendInfo) GetNickNameOk() (*string, bool)`

GetNickNameOk returns a tuple with the NickName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetNickName

`func (o *FriendInfo) SetNickName(v string)`

SetNickName sets NickName field to given value.

### HasNickName

`func (o *FriendInfo) HasNickName() bool`

HasNickName returns a boolean if a field has been set.

### SetNickNameNil

`func (o *FriendInfo) SetNickNameNil(b bool)`

 SetNickNameNil sets the value for NickName to be an explicit nil

### UnsetNickName
`func (o *FriendInfo) UnsetNickName()`

UnsetNickName ensures that no value is present for NickName, not even an explicit nil
### GetMemoName

`func (o *FriendInfo) GetMemoName() string`

GetMemoName returns the MemoName field if non-nil, zero value otherwise.

### GetMemoNameOk

`func (o *FriendInfo) GetMemoNameOk() (*string, bool)`

GetMemoNameOk returns a tuple with the MemoName field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetMemoName

`func (o *FriendInfo) SetMemoName(v string)`

SetMemoName sets MemoName field to given value.

### HasMemoName

`func (o *FriendInfo) HasMemoName() bool`

HasMemoName returns a boolean if a field has been set.

### SetMemoNameNil

`func (o *FriendInfo) SetMemoNameNil(b bool)`

 SetMemoNameNil sets the value for MemoName to be an explicit nil

### UnsetMemoName
`func (o *FriendInfo) UnsetMemoName()`

UnsetMemoName ensures that no value is present for MemoName, not even an explicit nil
### GetArea

`func (o *FriendInfo) GetArea() string`

GetArea returns the Area field if non-nil, zero value otherwise.

### GetAreaOk

`func (o *FriendInfo) GetAreaOk() (*string, bool)`

GetAreaOk returns a tuple with the Area field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetArea

`func (o *FriendInfo) SetArea(v string)`

SetArea sets Area field to given value.

### HasArea

`func (o *FriendInfo) HasArea() bool`

HasArea returns a boolean if a field has been set.

### SetAreaNil

`func (o *FriendInfo) SetAreaNil(b bool)`

 SetAreaNil sets the value for Area to be an explicit nil

### UnsetArea
`func (o *FriendInfo) UnsetArea()`

UnsetArea ensures that no value is present for Area, not even an explicit nil
### GetLable

`func (o *FriendInfo) GetLable() []string`

GetLable returns the Lable field if non-nil, zero value otherwise.

### GetLableOk

`func (o *FriendInfo) GetLableOk() (*[]string, bool)`

GetLableOk returns a tuple with the Lable field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetLable

`func (o *FriendInfo) SetLable(v []string)`

SetLable sets Lable field to given value.

### HasLable

`func (o *FriendInfo) HasLable() bool`

HasLable returns a boolean if a field has been set.

### SetLableNil

`func (o *FriendInfo) SetLableNil(b bool)`

 SetLableNil sets the value for Lable to be an explicit nil

### UnsetLable
`func (o *FriendInfo) UnsetLable()`

UnsetLable ensures that no value is present for Lable, not even an explicit nil
### GetSameGroupNumber

`func (o *FriendInfo) GetSameGroupNumber() string`

GetSameGroupNumber returns the SameGroupNumber field if non-nil, zero value otherwise.

### GetSameGroupNumberOk

`func (o *FriendInfo) GetSameGroupNumberOk() (*string, bool)`

GetSameGroupNumberOk returns a tuple with the SameGroupNumber field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSameGroupNumber

`func (o *FriendInfo) SetSameGroupNumber(v string)`

SetSameGroupNumber sets SameGroupNumber field to given value.

### HasSameGroupNumber

`func (o *FriendInfo) HasSameGroupNumber() bool`

HasSameGroupNumber returns a boolean if a field has been set.

### SetSameGroupNumberNil

`func (o *FriendInfo) SetSameGroupNumberNil(b bool)`

 SetSameGroupNumberNil sets the value for SameGroupNumber to be an explicit nil

### UnsetSameGroupNumber
`func (o *FriendInfo) UnsetSameGroupNumber()`

UnsetSameGroupNumber ensures that no value is present for SameGroupNumber, not even an explicit nil
### GetSignature

`func (o *FriendInfo) GetSignature() string`

GetSignature returns the Signature field if non-nil, zero value otherwise.

### GetSignatureOk

`func (o *FriendInfo) GetSignatureOk() (*string, bool)`

GetSignatureOk returns a tuple with the Signature field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSignature

`func (o *FriendInfo) SetSignature(v string)`

SetSignature sets Signature field to given value.

### HasSignature

`func (o *FriendInfo) HasSignature() bool`

HasSignature returns a boolean if a field has been set.

### SetSignatureNil

`func (o *FriendInfo) SetSignatureNil(b bool)`

 SetSignatureNil sets the value for Signature to be an explicit nil

### UnsetSignature
`func (o *FriendInfo) UnsetSignature()`

UnsetSignature ensures that no value is present for Signature, not even an explicit nil
### GetSource

`func (o *FriendInfo) GetSource() string`

GetSource returns the Source field if non-nil, zero value otherwise.

### GetSourceOk

`func (o *FriendInfo) GetSourceOk() (*string, bool)`

GetSourceOk returns a tuple with the Source field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSource

`func (o *FriendInfo) SetSource(v string)`

SetSource sets Source field to given value.

### HasSource

`func (o *FriendInfo) HasSource() bool`

HasSource returns a boolean if a field has been set.

### SetSourceNil

`func (o *FriendInfo) SetSourceNil(b bool)`

 SetSourceNil sets the value for Source to be an explicit nil

### UnsetSource
`func (o *FriendInfo) UnsetSource()`

UnsetSource ensures that no value is present for Source, not even an explicit nil
### GetWxId

`func (o *FriendInfo) GetWxId() string`

GetWxId returns the WxId field if non-nil, zero value otherwise.

### GetWxIdOk

`func (o *FriendInfo) GetWxIdOk() (*string, bool)`

GetWxIdOk returns a tuple with the WxId field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetWxId

`func (o *FriendInfo) SetWxId(v string)`

SetWxId sets WxId field to given value.

### HasWxId

`func (o *FriendInfo) HasWxId() bool`

HasWxId returns a boolean if a field has been set.

### SetWxIdNil

`func (o *FriendInfo) SetWxIdNil(b bool)`

 SetWxIdNil sets the value for WxId to be an explicit nil

### UnsetWxId
`func (o *FriendInfo) UnsetWxId()`

UnsetWxId ensures that no value is present for WxId, not even an explicit nil
### GetAvatarPath

`func (o *FriendInfo) GetAvatarPath() string`

GetAvatarPath returns the AvatarPath field if non-nil, zero value otherwise.

### GetAvatarPathOk

`func (o *FriendInfo) GetAvatarPathOk() (*string, bool)`

GetAvatarPathOk returns a tuple with the AvatarPath field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetAvatarPath

`func (o *FriendInfo) SetAvatarPath(v string)`

SetAvatarPath sets AvatarPath field to given value.

### HasAvatarPath

`func (o *FriendInfo) HasAvatarPath() bool`

HasAvatarPath returns a boolean if a field has been set.

### SetAvatarPathNil

`func (o *FriendInfo) SetAvatarPathNil(b bool)`

 SetAvatarPathNil sets the value for AvatarPath to be an explicit nil

### UnsetAvatarPath
`func (o *FriendInfo) UnsetAvatarPath()`

UnsetAvatarPath ensures that no value is present for AvatarPath, not even an explicit nil
### GetAvatarImage

`func (o *FriendInfo) GetAvatarImage() Image`

GetAvatarImage returns the AvatarImage field if non-nil, zero value otherwise.

### GetAvatarImageOk

`func (o *FriendInfo) GetAvatarImageOk() (*Image, bool)`

GetAvatarImageOk returns a tuple with the AvatarImage field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetAvatarImage

`func (o *FriendInfo) SetAvatarImage(v Image)`

SetAvatarImage sets AvatarImage field to given value.

### HasAvatarImage

`func (o *FriendInfo) HasAvatarImage() bool`

HasAvatarImage returns a boolean if a field has been set.

### GetAvatarImageBase64

`func (o *FriendInfo) GetAvatarImageBase64() string`

GetAvatarImageBase64 returns the AvatarImageBase64 field if non-nil, zero value otherwise.

### GetAvatarImageBase64Ok

`func (o *FriendInfo) GetAvatarImageBase64Ok() (*string, bool)`

GetAvatarImageBase64Ok returns a tuple with the AvatarImageBase64 field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetAvatarImageBase64

`func (o *FriendInfo) SetAvatarImageBase64(v string)`

SetAvatarImageBase64 sets AvatarImageBase64 field to given value.

### HasAvatarImageBase64

`func (o *FriendInfo) HasAvatarImageBase64() bool`

HasAvatarImageBase64 returns a boolean if a field has been set.

### SetAvatarImageBase64Nil

`func (o *FriendInfo) SetAvatarImageBase64Nil(b bool)`

 SetAvatarImageBase64Nil sets the value for AvatarImageBase64 to be an explicit nil

### UnsetAvatarImageBase64
`func (o *FriendInfo) UnsetAvatarImageBase64()`

UnsetAvatarImageBase64 ensures that no value is present for AvatarImageBase64, not even an explicit nil
### GetFriendSearchResult

`func (o *FriendInfo) GetFriendSearchResult() FriendSearchResultEnums`

GetFriendSearchResult returns the FriendSearchResult field if non-nil, zero value otherwise.

### GetFriendSearchResultOk

`func (o *FriendInfo) GetFriendSearchResultOk() (*FriendSearchResultEnums, bool)`

GetFriendSearchResultOk returns a tuple with the FriendSearchResult field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetFriendSearchResult

`func (o *FriendInfo) SetFriendSearchResult(v FriendSearchResultEnums)`

SetFriendSearchResult sets FriendSearchResult field to given value.

### HasFriendSearchResult

`func (o *FriendInfo) HasFriendSearchResult() bool`

HasFriendSearchResult returns a boolean if a field has been set.

### GetChatType

`func (o *FriendInfo) GetChatType() ChatType`

GetChatType returns the ChatType field if non-nil, zero value otherwise.

### GetChatTypeOk

`func (o *FriendInfo) GetChatTypeOk() (*ChatType, bool)`

GetChatTypeOk returns a tuple with the ChatType field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetChatType

`func (o *FriendInfo) SetChatType(v ChatType)`

SetChatType sets ChatType field to given value.

### HasChatType

`func (o *FriendInfo) HasChatType() bool`

HasChatType returns a boolean if a field has been set.

### GetAddDateTime

`func (o *FriendInfo) GetAddDateTime() string`

GetAddDateTime returns the AddDateTime field if non-nil, zero value otherwise.

### GetAddDateTimeOk

`func (o *FriendInfo) GetAddDateTimeOk() (*string, bool)`

GetAddDateTimeOk returns a tuple with the AddDateTime field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetAddDateTime

`func (o *FriendInfo) SetAddDateTime(v string)`

SetAddDateTime sets AddDateTime field to given value.

### HasAddDateTime

`func (o *FriendInfo) HasAddDateTime() bool`

HasAddDateTime returns a boolean if a field has been set.

### SetAddDateTimeNil

`func (o *FriendInfo) SetAddDateTimeNil(b bool)`

 SetAddDateTimeNil sets the value for AddDateTime to be an explicit nil

### UnsetAddDateTime
`func (o *FriendInfo) UnsetAddDateTime()`

UnsetAddDateTime ensures that no value is present for AddDateTime, not even an explicit nil
### GetName

`func (o *FriendInfo) GetName() string`

GetName returns the Name field if non-nil, zero value otherwise.

### GetNameOk

`func (o *FriendInfo) GetNameOk() (*string, bool)`

GetNameOk returns a tuple with the Name field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetName

`func (o *FriendInfo) SetName(v string)`

SetName sets Name field to given value.

### HasName

`func (o *FriendInfo) HasName() bool`

HasName returns a boolean if a field has been set.

### SetNameNil

`func (o *FriendInfo) SetNameNil(b bool)`

 SetNameNil sets the value for Name to be an explicit nil

### UnsetName
`func (o *FriendInfo) UnsetName()`

UnsetName ensures that no value is present for Name, not even an explicit nil

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


