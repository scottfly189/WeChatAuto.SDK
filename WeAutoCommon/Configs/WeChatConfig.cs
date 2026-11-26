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
using FlaUI.Core.AutomationElements;

namespace WeAutoCommon.Configs
{
    /// <summary>
    /// 微信参数配置类
    /// </summary>
    public class WeChatConfig
    {
        /// <summary>
        /// 是否自动刷新朋友圈数据,默认为<c>false</c>,即不会自动刷新朋友圈数据，
        /// 可选的：也可以在调用获取朋友圈数据的api时将参数<c>ensureFresh</c>设置为<c>true</c>，也会帮自动刷新朋友圈
        /// </summary>
        public bool SnsAutoRefreshEnabled = false;
        /// <summary>
        /// 朋友圈自动刷新的秒数，默认为<c>30</c>秒.
        /// 此参数在<seealso cref="SnsAutoRefreshEnabled"/>开启时才生效.
        /// </summary>
        public int SnsAutoRefreshIntervalSeconds = 30;
        /// <summary>
        /// 是否启用微信数据库安全读取（VIP）。默认 <c>false</c>，即纯 UI Automation；
        /// 设为 <c>true</c> 后启用数据库读取 / 热同步，需要配置微信存储目录和数据库连接。
        /// </summary>
        public bool UseDatabaseRead { get; set; } = false;

        /// <summary>
        /// 微信存储位置，取法: 微信主窗口--> 设置 --> 账号与存储 --> 存储位置（形如 <c>D:\xwechat_files</c>）。
        /// 如果有多微信，多个微信应该设置成一样（微信自身就是这种设定）。
        /// <para>
        /// **可以留空**：留空时 SDK 会在第一个微信客户端启动、需要读取数据库时，
        /// 通过自动化读取一次，并把结果写入 <c>%LOCALAPPDATA%\WeChatAuto\wechatkey.dat</c>；
        /// 之后再启动直接读缓存，不再操作微信设置窗口。整个进程内最多自动化一次，多微信共用同一份值。
        /// </para>
        /// <para>显式配置（且目录存在）时以配置为准，并顺带写回缓存。</para>
        /// </summary>
        public string WeChatStorageRoot { get; set; }

        /// <summary>
        /// 数据库类型，几乎支持所有数据库类型，默认是sqlite数据库,可以改成其他的数据库
        /// </summary>
        public DbType DbTppe { get; set; } = DbType.Sqlite;
        /// <summary>
        /// 数据库连接字符串
        /// "ConnectionString": "PORT=5432;DATABASE=xxx;HOST=localhost;PASSWORD=xxx;USER ID=xxx", // PostgreSQL（Kdbndp、OpenGauss通用）
        /// "ConnectionString": "Server=localhost;Database=xxx;Uid=xxx;Pwd=xxx;SslMode=None;AllowLoadLocalInfile=true;AllowUserVariables=true;", // MySql,
        /// "ConnectionString": "User Id=xxx; Password=xxx; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)))", // Oracle
        /// "ConnectionString": "Server=localhost;Database=xxx;User Id=xxx;Password=xxx;Encrypt=True;TrustServerCertificate=True;", // SqlServer
        /// "ConnectionString": "host=222.71.212.3;Port=27017;Database=testDB;Username= root;Password=123456;authSource=admin;replicaSet=", // MongoDB
        /// </summary>
        public string ConnectionString { get; set; } = "Data Source=wechat.db";
        /// <summary>
        /// 账号首次同步时的历史窗口天数，默认 30。
        /// 0 表示首轮不回补历史，只同步从启动时刻起的新数据。负数按 0 处理（记一条日志，不会抛异常或中断启动）。
        /// </summary>
        [JsonPropertyName("first_run_days")]
        public int FirstRunDays { get; set; } = 30;

        /// <summary>
        /// 是否用 VSS 卷快照作为 hot-sync 只读源（默认 <c>true</c>）。
        /// <para>
        /// 开启后首轮/增量都不再整库拷贝；每轮按源库变化/最小间隔创建或复用快照，
        /// 读 db+wal；需要管理员/VSS，失败回退 live（若开启），否则报错。
        /// </para>
        /// </summary>
        [JsonPropertyName("hotsync_vss_source")]
        public bool HotSyncVssSource { get; set; } = true;

        /// <summary>
        /// VSS 不可用/失败时是否直接只读打开微信源库（默认 <c>true</c>）。
        /// 优先级低于 <see cref="HotSyncVssSource"/>；两者都开时先 VSS，失败才 live。
        /// </summary>
        [JsonPropertyName("hotsync_live_source")]
        public bool HotSyncLiveSource { get; set; } = true;

        /// <summary>
        /// 首轮仅同步文字：历史图片/文件/视频不做路径解析、解密、下载，Content 保留
        /// <c>[图片]</c>/<c>[文件]</c>/<c>[视频]</c> 占位符；增量轮次恢复正常媒体处理。
        /// <para>语音不受影响，仍按 <c>AsrIncludeFirstRound</c> 等现有机制处理。</para>
        /// </summary>
        [JsonPropertyName("hotsync_first_run_text_only")]
        public bool HotSyncFirstRunTextOnly { get; set; }

        /// <summary>
        /// 是否把 hot-sync 的 VSS/live 源读取诊断日志写到独立文件（默认 <c>false</c>）。
        /// <para>
        /// 开启后写 <c>%LOCALAPPDATA%\WeChatAuto\logs\hotsync-copy.log</c>，每行一条
        /// <c>[copy] t=... event=start|progress|retry|done|fail db=... size=... ms=...</c>，
        /// 用于在几十 GB 的大库现场定位“慢在哪个库、是否反复重试”；对同步逻辑无影响。
        /// </para>
        /// </summary>
        [JsonPropertyName("hotsync_copy_log")]
        public bool HotSyncCopyLog { get; set; }

        /// <summary>
        /// 朋友圈（SNS）首次同步的回看天数。<b>0 或负数表示全量回补</b>
        /// <para>注意与 <see cref="FirstRunDays"/> 的语义不同：消息的 0 表示「不回补历史」，
        /// 朋友圈的 0 表示「全部历史都要」。朋友圈单账号通常只有几千条，全量成本可控。</para>
        /// <para>只影响「中间表还没有该账号朋友圈水位」的首次同步；已有水位的账号不受影响。</para>
        /// </summary>
        [JsonPropertyName("sns_first_run_days")]
        public int SnsFirstRunDays { get; set; } = 0;

        /// <summary>
        /// 是否下载朋友圈图片 / 视频到本机，默认 <c>false</c>（不下载，保持 hot-sync 原有耗时）。
        /// <para>开启后由独立的旁路后台任务处理：只处理 <c>wechat_sns_media</c> 里
        /// <c>MediaType</c> 为 2（图片）/ 4、5、6（视频）且 <c>DownloadStatus=0</c> 的行，
        /// 落盘路径写入 <c>LocalPath</c>，成功 / 永久失败都会写状态，不会反复重试。</para>
        /// </summary>
        [JsonPropertyName("download_sns_images")]
        public bool DownloadSnsImages { get; set; } = false;


        /// <summary>VSS 两次新建快照的最小间隔（秒），默认 2；源库没变时不新建。</summary>
        [JsonPropertyName("hotsync_vss_min_interval_seconds")]
        public double HotSyncVssMinIntervalSeconds { get; set; } = 2.0;

        /// <summary>
        /// 消息媒体缓存根目录（图片 / 语音解密后的文件）。为空时用
        /// <c>%LOCALAPPDATA%\WeChatAuto\media-cache</c>。支持绝对路径和 <c>%ENV%</c> 环境变量。
        /// </summary>
        [JsonPropertyName("media_cache_dir")]
        public string MediaCacheDir { get; set; } = "";

        #region 语音转写（ASR）

        /// <summary>
        /// 是否启用本地语音转写：增量同步到的语音 wav 在后台线程转文字，回写中间表
        /// <c>wechat_message.Content</c>（首轮同步不转写，避免拖慢首轮）。
        /// <para>默认关闭；模型由用户自行下载并放到 <see cref="AsrModelDir"/>，SDK 不随包发布模型。</para>
        /// 默认 <c>false</c>即不启用本地语音转写.
        /// 模型下载链接: https://pub-d387648081a84f6cb4fe364e2e8ccbe3.r2.dev/sensevoice-small.rar
        /// 下载后请放到 <see cref="AsrModelDir"/>的目录
        /// </summary>
        [JsonPropertyName("asr_enabled")]
        public bool AsrEnabled { get; set; }

        /// <summary>
        /// 首轮同步也转写（默认 <c>false</c>）。
        /// <para>
        /// 首轮要全量导入且通常是冷启动，ASR 与同步会争 CPU；默认只在增量轮次转写。
        /// 打开后首轮语音也会在媒体落地后入队——注意这是**性能权衡**，不是功能差异。
        /// </para>
        /// </summary>
        [JsonPropertyName("asr_include_first_round")]
        public bool AsrIncludeFirstRound { get; set; }

        /// <summary>
        /// ASR 模型目录（必填，启用时）。约定目录下直接放 sherpa-onnx SenseVoice 的两个文件：
        /// <c>model.int8.onnx</c>（或 <c>model.onnx</c>）与 <c>tokens.txt</c>。
        /// 支持绝对路径和 <c>%ENV%</c> 环境变量。
        /// </summary>
        [JsonPropertyName("asr_model_dir")]
        public string AsrModelDir { get; set; } = "";

        /// <summary>
        /// ASR 模型下载地址（**说明用，不会自动下载**）。
        /// <para>
        /// 默认指向仓库 <c>Resources/sensevoice-small.rar</c> 中记录的 R2 地址：
        /// 该 RAR 只有 191 字节，里面是一个 txt（真正的下载链接），因为模型约 228MB 不适合进仓库。
        /// 下载并解压后，把 <see cref="AsrModelDir"/> 指到含 <c>model.int8.onnx</c> + <c>tokens.txt</c> 的目录即可。
        /// </para>
        /// <para>
        /// 换成自建镜像时改这里；「模型不可用」的日志提示会带上该地址，方便用户自助排查。
        /// </para>
        /// </summary>
        [JsonPropertyName("asr_model_download_url")]
        public string AsrModelDownloadUrl { get; set; } =
            "https://pub-d387648081a84f6cb4fe364e2e8ccbe3.r2.dev/sensevoice-small.rar";

        /// <summary>模型标识，写入日志与中间表内容来源；默认 <c>sensevoice-small-int8</c>。</summary>
        [JsonPropertyName("asr_model_name")]
        public string AsrModelName { get; set; } = "sensevoice-small-int8";

        /// <summary>ASR 线程数（onnxruntime 内部并行度），默认 2；CPU 优先建议 1–4。</summary>
        [JsonPropertyName("asr_num_threads")]
        public int AsrNumThreads { get; set; } = 2;

        /// <summary>
        /// 语种：<c>auto</c>（默认，自动识别）/ <c>zh</c> / <c>yue</c>（粤语）/ <c>en</c> / <c>ja</c> / <c>ko</c>。
        /// </summary>
        [JsonPropertyName("asr_language")]
        public string AsrLanguage { get; set; } = "auto";

        /// <summary>是否启用 SenseVoice 的逆文本归一化（数字/日期转写），默认开启。</summary>
        [JsonPropertyName("asr_use_itn")]
        public bool AsrUseItn { get; set; } = true;

        /// <summary>
        /// 写入 <c>Content</c> 的前缀，默认空串（即只写转写文本）。
        /// 设为 <c>[语音] </c> 可保留「这是语音」的标记，同时让检索/机器人仍能拿到文本。
        /// </summary>
        [JsonPropertyName("asr_content_prefix")]
        public string AsrContentPrefix { get; set; } = "";

        /// <summary>后台队列容量，默认 512；满时丢弃新任务（不影响同步链路）。</summary>
        [JsonPropertyName("asr_queue_capacity")]
        public int AsrQueueCapacity { get; set; } = 512;

        /// <summary>超过该秒数的音频跳过（微信语音 &lt;60s），默认 120</summary>
        [JsonPropertyName("asr_max_seconds")]
        public int AsrMaxSeconds { get; set; } = 120;

        #endregion


        /// <summary>
        /// 朋友圈下载媒体根目录。为空时用 <c>%LOCALAPPDATA%\WeChatAuto\sns-media</c>。
        /// 支持绝对路径和 <c>%ENV%</c> 环境变量。
        /// </summary>
        [JsonPropertyName("sns_media_dir")]
        public string SnsMediaDir { get; set; } = "";

        /// <summary>
        /// "进入系统"的启动同步轮（打开应用后、首轮全量之外的补齐/增量 UPSERT/补水位那一次）的提示窗口工厂，可为空。
        /// <para>参数：本微信昵称、本微信主窗口句柄；返回的 <see cref="IDisposable"/> 在本轮同步结束时释放，
        /// Dispose 时应关闭提示窗口。只在这个启动轮调用；启动完成后运行期 hot-sync 的增量轮次不再提示。</para>
        /// <para>请在工厂启动前（即 <c>WeAutomation.Initialize</c> 的配置回调里）设置；运行期修改对下一次启动轮生效。</para>
        /// </summary>
        [JsonIgnore]
        public Func<string, IntPtr, IDisposable> StartupSyncOverlayFactory { get; set; }

        /// <summary>
        /// 在微信的 UI Tree 未被腾讯公开的情况下是否强开 UI Tree，默认 <c>true</c>。
        /// </summary>
        public bool IsForceOpenUITree { get; set; } = true;

        /// <summary>
        /// 是否启动的时候自动优化主窗口布局,默认是True,即优化，可以设置为不优化。
        /// </summary>
        public bool IsOptimizeMainWindowLayout { get; set; } = true;

        /// <summary>
        /// 消息监听时往下滚动的次数，如果监听列表多，建议设置成：10-30
        /// 如果监听列表少，建议设置成:5~10，以提高效率 
        /// 用于UI Automation方式的监听中
        /// </summary>
        [JsonPropertyName("monitor_message_max_down_interval")]
        public int MonitorMessageMaxDownInterval { get; set; } = 8;
        /// <summary>
        /// 好友申请监听间隔时间，单位为秒
        /// </summary>
        [JsonPropertyName("monitor_new_friend_request_interval")]
        public int MonitorNewFriendRequestInterval { get; set; } = 20;
        /// <summary>
        /// 固定式消息监听的时间间隔，单位为秒
        /// 注：获取微信消息官方推荐以 Database Read 为主;
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
        /// 如果启用UI Automation方式的消息监听，请设置为<c>true</c>
        /// 注：获取微信消息官方推荐以 Database Read 为主;
        /// </summary>
        [JsonPropertyName("enable_ocr")]
        public bool EnableOCR { get; set; } = false;
        /// <summary>
        /// ocr-det模型路径
        /// </summary>
        [JsonPropertyName("ocr_det_model_file_path")]
        public string OCRDetModelFilePath { get; set; } = "PP-OCRv6_det_small.onnx";  //v6
        /// <summary>
        /// ocr-cls模型路径
        /// </summary>
        [JsonPropertyName("ocr_cls_model_file_path")]
        public string OCRClsModelFilePath { get; set; } = "ch_ppocr_mobile_v2.0_cls_mobile.onnx";  //v6
        /// <summary>
        /// ocr-rec模型路径
        /// </summary>
        [JsonPropertyName("ocr_rec_model_file_path")]
        public string OCRRecModelFilePath { get; set; } = "PP-OCRv6_rec_small.onnx";  //v6

        /// <summary>
        /// OCR的字典文件路径
        /// </summary>
        [JsonPropertyName("ocr_dict_model_file_path")]
        public string OCRDictModelFilePath { get; set; } = "ppocrv6_dict.txt";  //v6
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
        /// 注：获取微信消息官方推荐以 Database Read 为主;
        /// </summary>
        [JsonPropertyName("max_history_message_fetch_number")]
        public int MaxHistoryMessageFetchNumber { get; set; } = 40;
    }

    public static class Language
    {
        public static string CurrentLanguage { get; set; } = Language.Chinese;
        public const string Chinese = "Cn";
        public const string ChineseTraditional = "CnT";
        public const string English = "En";
    }
}





