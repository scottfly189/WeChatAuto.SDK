
# WebChatAuto.SDK Web Support介绍

WeChatAuto.SDK 原本为一个 SDK 形式的微信自动化工具库。
当前版本在 SDK 基础之上，提供了一个 可视化 UI + REST API 服务，方便用户通过 HTTP 接口实现微信自动化操作。

启动后，系统会在本地启动一个 Web API 服务，开发者或其他程序可以通过发送 HTTP 请求，实现自动化控制微信客户端。

![图片](./websupport.png)


**源码:** [WechatAuto.SDK Web Support](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/WeChatAutoWebSupport/WeChatAutoSDK_WebSupport)

**直接下载:** [直接下载](https://github.com/scottfly189/WeChatAuto.SDK/releases/tag/1.2.8)
> 直接下载的已经包含.net10运行时，不需要安装.net环境

## 二、功能特性

当前版本已支持以下能力：

- ✅ 发送文本消息（好友 / 群）
- ✅ 发送图片消息
- ✅ 发送文件消息
- ✅ 基于 REST API 的自动化调用
- ✅ 本地 UI 控制（启动 / 停止服务 + 日志查看）

## 三、使用流程
## 步骤 1：登录微信

确保微信客户端已登录，并处于正常可操作状态。

### 步骤 2：启动服务

点击 UI 中的【运行】按钮：

系统将启动本地 Web API 服务

默认监听地址：

http://localhost:5000

### 步骤 3：调用 REST API

## 四、注意事宜
1. 请确保微信已经打开，并且已经被拉到状态栏，如下图所示:
![status](./status.png)
2. 发送速度为一条消息平均为5..12秒,已经考虑了风控被退出情况。

## 五、适用场景
- 自动客服机器人
- 消息批量发送工具
- 企业内部通知系统
- 自动化测试 / 演示工具
- 与其他系统集成（如 ERP / CRM）


