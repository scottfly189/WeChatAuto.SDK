# 两条路径：社区版 UIA 与 VIP Database Read

本 SDK 支持两条线路，同时存在，不是二选一：

- **社区版（UIA）**：用 Windows UI Automation 操作真实微信客户端。监听、发送、好友、群等基础能力走这条，社区开源版即可先上手（源码或 NuGet）。
- **VIP（Database Read）**：在社区版能力之上，把取消息主路改为只读本机微信库（先拷到 staging 再读），不 hook、不回写微信原库。发送、好友、群等必须「点」界面的能力，VIP 里仍然走 UIA。

Database Read、热同步、自动获取微信存储位置目前只在 VIP 中提供，社区开源仓尚未包含。

## 本机路径对照

|  | 截屏 Computer Use | 本机 UIA / 无障碍树 | Database Read（VIP 取消息主路） |
| --- | --- | --- | --- |
| 做法 | 对屏幕截图，并用键鼠操作界面 | 读取本机无障碍树（UI Tree），按客户端界面结构取数或操作。适合发送、好友、群、导航等必须「点」微信的能力，社区版可先走这条试用 | 只读本机微信数据库（拷到 staging 后再读），不 hook、不回写微信原库 |
| 公开讨论中常见情况 | 可能截不到微信画面，或出现被踢下线 | 依赖微信 PC 客户端以及 UI Tree 是否可用；有边界，不承诺官方授权。本 SDK 可在需要时强开 UI Tree（见 [issue#3](https://github.com/scottfly189/WeChatAuto.SDK/issues/3)） | 取消息不再依赖在界面上逐条抓取 |
| 在本产品中的位置 | 不是本 SDK 的主路径 | 社区版的主路径，也是 VIP 里发送、好友、群等操作仍在用的路径。两条线并存 | VIP 在社区版之上增加的更稳取消息主路。以前用 UIA 取消息又慢、又会占用键鼠，这条主路已不靠抓界面来取消息。不把「不抢键鼠」单独写成卖点 |

要 Database Read / 热同步 / 自动取微信存储位置，请使用 VIP。完整能力对照见 [WECHATAUTO.SDK 能力表](ability.md)。

请注意：GitHub Releases 中的 [community_allin.rar](https://github.com/scottfly189/WeChatAuto.SDK/releases/download/2.0.7/community_allin.rar) 是限时可用的 VIP 体验包，不是本仓库社区开源版的构建。日常开发请从本仓库源码或 NuGet 接入社区版。
