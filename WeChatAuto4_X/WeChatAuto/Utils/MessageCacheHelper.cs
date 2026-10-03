
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Emgu.CV.Aruco;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using WeAutoCommon.Configs;
using WeAutoCommon.Models;
using WeAutoCommon.Utils;
using WeChatAuto.Components;
using WeChatAuto.Models;
using WeChatAuto.Services;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 消息缓存助手类，提供消息缓存的读取和写入功能
    /// </summary>
    public static class MessageCacheHelper
    {
        private static string _RootCachePath = Path.Combine(AppContext.BaseDirectory, "MessageCaches");
        private static string _AllDataRootPath;
        static MessageCacheHelper()
        {
            if (!Directory.Exists(_RootCachePath))
            {
                Directory.CreateDirectory(_RootCachePath);
            }
        }
        /// <summary>
        /// 初始化每个微信的消息记录根目录
        /// </summary>
        /// <param name="fromWechat">微信号</param>
        internal static void Initialize(string fromWechat)
        {
            _AllDataRootPath = Path.Combine(_RootCachePath, fromWechat);
            if (!Directory.Exists(_AllDataRootPath))
            {
                Directory.CreateDirectory(_AllDataRootPath);
            }
        }
        /// <summary>
        /// 获取指定好友的今天的消息缓存
        /// </summary>
        /// <param name="who">好友/群聊</param>
        /// <returns>消息列表，具体请参考<see cref="SimpleMessageBubble"/></returns>
        public static List<SimpleMessageBubble> GetTodayMessageCaches(string who) => GetTodayMessageCacheCore(who, DateTime.Today);
        /// <summary>
        /// 获取指定好友的今天的最后几条消息缓存
        /// </summary>
        /// <param name="who">好友/群聊</param>
        /// <param name="lastCount">最后几条消息</param>
        /// <returns>消息列表，具体请参考<see cref="SimpleMessageBubble"/></returns>
        public static List<SimpleMessageBubble> GetTodayLastMessages(string who, int lastCount)
        {
            var messages = GetTodayMessageCacheCore(who, DateTime.Today);
            if (messages.Count <= lastCount)
                return messages;
            return messages.GetRange(messages.Count - lastCount, lastCount);
        }
        public static List<SimpleMessageBubble> GetLastMessages(string who, int lastCount, string fromWechat)
        {
            return null;
        }
        /// <summary>
        /// 获取指定好友的指定日期的消息缓存
        /// </summary>
        /// <param name="who">好友/群聊</param>
        /// <param name="date">日期</param>
        /// <returns>消息列表，具体请参考<see cref="SimpleMessageBubble"/></returns>
        public static List<SimpleMessageBubble> GetTodayMessageCacheCore(string who, DateTime date)
        {
            var result = new List<SimpleMessageBubble>();
            var fileName = GetStandFileName(who);
            var path = Path.Combine(_RootCachePath, date.ToString("yyyy-MM-dd"), $"{fileName}.dat");
            if (!File.Exists(path))
                return result;
            byte[] bytes = File.ReadAllBytes(path);
            result = MessagePack.MessagePackSerializer.Deserialize<List<SimpleMessageBubble>>(bytes);

            return result;
        }
        /// <summary>
        /// 保存今天的消息缓存，保存到以日期为文件夹，好友/群聊为文件名的文件中，文件内容为消息列表，具体请参考<see cref="SimpleMessageBubble"/>
        /// </summary>
        /// <param name="who">好友/群聊</param>
        /// <param name="messages">消息列表,具体请参考<see cref="SimpleMessageBubble"/></param>
        public static void SaveTodayMessageCaches(string who, List<SimpleMessageBubble> messages) => SaveMessageCaches(who, DateTime.Today, messages);

        /// <summary>
        /// 保存消息缓存，保存到以日期为文件夹，好友/群聊为文件名的文件中，文件内容为消息列表，具体请参考<see cref="SimpleMessageBubble"/>
        /// </summary>
        /// <param name="who">好友/群聊</param>
        /// <param name="date">日期</param>
        /// <param name="messages">消息列表,具体请参考<see cref="SimpleMessageBubble"/></param>
        public static void SaveMessageCaches(string who, DateTime date, List<SimpleMessageBubble> messages)
        {
            var dir = Path.Combine(_RootCachePath, date.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var fileName = GetStandFileName(who);
            var path = Path.Combine(dir, $"{fileName}.dat");
            byte[] bytes = MessagePack.MessagePackSerializer.Serialize(messages);
            File.WriteAllBytes(path, bytes);
        }
        /// <summary>
        /// 增加缓存列表中.
        /// </summary>
        /// <param name="who"></param>
        /// <param name="messages"></param>
        public static void AddTodayMessageCaches(string who, List<SimpleMessageBubble> messages)
        {
            List<SimpleMessageBubble> list = GetTodayMessageCaches(who);
            list.AddRange(messages);
            SaveTodayMessageCaches(who, list);
        }
        /// <summary>
        /// 文件名可能会无效.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetStandFileName(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }
            return fileName;
        }
    }
}