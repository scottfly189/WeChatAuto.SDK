# send-wechat-message（Claude Code Skill）

一个可公开分享的 Claude Code Skill：通过 [WeChatAuto.SDK](https://github.com/scottfly189/WeChatAuto.SDK)（UI Automation，非内存注入、非破译数据库）驱动 Windows 本机微信 4.x 客户端，向指定好友或群聊发送文本消息。

发送纯文本消息走 UIA 搜索 + 文本输入，**不需要 OCR 模型**，开箱即用。

## 目录结构

```
send-wechat-message/
├── SKILL.md      # Claude Code Skill 定义（frontmatter + 调用指引）
├── README.md     # 本文档
└── sender/       # 发送器命令行小工具（引用 NuGet 包 WeChatAuto4x.SDK）
    ├── SendWeChat.csproj
    └── Program.cs
```

## 依赖与前置条件

- Windows + .NET 10 SDK（`dotnet --version` 应为 10.x）
- 本机已登录并打开微信 4.x，窗口处于显示状态（不能关闭、隐藏微信窗口）

## 安装（供其他人使用）

把本目录复制到 Claude Code 的 skills 目录之一：

- 用户级（对当前用户全局生效）：
  ```
  ~/.claude/skills/send-wechat-message/
  ```
- 项目级（仅当前项目）：
  ```
  <项目>/.claude/skills/send-wechat-message/
  ```

安装后在 Claude Code 里直接说「用微信给小明发一条消息：会议改到 3 点」即可触发。

## 手动使用（不依赖 Claude Code）

```bash
# 构建
dotnet build sender/SendWeChat.csproj

# 列出已登录的微信账号
dotnet run --project sender -- --list

# 发送：<账号昵称> <好友/群聊名> <内容>
dotnet run --project sender -- Alex 小明 "会议改到 3 点"
```

## 开源发布

本 skill 已随 [WeChatAuto.SDK](https://github.com/scottfly189/WeChatAuto.SDK) 仓库开源，把 `skills/` 目录提交并推送到 `master` 即对外可见。三种发布方式：

1. **随本仓库开源**（最简单）：合并 `skills/` 到 `master`，README/文档里给出上面「安装」指引。
2. **独立仓库**：把 `send-wechat-message/` 单独推成一个 repo，其他人 `git clone` 后复制到 `~/.claude/skills/`。
3. **Claude Code 插件市场**（推荐给最终用户）：在独立 repo 根加 `.claude-plugin/plugin.json` 声明本 skill，用户可用 `/plugin marketplace add <owner>/<repo>` 一键安装。

## 许可

与 WeChatAuto.SDK 相同（MIT）。
