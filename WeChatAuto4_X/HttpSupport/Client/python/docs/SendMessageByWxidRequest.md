# SendMessageByWxidRequest

按联系人 UserName（wxid / 群 id）发送文本。user_name 必填。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**user_name** | **str** |  | [optional] 
**message** | **str** |  | [optional] 
**at_users** | **List[str]** |  | [optional] 
**refer** | [**ChatRefer**](ChatRefer.md) |  | [optional] 

## Example

```python
from wechatauto.models.send_message_by_wxid_request import SendMessageByWxidRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SendMessageByWxidRequest from a JSON string
send_message_by_wxid_request_instance = SendMessageByWxidRequest.from_json(json)
# print the JSON string representation of the object
print(SendMessageByWxidRequest.to_json())

# convert the object into a dict
send_message_by_wxid_request_dict = send_message_by_wxid_request_instance.to_dict()
# create an instance of SendMessageByWxidRequest from a dict
send_message_by_wxid_request_from_dict = SendMessageByWxidRequest.from_dict(send_message_by_wxid_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


