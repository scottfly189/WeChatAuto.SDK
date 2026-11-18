#nullable enable
// Sdk/Core/WxCountryNames.cs — contact.extra_buffer #5（ISO2 国家/地区码）→ 中文。
//
// 国家码中文名对照，命名沿用微信资料页口径，并把本机实测出现、原表未收录的 30 多个码补齐：
// CN=中国大陆、HK=中国香港、MO=中国澳门、TW=中国台湾（不采用只写「中国」的社区 ISO 译法）。
// 未收录的码原样返回，不猜。

using System;
using System.Collections.Generic;

namespace WxDecryptNet.Sdk.Core;

/// <summary>国家 / 地区 ISO2 码到中文名的映射。</summary>
public static class WxCountryNames
{
    static readonly Dictionary<string, string> Names = new(StringComparer.OrdinalIgnoreCase)
    {
        // —— 对照表主体 ——
        ["AD"] = "安道尔",
        ["AE"] = "阿联酋",
        ["AG"] = "安提瓜和巴布达",
        ["AU"] = "澳大利亚",
        ["CN"] = "中国大陆",
        ["CX"] = "圣诞岛",
        ["DE"] = "德国",
        ["DZ"] = "阿尔及利亚",
        ["EC"] = "厄瓜多尔",
        ["EG"] = "埃及",
        ["ER"] = "厄立特里亚",
        ["FR"] = "法国",
        ["GB"] = "英国",
        ["HK"] = "中国香港",
        ["HR"] = "克罗地亚",
        ["IE"] = "爱尔兰",
        ["IS"] = "冰岛",
        ["JE"] = "泽西岛",
        ["JP"] = "日本",
        ["KP"] = "朝鲜",
        ["KR"] = "韩国",
        ["MA"] = "摩洛哥",
        ["MO"] = "中国澳门",
        ["MP"] = "北马里亚纳群岛",
        ["MV"] = "马尔代夫",
        ["MY"] = "马来西亚",
        ["NO"] = "挪威",
        ["RU"] = "俄罗斯",
        ["SB"] = "所罗门群岛",
        ["SG"] = "新加坡",
        ["SY"] = "叙利亚",
        ["TH"] = "泰国",
        ["TW"] = "中国台湾",
        ["US"] = "美国",
        ["UK"] = "英国",
        ["VA"] = "梵蒂冈",
        ["WF"] = "瓦利斯和富图纳",
        ["CA"] = "加拿大",
        ["CH"] = "瑞士",
        ["IT"] = "意大利",
        ["ES"] = "西班牙",
        ["NL"] = "荷兰",
        // —— 本机实测出现、原表未收录的补充 ——
        ["AF"] = "阿富汗",
        ["AL"] = "阿尔巴尼亚",
        ["AO"] = "安哥拉",
        ["AQ"] = "南极洲",
        ["AR"] = "阿根廷",
        ["AT"] = "奥地利",
        ["AW"] = "阿鲁巴",
        ["AZ"] = "阿塞拜疆",
        ["BG"] = "保加利亚",
        ["BJ"] = "贝宁",
        ["BM"] = "百慕大",
        ["BT"] = "不丹",
        ["CF"] = "中非",
        ["CO"] = "哥伦比亚",
        ["DK"] = "丹麦",
        ["ET"] = "埃塞俄比亚",
        ["FI"] = "芬兰",
        ["GI"] = "直布罗陀",
        ["GL"] = "格陵兰",
        ["GR"] = "希腊",
        ["GS"] = "南乔治亚和南桑威奇群岛",
        ["LA"] = "老挝",
        ["LR"] = "利比里亚",
        ["MC"] = "摩纳哥",
        ["MM"] = "缅甸",
        ["NP"] = "尼泊尔",
        ["NU"] = "纽埃",
        ["NZ"] = "新西兰",
        ["OM"] = "阿曼",
        ["PA"] = "巴拿马",
        ["PK"] = "巴基斯坦",
        ["TD"] = "乍得",
        ["TG"] = "多哥",
        ["UA"] = "乌克兰",
        ["UM"] = "美国本土外小岛屿",
        ["VG"] = "英属维尔京群岛",
    };

    /// <summary>ISO2 码转中文名；未收录返回原码（大写保持输入样），空输入返回空串。</summary>
    public static string ToChinese(string? code)
    {
        var key = code?.Trim();
        if (string.IsNullOrEmpty(key))
            return "";
        return Names.TryGetValue(key, out var zh) ? zh : key;
    }
}
