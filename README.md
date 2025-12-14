# WECHATAUTO.SDK - 而向AI的现代化微信自动化框架

[![.NET](https://img.shields.io/badge/.NET-4.8%20%7C%206.0%2B-blue)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

WeChatAuto.SDK 是一款面向 AI 的微信 PC 客户端自动化 SDK，基于 .NET 与 UI 自动化技术开发。它支持消息收发、转发、群聊与好友管理、朋友圈操作等多种功能，并专为集成人工智能（如 LLM 上下文交互）场景设计。SDK 提供丰富直观的 API，支持 .NET 现代化特性，比如依赖注入，让你轻松将自定义对象集成进自动化流程。

## ✨ 特性

- 💬 **消息操作** - 发送文字、表情、文件，支持 @ 提醒,转发消息等
- 👥 **群聊管理** - 创建群聊、添加/移除成员、更新群公告等
- 📱 **朋友圈操作** - 点赞、评论、监听朋友圈动态
- 📋 **通讯录管理** - 自动添加好友、管理联系人、处理新好友请求
- 👂 **事件监听** - 消息监听(提供LLM上下文)、朋友圈监听、新好友监听等
- 🛡️ **风控保护** - 支持键鼠模拟器，降低被风控风险
- 🔧 **易于集成** - 支持依赖注入，可轻松集成到现有项目
- 🚀 **多微信实例支持** - 同时管理多个微信客户端实例
- 😊 **AI 友好集成** - 原生支持 LLM 上下文对接并提供 MCP Server，便于对接主流智能体与平台（如 MEAI、SK、MAF），助力智能应用高效闭环与创新集成

## 📋 系统要求

- Windows 操作系统
- .NET Framework 4.8+ 或 .NET 6.0+ (Windows)，支持.NET的框架有:net48;net481;net6.0-windows; net7.0-windows;net8.0-windows;net9.0-windows;net10.0-windows;
- 微信 PC 客户端已安装并运行

## 🚀 快速开始

### 安装

通过 NuGet 安装：

```bash
dotnet add package WeChatAuto.SDK
```

### 基本使用

#### 方式一：使用内部依赖注入（适用于独立应用）

```csharp
using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;

// 初始化 SDK
var serviceProvider = WeAutomation.Initialize(options =>
{
    options.DebugMode = true;
    options.EnableMouseKeyboardSimulator = false;
});

// 获取微信客户端工厂
var factory = serviceProvider.GetRequiredService<WeChatClientFactory>();

// 获取所有微信客户端
var clients = factory.WxClientList;

// 获取第一个微信客户端
var client = clients.Values.First();

// 发送消息
await client.SendWho("好友名称", "Hello, World!");
```

#### 方式二：使用外部依赖注入（适用于已有 DI 框架的应用）

```csharp
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Services;
using WeChatAuto.Components;

var services = new ServiceCollection();

// 初始化 SDK
WeAutomation.Initialize(services, options =>
{
    options.DebugMode = true;
    options.EnableMouseKeyboardSimulator = false;
});

var serviceProvider = services.BuildServiceProvider();
var factory = serviceProvider.GetRequiredService<WeChatClientFactory>();

// 使用微信客户端
var client = factory.GetWeChatClient("微信昵称");
await client.SendWho("好友名称", "Hello, World!");
```

## 📖 功能示例

### 发送消息

```csharp
// 发送给单个好友
await client.SendWho("好友名称", "消息内容");

// 批量发送消息
await client.SendWhos(new[] { "好友1", "好友2" }, "消息内容");

// 群聊中 @ 用户
await client.SendWho("群聊名称", "消息内容", "被@的用户");

// 群聊中 @ 多个用户
await client.SendWho("群聊名称", "消息内容", new[] { "用户1", "用户2" });

// 发送表情
await client.SendEmoji("好友名称", "微笑");

// 发送文件
await client.SendFile("好友名称", "文件路径");
```

### 群聊管理

```csharp
// 获取群成员列表
var members = await client.GetChatGroupMemberList("群聊名称");

// 创建群聊
var result = client.CreateOrUpdateOwnerChatGroup("群聊名称", new[] { "成员1", "成员2" });

// 添加群成员
await client.AddOwnerChatGroupMember("群聊名称", "新成员");

// 移除群成员
await client.RemoveOwnerChatGroupMember("群聊名称", "成员名称");

// 更新群公告
await client.UpdateGroupNotice("群聊名称", "群公告内容");

// 设置群聊置顶
client.SetChatTop("群聊名称", true);
```

### 朋友圈操作

```csharp
// 打开朋友圈
client.OpenMoments();

// 获取朋友圈列表
var moments = client.GetMomentsList(20);

// 点赞朋友圈
client.LikeMoments("好友名称");

// 回复朋友圈
client.ReplyMoments("好友名称", "评论内容");
```

### 通讯录管理

```csharp
// 获取所有好友
var friends = client.GetAllFriends();

// 添加好友
client.AddFriend("微信号或手机号", "标签");

// 通过新好友请求
var passedFriends = client.PassedAllNewFriend("关键字", "后缀", "标签");

// 移除好友
client.RemoveFriend("好友昵称");
```

### 事件监听

```csharp
// 消息监听
await client.AddMessageListener("好友名称", (messageContext) =>
{
    Console.WriteLine($"收到消息: {messageContext.Message}");
    // 处理消息逻辑
});

// 朋友圈监听
client.AddMomentsListener("好友名称", autoLike: true, (momentsContext, serviceProvider) =>
{
    Console.WriteLine($"好友发了朋友圈");
    // 处理朋友圈逻辑
});

// 新好友监听（自动通过）
client.AddNewFriendAutoPassedListener((newFriends) =>
{
    Console.WriteLine($"新好友: {string.Join(", ", newFriends)}");
}, keyWord: "关键字", suffix: "后缀", label: "标签");
```

### 会话管理

```csharp
// 获取会话列表
var conversations = client.GetVisibleConversations();

// 点击会话
client.ClickConversation("会话名称");

// 查找并打开好友或群聊
var result = client.FindAndOpenFriendOrGroup("名称");
```

## ⚙️ 配置选项

```csharp
WeAutomation.Initialize(options =>
{
    // 调试模式
    options.DebugMode = true;
    
    // 启用键鼠模拟器（降低风控风险）
    options.EnableMouseKeyboardSimulator = true;
    options.KMDeiviceVID = 0x30FA;
    options.KMDeivicePID = 0x0300;
    options.KMVerifyUserData = "验证数据";
    
    // 启用视频录制
    options.EnableRecordVideo = true;
    options.TargetVideoPath = "录制视频保存路径";
    
    // UI 截图保存路径
    options.CaptureUIPath = "截图保存路径";
    
    // 监听间隔（秒）
    options.ListenInterval = 5;
    options.MomentsListenInterval = 10;
    options.NewUserListenerInterval = 5;
    
    // 风控检查间隔（秒）
    options.CheckAppRunningInterval = 3;
    options.EnableCheckAppRunning = true;
    
    // 鼠标移动模式
    options.MouseMoveMode = 8;
    
    // DPI 感知
    options.ProcessDpiAwareness = 1;
});
```

## 🏗️ 项目结构

```
WeChatAuto.SDK/
├── WeAutoCommon/          # 公共库
│   ├── Configs/           # 配置类
│   ├── Enums/            # 枚举定义
│   ├── Models/           # 数据模型
│   └── Utils/            # 工具类
├── WeChatAuto/           # 核心库
│   ├── Components/       # 核心组件
│   ├── Services/         # 服务类
│   └── Utils/           # 工具类
├── WeChatAuto.MCP/       # MCP 协议支持
└── WeChatAuto.Tests/     # 测试项目
```

## 🔧 技术栈

- **FlaUI** - UI 自动化框架
- **Microsoft.Extensions.DependencyInjection** - 依赖注入
- **Microsoft.Extensions.Logging** - 日志记录
- **OneOf** - 联合类型支持
- **Newtonsoft.Json** - JSON 序列化

## ⚠️ 注意事项

1. **风控风险**：频繁操作可能触发微信风控机制，建议：
   - 使用键鼠模拟器降低风险
   - 控制操作频率
   - 避免短时间内大量操作

2. **微信版本**：本 SDK 基于微信 PC 客户端的 UI 结构，不同版本可能存在兼容性问题。

3. **多实例支持**：支持同时管理多个微信客户端，通过微信昵称区分不同实例。

4. **资源释放**：使用完毕后请调用 `Dispose()` 方法释放资源。

## 📝 许可证

本项目采用 MIT 许可证。详见 [LICENSE](LICENSE) 文件。

## 🤝 贡献

欢迎提交 Issue 和 Pull Request！

## 📚 更多文档

详细的 API 文档请参考 [文档站点](docs/index.md)。

## 🙏 致谢

特别感谢我的妻子，虽然她并不完全了解我在做什么，也未曾因之获利，但她始终无条件地支持和信任我。

---

**免责声明**：
本 SDK 仅供学习和研究使用，请遵守微信使用条款，不得用于任何违法违规用途。使用本 SDK 产生的任何后果由使用者自行承担。

