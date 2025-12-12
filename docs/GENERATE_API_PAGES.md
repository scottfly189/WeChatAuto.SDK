# API 页面生成说明

由于需要创建大量的 API 页面，建议使用以下方法快速生成：

## 已创建的页面

以下页面已经创建完成：
- `api.html` - API 概览页面
- `api-wechat-client.html` - WeChatClient 详细页面（示例）
- `api-utils.html` - 工具类页面
- `api-enums.html` - 枚举类型页面
- `api-configs.html` - 配置类页面

## 需要创建的页面

按照左侧菜单的顺序，还需要创建以下页面：

### 核心组件页面
1. `api-wechat-client-factory.html` - WeChatClientFactory
2. `api-wechat-main-window.html` - WeChatMainWindow
3. `api-sub-win-list.html` - SubWinList
4. `api-sub-win.html` - SubWin
5. `api-navigation.html` - Navigation
6. `api-moments.html` - Moments
7. `api-message-bubble-list.html` - MessageBubbleList
8. `api-message-bubble.html` - MessageBubble
9. `api-conversation-list.html` - ConversationList
10. `api-chat-header.html` - ChatHeader
11. `api-chat-content.html` - ChatContent
12. `api-chat-body.html` - ChatBody
13. `api-address-book-list.html` - AddressBookList
14. `api-sender.html` - Sender

### 公共类页面
1. `api-models.html` - 数据模型 (Models)
2. `api-common-utils.html` - 公共工具类 (Common Utils)

## 页面模板

每个页面都应该遵循以下结构：

1. **HTML 头部** - 包含侧边栏导航（复制自 `api-wechat-client.html`）
2. **类概述** - 类的用途和功能说明
3. **命名空间** - 类的命名空间
4. **继承关系** - 实现的接口或继承的类
5. **属性列表** - 表格形式列出所有公共属性
6. **方法列表** - 详细列出所有公共方法，包括参数和返回值
7. **使用示例** - 提供实际可运行的代码示例
8. **相关类型** - 链接到相关的其他 API 页面

## 快速生成方法

1. 复制 `api-wechat-client.html` 作为模板
2. 修改页面标题和类名
3. 更新侧边栏中的 active 状态
4. 根据源代码文件填充类的详细信息
5. 添加使用示例

## 侧边栏导航

所有页面的侧边栏导航应该保持一致，确保用户可以方便地在不同页面间跳转。

## 注意事项

- 所有代码示例必须使用 C# 语法高亮
- 方法签名应该完整准确
- 参数说明要清晰
- 返回值类型要明确
- 使用示例要实际可运行

