using System.Collections.Generic;
using MessagePack;

namespace WeChatAuto.Models
{
    [MessagePackObject]
    public class ImageCacheItem
    {
        [Key(1)]
        public string HashCode { get; set; }

        [Key(2)]
        public string NickName { get; set; }
    }
}