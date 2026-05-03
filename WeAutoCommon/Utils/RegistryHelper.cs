using Microsoft.Win32;
using System;

namespace WeAutoCommon.Utils
{
    public static class RegistryHelper
    {
        /// <summary>
        /// 写入 DWORD (int)
        /// </summary>
        public static void SetDword(RegistryHive hive, string subKey, string name, int value)
        {
            using (var baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default))
            {
                using (var key = baseKey.CreateSubKey(subKey, true))
                {

                    if (key == null)
                        throw new Exception("无法打开或创建注册表项");

                    key.SetValue(name, value, RegistryValueKind.DWord);
                }
            }
        }

        /// <summary>
        /// 读取 DWORD
        /// </summary>
        public static int? GetDword(RegistryHive hive, string subKey, string name)
        {
            using (var baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default))
            {
                using (var key = baseKey.OpenSubKey(subKey))
                {

                    if (key == null)
                        return null;

                    var value = key.GetValue(name);
                    if (value == null)
                        return null;

                    return Convert.ToInt32(value);
                }
            }
        }

        /// <summary>
        /// 删除值
        /// </summary>
        public static void DeleteValue(RegistryHive hive, string subKey, string name)
        {
            using (var baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default))
            {
                using (var key = baseKey.OpenSubKey(subKey, true))
                {
                    key?.DeleteValue(name, false);
                }
            }
        }
    }
}
