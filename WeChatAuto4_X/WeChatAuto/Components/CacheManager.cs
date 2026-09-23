using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon.Models;
using WeAutoCommon.Utils;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using WeAutoCommon.Enums;
using System.Threading.Tasks;
using FlaUI.UIA3;
using WeAutoCommon.Extentions;
using System.IO;
using System.Windows;
using FlaUI.Core;
using MessagePack;
using System.Net.Http;
using System.Drawing;
using WeChatAuto.Options;
using WeChatAuto.Models;


namespace WeChatAuto.Components
{
    /// <summary>
    /// cache管理类
    /// </summary>
    public class CacheManager
    {
        private readonly SemaphoreSlim _cacheLock = new SemaphoreSlim(1, 1);
        private WeChatClient _Client;
        internal CacheManager(WeChatClient client)
        {
            this._Client = client;
        }
        /// <summary>
        /// 显示缓存中存储的好友信息.
        /// </summary>
        /// <returns></returns>
        public List<FriendInfo> GetFriendListFromCache()
        {
            try
            {
                _cacheLock.Wait();
                List<FriendInfo> list = new List<FriendInfo>();
                var path = Path.Combine(AppContext.BaseDirectory, _Client.WxId + "_cache.dat");
                if (File.Exists(path))
                {
                    byte[] bytes = File.ReadAllBytes(path);
                    return MessagePackSerializer.Deserialize<List<FriendInfo>>(bytes);
                }
                return list;
            }
            finally
            {
                _cacheLock.Release();
            }
        }
        /// <summary>
        /// 显示缓存中存储的好友信息,异步调用
        /// </summary>
        /// <returns></returns>
        public async Task<List<FriendInfo>> GetFriendListFromCacheAsync()
        {
            try
            {
                await _cacheLock.WaitAsync();
                List<FriendInfo> list = new List<FriendInfo>();
                var path = Path.Combine(AppContext.BaseDirectory, _Client.WxId + "_cache.dat");
                if (File.Exists(path))
                {
                    byte[] bytes = await File.ReadAllBytesAsync(path);
                    using MemoryStream ms = new MemoryStream(bytes);
                    return await MessagePackSerializer.DeserializeAsync<List<FriendInfo>>(ms);
                }
                return list;
            }
            finally
            {
                _cacheLock.Release();
            }
        }
        /// <summary>
        /// 从缓存中得到一个好友的信息
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <returns>好友对象，请参考:<see cref="FriendInfo"/></returns>
        public FriendInfo GetFriendFromCache(string who)
        {
            if (string.IsNullOrWhiteSpace(who))
                return null;
            List<FriendInfo> list = GetFriendListFromCache();
            return list.FirstOrDefault(x => x.Name == who);
        }
        /// <summary>
        /// 从缓存中得到一个好友的信息
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <returns>好友对象，请参考:<see cref="FriendInfo"/></returns>
        public List<FriendInfo> GetFriendsFromCache(string who)
        {
            if (string.IsNullOrWhiteSpace(who))
                return null;
            List<FriendInfo> list = GetFriendListFromCache();
            return list;
        }
        /// <summary>
        /// 从缓存中得到一个好友的信息,因为名字可能重复，而wxid永远不重复
        /// </summary>
        /// <param name="wxid">微信号</param>
        /// <returns>好友对象，请参考:<see cref="FriendInfo"/></returns>
        public FriendInfo GetFriendWithWxIDFromCache(string wxid)
        {
            if (string.IsNullOrWhiteSpace(wxid))
                return null;
            List<FriendInfo> list = GetFriendListFromCache();
            return list.FirstOrDefault(x => x.WxId == wxid);
        }
        /// <summary>
        /// 从缓存中得到一个好友的信息
        /// </summary>
        /// <param name="who"></param>
        /// <returns>好友对象，请参考:<see cref="FriendInfo"/></returns>
        public async Task<FriendInfo> GetFriendFromCacheAsync(string who)
        {
            if (string.IsNullOrWhiteSpace(who))
                return null;
            List<FriendInfo> list = await GetFriendListFromCacheAsync();
            return list.FirstOrDefault(x => x.Name == who);
        }
        /// <summary>
        /// 从缓存中得到一个好友的信息,通过wxid来获取，因为名字可能重复,而wxid号永远不重复
        /// </summary>
        /// <param name="wxid">微信号</param>
        /// <returns>好友对象，请参考:<see cref="FriendInfo"/></returns>
        public async Task<FriendInfo> GetFriendWithWxIDFromCacheAsync(string wxid)
        {
            if (string.IsNullOrWhiteSpace(wxid))
                return null;
            List<FriendInfo> list = await GetFriendListFromCacheAsync();
            return list.FirstOrDefault(x => x.WxId == wxid);

        }
        /// <summary>
        /// 从缓存中移除一个好友
        /// </summary>
        /// <param name="who"></param>
        public void RemoveFriendFromCache(string who)
        {
            if (string.IsNullOrWhiteSpace(who))
                return;
            List<FriendInfo> friendInfos = GetFriendListFromCache();
            friendInfos = friendInfos.Where(u => !u.Name.Equals(who)).ToList();
            try
            {
                _cacheLock.Wait();
                byte[] bytes = MessagePack.MessagePackSerializer.Serialize<List<FriendInfo>>(friendInfos);
                var path = Path.Combine(AppContext.BaseDirectory, _Client.WxId + "_cache.dat");
                File.WriteAllBytes(path, bytes);
            }
            finally
            {
                _cacheLock.Release();
            }
        }
        /// <summary>
        /// 从缓存中移除一个好友,通过微信id，因为通过微信名可能会重复,而微信id号永不重复
        /// </summary>
        /// <param name="wxid">微信号</param>
        public void RemoveFriendWithWxIDFromCache(string wxid)
        {
            if (string.IsNullOrWhiteSpace(wxid))
                return;
            List<FriendInfo> friendInfos = GetFriendListFromCache();
            friendInfos = friendInfos.Where(u => !u.WxId.Equals(wxid)).ToList();
            try
            {
                _cacheLock.Wait();
                byte[] bytes = MessagePack.MessagePackSerializer.Serialize<List<FriendInfo>>(friendInfos);
                var path = Path.Combine(AppContext.BaseDirectory, _Client.WxId + "_cache.dat");
                File.WriteAllBytes(path, bytes);
            }
            finally
            {
                _cacheLock.Release();
            }
        }
        /// <summary>
        /// 从缓存中移除一个好友,异步方法
        /// </summary>
        /// <param name="who"></param>
        public async Task RemoveFriendFromCacheAsync(string who)
        {
            await _cacheLock.WaitAsync();
            if (string.IsNullOrWhiteSpace(who))
                return;
            List<FriendInfo> friendInfos = await GetFriendListFromCacheAsync();
            try
            {
                await _cacheLock.WaitAsync();
                friendInfos = friendInfos.Where(u => !u.Name.Equals(who)).ToList();
                using MemoryStream ms = new MemoryStream();
                await MessagePack.MessagePackSerializer.SerializeAsync<List<FriendInfo>>(ms, friendInfos);
                var path = Path.Combine(AppContext.BaseDirectory, _Client.WxId + "_cache.dat");
                await File.WriteAllBytesAsync(path, ms.ToArray());
            }
            finally
            {
                _cacheLock.Release();
            }
        }
        /// <summary>
        /// 从缓存中移除一个好友,通过wxid,异步方法
        /// </summary>
        /// <param name="wxid"></param>
        public async Task RemoveFriendWithWxIDFromCacheAsync(string wxid)
        {
            await _cacheLock.WaitAsync();
            if (string.IsNullOrWhiteSpace(wxid))
                return;
            List<FriendInfo> friendInfos = await GetFriendListFromCacheAsync();
            friendInfos = friendInfos.Where(u => !u.WxId.Equals(wxid)).ToList();
            try
            {
                await _cacheLock.WaitAsync();
                using MemoryStream ms = new MemoryStream();
                await MessagePack.MessagePackSerializer.SerializeAsync<List<FriendInfo>>(ms, friendInfos);
                var path = Path.Combine(AppContext.BaseDirectory, _Client.WxId + "_cache.dat");
                await File.WriteAllBytesAsync(path, ms.ToArray());
            }
            finally
            {
                _cacheLock.Release();
            }
        }
        /// <summary>
        /// 手动增加或者修改一个好友对象
        /// </summary>
        /// <param name="friend">好友对象，请参考<see cref="FriendInfo"/></param>
        public void AddOrUpdateFriendFromCache(FriendInfo friend)
        {
            if (friend == null || string.IsNullOrWhiteSpace(friend.MemoName) || string.IsNullOrWhiteSpace(friend.WxId))
                return;
            try
            {
                List<FriendInfo> friendInfos = GetFriendListFromCache();
                var old = friendInfos.FirstOrDefault(u => u.WxId == friend.WxId);
                if (old != null)
                {
                    //修改
                    old.NickName = friend.NickName;
                    old.MemoName = friend.MemoName;
                    old.Area = friend.Area;
                    old.Lable = friend.Lable;
                    old.SameGroupNumber = friend.SameGroupNumber;
                    old.Signature = friend.Signature;
                    old.Source = friend.Source;
                    old.WxId = string.IsNullOrWhiteSpace(friend.WxId) ? old.WxId : friend.WxId;
                    old.AvatarPath = friend.AvatarPath;
                    old.ChatType = friend.ChatType;
                    old.AddDateTime = friend.AddDateTime;
                }
                else
                {
                    friendInfos.Add(friend);
                }
                _cacheLock.Wait();
                byte[] bytes = MessagePack.MessagePackSerializer.Serialize<List<FriendInfo>>(friendInfos);
                var path = Path.Combine(AppContext.BaseDirectory, _Client.WxId + "_cache.dat");
                File.WriteAllBytes(path, bytes);
            }
            finally
            {
                _cacheLock.Release();
            }
        }
        /// <summary>
        /// 手动增加或者修改一个好友对象，使用异步方法
        /// </summary>
        /// <param name="friend">好友对象，请参考<see cref="FriendInfo"/></param>
        public async Task AddOrUpdateFriendFromCacheAsync(FriendInfo friend)
        {

            if (friend == null || string.IsNullOrWhiteSpace(friend.MemoName) || string.IsNullOrWhiteSpace(friend.WxId))
                return;
            List<FriendInfo> friendInfos = await GetFriendListFromCacheAsync();
            var old = friendInfos.FirstOrDefault(u => u.WxId == friend.WxId);
            if (old != null)
            {
                //修改
                old.NickName = friend.NickName;
                old.MemoName = friend.MemoName;
                old.Area = friend.Area;
                old.Lable = friend.Lable;
                old.SameGroupNumber = friend.SameGroupNumber;
                old.Signature = friend.Signature;
                old.Source = friend.Source;
                old.WxId = string.IsNullOrWhiteSpace(friend.WxId) ? old.WxId : friend.WxId;
                old.AvatarPath = friend.AvatarPath;
                old.ChatType = friend.ChatType;
                old.AddDateTime = friend.AddDateTime;
            }
            else
            {
                friendInfos.Add(friend);
            }
            try
            {
                await _cacheLock.WaitAsync();
                var path = Path.Combine(AppContext.BaseDirectory, _Client.WxId + "_cache.dat");
                using FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
                await MessagePack.MessagePackSerializer.SerializeAsync(fs, friendInfos);
            }
            finally
            {
                _cacheLock.Release();
            }
        }

    }
}