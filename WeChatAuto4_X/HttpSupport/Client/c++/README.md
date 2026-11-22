# Documentation for OpenAPI Petstore
This is a client generator for microcontrollers on the Espressif32 platform and the Arduino framework
After the client have been generated, you have to change these following variables:
- root.cert | Provide your service root certificate.
- src/main.cpp | Change wifi name
- src/main.cpp | Change wifi password
- lib/service/AbstractService.h | Change to your url

# Documentation for WECHATAUTO.SDK v1 Tiny client cpp (Arduino) 

The project is structured like this:
```
samples/client/petstore/tiny/cpp/
├── lib
│   ├── Models
│   ├── service
│   └── TestFiles
├── platformio.ini
├── pre_compiling_bourne.py
├── README.md
├── root.cert
├── src
│   └── main.cpp
└── test
    └── RunTests.cpp
```

All URIs are relative to http://localhosthttp://localhost

### ClientsApi
|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|*clientsClickNotifyIcon* | *POST* /api/v1/clients/{clientId}/notify-icon/click | 点击任务栏微信图标，按索引或微信名称定位。.|
|*clientsGetAll* | *GET* /api/v1/clients | 获取全部微信客户端的概要信息列表。.|
|*clientsGetInfo* | *GET* /api/v1/clients/{clientId} | 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。.|
|*clientsGetOwnerInfo* | *GET* /api/v1/clients/{clientId}/owner-info | 获取本微信的个人信息（头像文件位置、wxid、昵称）。.|
|*clientsGetStorageRoot* | *GET* /api/v1/clients/{clientId}/storage-root | 通过界面自动读取微信存储位置（VIP，一般不需要，SDK 会自动读取并缓存）。.|
|*clientsGetVersion* | *GET* /api/v1/clients/{clientId}/version | 获取本微信的版本号。.|
|*clientsSaveAvatar* | *POST* /api/v1/clients/{clientId}/avatar | 保存本微信头像到指定路径。.|

### ConversationsApi
|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|*conversationsGetAllConversations* | *GET* /api/v1/clients/{clientId}/conversations | 获取会话列表所有会话的标题。.|
|*conversationsGetVisibleConversations* | *GET* /api/v1/clients/{clientId}/conversations/visible | 获取可见会话列表（含名称、未读消息数、头像等）。.|
|*conversationsGetVisibleTitles* | *GET* /api/v1/clients/{clientId}/conversations/visible-titles | 获取会话列表可见会话的标题。.|
|*conversationsLocate* | *POST* /api/v1/clients/{clientId}/conversations/locate | 定位会话，滚动会话列表使指定会话可见。.|
|*conversationsSearch* | *POST* /api/v1/clients/{clientId}/conversations/search | 搜索好友/群聊。.|
|*conversationsSetDoNotDisturb* | *POST* /api/v1/clients/{clientId}/conversations/set-do-not-disturb | 设置会话消息免打扰。.|
|*conversationsSetTopMost* | *POST* /api/v1/clients/{clientId}/conversations/set-top-most | 设置会话置顶。.|

### DatabaseApi
|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|*databaseGetContact* | *GET* /api/v1/clients/{clientId}/database/contacts/{userName} | 按 username（wxid / 群 id）取一条联系人。.|
|*databaseGetContactByShowName* | *GET* /api/v1/clients/{clientId}/database/contacts/by-show-name/first | 按显示名取第一条联系人（重名时取 UserName 升序第一条）。.|
|*databaseGetContacts* | *GET* /api/v1/clients/{clientId}/database/contacts | 获取当前微信号全部联系人（不含已删除）。.|
|*databaseGetContactsByShowName* | *GET* /api/v1/clients/{clientId}/database/contacts/by-show-name | 按显示名取联系人；重名时返回多个。.|
|*databaseGetMaxSortSeq* | *GET* /api/v1/clients/{clientId}/database/messages/max-sort-seq | 取当前微信号消息的最大 SortSeq（增量水位）。.|
|*databaseGetMessages* | *GET* /api/v1/clients/{clientId}/database/messages | 取指定会话最近 N 条消息（从新到旧）。.|
|*databaseGetNewMessages* | *GET* /api/v1/clients/{clientId}/database/messages/new | 取 SortSeq 大于指定值的新消息（从旧到新）。.|
|*databaseGetRecentMessages* | *GET* /api/v1/clients/{clientId}/database/messages/recent | 取指定会话最近 N 条消息（从旧到新，适合 LLM 上下文）。.|
|*databaseGetRoomByShowName* | *GET* /api/v1/clients/{clientId}/database/rooms/by-show-name/first | 按群的显示名取第一条群。.|
|*databaseGetRoomMembers* | *GET* /api/v1/clients/{clientId}/database/rooms/{roomUsername}/members | 按群 username 取成员列表。.|
|*databaseGetRoomMembersByShowName* | *GET* /api/v1/clients/{clientId}/database/rooms/by-show-name/members | 按群的显示名取成员列表；重名时取第一条群。.|
|*databaseGetRooms* | *GET* /api/v1/clients/{clientId}/database/rooms | 获取当前微信号全部群聊。.|
|*databaseGetRoomsByLocalType* | *GET* /api/v1/clients/{clientId}/database/rooms/by-local-type | 按联系人 LocalType 过滤群聊（本机观察群多为 2）。.|
|*databaseGetRoomsByShowName* | *GET* /api/v1/clients/{clientId}/database/rooms/by-show-name | 按群的显示名取群；重名时返回多个。.|
|*databaseGetSnsPost* | *GET* /api/v1/clients/{clientId}/database/sns/posts/{tid} | 按 tid 取一条朋友圈（含媒体、点赞、评论）。.|
|*databaseGetSnsPosts* | *GET* /api/v1/clients/{clientId}/database/sns/posts | 取最近 N 条朋友圈（含媒体、点赞、评论）。.|
|*databaseGetSnsPostsByDate* | *GET* /api/v1/clients/{clientId}/database/sns/posts/by-date | 取某一天的朋友圈（按中国标准时间 UTC+8 的日历日）。.|
|*databaseGetSnsPostsByShowName* | *GET* /api/v1/clients/{clientId}/database/sns/posts/by-show-name | 按发布者显示名取朋友圈；重名会一并返回。.|
|*databaseGetSnsPostsByUserName* | *GET* /api/v1/clients/{clientId}/database/sns/posts/by-user | 取某个发布者（真实 UserName / wxid）的朋友圈。.|
|*databaseQuery* | *POST* /api/v1/clients/{clientId}/database/query | 执行一条 SELECT 查询（仅查询，不允许写操作；不会自动追加当前微信号条件）。.|
|*databaseSearchAllMessages* | *GET* /api/v1/clients/{clientId}/database/messages/search-all | 按正文关键字跨所有微信号搜索消息。.|
|*databaseSearchContacts* | *GET* /api/v1/clients/{clientId}/database/contacts/search | 按关键字模糊搜索联系人（匹配 UserName / NickName / Remark / Alias）。.|
|*databaseSearchMessages* | *GET* /api/v1/clients/{clientId}/database/messages/search | 按正文关键字搜索当前微信号消息。.|

### FriendsApi
|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|*friendsAdd* | *POST* /api/v1/clients/{clientId}/friends/add | 通过手机号码/微信号查找并添加好友。.|
|*friendsAddOrUpdateFriend* | *POST* /api/v1/clients/{clientId}/friends/cache | 手动新增或修改缓存中的好友对象。.|
|*friendsCloseAddWindow* | *POST* /api/v1/clients/{clientId}/friends/add-window/close | 关闭新增朋友窗口。.|
|*friendsGetAllFriendNames* | *GET* /api/v1/clients/{clientId}/friends/names | 获取所有好友名称列表（有备注优先备注名）。.|
|*friendsGetAllFriends* | *GET* /api/v1/clients/{clientId}/friends | 获取所有好友的信息列表（默认优先从缓存读取）。.|
|*friendsGetFriendByWxidFromCache* | *GET* /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存获取好友信息。.|
|*friendsGetFriendFromCache* | *GET* /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存获取单个好友信息。.|
|*friendsGetFriendListFromCache* | *GET* /api/v1/clients/{clientId}/friends/cache | 获取缓存中的全部好友信息列表。.|
|*friendsGetFriendsFromCache* | *GET* /api/v1/clients/{clientId}/friends/cache/by-name/{who} | 按名称从缓存获取好友列表（适用于昵称重复场景）。.|
|*friendsOpenAddWindow* | *POST* /api/v1/clients/{clientId}/friends/add-window/open | 打开新增朋友窗口。.|
|*friendsPassAll* | *POST* /api/v1/clients/{clientId}/friends/pass-all | 立即批量通过当前待处理的新好友申请（不启动持续监听）。.|
|*friendsPauseAutoAccept* | *POST* /api/v1/clients/{clientId}/friends/auto-accept/pause | 暂停新好友自动通过监听。.|
|*friendsRemoveFriend* | *DELETE* /api/v1/clients/{clientId}/friends/{nickName} | 从通讯录移除好友。.|
|*friendsRemoveFriendByWxidFromCache* | *DELETE* /api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid} | 按 wxid 从缓存移除好友。.|
|*friendsRemoveFriendFromCache* | *DELETE* /api/v1/clients/{clientId}/friends/cache/{who} | 按名称从缓存移除好友。.|
|*friendsResumeAutoAccept* | *POST* /api/v1/clients/{clientId}/friends/auto-accept/resume | 恢复新好友自动通过监听。.|
|*friendsStartAutoAccept* | *POST* /api/v1/clients/{clientId}/friends/auto-accept/start | 启动新好友自动通过监听（按关键词/后缀/标签规则）。.|
|*friendsStopAutoAccept* | *POST* /api/v1/clients/{clientId}/friends/auto-accept/stop | 停止新好友自动通过监听。.|

### GroupsApi
|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|*groupsAddMembers* | *POST* /api/v1/clients/{clientId}/groups/members | 添加群聊成员（适用于自有群）。.|
|*groupsAddMembersToFriends* | *POST* /api/v1/clients/{clientId}/groups/add-members-to-friends | 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。.|
|*groupsChangeMemo* | *PUT* /api/v1/clients/{clientId}/groups/memo | 修改群备注（仅自己可见）。.|
|*groupsChangeName* | *PUT* /api/v1/clients/{clientId}/groups/name | 修改群名（适用于自有群）。.|
|*groupsChangeNickName* | *PUT* /api/v1/clients/{clientId}/groups/nickname | 修改自己在群中的昵称。.|
|*groupsCreate* | *POST* /api/v1/clients/{clientId}/groups | 创建群聊，已存在则打开群聊。.|
|*groupsGetMembers* | *GET* /api/v1/clients/{clientId}/groups/members | 获取群聊成员列表。.|
|*groupsGetOwner* | *GET* /api/v1/clients/{clientId}/groups/{groupName}/owner | 获取群主昵称。.|
|*groupsInvite* | *POST* /api/v1/clients/{clientId}/groups/invite | 邀请群聊成员（适用于外部群）。.|
|*groupsIsOwner* | *GET* /api/v1/clients/{clientId}/groups/{groupName}/is-owner | 判断群聊是否为自有群。.|
|*groupsQuit* | *DELETE* /api/v1/clients/{clientId}/groups/{groupName} | 退出群聊。.|
|*groupsRemoveMembers* | *DELETE* /api/v1/clients/{clientId}/groups/members | 移除群聊成员（适用于自有群）。.|
|*groupsUpdateNotice* | *PUT* /api/v1/clients/{clientId}/groups/notice | 更新群公告（仅适用于自有群）。.|

### MessagesApi
|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|*messagesCloneVoice* | *POST* /api/v1/clients/{clientId}/messages/clone-voice | 声音复刻：上传参考音频创建一个自定义音色，返回 voice_id。.|
|*messagesFocusInput* | *POST* /api/v1/clients/{clientId}/messages/focus-input | 聚焦当前窗口的消息输入区。.|
|*messagesForwardMultiple* | *POST* /api/v1/clients/{clientId}/messages/forward-multiple | 转发多条消息（默认合并转发）。.|
|*messagesForwardSingle* | *POST* /api/v1/clients/{clientId}/messages/forward-single | 转发单条消息（message 与 chat_simple_message 二选一）。.|
|*messagesGetHistory* | *GET* /api/v1/clients/{clientId}/messages/history | 获取聊天历史（支持按日期或时间段查询）。.|
|*messagesGetOnlyTitle* | *GET* /api/v1/clients/{clientId}/messages/only-title | 获取当前聊天窗口的标题名称。.|
|*messagesGetTitle* | *GET* /api/v1/clients/{clientId}/messages/title | 获取当前聊天窗口的标题对象。.|
|*messagesPull* | *POST* /api/v1/clients/{clientId}/messages/pull | 轮询拉取指定会话的新消息（用于自己实现轮询）。.|
|*messagesReference* | *POST* /api/v1/clients/{clientId}/messages/reference | 引用当前窗口的好友消息。.|
|*messagesReferenceLast* | *POST* /api/v1/clients/{clientId}/messages/reference-last | 引用当前窗口最后一条消息。.|
|*messagesSend* | *POST* /api/v1/clients/{clientId}/messages/send | 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。.|
|*messagesSendAndClose* | *POST* /api/v1/clients/{clientId}/messages/send-and-close | 发送文本消息后关闭微信窗口（指定对象或当前聊天窗口，支持 @ 与引用）。.|
|*messagesSendByWxid* | *POST* /api/v1/clients/{clientId}/messages/send-by-wxid | 按联系人 UserName（wxid / 群 id）发送文本。.|
|*messagesSendEmoji* | *POST* /api/v1/clients/{clientId}/messages/send-emoji | 发送表情（按名称/描述/索引）。.|
|*messagesSendEmojiAndClose* | *POST* /api/v1/clients/{clientId}/messages/send-emoji-and-close | 发送表情后关闭微信窗口（按名称/描述/索引）。.|
|*messagesSendEmojiByWxid* | *POST* /api/v1/clients/{clientId}/messages/send-emoji-by-wxid | 按联系人 UserName（wxid / 群 id）发送表情。.|
|*messagesSendFile* | *POST* /api/v1/clients/{clientId}/messages/send-file | 发送文件。.|
|*messagesSendFileAndClose* | *POST* /api/v1/clients/{clientId}/messages/send-file-and-close | 发送文件后关闭微信窗口。.|
|*messagesSendFileByWxid* | *POST* /api/v1/clients/{clientId}/messages/send-file-by-wxid | 按联系人 UserName（wxid / 群 id）发送文件。.|
|*messagesSendVideoChat* | *POST* /api/v1/clients/{clientId}/messages/send-video-chat | 发起单人视频聊天。.|
|*messagesSendVoiceChat* | *POST* /api/v1/clients/{clientId}/messages/send-voice-chat | 发起单人语音聊天。.|
|*messagesSendVoiceChats* | *POST* /api/v1/clients/{clientId}/messages/send-voice-chats | 发起多人语音聊天（群聊）。.|
|*messagesSendVoiceMessage* | *POST* /api/v1/clients/{clientId}/messages/send-voice-message | 发送语音消息（需虚拟声卡 Cable input/Cable output）。.|
|*messagesSendVoiceMessageAndClose* | *POST* /api/v1/clients/{clientId}/messages/send-voice-message-and-close | 发送语音消息后关闭微信窗口（需虚拟声卡 Cable input/Cable output）。.|
|*messagesSendVoiceMessageByWxid* | *POST* /api/v1/clients/{clientId}/messages/send-voice-message-by-wxid | 按联系人 UserName（wxid / 群 id）发送语音文件。.|
|*messagesSendVoiceMessageTts* | *POST* /api/v1/clients/{clientId}/messages/send-voice-message-tts | 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。.|
|*messagesSendVoiceMessageTtsAndClose* | *POST* /api/v1/clients/{clientId}/messages/send-voice-message-tts-and-close | 文字转语音发送后关闭微信窗口（默认阿里千问 Qwen-Audio-TTS）。.|
|*messagesSendVoiceMessageTtsByWxid* | *POST* /api/v1/clients/{clientId}/messages/send-voice-message-tts-by-wxid | 按联系人 UserName（wxid / 群 id）用 TTS 发送语音。.|
|*messagesTap* | *POST* /api/v1/clients/{clientId}/messages/tap | 拍一拍当前聊天窗口的好友。.|

### MomentsApi
|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|*momentsAdd* | *POST* /api/v1/clients/{clientId}/moments/add | 发朋友圈（图文，多图需在同一目录）。.|
|*momentsClose* | *POST* /api/v1/clients/{clientId}/moments/close | 关闭朋友圈。.|
|*momentsOpen* | *POST* /api/v1/clients/{clientId}/moments/open | 打开朋友圈（已打开则提到最前）。.|
|*momentsRemove* | *POST* /api/v1/clients/{clientId}/moments/remove | 删除自己发的朋友圈（按文字内容）。.|

### NavigationApi
|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|*navigationClose* | *POST* /api/v1/clients/{clientId}/navigation/close | 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。.|
|*navigationSwitch* | *POST* /api/v1/clients/{clientId}/navigation/switch | 切换导航栏。.|

### WindowApi
|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|*windowClose* | *POST* /api/v1/clients/{clientId}/window/close | 关闭本微信窗口。.|
|*windowCloseSearchWindow* | *POST* /api/v1/clients/{clientId}/window/close-search-window | 关闭查询窗口（已打开则关闭）。.|
|*windowCloseSubWindow* | *POST* /api/v1/clients/{clientId}/window/close-sub-window | 关闭指定对象的子窗口（已存在则关闭）。.|
|*windowFocus* | *POST* /api/v1/clients/{clientId}/window/focus | 使主窗口获取焦点。.|
|*windowGetHandler* | *GET* /api/v1/clients/{clientId}/window/handler | 获取本微信窗口句柄。.|
|*windowGetProcessId* | *GET* /api/v1/clients/{clientId}/window/process-id | 获取本微信窗口进程 ID。.|
|*windowMaximize* | *POST* /api/v1/clients/{clientId}/window/maximize | 最大化微信窗口。.|
|*windowOpen* | *POST* /api/v1/clients/{clientId}/window/open | 打开本微信窗口。.|
|*windowOpenSubWindow* | *POST* /api/v1/clients/{clientId}/window/open-sub-window | 打开指定对象的子窗口。.|
|*windowPin* | *POST* /api/v1/clients/{clientId}/window/pin | 置顶微信窗口。.|
|*windowRestore* | *POST* /api/v1/clients/{clientId}/window/restore | 还原微信窗口。.|
|*windowUnpin* | *POST* /api/v1/clients/{clientId}/window/unpin | 取消置顶微信窗口。.|


## What are the Model files for the data structures/objects?
|Class | Description|
|------------- | -------------|
|*AddFriendsOptions* | 增加朋友选项|
|*AddFriendsRequest* | 通过手机号码或微信号添加好友。|
|*AddGroupMemberToFriendsRequest* | 从群聊中添加好友。|
|*AddMomentsRequest* | 发朋友圈。|
|*ChangeGroupMemoRequest* | 修改群备注。|
|*ChangeGroupNickNameRequest* | 修改自己在群中的昵称。|
|*ChatRefer* | 消息引用|
|*ChatSimpleMessage* | |
|*ChatType* | 聊天类型|
|*CloneVoiceRequest* | 声音复刻。|
|*Color* | |
|*ColorPalette* | |
|*CreateGroupRequest* | 创建群聊。|
|*DatabaseQueryRequest* | 数据库只读查询。仅支持 SELECT（含 WITH ... SELECT），不会自动追加当前微信号条件。|
|*ForwardMessageTypeEnums* | |
|*ForwardMultipleMessageRequest* | 转发多条消息。|
|*ForwardSingleMessageRequest* | 转发单条消息（message 与 chat_simple_message 二选一）。|
|*FriendInfo* | 个人信息|
|*FriendKeywordRuleRequest* | 单个关键词对应的备注后缀和标签。|
|*FriendSearchResultEnums* | 通过wxid查询，或者通过手机号查询结果枚举|
|*GroupMemberRequest* | 添加/移除群成员。group_name 为空时作用于当前焦点群聊。|
|*Image* | |
|*ImageFormat* | |
|*InviteGroupMemberRequest* | 邀请群成员（外部群）。|
|*LocateConversationRequest* | 定位会话。|
|*MomentsOptions* | 朋友圈选项|
|*NavigationType* | 导航栏类型|
|*NotifyIconClickRequest* | 点击任务栏微信图标。index 与 wechat_name 二选一。|
|*PassedAllNewFriendRequest* | 批量通过新好友申请 / 启动自动通过监听。|
|*PixelFormat* | |
|*PropertyItem* | |
|*PullMessagesRequest* | 轮询拉取消息。|
|*ReferenceRequest* | 引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。|
|*RemoveMomentsRequest* | 删除自己发的朋友圈（按文字内容）。|
|*RenameGroupRequest* | 修改群名。|
|*SaveAvatarRequest* | 保存头像到指定路径。|
|*SearchConversationRequest* | 搜索会话。|
|*SendEmojiByWxidRequest* | 按联系人 UserName 发送表情。emoji 与 index 二选一（优先 index）。|
|*SendEmojiRequest* | 发送表情。emoji 与 index 二选一（优先使用 index）。|
|*SendFileByWxidRequest* | 按联系人 UserName 发送文件。|
|*SendFileRequest* | 发送文件。who 为空时发送给当前焦点聊天窗口。|
|*SendMessageByWxidRequest* | 按联系人 UserName（wxid / 群 id）发送文本。user_name 必填。|
|*SendMessageRequest* | 发送文本消息。who 为空时发送给当前焦点聊天窗口。|
|*SendVoiceChatsRequest* | 发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。|
|*SendVoiceMessageByWxidRequest* | 按联系人 UserName 发送语音文件。|
|*SendVoiceMessageRequest* | 发送语音消息。who 为空时发送给当前焦点聊天窗口。|
|*SendVoiceMessageTtsByWxidRequest* | 按联系人 UserName 用 TTS 发送语音。|
|*SendVoiceMessageTtsRequest* | 文字转语音发送。|
|*SetDoNotDisturbRequest* | 设置会话免打扰。who 为空时作用于当前窗口。|
|*SetTopMostRequest* | 设置会话置顶。who 为空时作用于当前窗口。|
|*Size* | |
|*SizeF* | |
|*SubWindowRequest* | 打开/关闭指定好友或群的子窗口。|
|*SwitchNavigationRequest* | 切换/关闭导航栏窗口。|
|*TapRequest* | 拍一拍。|
|*UpdateGroupNoticeRequest* | 更新群公告。|
|*VoiceOptions* | |
|*WhoRequest* | 仅携带 who（可为空，作用于当前焦点窗口）的请求。|

