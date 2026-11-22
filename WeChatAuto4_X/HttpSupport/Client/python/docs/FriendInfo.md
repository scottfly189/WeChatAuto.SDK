# FriendInfo

个人信息

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**nick_name** | **str** | 昵称 | [optional] 
**memo_name** | **str** | 备注名 | [optional] 
**area** | **str** | 地区,建议仅供参考 | [optional] 
**lable** | **List[str]** | 标签 | [optional] 
**same_group_number** | **str** | 共同群数量 | [optional] 
**signature** | **str** | 个性签名 | [optional] 
**source** | **str** | 来源 | [optional] 
**wx_id** | **str** | 微信ID | [optional] 
**avatar_path** | **str** | 头像路径 | [optional] 
**avatar_image** | [**Image**](Image.md) |  | [optional] 
**avatar_image_base64** | **str** |  | [optional] 
**friend_search_result** | [**FriendSearchResultEnums**](FriendSearchResultEnums.md) |  | [optional] 
**chat_type** | [**ChatType**](ChatType.md) |  | [optional] 
**add_date_time** | **str** | 添加好友时间，&gt;4.x版本才有此属性 | [optional] 
**name** | **str** | 微信中使用的名称  本质是: 如果昵称不等于备注名，则以备注名不为主. | [optional] [readonly] 

## Example

```python
from wechatauto.models.friend_info import FriendInfo

# TODO update the JSON string below
json = "{}"
# create an instance of FriendInfo from a JSON string
friend_info_instance = FriendInfo.from_json(json)
# print the JSON string representation of the object
print(FriendInfo.to_json())

# convert the object into a dict
friend_info_dict = friend_info_instance.to_dict()
# create an instance of FriendInfo from a dict
friend_info_from_dict = FriendInfo.from_dict(friend_info_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


