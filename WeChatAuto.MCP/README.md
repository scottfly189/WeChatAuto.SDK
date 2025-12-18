# 关于WeChatAuto.MCP MCP Server
WeChatAuto.MCP 是一个基于 WeChatAuto.SDK 的 MCP Server，用于微信自动化。

## 本地开发与调试

如需在本地不通过已编译的 MCP 服务包测试该 MCP Server，可以通过配置 IDE 直接运行源码。

```json
{
  "servers": {
    "WeChatAuto.MCP": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "<项目目录路径>"
      ]
    }
  }
}
```

## 测试 MCP Server

配置好后，你可以在 Copilot Chat 中通过人工智能自动化操作微信桌面客户端


## 从 NuGet.org 使用 MCP Server


- **VS Code**：在 `<工作区目录>/.vscode/mcp.json` 文件中配置
- **Visual Studio**：在 `<解决方案目录>\.mcp.json` 文件中配置

两者配置文件内容如下：

```json
{
  "servers": {
    "WeChatAuto.MCP": {
      "type": "stdio",
      "command": "dnx",
      "args": [
        "WeChatAuto.MCP",
        "--version",
        "1.0.2",
        "--yes"
      ]
    }
  }
}
```

