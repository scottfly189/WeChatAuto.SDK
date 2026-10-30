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
    /// dHash 归一化目标尺寸：裁剪出文字包围盒后缩放到 (HashWidth+1) x HashHeight，得到 HashWidth*HashHeight 位。
    /// 64 x 32 = 2048 位。这里的缩放和旧版 8x8 / 16x16 本质不同：裁剪后框内是纯文字（没有空白稀释），
    /// 且尺寸足够大、笔画清晰，既提高命中率又不会串号。
    /// </summary>
    private const int HashWidth = 64;
    private const int HashHeight = 32;

    /// <summary>
    /// 命中允许的最大汉明距离，按哈希位数的千分比给出（30‰ = 3%）。
    /// 用于吸收文字抗锯齿/子像素与 DPI 缩放带来的少量像素差异，同时又远小于不同昵称之间的位差，不会串号。
    /// 命中率偏低时调大，出现串号时调小。
    /// </summary>
    private const int MaxHammingPermil = 30;

    /// <summary>判断像素是否为文字（非背景）的灰度差阈值：越小越敏感，越大越严格。</summary>
    private const int TextDiffThreshold = 30;

    static ImageHashCacheHelper()
    {
        if (!Directory.Exists(_RootCachePath))
        {
            Directory.CreateDirectory(_RootCachePath);
        }
    }

    /// <summary>
    /// 计算 Mat 的 dHash（差分感知哈希）。
    /// 先转灰度，再裁出文字包围盒（去掉四周/右侧大量空白），只对文字区域缩放到 (HashWidth+1) x HashHeight 后计算 dHash。
    /// 空白是串号的元凶：整条昵称里空白占绝大多数时，阈值会被空白稀释、文字差异被淹没，导致不同昵称的哈希趋同。
    /// 裁掉空白后框内只有文字，缩放只做 DPI 归一化与低通滤波，不会再把文字稀释掉。
    /// 若裁剪图完全没有文字（全空白），返回 null，表示不参与缓存匹配。
    /// 注意：本方法只读取传入的 Mat，不会修改或释放它，调用方后续可继续使用原图。
    /// </summary>
    public static byte[] ComputeDHash(Mat mat)
    {
        if (mat == null || mat.IsEmpty)
            throw new ArgumentException("Mat 不能为空", nameof(mat));

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

        var bbox = FindTextBoundingBox(gray);
        if (!bbox.HasValue)
            return null;

        using Mat textOnly = new Mat(gray, bbox.Value);
        using Mat resized = new Mat();
        CvInvoke.Resize(textOnly, resized, new Size(HashWidth + 1, HashHeight), 0, 0, Inter.Area);

        int totalBits = HashWidth * HashHeight;
        var bytes = new byte[(totalBits + 7) / 8];
        int bitIndex = 0;
        for (int y = 0; y < HashHeight; y++)
        {
            for (int x = 0; x < HashWidth; x++)
            {
                byte left = Marshal.ReadByte(resized.DataPointer, y * resized.Step + x);
                byte right = Marshal.ReadByte(resized.DataPointer, y * resized.Step + x + 1);
                if (left > right)
                {
                    bytes[bitIndex / 8] |= (byte)(1 << (bitIndex % 8));
                }
                bitIndex++;
            }
        }
        return bytes;
    }

    /// <summary>
    /// 计算灰度图中“文字”的包围盒：取直方图众数作为背景色，把与背景灰度差超过 TextDiffThreshold 的像素视为文字。
    /// 用于裁掉昵称文字四周（尤其右侧）的大量空白，避免空白稀释阈值导致串号。没有任何文字像素时返回 null。
    /// 背景色取众数而非固定值，可同时适配浅色/深色主题。
    /// </summary>
    private static Rectangle? FindTextBoundingBox(Mat gray)
    {
        var hist = new int[256];
        for (int y = 0; y < gray.Rows; y++)
        {
            for (int x = 0; x < gray.Cols; x++)
            {
                byte v = Marshal.ReadByte(gray.DataPointer, y * gray.Step + x);
                hist[v]++;
            }
        }

        int bg = 0;
        int maxCount = -1;
        for (int i = 0; i < 256; i++)
        {
            if (hist[i] > maxCount)
            {
                maxCount = hist[i];
                bg = i;
            }
        }

        int minX = gray.Cols, minY = gray.Rows, maxX = -1, maxY = -1;
        for (int y = 0; y < gray.Rows; y++)
        {
            for (int x = 0; x < gray.Cols; x++)
            {
                byte v = Marshal.ReadByte(gray.DataPointer, y * gray.Step + x);
                int diff = v > bg ? v - bg : bg - v;
                if (diff > TextDiffThreshold)
                {
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                }
            }
        }

        if (maxX < minX)
            return null;
        return new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
    }

    /// <summary>
    /// 计算两个哈希的汉明距离（不同位的个数）。长度不同返回 int.MaxValue，表示不可比较。
    /// </summary>
    public static int HammingDistance(byte[] a, byte[] b)
    {
        if (a == null || b == null || a.Length != b.Length)
            return int.MaxValue;
        int distance = 0;
        for (int i = 0; i < a.Length; i++)
        {
            byte x = (byte)(a[i] ^ b[i]);
            while (x != 0)
            {
                x &= (byte)(x - 1);   // 每次清除最低位的 1
                distance++;
            }
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
            // 旧版缓存文件（哈希字段类型不同）与新格式不兼容，视为无缓存，之后会以新格式重写，避免反序列化崩溃。
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
    /// 往cache中添加一个item.
    /// </summary>
    /// <param name="who">群聊昵称</param>
    /// <param name="item">图片cache项目</param>
    public static void AddImageCacheItem(string who, ImageCacheItem item)
    {
        // 没有可哈希内容的空条目直接跳过，避免互相误命中
        if (item == null || item.HashCode == null || item.HashCode.Length == 0)
            return;
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
        if (hashCode == null)
            return null;
        return GetImageCacheObject(who, hashCode);
    }

    public static ImageCacheItem GetImageCacheObject(string who, byte[] hashCode)
    {
        if (hashCode == null || hashCode.Length == 0)
            return null;
        var list = GetImageCaches(who);
        return FindBestMatch(list, hashCode);
    }

    /// <summary>
    /// 在缓存列表中查找与给定哈希汉明距离最小、且在阈值范围内的记录.
    /// 哈希长度不同视为不匹配，避免跨尺寸误命中。
    /// </summary>
    private static ImageCacheItem FindBestMatch(List<ImageCacheItem> list, byte[] hashCode)
    {
        int allowedDistance = Math.Max(8, hashCode.Length * 8 * MaxHammingPermil / 1000);
        ImageCacheItem best = null;
        int bestDistance = int.MaxValue;
        foreach (var item in list)
        {
            if (item.HashCode == null || item.HashCode.Length != hashCode.Length)
                continue;
            int d = HammingDistance(item.HashCode, hashCode);
            if (d < bestDistance)
            {
                bestDistance = d;
                best = item;
            }
        }
        if (best != null && bestDistance <= allowedDistance)
            return best;
        return null;
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
