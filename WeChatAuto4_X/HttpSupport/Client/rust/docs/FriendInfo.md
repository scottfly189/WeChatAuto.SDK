# FriendInfo

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**nick_name** | Option<**String**> | 昵称 | [optional]
**memo_name** | Option<**String**> | 备注名 | [optional]
**area** | Option<**String**> | 地区,建议仅供参考 | [optional]
**lable** | Option<**Vec<String>**> | 标签 | [optional]
**same_group_number** | Option<**String**> | 共同群数量 | [optional]
**signature** | Option<**String**> | 个性签名 | [optional]
**source** | Option<**String**> | 来源 | [optional]
**wx_id** | Option<**String**> | 微信ID | [optional]
**avatar_path** | Option<**String**> | 头像路径 | [optional]
**avatar_image** | Option<[**models::Image**](Image.md)> |  | [optional]
**avatar_image_base64** | Option<**String**> |  | [optional]
**friend_search_result** | Option<[**models::FriendSearchResultEnums**](FriendSearchResultEnums.md)> |  | [optional]
**chat_type** | Option<[**models::ChatType**](ChatType.md)> |  | [optional]
**add_date_time** | Option<**String**> | 添加好友时间，>4.x版本才有此属性 | [optional]
**name** | Option<**String**> | 微信中使用的名称  本质是: 如果昵称不等于备注名，则以备注名不为主. | [optional][readonly]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


