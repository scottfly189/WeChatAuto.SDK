using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using OneOf.Types;
using System.ComponentModel;
using System.Text.Json.Serialization;
using SqlSugar;

namespace WeAutoCommon.Configs
{
    /// <summary>
    /// 微信自动化参数配置类
    /// </summary>
    public class WeChatConfig
    {
        /// <summary>
        /// 下载文件/图片默认保存路径
        /// </summary>
        [JsonPropertyName("default_save_path")]
        public string DefaultSavePath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "wxauto_download");

        /// <summary>
        /// 监听消息时间间隔，单位秒
        /// </summary>
        [JsonPropertyName("listen_interval")]
        public int ListenInterval { get; set; } = 5;
        /// <summary>
        /// 朋友圈监听时间间隔，单位秒
        /// </summary>
        [JsonPropertyName("moments_listen_interval")]
        public int MomentsListenInterval { get; set; } = 10;

        /// <summary>
        /// 新好友监听时间间隔，单位秒
        /// </summary>
        [JsonPropertyName("new_user_listener_interval")]
        public int NewUserListenerInterval { get; set; } = 5;

        /// <summary>
        /// 监听子窗口时间间隔，单位秒
        /// </summary>
        [JsonPropertyName("monitor_sub_win_interval")]
        public int MonitorSubWinInterval { get; set; } = 5;

        /// <summary>
        /// 监听会话列表切换时间间隔，单位：秒
        /// </summary>
        [JsonPropertyName("conversation_change_listener_interval")]
        public int ConversationChangeListenerInterval { get; set; } = 5;
        /// <summary>
        /// 消息监听时往下滚动的次数，如果监听列表多，建议设置成：10-30
        /// 如果监听列表少，建议设置成:5~10，以提高效率 
        /// </summary>
        [JsonPropertyName("monitor_message_max_down_interval")]
        public int MonitorMessageMaxDownInterval { get; set; } = 8;
        /// <summary>
        /// 好友申请监听间隔时间，单位为秒
        /// </summary>
        [JsonPropertyName("monitor_new_friend_request_interval")]
        public int MonitorNewFriendRequestInterval { get; set; } = 20;
        /// <summary>
        /// 监听群聊系统消息的间隔时间，单位为秒
        /// </summary>
        [JsonPropertyName("monitor_group_interval")]
        public int MonitorGroupInterval { get; set; } = 10;
        /// <summary>
        /// 固定式消息监听的时间间隔，单位为秒
        /// </summary>
        [JsonPropertyName("monitor_interval")]
        public int MonitorInterval { get; set; } = 3;
        /// <summary>
        /// 会话列表鼠标滚动行数.
        /// </summary>
        [JsonPropertyName("conversation_interval")]
        public int ConversationInterval { get; set; } = 5;

        /// <summary>
        /// 当滚动删除朋友圈内容时，最大滚动次数,如果朋友圈内容多，请将此值设置大一些。
        /// </summary>
        [JsonPropertyName("monents_scroll_max_step")]
        public int MonentsScrollMaxStep { get; set; } = 30;
        /// <summary>
        /// 是否启用OCR,但是一些功能由于腾迅的限制必须要启用OCR
        /// 如果启用OCR,则必须保证OCR模型在models目录下，并且需要配置好模型文件名称
        /// </summary>
        [JsonPropertyName("enable_ocr")]
        public bool EnableOCR { get; set; } = false;
        /// <summary>
        /// ocr-det模型路径
        /// </summary>
        [JsonPropertyName("ocr_det_model_file_path")]
        public string OCRDetModelFilePath { get; set; } = "ch_PP-OCRv5_mobile_det.onnx";
        /// <summary>
        /// ocr-cls模型路径
        /// </summary>
        [JsonPropertyName("ocr_cls_model_file_path")]
        public string OCRClsModelFilePath { get; set; } = "ch_ppocr_mobile_v2.0_cls_infer.onnx";
        /// <summary>
        /// ocr-rec模型路径
        /// </summary>
        [JsonPropertyName("ocr_rec_model_file_path")]
        public string OCRRecModelFilePath { get; set; } = "ch_PP-OCRv5_rec_mobile_infer.onnx";
        /// <summary>
        /// OCR的字典文件路径
        /// </summary>
        [JsonPropertyName("ocr_dict_model_file_path")]
        public string OCRDictModelFilePath { get; set; } = "ppocrv5_dict.txt";
        /// <summary>
        /// 是否启用调试模式
        /// </summary>
        [JsonPropertyName("debug_mode")]
        public bool DebugMode { get; set; } = false;
        /// <summary>
        /// 出错后捕获UI保存路径
        /// 默认保存到当前目录下的Capture文件夹,可以修改为其他路径
        /// </summary>
        [JsonPropertyName("capture_ui_path")]
        public string CaptureUIPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "Captures");
        /// <summary>
        /// 是否启用视频录制
        /// </summary>
        [JsonPropertyName("enable_record_video")]
        public bool EnableRecordVideo { get; set; } = false;
        /// <summary>
        /// 视频录制保存路径
        /// 默认保存到当前目录下的Video文件夹,可以修改为其他路径
        /// </summary>
        [JsonPropertyName("target_video_path")]
        public string TargetVideoPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "Videos");
        /// <summary>
        /// 是否启用鼠标键盘模拟器
        /// 启用后，键盘鼠标操作会通过模拟器进行操作，而不是通过windows automation进行操作。
        /// 注意：需要购买键鼠模拟器，并在此处启用。
        /// </summary>
        [JsonPropertyName("enable_mouse_keyboard_simulator")]
        public bool EnableMouseKeyboardSimulator { get; set; } = false;
        /// <summary>
        /// 键鼠模拟器设备VID
        /// </summary>
        [JsonPropertyName("km_device_vid")]
        public int KMDeviceVID { get; set; } = 0x1701;
        /// <summary>
        /// 键鼠模拟器设备PID
        /// </summary>
        [JsonPropertyName("km_device_pid")]
        public int KMDevicePID { get; set; } = 0x2612;
        /// <summary>
        /// 键鼠模拟器校验数据
        /// </summary>
        [JsonPropertyName("km_verify_user_data")]
        public string KMVerifyUserData { get; set; } = "4F6A21981BE675822DEE7B9BC39F3791";
        /// <summary>
        /// 点击偏移量,单位像素
        /// 为了避免每次点击都点击到同一个位置，可以设置一个偏移量，实际点击位置为点击位置减去偏移量的一个随机值
        /// </summary>
        [JsonPropertyName("km_offset_of_click")]
        public int KMOffsetOfClick { get; set; } = 5;
        /// <summary>
        /// 配置键鼠模拟器输出字符串编码类型
        /// 0: 输出ANSI字符串
        /// 1：输出Unicode字符串
        /// 2: 输出ANSI字符串，速度更快，但受输入法影响更大
        /// 3: 输出UNICODE字符串，速度更快，但受输入法影响更磊
        /// 4: 使用剪贴板粘贴输出字符串。优点是输出字符多时速度更快且不受输入法影响
        /// </summary>
        [JsonPropertyName("km_output_string_type")]
        public int KMOutputStringType { get; set; } = 0;
        /// <summary>
        /// 配置键鼠模拟器鼠标移动模式
        /// </summary>
        [JsonPropertyName("km_mouse_move_mode")]
        public int KMMouseMoveMode { get; set; } = 0;
        /// <summary>
        /// 进程DPI感知值,如果使用库的应用已经设置DPI感知，此参数无效。
        /// 0: 不设置,进程对DPI完全不知晓，按逻辑像素绘制，可能会出现点击不准确的情况。
        /// 1: PROCESS_SYSTEM_DPI_AWARE 默认值,进程只根据主显示器DPI绘制，DPI感知生效。
        /// 2: PROCESS_PER_MONITOR_DPI_AWARE，进程根据每个显示器DPI绘制,DPI感知生效。
        /// </summary>
        [JsonPropertyName("process_dpi_awareness")]
        public int ProcessDpiAwareness { get; set; } = 1;
        /// <summary>
        /// 是否一开始就初始化通讯录所有好友
        /// 如果以wxid为业务核心，强烈开启此选项.
        /// </summary>
        [JsonPropertyName("init_adress_book")]
        public bool InitAdressBook { get; set; } = false;
        /// <summary>
        /// 历史消息X偏移距离
        /// </summary>
        [JsonPropertyName("history_message_offset_x")]
        public int HistoryMessageOffset_X = 77;
        /// <summary>
        /// 历史消息Y偏移距离
        /// </summary>
        [JsonPropertyName("history_message_offset_y")]
        public int HistoryMessageOffset_Y = 40;
        /// <summary>
        /// 历史消息滚动时重试次数
        /// </summary>
        [JsonPropertyName("history_retry_number")]
        public int HistoryRetryNumber = 6;
        /// <summary>
        /// 头像按钮距离微信按钮的Y轴偏移量
        /// </summary>
        [JsonPropertyName("avator_to_weixin_button_offset_y")]
        public int AvatorToWeixinButtonOffsetY = 40;
        /// <summary>
        /// 用于消息监听中，返回给回调函数历史消息最大记录数，因为事实上无须读完整个历史消息的。
        /// </summary>
        [JsonPropertyName("max_history_message_fetch_number")]
        public int MaxHistoryMessageFetchNumber { get; set; } = 40;
        /// <summary>
        /// 为了预防全量搜索历史消息设置的阈值
        /// </summary>
        [JsonPropertyName("max_history_fallback_threshold_number")]
        public int MaxHistoryFallbackThresholdNumber { get; set; } = 50;
        /// <summary>
        /// 消息监听中，首次运行取历史消息的最大数量.
        /// </summary>
        [JsonPropertyName("message_first_fetch_number")]
        public int MessageFirstFetchNumber { get; set; } = 10;
        /// <summary>
        /// 消息监听中，为了消息稳定下来重试次数
        /// </summary>
        [JsonPropertyName("message_stability_retry_number")]
        public int MessageStabilityRetryNumber { get; set; } = 5;

        /// <summary>
        /// 数据库类型，几乎支持所有数据库类型，默认是sqlite数据库,可以改成其他的数据库
        /// </summary>
        [JsonPropertyName("db_type")]
        public DbType DbTppe { get; set; } = DbType.Sqlite;
        /// <summary>
        /// 数据库连接字符串,如：
        /// "ConnectionString": "PORT=5432;DATABASE=xxx;HOST=localhost;PASSWORD=xxx;USER ID=xxx", // PostgreSQL（Kdbndp、OpenGauss通用）
        /// "ConnectionString": "Server=localhost;Database=xxx;Uid=xxx;Pwd=xxx;SslMode=None;AllowLoadLocalInfile=true;AllowUserVariables=true;", // MySql,
        /// "ConnectionString": "User Id=xxx; Password=xxx; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)))", // Oracle
        /// "ConnectionString": "Server=localhost;Database=xxx;User Id=xxx;Password=xxx;Encrypt=True;TrustServerCertificate=True;", // SqlServer
        /// "ConnectionString": "host=222.71.212.32;Port=27017;Database=testDB;Username= root;Password=123456;authSource=admin;replicaSet=", // MongoDB
        /// 注：此配置项为公共配置项，在各个消息监听函数中可以修改此配置，并且优先级以各个消息监听函数中配置的配置项最高。
        /// </summary>
        [JsonPropertyName("connection_string")]
        public string ConnectionString { get; set; } = "Data Source=wechat.db";
    }

    public static class Language
    {
        public static string CurrentLanguage { get; set; } = Language.Chinese;
        public const string Chinese = "Cn";
        public const string ChineseTraditional = "CnT";
        public const string English = "En";
    }
}