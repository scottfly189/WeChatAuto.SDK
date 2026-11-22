# HttpSupport 客户端 SDK（多语言）

本目录是 **OpenAPI Generator 7.27.0** 根据 `HttpSupport/Server/Docs/api-site/swagger.json` 生成的标准客户端：

- **115 个 HTTP 接口**
- **58 个 `components.schemas`**
- 按模块生成 API 类：`ClientsApi`、`ConversationsApi`、`DatabaseApi`、`FriendsApi`、`GroupsApi`、`MessagesApi`、`MomentsApi`、`NavigationApi`、`WindowApi`

> 本次已用标准 OpenAPI Generator 重新生成，**替换了之前手写的统一 `WeChatAutoClient` 封装**。方法名、返回类型和目录结构以各语言生成结果为准；旧的调用代码需要按新 API 调整。

## 公共约定

| 项目 | 值 |
| --- | --- |
| 默认 Base URL | `http://localhost:5000`（来自 `App.json` 的 `Http.Port`） |
| 鉴权 | 可选 `X-API-Key` 请求头（`App.json` 的 `Http.ApiKey`，为空时服务端不校验） |
| 接口前缀 | `/api/v1/...` |
| 字段命名 | 传输层保持 swagger 的 `snake_case` |
| 响应 | 以 swagger 中各接口的 schema 为准；生成器会按 schema 生成响应模型 |

## 各语言目录

| 语言 | 目录 | 入口 | 构建 / 测试 |
| --- | --- | --- | --- |
| .NET (C#) | [`.net/`](.net/) | `WeChatAuto.Client.sln` | `dotnet build .net/WeChatAuto.Client.sln` |
| C++ | [`c++/`](c++/) | `cpp-tiny` 生成（`lib/` + `src/`） | 见 `c++/README.md` |
| Go | [`go/`](go/) | 模块 `wechatauto` | `cd go && go test ./...` |
| Java | [`java/`](java/) | Maven / Gradle 工程 | `cd java && mvn test` |
| Node.js | [`node/`](node/) | `src/index.js` | `cd node && npm install && npm test` |
| Python | [`python/`](python/) | `wechatauto` 包 | `cd python && pip install -r requirements.txt && pytest` |
| Rust | [`rust/`](rust/) | `Cargo.toml` | `cd rust && cargo test` |

## 使用方式

每个语言目录的 `README.md` 都有完整的安装、初始化和示例。通用步骤：

1. 启动 HTTP Server（`Server.exe`，默认端口 `5000`）；
2. 用 `GET /api/v1/clients` 拿到 `clientId`；
3. 按模块调用生成的 API 类，例如发送文本：
   - C#：`MessagesApi.Send(...)`
   - Python：`MessagesApi(...).send(...)`
   - Node：`MessagesApi.send(...)`
   - 其余语言见各自 README。

## 重新生成

生成命令（在仓库根目录执行，需要 Node.js + Java）：

```bash
npx --yes @openapitools/openapi-generator-cli generate \
  -i HttpSupport/Server/Docs/api-site/swagger.json \
  -g python -o HttpSupport/Client/python \
  --additional-properties packageName=wechatauto
```

其他语言把 `-g` 换成对应 generator 即可：

- C#：`csharp`
- C++：`cpp-tiny`
- Go：`go`
- Java：`java`
- Node.js：`javascript`
- Rust：`rust`

> 生成器版本固定为 `7.27.0`；不同版本生成的文件结构可能不同。
