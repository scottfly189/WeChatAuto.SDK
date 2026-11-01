---
name: send-wechat-message
description: 通过 WeChatAuto.SDK（UI Automation，非注入/非破译）驱动 Windows 本机已登录的微信 4.x 客户端，向指定好友或群聊发送文本消息。当用户要求「发微信 / 给某人发消息 / 微信通知 / 群发」时使用。
---

# 发送微信消息（WeChatAuto.SDK）

驱动本机微信 4.x 客户端发送文本消息。实现载体是本目录下的命令行小工具 `sender/`，它引用 NuGet 包 `WeChatAuto4x.SDK`，**不依赖本仓库源码**，可单独运行。发送纯文本走 UIA 搜索 + 文本输入，**不需要 OCR 模型**。

## 前置条件

1. **微信已登录并打开**：Windows 上运行微信 4.x，窗口处于显示（非关闭，可以最小化）状态。UI Automation 无法驱动关闭的窗口。
2. **.NET 10 SDK** 已安装（`dotnet --version` 应为 10.x）。

## 调用步骤

`sender/` 位于本 SKILL.md 同目录下。

1. 首次使用先构建：
   ```bash
   dotnet build skills/send-wechat-message/sender/SendWeChat.csproj
   ```
2. 列出当前登录的微信账号（注意是「账号本人昵称」，不是好友名）：
   ```bash
   dotnet run --project skills/send-wechat-message/sender -- --list
   ```
3. 发送消息：
   ```bash
   dotnet run --project skills/send-wechat-message/sender -- <微信账号昵称> <好友/群聊名> "<消息内容>"
   ```

> 以上路径以本仓库根目录 `src/WeChatAuto4_X/` 为基准。若 skill 被安装到别处（如 `~/.claude/skills/send-wechat-message/`），把 `skills/...` 换成实际安装路径即可。

## 参数说明

| 参数 | 含义 |
| --- | --- |
| `<微信账号昵称>` | 本机微信账号本人的昵称，用 `--list` 查看 |
| `<好友/群聊名>` | 会话列表里肉眼可见的标题（好友备注/昵称，或群名）；`-` 表示当前聊天窗口 |
| `<消息内容>` | 要发送的文本 |

## 示例

```bash
dotnet run --project skills/send-wechat-message/sender -- Alex 小明 "会议改到 3 点"
dotnet run --project skills/send-wechat-message/sender -- Alex 项目组 "今晚发布"
dotnet run --project skills/send-wechat-message/sender -- --list
```

## 排查

- 报「微信客户端不存在 / 未打开」：确认微信已登录且未最小化；用 `--list` 核对昵称拼写。
- 报「不支持 UI Tree」：`sender` 已默认 `IsForceOpenUITree = true`，若仍失败参考 https://github.com/scottfly189/WeChatAuto.SDK/issues/3 。
- 未报错但消息没发出：检查目标会话是否被正确聚焦，必要时先在微信里手动点开该会话再重试。
