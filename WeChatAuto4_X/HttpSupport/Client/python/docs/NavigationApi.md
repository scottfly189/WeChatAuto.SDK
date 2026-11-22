# wechatauto.NavigationApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**navigation_close**](NavigationApi.md#navigation_close) | **POST** /api/v1/clients/{clientId}/navigation/close | 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。
[**navigation_switch**](NavigationApi.md#navigation_switch) | **POST** /api/v1/clients/{clientId}/navigation/switch | 切换导航栏。


# **navigation_close**
> navigation_close(client_id, switch_navigation_request=switch_navigation_request)

关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.switch_navigation_request import SwitchNavigationRequest
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
    api_instance = wechatauto.NavigationApi(api_client)
    client_id = 'client_id_example' # str | 
    switch_navigation_request = wechatauto.SwitchNavigationRequest() # SwitchNavigationRequest |  (optional)

    try:
        # 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。
        api_instance.navigation_close(client_id, switch_navigation_request=switch_navigation_request)
    except Exception as e:
        print("Exception when calling NavigationApi->navigation_close: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **switch_navigation_request** | [**SwitchNavigationRequest**](SwitchNavigationRequest.md)|  | [optional] 

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

# **navigation_switch**
> navigation_switch(client_id, switch_navigation_request=switch_navigation_request)

切换导航栏。

### Example

* Api Key Authentication (ApiKey):

```python
import wechatauto
from wechatauto.models.switch_navigation_request import SwitchNavigationRequest
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
    api_instance = wechatauto.NavigationApi(api_client)
    client_id = 'client_id_example' # str | 
    switch_navigation_request = wechatauto.SwitchNavigationRequest() # SwitchNavigationRequest |  (optional)

    try:
        # 切换导航栏。
        api_instance.navigation_switch(client_id, switch_navigation_request=switch_navigation_request)
    except Exception as e:
        print("Exception when calling NavigationApi->navigation_switch: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client_id** | **str**|  | 
 **switch_navigation_request** | [**SwitchNavigationRequest**](SwitchNavigationRequest.md)|  | [optional] 

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

