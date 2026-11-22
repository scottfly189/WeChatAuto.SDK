!> 内嵌方式（Embedded）即：在你的 .NET 应用里直接引用 **NuGet 包 / DLL**，在**自己的进程内**调用 SDK 的 API。类型安全、能力最全、可深度定制，尤其适合需要**事件回调、低延迟、把微信能力深度集成进自己业务**的场景。本文档覆盖社区版 + VIP 版全部 API，标 💎 的为 **VIP 专有能力**。

> 如果你不是 .NET、不想管理 SDK 生命周期，或需要跨语言/远程调用，请改用 [HTTP WebAPI 方式](https://scottfly189.github.io/WeChatAuto.SDK/main.html#/httpserveruse)，官方推荐。

---

## 一、适用场景（先选对方式）

| | 内嵌方式（本文档） | HTTP WebAPI 方式 |
|---|---|---|
| 适合 | .NET / Windows 原生应用，需要事件回调、低延迟、完整 API,深度将SDK集成在自己应用中 | 后端/云服务、跨语言、多系统共享、快速集成 |
| 入口 | `WeAutomation.Initialize(...)` → `WeChatClientFactory` → `WeChatClient` | 启动 Server，REST + Swagger |
| 特点 | 类型安全、能力最全、深度定制 | 语言无关、进程隔离、可远程、开箱即用 |

!> 绝大多数场景推荐 HTTP WebAPI 方式；需要 DLL/NuGet 包、深度回调（消息监听、好友监听）、或要把 SDK 编译进自己应用时，选内嵌方式。

---

## 二、环境要求与准备

### 2.1 基础要求

- **操作系统**：仅支持 **Windows**（Win10 及以上），不支持 Linux / macOS；
- **.NET**：`net6.0-windows` / `net7.0-windows` / `net8.0-windows` / `net9.0-windows` / `net10.0-windows` 任选其一；
- **路径说明**：界面操作走 UI Automation；VIP 取消息主路为 Database Read（只读、不 hook、不回写原库）。两条线并存，详见 [两条路径](./versions.md)。
- **微信状态**：先打开微信并登录，**保持微信窗口为打开（非关闭）状态**，支持多开多个微信。

### 2.2 UI Tree（最关键的前置条件）

SDK 的界面操作通过 Windows UI Automation（FlaUI/UIA3）驱动微信，前提是微信暴露 **UI Tree**。这是内嵌/界面操作路径；VIP 取消息主路见 [两条路径](./versions.md)，两条线并存。

- 如果当前微信没有 UI Tree，SDK 支持**强开 UI Tree**：初始化时设置 `IsForceOpenUITree = true`；
- 参考 [如果没有 UI Tree 怎么办](https://github.com/scottfly189/WeChatAuto.SDK/issues/3)；
- 用 `inspect.exe` / `FlaUInspect` 等工具可查看 UI Tree（[工具下载](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/Tools)）。

### 2.3 OCR 模型（部分能力必需）

若启用 OCR（`EnableOCR = true`），需把 OCR 模型文件 `models` 文件夹放到**程序运行目录**下：

- 开发环境（VS 运行）：`bin\Debug\netx.x-windows\models`
- 模型文件下载：[Tools/models](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/Tools)

> 部分能力（如消息监听中的昵称识别）依赖 OCR，建议直接开启,但现在获取微信消息官方建议走Database read路线，所以此约定基本上过时了。

### 2.4 虚拟声卡（仅发微信语音消息需要）

`SendVoiceMessage` / TTS 语音依赖虚拟声卡 **Cable input / Cable output**：

- 在 `声音 → 设置 → 输入设备` 改成 `Cable output`；
- 未安装请在 [VBCABLE_Driver_Pack45.zip](https://github.com/alexzhao189/wechatautosdk/blob/main/Resources/VBCABLE_Driver_Pack45.zip) 下载。
- 如果需要发送微信语音消息的能力，请务必安装虚拟声卡并设置好输入输出设备;

### 2.5 技术基础知识准备

- 要想熟悉调用/看懂SDK的源码，最好能熟悉一下.net的`OneOf`库的使用，这个类似`Typescript`的union类型;
- SDK使用`MessagePack`做为缓存，并且也使用了`SqlSugarCore`以支持各种数据库，如果有条件，也请先了解;
- 如果你能深入`FlaUI`,就能为社区做代码贡献啦！

---

## 三、安装

### 3.1 通过 NuGet 安装

```bash
dotnet add package WeChatAuto4x.SDK
```

或使用 Visual Studio / Rider 的 NuGet 包管理器搜索 `WeChatAuto4x.SDK`。

### 3.2 修改目标框架

SDK 面向 Windows 目标框架，把 `.csproj` 的 `TargetFramework` 改成带 `-windows` 后缀：

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

### 3.3 直接引用 DLL（可选）

也可以不通过 NuGet，直接把编译产物 `WeChatAuto.dll`（及依赖）拷贝到项目并添加引用。

---

## 四、快速开始（5 分钟跑通）

!> 一个最小可运行示例：初始化 → 拿客户端 → 发一条消息,最小示例为一个控制台.net10应用，首先保证电脑安装有.net10的环境

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

5. 在 Program.cs 中编写如下代码：

```csharp
using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;

// 1. 初始化（内部依赖注入，适用于普通控制台程序）
var serviceProvider = WeAutomation.Initialize(options =>
{
    options.IsForceOpenUITree = true; // 微信未公开 UI Tree 时自动强开；确认已开启可设为 false
    options.DebugMode = false;        // 生产环境建议关闭（开启会在获得焦点时高亮边框）
});

// 2. 获取客户端工厂（单例，务必记得释放）
using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();

// 3. 查看电脑上打开了几个微信
var names = clientFactory.GetWeChatClientNames();
Console.WriteLine($"检测到 {names.Count} 个微信：{string.Join(", ", names)}");

// 4. 拿到第一个微信客户端
var client = clientFactory.GetWeChatClient(names.First());

// 5. 给好友发消息（把 "某好友" 换成你自己的好友 / 群聊昵称）
await client.SendMessage("某好友", "hello world!!");
```

6. 编译、运行

```bash
dotnet build
dotnet run
```

示例库说明:

- [FirstDemo](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/WeChatAuto4_X/Examples/FirstDemo): 一个控制台应用，用来给好友发送微信消息;
- [WinForm_Demo](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/WeChatAuto4_X/Examples/WinForm_Demo): 一个Winform接入应用，开发者注意学习如何在Winform中管理`clientFactory`的生命周期;


!> 运行前提：微信已登录且窗口可见；若报 `WechatNotOpenedException` / `WechatNotSupportUITreeException`，请回到 [2.2 UI Tree](#22-ui-tree最关键的前置条件) 排查。

---

## 五、核心概念（务必先读）

### 5.1 对象模型

```text
WeAutomation.Initialize(...)        // 初始化入口（全局唯一）
        └── WeChatClientFactory     // 多微信实例管理（单例，IDisposable）
                └── WeChatClient    // 单个微信客户端（IDisposable）
                        ├── Conversations    会话列表
                        ├── ChatContent      聊天内容 / 发送
                        ├── AddressBookList  通讯录
                        ├── MessageMonitor   消息监听 💎
                        ├── NewFriendMonitor 新好友监听 💎
                        ├── Moments          朋友圈 💎
                        ├── OwnerGroup       自有群
                        ├── OuterGroup       外部群
                        ├── Search           搜索 / 加好友
                        ├── NotifyIcon       任务栏图标
                        └── CacheManager     好友缓存
```

大部分能力都直接挂在 `WeChatClient` 上，即 `client.SendMessage(...)`、`client.AddMessageListener(...)`，无需深入子对象。

### 5.2 `who` 命名约定（很重要）

- 几乎所有方法的 `who` 参数都用**肉眼可见的昵称 / 备注名**定位好友/群聊，**不是 wxid**；
- `who` **可以为空**，空表示作用于**当前焦点所在的聊天窗口**（有状态，调用前请确保焦点在目标会话上）；
- 昵称可能重复，`wxid` 永不重复：需要精确区分时，建议给好友设置备注，或使用缓存中基于 `wxid` 的查询方法（如 `GetFriendWithWxIDFromCache`）。

### 5.3 异步 + 单 UI 线程模型

- 所有 API 都是 `async Task`，请 `await`；
- SDK 内部所有 UI 动作串行跑在**一个专用线程** `wechatauto.sdk` 上，**监听与发送互不抢鼠标/窗口**——这是刻意设计，业务上多线程调用最终也会排队，无需手动加锁；
- 消息监听等回调会在后台多线程触发，但**真正操作 UI 仍要回到微信单线程执行**。

### 5.4 两种依赖注入模式（二选一，互斥）

| 模式 | 方法 | 返回值 | 适用 |
|---|---|---|---|
| 内部 DI | `WeAutomation.Initialize(Action<WeChatConfig>)` | `IServiceProvider` | 裸控制台 / 无 DI 框架 |
| 外部 DI | `WeAutomation.Initialize(IServiceCollection, Action<WeChatConfig>)` | `IServiceCollection` | ASP.NET Core / Generic Host 已有 DI |

!> 两种 `Initialize` **不能混用**，混用会抛 `InvalidOperationException`。宿主已有 DI 用外部重载；否则用内部重载。

---

## 六、初始化与配置

### 6.1 内部 DI（普通控制台）

```csharp
var serviceProvider = WeAutomation.Initialize(options =>
{
    options.EnableOCR = true;
    options.IsForceOpenUITree = true;
});

using var factory = serviceProvider.GetRequiredService<WeChatClientFactory>();
var client = factory.GetWeChatClient(factory.GetWeChatClientNames().First());
```

### 6.2 外部 DI（ASP.NET Core / Generic Host）

```csharp
var builder = WebApplication.CreateBuilder(args);

WeAutomation.Initialize(builder.Services, options =>
{
    options.EnableOCR = true;
});

var app = builder.Build();

app.MapGet("/send", async (string who, string message, IServiceProvider sp) =>
{
    var factory = sp.GetRequiredService<WeChatClientFactory>();
    var client = factory.GetWeChatClient(factory.GetWeChatClientNames().First());
    await client.SendMessage(who, message);
    return Results.Ok();
});

app.Run();
```

### 6.3 配置项说明（WeChatConfig.cs）

这一节把 `WeChatConfig` 里的字段一次讲全。你不需要一次记住，用到哪个查哪个。

表格里的默认值以**代码里的初始值**为准；`💎` 表示这是 VIP 版才用得到的能力，社区版保持默认即可，不需要改。

| 配置项 | 默认值 | 适用版本 | 说明 |
|---|---|---|---|
| `UseDatabaseRead` | `false` | 💎 VIP | 是否启用“数据库安全读取 / 热同步”。`false` 是纯 UI 自动化；`true` 才会读微信数据库、建中间表。只做发送、监听、界面操作时保持 `false` 即可。 |
| `WeChatStorageRoot` | 空 | 💎 VIP | 微信数据存储根目录，例如 `D:\xwechat_files`。只有 `UseDatabaseRead=true` 才用得到；留空会自动读取并缓存，多微信共用同一个根目录；显式配置且目录存在时以配置为准。 |
| `DbTppe` | `Sqlite` | 💎 VIP | 中间表数据库类型，对应 SqlSugar 的 `DbType`。注意代码里的字段名就是 `DbTppe`，不是 `DbType`，写代码时照抄，不要自己改。默认 SQLite，也可以换成 SQL Server、MySQL、PostgreSQL、Oracle 等。 |
| `ConnectionString` | `Data Source=wechat.db` | 💎 VIP | 中间表连接字符串，和 `DbTppe` 配套使用。默认是当前目录下的 SQLite 文件；换其他数据库时，按对应数据库的格式填写。 |
| `FirstRunDays` | `30` | 💎 VIP | 账号第一次同步时，往回补多少天的历史消息。`0` 表示首轮不补历史，只同步启动之后的新消息；负数按 `0` 处理，只记日志，不会报错。 |
| `HotSyncVssSource` | `true` | 💎 VIP | 是否用 VSS 卷快照作为热同步的只读源。开启后不用整库拷贝，按源库变化和最小间隔创建或复用快照；需要管理员权限和 VSS，失败时回退到 live（如果开启了 live）。 |
| `HotSyncLiveSource` | `true` | 💎 VIP | VSS 不可用或失败时，是否直接以只读方式打开微信源库。优先级低于 VSS；两个都开时先试 VSS，失败再 live；两个都关且 VSS 失败时会报错。 |
| `HotSyncFirstRunTextOnly` | `false` | 💎 VIP | 首轮只同步文字：历史的图片、文件、视频只保留 `[图片]`、`[文件]`、`[视频]` 占位，不做路径解析、解密和下载；增量轮次会恢复正常媒体处理。语音不受影响，仍由 `AsrIncludeFirstRound` 控制。 |
| `HotSyncCopyLog` | `false` | 💎 VIP | 是否把热同步的 VSS / live 源读取诊断日志写到独立文件。开启后写到 `%LOCALAPPDATA%\WeChatAuto\logs\hotsync-copy.log`，用来排查“几十 GB 的大库慢在哪个库、有没有反复重试”；对同步逻辑没有影响。 |
| `SnsFirstRunDays` | `0` | 💎 VIP | 朋友圈首次同步往回看多少天。**`0` 或负数表示全量回补**，和 `FirstRunDays` 的语义正好相反，不要混用。只影响还没有朋友圈水位的账号。 |
| `DownloadSnsImages` | `false` | 💎 VIP | 是否下载朋友圈图片 / 视频到本机。默认关闭；开启后由独立的旁路后台任务处理，不进入热同步主循环，只处理待下载媒体，成功或永久失败都会写状态。 |
| `HotSyncVssMinIntervalSeconds` | `2.0` | 💎 VIP | 两次新建 VSS 快照的最小间隔（秒）。源库没有变化时不会新建快照。 |
| `MediaCacheDir` | 空 | 💎 VIP | 消息媒体缓存根目录，存放图片、语音解密后的文件。为空时用 `%LOCALAPPDATA%\WeChatAuto\media-cache`；支持绝对路径和 `%ENV%` 环境变量。 |
| `AsrEnabled` | `false` | 💎 VIP | 是否启用本地语音转写：增量同步到的语音会在后台转成文字，回写到中间表的 `wechat_message.Content`。首轮默认不转写。模型需要自己下载并放到 `AsrModelDir`，SDK 不随包提供模型。 |
| `AsrIncludeFirstRound` | `false` | 💎 VIP | 首轮同步是否也做语音转写。默认只在增量轮次转写，避免首轮全量同步时和 ASR 抢 CPU；打开它只是性能取舍，不是功能差异。 |
| `AsrModelDir` | 空 | 💎 VIP | ASR 模型目录。启用语音转写时必填，目录下直接放 `model.int8.onnx`（或 `model.onnx`）和 `tokens.txt`；支持绝对路径和 `%ENV%`。 |
| `AsrModelDownloadUrl` | 默认 R2 地址 | 💎 VIP | 模型下载地址，只用于说明和日志提示，**不会自动下载**。换自建镜像时改这里，模型不可用的日志会带上这个地址，方便排查。 |
| `AsrModelName` | `sensevoice-small-int8` | 💎 VIP | 模型标识，写入日志和中间表，方便知道这条转写是哪个模型产生的。 |
| `AsrNumThreads` | `2` | 💎 VIP | ASR 在 onnxruntime 里的线程数。CPU 场景建议 1–4，不是越大越快。 |
| `AsrLanguage` | `auto` | 💎 VIP | 语种：`auto`（自动识别）、`zh`（中文）、`yue`（粤语）、`en`、`ja`、`ko`。 |
| `AsrUseItn` | `true` | 💎 VIP | 是否启用 SenseVoice 的逆文本归一化，把数字、日期等转成更自然的写法。 |
| `AsrContentPrefix` | 空 | 💎 VIP | 写入 `Content` 的前缀。默认只写转写文本；设成 `[语音] ` 可以保留“这是语音”的标记，同时让检索和机器人还能拿到文本。 |
| `AsrQueueCapacity` | `512` | 💎 VIP | ASR 后台队列容量。队列满时丢弃新任务，不会影响同步主链路。 |
| `AsrMaxSeconds` | `120` | 💎 VIP | 超过这个秒数的音频跳过转写。微信语音通常不到 60 秒，默认值已经够用。 |
| `SnsMediaDir` | 空 | 💎 VIP | 朋友圈下载媒体的根目录。为空时用 `%LOCALAPPDATA%\WeChatAuto\sns-media`；支持绝对路径和 `%ENV%`。 |
| `StartupSyncOverlayFactory` | 空 | 💎 VIP | “进入系统”启动同步轮的提示窗口工厂，只在这个启动轮调用，运行期增量不再提示。标了 `[JsonIgnore]`，**不参与 JSON 配置**，要在 `WeAutomation.Initialize` 的配置回调里设置。 |
| `IsForceOpenUITree` | `true` | 社区 + VIP | 微信没有公开 UI Tree 时，是否强开 UI Tree。默认开启；普通环境建议保持开启，关掉后很多界面操作会找不到控件。它只影响界面自动化，不参与数据库读取。 |
| `IsOptimizeMainWindowLayout` | `true` | 社区 + VIP | 启动时是否自动优化微信主窗口布局，默认优化；不想让 SDK 动窗口时可以关掉。 |
| `MonitorMessageMaxDownInterval` | `8` | 社区 + VIP | UI 自动化消息监听时向下滚动的次数。监听列表多建议 10–30，少建议 5–10，太大会拖慢监听。 |
| `MonitorNewFriendRequestInterval` | `20` | 社区 + VIP | 好友申请监听的间隔，单位秒。 |
| `MonitorInterval` | `3` | 社区 + VIP | 固定式消息监听的间隔，单位秒。注意：消息读取官方推荐以 Database Read 为主（💎 VIP）。 |
| `ConversationInterval` | `5` | 社区 + VIP | 会话列表鼠标滚动的行数。 |
| `MonentsScrollMaxStep` | `30` | 社区 + VIP | 滚动删除朋友圈内容时的最大滚动次数。朋友圈内容多时可以调大。 |
| `EnableOCR` | `false` | 社区 + VIP | 是否启用 OCR。部分功能受微信限制必须用 OCR；启用时要把模型放到 `models/` 目录，并配置下面几个模型路径。UI 自动化消息监听建议开启；数据库读取不需要 OCR。 |
| `OCRDetModelFilePath` | `PP-OCRv6_det_small.onnx` | 社区 + VIP | OCR 检测模型路径。 |
| `OCRClsModelFilePath` | `ch_ppocr_mobile_v2.0_cls_mobile.onnx` | 社区 + VIP | OCR 方向分类模型路径。 |
| `OCRRecModelFilePath` | `PP-OCRv6_rec_small.onnx` | 社区 + VIP | OCR 文字识别模型路径。 |
| `OCRDictModelFilePath` | `ppocrv6_dict.txt` | 社区 + VIP | OCR 字典文件路径。 |
| `DebugMode` | `false` | 社区 + VIP | 调试模式。开启后获得焦点时会高亮边框，方便定位当前操作的是哪个控件。 |
| `CaptureUIPath` | 当前目录下 `Captures` | 社区 + VIP | 出错时捕获 UI 截图的保存路径。 |
| `EnableRecordVideo` | `false` | 社区 + VIP | 是否启用视频录制。开启后依赖 ffmpeg，第一次使用可能会自动下载。 |
| `TargetVideoPath` | 当前目录下 `Videos` | 社区 + VIP | 录制视频的保存路径。 |
| `EnableMouseKeyboardSimulator` | `false` | 社区 + VIP | 是否启用硬件键鼠模拟器。启用后键鼠操作走模拟器，而不是 Windows 自动化；需要购买对应设备。 |
| `KMDeviceVID` | `0x1701` | 社区 + VIP | 键鼠模拟器设备 VID。 |
| `KMDevicePID` | `0x2612` | 社区 + VIP | 键鼠模拟器设备 PID。 |
| `KMVerifyUserData` | `4F6A21981BE675822DEE7B9BC39F3791` | 社区 + VIP | 键鼠模拟器校验数据。 |
| `KMOffsetOfClick` | `5` | 社区 + VIP | 点击偏移量，单位像素。实际点击位置会减去一个随机偏移，避免每次都点同一个点。 |
| `KMOutputStringType` | `0` | 社区 + VIP | 模拟器输出字符串的编码方式：`0` ANSI、`1` Unicode、`2` ANSI 更快但受输入法影响、`3` Unicode 更快但受输入法影响、`4` 剪贴板粘贴（长文本更快，且不受输入法影响）。 |
| `KMMouseMoveMode` | `0` | 社区 + VIP | 模拟器鼠标移动模式。 |
| `ProcessDpiAwareness` | `1` | 社区 + VIP | 进程 DPI 感知：`0` 不设置、`1` 系统 DPI、`2` 每显示器 DPI。如果使用 SDK 的应用已经设置过 DPI 感知，这个参数无效。 |
| `HistoryMessageOffset_X` | `77` | 社区 + VIP | 历史消息 X 偏移距离，界面自动化滚动和定位时用。 |
| `HistoryMessageOffset_Y` | `40` | 社区 + VIP | 历史消息 Y 偏移距离。 |
| `HistoryRetryNumber` | `6` | 社区 + VIP | 历史消息滚动时的重试次数。 |
| `AvatorToWeixinButtonOffsetY` | `40` | 社区 + VIP | 头像按钮距离微信按钮的 Y 轴偏移量。 |
| `MaxHistoryMessageFetchNumber` | `40` | 社区 + VIP | UI 自动化消息监听时，回调最多返回多少条历史消息。通常不需要读完整历史，默认 40 条够用；消息读取官方推荐以 Database Read 为主（💎 VIP）。 |

> 配置项名称就是 C# 字段名。若通过 JSON 配置，优先看字段上的 `[JsonPropertyName]`（例如 `first_run_days`、`hotsync_copy_log`）；没有标注的字段，按你们的 JSON 序列化命名规则来写，拿不准就用代码方式设置。`StartupSyncOverlayFactory` 标了 `[JsonIgnore]`，只能通过代码设置。

#### 重要配置项说明

##### `UseDatabaseRead`（默认 `false`，💎 VIP）

这是“要不要读微信数据库”的总开关，也是最容易影响使用方式的一项。

- 保持 `false`：SDK 只通过界面自动化操作微信，能发消息、监听、加好友、管理群；不读数据库，也不需要配置存储目录和数据库连接。
- 改成 `true`：才会启用 VIP 的数据库安全读取和热同步，把联系人、消息、群成员、朋友圈同步到中间表；这时才需要关心 `WeChatStorageRoot`、`DbTppe`、`ConnectionString` 这些配置。
- 如果你只是做一个“帮我发消息、盯新消息”的机器人，保持默认 `false` 就行；要做数据平台、历史查询、多账号聚合，再打开它。

##### `WeChatStorageRoot`（默认空，💎 VIP）

微信把聊天数据放在一个“存储位置”里，常见形式是 `D:\xwechat_files`。

- 一般不用填：留空时，SDK 会在第一个微信客户端启动、真正需要读数据库时，自动读一次微信的设置，然后把结果缓存到 `%LOCALAPPDATA%\WeChatAuto\wechatkey.dat`；之后启动直接读缓存，不会再操作微信设置窗口。
- 多个微信要填成同一个路径，微信自己也是这么设计的；不要一个号填一个路径。
- 如果你明确知道路径，也可以手动填；填了并且目录存在时，以你填的为准，同时会顺手写回缓存。
- 只有 `UseDatabaseRead=true` 时这项才有意义。

##### `DbTppe`（默认 `Sqlite`，💎 VIP）

这是中间表使用的数据库类型。

- 代码里的字段名就是 `DbTppe`，不是 `DbType`，这是一个历史拼写；写代码或配置时照抄，不要自己改成 `DbType`，否则编译不过。
- 默认 `Sqlite`，数据落在 `ConnectionString` 指定的 `.db` 文件里，适合单机、小规模、快速上手。
- 数据量大、要多系统共享时，可以换成 SQL Server、MySQL、PostgreSQL、Oracle 等；换之前先确认目标数据库已建好、账号有建表和写数据权限。
- 它只影响 VIP 的中间表和数据库读取，和社区版的界面自动化无关。

##### `ConnectionString`（默认 `Data Source=wechat.db`，💎 VIP）

这是中间表的连接字符串，和 `DbTppe` 必须配套。

- 默认是当前目录下的 SQLite 文件 `wechat.db`，不需要装数据库，适合先跑通。
- 换 SQL Server、MySQL、PostgreSQL 等时，按对应数据库的标准格式填写；不要只改 `DbTppe` 不改连接字符串，也不要只改连接字符串不改 `DbTppe`。
- 连接字符串里通常包含账号密码，注意不要把它提交到公开仓库或发给无关人员。

##### `FirstRunDays`（默认 `30`，💎 VIP）

账号第一次同步时，往回补多少天的历史消息。

- 默认补 30 天，适合大多数“先看到最近一个月”的场景。
- 设为 `0`：首轮不补历史，只同步从启动时刻开始的新消息；适合只想从今天开始接数据的业务。
- 设为负数：按 `0` 处理，只记一条日志，不会抛异常或中断启动。
- 注意：这是“消息”的语义；朋友圈的 `SnsFirstRunDays` 是另一套规则，`0` 表示全量回补，不要混用。

##### `HotSyncVssSource`（默认 `true`，💎 VIP）

热同步读取微信数据库时，是否使用 Windows 的 VSS 卷快照。

- VSS 可以理解成“给磁盘拍一个瞬间快照”，SDK 读快照，不直接读正在写的微信库，也不用把几十 GB 的库整个拷贝一份。
- 开启后需要管理员权限和 VSS 服务；每一轮会根据源库有没有变化、以及最小间隔，决定新建还是复用快照。
- 如果 VSS 不可用（权限、系统限制、杀毒软件拦截等），会回退到 `HotSyncLiveSource`；如果 live 也关了，就会报错。
- 大库、长期运行，建议保持开启；小库、VSS 经常失败的环境，可以关掉它、只开 live。

##### `HotSyncLiveSource`（默认 `true`，💎 VIP）

VSS 不可用或失败时，是否直接以只读方式打开微信源库。

- 它的优先级比 VSS 低：两个都开时，先试 VSS，失败才用 live。
- live 是“直接读微信正在用的库”，不需要 VSS 权限，但更依赖微信自己的写入节奏；适合作为兜底方案。
- 两个都关掉，又遇到 VSS 失败，热同步会报错；一般不建议两个都关。
- 常见组合：VSS 开 + live 开（默认，最稳）；VSS 关 + live 开（VSS 权限搞不定时）；VSS 开 + live 关（只允许快照，不允许直接读源库）。

##### `HotSyncFirstRunTextOnly`（默认 `false`，💎 VIP）

首轮同步时，是否只同步文字。

- 开启后，首轮遇到图片、文件、视频只写 `[图片]`、`[文件]`、`[视频]` 占位，不去解析路径、解密或下载；这样首轮会快很多。
- 增量轮次会恢复正常媒体处理，所以不用担心“开了就永远没有图片”。
- 语音不受这个开关影响，仍然由 `AsrIncludeFirstRound` 控制首轮要不要转写。
- 历史媒体很多、首轮想先快速把文字接进来时，可以打开；不着急、希望首轮就带媒体，就保持 `false`。

##### `HotSyncCopyLog`（默认 `false`，💎 VIP）

是否把热同步读取源库的诊断日志单独写一个文件。

- 开启后写到 `%LOCALAPPDATA%\WeChatAuto\logs\hotsync-copy.log`，每行会记录开始、进度、重试、完成、失败，以及是哪个库、多大、耗时多少。
- 它的用途是排查“几十 GB 的大库到底慢在哪个库、有没有反复重试”，对同步逻辑本身没有影响，也不会改数据。
- 平时可以关着；现场排查大库问题时再打开，排查完关掉即可。

##### `MediaCacheDir`（默认空，💎 VIP）

消息媒体缓存根目录，用来放图片、语音解密后的文件。

- 为空时用 `%LOCALAPPDATA%\WeChatAuto\media-cache`，一般不需要改。
- 支持绝对路径和 `%ENV%` 环境变量；C 盘空间紧张时，可以指到 D 盘等大容量磁盘。
- 它和 `SnsMediaDir` 不是一回事：`MediaCacheDir` 放聊天消息的媒体，`SnsMediaDir` 放朋友圈下载的媒体。
- 只有 VIP 的数据库读取 / 媒体同步会用到。

##### `IsForceOpenUITree`（默认 `true`，社区 + VIP）

微信默认不一定会把 UI Tree 暴露给外部程序；没有 UI Tree，界面自动化就很难找到控件。

- 开启后，SDK 会在启动阶段扫描 `weixin.dll` 里的门控字节并写入，让微信把 UI Tree 暴露出来；这是启动阶段的兼容处理，不参与业务数据读写。
- 默认是开启的；普通环境建议保持开启。关掉后，很多界面操作会找不到控件，除非你确认当前微信版本本身已经开放了 UI Tree。
- 它只影响界面自动化，和 `UseDatabaseRead`、数据库读取没有关系；数据库读取不需要强开 UI Tree。
- 如果企业电脑有杀毒软件、EDR 或强管控，写入 `weixin.dll` 可能被拦截；这时可以和 IT 确认后再决定是否开启，或者只用 VIP 的数据库读取能力。

---

## 七、API 使用指南

> 以下示例均假设已拿到 `client`（`WeChatClient`）与 `factory`（`WeChatClientFactory`），并按需引入命名空间：`WeChatAuto.Services`、`WeChatAuto.Components`、`WeChatAuto.Models`、`WeChatAuto.Options`、`WeAutoCommon.Enums`。社区版 / VIP 版能力差异见 [WECHATAUTO.SDK 能力表](./ability.md)。
>
> 7.12 是 VIP 的数据库读取（Database Read），社区版没有；7.3 里的“按 WxId 精准发送”也只在 VIP 版提供。

### 7.1 获取微信客户端

```csharp
// 列出所有已打开微信的昵称
var names = factory.GetWeChatClientNames();

// 按昵称取客户端（不存在会抛 WechatClientNotExistException）
var client = factory.GetWeChatClient("某昵称");

// 或一次性取全部（Dictionary<string, WeChatClient>，key 为昵称）
var all = factory.GetWeChatClientList();
```

### 7.2 个人信息与窗口

```csharp
// 本微信个人信息（昵称 / wxid / 头像路径）
var info = client.GetOwerInfo();
Console.WriteLine($"昵称={info.NickName}, wxid={info.WxId}");

// 窗口控制
await client.Focus();          // 使主窗口获得焦点
await client.Max();            // 最大化
await client.Restore();        // 还原
await client.Pinned();         // 置顶
await client.UnPinned();       // 取消置顶

// 句柄 / 进程 ID
nint hwnd = client.GetHandler();
nint pid  = client.GetProcessId();

// 打开 / 关闭子窗口（好友、群聊的聊天子窗口）
await client.OpenSubWin("某好友");
await client.CloseSubWin("某好友");

// 导航
await client.SwitchNavigation(NavigationType.朋友圈);

// 版本号 / 保存头像
string version = client.GetWechatVersion();
await client.SaveOwnerAvator(@"C:\temp\avatar.png");

// 关闭查询窗口 / 关闭导航窗口
await client.CloseSearchWindow("某好友");
await client.CloseNavWin(NavigationType.朋友圈);

// 打开 / 关闭本微信主窗口（同步写法和异步写法都有）
client.CloseWindow();
client.OpenWindow();
await client.CloseWindowAsync();
await client.OpenWindowAsync();

// 点击任务栏微信图标：索引从 1 开始，也可以按昵称
await client.ClickNotifyIcon(1);
await client.ClickNotifyIcon("某昵称");

// 把你自己的窗口移动到微信主窗口中间（例如你弹出的提示窗）
client.MoveWinToMainCenter(yourWindow);

// 💎 VIP：通过界面自动读取微信存储位置（一般不需要，SDK 会自动读并缓存）
var storageRoot = await client.GetWeChatStorageRootByAutomation();
```

> `CloseNavWin` 只支持聊天文件、朋友圈、视频号、看一看、搜一搜、小程序面板等通过导航栏打开的窗口；不是所有窗口都能关。

| 方法 | 说明 |
|---|---|
| `GetTitle()` | 取当前聊天窗口的标题对象 `HeaderInfo`，含标题、会话类型、群人数、企业微信公司名等；判断“当前是不是可聊天对象”时很有用 |
| `GetOnlyTitle()` | 只取当前聊天窗口的标题字符串，等价于 `GetTitle()` 再取 `Title` |
| `FocuseSenderInput()` | 点击当前聊天窗口的输入区域获取焦点；也可以用来取消系统消息提醒、关闭右侧栏 |
| `Dispose()` | 释放单个客户端；**不建议手动调用**，`WeChatClient` 由 `WeChatClientFactory` 统一创建和释放 |

### 7.3 发送消息

#### 发送文本

```csharp
// 给指定好友/群聊发文本
await client.SendMessage("某好友", "你好");

// who 传空 → 发给当前焦点窗口
await client.SendMessage("你好");

// @某人（群聊，支持多个）
await client.SendMessage("某群", "开会了", atUser: new[] { "张三", "李四" });

// 引用某条历史消息后回复
await client.SendMessage("某好友", "好的", refer: new ChatRefer
{
    Message = new ChatSimpleMessage { Who = "某好友", Message = "明天有空吗？" }
});
```

#### 发送文件 / 表情

```csharp
// 发文件（多个）
await client.SendFile("某好友", new[] { @"C:\report.pdf", @"C:\photo.jpg" });

// 发表情（按名称或索引）
await client.SendEmoji("某好友", "[微笑]");
await client.SendEmoji("某好友", 1);

```

<details>
<summary>👉 所有表情名称/索引列表</summary>

|索引(index)|描述 (desc)|表情名 (value)|
|:---:|:---:|:---:|
| 1 | 微笑 | [微笑] |
| 2 | 撇嘴 | [撇嘴] |
| 3 | 色色表情 | [色] |
| 4 | 发呆 | [发呆] |
| 5 | 得意 | [得意] |
| 6 | 流泪 | [流泪] |
| 7 | 害羞 | [害羞] |
| 8 | 闭嘴 | [闭嘴] |
| 9 | 睡 | [睡] |
| 10 | 大哭 | [大哭] |
| 11 | 尴尬 | [尴尬] |
| 12 | 发怒 | [发怒] |
| 13 | 调皮 | [调皮] |
| 14 | 呲牙 | [呲牙] |
| 15 | 惊讶 | [惊讶] |
| 16 | 难过 | [难过] |
| 17 | 囧 | [囧] |
| 18 | 抓狂 | [抓狂] |
| 19 | 吐 | [吐] |
| 20 | 偷笑 | [偷笑] |
| 21 | 愉快 | [愉快] |
| 22 | 白眼 | [白眼] |
| 23 | 傲慢 | [傲慢] |
| 24 | 困 | [困] |
| 25 | 惊恐 | [惊恐] |
| 26 | 憨笑 | [憨笑] |
| 27 | 悠闲 | [悠闲] |
| 28 | 咒骂 | [咒骂] |
| 29 | 疑问 | [疑问] |
| 30 | 嘘 | [嘘] |
| 31 | 晕 | [晕] |
| 32 | 衰 | [衰] |
| 33 | 骷髅 | [骷髅] |
| 34 | 敲打 | [敲打] |
| 35 | 再见 | [再见] |
| 36 | 擦汗 | [擦汗] |
| 37 | 抠鼻 | [抠鼻] |
| 38 | 鼓掌 | [鼓掌] |
| 39 | 坏笑 | [坏笑] |
| 40 | 右哼哼 | [右哼哼] |
| 41 | 鄙视 | [鄙视] |
| 42 | 委屈 | [委屈] |
| 43 | 快哭了 | [快哭了] |
| 44 | 阴险 | [阴险] |
| 45 | 亲亲 | [亲亲] |
| 46 | 可怜 | [可怜] |
| 47 | 笑脸 | [笑脸] |
| 48 | 生病 | [生病] |
| 49 | 脸红 | [脸红] |
| 50 | 破涕为笑 | [破涕为笑] |
| 51 | 恐惧 | [恐惧] |
| 52 | 失望 | [失望] |
| 53 | 无语 | [无语] |
| 54 | 嘿哈 | [嘿哈] |
| 55 | 捂脸 | [捂脸] |
| 56 | 奸笑 | [奸笑] |
| 57 | 机智 | [机智] |
| 58 | 皱眉 | [皱眉] |
| 59 | 耶 | [耶] |
| 60 | 吃瓜 | [吃瓜] |
| 61 | 加油 | [加油] |
| 62 | 汗 | [汗] |
| 63 | 天啊 | [天啊] |
| 64 | 嗯嗯嗯 | [Emm] |
| 65 | 社会社会 | [社会社会] |
| 66 | 旺柴 | [旺柴] |
| 67 | 好的 | [好的] |
| 68 | 打脸 | [打脸] |
| 69 | 哇 | [哇] |
| 70 | 翻白眼 | [翻白眼] |
| 71 | 666 | [666] |
| 72 | 让我看看 | [让我看看] |
| 73 | 叹气 | [叹气] |
| 74 | 苦涩 | [苦涩] |
| 75 | 裂开 | [裂开] |
| 76 | 嘴唇 | [嘴唇] |
| 77 | 爱心 | [爱心] |
| 78 | 心碎 | [心碎] |
| 79 | 拥抱 | [拥抱] |
| 80 | 强 | [强] |
| 81 | 弱 | [弱] |
| 82 | 握手 | [握手] |
| 83 | 胜利 | [胜利] |
| 84 | 抱拳 | [抱拳] |
| 85 | 勾引 | [勾引] |
| 86 | 拳头 | [拳头] |
| 87 | OK | [OK] |
| 88 | 合十 | [合十] |
| 89 | 啤酒 | [啤酒] |
| 90 | 咖啡 | [咖啡] |
| 91 | 蛋糕 | [蛋糕] |
| 92 | 玫瑰 | [玫瑰] |
| 93 | 凋谢 | [凋谢] |
| 94 | 菜刀 | [菜刀] |
| 95 | 炸弹 | [炸弹] |
| 96 | 便便 | [便便] |
| 97 | 月亮 | [月亮] |
| 98 | 太阳 | [太阳] |
| 99 | 庆祝 | [庆祝] |
| 100 | 礼物 | [礼物] |
| 101 | 红包 | [红包] |
| 102 | 發 | [發] |
| 103 | 福 | [福] |
| 104 | 烟花 | [烟花] |
| 105 | 爆竹 | [爆竹] |
| 106 | 猪头 | [猪头] |
| 107 | 跳跳 | [跳跳] |
| 108 | 发抖 | [发抖] |
| 109 | 转圈 | [转圈] |

</details>

#### 语音 / 视频通话

```csharp
await client.SendVoiceChat("某好友");            // 单人语音通话
await client.SendVedioChat("某好友");            // 单人视频通话
await client.SendVoiceChats("某群", new[] { "张三", "李四" }); // 群多人语音
```

#### 发送语音消息（需虚拟声卡）

```csharp
await client.SendVoiceMessage("某好友", @"C:\audio\hello.wav");
```

!> `SendVoiceMessage` 依赖虚拟声卡 Cable input/output，见 [2.4 虚拟声卡](#24-虚拟声卡仅发语音消息需要)。

#### 文字转语音（TTS，💎）

!> 文字转语音及声音复刻内部使用了通义千问非实时语音合成模型: `qwen-audio-3.1-tts-flash`,详细了解请阅读[千问AI平台 - 语音合成](https://platform.qianwenai.com/docs/developer-guides/speech/tts-models)官方文档.


```csharp
// 系统默认走阿里千问 Qwen-Audio-TTS
await client.SendVoiceMessageWithTTS(
    "某好友",
    "sk-你的阿里云百炼APIKey",   // 申请：https://bailian.console.aliyun.com
    "你好，欢迎使用我们的服务");

// 自定义音色 / 指令控制
await client.SendVoiceMessageWithTTS(
    "某好友", "sk-xxx", "你好",
    new VoiceOptions
    {
        Voice = "你的音色",
        Instructions = "用温柔的语气说，语速稍慢"
    });
```


> Voice的值取自下面的 预置音色列表 ,或者 音色复刻 产生的音色ID

<details>
<summary>👉 所有系统预置音色列表(即Voice选项对应下面的音色ID)</summary>

| 音色 ID | 音色名称 | 性别 | 音色特点 | 主语言 |
|---|---|---|---|---|
| longanhuan_v3.1 | 龙安欢 | 女 | 支持多方言多语言 | 普通话 |
| longanlingxin_v3.1 | 龙安灵心 | 女 | 支持多方言多语言 | 普通话 |
| longanfengyue_v3.1 | 龙安风悦 | 女 | 支持多方言多语言 | 普通话 |
| yuxiaoyun_v3.1 | 于小云 | 女 | 元气、亲切、自然 | 普通话 |
| qiaoxiaojiao_v3.1 | 乔小娇 | 女 | 俏丽、可爱 | 普通话 |
| xiaxiaochen_v3.1 | 夏小晨 | 女 | 元气、明亮 | 普通话 |
| wenhuaiqing_v3.1 | 温怀清 | 女 | 清亮、柔和 | 普通话 |
| anxiaolan_v3.1 | 安小岚 | 女 | 清甜、纯净 | 普通话 |
| xieshurou_v3.1 | 谢舒柔 | 女 | 柔和、自然、知性 | 普通话 |
| baiqinglan_v3.1 | 白清岚 | 女 | 明亮、清纯 | 普通话 |
| xuyuyuan_v3.1 | 许玉远 | 女 | 知性、成熟、质感 | 普通话 |
| anruorou_v3.1 | 安若柔 | 女 | 气声、知性 | 普通话 |
| wenhuaizhi_v3.1 | 闻怀之 | 女 | 稳重、成熟 | 普通话 |
| xiaoxingzhi_v3.1 | 萧行之 | 女 | 端庄、贵气 | 普通话 |
| guyunshu_v3.1 | 顾云舒 | 女 | 成熟、稳重 | 普通话 |
| yeqinghe_v3.1 | 叶清禾 | 女 | 亲切、温柔 | 普通话 |
| yunhuanhuan_v3.1 | 云欢欢 | 女 | 高亢、热情 | 普通话 |
| xuxiaoqiao_v3.1 | 徐小俏 | 女 | 自然、俏皮 | 普通话 |
| baianran_v3.1 | 白安然 | 女 | 低沉、浑厚、气声 | 普通话 |
| xuyanchu_v3.1 | 许言初 | 女 | 沉稳、磁性 | 普通话 |
| yezhiqing_v3.1 | 叶知晴 | 女 | 轻快、自然 | 普通话 |
| anyuqing_v3.1 | 安语晴 | 女 | 甜妹 | 普通话 |
| Emily_v3.1 | Emily | 女 | 英式女声 | 英语 |
| Luna_v3.1 | Luna | 女 | 英式女声 | 英语 |
| Abby_v3.1 | Abby | 女 | 美式女声 | 英语 |
| Annie_v3.1 | Annie | 女 | 美式女声 | 英语 |
| Ava_v3.1 | Ava | 女 | 美式女声 | 英语 |
| Beth_v3.1 | Beth | 女 | 美式女声 | 英语 |
| Betty_v3.1 | Betty | 女 | 美式女声 | 英语 |
| Cally_v3.1 | Cally | 女 | 美式女声 | 英语 |
| Cindy_v3.1 | Cindy | 女 | 美式女声 | 英语 |
| Donna_v3.1 | Donna | 女 | 美式女声 | 英语 |
| longanyuanfei_v3.1 | 龙安元妃 | 女 | 高傲妃子音 | 普通话 |
| longanlingxi_v3.1 | 龙安灵希 | 女 | 可爱甜美音 | 普通话 |
| longyingtao_v3.1 | 龙应桃 | 女 | 温柔淡定女 | 普通话 |
| longanya_v3.1 | 龙安雅 | 女 | 高雅气质女 | 普通话 |
| longwan_v3.1 | 龙婉 | 女 | 细腻柔声女 | 普通话 |
| longxing_v3.1 | 龙星 | 女 | 温婉邻家女 | 普通话 |
| longhua_v3.1 | 龙华 | 女 | 元气甜美女 | 普通话 |
| longling_v3.1 | 龙铃 | 女 | 稚气呆板女 | 普通话 |
| longpaopao_v3.1 | 龙泡泡 | 女 | 飞天泡泡音 | 普通话 |
| loongstella_v3.1 | loongstella | 女 | 飒爽利落女 | 普通话 |
| longyuan_v3.1 | 龙媛 | 女 | 温暖治愈女 | 普通话 |
| longmiao_v3.1 | 龙妙 | 女 | 抑扬顿挫女 | 普通话 |
| longanli_v3.1 | 龙安莉 | 女 | 利落从容女 | 普通话 |
| longanwen_v3.1 | 龙安温 | 女 | 优雅知性女 | 普通话 |
| longxiaoxia_v3.1 | 龙小夏 | 女 | 沉稳权威女 | 普通话 |
| xunanchuan_v3.1 | 许南川 | 男 | 支持多方言多语言 | 普通话 |
| anmingyuan_v3.1 | 安明远 | 男 | 清亮、自然 | 普通话 |
| huozhuoshi_v3.1 | 霍拙石 | 男 | 清亮 | 普通话 |
| andi_v3.1 | 安迪 | 男 | ABC口音 | 普通话 |
| Eric_v3.1 | Eric | 男 | 英式男声 | 英语 |
| Luca_v3.1 | Luca | 男 | 英式男声 | 英语 |
| Andy_v3.1 | Andy | 男 | 美式男声 | 英语 |
| Brian_v3.1 | Brian | 男 | 美式男声 | 英语 |
| David_v3.1 | David | 男 | 美式男声 | 英语 |
| longjielidou_v3.1 | 龙杰力豆 | 男 | 天真男童音 | 普通话 |
| longhuohuo_v3.1 | 龙火火 | 男 | 顽皮少年音 | 普通话 |
| longhan_v3.1 | 龙寒 | 男 | 温暖痴情男 | 普通话 |
| longanzhi_v3.1 | 龙安智 | 男 | 睿智轻熟男 | 普通话 |
| longzhe_v3.1 | 龙哲 | 男 | 呆板大暖男 | 普通话 |
| longanyang_v3.1 | 龙安洋 | 男 | 阳光大男孩 | 普通话 |
| libai_v3.1 | 李白 | 男 | 古代诗仙男 | 普通话 |
| longniuniu_v3.1 | 龙牛牛 | 男 | 阳光男童声 | 普通话 |
| longshanshan_v3.1 | 龙闪闪 | 男 | 戏剧化童声 | 普通话 |
| longsanshu_v3.1 | 龙三叔 | 男 | 沉稳质感男 | 普通话 |
| longanlang_v3.1 | 龙安朗 | 男 | 清爽利落男 | 普通话 |
| longanchong_v3.1 | 龙安冲 | 男 | 激情推销男 | 普通话 |

说明：上表共 68 个音色，均为 `qwen-audio-3.1-tts-flash` 系统音色。音色 ID 必须与请求模型匹配，不能跨模型混用

</details>

#### 声音复刻（💎）

```csharp
// 上传参考音频克隆音色，返回 voice_id，可用于后续 TTS
var voiceId = await client.CloneVoice(
    "sk-你的千问APIKey",
    "https://example.com/ref.wav");   // 参考音频公网 URL

// 后续用它发语音
await client.SendVoiceMessageWithTTS("某好友", "sk-xxx", "你好",
    new VoiceOptions { Voice = voiceId });
```

!> 复刻的 `targetModel` 必须与合成时的 `model` 一致，不能跨模型使用。

#### 发送后自动关闭窗口（适合 Harness 调用）

下面这些方法会在发送成功后调用 `CloseWindowAsync()` 关闭微信主窗口，适合 DeepSeek Harness 这类“发完就退出”的自动化脚本；如果用户还要继续操作微信，不要用带 `AndClose` 的版本。

| 方法 | 说明 |
|---|---|
| `SendMessageAndClose(who, message, atUser, refer)` | 发文本后关闭窗口 |
| `SendMessageAndClose(message, atUser, refer)` | 给当前窗口发文本后关闭窗口 |
| `SendFileAndClose(who, files)` | 发文件后关闭窗口 |
| `SendEmojiAndClose(who, emoji, atUserList)` | 发表情后关闭窗口 |
| `SendVoiceMessageAndClose(who, filePath)` | 发语音文件后关闭窗口 |
| `SendVoiceMessageAndClose(filePath)` | 给当前窗口发语音后关闭窗口 |
| `SendVoiceMessageWithTTSAndClose(...)` | TTS 发语音后关闭窗口 |

> `SendVoiceMessageAndClose` 和 `SendVoiceMessageWithTTSAndClose` 仍然依赖虚拟声卡（Cable input/output），和普通语音发送的要求一样。

#### 按 WxId 精准发送（💎）

> 这一小节对应 `WeChatClient_Vip.cs` 的 `#region 按 username 发送`。它先到中间表里按 `UserName`（好友 wxid / 群 `xxx@chatroom`）找到联系人，再用联系人的 `ShowName` 调用普通发送方法，所以叫“按 WxId 精准发送”，但群聊传的是群 id。

##### 怎么拿到 WxId

`Send...ByWxid` 里的 `userName` 不是备注，不是昵称，也不是联系人的“微信号”；它是微信内部的 `UserName`：好友通常是 `wxid_...`，群是 `...@chatroom`。

| 要拿谁 | 推荐方法 | 取哪个字段 |
|---|---|---|
| 当前登录账号 | `client.GetOwerInfo()` | `.WxId`；VIP 版也可以直接用 `client.UserName` |
| 好友（界面自动化 / 社区版） | `await client.GetAllFriends(fromCache: false)` | `FriendInfo.WxId`；缓存里也可以用 `GetFriendFromCache(名称).WxId` |
| 好友或群（VIP 数据库） | `client.GetAllContacts()`、`GetContactWithShowName(...)` | `WeChatContact.UserName` |
| 群（VIP 数据库） | `client.GetAllChatRooms()`、`GetChatRoomWithShowName(...)` | `WeChatContact.UserName` |
| 从消息反推 | `GetChatMessages(...)`、`GetRecentChatMessages(...)`、`SearchMessages(...)` | 会话看 `Message.ChatUsername`，发送者看 `Message.SenderWxId` |
| 直接查库 | `SELECT UserName, ShowName FROM wechat_contact ...` | `UserName` |

```csharp
// 1. 当前登录账号的 wxid
var me = client.GetOwerInfo().WxId;


// 2. 好友和群：VIP 数据库方式更稳
var contact = client.GetContactWithShowName("张三");
if (contact != null)
    await client.SendMessageByWxid(contact.UserName, "你好");

var room = client.GetChatRoomWithShowName("某群");
if (room != null)
    await client.SendMessageByWxid(room.UserName, "大家好");
```

> `WeChatContact.Alias` 是对方自己设置的“微信号”，不是 `UserName`；`ShowName` 只是给人看的显示名。`Send...ByWxid` 要的是 `UserName`，不要传 `Alias`，也不要传备注/昵称。
>
> `GetAllFriends` 对普通好友能拿到 `WxId`，但群聊、部分企业微信联系人可能拿不到；群聊建议走 VIP 的 `GetAllChatRooms` / `GetChatRoomWithShowName`。拿到后先判断 `WxId` / `UserName` 是否为空，再调用发送。

| 方法 | 说明 |
|---|---|
| `SendMessageByWxid(userName, message, atUser, refer)` | 按 username 发文本，支持 @ 和引用 |
| `SendFileByWxid(userName, files)` | 按 username 发文件 |
| `SendEmojiByWxid(userName, emoji, atUserList)` | 按 username 发表情 |
| `SendVoiceMessageByWxid(userName, filePath)` | 按 username 发语音文件（需要虚拟声卡） |
| `SendVoiceMessageWithTTSByWxid(userName, apiKey, message, options, optimizeWithLlm, customProcess)` | 按 username 用 TTS 发语音 |

```csharp
// 按好友 wxid 发文本
bool ok = await client.SendMessageByWxid("wxid_xxx", "你好");

// 按群 id 发文件（群 id 形如 xxx@chatroom）
bool fileOk = await client.SendFileByWxid("1234567890@chatroom", new[] { @"C:\report.pdf" });

// 按 wxid 用 TTS 发语音
bool voiceOk = await client.SendVoiceMessageWithTTSByWxid(
    "wxid_xxx", "sk-你的千问APIKey", "你好",
    new VoiceOptions { Voice = "longanhuan_v3.1" });
```

> **返回值怎么理解**：成功不是看界面有没有弹提示，而是发送前记下当前账号的最大 `SortSeq`，发送后最多等 8 秒、每 3 秒查一次，目标会话出现 `IsSelf` 且 `SortSeq` 更大的消息才算成功。
>
> `userName` 不存在于中间表、或者 `ShowName` 为空时，这些方法直接返回 `false`，不会抛异常。

### 7.4 获取消息与聊天历史

```csharp
// 主动拉取（轮询），适用于 wechat bot 定时取最新消息
var msgs = await client.PullMessages("某好友", maxFetchNumber: 20);
foreach (var m in msgs)
    Console.WriteLine(m);   // 格式：类型 - 时间 - 谁: 内容

// 当前聊天窗口的历史：不传日期 = 今天；也可以只传日期
var currentToday = await client.GetChatHistory();
var currentDay = await client.GetChatHistory(new DateTime(2026, 10, 1));

// 指定好友/群的历史：只传日期，或传开始/结束时间
var today = await client.GetChatHistory("某好友", DateTime.Today);
var range = await client.GetChatHistory("某好友", new DateTime(2026, 10, 1), new DateTime(2026, 10, 20));

// 拍一拍 / 引用
await client.TapWho("某好友");
await client.ReferencedMessage("某好友", "某条消息内容");

// 引用一个消息对象（例如 MessageContext / PullMessages 返回的消息）
await client.ReferencedMessage(chatSimpleMessage);

// 引用当前页最后一条；只引用当前页，不会翻页
await client.ReferencedLastMessage();
```

| 方法 | 说明 |
|---|---|
| `GetChatHistory(DateTime date)` | 当前窗口指定日期；不传日期就是今天 |
| `GetChatHistory(string who, DateTime date)` | 指定好友/群指定日期 |
| `GetChatHistory(string who, DateTime start, DateTime end)` | 指定好友/群的时间段，适合历史消息很多时分段取 |
| `ReferencedMessage(ChatSimpleMessage message, int prevScrollNumber = 30)` | 用消息对象引用；当前页找不到会往前滚动 |
| `ReferencedMessage(string who, string message, int prevScrollNumber = 30)` | 按“谁 + 内容”引用 |
| `ReferencedLastMessage()` | 引用当前页最后一条，不翻页 |

### 7.5 会话管理

```csharp
// 所有会话标题（仅名称列表，高效）
var titles = await client.GetAllConversations();

// 可见会话（含名称/未读数/头像等）
var visible = await client.GetVisibleConversations();

// 搜索 / 定位会话
bool found = await client.SearchFriend("某好友");
bool ok = await client.LocateConversation("某好友");

// 会话设置
await client.SetTopMost("某好友", true);       // 置顶
await client.SetTopMost("某好友", false);       // 取消置顶
await client.SetDoNotDisturb("某好友", true);  // 免打扰
await client.SetDoNotDisturb("某好友", false);  // 取消 免打扰 模式
```

| 方法 | 说明 |
|---|---|
| `GetVisibleConversationTitles()` | 只取会话列表当前可见的标题，比完整会话对象更轻 |
| `Up(callback)` / `Down(callback)` | 手动向上/向下滚动会话列表；回调返回 `true` 表示继续滚，返回 `false` 表示停 |

> `Up` / `Down` 适合自己实现“边滚边找”的场景，回调参数里带当前可见元素和会话列表区域；一般业务直接用 `SearchFriend` / `LocateConversation` 就够了。

### 7.6 好友与通讯录

```csharp
// 获取所有好友（fromCache=true 从缓存读，快；false 重新刷通讯录）
var friends = await client.GetAllFriends(fromCache: true);
foreach (var f in friends)
    Console.WriteLine($"{f.Name}  wxid={f.WxId}");

// 好友名称列表
var names = await client.GetAllFriendNames();

// 缓存查询（昵称可能重复，wxid 唯一）
var f1 = client.GetFriendFromCache("某好友");
var f2 = client.GetFriendWithWxIDFromCache("wxid_xxx");
var f3 = client.GetFriendsFromCache("某好友");   // 重名时返回多个

// 手动维护缓存
client.AddOrUpdateFriendFromCache(f1);
client.RemoveFriendFromCache("某好友");

// 通过手机号/微信号 加好友
var results = await client.AddFriends(
    new[] { "13800138000", "wxid_xxxx" },
    new AddFriendsOptions
    {
        SayHi = "你好，我是 xx",      // 打招呼内容（空则微信默认）
        Suffix = "_新客户",           // 备注后缀
        IntervalTime = 5              // 间隔秒数，担心风控可调大
    });
foreach (var kv in results)
    Console.WriteLine($"{kv.Key}: {kv.Value}");   // FriendAddResult 枚举

// 删除好友
await client.RemoveFriend("某好友");
```

#### 好友缓存与“添加朋友”窗口

| 方法 | 说明 |
|---|---|
| `GetFriendListFromCache()` / `GetFriendListFromCacheAsync()` | 取缓存里的全部好友，同步/异步各一版 |
| `GetFriendFromCache(who)` / `GetFriendFromCacheAsync(who)` | 按好友名取一个；重名时可能不唯一 |
| `GetFriendsFromCache(who)` | 按好友名取多个，专门处理重名 |
| `GetFriendWithWxIDFromCache(wxid)` / `GetFriendWithWxIDFromCacheAsync(wxid)` | 按 wxid 取，wxid 不会重复，最稳 |
| `RemoveFriendFromCache(who)` / `RemoveFriendFromCacheAsync(who)` | 从缓存移除一个好友 |
| `RemoveFriendWithWxIDFromCache(wxid)` / `RemoveFriendWithWxIDFromCacheAsync(wxid)` | 按 wxid 从缓存移除 |
| `AddOrUpdateFriendFromCache(friend)` / `AddOrUpdateFriendFromCacheAsync(friend)` | 手动往缓存里加/改一个好友 |
| `OpenAddFriensWin()` | 打开“添加朋友”窗口；方法名就是 `Friens`，少一个 `d`，照抄 |
| `CloseAddFriendWin()` | 关闭“添加朋友”窗口 |

> 缓存只是“快”，不是“唯一真相”：新好友、备注变化时，SDK 在需要 wxid 的业务里会自动补拉；但批量业务最好先用 `GetAllFriends(fromCache: false)` 刷一遍，再按 wxid 维护自己的映射。

### 7.7 群管理

```csharp
// 判断是否是自有群
bool isOwner = await client.IsOwnerChatGroup("测试群");

// 自有群
// 创建群聊：如果存在则打开群聊，否则创建一个新群聊（返回 Result，失败含原因）
var createResult = await client.CreateOwnerChatGroup("测试群", "某好友", new[] { "张三", "李四" });
// 获取群聊成员列表（群名可为空，空表示焦点群聊）
var members = await client.GetChatGroupMemberList("测试群");
// 添加群聊成员，适用于自有群（支持单个或数组）
await client.AddOwnerChatGroupMember("测试群", "王五");
// 移除群聊成员，适用于自有群（支持单个或数组）
await client.RemoveOwnerChatGroupMember("测试群", "王五");
// 修改群名，适用于自有群群名修改
await client.ChangeOwnerChatGroupName("测试群", "新群名");
// 修改自己在群中的昵称（nickName 为空则删除本群昵称）
await client.ChangeChatGroupNickName("测试群", "我的昵称");
// 改变群备注，群备注仅自己可见（newMemo 为空则删除备注）
await client.ChangeChatGroupMemo("测试群", "这是备注");
// 更新群聊公告，仅适用于自有群
await client.UpdateGroupNotice("测试群", "大家好");
// 获取群主昵称
var owner = await client.GetGroupOwner("测试群");
// 退出群聊（clearHistory 是否清除历史消息，默认 true）
await client.QuitChatGroup("测试群", clearHistory: true);

// 外部群
// 邀请群聊成员（inviteReasonIfNeed：邀请原因，仅群管理员开启"进群需确认"时有效）
await client.InviteChatGroupMember("某外部群", new List<string> { "张三", "李四" });

// 群内加好友（微信严风控操作，谨慎分批；options 可设置备注后缀/打招呼内容/标签）
await client.AddChatGroupMemberToFriends("某外部群", new List<string> { "张三" });
```

!> 群内加好友是**严风控**操作，建议分批、一次不超过 20~30 个，时间跨度拉长到 4 小时或一天后。

### 7.8 消息监听（事件驱动）

监听以**事件回调**方式触发：来消息时调用你的回调，`MessageContext` 里带有新消息、历史消息。

**注：消息监听是以UI Automation的方式实现的，会占用键盘鼠标资源，并且同时服务几十个群时，会存在卡顿的情况，建议仅在<20个群/好友的场景下使用。**

!>官方更推荐使用数据库Database read的方式来获取消息，性能更好，且不会占用键鼠资源，详情请看 [7.12 数据库读取（Database Read，💎）](#712-数据库读取database-read)。

```csharp
// 固定监听某个好友/群
await client.AddMessageListener("某好友", ctx =>
{
    foreach (var m in ctx.NewMessages)
        Console.WriteLine($"{m.Who}: {m.Message}");

    // 回复当前监听窗口（Sender 只能给当前窗口发）
    ctx.Sender.SendMessage("已收到").GetAwaiter().GetResult();
});

// 监听多个
await client.AddMessageListener(new[] { "好友A", "群B" }, ctx => { /* ... */ });

// 开放式监听：不固定对象，所有"未开免打扰"的好友/群都监听
await client.AddMessageListener((string)null, ctx => { /* ... */ }, IsOpenMonitor: true);

// 固定时间段监听（9:00~18:00 生效）
await client.AddMessageListener(
    "某好友", ctx => { /* ... */ },
    new TimeOnly(9, 0), new TimeOnly(18, 0));

// 一天多个时间段，也可以跨天（例如 23:00~02:00）
await client.AddMessageListener(
    "某好友", ctx => { /* ... */ },
    new List<TimeOnlyRange>
    {
        new() { StarTime = new TimeOnly(9, 0), EndTime = new TimeOnly(12, 0) },
        new() { StarTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },
    });

// 子窗口监听（弹窗式，返回更及时，回调为 async）
await client.AddMessageListenerWithSubWin("某好友", async ctx =>
{
    foreach (var m in ctx.NewMessages)
        await ctx.Sender.SendMessage($"收到：{m.Message}");
});

// 群聊系统消息监听（如：群主邀请好友后发欢迎语）
await client.AddGroupSystemMessageListener("某群", async ctx =>
{
    foreach (var m in ctx.NewMessages)
        Console.WriteLine($"[{ctx.FromWho}] 系统消息: {m}");
});

// 暂停 / 恢复 / 增减监听对象
await client.PauseMessageListener();
await client.ResumeMessageListener();
await client.AddListeningFriend("某好友");
await client.RemoveListeningFriend("某好友");

// 弹窗式监听增减对象
await client.AddSubWinListeningFriend("某好友");
await client.RemoveSubWinListeningFriend("某好友");
```

> **监听规则**（务必注意）：只能监听"允许消息通知"的好友/群聊，请勿给被监听对象设置"消息免打扰"；不监听的对象建议设免打扰以提升性能；建议减少"置顶"数量以降低滚动。

> `MessageMonitorOptions` 可精细控制：是否抓图片/附件/视频（`FetchImage`/`FetchAttachment`/`FetchVideo`）、是否点红包（`ClickRedEnvelope`）、风控预防（`IsRiskPrevention` + `RiskPreventionAction`）、数据库写入（`DbType`/`ConnectionString`/`WriteDataBaseCallback`）等。

> `UIInvoker` 参数（`Action<string>`）用于把微信嵌入 UI 的场景（如多微信切换 Tab 页），SDK 会把微信名称注入给你的调度器。

### 7.9 新好友自动通过（💎）

!> 新好友申请加好友自动通过可以根据关键词设置单独的备注后缀与标签

```csharp
var options = new FriendRequestAutoAcceptOptions
{
    KeyWord = new List<string> { "合作", "商务" },  // 只有打招呼命中关键词才通过
    Suffix = "默认",                               // 全局默认后缀（未命中 KeywordRules 时回退到这里）
    Label = "客户",                                 // 全局默认标签

    // 下面的 KeywordRules 演示单独为每个关键词设置 备注后缀 及 标签
    // 按关键词设置单独的备注后缀与标签：命中关键词时优先用此规则，否则回退到全局 Suffix/Label
    KeywordRules = new Dictionary<string, FriendKeywordRule>
    {
        { "合作", new FriendKeywordRule { Suffix = "合作", Label = "合作客户" } },
        { "商务", new FriendKeywordRule { Suffix = "商务", Label = "商务客户" } },
    },

    PassedDelete = true,                            // 通过后删除申请记录
    PassedCallBack = async (passedList, wxClient, sp) =>
    {
        foreach (var item in passedList)
            await wxClient.SendMessage(item.Who, "您好，欢迎添加！");
    }
};


await client.AddFriendRequestAutoAcceptListener(options);

// 不启动监听，立即把当前待处理的好友申请批量过一遍
var passed = await client.PassedAllNewFriend(options);
foreach (var item in passed)
    Console.WriteLine($"已通过：{item.Who}");   // NewFriendBackItem

// 暂停 / 恢复 / 停止
await client.PauseNewFriendListener();
await client.ResumeNewFriendListener();
await client.StopNewFriendListener();   // 停止后可换配置重新启动
```

### 7.10 朋友圈（💎）

```csharp
// 打开朋友圈（已打开则提到最前），需要自己控制窗口时用
var momentsWindow = await client.OpenMoments();

// 关闭朋友圈
await client.CloseMoments();

// 发朋友圈（图文，多图需在同一目录）
await client.AddMoments(
    new List<string> { @"C:\img\a.jpg", @"C:\img\b.jpg" },
    "今天天气不错",
    new MomentsOptions { Labels = new List<string> { "朋友" } });

// 删除自己发的朋友圈（按文字内容）
await client.RemoveMoments("今天天气不错");
```

### 7.11 消息转发（💎）

```csharp
// 转发多条（默认最后 5 条，ForwardMerge = 合并转发）
await client.ForwardMultipleMessage("某好友", "某群", rowCount: 5);

// 逐条转发，并同时转给多个对象
await client.ForwardMultipleMessage(
    "某好友",
    new[] { "群A", "群B" },
    ForwardMessageTypeEnums.ForwardOneByOne,
    rowCount: 3);

// 转发单条：按“谁 + 内容”
await client.ForwardSingleMessage("某好友", "某条消息内容", "某群");

// 转发单条：直接传消息对象（例如 MessageContext / PullMessages 拿到的消息）
await client.ForwardSingleMessage(chatSimpleMessage, "某群");
```

### 7.12 数据库读取（Database Read，💎）

> 这一节对应 `WeChatClient_Vip.cs` 的 `#region 数据库database read`。只有 VIP 版、并且配置了 `UseDatabaseRead = true` 时才可用；社区版没有这些方法。
>
> 这些方法都是**同步方法**，查的是 SDK 已经同步到中间表的数据，不会写微信原库。`client.UserName` 是当前微信号的 wxid，查询用它做账号隔离；`DataBaseKey`、`DbStorage` 由 SDK 内部维护，一般不需要手写。

#### 前置条件：怎么启用 Database Read

Database Read 默认是关的，要在初始化回调里显式打开。下面是最小可用配置：

```csharp
using SqlSugar;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Components;
using WeChatAuto.Services;

var serviceProvider = WeAutomation.Initialize(options =>
{
    // 1. 打开“数据库读取 / 热同步”总开关
    options.UseDatabaseRead = true;

    // 2. 微信数据存储位置：一般留空，SDK 会自动读取并缓存
    //    如果明确知道路径，也可以手动指定；多微信必须填同一个路径
    // options.WeChatStorageRoot = @"D:\xwechat_files";

    // 3. 中间表数据库：默认 SQLite，文件会落在当前工作目录,可以换成 SQL Server / MySQL / PostgreSQL等数据库;
    options.DbTppe = DbType.Sqlite;
    options.ConnectionString = "Data Source=wechat.db";
    //可选 启用vss卷快照与直连访问微信数据库，默认都为True,如果 HotSyncVssSource=false,HotSyncLiveSource=true,则仅使用直连访问微信数据库的方式
    // options.HotSyncVssSource = true;  
    // options.HotSyncLiveSource = true;

    // 可选：首轮往回补多少天历史（默认 30；0 表示只同步启动后的新消息）
    // options.FirstRunDays = 30;

    // 可选：首轮只同步文字，历史图片/文件/视频先跳过，速度更快
    // options.HotSyncFirstRunTextOnly = true;

    // 可选：大库排查时打开热同步复制日志
    // options.HotSyncCopyLog = true;
});

var factory = serviceProvider.GetRequiredService<WeChatClientFactory>();
var client = factory.GetWeChatClient("你的微信名");

// 初始化后先确认同步有没有数据，再调用 7.12 的查询方法
var contacts = client.GetAllContacts();
Console.WriteLine($"联系人：{contacts.Count}");
```

如果你用的是外部 DI（ASP.NET Core / Generic Host），写法一样，只是把 `WeAutomation.Initialize(...)` 换成 `WeAutomation.Initialize(builder.Services, options => { ... })`。

**这几步分别在做什么：**

1. `UseDatabaseRead = true`*：总开关。不开就是纯 UI 自动化，7.12 的方法不能用，也不会建中间表。
2. `WeChatStorageRoot`*：微信的“设置 → 账号与存储 → 存储位置”。一般留空，SDK 会在第一次启动时自动通过自动化来获取到，并把结果缓存到 `%LOCALAPPDATA%\WeChatAuto\wechatkey.dat`；多个微信要共用同一个路径，不要一个号填一个。
3. **`DbTppe` + `ConnectionString`**：中间表写到哪个数据库。默认 SQLite，零安装；换成 SQL Server / MySQL / PostgreSQL 时，这两个要一起改。
4. **可选项**：`FirstRunDays` 控制首轮回看天数；`HotSyncFirstRunTextOnly` 让首轮先只同步文字、媒体之后增量补；`HotSyncCopyLog` 用于大库排查，平时关着即可。

**常见卡点：**

- `DbTppe` 改了、`ConnectionString` 没改：连不上，或者还写到旧库；
- `WeChatStorageRoot` 填了不存在的目录：以配置为准时读不到库；不确定就留空；
- 多个微信填了不同存储路径：微信本身要求同一个存储位置，应该统一；
- 社区版没有 Database Read：这一段只适用于 VIP 版。

> 用 HTTP Server 的话，数据库连接在 `App.json` 的 `Database` 节点配置，不需要在 `Initialize` 里再写一遍；内嵌 SDK 才用上面的 `WeAutomation.Initialize` 回调。

#### 7.12.1 联系人

| 方法 | 说明 |
|---|---|
| `GetContact(userName)` | 按 username（wxid / 群 id）精确取一条联系人；找不到或已删除返回 `null` |
| `FindContacts(keyword)` | 按关键字模糊查找，匹配 `UserName`、`NickName`、`Remark`、`Alias` |
| `GetAllContacts()` | 取当前微信号全部联系人，自动排除已删除行 |
| `GetContactWithShowName(showName)` | 按显示名精确取一条；重名时返回 `UserName` 升序的第一条 |
| `GetContactsWithShowName(showName)` | 按显示名精确取多条，专门处理重名 |

> `WeChatContact.ShowName` 的显示顺序是：**备注 → 微信号别名 → 昵称 → UserName**。所以界面上看到的群名可能是群备注，不一定是群本身的 `NickName`；按显示名查的时候要用 `ShowName`，不要自己拼。

```csharp
// 先拿全部联系人，再从里面取 username 和显示名
var contacts = client.GetAllContacts();
foreach (var c in contacts)
    Console.WriteLine($"{c.UserName}  {c.ShowName}");

// 按 username 精确取
var one = client.GetContact("wxid_xxx");

// 按显示名取（可能重名，所以有“一条”和“多条”两个版本）
var byName = client.GetContactWithShowName("张三");
var sameName = client.GetContactsWithShowName("张三");
```

#### 7.12.2 聊天记录与搜索

| 方法 | 说明 |
|---|---|
| `GetChatMessages(chatUsername, count = 50)` | 取指定会话最近 `count` 条，按 `SortSeq` **从新到旧**，适合浏览 |
| `GetRecentChatMessages(chatUsername, count = 50)` | 取最近 `count` 条后，再按 `SortSeq` **从旧到新**，适合直接喂给 LLM 当上下文 |
| `GetNewMessages(afterSortSeq)` | 取 `SortSeq` 比给定值更大的新消息，按从旧到新；做增量同步用 |
| `GetMaxSortSeq()` | 当前微信号消息的最大 `SortSeq`；没有消息返回 `0`，用来做增量水位 |
| `SearchMessages(keyword, chatUsername = null)` | 只在当前微信号搜索 `Content`；可限定会话；结果从新到旧 |
| `SearchAllMessages(keyword, chatUsername = null)` | 跨所有微信号搜索 `Content`；结果里带 `FromWechatWxId`，显示名按各自账号解析 |

> **水位只能用 `SortSeq`**，不要用自增主键 `Id`，也不要用写入时间 `CreateTime`。第一次先 `GetMaxSortSeq()`，之后每次只取比它更大的消息，再把水位更新成这次最大的 `SortSeq`。
>
> `SearchAllMessages` 不绑定当前 `client.UserName`；`SortSeq` 只在同一个微信号内有序，跨账号比较时要结合 `FromWechatWxId`。

```csharp
// 最近 20 条，按从旧到新，直接当 LLM 上下文
var recent = client.GetRecentChatMessages("wxid_xxx", 20);
foreach (var item in recent)
    Console.WriteLine($"{item.SenderDisplayName}: {item.Message.Content}");

// 增量：先取当前水位，再取新消息，最后更新水位
long watermark = client.GetMaxSortSeq();
foreach (var item in client.GetNewMessages(watermark))
{
    Console.WriteLine($"{item.ChatDisplayName} / {item.SenderDisplayName}: {item.Message.Content}");
    watermark = item.Message.SortSeq;
}

// 搜索：当前账号 / 全部账号
var mine = client.SearchMessages("合同", "wxid_xxx");
var all = client.SearchAllMessages("合同");
```

`WeChatMessageQueryResult` 里常用的字段：

- `Message`：原始消息对象（`Content`、`MsgCreateTime`、`MessageTime`、`SortSeq`、`IsSelf` 等）；
- `ChatDisplayName`：会话显示名；
- `SenderDisplayName`：发送者显示名；
- `QuoteDisplayName`：被引用消息的显示名（有引用时）。

#### 7.12.3 群与群成员

| 方法 | 说明 |
|---|---|
| `GetRoomMembers(roomUsername)` | 按群 username 取成员，并带上群名片和各人的显示名 |
| `GetAllChatRooms()` | 取当前微信号全部群聊（`UserName` 含 `@chatroom`，不含已删除） |
| `GetChatRoomWithShowName(showName)` | 按群的显示名精确取一个；重名时返回 `UserName` 升序的第一条 |
| `GetChatRoomsWithShowName(showName)` | 按群的显示名精确取多个，处理重名 |
| `GetRoomMembersByShowName(showName)` | 按群的显示名取成员；重名时取第一条对应的群，找不到返回空列表 |
| `GetChatRoomsByLocalType(int localType)` | 在群聊中按原生 `LocalType` 过滤；本机观察群多为 `2` |
| `GetChatRoomsByLocalType(ContactLocalType localType)` | 同上，但用枚举；目前收录了 `Friend = 1`、`ChatRoom = 2` |

> 判断“是不是群”最终仍以 `UserName` 是否包含 `@chatroom` 为准；`LocalType` 只是附加过滤。群有备注时，显示名是备注，不是群名，所以按显示名查要用 `ShowName`。

```csharp
var rooms = client.GetAllChatRooms();
foreach (var room in rooms)
    Console.WriteLine($"{room.UserName}  {room.ShowName}");

// 按显示名取群，再按群 username 取成员
var room = client.GetChatRoomWithShowName("某个群");
if (room != null)
{
    var members = client.GetRoomMembers(room.UserName);
    foreach (var m in members)
        Console.WriteLine($"{m.Member.MemberUsername}  {m.MemberDisplayName}");
}

// 只看“群”类型的群
var chatRooms = client.GetChatRoomsByLocalType(ContactLocalType.ChatRoom);
```

#### 7.12.4 朋友圈

| 方法 | 说明 |
|---|---|
| `GetSnsPost(tid)` | 按 tid 取一条朋友圈，一次带回媒体、点赞、评论；找不到返回 `null` |
| `GetSnsPosts(limit)` | 取最近 `limit` 条朋友圈，按发布时间从新到旧；`limit` 必须大于 0 |
| `GetSnsPostsByDate(date)` | 取某一天的朋友圈；按中国标准时间（UTC+8）的日历日判断，时分秒会被忽略 |
| `GetSnsPostsByUserName(userName)` | 取某个发布者（真实 `UserName`/wxid）的朋友圈，从新到旧 |
| `GetSnsPostsByShowName(showName)` | 按发布者显示名取朋友圈；显示名完全一致，重名会一并返回 |

`WeChatSnsPostQueryResult` 里常用的字段：

- `Post`：朋友圈原始对象（正文、时间、tid 等）；
- `UserDisplayName`：发布者显示名；
- `Media`：图片 / 视频 / 文件等媒体列表；
- `Likes`：点赞列表；
- `Comments`：评论列表。

```csharp
var latest = client.GetSnsPosts(20);
var one = client.GetSnsPost(1234567890123456789);
var today = client.GetSnsPostsByDate(DateTime.Today);
var byUser = client.GetSnsPostsByUserName("wxid_xxx");
var byName = client.GetSnsPostsByShowName("张三");
```

#### 7.12.5 自定义查询

| 方法 | 说明 |
|---|---|
| `Query(sql)` | 执行一条 `SELECT`（含 `WITH ... SELECT`），返回 `IList<IDictionary<string, object>>` |

> `Query` 是给复杂查询留的通用入口：它**不会自动追加当前微信号条件**，SQL 由你自己写；只支持查询，`INSERT` / `UPDATE` / `DELETE` 这类写操作会抛异常。SQL 也是你自己拼的，注意注入风险，不要直接把用户输入拼进去。

```csharp
var rows = client.Query(
    "SELECT ChatUsername, SenderWxId, Content, MsgCreateTime " +
    "FROM wechat_message " +
    "WHERE FromWechatWxId = 'wxid_xxx' " +
    "ORDER BY SortSeq DESC LIMIT 20");

foreach (var row in rows)
    Console.WriteLine($"{row["ChatUsername"]}: {row["Content"]}");
```

> `client.Query(...)` 只适合临时查数：它不会自动帮你加 `FromWechatWxId` 条件，也不做写入。真正常态化的报表，建议直接连中间库、用视图或定时任务跑，别把复杂 SQL 塞在业务代码里。


#### 7.12.6 存储位置（辅助）

`GetWeChatStorageRootByAutomation()` 会临时打开微信的“设置 → 账号与存储”，读出“存储位置”再关掉设置窗口。

- 一般不需要调用：SDK 在第一次需要读数据库时会自动读一次并缓存；
- 只有在做诊断、或者你想自己确认当前存储路径时才用；
- 它依赖 UI Tree 和设置窗口的控件，微信改版后可能需要更新；
- 属于 VIP 能力，社区版没有数据库读取，也就不需要这个值。

```csharp
var storageRoot = await client.GetWeChatStorageRootByAutomation();
Console.WriteLine(storageRoot);
```

#### 7.12.7 扩展阅读：数据库 schema

上面这些方法是“SDK 帮你查”；如果你更习惯 SQL，也可以直接连中间库，或者用 `client.Query(...)` 执行 `SELECT`，看到的是同一套表、同一批数据。两条路不冲突：

- **走 API**：适合常规业务，不用关心表名和字段名，升级时也更稳；
- **走 SQL**：适合复杂报表、跨表统计、和客户已有 BI / 数仓对接；表结构见 [数据库 Schema（VIP 中间表）](schema.md)。

VIP 中间表一共 7 张：

| 表名 | 它负责什么 |
|---|---|
| `wechat_contact` | 通讯录：好友、群、公众号等，显示名、备注、头像、地区、来源都在这里 |
| `wechat_message` | 一条条聊天消息，包括文字、图片、语音、引用等 |
| `wechat_chat_room_member` | 某个群里有哪些人，以及每个人的群名片 |
| `wechat_sns_post` | 一条条朋友圈动态 |
| `wechat_sns_like` | 某条动态下面的点赞 |
| `wechat_sns_comment` | 某条动态下面的评论（含回复） |
| `wechat_sns_media` | 某条动态里的图片 / 视频明细，一张图或一个视频一行 |


---

## 八、API 参考

> 这里放的是完整的 C# API 参考：类、方法、参数、返回值、枚举，按命名空间和类浏览。

<a href="sdk-api/api/WeChatAuto.Components.WeChatClient.html" target="_blank" rel="noopener" style="display:inline-block;padding:10px 18px;border-radius:6px;background:#42b983;color:#fff;text-decoration:none;font-weight:600;">打开 API 参考</a>

常用入口：

- <a href="sdk-api/api/WeChatAuto.Components.WeChatClient.html" target="_blank" rel="noopener">WeChatClient</a>：发消息、好友、群、监听、数据库读取的主入口
- <a href="sdk-api/api/WeChatAuto.Components.WeChatClientFactory.html" target="_blank" rel="noopener">WeChatClientFactory</a>：多微信客户端的创建与管理
- <a href="sdk-api/api/WeAutoCommon.Configs.WeChatConfig.html" target="_blank" rel="noopener">WeChatConfig</a>：初始化配置项


## 九、多微信实例


`WeChatClientFactory` 一个进程可同时管理多个微信客户端：

```csharp
using var factory = serviceProvider.GetRequiredService<WeChatClientFactory>();

foreach (var name in factory.GetWeChatClientNames())
{
    var c = factory.GetWeChatClient(name);
    await c.SendMessage("某好友", $"来自 {name} 的问候");
}
```

> 多实例共用一个 UI 动作线程，操作会被串行调度，天然避免多微信之间抢鼠标。

---

## 十、生命周期与资源释放

- `WeChatClientFactory` 是单例且 `IDisposable`，建议 `using var factory = ...` 或手动 `factory.Dispose()`；
- `WeChatClient` 由工厂统一创建与管理，**不要手动 `Dispose` 单个 client**——工厂 `Dispose` 时会统一释放所有 client、监听器、录制器与 UI 线程；
- 释放后 SDK 会停止消息监听、取消相关 `CancellationToken`。

```csharp
using var factory = serviceProvider.GetRequiredService<WeChatClientFactory>();
// ... 业务逻辑 ...
// using 结束会自动释放，无需额外处理
```

---

## 十一、风控与最佳实践

!> SDK 驱动的是**真实微信客户端**（UI 自动化），非幂等纯 CRUD。加好友、邀请群成员、群内加好友等属于微信**严风控**操作。

- **控制频率**：批量加好友/拉群建议分批、拉长时间间隔（几小时到一天）；
- **随机延时**：SDK 内置随机延时与点击偏移，可在配置中调整（如 `AddFriendsOptions.IntervalTime`）；
- **键鼠模拟器**：高敏感场景（如群内加好友）强烈建议搭配硬件键鼠模拟器，输入等同真人，显著降低风控误判；
- **监听风控预防**：`MessageMonitorOptions.IsRiskPrevention = true` 会在运行一段时间后自动暂停监听、随机等候（可自定义 `RiskPreventionAction`）；
- **合规**：请遵守微信使用条款，仅用于合法用途。

---

## 十二、常见问题（FAQ）

- **运行自动化不了微信**：确认微信已登录且窗口可见、系统为 Win10+；若当前微信未暴露 UI Tree，请开启 `IsForceOpenUITree` 或参考 [issue#3](https://github.com/scottfly189/WeChatAuto.SDK/issues/3)。
- **提示 `Det模型文件不存在`**：未放置 OCR 模型，把 `models` 文件夹拷到程序运行目录（见 [2.3 OCR 模型](#23-ocr-模型部分能力必需)）。
- **提示 `获取微信窗口失败` / `微信客户端不存在`**：微信未打开、窗口隐藏，或昵称写错（区分大小写、精确匹配）。
- **提示 `不支持UI Tree`**：微信未公开 UI Tree，开启 `IsForceOpenUITree`，或用工具确认 UI Tree。
- **两种 `Initialize` 混用报错**：外部 DI 与内部 DI 只能二选一，见 [5.4 两种依赖注入模式](#54-两种依赖注入模式二选一互斥)。
- **消息监听不回调**：被监听对象被设了"消息免打扰"；或未 `await AddMessageListener`。只有"允许消息通知"的对象才能监听。
- **发语音失败**：需安装虚拟声卡 Cable input/output（见 [2.4 虚拟声卡](#24-虚拟声卡仅发语音消息需要)）。
- **远程桌面最小化后自动化异常 / 发不了消息**：见 [FAQ](https://github.com/scottfly189/WeChatAuto.SDK/blob/master/docs/faq.md) 中的 RDP 与剪贴板设置。
- **更多问题**：参见 [完整 FAQ](./faq.md)，或提交 [Issues](https://github.com/scottfly189/WeChatAuto.SDK/issues)。
