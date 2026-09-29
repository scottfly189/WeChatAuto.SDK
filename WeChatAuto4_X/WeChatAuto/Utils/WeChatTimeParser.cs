using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
namespace WeChatAuto.Utils
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public class WeChatTimeParser
    {
        /// <summary>
        /// 将微信 UI Tree 的时间字符串转换为真实 DateTime
        /// </summary>
        /// <param name="timeStr">UI 拿到的时间文本，例如 "昨天 23:53"</param>
        /// <param name="baseTime">基准参考时间，默认取当前 DateTime.Now</param>
        /// <returns>解析后的 DateTime，解析失败返回 null</returns>
        public static DateTime? ParseWeChatTime(string timeStr, DateTime? baseTime = null)
        {
            if (string.IsNullOrWhiteSpace(timeStr))
                return null;

            DateTime now = baseTime ?? DateTime.Now;
            timeStr = timeStr.Trim();

            // 1. 当天格式："22:53"
            Match match = Regex.Match(timeStr, @"^(\d{1,2}):(\d{2})$");
            if (match.Success)
            {
                int hour = int.Parse(match.Groups[1].Value);
                int minute = int.Parse(match.Groups[2].Value);
                return new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
            }

            // 2. 昨天/前天格式："昨天 23:53" 或 "前天 23:53"
            match = Regex.Match(timeStr, @"^(昨天|前天)\s+(\d{1,2}):(\d{2})$");
            if (match.Success)
            {
                string dayType = match.Groups[1].Value;
                int hour = int.Parse(match.Groups[2].Value);
                int minute = int.Parse(match.Groups[3].Value);

                int daysAgo = dayType == "昨天" ? 1 : 2;
                DateTime targetDate = now.Date.AddDays(-daysAgo);
                return new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, hour, minute, 0);
            }

            // 3. 星期几格式："星期五 14:18" 或 "周五 14:18"
            match = Regex.Match(timeStr, @"^(星期|周)([一|二|三|四|五|六|日|天])\s+(\d{1,2}):(\d{2})$");
            if (match.Success)
            {
                DayOfWeek targetDayOfWeek = ConvertChineseToDayOfWeek(match.Groups[2].Value);
                int hour = int.Parse(match.Groups[3].Value);
                int minute = int.Parse(match.Groups[4].Value);

                // 计算与上一个星期几的天数差
                int daysOffset = ((int)now.DayOfWeek - (int)targetDayOfWeek + 7) % 7;
                if (daysOffset == 0) daysOffset = 7; // 如果计算出的星期与今天相同，微信显示的是上周的该天

                DateTime targetDate = now.Date.AddDays(-daysOffset);
                return new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, hour, minute, 0);
            }
            // 增加对: 9月24日 星期四 22:01 格式的支持
            match = Regex.Match(
                    timeStr,
                    @"^(\d{1,2})月(\d{1,2})日\s+(星期|周)([一二三四五六日天])\s+(\d{1,2}):(\d{2})$");
            if (match.Success)
            {
                int month = int.Parse(match.Groups[1].Value);
                int day = int.Parse(match.Groups[2].Value);
                int hour = int.Parse(match.Groups[5].Value);
                int minute = int.Parse(match.Groups[6].Value);

                int year = now.Year;
                DateTime calculatedDate = new DateTime(year, month, day, hour, minute, 0);

                // 如果推算出的日期比当前时间晚，说明是上一年的日期
                if (calculatedDate > now)
                {
                    year--;
                }

                return new DateTime(year, month, day, hour, minute, 0);
            }

            // 4. 月日格式："8月9日 22:39"
            match = Regex.Match(timeStr, @"^(\d{1,2})月(\d{1,2})日\s+(\d{1,2}):(\d{2})$");
            if (match.Success)
            {
                int month = int.Parse(match.Groups[1].Value);
                int day = int.Parse(match.Groups[2].Value);
                int hour = int.Parse(match.Groups[3].Value);
                int minute = int.Parse(match.Groups[4].Value);

                // 微信中当年不带年份，但如果是过去12个月内的月份（例如当前1月，收到12月消息），可能跨年
                int year = now.Year;
                DateTime calculatedDate = new DateTime(year, month, day, hour, minute, 0);

                // 如果推算出的日期比今天晚，说明是去年的这个月份
                if (calculatedDate > now)
                {
                    year -= 1;
                }

                return new DateTime(year, month, day, hour, minute, 0);
            }

            // 5. 完整年月日格式："2025年10月29日 15:43"
            match = Regex.Match(timeStr, @"^(\d{4})年(\d{1,2})月(\d{1,2})日\s+(\d{1,2}):(\d{2})$");
            if (match.Success)
            {
                int year = int.Parse(match.Groups[1].Value);
                int month = int.Parse(match.Groups[2].Value);
                int day = int.Parse(match.Groups[3].Value);
                int hour = int.Parse(match.Groups[4].Value);
                int minute = int.Parse(match.Groups[5].Value);

                return new DateTime(year, month, day, hour, minute, 0);
            }

            return null;
        }

        private static DayOfWeek ConvertChineseToDayOfWeek(string chineseDay)
        {
            return chineseDay switch
            {
                "一" => DayOfWeek.Monday,
                "二" => DayOfWeek.Tuesday,
                "三" => DayOfWeek.Wednesday,
                "四" => DayOfWeek.Thursday,
                "五" => DayOfWeek.Friday,
                "六" => DayOfWeek.Saturday,
                "日" or "天" => DayOfWeek.Sunday,
                _ => throw new ArgumentException("无效的星期字符串")
            };
        }
    }
}

