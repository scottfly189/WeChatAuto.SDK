# WECHATAUTO.SDK 数据库 Schema（VIP 中间表）

HTTP 服务跑起来之后，你不用再去翻微信自己的文件。通讯录、聊天记录、群成员和朋友圈，会被整理进你配置的那一个数据库里。这篇就是这套库的地图：有哪些表、字段是什么意思、它们怎么连在一起。

适合刚接手 WeChatAuto.SDK、准备直接查库做 Bot 的同学。发送消息、通过好友、群管理等操作，请看 [HTTP 服务使用说明](httpserveruse.md)；本文只讲「数据已经在库里了，怎么查」。

!> 这里介绍的是 **VIP 版中间表**（HTTP 服务体验包写入的也是这一套）。社区开源版不包含这套库同步代码。联系人的稳定标识字段是 `UserName`，不要按社区版的 `WxId` 来理解。

多号默认写进同一套库，用 `FromWechatWxId` 区分这条数据属于哪个微信号。默认是本地 SQLite，也可以改成 MySQL 等你自己准备的库。

## 1. 先看全局：一共 7 张表

可以把它们想成两户人家：一边是聊天，一边是朋友圈。通讯录是公用的「电话本」。

| 表名 | 它负责什么 |
| --- | --- |
| `wechat_contact` | 通讯录：好友、群、公众号等。显示名、备注、头像、地区、来源都在这里 |
| `wechat_message` | 一条条聊天消息，包括文字、图片、语音、引用等 |
| `wechat_chat_room_member` | 某个群里有哪些人，以及每个人的群名片 |
| `wechat_sns_post` | 一条条朋友圈动态 |
| `wechat_sns_like` | 某条动态下面的点赞 |
| `wechat_sns_comment` | 某条动态下面的评论（含回复） |
| `wechat_sns_media` | 某条动态里的图片 / 视频明细（一张图或一个视频一行） |

它们的关系可以记成下面这张简图。所有「人」的显示名，最后都回到通讯录：

```text
wechat_contact  ←  几乎所有显示名都来这里（备注优先，没有再用昵称）
       ↑
       │  UserName = 消息里的 ChatUsername / SenderWxId
       │           = 群成员里的 RoomUsername / MemberUsername / Owner / Inviter
       │           = 朋友圈里的 UserName 等
       │
wechat_message          wechat_chat_room_member

wechat_sns_post ── Tid ──┬── wechat_sns_like
                         ├── wechat_sns_comment
                         └── wechat_sns_media
```

新手先把这 5 条记住，后面查库会少走很多弯路：

1. **显示名不要在消息表里找。** 表里已经不再保留 `Who` / `Sender` 这类列。用 `ChatUsername` / `SenderWxId` 去联 `wechat_contact.UserName`，备注优先，空则用昵称。
2. **多号筛选永远带上 `FromWechatWxId`。** `FromWechat` 只是该账号当时的昵称，可能重名，也可能改名。
3. **定位一条消息**用 `FromWechatWxId + ChatUsername + Shard + LocalId`。各分片里的 `LocalId` 都从 1 计，不带 `Shard` 会把不同消息当成同一条。增量水位请用 `SortSeq`，不要用 `Id`，也不要用写入时间 `CreateTime`。
4. **朋友圈用 `Tid` 把动态、点赞、评论、媒体串起来。** `Tid`、消息里的 `LocalId` / `ServerId` / `SortSeq` / `LocalType` / `MsgCreateTime` 都可能超过 32 位整数范围，请按 64 位整数读写。
5. **两个名叫 `CreateTime` 的字段不是一回事。** 在 `wechat_message` 里，聊天发生时间是 `MsgCreateTime`（Unix 秒），`CreateTime` 只是这条记录进中间表的时间。在朋友圈各表里，`CreateTime` 才是动态、点赞或评论的发布时间（也是 Unix 秒）。

下面按表说明字段。查询示例在最后一节。

## 2. `wechat_contact`（通讯录）

这是整套库的「电话本」。好友、群、公众号都会出现在这里。一个人在不同微信号的通讯录里，会是不同的行，靠 `FromWechatWxId` 区分。

**唯一键：** `FromWechatWxId + UserName`

显示名怎么取：`Remark`（备注）优先，没有备注再用 `NickName`（昵称）。`Alias` 是对方自己设置的微信号，不是备注。

| 字段名 | 说明 |
| --- | --- |
| `Id` | 中间表自增主键 |
| `FromWechat` | 这条联系人属于哪个微信号（该账号的昵称） |
| `FromWechatWxId` | 这条联系人属于哪个微信号（该账号的 wxid）。多号筛选和跨表关联请用这个 |
| `WxContactId` | 微信侧联系人编号，一般查询用不到。不要把它当成 wxid |
| `LocalType` | 联系人类型编码。完整枚举尚未逐项核实，不要只靠猜测分流；群的 `UserName` 通常带 `@chatroom` 后缀 |
| `UserName` | 稳定标识：好友是对方 wxid，群是群 id（如 `123@chatroom`）。跨表关联用这个，不要用显示名 |
| `NickName` | 昵称 |
| `Alias` | 对方自己设置的微信号 |
| `Remark` | 你给对方（或这个群）取的备注 |
| `BigHeadUrl` | 头像大图地址 |
| `SmallHeadUrl` | 头像小图地址 |
| `Country` | 国家 / 地区代码（ISO2，如 `CN` / `HK` / `US`），未设置时为空 |
| `Province` | 省 / 直辖市 / 州。同步时会尽量译成中文；没译到的境外名称会保留原文 |
| `City` | 市 / 区。直辖市时，这里常常是区名（如「海淀」） |
| `Labels` | 微信标签名，逗号分隔。这是同步当时的名字快照，之后在微信里改名或删标签，这里不会自动跟着改 |
| `Gender` | 性别：`0` 未知 / `1` 男 / `2` 女 |
| `SourceScene` | 来源场景码。已知取值见下表；`0` 表示原数据里没写 |
| `Source` | 来源中文文案（由 `SourceScene` 翻译，如「通过群聊添加」）；未知码会写成「场景码 N」，方便直接查询和导出 |
| `AddTime` | 添加好友或关注公众号的时间（Unix 秒）。`0` 表示原数据里没有这个时间（不少从旧版迁移过来的联系人会是 0） |

`SourceScene` 已核实的取值：

| 取值 | 含义 |
| --- | --- |
| 1 | 通过QQ号添加 |
| 3 | 通过微信号添加 |
| 6 | 通过手机号添加 |
| 10 | 通过名片添加 |
| 14 | 通过群聊添加 |
| 15 | 通过搜索手机号添加 |
| 17 | 通过名片分享添加 |
| 30 | 通过扫一扫添加 |

## 3. `wechat_message`（消息）

你在聊天窗口里看到的一条条消息，对应本表。会话是谁、发送者是谁，请用 `ChatUsername` / `SenderWxId` 去联通讯录，不要在本表里找显示名列。

**定位一条消息：** `FromWechatWxId + ChatUsername + Shard + LocalId`。本表没有唯一约束。`ChatUsername` 为空，或 `LocalId` 小于等于 0 的行，不能当成可靠的定位键。

**增量水位请用 `SortSeq`。** 按「账号 + 会话 + 分片」取最大 `SortSeq` 即可。查询聊天记录时，按 `SortSeq` 或 `MsgCreateTime` 从大到小排，就是从新到旧。

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

`Quote*` 各列只在引用消息中有值，其它消息为空。

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

## 4. `wechat_chat_room_member`（群成员）

想知道「这个群里到底有谁」，来看这张表。每一个成员一行。群名称、群主昵称、成员昵称、邀请人昵称都不在本表里重复存放，请用对应的 username 去联通讯录。**唯一要在本表读的显示名，是群名片 `GroupNickname`**——通讯录里没有这一列。

**唯一键：** `FromWechatWxId + RoomUsername + MemberUsername`

| 字段名 | 说明 |
| --- | --- |
| `Id` | 中间表自增主键 |
| `FromWechat` | 这条记录属于哪个微信号（该账号的昵称） |
| `FromWechatWxId` | 这条记录属于哪个微信号（该账号的 wxid） |
| `RoomUsername` | 群 id，如 `123@chatroom`。群的显示名请联 `wechat_contact` |
| `Owner` | 群主的 username。同一群里每一行都会带上，方便单表过滤群主 |
| `MemberUsername` | 这位成员的 username。成员显示名请联 `wechat_contact` |
| `GroupNickname` | 群名片（他在这个群里的称呼）。联系人表没有这个字段，要在本表读 |
| `Inviter` | 邀请人的 username，可能为空。邀请人显示名请联 `wechat_contact` |
| `Flag` | 成员标志。具体每一位的含义尚未逐项核实 |

## 5. `wechat_sns_post`（朋友圈动态）

一条朋友圈就是这里的一行。点赞、评论、图片视频不堆在这一行里，分别在后面三张表，用同一个 `Tid` 串起来。

**唯一键：** `FromWechatWxId + Tid`

`Tid` 是有符号的 64 位整数，关联点赞、评论、媒体时用它。`TidUnsigned` 是同一值的无符号十进制字符串，方便对照和展示，查询关联请仍用 `Tid`。

发布者显示名：本表有 `NickName`，写入时会按 `UserName` 查通讯录，备注优先；没有备注时，才保留动态里原来的昵称。更稳妥的做法，仍然是自己再联一次 `wechat_contact`。

| 字段名 | 说明 |
| --- | --- |
| `Id` | 中间表自增主键 |
| `FromWechat` | 这条动态属于哪个微信号（该账号的昵称） |
| `FromWechatWxId` | 这条动态属于哪个微信号（该账号的 wxid） |
| `Tid` | 动态 id（有符号）。和点赞、评论、媒体表关联请用这个。请按 64 位整数读写 |
| `TidUnsigned` | 同一 `Tid` 的无符号十进制字符串，便于展示和对照 |
| `UserName` | 发布者 username。显示名可再联 `wechat_contact` |
| `NickName` | 发布者显示名（备注优先，否则为动态里的昵称） |
| `CreateTime` | 发布时间（Unix 秒）。这是动态发出的时间，不是进中间表的时间 |
| `ContentType` | 动态形态。已知取值见下表。不要用「某段文案在不在」判断类型 |
| `ContentDesc` | 正文。纯图片或纯视频时可能为空 |
| `LikeFlag` | 我是否赞过：`1` 表示是 |
| `LikeCount` | 点赞人数（写入时按点赞名单条数填写，方便单表阅读） |
| `CommentCount` | 评论条数（写入时按评论名单条数填写，方便单表阅读） |
| `Content` | 动态内容的 XML 快照（含媒体列表等）。日常查点赞、评论不必再解析本字段，请直接查另外三张表 |
| `ContentHash` | `Content` 的 SHA256（小写十六进制），用来判断旧帖是否被改写。为空表示这是早期写入、尚未计算 |
| `IsDeleted` | 软删除：原数据里这条动态已经不在了（作者删除或缓存被清理）。历史和互动数据会保留；如果它后来又出现，会自动改回未删除 |
| `Title` | 链接 / 分享卡片的标题。图片、纯文字、视频通常为空 |
| `Description` | 链接 / 分享卡片的描述 |
| `ContentUrl` | 链接 / 分享卡片的跳转地址 |
| `PoiCity` | 位置里的城市名。很多动态只有空的位置占位，本列可能为空 |
| `LocationLatitude` | 位置纬度（原值）。请注意：微信有时会把经纬度放反（例如北京出现纬度 116、经度 39 的情况），这里保留原值，不做交换 |
| `LocationLongitude` | 位置经度（原值，同上，可能被放反） |
| `SourceUserName` | 转发来源 username，可能为空 |
| `SourceNickName` | 转发来源显示名 |
| `PublicUserName` | 与公众号相关的 username，可能为空 |
| `PublicBrandContactType` | 与公众号相关的类型值 |
| `IsPrivate` | 是否仅自己可见 |
| `IsTop` | 是否置顶 |
| `SightFolded` | 是否被折叠 |
| `ShowFlag` | 展示位标志（原值） |
| `FinderKind` | 视频号形态：`0` 普通朋友圈，`1` 视频号动态，`2` 视频号直播 |
| `FinderId` | 视频号的 objectId，或直播的 finderLiveID |
| `FinderNickName` | 视频号 / 直播的作者昵称 |
| `FinderUsername` | 视频号 / 直播的作者 username（常见 `@finder` 后缀） |
| `FinderDesc` | 视频号 / 直播的文案 |
| `FinderUrl` | 视频号媒体文件链接，可能带过期参数；实测脱离微信直接请求不一定能下载，直播通常没有播放直链，本列为空 |
| `FinderCoverUrl` | 视频号 / 直播封面链接，一般可直接下载 |
| `FinderNonceId` | 视频号的 nonceId，留作后续拼接页面链接 |
| `MediaCount` | 本条动态的媒体数量。明细在 `wechat_sns_media`。视频号媒体结构不同，暂不写入媒体表，请看上面的 `Finder*` 字段 |

`ContentType` 已知取值（其余请视为原有类型码，不要猜）：

| 取值 | 含义 |
| --- | --- |
| 1 | 图片 |
| 2 | 纯文字 |
| 3 | 链接 / 分享 |
| 4 | 音乐（样本较少） |
| 5 | App / 音乐分享（样本较少） |
| 7 / 15 | 视频（尚未按文件逐项坐实） |
| 28 | 视频号动态 |
| 30 | 尚未核实 |
| 34 | 视频号直播 |
| 54 | Live Photo |

## 6. `wechat_sns_like`（朋友圈点赞）

一条动态下的一个赞，就是这里的一行。用 `Tid` 回到 `wechat_sns_post`，用 `UserName` 去联通讯录。

**唯一键：** `FromWechatWxId + Tid + UserName`

| 字段名 | 说明 |
| --- | --- |
| `Id` | 中间表自增主键 |
| `FromWechat` | 这条记录属于哪个微信号（该账号的昵称） |
| `FromWechatWxId` | 这条记录属于哪个微信号（该账号的 wxid） |
| `Tid` | 所属动态 id，与 `wechat_sns_post.Tid` 相同。请按 64 位整数读写 |
| `TidUnsigned` | 同一 `Tid` 的无符号十进制字符串 |
| `CommentId` | 这条点赞自己的 id。请按 64 位整数读写 |
| `UserName` | 点赞人的 username |
| `NickName` | 点赞人显示名（写入时备注优先，否则保留来源昵称）。也可再联 `wechat_contact` |
| `CreateTime` | 点赞时间（Unix 秒） |
| `CommentFlag` | 标志位，具体含义尚未逐项核实 |

## 7. `wechat_sns_comment`（朋友圈评论）

一条动态下的一条评论，就是这里的一行。如果这是对另一条评论的回复，`RefCommentId` 会指向父评；`0` 表示不是回复。

**唯一键：** `FromWechatWxId + Tid + CommentId`

| 字段名 | 说明 |
| --- | --- |
| `Id` | 中间表自增主键 |
| `FromWechat` | 这条记录属于哪个微信号（该账号的昵称） |
| `FromWechatWxId` | 这条记录属于哪个微信号（该账号的 wxid） |
| `Tid` | 所属动态 id，与 `wechat_sns_post.Tid` 相同。请按 64 位整数读写 |
| `TidUnsigned` | 同一 `Tid` 的无符号十进制字符串 |
| `CommentId` | 本条评论的 id。请按 64 位整数读写 |
| `UserName` | 评论人的 username |
| `NickName` | 评论人显示名（写入时备注优先，否则保留来源昵称）。也可再联 `wechat_contact` |
| `ToUserName` | 评论对象的 username，可能为空 |
| `ToNickName` | 评论对象的显示名，可能为空。可再按 `wechat_contact` 补 |
| `Content` | 评论正文 |
| `Type` | 条目类型。本表来自评论名单，一般为 `2` |
| `CreateTime` | 评论时间（Unix 秒） |
| `RefCommentId` | 父评 id。`0` 表示不是回复。请按 64 位整数读写 |
| `RefUserName` | 父评作者的 username |
| `RefContent` | 父评正文。不是每条回复都能带上，可能为空 |
| `CommentFlag` | 标志位，具体含义尚未逐项核实 |
| `IsDeleted` | 是否已删除 |

## 8. `wechat_sns_media`（朋友圈媒体）

一条动态可以有好几张图、好几个视频，每张图或每个视频在这里占一行。用 `Tid` 回到动态，用 `Idx` 看出顺序（从 0 开始）。

**唯一键：** `FromWechatWxId + Tid + Idx`

请注意：视频号动态的媒体结构跟普通朋友圈不一样，**暂不写入本表**。视频号请看 `wechat_sns_post` 里的 `FinderKind` / `FinderId` / `FinderUrl` / `FinderCoverUrl` 等字段。

表里的 `Url`、`ThumbUrl` 等是带时效参数的地址，**会过期**，不适合当长期文件路径。如果开启了朋友圈媒体下载并且下载成功，本地文件路径会写在 `LocalPath`。

| 字段名 | 说明 |
| --- | --- |
| `Id` | 中间表自增主键 |
| `FromWechat` | 这条记录属于哪个微信号（该账号的昵称） |
| `FromWechatWxId` | 这条记录属于哪个微信号（该账号的 wxid） |
| `Tid` | 所属动态 id，与 `wechat_sns_post.Tid` 相同。请按 64 位整数读写 |
| `TidUnsigned` | 同一 `Tid` 的无符号十进制字符串 |
| `Idx` | 在媒体列表中的顺序，从 0 开始 |
| `MediaId` | 媒体 id |
| `MediaType` | 媒体类型。本机常见：`2` 图片，`4` / `5` / `6` 视频类，`3` 其它 |
| `SubType` | 媒体子类型 |
| `Title` | 媒体标题，少数视频才有 |
| `Description` | 媒体描述，例如视频封面文案 |
| `Url` | 原图 / 原视频地址，带时效参数，会过期 |
| `UrlMd5` | 媒体文件 MD5 |
| `VideoMd5` | 视频文件 MD5，仅视频可能有值 |
| `ThumbUrl` | 缩略图地址，带时效参数，会过期 |
| `LowBandUrl` | 低码率视频地址，部分视频才有 |
| `UhdUrl` | 超清视频地址，部分视频才有 |
| `Width` | 宽度（像素） |
| `Height` | 高度（像素） |
| `FileSize` | 文件大小（字节），可能为空。请按 64 位整数读写 |
| `VideoDuration` | 视频时长（秒）；图片为 0 |
| `IsLivePhoto` | 是否带 Live Photo 段 |
| `LocalPath` | 下载到本机后的路径。只有开启朋友圈媒体下载且下载成功时才会写入 |
| `UrlToken` | 请求原图 / 原视频地址时需要带上的 token 参数 |
| `UrlKey` | 请求原图 / 原视频地址时可能用到的 key 参数 |
| `ThumbToken` | 请求缩略图地址时需要带上的 token 参数 |
| `VideoKey` | 视频解密用的 key 参数 |
| `DownloadStatus` | 下载状态：`0` 未处理 / `1` 成功 / `2` 永久失败（例如地址过期、404、内容不是图片）/ `3` 跳过（非图片）。永久失败不会再重试；如果这条动态后来被改写重建，新行会重新从 0 开始 |
| `DownloadAttempts` | 已尝试下载次数（只对可重试的错误累加，到 3 次会转为永久失败） |
| `DownloadError` | 最近一次失败原因（截断到 200 字），成功时会清空 |
| `DownloadedAt` | 下载成功时间（Unix 秒）；未成功为 0 |

## 9. 几段拿来就能跑的查询

下面按默认的 SQLite 来写。如果你改成了 MySQL，把字符串拼接按你的库改一下即可。请把 `:wxid`、`:chat`、`:room`、`:tid` 换成你的实际值。

查某个会话的最近消息，并带上发送者显示名：

```sql
SELECT
  m.MsgCreateTime,
  m.MessageType,
  m.Content,
  m.FilePath,
  m.IsSelf,
  COALESCE(NULLIF(c.Remark, ''), c.NickName, m.SenderWxId) AS sender_name
FROM wechat_message m
LEFT JOIN wechat_contact c
  ON c.FromWechatWxId = m.FromWechatWxId
 AND c.UserName = m.SenderWxId
WHERE m.FromWechatWxId = :wxid
  AND m.ChatUsername = :chat
ORDER BY m.SortSeq DESC
LIMIT 50;
```

列出某个群的成员，以及群名片和通讯录里的备注 / 昵称：

```sql
SELECT
  r.MemberUsername,
  r.GroupNickname,
  COALESCE(NULLIF(c.Remark, ''), c.NickName, r.MemberUsername) AS display_name,
  CASE WHEN r.MemberUsername = r.Owner THEN 1 ELSE 0 END AS is_owner
FROM wechat_chat_room_member r
LEFT JOIN wechat_contact c
  ON c.FromWechatWxId = r.FromWechatWxId
 AND c.UserName = r.MemberUsername
WHERE r.FromWechatWxId = :wxid
  AND r.RoomUsername = :room
ORDER BY is_owner DESC, display_name;
```

看一条朋友圈，以及它的媒体、点赞和评论：

```sql
-- 动态本身
SELECT Tid, UserName, NickName, ContentDesc, LikeCount, CommentCount, MediaCount, CreateTime
FROM wechat_sns_post
WHERE FromWechatWxId = :wxid
  AND Tid = :tid
  AND IFNULL(IsDeleted, 0) = 0;

-- 媒体（按顺序）
SELECT Idx, MediaType, LocalPath, Url, Width, Height, VideoDuration
FROM wechat_sns_media
WHERE FromWechatWxId = :wxid
  AND Tid = :tid
ORDER BY Idx;

-- 点赞
SELECT UserName, NickName, CreateTime
FROM wechat_sns_like
WHERE FromWechatWxId = :wxid
  AND Tid = :tid;

-- 评论（含回复关系）
SELECT CommentId, UserName, NickName, Content, RefCommentId, RefUserName, CreateTime
FROM wechat_sns_comment
WHERE FromWechatWxId = :wxid
  AND Tid = :tid
  AND IFNULL(IsDeleted, 0) = 0
ORDER BY CreateTime;
```

先把通讯录和消息跑通，Bot 的主干就立住了。群成员和朋友圈是同一套库里的邻桌，需要时再联过来即可。
