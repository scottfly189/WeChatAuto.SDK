using System.Collections.Generic;
using WeAutoCommon.Models;

namespace WeAutoCommon.Utils
{
    /// <summary>
    /// 表情列表帮助类
    /// </summary>
    public static class EmojiListHelper
    {
        private static List<EmojiItem> _Items = new List<EmojiItem>();
        public static List<EmojiItem> Items { get => _Items; }

        static EmojiListHelper()
        {
            Init();
        }
        public static void Init()
        {
            _Items = new List<EmojiItem>()
            {
                new EmojiItem { Index = 1, Description = "微笑", Value = "[微笑]" },
                new EmojiItem { Index = 2, Description = "撇嘴", Value = "[撇嘴]" },
                new EmojiItem { Index = 3, Description = "色色表情", Value = "[色]" },
                new EmojiItem { Index = 4, Description = "发呆", Value = "[发呆]" },
                new EmojiItem { Index = 5, Description = "得意", Value = "[得意]" },
                new EmojiItem { Index = 6, Description = "流泪", Value = "[流泪]" },
                new EmojiItem { Index = 7, Description = "害羞", Value = "[害羞]" },
                new EmojiItem { Index = 8, Description = "闭嘴", Value = "[闭嘴]" },
                new EmojiItem { Index = 9, Description = "睡", Value = "[睡]" },
                new EmojiItem { Index = 10, Description = "大哭", Value = "[大哭]" },
                new EmojiItem { Index = 11, Description = "尴尬", Value = "[尴尬]" },
                new EmojiItem { Index = 12, Description = "发怒", Value = "[发怒]" },
                new EmojiItem { Index = 13, Description = "调皮", Value = "[调皮]" },
                new EmojiItem { Index = 14, Description = "呲牙", Value = "[呲牙]" },
                new EmojiItem { Index = 15, Description = "惊讶", Value = "[惊讶]" },
                new EmojiItem { Index = 16, Description = "难过", Value = "[难过]" },
                new EmojiItem { Index = 17, Description = "囧", Value = "[囧]" },
                new EmojiItem { Index = 18, Description = "抓狂", Value = "[抓狂]" },
                new EmojiItem { Index = 19, Description = "吐", Value = "[吐]" },
                new EmojiItem { Index = 20, Description = "偷笑", Value = "[偷笑]" },
                new EmojiItem { Index = 21, Description = "愉快", Value = "[愉快]" },
                new EmojiItem { Index = 22, Description = "白眼", Value = "[白眼]" },
                new EmojiItem { Index = 23, Description = "傲慢", Value = "[傲慢]" },
                new EmojiItem { Index = 24, Description = "困", Value = "[困]" },
                new EmojiItem { Index = 25, Description = "惊恐", Value = "[惊恐]" },
                new EmojiItem { Index = 26, Description = "憨笑", Value = "[憨笑]" },
                new EmojiItem { Index = 27, Description = "悠闲", Value = "[悠闲]" },
                new EmojiItem { Index = 28, Description = "咒骂", Value = "[咒骂]" },
                new EmojiItem { Index = 29, Description = "疑问", Value = "[疑问]" },
                new EmojiItem { Index = 30, Description = "嘘", Value = "[嘘]" },
                new EmojiItem { Index = 31, Description = "晕", Value = "[晕]" },
                new EmojiItem { Index = 32, Description = "衰", Value = "[衰]" },
                new EmojiItem { Index = 33, Description = "骷髅", Value = "[骷髅]" },
                new EmojiItem { Index = 34, Description = "敲打", Value = "[敲打]" },
                new EmojiItem { Index = 35, Description = "再见", Value = "[再见]" },
                new EmojiItem { Index = 36, Description = "擦汗", Value = "[擦汗]" },
                new EmojiItem { Index = 37, Description = "抠鼻", Value = "[抠鼻]" },
                new EmojiItem { Index = 38, Description = "鼓掌", Value = "[鼓掌]" },
                new EmojiItem { Index = 39, Description = "坏笑", Value = "[坏笑]" },
                new EmojiItem { Index = 40, Description = "右哼哼", Value = "[右哼哼]" },
                new EmojiItem { Index = 41, Description = "鄙视", Value = "[鄙视]" },
                new EmojiItem { Index = 42, Description = "委屈", Value = "[委屈]" },
                new EmojiItem { Index = 43, Description = "快哭了", Value = "[快哭了]" },
                new EmojiItem { Index = 44, Description = "阴险", Value = "[阴险]" },
                new EmojiItem { Index = 45, Description = "亲亲", Value = "[亲亲]" },
                new EmojiItem { Index = 46, Description = "可怜", Value = "[可怜]" },
                new EmojiItem { Index = 47, Description = "笑脸", Value = "[笑脸]" },
                new EmojiItem { Index = 48, Description = "生病", Value = "[生病]" },
                new EmojiItem { Index = 49, Description = "脸红", Value = "[脸红]" },
                new EmojiItem { Index = 50, Description = "破涕为笑", Value = "[破涕为笑]" },
                new EmojiItem { Index = 51, Description = "恐惧", Value = "[恐惧]" },
                new EmojiItem { Index = 52, Description = "失望", Value = "[失望]" },
                new EmojiItem { Index = 53, Description = "无语", Value = "[无语]" },
                new EmojiItem { Index = 54, Description = "嘿哈", Value = "[嘿哈]" },
                new EmojiItem { Index = 55, Description = "捂脸", Value = "[捂脸]" },
                new EmojiItem { Index = 56, Description = "奸笑", Value = "[奸笑]" },
                new EmojiItem { Index = 57, Description = "机智", Value = "[机智]" },
                new EmojiItem { Index = 58, Description = "皱眉", Value = "[皱眉]" },
                new EmojiItem { Index = 59, Description = "耶", Value = "[耶]" },
                new EmojiItem { Index = 60, Description = "吃瓜", Value = "[吃瓜]" },
                new EmojiItem { Index = 61, Description = "加油", Value = "[加油]" },
                new EmojiItem { Index = 62, Description = "汗", Value = "[汗]" },
                new EmojiItem { Index = 63, Description = "天啊", Value = "[天啊]" },
                new EmojiItem { Index = 64, Description = "嗯嗯嗯", Value = "[Emm]" },
                new EmojiItem { Index = 65, Description = "社会社会", Value = "[社会社会]" },
                new EmojiItem { Index = 66, Description = "旺柴", Value = "[旺柴]" },
                new EmojiItem { Index = 67, Description = "好的", Value = "[好的]" },
                new EmojiItem { Index = 68, Description = "打脸", Value = "[打脸]" },
                new EmojiItem { Index = 69, Description = "哇", Value = "[哇]" },
                new EmojiItem { Index = 70, Description = "翻白眼", Value = "[翻白眼]" },
                new EmojiItem { Index = 71, Description = "666", Value = "[666]" },
                new EmojiItem { Index = 72, Description = "让我看看", Value = "[让我看看]" },
                new EmojiItem { Index = 73, Description = "叹气", Value = "[叹气]" },
                new EmojiItem { Index = 74, Description = "苦涩", Value = "[苦涩]" },
                new EmojiItem { Index = 75, Description = "裂开", Value = "[裂开]" },
                new EmojiItem { Index = 76, Description = "嘴唇", Value = "[嘴唇]" },
                new EmojiItem { Index = 77, Description = "爱心", Value = "[爱心]" },
                new EmojiItem { Index = 78, Description = "心碎", Value = "[心碎]" },
                new EmojiItem { Index = 79, Description = "拥抱", Value = "[拥抱]" },
                new EmojiItem { Index = 80, Description = "强", Value = "[强]" },
                new EmojiItem { Index = 81, Description = "弱", Value = "[弱]" },
                new EmojiItem { Index = 82, Description = "握手", Value = "[握手]" },
                new EmojiItem { Index = 83, Description = "胜利", Value = "[胜利]" },
                new EmojiItem { Index = 84, Description = "抱拳", Value = "[抱拳]" },
                new EmojiItem { Index = 85, Description = "勾引", Value = "[勾引]" },
                new EmojiItem { Index = 86, Description = "拳头", Value = "[拳头]" },
                new EmojiItem { Index = 87, Description = "OK", Value = "[OK]" },
                new EmojiItem { Index = 88, Description = "合十", Value = "[合十]" },
                new EmojiItem { Index = 89, Description = "啤酒", Value = "[啤酒]" },
                new EmojiItem { Index = 90, Description = "咖啡", Value = "[咖啡]" },
                new EmojiItem { Index = 91, Description = "蛋糕", Value = "[蛋糕]" },
                new EmojiItem { Index = 92, Description = "玫瑰", Value = "[玫瑰]" },
                new EmojiItem { Index = 93, Description = "凋谢", Value = "[凋谢]" },
                new EmojiItem { Index = 94, Description = "菜刀", Value = "[菜刀]" },
                new EmojiItem { Index = 95, Description = "炸弹", Value = "[炸弹]" },
                new EmojiItem { Index = 96, Description = "便便", Value = "[便便]" },
                new EmojiItem { Index = 97, Description = "月亮", Value = "[月亮]" },
                new EmojiItem { Index = 98, Description = "太阳", Value = "[太阳]" },
                new EmojiItem { Index = 99, Description = "庆祝", Value = "[庆祝]" },
                new EmojiItem { Index = 100, Description = "礼物", Value = "[礼物]" },
                new EmojiItem { Index = 101, Description = "红包", Value = "[红包]" },
                new EmojiItem { Index = 102, Description = "發", Value = "[發]" },
                new EmojiItem { Index = 103, Description = "福", Value = "[福]" },
                new EmojiItem { Index = 104, Description = "烟花", Value = "[烟花]" },
                new EmojiItem { Index = 105, Description = "爆竹", Value = "[爆竹]" },
                new EmojiItem { Index = 106, Description = "猪头", Value = "[猪头]" },
                new EmojiItem { Index = 107, Description = "跳跳", Value = "[跳跳]" },
                new EmojiItem { Index = 108, Description = "发抖", Value = "[发抖]" },
                new EmojiItem { Index = 109, Description = "转圈", Value = "[转圈]" }
            };
        }
    }
}