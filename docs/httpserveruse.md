!> 如果你不是 .NET、不想管理 SDK 生命周期，或需要跨语言/远程调用,请使用 [HTTP WebAPI 方式](https://scottfly189.github.io/WeChatAuto.SDK/main.html#/httpserveruse)，<strong>`不需要书写一行代码获取微信消息`</strong>，官方推荐!

# 一、HTTP WebAPI介绍

## 1. 适用场景
- 你的业务是后端服务 / 云服务 / 跨语言系统，需要从 Python、Go、Java、Node.js、Rust 等调用微信能力
- 需要多个业务系统共享同一个微信自动化实例，支持远程调用（局域网或云服务器），例如客服系统、运营系统、RPA、低代码平台、第三方系统对接。
- 团队/业务方不想碰 .NET，只想用 HTTP 快速集成。
- 需要开箱即用的 GUI 管理界面、Swagger 在线文档与调试、定时发送、好友自动通过等现成功能。
- 希望业务与自动化解耦：自动化作为独立服务运维，业务代码只依赖稳定的 HTTP 契约。


## 2. 优点
- 语言无关：任何能发 HTTP、解析 JSON 的语言都能用，无需依赖 .NET;
- 进程隔离：自动化跑在独立 Server 进程里，业务系统崩溃不影响微信自动化；SDK 升级只需替换 Server;
- 可远程 / 多端共享：监听所有网卡（0.0.0.0），本机、局域网、云服务器都能访问;
- 开箱即用：自带 GUI 管理界面、Swagger 文档（可在线试调）、定时任务、好友自动通过等成品功能;
- 标准化契约：REST + snake_case JSON + 错误码（RFC 7807），可用 openapi-generator / Kiota / openapi-python-client / progenitor 自动生成各语言客户端;
- 有鉴权：X-API-Key 请求头鉴权，配合 Nginx/Caddy 反向代理加 HTTPS 可安全上公网;
- 获取好友/群的消息、自动接入好友申请等操作不需要写一行代码，只要通过设置就可以实现，极大的降低集成工作量;

## 3. HTTP WebAPI接入原理

!> `WeChatAuto.SDK` 的 HTTP 服务启动后，即可获取微信消息，也可自动通过加好友申请。

<img src='./images/server_ui.png' width='900'/>

**一般开发微信能力集成在自己应用中的开发者有两个诉求:**

- 希望很方便的获取微信的好友/群的微信消息;
- 调用微信的能力，如：发送消息，转发消息，群管理等;

🏌️‍♂️ **WeChatAuto.SDK HTTP WebAPI 的使用方式如下:**

1. 启动 `HTTP服务` 后，通讯录、消息、群成员、朋友圈等会写入你配置的数据库。开发者直接查询数据库，就可以获取好友/群的消息，`无须开发任何一行代码`;
2. `HTTP服务` 只要正确开启，就会提供 `HTTP WebAPI` 服务。任何开发语言都可以通过发送 `Http` 请求调用微信的能力：如发送消息、进行群管理等;
3. 简言之：现在是 WeChatAuto.SDK 把微信数据送到你指定的数据库，你的 Bot 应用直接访问数据库即可；需要操作微信时，再调用 WebAPI。

多号默认写入同一套数据库，并用所属微信号区分来源。默认是本地 SQLite，也可以改成 MySQL 等你自己准备的库。

👉 数据表 `wechat_message` 字段如下（支持多微信消息聚合）:

| 字段名 | 说明 |
| --- | --- |
| `Id` | 中间表自增主键。不要用它做增量水位 |
| `FromWechat` | 这条消息属于哪个微信号（该账号的昵称） |
| `FromWechatWxId` | 这条消息属于哪个微信号（该账号的 wxid）。多号筛选和跨表关联请用这个 |
| `ChatUsername` | 会话标识：私聊是对方 wxid，群聊是群 id（如 `123@chatroom`）。不是窗口顶部的显示名 |
| `Shard` | 本地消息分片编号（0/1/2）。各分片里的 `LocalId` 都从 1 计，定位一条消息时必须带上本字段 |
| `LocalId` | 该会话在该分片内的本地消息编号。请按 64 位整数读写 |
| `ServerId` | 微信服务端消息 id，可能为 0。可作辅助查找，不是本表唯一键。请按 64 位整数读写 |
| `SortSeq` | 消息排序序号。查询聊天记录请按本字段或 `MsgCreateTime` 排序（从大到小即从新到旧）。请按 64 位整数读写 |
| `SenderWxId` | 发送者 wxid。会话和发送者的显示名，请用 `ChatUsername` / `SenderWxId` 关联 `wechat_contact`（备注优先，空则昵称） |
| `LocalType` | 微信原有的消息类型编码。部分类型带有子类型，数值会很大，请按 64 位整数读写。常见取值见下表 |
| `MessageType` | 消息类型名称，如文字、图片、语音、引用、文件。已核实的标签还包括：个人名片、视频、动画表情、位置、链接、语音通话、系统、撤回消息、拍一拍、聊天记录、红包，以及未单独列出时的「其他」。`LocalType` 为 0 时，本字段可能为空 |
| `Content` | 消息正文。引用消息只存回复文字，被引用内容在 `Quote*` 列；聊天记录的完整内容在 `RecordItem`。常见写法见下方 |
| `RecordItem` | 聊天记录消息的完整 XML。非聊天记录为空，内容可能较大 |
| `QuoteUsername` | 引用消息：被引用人的 username；群引用时可能是群 id |
| `QuoteTitle` | 引用消息：被引用人的显示名，可能为空 |
| `QuoteContent` | 引用消息：被引用内容（已转成可读文本） |
| `QuoteType` | 引用消息：被引用消息的类型编码，如 1/3/34/49/57 |
| `QuoteServerId` | 引用消息：被引用消息的 `server_id`，以文本存储，避免整数溢出 |
| `QuoteThumbUrl` | 引用消息：被引用链接的缩略图 URL |
| `QuoteVoiceLength` | 引用消息：被引用语音的时长（毫秒） |
| `MessageTime` | 展示用时间，精确到分钟。不要用它做排序水位 |
| `MsgCreateTime` | 消息在微信里实际发生的时间（Unix 秒）。聊天时间请用这个，不要用下面的 `CreateTime` |
| `FilePath` | 已同步到本地的图片、文件、视频、语音等路径，可能为空。文件和视频优先为微信原目录下已下载的绝对路径；图片和语音一般在本机媒体缓存目录。消息里出现的 `cdn*url`、`voiceurl` 一类标识不是可直接打开的网址，不会写入本列 |
| `Status` | 微信原有的发送状态码。曾观察到 3、4、2 等取值，具体含义尚未逐项核实 |
| `IsSelf` | 是否由自己发送 |
| `IsBotProcessed` | 是否已被 Bot 处理。由你的业务自行更新，同步不会覆盖 |
| `CreateTime` | 本条记录被写入数据库的时间，不是消息在微信里发生的时间 |

定位一条消息请用 `FromWechatWxId + ChatUsername + Shard + LocalId`。增量水位请用 `SortSeq`，不要用 `Id` 或 `CreateTime`。`Quote*` 各列只在引用消息中有值，其它消息为空。

`LocalType` 常见取值如下（未列出的编码，`MessageType` 可能为「其他」）:

| 取值 | 含义 |
| --- | --- |
| 1 | 文字（看低 32 位） |
| 3 | 图片 |
| 34 | 语音 |
| 42 | 个人名片 |
| 43 | 视频 |
| 47 | 动画表情 |
| 48 | 位置 |
| 49 | 链接（看低 32 位） |
| 50 | 语音通话 |
| 10000 | 系统消息 |
| 10002 | 撤回消息 |
| `21474836529` | 链接 |
| `244813135921` | 引用消息 |
| `25769803825` | 文件 |
| `266287972401` | 拍一拍 |
| `81604378673` | 聊天记录 |
| `8594229559345` | 红包 |

`Content` 常见写法：

- 图片 / 视频 / 语音：`[图片]` / `[视频]` / `[语音]`（本地文件看 `FilePath`）
- 文件：`[文件] 文件名`
- 表情：`[表情]描述`，没有描述时 `[表情]`
- 链接 / 小程序 / 视频号 / 音乐：`[链接] 标题` + 换行 + URL
- 聊天记录：预览文本（完整内容看 `RecordItem`）
- 位置：地点名称
- 系统消息：核心文案
- 拍一拍：`A 拍了拍 B ...`
- 引用消息：只放回复文字，被引用内容在 `Quote*` 列

请注意：表中已不再保留 `Who` / `Sender` 这类显示名列。同一套库里还有 `wechat_contact`、`wechat_chat_room_member` 以及朋友圈相关表，都按所属微信号区分，可直接查询。

## 4. 使用步骤

!> **注意**： HTTP服务需要先下载！ 👉 [下载](https://github.com/scottfly189/WeChatAuto.SDK/releases/download/2.0.7/community_allin.rar)

请注意：这个一键试用包 [community_allin.rar](https://github.com/scottfly189/WeChatAuto.SDK/releases/download/2.0.7/community_allin.rar) 是限时可用的 VIP 体验包，不是本仓库社区开源版的构建。日常开发请从本仓库源码或 NuGet 接入社区版。


#### 🚀请按下面的步骤使用:
- 请先下载[HTTP服务端](https://github.com/scottfly189/WeChatAuto.SDK/releases/download/2.0.7/community_allin.rar),放置在本机/服务器的某个目录，然后解压缩;
- HTTP服务要与需要自动化的微信放在同一台电脑上;
- 先打开微信，并登入自己账号，保持微信窗口为打开状态(非关闭状态),支持多微信; 
- 最后打开HTTP服务(运行解压缩目录的Server.exe);
- 双击或者右键屏幕右下角的HTTP服务，在弹出的控制台窗口中点击链接打开Swagger页面,在弹出的Swagger页面可以在线调试API;
- HTTP服务请参考:[HTTP服务使用指南](#http服务使用指南);
- HTTP WebAPI参考请参考:[HTTP WebAPI 接入指南](#http-webapi-接入指南);
- 其他问题请参见[FAQ](https://github.com/scottfly189/WeChatAuto.SDK/blob/master/docs/faq.md);
- 有什么问题或者功能增强要求，请提[Issues](https://github.com/scottfly189/WeChatAuto.SDK/issues);

!> 也可以浏览[在线Swagger](https://scottfly189.github.io/WeChatAuto.SDK/api-site/index.html),但请不要操作，因为服务器没有打开对应的微信,具体还是以你本地打开的HTTP WebAPI服务为准！

# 二、HTTP服务使用指南
!> 后面的WebAPI调用依赖打开`HTTP服务`, 请使用者在使用前阅读本 `HTTP服务使用指南`,重要！

**1. 请先下载[HTTP服务端](https://github.com/scottfly189/WeChatAuto.SDK/releases/download/2.0.7/community_allin.rar),放置在本机/服务器的某个目录，然后解压缩;**
**2. 在解压缩的目录中打开 `Server.exe` 应用，如下图所示:**

![打开server](./images/server_1_open.png)

双击`Server.exe`就可以运行本服务端

**3. 运行后，如果成功运行，会在右下角产生一个notifyicon图标，双击这个图标，可以看到主控台，如下图所示:**

![打开主控台](./images/server_2_main.png)

!> 这里是HTTP WebAPI的主控台，如果看到以上的界面，证明Http Server正常工作中！

- 点击`HTTP Server`右边的链接，可以打开SWagger页面，可以在弹出的Swagger页面查看API定义及进行在线测试;
- 下面的`功能显示区`会显示自动通过好友、定时发送等功能的状态;
- 如果运行失败，也会显示在主控台，你可以点击 `日志` 选项卡查看为什么失败;

**4. 在主控台点击 `日志` 选项卡，展示运行日志**

- 在日志显示区可以查看日志;
- 点击右边的 `复制` 按钮，可以把日志全部复制到剪切板，你可以把日志复制到外部保存;
- 点击右边的 `清除日志` 按钮，可以清除日志，方便调试;
- 日志不会自动刷新，请点击右边的 `刷新` 按钮刷新最新日志;

![日志](./images/server_3_log.png)

**5. 在每个窗口的底部，会产生一个吸附窗口,会随着窗口移动而移动，如下图所示:**

![吸附窗口](./images/server_4_main.png)

!> 吸附窗口与每个微信相关，当某微信获得焦点时会显示，失去焦点时会消失，把鼠标移到每个按钮上面会显示每个按钮的 `tooltips`;

- `查看数据`：打开数据查看窗口，可查看消息、群成员、朋友圈和联系人;
- `配置`：设置消息写入的数据库。所有微信号共用同一个数据库，多号数据会自动聚合，并用所属微信号区分;
- `测试集`：发送消息、定时发送、设置演示、自动接入好友等现成功能。

启动服务并登录微信后，数据会写入你配置的数据库，不必再选择监听方式，也不必在微信窗口里点开会话来抓取消息。

**6. 点击吸附窗口的 `配置` 按钮，打开数据库配置界面，如下图所示:**

![数据库配置](./images/server_4_database_config.png)

!> 中间库是全局唯一连接，所有微信号共用同一个数据库。默认是本地 `sqlite`，你可以改成自己的数据库，如 `mysql` 等。

- 在数据库类型列表选择所需要的数据库类型;
- 在连接字符串填写上面选择的数据库类型正确的连接字符串;
- 点击 `测试连接` 可以测试数据库的连通性;
- 点击 `保存配置` 后，需要重启应用才会切换到新库；重启时会先把旧库数据迁移到新库。

**7. 点击吸附窗口的 `查看数据`，如下图所示:**

![数据库查看](./images/server_4_database_view.png)

- 窗口里有 `消息查看`、`群管理`、`朋友圈`、`联系人` 四个页签;
- 在 `消息查看` 中选择 `好友/群`，可以查看对应会话的消息记录;
- 也可以点击右边的 `刷新`、`导出` 按钮，查看或导出数据;

<details>
<summary>扩展学习：测试集</summary>

1. 发送消息: 可以测试发送消息，也可以保存昵称集合，方便下次发送消息的时候可以快捷选择;

2. 定时发送: 可以设定定时任务，给多人发送一条或者多条消息;

3. 设置演示: 用于配置演示相关选项;

4. 自动接入好友申请: 通过设定，有好友申请加你好友时，会自动通过，并可以根据每个关键词设定单独的标签与备注后缀;

!> 注： 测试集的一些设定会反馈在`控制台`的功能区中;

</details>

<br/>

# 三、HTTP WebAPI 接入指南

> 本服务是标准 HTTP RESTful 接口：任何能发 HTTP 请求、解析 JSON 的语言（.NET / Python / Rust / Go / Java / Node.js 等）都能直接调用，**无需依赖 .NET**。
>
> 每个接口的完整参数与返回结构，以运行中的 **Swagger UI** 为准：启动服务后访问 `http://<主机>:<端口>/swagger`。本文只讲「概念 + 约定 + 快速上手」——这些是 Swagger 页面本身不会教你的。

---

## 1. 配置文件

<img src='./images/appconfig.png' width='1200'/>

> 根目录的App.json为配置文件，可以在这里更改HTTP监听端口、设置鉴权的Key等

## 2. 接入信息

| 项 | 说明 |
|---|---|
| Base URL | `http://<主机IP>:<端口>` |
| 默认端口 | `5000`，在程序运行目录的 `App.json` 中改（`Http.Port`） |
| 传输 | 明文 HTTP（无 TLS）。服务监听所有网卡（`0.0.0.0`），本机、局域网、云服务器均可访问 |
| Swagger 文档 | `http://<主机IP>:<端口>/swagger` |

> 部署到公网/云服务器时，强烈建议通过反向代理（Nginx / Caddy）加 HTTPS，并设置 API Key（见下一节）。

## 3. 鉴权（API Key）

- 请求头：`X-API-Key: <你的Key>`
- 配置位置：`App.json` → `Http.ApiKey`
- 规则：
  - `ApiKey` 为**空**（默认）→ **不校验**，任何能访问到端口的人都能调用（适合纯内网/单机）。
  - 设置后 → 所有 `/api/**` 请求必须携带且匹配该 Header，否则返回 `401`。

```json
{
  "Http": {
    "Port": 5000,
    "ApiKey": "改成足够随机的长字符串"
  }
}
```

> 浏览 Swagger 文档（`/swagger`）**不需要** API Key；只有实际调用 `/api/**` 才需要。

## 4. 全局约定

| 项 | 约定 |
|---|---|
| 请求/响应体 | 都是 `application/json`，请求头带 `Content-Type: application/json` |
| 字段命名 | **snake_case**（小写 + 下划线），如 `at_users`、`wx_id`、`group_name`。输入大小写不敏感，输出一律 snake_case |
| 枚举 | 一律传**字符串值**（不是数字） |
| 日期时间 | ISO 8601，如 `2026-10-19` 或 `2026-10-19T14:30:00`；未写时区时按服务运行机器的本地时间解释 |
| 空返回的操作 | 写操作（POST / PUT / DELETE）成功通常返回 `204 No Content`，**没有响应体**，不要去找 body |

## 5. 状态码与错误

| 状态码 | 含义 |
|---|---|
| `200` | 成功，有返回体（JSON） |
| `204` | 成功，无返回体 |
| `400` | 参数错误 |
| `401` | API Key 缺失或不正确（仅当配置了 ApiKey） |
| `404` | `clientId` 对应的微信客户端不存在 |
| `500` | 服务器内部错误 |

错误响应体遵循 RFC 7807（`ProblemDetails`）：

```json
{ "status": 404, "title": "微信客户端不存在", "detail": "..." }
```

## 6 核心概念（务必先读）

### 6.1 `clientId` 是什么

几乎所有接口路径都是 `api/v1/clients/{clientId}/...`。`clientId` **同时接受微信昵称或 wxid**，且**区分大小写、精确匹配**。

- 先调用 `GET /api/v1/clients` 拿到所有客户端的 `wx_id`；
- **推荐用 `wx_id`**：唯一且稳定；昵称可能重复、可能被用户改掉。

### 6.2 `who` / `group_name` 为空 = 当前焦点窗口

发消息、群操作等接口里的 `who`（或 `group_name`）字段**可留空**，留空表示作用于**当前焦点所在的聊天窗口**。这是有状态的行为，调用前请确保焦点在目标会话上（可配合 `POST .../conversations/locate` 先定位会话）。

### 6.3 副作用与微信风控

发送、好友、群等操作作用在本机正在运行的微信客户端上，不是幂等的纯 CRUD：

- 加好友、邀请群成员、把群成员加为好友等操作存在微信风控，**控制频率、谨慎批量**；
- 发送语音消息需要虚拟声卡（Cable input / Cable output）支持。

## 7. 快速上手（端到端）

三步：列客户端 → 取 clientId → 发消息。

> 若你的语言有对应的官方客户端 SDK，推荐直接使用第 7 节的封装库，无需手写 HTTP 请求。下面的原始 HTTP 示例用于理解协议，或你的语言没有对应 SDK 时使用。

### curl

```bash
# 1. 列出所有微信客户端
curl http://localhost:5000/api/v1/clients

# 2. 发一条文本消息（<wxid> 换成上一步的 wx_id；who 留空则发给当前焦点窗口）
curl -X POST http://localhost:5000/api/v1/clients/<wxid>/messages/send \
  -H "Content-Type: application/json" \
  -H "X-API-Key: your-key" \
  -d '{"who": "某个好友昵称", "message": "你好"}'
```

### Python

```python
import requests

BASE = "http://localhost:5000"
HEADERS = {"X-API-Key": "your-key"}   # 未配置 ApiKey 可省略

# 1. 列出客户端，取第一个的 wx_id
clients = requests.get(f"{BASE}/api/v1/clients", headers=HEADERS).json()
client_id = clients[0]["wx_id"]

# 2. 发消息
r = requests.post(
    f"{BASE}/api/v1/clients/{client_id}/messages/send",
    headers=HEADERS,
    json={"who": "某个好友昵称", "message": "你好"},
)
assert r.status_code == 204   # 写操作成功是 204，无返回体
```

### .NET

```csharp
using var http = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
http.DefaultRequestHeaders.Add("X-API-Key", "your-key");   // 未配置 ApiKey 可省略

// 1. 列出客户端
var clients = await http.GetFromJsonAsync<JsonElement>("/api/v1/clients");
var clientId = clients[0].GetProperty("wx_id").GetString();

// 2. 发消息
using var resp = await http.PostAsJsonAsync(
    $"/api/v1/clients/{clientId}/messages/send",
    new { who = "某个好友昵称", message = "你好" });
// resp.StatusCode == HttpStatusCode.NoContent
```

### Rust

```rust
use serde_json::json;

let client = reqwest::Client::new();

// 1. 列出客户端
let clients: serde_json::Value = client
    .get("http://localhost:5000/api/v1/clients")
    .header("X-API-Key", "your-key")   // 未配置 ApiKey 可省略
    .send().await?
    .json().await?;
let client_id = clients[0]["wx_id"].as_str().unwrap();

// 2. 发消息
client
    .post(format!("http://localhost:5000/api/v1/clients/{client_id}/messages/send"))
    .header("X-API-Key", "your-key")
    .json(&json!({ "who": "某个好友昵称", "message": "你好" }))
    .send().await?;
```

## 8. 官方客户端 SDK（多语言）

!>不想手写 HTTP 请求的话，仓库已提供 **7 个语言**的客户端封装、由 `swagger.json` 自动生成的官方客户端 SDK，位于 [HttpSupport/Client](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/WeChatAuto4_X/HttpSupport/Client)中。

统一约定：

| 项 | 约定 |
|---|---|
| 默认 Base URL | `http://localhost:5000`（来自 `App.json` → `Http.Port`） |
| 鉴权 | 可选 `X-API-Key` 请求头（`App.json` → `Http.ApiKey`，为空时服务端不校验、客户端也不发送） |
| 接口前缀 | 客户端自动为所有请求加 `/api/v1` |
| 字段命名 | `snake_case`，严格保持 swagger 原样 |
| 响应 | swagger 中响应无 schema（仅 `200 OK`），各客户端统一返回**原始响应体**，不构造响应 DTO |
| 接口规模 | 65 个操作（7 组）、42 个数据模型/枚举 |

> ⚠️ 这些客户端由 `swagger.json` 自动生成，**未经充分测试**；接口的准确行为以本指南与 `HttpSupport/Server/Docs/api-site/swagger.json` 为准。

| 语言 | 目录 | 构建 / 测试 | 说明 |
|---|---|---|---|
| .NET (C#) | `HttpSupport/Client/.net/` | `dotnet build` / `dotnet test` | net8.0 类库，入口 `WeChatAutoClient` |
| C++ | `HttpSupport/Client/c++/` | `cmake` / `ctest` | C++17 header-only，可选 libcurl |
| Go | `HttpSupport/Client/go/` | `go test ./...` | 仅标准库，入口 `NewClient` |
| Java | `HttpSupport/Client/java/` | `mvn test` | JDK 11+ + Maven + Jackson |
| Node.js | `HttpSupport/Client/node/` | `npm test` | ES modules，Node 18+，零运行时依赖 |
| Python | `HttpSupport/Client/python/` | `python -m pytest tests/` | 仅依赖 `requests` |
| Rust | `HttpSupport/Client/rust/` | `cargo test` | `tokio` + `serde_json` |

### 快速上手：发送一条文本消息

各语言构造方式一致：传入 `baseUrl`（默认 `http://localhost:5000`）与可选 `apiKey`；方法名与接口一一对应，路径参数按位置传入。

**C#**

```csharp
using WeChatAuto.Client;
using WeChatAuto.Client.Models;

var client = new WeChatAutoClient("http://localhost:5000", "your-api-key");
await client.SendMessageAsync("wxid_xxx", new SendMessageRequest { Who = "某好友", Message = "你好" });
```

**Python**

```python
from wechatauto import WeChatAutoClient, models

client = WeChatAutoClient(base_url="http://localhost:5000", api_key="")
client.send_message("wxid_xxx", models.SendMessageRequest(who="某好友", message="你好"))
```

**Go**

```go
import wechatauto "wechatauto/client"

c := wechatauto.NewClient("http://localhost:5000", "", nil)
who, msg := "某好友", "你好"
body, err := c.SendMessage("wxid_xxx", wechatauto.SendMessageRequest{Who: &who, Message: &msg})
```

**Java**

```java
WeChatAutoClient client = new WeChatAutoClient("http://localhost:5000", "your-api-key");
SendMessageRequest req = new SendMessageRequest();
req.setWho("某好友");
req.setMessage("你好");
String rawJson = client.sendMessage("wxid_xxx", req);
```

**Node.js**

```js
import { WeChatAutoClient } from 'wechatauto-client';

const client = new WeChatAutoClient('http://localhost:5000', 'your-api-key');
await client.sendMessage('wxid_xxx', { who: '某好友', message: '你好' });
```

**Rust**

```rust
use wechatauto_client::models::{ChatRefer, ChatSimpleMessage, SendMessageRequest};
use wechatauto_client::WeChatAutoClient;

let client = WeChatAutoClient::new("http://localhost:5000");
let body = SendMessageRequest {
    who: Some("某好友".to_string()),
    message: Some("你好".to_string()),
    at_users: None,
    // 注意：生成模型里 refer 为必填字段，需构造一个空的引用对象
    refer: ChatRefer {
        date: String::new(),
        message: ChatSimpleMessage { who: None, message: None, send_date_time: None, date_time: String::new(), unique_string: None },
        is_close_search_win: false,
    },
};
client.send_message("wxid_xxx", &body).await?;
```

**C++**

```cpp
#include <wechatauto/wechatauto_client.hpp>

wechatauto::WeChatAutoClient client("http://localhost:5000", "your-api-key");
wechatauto::SendMessageRequest req;
req.who = "某好友";
req.message = "你好";
std::string result = client.sendMessage("wxid_xxx", req);
```

> 每个语言目录内都自带 README 与单元测试；测试通过 mock/fake 传输层离线验证 URL、查询参数、`X-API-Key` 头与 JSON 请求体。更多细节见 `HttpSupport/Client/README.md` 及各语言目录下的 README。

## 9. 接口参考

完整接口清单（客户端、消息、好友、群聊、会话、导航、窗口）见 Swagger UI：

`http://<主机IP>:<端口>/swagger`

该页面可直接在线试调（先点右上角 **Authorize** 填入 API Key）。

## 10. 常见问题

- **404 微信客户端不存在**：`clientId` 填错，或大小写/精确匹配不符——先用 `GET /api/v1/clients` 取准确的 `wx_id`。
- **返回 204 没有 body，是不是失败了**：不是，写操作成功就是 204。
- **有现成的客户端 SDK 吗**：有。仓库已提供 7 个语言（.NET / C++ / Go / Java / Node.js / Python / Rust）的官方客户端 SDK（见第 7 节），位于 `HttpSupport/Client/`。想自己生成或需要其他语言，仍可把 `http://<主机>:<端口>/swagger/v1/swagger.json` 交给 openapi-generator / Kiota(.NET) / openapi-python-client(Python) / progenitor(Rust) 自动生成。
