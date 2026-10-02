using MessagePack;
using Emgu.CV;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System;
using WeChatAuto.Models;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace WeChatAuto.Utils;

/// <summary>
/// 昵称Image的缓存帮助类
/// </summary>
public static class ImageHashCacheHelper
{
    private static string _RootCachePath = Path.Combine(AppContext.BaseDirectory, "Temp");
    static ImageHashCacheHelper()
    {
        if (!Directory.Exists(_RootCachePath))
        {
            Directory.CreateDirectory(_RootCachePath);
        }
    }

    /// <summary>
    /// 计算 Mat 的 SHA-256 哈希。
    /// Hash 包含 Mat 的尺寸、类型以及像素数据。
    /// </summary>
    public static string ComputeSha256(Mat mat)
    {
        if (mat == null || mat.IsEmpty)
            throw new ArgumentException("Mat 不能为空", nameof(mat));

        using var sha256 = SHA256.Create();

        // 将 Mat 的基本信息加入 Hash
        var metadata = Encoding.UTF8.GetBytes(
            $"{mat.Rows}:{mat.Cols}:{mat.Depth}:{mat.NumberOfChannels}");

        sha256.TransformBlock(
            metadata,
            0,
            metadata.Length,
            null,
            0);

        // 每行实际有效像素数据的字节数
        int rowBytes = checked(mat.Cols * mat.ElementSize);
        var buffer = new byte[rowBytes];

        for (int row = 0; row < mat.Rows; row++)
        {
            IntPtr rowPtr = mat.DataPointer + row * mat.Step;

            Marshal.Copy(
                rowPtr,
                buffer,
                0,
                rowBytes);

            sha256.TransformBlock(
                buffer,
                0,
                buffer.Length,
                null,
                0);
        }

        sha256.TransformFinalBlock(Array.Empty<byte>(), 0, 0);

        return Convert.ToHexString(sha256.Hash!);
    }



    /// <summary>
    /// 获取至某个群的缓存记录列表.
    /// </summary>
    /// <param name="who">群昵称</param>
    /// <returns>缓存列表</returns>
    public static List<ImageCacheItem> GetImageCaches(string who)
    {
        var result = new List<ImageCacheItem>();
        var fileName = GetStandFileName(who);
        var path = Path.Combine(_RootCachePath, $"{fileName}.dat");
        if (!File.Exists(path))
            return result;
        byte[] bytes = File.ReadAllBytes(path);
        result = MessagePack.MessagePackSerializer.Deserialize<List<ImageCacheItem>>(bytes);
        return result;
    }
    /// <summary>
    /// 保存某群的缓存列表.
    /// </summary>
    /// <param name="who">群昵称</param>
    /// <param name="imageCaches">缓存列表</param>
    public static void SaveImageCaches(string who, List<ImageCacheItem> imageCaches)
    {
        var fileName = GetStandFileName(who);
        var path = Path.Combine(_RootCachePath, $"{fileName}.dat");
        byte[] bytes = MessagePack.MessagePackSerializer.Serialize(imageCaches);
        File.WriteAllBytes(path, bytes);
    }

    /// <summary>
    /// 往cache中添加一个item.
    /// </summary>
    /// <param name="who">群聊昵称</param>
    /// <param name="item">图片cache项目</param>
    public static void AddImageCacheItem(string who, ImageCacheItem item)
    {
        var list = GetImageCaches(who);
        var result = list.Find(u => u.HashCode.Equals(item.HashCode));
        if (result != null)
            return;
        list.Add(item);
        SaveImageCaches(who, list);
    }
    public static ImageCacheItem GetImageCacheItem(string who, Mat mat)
    {
        var hashCode = ComputeSha256(mat);
        return GetImageCacheObject(who, hashCode);
    }
    public static ImageCacheItem GetImageCacheObject(string who, string hashCode)
    {
        var list = GetImageCaches(who);
        var result = list.Find(u => u.HashCode.Equals(hashCode));
        return result;
    }
    /// <summary>
    /// 文件名可能会无效,对文件名进行处理
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