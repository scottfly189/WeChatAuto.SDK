using System.Collections.Generic;
using WxAutoCommon.Models;

namespace WxAutoCommon.Utils
{
    /// <summary>
    /// 微信界面文本标识符集合
    /// </summary>
    public static class WeChatTextIdentifiers
    {
        public static string GetText(Dictionary<string, TextIdentifier> textIdentifiers, string key)
        {
            if (textIdentifiers.TryGetValue(key, out var textIdentifier))
            {
                switch (WxConfig.CurrentLanguage)
                {
                    case WxConfig.Language.Chinese:
                        return textIdentifier.Cn;
                    case WxConfig.Language.ChineseTraditional:
                        return textIdentifier.CnT;
                    case WxConfig.Language.English:
                        return textIdentifier.En;
                    default:
                        return textIdentifier.Cn;
                }
            }
            return "";
        }
        /// <summary>
        /// 微信主界面文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> WECHAT_MAIN = new Dictionary<string, TextIdentifier>
        {
            ["新的朋友"] = new TextIdentifier { Cn = "新的朋友", CnT = "", En = "" },
            ["添加朋友"] = new TextIdentifier { Cn = "添加朋友", CnT = "", En = "" },
            ["搜索结果"] = new TextIdentifier { Cn = "搜索：", CnT = "", En = "" },
            ["找不到相关账号或内容"] = new TextIdentifier { Cn = "找不到相关账号或内容", CnT = "", En = "" }
        };

        public static readonly Dictionary<string, TextIdentifier> WECHAT_SYSTEM = new Dictionary<string, TextIdentifier>
        {
            ["微信"] = new TextIdentifier { Cn = "微信", CnT = "", En = "" },
            ["任务栏"] = new TextIdentifier { Cn = "任务栏", CnT = "", En = "" },
            ["用户提示通知区域"] = new TextIdentifier { Cn = "用户提示通知区域", CnT = "", En = "" }
        };

        /// <summary>
        /// 微信聊天框文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> WECHAT_CHAT_BOX = new Dictionary<string, TextIdentifier>
        {
            ["查看更多消息"] = new TextIdentifier { Cn = "查看更多消息", CnT = "", En = "" },
            ["消息"] = new TextIdentifier { Cn = "消息", CnT = "", En = "" },
            ["表情"] = new TextIdentifier { Cn = "表情(Alt+E)", CnT = "", En = "" },
            ["发送文件"] = new TextIdentifier { Cn = "发送文件", CnT = "", En = "" },
            ["截图"] = new TextIdentifier { Cn = "截图", CnT = "", En = "" },
            ["聊天记录"] = new TextIdentifier { Cn = "聊天记录", CnT = "", En = "" },
            ["语音聊天"] = new TextIdentifier { Cn = "语音聊天", CnT = "", En = "" },
            ["视频聊天"] = new TextIdentifier { Cn = "视频聊天", CnT = "", En = "" },
            ["聊天信息"] = new TextIdentifier { Cn = "聊天信息", CnT = "", En = "" },
            ["发送"] = new TextIdentifier { Cn = "发送(S)", CnT = "", En = "" },
            ["置顶"] = new TextIdentifier { Cn = "置顶", CnT = "", En = "" },
            ["取消置顶"] = new TextIdentifier { Cn = "取消置顶", CnT = "", En = "" },
            ["最小化"] = new TextIdentifier { Cn = "最小化", CnT = "", En = "" },
            ["最大化"] = new TextIdentifier { Cn = "最大化", CnT = "", En = "" },
            ["向下还原"] = new TextIdentifier { Cn = "向下还原", CnT = "", En = "" },
            ["关闭"] = new TextIdentifier { Cn = "关闭", CnT = "", En = "" },
            ["以下为新消息"] = new TextIdentifier { Cn = "以下为新消息", CnT = "", En = "" },
            ["re_新消息按钮"] = new TextIdentifier { Cn = ".*?条新消息", CnT = "", En = "" }
        };

        /// <summary>
        /// 微信会话框文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> WECHAT_SESSION_BOX = new Dictionary<string, TextIdentifier>
        {
            // 聊天页面
            ["聊天记录"] = new TextIdentifier { Cn = "聊天记录", CnT = "", En = "" },
            ["会话"] = new TextIdentifier { Cn = "会话", CnT = "", En = "" },
            ["已置顶"] = new TextIdentifier { Cn = "已置顶", CnT = "", En = "" },
            ["文件传输助手"] = new TextIdentifier { Cn = "文件传输助手", CnT = "", En = "" },
            ["折叠的群聊"] = new TextIdentifier { Cn = "折叠的群聊", CnT = "", En = "" },
            ["发起群聊"] = new TextIdentifier { Cn = "发起群聊", CnT = "", En = "" },
            ["搜索"] = new TextIdentifier { Cn = "搜索", CnT = "", En = "" },
            ["re_条数"] = new TextIdentifier { Cn = @"\[\d+条\]", CnT = "", En = "" },
            ["清空"] = new TextIdentifier { Cn = "清空", CnT = "", En = "" },

            // 联系人页面
            ["添加朋友"] = new TextIdentifier { Cn = "添加朋友", CnT = "", En = "" },
            ["联系人"] = new TextIdentifier { Cn = "联系人", CnT = "", En = "" },
            ["通讯录管理"] = new TextIdentifier { Cn = "通讯录管理", CnT = "", En = "" },
            ["新的朋友"] = new TextIdentifier { Cn = "新的朋友", CnT = "", En = "" },
            ["公众号"] = new TextIdentifier { Cn = "公众号", CnT = "", En = "" },
            ["企业号"] = new TextIdentifier { Cn = "企业号", CnT = "", En = "" },
            ["群聊"] = new TextIdentifier { Cn = "群聊", CnT = "", En = "" },

            // 收藏页面
            ["分类"] = new TextIdentifier { Cn = "分类", CnT = "", En = "" },
            ["新建笔记"] = new TextIdentifier { Cn = "新建笔记", CnT = "", En = "" },
            ["全部收藏"] = new TextIdentifier { Cn = "全部收藏", CnT = "", En = "" },
            ["最近使用"] = new TextIdentifier { Cn = "最近使用", CnT = "", En = "" },
            ["链接"] = new TextIdentifier { Cn = "链接", CnT = "", En = "" },
            ["图片与视频"] = new TextIdentifier { Cn = "图片与视频", CnT = "", En = "" },
            ["笔记"] = new TextIdentifier { Cn = "笔记", CnT = "", En = "" },
            ["文件"] = new TextIdentifier { Cn = "文件", CnT = "", En = "" },
            ["分割线"] = new TextIdentifier { Cn = "分割线", CnT = "", En = "" },
            ["展开标签"] = new TextIdentifier { Cn = "展开标签", CnT = "", En = "" },
            ["折叠标签"] = new TextIdentifier { Cn = "折叠标签", CnT = "", En = "" },
            ["标签"] = new TextIdentifier { Cn = "标签", CnT = "", En = "" }
        };

        /// <summary>
        /// 微信导航框文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> WECHAT_NAVIGATION_BOX = new Dictionary<string, TextIdentifier>
        {
            ["导航"] = new TextIdentifier { Cn = "导航", CnT = "", En = "" },
            ["聊天"] = new TextIdentifier { Cn = "聊天", CnT = "", En = "" },
            ["通讯录"] = new TextIdentifier { Cn = "通讯录", CnT = "", En = "" },
            ["收藏"] = new TextIdentifier { Cn = "收藏", CnT = "", En = "" },
            ["聊天文件"] = new TextIdentifier { Cn = "聊天文件", CnT = "", En = "" },
            ["朋友圈"] = new TextIdentifier { Cn = "朋友圈", CnT = "", En = "" },
            ["搜一搜"] = new TextIdentifier { Cn = "搜一搜", CnT = "", En = "" },
            ["视频号"] = new TextIdentifier { Cn = "视频号", CnT = "", En = "" },
            ["看一看"] = new TextIdentifier { Cn = "看一看", CnT = "", En = "" },
            ["小程序面板"] = new TextIdentifier { Cn = "小程序面板", CnT = "", En = "" },
            ["手机"] = new TextIdentifier { Cn = "手机", CnT = "", En = "" },
            ["设置及其他"] = new TextIdentifier { Cn = "设置及其他", CnT = "", En = "" }
        };

        /// <summary>
        /// 表情窗口文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> EMOTION_WINDOW = new Dictionary<string, TextIdentifier>
        {
            ["添加的单个表情"] = new TextIdentifier { Cn = "添加的单个表情", CnT = "", En = "" }
        };

        /// <summary>
        /// 朋友圈隐私设置文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> MOMENT_PRIVACY = new Dictionary<string, TextIdentifier>
        {
            ["谁可以看"] = new TextIdentifier { Cn = "谁可以看", CnT = "", En = "" },
            ["公开"] = new TextIdentifier { Cn = "公开", CnT = "", En = "" },
            ["所有朋友可见"] = new TextIdentifier { Cn = "所有朋友可见", CnT = "", En = "" },
            ["私密"] = new TextIdentifier { Cn = "私密", CnT = "", En = "" },
            ["仅自己可见"] = new TextIdentifier { Cn = "仅自己可见", CnT = "", En = "" },
            ["白名单"] = new TextIdentifier { Cn = "选中的标签或朋友可见", CnT = "", En = "" },
            ["黑名单"] = new TextIdentifier { Cn = "选中的标签或朋友不可见", CnT = "", En = "" },
            ["完成"] = new TextIdentifier { Cn = "完成", CnT = "", En = "" },
            ["确定"] = new TextIdentifier { Cn = "确定", CnT = "", En = "" },
            ["取消"] = new TextIdentifier { Cn = "取消", CnT = "", En = "" }
        };

        /// <summary>
        /// 个人资料卡片文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> PROFILE_CARD = new Dictionary<string, TextIdentifier>
        {
            ["微信号"] = new TextIdentifier { Cn = "微信号：", CnT = "", En = "" },
            ["昵称"] = new TextIdentifier { Cn = "昵称：", CnT = "", En = "" },
            ["备注"] = new TextIdentifier { Cn = "备注", CnT = "", En = "" },
            ["地区"] = new TextIdentifier { Cn = "地区：", CnT = "", En = "" },
            ["标签"] = new TextIdentifier { Cn = "标签", CnT = "", En = "" },
            ["共同群聊"] = new TextIdentifier { Cn = "共同群聊", CnT = "", En = "" },
            ["来源"] = new TextIdentifier { Cn = "来源", CnT = "", En = "" },
            ["发消息"] = new TextIdentifier { Cn = "发消息", CnT = "", En = "" },
            ["语音聊天"] = new TextIdentifier { Cn = "语音聊天", CnT = "", En = "" },
            ["视频聊天"] = new TextIdentifier { Cn = "视频聊天", CnT = "", En = "" },
            ["更多"] = new TextIdentifier { Cn = "更多", CnT = "", En = "" },
            ["设置备注和标签"] = new TextIdentifier { Cn = "设置备注和标签", CnT = "", En = "" },
            ["确定"] = new TextIdentifier { Cn = "确定", CnT = "", En = "" },
            ["输入标签"] = new TextIdentifier { Cn = "输入标签", CnT = "", En = "" },
            ["备注名"] = new TextIdentifier { Cn = "备注名", CnT = "", En = "" }
        };

        /// <summary>
        /// 消息类型文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> MESSAGES = new Dictionary<string, TextIdentifier>
        {
            ["[图片]"] = new TextIdentifier { Cn = "[图片]", CnT = "", En = "" },
            ["[视频]"] = new TextIdentifier { Cn = "[视频]", CnT = "", En = "" },
            ["[语音]"] = new TextIdentifier { Cn = "[语音]", CnT = "", En = "" },
            ["[音乐]"] = new TextIdentifier { Cn = "[音乐]", CnT = "", En = "" },
            ["[位置]"] = new TextIdentifier { Cn = "[位置]", CnT = "", En = "" },
            ["[链接]"] = new TextIdentifier { Cn = "[链接]", CnT = "", En = "" },
            ["[文件]"] = new TextIdentifier { Cn = "[文件]", CnT = "", En = "" },
            ["[名片]"] = new TextIdentifier { Cn = "[名片]", CnT = "", En = "" },
            ["[笔记]"] = new TextIdentifier { Cn = "[笔记]", CnT = "", En = "" },
            ["[视频号]"] = new TextIdentifier { Cn = "[视频号]", CnT = "", En = "" },
            ["[动画表情]"] = new TextIdentifier { Cn = "[动画表情]", CnT = "", En = "" },
            ["[聊天记录]"] = new TextIdentifier { Cn = "[聊天记录]", CnT = "", En = "" },
            ["微信转账"] = new TextIdentifier { Cn = "微信转账", CnT = "", En = "" },
            ["接收中"] = new TextIdentifier { Cn = "接收中", CnT = "", En = "" },
            ["re_语音"] = new TextIdentifier { Cn = @"^\[语音\]\d+秒(,未播放)?$", CnT = "", En = "" },
            ["re_引用消息"] = new TextIdentifier { Cn = @"(^.+)\n引用.*?的消息 : (.+$)", CnT = "", En = "" },
            ["re_拍一拍"] = new TextIdentifier { Cn = "^.+拍了拍.+$", CnT = "", En = "" }
        };

        /// <summary>
        /// 群聊详情窗口文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> CHATROOM_DETAIL_WINDOW = new Dictionary<string, TextIdentifier>
        {
            ["聊天信息"] = new TextIdentifier { Cn = "聊天信息", CnT = "", En = "" },
            ["查看更多"] = new TextIdentifier { Cn = "查看更多", CnT = "", En = "" },
            ["群聊名称"] = new TextIdentifier { Cn = "群聊名称", CnT = "", En = "" },
            ["仅群主或管理员可以修改"] = new TextIdentifier { Cn = "仅群主或管理员可以修改", CnT = "", En = "" },
            ["我在本群的昵称"] = new TextIdentifier { Cn = "我在本群的昵称", CnT = "", En = "" },
            ["仅群主和管理员可编辑"] = new TextIdentifier { Cn = "仅群主和管理员可编辑", CnT = "", En = "" },
            ["点击编辑群公告"] = new TextIdentifier { Cn = "点击编辑群公告", CnT = "", En = "" },
            ["编辑"] = new TextIdentifier { Cn = "编辑", CnT = "", En = "" },
            ["备注"] = new TextIdentifier { Cn = "备注", CnT = "", En = "" },
            ["群公告"] = new TextIdentifier { Cn = "群公告", CnT = "", En = "" },
            ["分隔线"] = new TextIdentifier { Cn = "分隔线", CnT = "", En = "" },
            ["完成"] = new TextIdentifier { Cn = "完成", CnT = "", En = "" },
            ["发布"] = new TextIdentifier { Cn = "发布", CnT = "", En = "" },
            ["退出群聊"] = new TextIdentifier { Cn = "退出群聊", CnT = "", En = "" },
            ["退出"] = new TextIdentifier { Cn = "退出", CnT = "", En = "" },
            ["聊天成员"] = new TextIdentifier { Cn = "聊天成员", CnT = "", En = "" },
            ["添加"] = new TextIdentifier { Cn = "添加", CnT = "", En = "" },
            ["移出"] = new TextIdentifier { Cn = "移出", CnT = "", En = "" },
            ["re_退出群聊"] = new TextIdentifier { Cn = @"将退出群聊"".*?""", CnT = "", En = "" }
        };

        /// <summary>
        /// 个人资料窗口文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> PROFILE_WINDOW = new Dictionary<string, TextIdentifier>
        {
            ["微信号"] = new TextIdentifier { Cn = "微信号：", CnT = "", En = "" },
            ["昵称"] = new TextIdentifier { Cn = "昵称：", CnT = "", En = "" },
            ["地区"] = new TextIdentifier { Cn = "地区：", CnT = "", En = "" },
            ["个性签名"] = new TextIdentifier { Cn = "个性签名", CnT = "", En = "" },
            ["来源"] = new TextIdentifier { Cn = "来源", CnT = "", En = "" },
            ["备注"] = new TextIdentifier { Cn = "备注", CnT = "", En = "" },
            ["共同群聊"] = new TextIdentifier { Cn = "共同群聊", CnT = "", En = "" },
            ["添加到通讯录"] = new TextIdentifier { Cn = "添加到通讯录", CnT = "", En = "" },
            ["更多"] = new TextIdentifier { Cn = "更多", CnT = "", En = "" }
        };

        /// <summary>
        /// 添加新朋友窗口文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> ADD_NEW_FRIEND_WINDOW = new Dictionary<string, TextIdentifier>
        {
            ["标签"] = new TextIdentifier { Cn = "标签", CnT = "", En = "" },
            ["确定"] = new TextIdentifier { Cn = "确定", CnT = "", En = "" },
            ["备注名"] = new TextIdentifier { Cn = "备注名", CnT = "", En = "" },
            ["朋友圈"] = new TextIdentifier { Cn = "朋友圈", CnT = "", En = "" },
            ["仅聊天"] = new TextIdentifier { Cn = "仅聊天", CnT = "", En = "" },
            ["聊天、朋友圈、微信运动等"] = new TextIdentifier { Cn = "聊天、朋友圈、微信运动等", CnT = "", En = "" },
            ["你的联系人较多，添加新的朋友时需选择权限"] = new TextIdentifier { Cn = "你的联系人较多，添加新的朋友时需选择权限", CnT = "", En = "" },
            ["发送添加朋友申请"] = new TextIdentifier { Cn = "发送添加朋友申请", CnT = "", En = "" }
        };

        /// <summary>
        /// 添加群成员窗口文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> ADD_GROUP_MEMBER_WINDOW = new Dictionary<string, TextIdentifier>
        {
            ["搜索"] = new TextIdentifier { Cn = "搜索", CnT = "", En = "" },
            ["确定"] = new TextIdentifier { Cn = "确定", CnT = "", En = "" },
            ["完成"] = new TextIdentifier { Cn = "完成", CnT = "", En = "" },
            ["发送"] = new TextIdentifier { Cn = "发送", CnT = "", En = "" },
            ["已选择联系人"] = new TextIdentifier { Cn = "已选择联系人", CnT = "", En = "" },
            ["请勾选需要添加的联系人"] = new TextIdentifier { Cn = "请勾选需要添加的联系人", CnT = "", En = "" }
        };

        /// <summary>
        /// 图片窗口文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> IMAGE_WINDOW = new Dictionary<string, TextIdentifier>
        {
            ["上一张"] = new TextIdentifier { Cn = "上一张", CnT = "上一張", En = "Previous" },
            ["下一张"] = new TextIdentifier { Cn = "下一张", CnT = "下一張", En = "Next" },
            ["预览"] = new TextIdentifier { Cn = "预览", CnT = "預覽", En = "Preview" },
            ["放大"] = new TextIdentifier { Cn = "放大", CnT = "放大", En = "Zoom" },
            ["缩小"] = new TextIdentifier { Cn = "缩小", CnT = "縮小", En = "Shrink" },
            ["图片原始大小"] = new TextIdentifier { Cn = "图片原始大小", CnT = "圖片原始大小", En = "Original image size" },
            ["旋转"] = new TextIdentifier { Cn = "旋转", CnT = "旋轉", En = "Rotate" },
            ["编辑"] = new TextIdentifier { Cn = "编辑", CnT = "編輯", En = "Edit" },
            ["翻译"] = new TextIdentifier { Cn = "翻译", CnT = "翻譯", En = "Translate" },
            ["提取文字"] = new TextIdentifier { Cn = "提取文字", CnT = "提取文字", En = "Extract Text" },
            ["识别图中二维码"] = new TextIdentifier { Cn = "识别图中二维码", CnT = "識别圖中QR Code", En = "Extract QR Code" },
            ["另存为"] = new TextIdentifier { Cn = "另存为...", CnT = "另存爲...", En = "Save as…" },
            ["更多"] = new TextIdentifier { Cn = "更多", CnT = "更多", En = "More" },
            ["复制"] = new TextIdentifier { Cn = "复制", CnT = "複製", En = "Copy" },
            ["最小化"] = new TextIdentifier { Cn = "最小化", CnT = "最小化", En = "Minimize" },
            ["最大化"] = new TextIdentifier { Cn = "最大化", CnT = "最大化", En = "Maximize" },
            ["关闭"] = new TextIdentifier { Cn = "关闭", CnT = "關閉", En = "Close" }
        };

        /// <summary>
        /// 菜单选项文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> MENU_OPTIONS = new Dictionary<string, TextIdentifier>
        {
            // session
            ["置顶"] = new TextIdentifier { Cn = "置顶", CnT = "", En = "" },
            ["取消置顶"] = new TextIdentifier { Cn = "取消置顶", CnT = "", En = "" },
            ["标为未读"] = new TextIdentifier { Cn = "标为未读", CnT = "", En = "" },
            ["消息免打扰"] = new TextIdentifier { Cn = "消息免打扰", CnT = "", En = "" },
            ["在独立窗口打开"] = new TextIdentifier { Cn = "在独立窗口打开", CnT = "", En = "" },
            ["不显示聊天"] = new TextIdentifier { Cn = "不显示聊天", CnT = "", En = "" },
            ["删除聊天"] = new TextIdentifier { Cn = "删除聊天", CnT = "", En = "" },

            // msg
            ["撤回"] = new TextIdentifier { Cn = "撤回", CnT = "", En = "" },
            ["复制"] = new TextIdentifier { Cn = "复制", CnT = "", En = "" },
            ["放大阅读"] = new TextIdentifier { Cn = "放大阅读", CnT = "", En = "" },
            ["翻译"] = new TextIdentifier { Cn = "翻译", CnT = "", En = "" },
            ["转发"] = new TextIdentifier { Cn = "转发...", CnT = "", En = "" },
            ["收藏"] = new TextIdentifier { Cn = "收藏", CnT = "", En = "" },
            ["多选"] = new TextIdentifier { Cn = "多选", CnT = "", En = "" },
            ["引用"] = new TextIdentifier { Cn = "引用", CnT = "", En = "" },
            ["搜一搜"] = new TextIdentifier { Cn = "搜一搜", CnT = "", En = "" },
            ["删除"] = new TextIdentifier { Cn = "删除", CnT = "", En = "" },
            ["编辑"] = new TextIdentifier { Cn = "编辑", CnT = "", En = "" },
            ["另存为"] = new TextIdentifier { Cn = "另存为...", CnT = "", En = "" },
            ["语音转文字"] = new TextIdentifier { Cn = "语音转文字", CnT = "", En = "" },
            ["在文件夹中显示"] = new TextIdentifier { Cn = "在文件夹中显示", CnT = "", En = "" },

            // edit
            ["剪切"] = new TextIdentifier { Cn = "剪切", CnT = "", En = "" },
            ["粘贴"] = new TextIdentifier { Cn = "粘贴", CnT = "", En = "" }
        };

        /// <summary>
        /// 朋友圈文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> MOMENTS = new Dictionary<string, TextIdentifier>
        {
            ["朋友圈"] = new TextIdentifier { Cn = "朋友圈", CnT = "", En = "" },
            ["刷新"] = new TextIdentifier { Cn = "刷新", CnT = "", En = "" },
            ["评论"] = new TextIdentifier { Cn = "评论", CnT = "", En = "" },
            ["广告"] = new TextIdentifier { Cn = "广告", CnT = "", En = "" },
            ["赞"] = new TextIdentifier { Cn = "赞", CnT = "", En = "" },
            ["取消"] = new TextIdentifier { Cn = "取消", CnT = "", En = "" },
            ["发送"] = new TextIdentifier { Cn = "发送", CnT = "", En = "" },
            ["分隔符_点赞"] = new TextIdentifier { Cn = "，", CnT = "", En = "" },
            ["re_图片数"] = new TextIdentifier { Cn = @"包含\d+张图片", CnT = "", En = "" }
        };

        /// <summary>
        /// 新朋友元素文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> NEW_FRIEND_ELEMENT = new Dictionary<string, TextIdentifier>
        {
            ["新的朋友"] = new TextIdentifier { Cn = "新的朋友", CnT = "", En = "" },
            ["回复"] = new TextIdentifier { Cn = "回复", CnT = "", En = "" },
            ["发送"] = new TextIdentifier { Cn = "发送", CnT = "", En = "" },
            ["朋友圈"] = new TextIdentifier { Cn = "朋友圈", CnT = "", En = "" },
            ["仅聊天"] = new TextIdentifier { Cn = "仅聊天", CnT = "", En = "" },
            ["聊天、朋友圈、微信运动等"] = new TextIdentifier { Cn = "聊天、朋友圈、微信运动等", CnT = "", En = "" },
            ["备注名"] = new TextIdentifier { Cn = "备注名", CnT = "", En = "" },
            ["标签"] = new TextIdentifier { Cn = "标签", CnT = "", En = "" }
        };

        /// <summary>
        /// 微信浏览器文本标识符
        /// </summary>
        public static readonly Dictionary<string, TextIdentifier> WECHAT_BROWSER = new Dictionary<string, TextIdentifier>
        {
            ["关闭"] = new TextIdentifier { Cn = "关闭", CnT = "", En = "" },
            ["更多"] = new TextIdentifier { Cn = "更多", CnT = "", En = "" },
            ["地址和搜索栏"] = new TextIdentifier { Cn = "地址和搜索栏", CnT = "", En = "" },
            ["转发给朋友"] = new TextIdentifier { Cn = "转发给朋友", CnT = "", En = "" },
            ["复制链接"] = new TextIdentifier { Cn = "复制链接", CnT = "", En = "" }
        };
        public static readonly Dictionary<string, TextIdentifier> WECHAT_CONVERSATION_TITLE = new Dictionary<string, TextIdentifier>
        {
            ["微信团队"] = new TextIdentifier { Cn = "微信团队", CnT = "", En = "" },
            ["服务通知"] = new TextIdentifier { Cn = "服务通知", CnT = "", En = "" },
            ["微信支付"] = new TextIdentifier { Cn = "微信支付", CnT = "", En = "" },
            ["腾讯新闻"] = new TextIdentifier { Cn = "腾讯新闻", CnT = "", En = "" },
            ["订阅号"] = new TextIdentifier { Cn = "订阅号", CnT = "", En = "" },
            ["文件传输助手"] = new TextIdentifier { Cn = "文件传输助手", CnT = "", En = "" },
            ["折叠的群聊"] = new TextIdentifier { Cn = "折叠的群聊", CnT = "", En = "" }
        };
    }

    /// <summary>
    /// 文本标识符结构
    /// </summary>
    public class TextIdentifier
    {
        /// <summary>
        /// 简体中文
        /// </summary>
        public string Cn { get; set; } = "";

        /// <summary>
        /// 繁体中文
        /// </summary>
        public string CnT { get; set; } = "";

        /// <summary>
        /// 英文
        /// </summary>
        public string En { get; set; } = "";
    }
}