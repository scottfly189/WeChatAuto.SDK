# wechatauto-client.FriendInfo

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**nickName** | **String** | 昵称 | [optional] 
**memoName** | **String** | 备注名 | [optional] 
**area** | **String** | 地区,建议仅供参考 | [optional] 
**lable** | **[String]** | 标签 | [optional] 
**sameGroupNumber** | **String** | 共同群数量 | [optional] 
**signature** | **String** | 个性签名 | [optional] 
**source** | **String** | 来源 | [optional] 
**wxId** | **String** | 微信ID | [optional] 
**avatarPath** | **String** | 头像路径 | [optional] 
**avatarImage** | [**Image**](Image.md) |  | [optional] 
**avatarImageBase64** | **String** |  | [optional] 
**friendSearchResult** | [**FriendSearchResultEnums**](FriendSearchResultEnums.md) |  | [optional] 
**chatType** | [**ChatType**](ChatType.md) |  | [optional] 
**addDateTime** | **String** | 添加好友时间，&gt;4.x版本才有此属性 | [optional] 
**name** | **String** | 微信中使用的名称  本质是: 如果昵称不等于备注名，则以备注名不为主. | [optional] [readonly] 


