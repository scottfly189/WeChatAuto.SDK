using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeChatAuto.Models
{
    public class FetchedMedia
    {
        public string FilePath { get; set; }    // 微信本地原图 / 文件路径
        public string Base64Str { get; set; }   // 内容 base64
        public string Error { get; set; }       // 失败原因（如 clipboard_busy / timeout / not_changed）
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}