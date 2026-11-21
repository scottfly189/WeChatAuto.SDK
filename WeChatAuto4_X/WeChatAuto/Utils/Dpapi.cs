#nullable enable
using System;
using System.Runtime.InteropServices;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// Windows DPAPI 的最小封装（<c>CryptProtectData</c> / <c>CryptUnprotectData</c>，CurrentUser 作用域）。
    /// <para>
    /// 用途：给 <c>wechatkey.dat</c> 里的数据库密钥加密。加密后文件被拷到别的 Windows 用户 / 别的机器
    /// 都解不开，等于「拿到文件也解不了库」；而进程自己读取时不需要额外口令。
    /// </para>
    /// <para>
    /// 刻意不引 <c>System.Security.Cryptography.ProtectedData</c> NuGet：本 SDK 已经 Windows-only，
    /// 直接 P/Invoke crypt32 可以少一个包依赖，也不受多目标框架（net7~net10）版本差异影响。
    /// </para>
    /// <para>失败一律返回 <c>null</c>，由调用方决定「退回明文」还是「当没有缓存」；本类不抛业务异常。</para>
    /// </summary>
    internal static class Dpapi
    {
        /// <summary>禁止 DPAPI 弹 UI：自动化进程没有可交互的桌面会话时，弹窗会直接卡死。</summary>
        const int CryptProtectUiForbidden = 0x1;

        [StructLayout(LayoutKind.Sequential)]
        struct DataBlob
        {
            public int Size;
            public IntPtr Data;
        }

        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool CryptProtectData(
            ref DataBlob dataIn,
            string? description,
            IntPtr optionalEntropy,
            IntPtr reserved,
            IntPtr promptStruct,
            int flags,
            out DataBlob dataOut);

        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool CryptUnprotectData(
            ref DataBlob dataIn,
            IntPtr description,
            IntPtr optionalEntropy,
            IntPtr reserved,
            IntPtr promptStruct,
            int flags,
            out DataBlob dataOut);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LocalFree(IntPtr handle);

        /// <summary>
        /// 用当前用户的 DPAPI 主密钥加密。失败（极少见：无用户配置、策略禁用）返回 <c>null</c>。
        /// </summary>
        public static byte[]? Protect(byte[]? plain, string? description = "WeChatAuto database keys")
        {
            if (plain is null || plain.Length == 0)
                return null;

            var input = ToBlob(plain);
            try
            {
                if (!CryptProtectData(ref input, description, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero,
                        CryptProtectUiForbidden, out var output))
                    return null;

                try { return FromBlob(output); }
                finally { if (output.Data != IntPtr.Zero) LocalFree(output.Data); }
            }
            finally
            {
                if (input.Data != IntPtr.Zero)
                    Marshal.FreeHGlobal(input.Data);
            }
        }

        /// <summary>
        /// 解密。换 Windows 用户、换机器或文件损坏时会失败，返回 <c>null</c>；
        /// 调用方应当把它当成「没有缓存」，而不是「缓存是空的」。
        /// </summary>
        public static byte[]? Unprotect(byte[]? cipher)
        {
            if (cipher is null || cipher.Length == 0)
                return null;

            var input = ToBlob(cipher);
            try
            {
                if (!CryptUnprotectData(ref input, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero,
                        CryptProtectUiForbidden, out var output))
                    return null;

                try { return FromBlob(output); }
                finally { if (output.Data != IntPtr.Zero) LocalFree(output.Data); }
            }
            finally
            {
                if (input.Data != IntPtr.Zero)
                    Marshal.FreeHGlobal(input.Data);
            }
        }

        static DataBlob ToBlob(byte[] data)
        {
            var ptr = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, ptr, data.Length);
            return new DataBlob { Size = data.Length, Data = ptr };
        }

        static byte[] FromBlob(DataBlob blob)
        {
            if (blob.Size <= 0 || blob.Data == IntPtr.Zero)
                return Array.Empty<byte>();

            var bytes = new byte[blob.Size];
            Marshal.Copy(blob.Data, bytes, 0, blob.Size);
            return bytes;
        }
    }
}
