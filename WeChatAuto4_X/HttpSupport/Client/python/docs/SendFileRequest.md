# SendFileRequest

发送文件。who 为空时发送给当前焦点聊天窗口。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 
**files** | **List[str]** |  | [optional] 

## Example

```python
from wechatauto.models.send_file_request import SendFileRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SendFileRequest from a JSON string
send_file_request_instance = SendFileRequest.from_json(json)
# print the JSON string representation of the object
print(SendFileRequest.to_json())

# convert the object into a dict
send_file_request_dict = send_file_request_instance.to_dict()
# create an instance of SendFileRequest from a dict
send_file_request_from_dict = SendFileRequest.from_dict(send_file_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


