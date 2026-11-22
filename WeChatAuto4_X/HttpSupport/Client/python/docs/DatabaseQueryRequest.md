# DatabaseQueryRequest

数据库只读查询。仅支持 SELECT（含 WITH ... SELECT），不会自动追加当前微信号条件。

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**sql** | **str** |  | [optional] 

## Example

```python
from wechatauto.models.database_query_request import DatabaseQueryRequest

# TODO update the JSON string below
json = "{}"
# create an instance of DatabaseQueryRequest from a JSON string
database_query_request_instance = DatabaseQueryRequest.from_json(json)
# print the JSON string representation of the object
print(DatabaseQueryRequest.to_json())

# convert the object into a dict
database_query_request_dict = database_query_request_instance.to_dict()
# create an instance of DatabaseQueryRequest from a dict
database_query_request_from_dict = DatabaseQueryRequest.from_dict(database_query_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


