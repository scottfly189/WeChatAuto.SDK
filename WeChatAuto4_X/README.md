<div align="center">
<h1>WECHATAUTO.SDK - 安全构建微信Agent的开发基座</h1>
<p><strong>不注入、双路径、安全构建</strong></p>
<h4>微信是应用，WeChatAuto.SDK 是开发微信自动化应用的开发包。</h4>
<p>这是一个面向开发者的微信自动化开发工具包，帮助你构建自己的微信机器人、微信助手和 AI Agent。</p>
<p>本仓库是社区开源版，用 UI Automation 操作真实微信，可先跑监听、发送、好友、群等基础能力。</p>
<p>
<a href="https://dotnet.microsoft.com/"><img src="https://img.shields.io/badge/.NET-6.0%2B-blue" alt=".NET" /></a>
<a href="https://www.python.org/"><img src="https://img.shields.io/badge/Python-3.13%2B-blue?logo=python&logoColor=white" alt="Python" /></a>
<a href="https://isocpp.org/"><img src="https://img.shields.io/badge/C%2B%2B-blue?logo=cplusplus&logoColor=white" alt="C++" /></a>
<a href="https://go.dev/"><img src="https://img.shields.io/badge/Go-blue?logo=go&logoColor=white" alt="Go" /></a>
<a href="https://www.java.com/"><img src="https://img.shields.io/badge/Java-blue?logo=openjdk&logoColor=white" alt="Java" /></a>
<a href="https://nodejs.org/"><img src="https://img.shields.io/badge/Node.js-blue?logo=nodedotjs&logoColor=white" alt="Node.js" /></a>
<a href="https://www.rust-lang.org/"><img src="https://img.shields.io/badge/Rust-blue?logo=rust&logoColor=white" alt="Rust" /></a>
<a href="LICENSE"><img src="https://img.shields.io/badge/license-MIT-green" alt="license MIT" /></a>
</p>
<p><strong><a href="https://scottfly189.github.io/WeChatAuto.SDK/">Website</a> · <a href="https://github.com/scottfly189/WeChatAuto.SDK/releases/download/2.0.7/community_allin.rar">一键试用</a> · <a href="https://scottfly189.github.io/WeChatAuto.SDK/main.html#/README">Guides</a> · <a href="#-faq">FAQ</a></strong></p>
<p><a href="./docs/why.md">为什么选择我们?</a></p>
</div>
<div align="center">
<br />
<img src="https://github.com/scottfly189/WeChatAuto.SDK/raw/master/Images/main_1.png" alt="WECHATAUTO.SDK主界面" width="1200" />
<br/>
<img src="https://github.com/scottfly189/WeChatAuto.SDK/raw/master/Images/second.png" alt="WECHATAUTO.SDK 吸附框" width="1200" />
<br/>
</div>

> [!TIP]
> 体验提示：如果需要体验WeChatAuto.SDK的通力，我架设了一个测试导航机器人，欢迎添加微信好友体验，👉 [我要体验](./MD/Experience.md).


> [community_allin.rar](https://github.com/scottfly189/WeChatAuto.SDK/releases/download/2.0.7/community_allin.rar) 是限时可用的 VIP 体验包，不是本仓库社区开源版的构建。解压运行即可先完整体验；日常开发请从本仓库源码或 NuGet 接入社区版。

## 📋 系统要求

- Windows 操作系统,不支持Linux和MacOS;
- **风控风险**：频繁操作可能触发微信风控机制，建议：
   - 控制操作频率
   - 避免短时间内大量操作
   - 使用键鼠模拟器降低风险

## ✨ 快速入门

> [!NOTE]
> SDK支持两种使用方式: ```内嵌方式```(Embedded) 与 ```HTTP WebAPI``` 方式<br/>

#### 🚀 快速开始（30 秒上手）

> [!IMPORTANT]
> 使用前请用Claude code 安装.net10环境;
>
> 此示例在Examples目录的[FirstDemo](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/WeChatAuto4_X/Examples/FirstDemo)项目中;
> 
> 更多使用请参见: [使用指南](https://scottfly189.github.io/WeChatAuto.SDK/main.html#/README)

1. 新建一个.net10控制台项目

```csharp
dotnet new console -n demo
```

2. 修改项目属性，打开 demo.csproj 文件，把```TargetFramework```修改为```net10.0-windows```,如果你使用的是.net6.0或.net7.0，请把```TargetFramework```修改为```net6.0-windows```或```net7.0-windows```，如下所示:
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>

```

3. 安装依赖包
  
```bash
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package WeChatAuto4x.SDK
```

4. 在 Program.cs 中编写如下代码：

```csharp
using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = WeAutomation.Initialize(options =>
{
    options.DebugMode = false;   // 可选，在生产环境建议关闭
});

using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
//检查打开有几个微信
var wechat_list = clientFactory.GetWeChatClientNames();
Console.WriteLine($"电脑上打开微信{wechat_list.Count()}个: {string.Join(',',wechat_list)}");

// 打开第一个微信，发送消息
var client = clientFactory.GetWeChatClient(wechat_list.First());
// 给好友 AI.Net_test 发送文本消息,这里应该改成你自己的微信名称.
await client.SendMessage("AI.Net_test","hello world!!");

... 更多功能请参考文档
```

## 🎈 关于键鼠模拟器

键鼠模拟器是一类专门的硬件设备，能够模拟物理键盘和鼠标的真实输入。相较于直接调用 PostMessage、SetInput 等 API 进行注入，这类传统软件方式往往会留下可被识别的痕迹，极易被微信等应用检测为自动化行为并引发风控。而键鼠模拟器通过硬件底层发送信号，模拟出的输入和人手操作无异，从而高度还原人类使用方式，在风控安全性和隐蔽性方面具备天然优势。

实际测试表明，在微信某些高敏感操作场景（比如群聊内加好友）下，借助键鼠模拟器能有效降低被风控的概率。需要注意的是，即便是手动操作，部分极端高风险情况下也有可能触发风控。因此，强烈建议在高敏感度和易风控场景优先考虑且规范使用键鼠模拟器，以获得更稳定和安全的自动化体验。

本 SDK 同时支持纯软件自动化以及结合硬件键鼠模拟器的自动化操作，满足不同业务需求和安全等级场景下的使用选择。

关于键鼠模拟器更深度的了解，请参见：[键鼠模拟器](https://github.com/scottfly189/SKSimulator)


## 😊 关于VIP

看完社区版之后，如需对照完整能力或了解 VIP，请直接看 [WECHATAUTO.SDK 能力表](../docs/ability.md)。

如需升级成为 VIP，或了解具体权益和支持方案，👉[请与我联系](https://github.com/scottfly189/scottfly189/blob/main/vip.md)。


## 📝 许可证

本项目采用 MIT 许可证。详见 [LICENSE](LICENSE) 文件。

## 🔆 FAQ
<details>
<summary>❓ 运行WechatAuto.sdk的示例自动化不了微信客户端？</summary>
回答：请按下面的步骤检查：

- 确保待自动化的微信的窗口是打开状态;
- 检查windows版本，仅支持win10+;
- 若当前微信未暴露 UI Tree，请参考 [如果没有 UI Tree 怎么办](https://github.com/scottfly189/WeChatAuto.SDK/issues/3)
  > 查看UI Tree的方法：下载并安装 UI Tree查看工具，如: inspect.exe或者FlaUInspect等，[下载链接](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/Tools)
- 如果经过上面的步骤还不行，请联系作者，并将你的代码发给他看;
</details>

<details>
<summary>❓ WechatAuto.sdk可以直接运行吗？</summary>
回答：SDK支持两种开发模式

- 内嵌方式，即以DLL或者Nuget包引入的方式，这个不能直接运行，因为是一个SDK开发包，需要整合在你的项目中使用，当然你也可以直接运行[示例代码](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/WeChatAuto4_X/Examples/WinForm_Demo)或者单元测试;
- Http WebAPI的方式，这个可以直接运行，请[下载](https://github.com/scottfly189/WeChatAuto.SDK/releases/tag/2.0.7)后直接运行;
</details>

<details>
<summary>❓ 社区版和 VIP 是什么关系？</summary>
回答：本仓库是社区开源版，用 UI Automation 即可先跑监听、发送、好友、群等基础能力。VIP 为专用私库，提供更多能力与技术支持，完整对照见 [WECHATAUTO.SDK 能力表](../docs/ability.md)。请注意：一键试用包 [community_allin.rar](https://github.com/scottfly189/WeChatAuto.SDK/releases/download/2.0.7/community_allin.rar) 是限时可用的 VIP 体验包，不是社区开源版的构建。
</details>


<details>
<summary>❓ WeChatAuto.SDK开启录屏不成功？</summary>
问题：我使用了下面的代码开启了录屏后，运行被卡在在那里不往下运行了

```
WeAutomation.Initialize(builder.Services, options =>
{
    //开启调试模式，调试模式会在获得焦点时边框高亮，生产环境建议关闭
    options.DebugMode = true;
    //开启录制视频功能，录制的视频会保存在项目的运行目录下的Videos文件夹中
    options.EnableRecordVideo = true;  
});
```

回答：WechatAuto.SDK使用ffmpeg进行录制，如果系统没有安装ffmpeg，第一次运行会自动从官网下载ffmpeg.exe,此文件比较大，并且需要梯子，如果没有开梯子或者梯子质量不好，会产生上述情况，建议按下面的步骤解决：

1. 开VPN,等第一次下载完;
2. 手动上传ffmpeg.exe文件至项目运行的ffmepg目录下:
- 开发环境为：bin\Debug\net9.0-windows\ffmpeg\ffmpeg.exe
- 运行环境为: 你的执行文件所在目录\ffmpeg\ffmpeg.exe
</details>

<details>
<summary>❓ SDK安装在服务器运行，并且用远程桌面连接服务器，远程桌面最小化的时候自动化异常？</summary>
回答：请在"发起远程桌面的客户端电脑"上,而不是服务器上执行下面脚本:

1. [下载脚本](https://github.com/scottfly189/WeChatAuto.SDK/blob/master/Tools/EnableRdpMinimize.bat)
2. 下载脚本后按下面的步骤执行:
   - 执行作业之前，注销机器人（服务器）计算机上的所有用户。
   - 执行作业之后，重新打开远程桌面连接。
3. 现在，即使 RDP 窗口已最小化，您也可以自动化用户界面操作。
</details>

<details>
<summary>❓ 远程桌面连接服务器后SDK发送消息不了？</summary>
回答: 请远程桌面打开服务器的时候，远程桌面按下面步骤执行： 打开远程桌面->本地资源->剪切板 打上勾，让远程桌面支持剪切板;

如下图所示：

![剪切板](./MD/clipboard.png)
</details>

## 🤝 贡献

如果发现BUG或者有功能增强需求，欢迎提交 Issue 和 Pull Request！

## 🙏 致谢

在这里感谢那些还在为自由和正义而奋斗的人们🎉🎉

---


## ⚒️ 免责声明

本 SDK 仅供学习和研究使用，请遵守微信使用条款，不得用于任何违法违规用途。使用本 SDK 产生的任何后果由使用者自行承担。
