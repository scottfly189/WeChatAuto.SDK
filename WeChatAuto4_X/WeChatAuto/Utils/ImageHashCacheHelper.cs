using MessagePack;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using WeChatAuto.Models;

namespace WeChatAuto.Utils;

/// <summary>
/// 昵称Image的缓存帮助类
/// </summary>
public static class ImageHashCacheHelper
{
    private static string _RootCachePath = Path.Combine(AppContext.BaseDirectory, "Temp");

    /// <summary>
    /// dHash 为 64 位，此阈值表示最多允许多少个位不同仍视为同一昵称图。
    /// 阈值越大命中率越高，但不同昵称被误判为同一张图的风险也越大，可按需调整。
    /// </summary>
    private const int MaxHammingDistance = 8;

    static ImageHashCacheHelper()
    {
        if (!Directory.Exists(_RootCachePath))
        {
            Directory.CreateDirectory(_RootCachePath);
        }
    }

    /// <summary>
    /// 计算 Mat 的 dHash（差分感知哈希，64 位）。
    /// 先转灰度，再缩放到 9x8 做低通滤波，有效去除文字抗锯齿/子像素带来的逐像素差异。
    /// 注意：本方法只读取传入的 Mat，不会修改或释放它，调用方后续可继续使用原图。
    /// </summary>
    public static ulong ComputeDHash(Mat mat)
    {
        if (mat == null || mat.IsEmpty)
            throw new ArgumentException("Mat 不能为空", nameof(mat));

        const int hashSize = 8;

        using Mat gray = new Mat();
        switch (mat.NumberOfChannels)
        {
            case 1:
                mat.CopyTo(gray);
                break;
            case 4:
                CvInvoke.CvtColor(mat, gray, ColorConversion.Bgra2Gray);
                break;
            default:
                CvInvoke.CvtColor(mat, gray, ColorConversion.Bgr2Gray);
                break;
        }

        using Mat resized = new Mat();
        CvInvoke.Resize(gray, resized, new Size(hashSize + 1, hashSize), 0, 0, Inter.Area);

        ulong hash = 0;
        for (int y = 0; y < hashSize; y++)
        {
            for (int x = 0; x < hashSize; x++)
            {
                byte left = Marshal.ReadByte(resized.DataPointer, y * resized.Step + x);
                byte right = Marshal.ReadByte(resized.DataPointer, y * resized.Step + x + 1);
                if (left > right)
                {
                    hash |= 1UL << (y * hashSize + x);
                }
            }
        }
        return hash;
    }

    /// <summary>
    /// 计算两个 64 位感知哈希的汉明距离（不同位的个数）。
    /// </summary>
    public static int HammingDistance(ulong a, ulong b)
    {
        ulong x = a ^ b;
        int distance = 0;
        while (x != 0)
        {
            x &= x - 1;   // 每次清除最低位的 1
            distance++;
        }
        return distance;
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
        try
        {
            byte[] bytes = File.ReadAllBytes(path);
            result = MessagePack.MessagePackSerializer.Deserialize<List<ImageCacheItem>>(bytes);
        }
        catch (Exception)
        {
            // 旧版缓存文件（HashCode 为 string 类型）与新格式不兼容，视为无缓存，之后会以新格式重写。
            result = new List<ImageCacheItem>();
        }
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
    /// 在缓存列表中查找与给定哈希汉明距离最小、且在阈值范围内的记录.
    /// </summary>
    private static ImageCacheItem FindBestMatch(List<ImageCacheItem> list, ulong hashCode)
    {
        ImageCacheItem best = null;
        int bestDistance = int.MaxValue;
        foreach (var item in list)
        {
            int d = HammingDistance(item.HashCode, hashCode);
            if (d < bestDistance)
            {
                bestDistance = d;
                best = item;
            }
        }
        if (best != null && bestDistance <= MaxHammingDistance)
            return best;
        return null;
    }

    /// <summary>
    /// 往cache中添加一个item.
    /// </summary>
    /// <param name="who">群聊昵称</param>
    /// <param name="item">图片cache项目</param>
    public static void AddImageCacheItem(string who, ImageCacheItem item)
    {
        var list = GetImageCaches(who);
        var result = FindBestMatch(list, item.HashCode);
        if (result != null)
            return;
        list.Add(item);
        SaveImageCaches(who, list);
    }

    public static ImageCacheItem GetImageCacheItem(string who, Mat mat)
    {
        var hashCode = ComputeDHash(mat);
        return GetImageCacheObject(who, hashCode);
    }

    public static ImageCacheItem GetImageCacheObject(string who, ulong hashCode)
    {
        var list = GetImageCaches(who);
        return FindBestMatch(list, hashCode);
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
