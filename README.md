# WECHATAUTO.SDK - 面向AI的现代化微信自动化框架

[![.NET](https://img.shields.io/badge/.NET-4.8%20%7C%206.0%2B-blue)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

WeChatAuto.SDK 是一款面向 AI 的微信 PC 客户端自动化 SDK，基于 .NET 与 UI 自动化技术开发。它支持消息收发、转发、群聊与好友管理、朋友圈操作等多种功能，并专为集成人工智能（如 LLM 上下文交互）场景设计。SDK 提供丰富直观的 API，支持 .NET 现代化特性，比如依赖注入，让你轻松将自定义对象集成进自动化流程。

## ✨ 特性

- 💬 **消息操作** - 发送文字、表情、文件，支持 @ 提醒,转发消息等
- 👥 **群聊管理** - 创建群聊、添加/移除成员、更新群公告等
- 📱 **朋友圈操作** - 点赞、评论、监听朋友圈动态
- 📋 **通讯录管理** - 自动添加好友、管理联系人、处理新好友请求
- 👂 **事件监听** - 消息监听(提供LLM上下文)、朋友圈监听、新好友监听等
- 🛡️ **降低风控** - 同时支持纯软件自动化以及结合硬件键鼠模拟器的自动化操作，满足不同业务需求和安全等级场景下的使用选择。
- 🔧 **易于集成** - 支持依赖注入，可轻松集成到现有项目
- 🚀 **多微信实例支持** - 同时管理多个微信客户端实例
- 😊 **AI 友好集成** - 原生支持 LLM 上下文对接并提供 MCP Server，便于对接主流智能体与平台（如 MEAI、SK、MAF），助力智能应用高效闭环与创新集成

## 📋 系统要求

- Windows 操作系统
- .NET Framework 4.8+ 或 .NET 6.0+ (Windows)，支持.NET的框架有:net48;net481;net6.0-windows; net7.0-windows;net8.0-windows;net9.0-windows;net10.0-windows;
- 微信 PC 客户端已安装并运行,本 SDK 基于微信 PC 客户端(版本号:3.9.12.55)的 UI 结构开发，不同版本可能存在兼容性问题。

## 🚀 快速开始

### 安装

通过 NuGet 安装：

```bash
dotnet add package WeChatAuto.SDK
```

### 基本使用

#### 示例一 - 给好友（或群聊昵称）发送消息：

- 步骤一：新建项目，如下所示:

```
dotnet new console -n demo01
```

- 步骤二：将demo01.csproj项目文件的net10.0**修改**成net10.0-windows,如下所示:

```
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
```

- 步骤三：安装依赖

```
dotnet add package WeChatAuto.SdK
dotnet add package Microsoft.Extensions.DependencyInjection
```
- 步骤四：项目demo01的Program.cs修改成如下：

```csharp
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Components;
using WeChatAuto.Services;

// 初始化WeAutomation服务
var serviceProvider = WeAutomation.Initialize(options =>
{
    options.DebugMode = true;   //开启调试模式，调试模式会在获得焦点时边框高亮，生产环境建议关闭
    //options.EnableRecordVideo = true;  //开启录制视频功能，录制的视频会保存在项目的运行目录下的Videos文件夹中
});

using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
Console.WriteLine($"当前客户端打开的微信客户端为：{string.Join(",", clientFactory.GetWeChatClientNames())}，共计{clientFactory.GetWeChatClientNames().Count}个微信客户端。");
//获取当前打开的微信客户端名称列表
var clentNames = clientFactory.GetWeChatClientNames();    
//获取第一个微信客户端
var wxClient = clientFactory.GetWeChatClient(clentNames.First());  
 //通过微信客户端发送消息给好友昵称AI.Net，测试时请把AI.Net修改成自己的好友昵称
wxClient?.SendWho("AI.Net","你好，欢迎使用AI.Net微信自动化框架！"); 
```

> **注意**：  
> 1. 本项目仅支持 Windows 系统，请务必将项目文件的 TargetFramework 设置为 netxx.0-windows（如 net10.0-windows），否则编译时会出现警告。后续不再赘述。  
> 2. 如果是手动管理WeChatClientFactory,请在应用结束时运行clientFactory.Dispose(),或者象示例代码一样将代码放入using块自动释放,如果把WeChatAuto.SDK加入您的依赖注入容器，则不存在此问题。
> 3. WeAutomation.Initialize()方法有两个重载，分别适用于：加入外部依赖注入与使用内部依赖注入。


#### 示例二 - 演示监听好友（或者群聊昵称）的消息,使用消息上下文获取消息并回复,并且还演示了如何通过依赖注入获取消息上下文的注入对象,执行自己的业务逻辑：
- 前置步骤：安装依赖

```
dotnet add package WeChatAuto.SdK
dotnet add package Microsoft.Extensions.Hosting
```
- 将项目demo02的Program.cs修改成如下

```csharp
using Microsoft.Extensions.Hosting;
using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

WeAutomation.Initialize(builder.Services, options =>
{
    //开启调试模式，调试模式会在获得焦点时边框高亮，生产环境建议关闭
    options.DebugMode = true;
    //开启录制视频功能，录制的视频会保存在项目的运行目录下的Videos文件夹中
    //options.EnableRecordVideo = true;  
});

//这里注入自已的服务（或者对象），如LLM服务等
builder.Services.AddSingleton<LLMService>();

var serviceProvider = builder.Services.BuildServiceProvider();
var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
// 得到名称为"Alex"的微信客户端实例，测试时请将AI.net替换为你自己的微信昵称
var client = clientFactory.GetWeChatClient("Alex");
// 监听微信群测试11
await client.AddMessageListener("测试11", (messageContext) =>
{
    var index = 0;
    //打印收到最新消息
    foreach (var message in messageContext.NewMessages)
    {
        index++;
        Console.WriteLine($"收到消息：{index}：{message.ToString()}");
        Console.WriteLine($"收到消息：{index}：{message.Who}：{message.MessageContent}");
    }
    //打印收到所有消息的后十条
    var allMessages = messageContext.AllMessages.Skip(messageContext.AllMessages.Count - 10).ToList();
    index = 0;
    foreach (var message in allMessages)
    {
        index++;
        Console.WriteLine($"...收到所有消息的前10条之第{index}条：{message.Who}：{message.MessageContent}");
        Console.WriteLine($".................详细之第{index}条：{message.ToString()}");
    }
    //是否有人@我
    if (messageContext.IsBeAt())
    {
        var messageBubble = messageContext.MessageBubbleIsBeAt().FirstOrDefault();
        if (messageBubble != null)
        {
            messageContext.SendMessage("我被@了！！！！我马上就回复你！！！！", new List<string> { messageBubble.Who });
        }
        else
        {
            messageContext.SendMessage("我被@了！！！！我马上就回复你！！！！");
        }
    }
    //是否有人引用了我的消息
    if (messageContext.IsBeReferenced())
    {
        messageContext.SendMessage("我被引用了！！！！");
    }
    //是否有人拍了拍我
    if (messageContext.IsBeTap())
    {
        messageContext.SendMessage("我被拍一拍了[微笑]！！！！");
    }
    if (!messageContext.IsBeAt() && !messageContext.IsBeReferenced() && !messageContext.IsBeTap())
    {
        //回复消息，这里可以引入大模型自动回复
        messageContext.SendMessage($"我收到了{messageContext.NewMessages.FirstOrDefault()?.Who}的消息：{messageContext.NewMessages.FirstOrDefault()?.MessageContent}");
    }
    //可以通过注入的服务容器获取你注入的服务实例，然后调用你的业务逻辑,一般都是LLM的自动回复逻辑
    var llmService = messageContext.ServiceProvider.GetRequiredService<LLMService>();
    llmService.DoSomething();
});


var app = builder.Build();
await app.RunAsync();

/// <summary>
/// 一个包含LLM服务的Service类，用于注入到MessageContext中
/// </summary>
public class LLMService
{
    private ILogger<LLMService> _logger;
    public LLMService(ILogger<LLMService> logger)
    {
        _logger = logger;
    }
    public void DoSomething()
    {
        _logger.LogInformation("这里是你注入的服务实例，可以在这里编写你的业务逻辑  ");
    }
}


```

> 前置步骤跟Demo01一致,可以通过messageContext对象执行各种操作,也可以通过messageContext对象获得依赖注入容器，获取自己的对象，执行自己的业务逻辑;

#### 示例三 - MCP Server的使用 - 以vscode为例讲解
- 进入源码的.vscode\mcp.json,修改配置如下:

```
{
	"servers": {
		"wechat_mcp_server": {
			"type": "stdio",
			"command": "dotnet",
			"args": [
				"run",
                "--project",
                "改成你的WeChatAuto.MCP.csproj的路径"
			]
		}
	}
}
```

- 在mcp.json页面点击"Start"按钮启动mcp server  
- 启动GitHub Copilot Chat,在Chat页提问: 请帮我给微信好友:AI.Net发送消息：Hello world!

## ⚙️ 架构概览
### 🚀 WeChatAuto.SDK 架构图

> 敬请关注后续更新，目前最主要提供了微信聊天的自动化，后期会提供对腾迅会议、微信公众号/订阅号等的自动化与MCP Server,如果您有什么建议，也可以跟我提。

![WeChatAuto.SDK架构图](https://raw.githubusercontent.com/scottfly189/WeChatAuto.SDK/master/Images/article.png)

### 🚀 主要类与关系

> WeChatAuto.SDK 采用 POM（页面对象模型）设计思想，针对微信的各类操作场景提供了清晰、模块化的对象抽象，大幅提升了自动化脚本的可读性与可维护性。

![WeChatAuto.SDK 主要类关系示意](https://raw.githubusercontent.com/scottfly189/WeChatAuto.SDK/master/Images/class.png)

### ⛷️ 开发计划

| 类别 | 功能 | 完成度 | 备注 |
| --- | --- | --- | --- |
| 消息管理 | 发送文字消息 | ✅ |  |
| 消息管理 | 发送文件 | ✅ |  |
| 消息管理 | 发送自定义表情包 | ✅ | 可按表情包索引、名称或者描述发送 |
| 消息管理 | 引用消息 | ✅ |  |
| 消息管理 | 发送语音聊天/语音会议 | ✅ | 适用于单个好友与群聊 |
| 消息管理 | 发送视频聊天 | ✅ | 适用于单个好友 |
| 消息管理 | 发起直播 | ✅ | 适用于群聊 |
| 消息管理 | @群好友 | ✅ |  |
| 消息管理 | @所有人 | ✅ | 适用于自有群管理 |
| 消息管理 | 合并转发 | ✅ |  |
| 消息管理 | 获取消息 | ✅ |  |
| 消息管理 | 监听消息 | ✅ |  |
| 消息管理 | 引用时@ | ✅ |  |
| 消息管理 | 通过消息添加好友 | ✅ |  |
| 消息管理 | 通过消息获取详情 | ✅ |  |
| 消息管理 | 获取卡片消息链接 | ✅ |  |
| 消息管理 | 子窗口（好友/群）守护 | ✅ | 误关闭子窗口重新打开 |
| 通讯录管理 | 获取好友列表 | ✅ |  |
| 通讯录管理 | 发送好友请求 | ✅ |  |
| 通讯录管理 | 接受好友请求 | ✅ |  |
| 通讯录管理 | 删除好友 | ✅ |  |
| 通讯录管理 | 监听好友请求 | ✅ |  |
| 通讯录管理 | 监听好友请求，并自动通过 | ✅ |  |
| 通讯通讯录管理 | 监听好友请求，并仅通过指定关键词的好友，自动加备注、标签 | ✅ |  |
| 通讯管理 | 修改备注 | ✅ |  |
| 通讯管理 | 增加标签 | ✅ |  |
| 群管理 | 新建群 | ✅ |  |
| 群管理 | 邀请入群 | ✅ |  |
| 群管理 | 修改群名 | ✅ |  |
| 群管理 | 修改群备注 | ✅ |  |
| 群管理 | 修改群公告 | ✅ |  |
| 群管理 | 修改我在本群昵称 | ✅ |  |
| 群管理 | 消息免打扰 | ✅ |  |
| 群管理 | 获取群列表 | ✅ |  |
| 朋友圈 | 获取朋友圈内容 | ✅ |  |
| 朋友圈 | 下载朋友圈图片 | ✅ |  |
| 朋友圈 | 点赞朋友圈 | ✅ |  |
| 朋友圈 | 自动评论朋友圈 | ✅ |  |
| MCP | MCP Server | ✅ |  |
| 企业客服 | 自动根据企业知识库回答客户问题 | ❌ | 根据公司知识库回答问题 |
| 企业督办 | 企业客户群提出问题的督办 | ❌ | 企业客服的各种督办场景 |
| 腾迅会议 | 自动安排腾迅会议 | ❌ | 对腾迅会议的自动化 |
| 公众号/订阅号 | 自动发布公众号/订阅号文章 | ❌ | 对公众号/订阅号的自动化 |
| 效率 | 计划任务 | ❌ |  |


- 持续迭代优化核心功能，提升稳定性与兼容性
- 推出更丰富的自动化操作场景，满足多样化业务需求
- 完善开发文档与示例，提高使用与扩展的便捷性
- 社区需求优先，欢迎反馈建议 

## ⚠️ 注意事项

1. **风控风险**：频繁操作可能触发微信风控机制，建议：
   - 使用键鼠模拟器降低风险
   - 控制操作频率
   - 避免短时间内大量操作

2. **微信版本**：本 SDK 基于微信 PC 客户端(版本号:3.9.12.55)的 UI 结构开发，不同版本可能存在兼容性问题。

3. **多实例支持**：支持同时管理多个微信客户端，通过微信昵称区分不同实例。

## 🎈 关于键鼠模拟器

键鼠模拟器是一类专门的硬件设备，能够模拟物理键盘和鼠标的真实输入。相较于直接调用 PostMessage、SetInput 等 API 进行注入，这类传统软件方式往往会留下可被识别的痕迹，极易被微信等应用检测为自动化行为并引发风控。而键鼠模拟器通过硬件底层发送信号，模拟出的输入和人手操作无异，从而高度还原人类使用方式，在风控安全性和隐蔽性方面具备天然优势。

实际测试表明，在微信某些高敏感操作场景（比如群聊内加好友）下，借助键鼠模拟器能有效降低被风控的概率。需要注意的是，即便是手动操作，部分极端高风险情况下也有可能触发风控。因此，强烈建议在高敏感度和易风控场景优先考虑且规范使用键鼠模拟器，以获得更稳定和安全的自动化体验。

本 SDK 同时支持纯软件自动化以及结合硬件键鼠模拟器的自动化操作，满足不同业务需求和安全等级场景下的使用选择。

关于键鼠模拟器更深度的了解，请参见：[键鼠模拟器](https://github.com/scottfly189/SKSimulator)

## 😂 关于微信4.X

微信4.x.x版本目前正在研发中，新方案基于机器视觉实现。不过，目前受限于机器视觉技术，对聊天记录的监控仍存在难度，暂不支持在生产环境中使用。如果你有更优的解决思路或建议，欢迎随时交流讨论！


## 😊 关于VIP

由于时间和精力有限，为了更好地投入研发和持续改进产品，本人目前仅为**已购买VIP服务的客户**提供优先和深入的技术支持。这样做，是希望通过区分服务对象，专注为VIP客户带来更高品质、更有保障的体验。当然，广大普通用户依然欢迎通过 Issue 反馈和交流，只是服务响应的优先级和深度会有所不同。

**🎉 VIP 客户可享受以下专属服务保障：**
- 💡 **BUG 优先响应**：出现 Bug 时，第一时间定位和解决，保障 VIP 项目的稳定运行。
- 📚 **完整开发文档**：提供详细、及时更新的 API 开发文档，助力集成与开发效率。
- 🎬 **系统教学视频**：涵盖入门到进阶的全流程教学视频，帮助用户高效掌握 SDK。
- 👥 **VIP 技术交流群**：专属 VIP 交流群，优先、及时解答问题，实时高效支持。
- 🚀 **专属 VIP 私有仓库**：VIP 客户将获专属私有仓库，会不定期提供丰富的应用层扩展与独享内容。

**😊 非 VIP 客户：**  

WeChatAuto.SDK的非VIP与VIP的核心代码层面完全一致，非VIP没有任何功能与代码层面的限制，同样欢迎非VIP通过 Issue 提问或反馈问题，我会在时间允许情况下进行处理，但响应和解决可能会有延迟，敬请谅解。

如需升级成为 VIP，或了解 VIP 具体权益和支持方案，请与我联系。感谢理解与支持，让我有更多精力专注于技术创新与完善！

## 📝 许可证

本项目采用 MIT 许可证。详见 [LICENSE](LICENSE) 文件。

## 🤝 贡献

欢迎提交 Issue 和 Pull Request！

---

**免责声明**：
本 SDK 仅供学习和研究使用，请遵守微信使用条款，不得用于任何违法违规用途。使用本 SDK 产生的任何后果由使用者自行承担。

