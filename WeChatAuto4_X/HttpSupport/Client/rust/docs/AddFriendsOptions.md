# AddFriendsOptions

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**interval_time** | Option<**i32**> | 间隔时间,以秒为单位，默认为三秒，如果担心风控，可以把此时间设置长一点 | [optional]
**is_close_win** | Option<**bool**> | 是否关闭增加朋友窗口,默认关闭，可以设置为false不关闭 | [optional]
**say_hi** | Option<**String**> | 加好友时打招呼内容,如果为空，则保持微信默认 | [optional]
**suffix** | Option<**String**> | 备注后缀，如：设置suffix为\"test\",则备注为:xxxx_test,可以未来以备注的后缀来区分不同的好友分类 | [optional]
**label** | Option<**String**> | 设置标签 | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


