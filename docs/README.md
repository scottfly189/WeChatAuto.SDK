# 🔆 WECHATAUTO.SDK简介

---

[![.NET](https://img.shields.io/badge/.NET-6.0%2B-blue)](https://dotnet.microsoft.com/)
[![Python](https://img.shields.io/badge/Python-3.13%2B-blue?logo=python&logoColor=white)](https://www.python.org/)
[![C++](https://img.shields.io/badge/C%2B%2B-blue?logo=cplusplus&logoColor=white)](https://isocpp.org/)
[![Go](https://img.shields.io/badge/Go-blue?logo=go&logoColor=white)](https://go.dev/)
[![Java](https://img.shields.io/badge/Java-blue?logo=openjdk&logoColor=white)](https://www.java.com/)
[![Node.js](https://img.shields.io/badge/Node.js-blue?logo=nodedotjs&logoColor=white)](https://nodejs.org/)
[![Rust](https://img.shields.io/badge/Rust-blue?logo=rust&logoColor=white)](https://www.rust-lang.org/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

!> 不注入、双路径、安全构建。微信是应用，WeChatAuto.SDK 是开发微信自动化应用的开发包。这是一个面向开发者的微信自动化开发工具包，帮助你构建自己的微信机器人、微信助手和 AI Agent。本仓库是社区开源版，用 UI Automation 操作真实微信，可先跑监听、发送、好友、群等基础能力。完整能力对照见 [WECHATAUTO.SDK 能力表](ability.md)。

# 🎉 核心特性

- 🔓 **纯 UI 自动化，不注入** — 基于 Windows 官方 UI Automation（FlaUI/UIA3）驱动微信，不往微信进程注入任何 DLL、不挂消息钩子。适合发送、好友、群等必须「点」微信的能力，社区开源版即可先走这条上手。
- 🎯 **全场景能力覆盖** — 消息收发（文字/@/引用/文件/表情/语音）、群管理（建群/改名/公告/成员增删/退群/邀请）、好友与通讯录、朋友圈、消息监听、消息转发，一套 SDK 搞定整个微信业务。
- 🤖 **AI 原生** — 内置阿里千问 **TTS 语音消息**、**LLM 文本口语化优化**、**声音复刻 / 语音克隆**（VIP），为 AI 客服、语音机器人等场景开箱即用。
- 👥 **多微信实例支持** — `WeChatClientFactory` 一个进程同时管理多个微信客户端，天然支持矩阵运营与多账号客服。
- 🌐 **HTTP REST API + Swagger** — 内置 ASP.NET Core 服务，把消息/好友/群/会话/导航全能力暴露为 REST 接口，任何语言都能接入。
- 🧩 **7 种语言官方客户端 SDK** — 提供 .NET / Python / Java / Go / Node.js / Rust / C++ 官方客户端，跨语言二次开发零门槛。
- 🔐 **API Key 鉴权 + 离线授权** — 内置 `X-API-Key` 鉴权与 RSA/DPAPI 离线许可证体系，方便做授权分发与商业化交付。
- ⚙️ **界面操作统一排队** — 需要走界面时，监听与发送在同一条 UI 操作队列里串行执行。
- 🧠 **内置 OCR** — 集成 PaddleOCR（PP-OCRv6 ONNX），无需额外部署即可应对图像识别场景。
- 🖥️ **远程桌面 / 生产环境适配** — RDP 会话转移、分辨率恢复、随机延时、异常截图、视频录制调试，拿来即可上生产。
- 🔌 **可选硬件级键鼠模拟器** — 配套 USB HID 硬件（SKSimulator），在硬件层注入键鼠信号，输入等同真人，显著降低风控误判。
- 📋 **完整能力对照** — 看过社区版之后，可用 [WECHATAUTO.SDK 能力表](ability.md) 了解 VIP 增加的能力与支持。请注意：一键试用包 `community_allin.rar` 是限时可用的 VIP 体验包，不是社区开源版的构建。

# 🚀 开发模式选择
## 🏌️‍♂️ 两种开发模式，按需接入
| | 内嵌方式 | HTTP WebAPI 方式 |
|---|---|---|
| 适合 | .NET / Windows 原生应用，需要事件回调、低延迟、完整 API,深度集成在自己应用中 | 后端/云服务、跨语言、多系统共享、快速集成 |
| 入口 | `WeAutomation.Initialize(...)` → `WeChatClientFactory` → `WeChatClient` | 启动 Server，REST + Swagger，`http://<主机>:5000` |
| 特点 | 类型安全、能力最全、深度定制 | 语言无关、进程隔离、可远程、开箱即用,仅需要UI设置，不需要一行代码就能开启消息监听/自动通过加好友 |

👉 两者共享同一套核心能力（发送消息/文件/表情/语音、群操作、好友监听、朋友圈、导航等）；HTTP WebAPI 的每个接口注释里都标注了"对应 WeChatClient.xxx"。

!> 绝大多数场景推荐 HTTP WebAPI 方式；需要 dll/Nuget包、深度回调时选内嵌方式。

## 🧠 内嵌开发方式

👉 请参考 [内嵌开发方式指南](https://scottfly189.github.io/WeChatAuto.SDK/main.html#/inlineuse)

## 🌐 HTTP WebAPI开发方式

👉 请参考 [Http WebAPI开发方式指南](https://scottfly189.github.io/WeChatAuto.SDK/main.html#/httpserveruse)

