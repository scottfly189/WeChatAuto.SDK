using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WxAutoCore.Utils
{
    /// <summary>
    /// Windows剪贴板API工具类
    /// </summary>
    public static class ClipboardApi
    {
        // 剪贴板相关API
        [DllImport("user32.dll")]
        static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        static extern bool EmptyClipboard();

        [DllImport("user32.dll")]
        static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

        [DllImport("kernel32.dll")]
        static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll")]
        static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        static extern IntPtr GlobalFree(IntPtr hMem);

        // 剪贴板格式常量
        public const uint CF_HDROP = 15;

        // 全局内存分配标志
        const uint GMEM_MOVEABLE = 0x0002;
        const uint GMEM_ZEROINIT = 0x0040;

        /// <summary>
        /// 将文件复制到剪贴板
        /// </summary>
        /// <param name="filePaths">文件路径数组</param>
        /// <returns>是否成功</returns>
        public static bool CopyFilesToClipboard(string[] filePaths)
        {
            if (filePaths == null || filePaths.Length == 0)
                return false;

            // 验证所有文件是否存在
            foreach (var filePath in filePaths)
            {
                if (!System.IO.File.Exists(filePath) && !System.IO.Directory.Exists(filePath))
                    return false;
            }

            // 打开剪贴板
            if (!OpenClipboard(IntPtr.Zero))
                return false;

            try
            {
                // 清空剪贴板
                EmptyClipboard();

                // 创建DROPFILES结构
                var dropFiles = CreateDropFilesStructure(filePaths);
                if (dropFiles == IntPtr.Zero)
                    return false;

                // 设置剪贴板数据
                var result = SetClipboardData(CF_HDROP, dropFiles);
                return result != IntPtr.Zero;
            }
            finally
            {
                CloseClipboard();
            }
        }

        /// <summary>
        /// 将单个文件复制到剪贴板
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否成功</returns>
        public static bool CopyFileToClipboard(string filePath)
        {
            return CopyFilesToClipboard(new[] { filePath });
        }

        /// <summary>
        /// 创建DROPFILES结构
        /// </summary>
        /// <param name="filePaths">文件路径数组</param>
        /// <returns>DROPFILES结构的内存指针</returns>
        private static IntPtr CreateDropFilesStructure(string[] filePaths)
        {
            // 计算所需内存大小
            int totalSize = 20; // DROPFILES结构大小
            foreach (var filePath in filePaths)
            {
                totalSize += (filePath.Length + 1) * 2; // Unicode字符串
            }
            totalSize += 2; // 双重null终止符

            // 分配全局内存
            var hGlobal = GlobalAlloc(GMEM_MOVEABLE | GMEM_ZEROINIT, (UIntPtr)totalSize);
            if (hGlobal == IntPtr.Zero)
                return IntPtr.Zero;

            // 锁定内存
            var pData = GlobalLock(hGlobal);
            if (pData == IntPtr.Zero)
            {
                GlobalFree(hGlobal);
                return IntPtr.Zero;
            }

            try
            {
                // 写入DROPFILES结构
                var offset = 0;
                
                // pFiles (4字节) - 文件列表的偏移量
                Marshal.WriteInt32(pData, offset, 20);
                offset += 4;
                
                // pt (8字节) - 拖放点坐标 (0,0)
                Marshal.WriteInt32(pData, offset, 0);
                offset += 4;
                Marshal.WriteInt32(pData, offset, 0);
                offset += 4;
                
                // fNC (4字节) - 非客户端区域标志
                Marshal.WriteInt32(pData, offset, 0);
                offset += 4;
                
                // fWide (4字节) - Unicode标志
                Marshal.WriteInt32(pData, offset, 1);
                offset += 4;

                // 写入文件路径列表
                foreach (var filePath in filePaths)
                {
                    var bytes = Encoding.Unicode.GetBytes(filePath + "\0");
                    Marshal.Copy(bytes, 0, pData + offset, bytes.Length);
                    offset += bytes.Length;
                }
                
                // 添加双重null终止符
                Marshal.WriteInt16(pData, offset, 0);
                offset += 2;
                Marshal.WriteInt16(pData, offset, 0);

                return hGlobal;
            }
            finally
            {
                GlobalUnlock(hGlobal);
            }
        }
    }
}
