# WECHATAUTO.SDK - 面向AI的现代化微信自动化框架

[![.NET](https://img.shields.io/badge/.NET-4.8%20%7C%206.0%2B-blue)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

WeChatAuto.SDK 是一款面向 AI 的微信 PC 客户端自动化/RPA SDK，基于 .NET 与 UI 自动化技术开发。它支持消息收发、转发、群聊与好友管理、朋友圈操作等多种功能，并专为集成人工智能（如 LLM 上下文交互）场景设计。SDK 提供丰富直观的 API，支持 .NET 现代化特性，比如依赖注入，让你轻松将自定义对象集成进自动化流程。

## ✨ 特性

- 💬 **提供丰富API** - 发送文字、表情、文件，支持 @ 提醒,转发消息等消息管理、会话管理、好友/群聊管理、通讯录管理、朋友圈、各类事件监听等丰富API
- 🛡️ **降低风控** - 同时支持纯软件自动化以及结合硬件键鼠模拟器的自动化操作，满足不同业务需求和安全等级场景下的使用选择。
- 🚀 **多微信实例支持** - 支持同时管理多个微信客户端，通过微信昵称区分不同实例
- 🚀 **现有应用集成** - 支持依赖注入、日志等企业级特性，由于是SDK库，所以方便在现有系统集成,通过SDK,也非常容易的开发新的微信RPA/自动化应用
- 🚀 **Web Support** - 提供Rest API 供Python、js/ts等非.net的家人们调用。 👉[点击了解Web Support](./MD/WebSupport.md)
- 😊 **AI 友好集成** - 原生支持 LLM 上下文对接并提供 MCP Server，便于对接主流智能体与平台（如 MEAI、SK、MAF），助力智能应用高效闭环与创新集成

**👉 如需体验**，请点击链接进入: [WeChatAuto.SDK体验指引](./MD/Experience.md)

> 如果觉得有帮助，欢迎点赞、Star和Fork本项目，以免失联，感谢支持！

## 🎉 微信客户端版本说明(重要！！)

在进行微信自动化开发时，客户端版本是一个必须重点关注的因素。不同版本的微信在 UI 结构、控件树以及安全策略上存在差异，会直接影响自动化的稳定性与兼容性。

**WeChatAuto.SDK** 提供两个版本的SDK:

---

#### 🧱 微信 3.9.12.xx（稳定版）

**微信 3.9.12.x** 系列的最终版本;

- UI 结构已完全固化，变动极小
- 一旦问题被修复，基本不会因版本更新再次出现
- 建议使用场景是**自用**,而且并不发布出去给其他人用，优先选择此版本

**使用指南**

👉 完整的文档请参考: [WeChatAuto.SDK 3.9.12.xx文档](https://scottfly189.github.io/WeChatAuto.SDK/)

👉 安装安装不上微信客户端3.9.12.xx？ 请参考: [如何安装3.9.12.xx等微信低版本客户端](https://github.com/scottfly189/WeChatAuto.SDK/issues/2)

👉 项目源码与DEMO演示，请参考: [3.9.12.xx源码及DEMO](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/WeChatAuto3_9_12_xx)

---

#### ⚡ 微信 4.1.9.xx（最新版本）

- 基于微信 4.x 最新微信客户端的持续演进版本
- 客户端仍在不断更新，UI 和内部机制可能发生变化,所以自动化可能会受到版本更新影响，需要适配调整
- ```WeChatAuto.SDK``` 将持续跟进最新版本进行适配与优化
- 建议使用场景是**追求新功能 / 能接受版本变化** 有一定维护能力的项目

> 目前```4.1.9.xx微信版本```为VIP用户独享,并不对外开放


## ⚠️ 注意事项

1. **风控风险**：频繁操作可能触发微信风控机制，建议：
   - 使用键鼠模拟器降低风险
   - 控制操作频率
   - 避免短时间内大量操作

2. **微信版本**：做微信RPA一定要注意微信的版本，请确认微信版本正确的对应了WeChatAuto.SDK的版本;


## 🎈 关于键鼠模拟器

键鼠模拟器是一类专门的硬件设备，能够模拟物理键盘和鼠标的真实输入。相较于直接调用 PostMessage、SetInput 等 API 进行注入，这类传统软件方式往往会留下可被识别的痕迹，极易被微信等应用检测为自动化行为并引发风控。而键鼠模拟器通过硬件底层发送信号，模拟出的输入和人手操作无异，从而高度还原人类使用方式，在风控安全性和隐蔽性方面具备天然优势。

实际测试表明，在微信某些高敏感操作场景（比如群聊内加好友）下，借助键鼠模拟器能有效降低被风控的概率。需要注意的是，即便是手动操作，部分极端高风险情况下也有可能触发风控。因此，强烈建议在高敏感度和易风控场景优先考虑且规范使用键鼠模拟器，以获得更稳定和安全的自动化体验。

本 SDK 同时支持纯软件自动化以及结合硬件键鼠模拟器的自动化操作，满足不同业务需求和安全等级场景下的使用选择。

关于键鼠模拟器更深度的了解，请参见：[键鼠模拟器](https://github.com/scottfly189/SKSimulator)


## 😊 关于VIP

由于时间和精力有限，为了更好地投入研发和持续改进产品，本人目前仅为**已购买VIP服务的客户**提供优先和深入的技术支持。这样做，是希望通过区分服务对象，专注为VIP客户带来更高品质、更有保障的体验。当然，广大普通用户依然欢迎通过 Issue 反馈和交流，只是服务响应的优先级和深度会有所不同。

**🎉 VIP 客户可享受以下专属服务保障：**
- 💡 **BUG 优先响应**：出现 Bug 或有新的 Enhancement ，第一时间响应、定位和解决，保障 VIP 项目的稳定运行;
- 📚 **完整开发文档**：提供详细、及时更新的 API 开发文档，助力集成与开发效率;
- 🎬 **一小时入门教学视频**：涵盖入门到进阶的全流程教学视频，帮助用户高效掌握 SDK;
- 👥 **VIP 技术交流群**：专属 VIP 交流群，优先、及时解答问题，实时高效支持;
- 🚀 **专属 VIP 私有仓库**：VIP 客户将获专属私有仓库，会不定期提供丰富的应用层扩展与独享内容;
- 🚀 **一对一的专属vip服务**: 这是你加入 VIP 的核心理由,微信自动化能力由 WeChatAuto.SDK 提供深度支持，业务系统由你自由扩展，实现技术与业务的高效分工;

**😊 非 VIP 客户：**  

同样欢迎非VIP通过 Issue 提问或反馈问题;

非 VIP 会员私下找我，我会在时间允许情况下进行处理，但响应和解决可能会有延迟，敬请谅解。

如需升级成为 VIP，或了解 VIP 具体权益和支持方案，👉[请与我联系](https://github.com/scottfly189/scottfly189/blob/main/vip.md)。感谢理解与支持，让我有更多精力专注于技术创新与完善！

---

## 🎁 WechatAuto.SDK功能介绍视频

[【微信轻松接入人工智能（一） - WeChatAuto.SDK功能介绍】](https://www.bilibili.com/video/BV1PHFZzsEkf?vd_source=bfba6dfea0475f14a68ac5bc4ec2e165
)

[【微信轻松接入人工智能（二）- WeChatAuto.SDK发送消息】](https://www.bilibili.com/video/BV17qFZzVEix?vd_source=bfba6dfea0475f14a68ac5bc4ec2e165
)

[【微信轻松接入人工智能（三）- WeChatAuto.SDK发送消息2】](https://www.bilibili.com/video/BV1LrFZz6EGa?vd_source=bfba6dfea0475f14a68ac5bc4ec2e165
)

[【微信轻松接入人工智能（四）- WeChatAuto.SDK消息监听】](https://www.bilibili.com/video/BV1vrFZz6Ezf?vd_source=bfba6dfea0475f14a68ac5bc4ec2e165
)

[【微信轻松接入人工智能（五）- WeChatAuto.SDK群聊中@好友】](https://www.bilibili.com/video/BV1i6FZz3Exc?vd_source=bfba6dfea0475f14a68ac5bc4ec2e165
)

[【微信轻松接入人工智能（六）- WeChatAuto.SDK的MCP-Server的使用介绍】](https://www.bilibili.com/video/BV1j6FZz3EGu?vd_source=bfba6dfea0475f14a68ac5bc4ec2e165
)

更多入门视频，或想深度学习，请加入VIP


---

## 📝 许可证

本项目采用 MIT 许可证。详见 [LICENSE](LICENSE) 文件。

## 🤝 贡献

欢迎提交 Issue 和 Pull Request！

## 🙏 致谢

在这里感谢那些还在为自由和正义而奋斗的人们🎉🎉

---


## ⚒️ 免责声明

本 SDK 仅供学习和研究使用，请遵守微信使用条款，不得用于任何违法违规用途。使用本 SDK 产生的任何后果由使用者自行承担。

