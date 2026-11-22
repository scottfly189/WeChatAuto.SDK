# wechatauto.MomentsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**moments_add**](MomentsApi.md#moments_add) | **POST** /api/v1/clients/{clientId}/moments/add | 发朋友圈（图文，多图需在同一目录）。
[**moments_close**](MomentsApi.md#moments_close) | **POST** /api/v1/clients/{clientId}/moments/close | 关闭朋友圈。
[**moments_open**](MomentsApi.md#moments_open) | **POST** /api/v1/clients/{clientId}/moments/open | 打开朋友圈（已打开则提到最前）。
[**moments_remove**](MomentsApi.md#moments_remove) | **POST** /api/v1/clients/{clientId}/moments/remove | 删除自己发的朋友圈（按文字内容）。


# **moments_add**
> moments_add(client_id, add_moments_request=add_moments_request)

发朋友圈（图文，多图需在同一目录）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.add_moments_request import AddMomentsRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.MomentsApi(api_client)
    client_id = 'client_id_example' # str | 
    add_moments_request = wechatauto.AddMomentsRequest() # AddMomentsRequest |  (optional)

    try:
        # 发朋友圈（图文，多图需在同一目录）。
        api_instance.moments_add(client_id, add_moments_request=add_moments_request)
    except Exception as e:
        print("Exception when calling MomentsApi->moments_add: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **add_moments_request** | [**AddMomentsRequest**](AddMomentsRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **moments_close**
> moments_close(client_id)

关闭朋友圈。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.MomentsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 关闭朋友圈。
        api_instance.moments_close(client_id)
    except Exception as e:
        print("Exception when calling MomentsApi->moments_close: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **moments_open**
> moments_open(client_id)

打开朋友圈（已打开则提到最前）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.MomentsApi(api_client)
    client_id = 'client_id_example' # str | 

    try:
        # 打开朋友圈（已打开则提到最前）。
        api_instance.moments_open(client_id)
    except Exception as e:
        print("Exception when calling MomentsApi->moments_open: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **moments_remove**
> moments_remove(client_id, remove_moments_request=remove_moments_request)

删除自己发的朋友圈（按文字内容）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.remove_moments_request import RemoveMomentsRequest
from wechatauto.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = wechatauto.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure API key authorization: ApiKey
configuration.api_key['ApiKey'] = os.environ["API_KEY"]

# Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
# configuration.api_key_prefix['ApiKey'] = 'Bearer'

# Enter a context with an instance of the API client
with wechatauto.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = wechatauto.MomentsApi(api_client)
    client_id = 'client_id_example' # str | 
    remove_moments_request = wechatauto.RemoveMomentsRequest() # RemoveMomentsRequest |  (optional)

    try:
        # 删除自己发的朋友圈（按文字内容）。
        api_instance.moments_remove(client_id, remove_moments_request=remove_moments_request)
    except Exception as e:
        print("Exception when calling MomentsApi->moments_remove: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **remove_moments_request** | [**RemoveMomentsRequest**](RemoveMomentsRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[ApiKey](../README.md#ApiKey)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined

### HTTP response details

| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

