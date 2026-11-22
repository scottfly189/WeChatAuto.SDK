# SearchConversationRequest

搜索会话。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**who** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.search_conversation_request import SearchConversationRequest

# TODO update the JSON string below
json = "{}"
# create an instance of SearchConversationRequest from a JSON string
search_conversation_request_instance = SearchConversationRequest.from_json(json)
# print the JSON string representation of the object
print(SearchConversationRequest.to_json())

# convert the object into a dict
search_conversation_request_dict = search_conversation_request_instance.to_dict()
# create an instance of SearchConversationRequest from a dict
search_conversation_request_from_dict = SearchConversationRequest.from_dict(search_conversation_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


