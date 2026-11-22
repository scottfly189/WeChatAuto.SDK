# SendFileByWxidRequest

按联系人 UserName 发送文件。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**user_name** | **str** |  | [optional] 
**files** | **List[str]** |  | [optional] 

## Example

```python
from wechatauto.models.send_file_by_wxid_request import SendFileByWxidRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SendFileByWxidRequest from a JSON string
send_file_by_wxid_request_instance = SendFileByWxidRequest.from_json(json)
# print the JSON string representation of the object
print(SendFileByWxidRequest.to_json())

# convert the object into a dict
send_file_by_wxid_request_dict = send_file_by_wxid_request_instance.to_dict()
# create an instance of SendFileByWxidRequest from a dict
send_file_by_wxid_request_from_dict = SendFileByWxidRequest.from_dict(send_file_by_wxid_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


