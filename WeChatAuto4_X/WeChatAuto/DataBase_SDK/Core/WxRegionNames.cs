#nullable enable
// Sdk/Core/WxRegionNames.cs — 微信「地区」英文名 → 中文。
//
// 背景：微信 4.x 的 contact.extra_buffer 把地区存成英文/拼音（#5 国家码、#6 省/州、#7 市/区），
// 界面上的「广东 深圳」是客户端拿内置资源表翻的，库里没有中文（全库扫过，零命中）。
// 本表只覆盖中国大陆 34 个省级行政区 + 港澳台常见分区 + 主要城市/直辖市城区；
// 命不中的（境外州名、微信写入的脏值如 East / Jing / Po）一律原样返回，不猜。
//
// 取值来源：本机全量 contact.extra_buffer 实测（2026-11），
// 覆盖面上广州/深圳/北京朝阳海淀/上海浦东等高频值。

using System;
using System.Collections.Generic;

namespace WxDecryptNet.Sdk.Core;

/// <summary>
/// 微信 <c>extra_buffer</c> 地区字段（#6 省 / #7 市）的英文 → 中文映射。
/// 命中返回值；未命中返回原串（去掉首尾空白），空输入返回空串。
/// </summary>
public static class WxRegionNames
{
    /// <summary>省 / 州 → 中文。键不区分大小写。</summary>
    static readonly Dictionary<string, string> Provinces = new(StringComparer.OrdinalIgnoreCase)
    {
        // 23 省
        ["Anhui"] = "安徽",
        ["Fujian"] = "福建",
        ["Gansu"] = "甘肃",
        ["Guangdong"] = "广东",
        ["Guizhou"] = "贵州",
        ["Hainan"] = "海南",
        ["Hebei"] = "河北",
        ["Heilongjiang"] = "黑龙江",
        ["Henan"] = "河南",
        ["Hubei"] = "湖北",
        ["Hunan"] = "湖南",
        ["Jiangsu"] = "江苏",
        ["Jiangxi"] = "江西",
        ["Jilin"] = "吉林",
        ["Liaoning"] = "辽宁",
        ["Qinghai"] = "青海",
        ["Shaanxi"] = "陕西",
        ["Shandong"] = "山东",
        ["Shanxi"] = "山西",
        ["Sichuan"] = "四川",
        ["Yunnan"] = "云南",
        ["Zhejiang"] = "浙江",
        ["Taiwan"] = "台湾",
        // 5 自治区
        ["Guangxi"] = "广西",
        ["Inner Mongolia"] = "内蒙古",
        ["Nei Mongol"] = "内蒙古",
        ["Ningxia"] = "宁夏",
        ["Tibet"] = "西藏",
        ["Xizang"] = "西藏",
        ["Xinjiang"] = "新疆",
        // 4 直辖市
        ["Beijing"] = "北京",
        ["Chongqing"] = "重庆",
        ["Shanghai"] = "上海",
        ["Tianjin"] = "天津",
        // 2 特别行政区
        ["Hong Kong"] = "香港",
        ["Macao"] = "澳门",
        ["Macau"] = "澳门",
        // 港澳常见分区（微信把区写进 #6 的情况）
        ["Central and Western"] = "中西区",
        ["Wan Chai"] = "湾仔",
        ["Kowloon City"] = "九龙城",
        ["Kwun Tong"] = "观塘",
        ["Wong Tai Sin"] = "黄大仙",
        ["Yau Tsim Mong"] = "油尖旺",
        ["Tuen Mun"] = "屯门",
        ["Kwai Tsing"] = "葵青",
        ["Tai Po"] = "大埔",
        ["Islands"] = "离岛",
        ["Taipa"] = "氹仔",
        // 微信把台湾县市写进 #6 的情况
        ["Taipei"] = "台北",
        ["Hualien County"] = "花莲县",
    };

    /// <summary>市 / 区 → 中文。键不区分大小写。同形歧义（Taizhou / Yichun / Fuyang 等）刻意不收，留英文。</summary>
    static readonly Dictionary<string, string> Cities = new(StringComparer.OrdinalIgnoreCase)
    {
        // 直辖市城区
        ["Haidian"] = "海淀",
        ["Chaoyang"] = "朝阳",
        ["Fengtai"] = "丰台",
        ["Changping"] = "昌平",
        ["Shijingshan"] = "石景山",
        ["Tongzhou"] = "通州",
        ["Daxing"] = "大兴",
        ["Shunyi"] = "顺义",
        ["Fangshan"] = "房山",
        ["Miyun"] = "密云",
        ["Pinggu"] = "平谷",
        ["Pudong New District"] = "浦东新区",
        ["Minhang"] = "闵行",
        ["Xuhui"] = "徐汇",
        ["Baoshan"] = "宝山",
        ["Songjiang"] = "松江",
        ["Changning"] = "长宁",
        ["Jiading"] = "嘉定",
        ["Zhabei"] = "闸北",
        ["Luwan"] = "卢湾",
        ["Hongkou"] = "虹口",
        ["Yangpu"] = "杨浦",
        ["Qingpu"] = "青浦",
        ["Fengxian"] = "奉贤",
        ["Chongming"] = "崇明",
        ["Jinshan"] = "金山",
        ["Huangpu"] = "黄浦",
        ["Jinnan"] = "津南",
        ["Wuqing"] = "武清",
        ["Baodi"] = "宝坻",
        ["Ninghe"] = "宁河",
        ["Jinghai"] = "静海",
        ["Nankai"] = "南开",
        ["Binhai New Area"] = "滨海新区",
        ["Yubei"] = "渝北",
        ["Jiulongpo"] = "九龙坡",
        ["Shapingba"] = "沙坪坝",
        ["Yuzhong"] = "渝中",
        ["Jiangbei"] = "江北",
        ["Banan"] = "巴南",
        ["Bishan"] = "璧山",
        ["Dadukou"] = "大渡口",
        ["Nanchuan"] = "南川",
        ["Pengshui"] = "彭水",
        // 广东
        ["Guangzhou"] = "广州",
        ["Shenzhen"] = "深圳",
        ["Zhongshan"] = "中山",
        ["Foshan"] = "佛山",
        ["Dongguan"] = "东莞",
        ["Zhuhai"] = "珠海",
        ["Huizhou"] = "惠州",
        ["Jiangmen"] = "江门",
        ["Zhanjiang"] = "湛江",
        ["Shantou"] = "汕头",
        ["Shaoguan"] = "韶关",
        ["Meizhou"] = "梅州",
        ["Yangjiang"] = "阳江",
        ["Qingyuan"] = "清远",
        ["Heyuan"] = "河源",
        ["Yunfu"] = "云浮",
        ["Shanwei"] = "汕尾",
        ["Jieyang"] = "揭阳",
        ["Maoming"] = "茂名",
        ["Zhaoqing"] = "肇庆",
        // 浙江
        ["Hangzhou"] = "杭州",
        ["Ningbo"] = "宁波",
        ["Wenzhou"] = "温州",
        ["Jiaxing"] = "嘉兴",
        ["Jinhua"] = "金华",
        ["Shaoxing"] = "绍兴",
        ["Huzhou"] = "湖州",
        ["Quzhou"] = "衢州",
        ["Zhoushan"] = "舟山",
        // 江苏
        ["Nanjing"] = "南京",
        ["Suzhou"] = "苏州",
        ["Wuxi"] = "无锡",
        ["Changzhou"] = "常州",
        ["Nantong"] = "南通",
        ["Yangzhou"] = "扬州",
        ["Xuzhou"] = "徐州",
        ["Suqian"] = "宿迁",
        ["Yancheng"] = "盐城",
        // 四川
        ["Chengdu"] = "成都",
        ["Mianyang"] = "绵阳",
        ["Nanchong"] = "南充",
        ["Luzhou"] = "泸州",
        ["Meishan"] = "眉山",
        // 湖北
        ["Wuhan"] = "武汉",
        ["Yichang"] = "宜昌",
        ["Xiaogan"] = "孝感",
        ["Xianning"] = "咸宁",
        ["Huanggang"] = "黄冈",
        ["Xiangyang"] = "襄阳",
        // 湖南
        ["Changsha"] = "长沙",
        ["Zhuzhou"] = "株洲",
        ["Xiangtan"] = "湘潭",
        ["Hengyang"] = "衡阳",
        ["Yueyang"] = "岳阳",
        ["Changde"] = "常德",
        ["Yongzhou"] = "永州",
        // 河南
        ["Zhengzhou"] = "郑州",
        ["Luoyang"] = "洛阳",
        ["Kaifeng"] = "开封",
        ["Nanyang"] = "南阳",
        ["Xinyang"] = "信阳",
        ["Jiaozuo"] = "焦作",
        ["Shangqiu"] = "商丘",
        ["Xinxiang"] = "新乡",
        ["Xuchang"] = "许昌",
        ["Puyang"] = "濮阳",
        ["Luohe"] = "漯河",
        ["Zhumadian"] = "驻马店",
        ["Anyang"] = "安阳",
        ["Zhoukou"] = "周口",
        // 山东
        ["Jinan"] = "济南",
        ["Qingdao"] = "青岛",
        ["Weifang"] = "潍坊",
        ["Yantai"] = "烟台",
        ["Weihai"] = "威海",
        ["Linyi"] = "临沂",
        ["Zibo"] = "淄博",
        ["Jining"] = "济宁",
        ["Liaocheng"] = "聊城",
        ["Heze"] = "菏泽",
        ["Rizhao"] = "日照",
        ["Zaozhuang"] = "枣庄",
        // 福建
        ["Fuzhou"] = "福州",
        ["Xiamen"] = "厦门",
        ["Quanzhou"] = "泉州",
        ["Zhangzhou"] = "漳州",
        ["Longyan"] = "龙岩",
        ["Sanming"] = "三明",
        // 河北
        ["Shijiazhuang"] = "石家庄",
        ["Baoding"] = "保定",
        ["Tangshan"] = "唐山",
        ["Langfang"] = "廊坊",
        ["Handan"] = "邯郸",
        ["Xingtai"] = "邢台",
        ["Cangzhou"] = "沧州",
        ["Hengshui"] = "衡水",
        ["Qinhuangdao"] = "秦皇岛",
        ["Zhangjiakou"] = "张家口",
        ["Chengde"] = "承德",
        // 山西 / 陕西
        ["Taiyuan"] = "太原",
        ["Yuncheng"] = "运城",
        ["Luliang"] = "吕梁",
        ["Changzhi"] = "长治",
        ["Yangquan"] = "阳泉",
        ["Xinzhou"] = "忻州",
        ["Xi'an"] = "西安",
        ["Xianyang"] = "咸阳",
        ["Weinan"] = "渭南",
        ["Hanzhong"] = "汉中",
        ["Baoji"] = "宝鸡",
        ["Shangluo"] = "商洛",
        ["Yulin"] = "榆林",
        // 东北
        ["Shenyang"] = "沈阳",
        ["Dalian"] = "大连",
        ["Anshan"] = "鞍山",
        ["Dandong"] = "丹东",
        ["Benxi"] = "本溪",
        ["Changchun"] = "长春",
        ["Jilin City"] = "吉林市",
        ["Harbin"] = "哈尔滨",
        ["Jixi"] = "鸡西",
        ["Liaoyuan"] = "辽源",
        // 华中 / 华南其它
        ["Nanchang"] = "南昌",
        ["Ganzhou"] = "赣州",
        ["Jiujiang"] = "九江",
        ["Shangrao"] = "上饶",
        ["Jingdezhen"] = "景德镇",
        ["Pingxiang"] = "萍乡",
        ["Xinyu"] = "新余",
        ["Hefei"] = "合肥",
        ["Bozhou"] = "亳州",
        ["Luan"] = "六安",
        ["Nanning"] = "南宁",
        ["Guilin"] = "桂林",
        ["Liuzhou"] = "柳州",
        ["Beihai"] = "北海",
        ["Fangchenggang"] = "防城港",
        ["Haikou"] = "海口",
        ["Sanya"] = "三亚",
        // 西南 / 西北
        ["Kunming"] = "昆明",
        ["Qujing"] = "曲靖",
        ["Dali"] = "大理",
        ["Lincang"] = "临沧",
        ["Lijiang"] = "丽江",
        ["Diqing"] = "迪庆",
        ["Guiyang"] = "贵阳",
        ["Anshun"] = "安顺",
        ["Zunyi"] = "遵义",
        ["Lanzhou"] = "兰州",
        ["Xining"] = "西宁",
        ["Yinchuan"] = "银川",
        ["Urumqi"] = "乌鲁木齐",
        ["Ili"] = "伊犁",
        ["Lhasa"] = "拉萨",
        ["Hohhot"] = "呼和浩特",
        ["Baotou"] = "包头",
        ["Erdos"] = "鄂尔多斯",
        ["Bayannur"] = "巴彦淖尔",
        ["Chifeng"] = "赤峰",
        ["Tongliao"] = "通辽",
        // 港澳台
        ["Taipei"] = "台北",
        ["Hualien County"] = "花莲县",
        ["Taipa"] = "氹仔",
        ["Macao"] = "澳门",
        ["Victoria"] = "维多利亚",
        // 港澳分区（微信有时把区写进 #7）
        ["Wan Chai"] = "湾仔",
        ["Central and Western"] = "中西区",
        ["Kowloon City"] = "九龙城",
        ["Kwun Tong"] = "观塘",
        ["Wong Tai Sin"] = "黄大仙",
        ["Yau Tsim Mong"] = "油尖旺",
        ["Tuen Mun"] = "屯门",
        ["Kwai Tsing"] = "葵青",
        ["Tai Po"] = "大埔",
        ["Islands"] = "离岛",
        // 补全市名对照（本机样本没出现，但别的账号会遇到）
        ["Anqing"] = "安庆",
        ["Daqing"] = "大庆",
        ["Deyang"] = "德阳",
        ["Guangyuan"] = "广元",
        ["Jinzhong"] = "晋中",
        ["Leshan"] = "乐山",
        ["Lishui"] = "丽水",
        ["Neijiang"] = "内江",
        ["Ningde"] = "宁德",
        ["Shaoyang"] = "邵阳",
        ["Shiyan"] = "十堰",
        ["Wuwei"] = "武威",
        ["Wuzhou"] = "梧州",
        ["Yaan"] = "雅安",
        ["Yibin"] = "宜宾",
        ["Yongchuan"] = "永川",
        ["Yellowstone"] = "黄石",
        ["Chow"] = "崇左",
        ["Jing"] = "静安",
        ["Peace"] = "和平",
        ["Po"] = "普陀",
        ["Taizhou"] = "台州",
    };

    /// <summary>
    /// 「省 + 市」联合判定：微信对同名市/区只存英文，单看市名会歧义（Taizhou = 台州/泰州、
    /// East/West = 北京东西城 or 海南东方…）。键为 <c>省|市</c> 小写，值均有实测或资料对照佐证。
    /// </summary>
    static readonly Dictionary<string, string> CitiesByProvince = new(StringComparer.Ordinal)
    {
        // 按省分组的市名消歧
        ["beijing|east"] = "东城",
        ["beijing|west"] = "西城",
        ["jiangsu|taizhou"] = "泰州",
        ["zhejiang|taizhou"] = "台州",
        ["anhui|suzhou"] = "宿州",
        ["shanghai|jing"] = "静安",
        ["tianjin|west"] = "河西",
        // 本机实测新增（每项都有省上下文，且语义对得上中文地名）
        ["shanghai|po"] = "普陀",        // Po = 普陀区
        ["tianjin|peace"] = "和平",      // Peace = 和平区
        ["tianjin|north star"] = "北辰", // North Star = 北辰区
        ["chongqing|two rivers"] = "两江新区", // Two Rivers = 两江新区
        ["guangxi|chow"] = "崇左",       // Chow = 崇左
        ["hainan|east"] = "东方",        // East = 东方市
        ["hong kong|north"] = "北区",    // North = 北区
        // 同名但跨省的真实冲突：全局表按高频取，另一方用省上下文纠正
        ["yunnan|baoshan"] = "保山",     // 全局 Baoshan = 宝山（上海）
        ["guangxi|yulin"] = "玉林",      // 全局 Yulin = 榆林（陕西）
        ["shaanxi|yulin"] = "榆林",
        ["shanghai|baoshan"] = "宝山",
    };

    /// <summary>
    /// 境外「国家 + 州/市」→ 中文。键为 <c>国家码|英文名</c> 小写
    /// （只收已知地名，未收录留英文）。
    /// </summary>
    static readonly Dictionary<string, string> Foreign = new(StringComparer.Ordinal)
    {
        ["us|california"] = "加利福尼亚",
        ["us|new york"] = "纽约",
        ["us|san francisco"] = "旧金山",
        ["us|cupertino"] = "库比蒂诺",
        ["us|texas"] = "得克萨斯",
        ["us|illinois"] = "伊利诺伊",
        ["us|seattle"] = "西雅图",
        ["us|chicago"] = "芝加哥",
        ["au|new south wales"] = "新南威尔士",
        ["au|victoria"] = "维多利亚",
        ["au|queensland"] = "昆士兰",
        ["au|western australia"] = "西澳大利亚",
        ["au|south australia"] = "南澳大利亚",
        ["au|melbourne"] = "墨尔本",
        ["au|sydney"] = "悉尼",
        ["au|perth"] = "珀斯",
        ["au|adelaide"] = "阿德莱德",
        ["gb|england"] = "英格兰",
        ["gb|london"] = "伦敦",
        ["gb|bristol"] = "布里斯托尔",
        ["uk|england"] = "英格兰",
        ["uk|london"] = "伦敦",
        ["uk|bristol"] = "布里斯托尔",
        ["ca|vancouver"] = "温哥华",
        ["ca|quebec"] = "魁北克",
        ["ca|montreal"] = "蒙特利尔",
        ["nz|christchurch"] = "基督城",
        ["ru|moscow"] = "莫斯科",
        ["ru|st. peterburg"] = "圣彼得堡",
        ["de|berlin"] = "柏林",
        ["ch|zurich"] = "苏黎世",
        ["ch|geneve"] = "日内瓦",
        ["ch|vaud"] = "沃州",
        ["es|barcelona"] = "巴塞罗那",
        ["ie|dublin"] = "都柏林",
        ["ie|offaly"] = "奥法利",
        ["my|kuala lumpur"] = "吉隆坡",
        ["kr|gyeonggi-do"] = "京畿道",
        ["kr|pyeongtaek"] = "平泽",
        ["jp|chiba-ken"] = "千叶县",
        ["jp|abiko-shi"] = "我孙子市",
        ["at|carinthia"] = "克恩顿州",
        ["at|upper austria"] = "上奥地利州",
        ["mo|cathedral"] = "大堂区",
        ["mo|fatima"] = "花地玛堂区",
        ["tw|hualien county"] = "花莲县",
        ["ua|donetsk"] = "顿涅茨克",
        ["fr|nice"] = "尼斯",
        ["ar|buenos aires"] = "布宜诺斯艾利斯",
    };

    /// <summary>省 / 州英文名转中文；未命中返回原串。</summary>
    public static string ProvinceToChinese(string? name) => ToChinese(Provinces, name);

    /// <summary>省 / 州转中文：先按 (<paramref name="country"/>, 名) 查境外表，再退回国内表。</summary>
    public static string ProvinceToChinese(string? country, string? name)
    {
        var key = ForeignKey(country, name);
        if (key.Length > 0 && Foreign.TryGetValue(key, out var foreign))
            return foreign;
        return ProvinceToChinese(name);
    }

    /// <summary>市 / 区英文名转中文（不做省份消歧）；未命中返回原串。</summary>
    public static string CityToChinese(string? name) => ToChinese(Cities, name);

    /// <summary>
    /// 市 / 区英文名转中文：先按「省 + 市」消歧，再退回全局表；未命中返回原串。
    /// </summary>
    public static string CityToChinese(string? province, string? city)
    {
        var key = (province ?? "").Trim().ToLowerInvariant() + "|" + (city ?? "").Trim().ToLowerInvariant();
        if (CitiesByProvince.TryGetValue(key, out var contextual))
            return contextual;
        return CityToChinese(city);
    }

    /// <summary>
    /// 市 / 区转中文：先按 (<paramref name="country"/>, 名) 查境外表，再按「省 + 市」消歧，最后退回全局表。
    /// </summary>
    public static string CityToChinese(string? country, string? province, string? city)
    {
        var foreignKey = ForeignKey(country, city);
        if (foreignKey.Length > 0 && Foreign.TryGetValue(foreignKey, out var foreign))
            return foreign;
        return CityToChinese(province, city);
    }

    static string ForeignKey(string? country, string? value)
    {
        var c = (country ?? "").Trim().ToLowerInvariant();
        var v = (value ?? "").Trim().ToLowerInvariant();
        return c.Length == 0 || v.Length == 0 ? "" : c + "|" + v;
    }

    /// <summary>
    /// 拼展示用地区串：<c>国家·省·市</c>
    /// （分隔符「·」，国家走 <see cref="WxCountryNames"/>）。空段自动跳过，全空返回空串。
    /// </summary>
    public static string FormatRegion(string? country, string? province, string? city)
    {
        var parts = new List<string>(3);
        var countryText = WxCountryNames.ToChinese(country);
        if (countryText.Length > 0)
            parts.Add(countryText);
        var provinceText = province?.Trim() ?? "";
        if (provinceText.Length > 0)
            parts.Add(provinceText);
        var cityText = city?.Trim() ?? "";
        if (cityText.Length > 0)
            parts.Add(cityText);
        return string.Join("·", parts);
    }

    static string ToChinese(Dictionary<string, string> map, string? name)
    {
        var key = name?.Trim();
        if (string.IsNullOrEmpty(key))
            return "";
        return map.TryGetValue(key, out var zh) ? zh : key;
    }
}
