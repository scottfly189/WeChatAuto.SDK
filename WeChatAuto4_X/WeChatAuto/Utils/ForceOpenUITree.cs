using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Exceptions;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 强开 UI Tree。
    /// <para>
    /// 通过向正在运行的微信(Weixin)进程的 Qt 无障碍“门控字节”写入 1，强制微信公开 UI Tree。
    /// 该工具不会启动微信、不会注入代码、不会修改内存页保护，只会写入 Weixin.dll 中
    /// 那一个已存在的 Qt 无障碍状态字节，并在写入前校验进程、模块、PE 节、扫描证据与当前字节值。
    /// </para>
    /// <para>本类移植自 <c>qt_accessibility_activator.py</c>。</para>
    /// </summary>
    public class ForceOpenUITree
    {
        private readonly AutoLogger<ForceOpenUITree> _logger;

        public ForceOpenUITree(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<ForceOpenUITree>>();
        }

        #region 常量

        private const string WeixinProcessName = "weixin";
        private const string WeixinDllName = "weixin.dll";

        private const string MainWindowClassName = "mmui::MainWindow";
        private const string SearchBoxName = "搜索";
        private const string SearchBoxClassName = "mmui::XValidatorTextEdit";
        private const string SearchListAutomationId = "search_list";
        private const string ChatInputAutomationId = "chat_input_field";

        private const uint IMAGE_SCN_MEM_EXECUTE = 0x20000000;
        private const uint IMAGE_SCN_MEM_WRITE = 0x80000000;
        private const uint PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;
        private const uint PROCESS_VM_READ = 0x0010;
        private const uint PROCESS_VM_WRITE = 0x0020;
        private const uint TH32CS_SNAPMODULE = 0x00000008;
        private const uint TH32CS_SNAPMODULE32 = 0x00000010;

        // qt.accessibility.core 字符串，用于定位 Qt 无障碍门控逻辑。
        private static readonly byte[] QAccessibleCoreString = Encoding.ASCII.GetBytes("qt.accessibility.core");
        // 门控字节所在的指令序列前缀：test rcx,rcx; je ...; cmp byte ptr [rip+disp], 0; je ...
        private static readonly byte[] GatePrefix = new byte[] { 0x48, 0x85, 0xC9, 0x0F, 0x84 };
        private const uint GateDistanceLimit = 0x20000;

        // 扫描结果优先。此表仅在扫描器无法给出唯一可佐证候选时，用于特定版本的兜底。
        private static readonly Dictionary<string, uint> QAccessibleActiveRvaByVersion = new Dictionary<string, uint>
        {
            ["4.1.11.22"] = 0x0A1E7DB8u,
        };

        #endregion

        #region 结果类型

        /// <summary>
        /// 强开 UI Tree 的执行结果。
        /// </summary>
        public class ActivationResult
        {
            /// <summary>目标微信进程 PID。</summary>
            public int ProcessId { get; set; }
            /// <summary>微信主程序完整路径。</summary>
            public string ExePath { get; set; }
            /// <summary>Weixin.dll 完整路径。</summary>
            public string DllPath { get; set; }
            /// <summary>Weixin.dll 的加载基址。</summary>
            public long DllBaseAddress { get; set; }
            /// <summary>门控字节相对 Weixin.dll 基址的 RVA。</summary>
            public uint GateRva { get; set; }
            /// <summary>RVA 来源：scanner（扫描器）或 version fallback（版本兜底）。</summary>
            public string RvaSource { get; set; }
            /// <summary>写入前的门控字节值（0 或 1）。</summary>
            public int PreviousValue { get; set; }
            /// <summary>本次是否真正写入了内存。</summary>
            public bool Written { get; set; }
            /// <summary>写后读回验证的字节值。</summary>
            public int ReadbackValue { get; set; }
            /// <summary>
            /// 状态：ALREADY_ACTIVE / CHECK_ONLY_INACTIVE / ACTIVATION_WRITTEN。
            /// </summary>
            public string State { get; set; }
            /// <summary>UIA 验证结果；当 <see cref="ForceOpen(int?, bool, bool, double)"/> 的 verifyUia 为 false 时为 null。</summary>
            public UiaVerification Uia { get; set; }
        }

        /// <summary>
        /// UIA（UI Automation）验证结果，用于确认 UI Tree 是否真正物化。
        /// </summary>
        public class UiaVerification
        {
            /// <summary>是否找到 mmui::MainWindow 主窗口。</summary>
            public bool MainWindow { get; set; }
            /// <summary>是否找到搜索框（mmui::XValidatorTextEdit，Name=搜索）。</summary>
            public bool SearchBox { get; set; }
            /// <summary>是否找到搜索结果列表（AutomationId=search_list）。</summary>
            public bool SearchList { get; set; }
            /// <summary>是否找到聊天输入框（AutomationId=chat_input_field）。</summary>
            public bool ChatInput { get; set; }
            /// <summary>当前聊天窗口联系人名（从输入框 Name 属性读取）。</summary>
            public string ChatName { get; set; }
            /// <summary>验证详情描述。</summary>
            public string Detail { get; set; }

            /// <summary>是否已激活：主窗口与搜索框同时存在。</summary>
            public bool Activated => MainWindow && SearchBox;
        }

        #endregion

        #region 内部数据类型

        private sealed class ModuleInfo
        {
            public long Base;
            public uint Size;
            public string Name;
            public string Path;
        }

        private sealed class Target
        {
            public int Pid;
            public string ExePath;
            public string DllPath;
            public long DllBase;
            public uint Rva;
            public string Source;
        }

        private sealed class PeSection
        {
            public string Name;
            public uint Rva;
            public uint VirtualSize;
            public uint RawSize;
            public uint RawPtr;
            public uint Characteristics;
        }

        #endregion

        #region Win32 P/Invoke

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct MODULEENTRY32
        {
            public uint dwSize;
            public uint th32ModuleID;
            public uint th32ProcessID;
            public uint GlblcntUsage;
            public uint ProccntUsage;
            public IntPtr modBaseAddr;
            public uint modBaseSize;
            public IntPtr hModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExePath;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool Module32FirstW(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool Module32NextW(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool QueryFullProcessImageNameW(IntPtr hProcess, uint dwFlags, StringBuilder lpExeName, ref uint lpdwSize);

        #endregion

        #region 对外方法

        /// <summary>
        /// 检查或强开指定微信进程的 UI Tree。
        /// </summary>
        /// <param name="pid">
        /// 要针对的微信进程 PID；为 null 时自动探测第一个符合条件的微信进程（PID 最小者）。
        /// </param>
        /// <param name="checkOnly">
        /// 为 true 时仅读取门控字节并做 UIA 验证，不写内存。
        /// </param>
        /// <param name="verifyUia">
        /// 为 true 时在操作后执行一次 UIA 验证（对应 Python 的 --no-uia-verify 取反）。
        /// </param>
        /// <param name="uiaTimeoutSeconds">
        /// UIA 验证的最长等待秒数（默认 5 秒，下限 0.1 秒）。
        /// </param>
        /// <returns>执行结果，参见 <see cref="ActivationResult"/>。</returns>
        /// <exception cref="WechatNotOpenedException">未找到符合条件的微信进程（或指定 PID 不可用）。</exception>
        /// <exception cref="InvalidOperationException">打开进程、读写内存或读回验证失败。</exception>
        public ActivationResult ForceOpen(int? pid = null, bool checkOnly = false, bool verifyUia = true, double uiaTimeoutSeconds = 5.0)
        {
            _logger.Info($"UI Tree Activator started (check_only={checkOnly}, pid={(pid.HasValue ? pid.Value.ToString() : "auto")})");

            var target = FindTarget(pid);
            if (target == null)
            {
                string message = pid.HasValue
                    ? $"No eligible Weixin process found for PID {pid.Value}."
                    : "No eligible running Weixin.exe process was found.";
                _logger.Error(message);
                throw new WechatNotOpenedException(message);
            }

            _logger.Info($"Target detail: pid={target.Pid} source={target.Source} rva=0x{target.Rva:x}");

            var result = Activate(target, checkOnly);
            result.Uia = null;

            if (verifyUia)
            {
                result.Uia = VerifyUia(target.Pid, Math.Max(uiaTimeoutSeconds, 0.1));
                string chatNameJson = result.Uia.ChatName ?? "";
                _logger.Info($"UIA: main_window={result.Uia.MainWindow} search_box={result.Uia.SearchBox} search_list={result.Uia.SearchList} chat_input={result.Uia.ChatInput} chat_name_json=\"{chatNameJson}\"");
                if (result.Uia.Activated)
                {
                    _logger.Info("UIA_VERIFIED: UI Tree  is materialized.");
                }
                else
                {
                    _logger.Warn("UIA_NOT_VERIFIED: byte write/readback succeeded, but the required mmui::MainWindow + 搜索 node was not observed.");
                }
            }

            return result;
        }

        #endregion

        #region 目标定位

        private Target FindTarget(int? targetPid)
        {
            List<int> pids = new List<int>();
            if (targetPid.HasValue)
            {
                pids.Add(targetPid.Value);
            }
            else
            {
                try
                {
                    foreach (var process in Process.GetProcessesByName(WeixinProcessName))
                    {
                        pids.Add(process.Id);
                    }
                }
                catch
                {
                    // 忽略进程枚举异常，后续按无候选处理。
                }
                pids.Sort();
            }

            foreach (int pid in pids)
            {
                string exePath = GetProcessImagePath(pid);
                if (string.IsNullOrEmpty(exePath))
                {
                    continue;
                }

                var dll = FindModule(pid, WeixinDllName);
                if (dll == null || !File.Exists(dll.Path))
                {
                    _logger.Debug($"PID {pid} skipped: no loaded Weixin.dll");
                    continue;
                }

                uint? rva = ScanQAccessibleActiveRva(dll.Path);
                string source = "scanner";
                if (!rva.HasValue)
                {
                    string version = Path.GetFileName(Path.GetDirectoryName(dll.Path));
                    if (QAccessibleActiveRvaByVersion.TryGetValue(version, out uint fallback))
                    {
                        rva = fallback;
                        source = "version fallback";
                    }
                }
                if (!rva.HasValue)
                {
                    _logger.Warn($"PID {pid} skipped: no verified gate RVA for {dll.Path}");
                    continue;
                }

                byte[] dllData;
                try
                {
                    dllData = File.ReadAllBytes(dll.Path);
                }
                catch
                {
                    _logger.Warn($"PID {pid} skipped: Weixin.dll could no longer be read");
                    continue;
                }

                var sections = ParsePeSections(dllData);
                var section = SectionForRva(sections, rva.Value);
                if (section == null || (section.Characteristics & IMAGE_SCN_MEM_WRITE) == 0)
                {
                    _logger.Warn($"PID {pid} skipped: RVA 0x{rva.Value:x} is not in a writable PE section");
                    continue;
                }

                return new Target
                {
                    Pid = pid,
                    ExePath = exePath,
                    DllPath = dll.Path,
                    DllBase = dll.Base,
                    Rva = rva.Value,
                    Source = source,
                };
            }

            return null;
        }

        private static string GetProcessImagePath(int pid)
        {
            IntPtr handle = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, (uint)pid);
            if (handle == IntPtr.Zero)
            {
                return null;
            }
            try
            {
                uint size = 32768;
                var sb = new StringBuilder((int)size);
                if (QueryFullProcessImageNameW(handle, 0, sb, ref size))
                {
                    return sb.ToString();
                }
                return null;
            }
            finally
            {
                CloseHandle(handle);
            }
        }

        private static ModuleInfo FindModule(int pid, string moduleName)
        {
            IntPtr snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE | TH32CS_SNAPMODULE32, (uint)pid);
            if (snapshot == new IntPtr(-1))
            {
                return null;
            }
            try
            {
                var entry = new MODULEENTRY32();
                entry.dwSize = (uint)Marshal.SizeOf<MODULEENTRY32>();
                if (!Module32FirstW(snapshot, ref entry))
                {
                    return null;
                }
                do
                {
                    if (string.Equals(entry.szModule, moduleName, StringComparison.OrdinalIgnoreCase))
                    {
                        return new ModuleInfo
                        {
                            Base = entry.modBaseAddr.ToInt64(),
                            Size = entry.modBaseSize,
                            Name = entry.szModule,
                            Path = entry.szExePath,
                        };
                    }
                } while (Module32NextW(snapshot, ref entry));
                return null;
            }
            finally
            {
                CloseHandle(snapshot);
            }
        }

        #endregion

        #region PE 解析与门控字节扫描

        private static List<PeSection> ParsePeSections(byte[] data)
        {
            var sections = new List<PeSection>();
            try
            {
                if (data == null || data.Length < 0x40)
                {
                    return sections;
                }

                int peOffset = BitConverter.ToInt32(data, 0x3C);
                if (peOffset < 0 || peOffset + 24 > data.Length)
                {
                    return sections;
                }
                // 校验 "PE\0\0" 签名。
                if (data[peOffset] != 0x50 || data[peOffset + 1] != 0x45 || data[peOffset + 2] != 0 || data[peOffset + 3] != 0)
                {
                    return sections;
                }

                int coff = peOffset + 4;
                ushort numberOfSections = BitConverter.ToUInt16(data, coff + 2);
                ushort optionalHeaderSize = BitConverter.ToUInt16(data, coff + 16);
                int sectionOffset = coff + 20 + optionalHeaderSize;

                for (int i = 0; i < numberOfSections; i++)
                {
                    int offset = sectionOffset + i * 40;
                    if (offset + 40 > data.Length)
                    {
                        break;
                    }

                    string name = Encoding.ASCII.GetString(data, offset, 8);
                    int nullIndex = name.IndexOf('\0');
                    if (nullIndex >= 0)
                    {
                        name = name.Substring(0, nullIndex);
                    }

                    sections.Add(new PeSection
                    {
                        Name = name,
                        Rva = BitConverter.ToUInt32(data, offset + 12),
                        VirtualSize = BitConverter.ToUInt32(data, offset + 8),
                        RawSize = BitConverter.ToUInt32(data, offset + 16),
                        RawPtr = BitConverter.ToUInt32(data, offset + 20),
                        Characteristics = BitConverter.ToUInt32(data, offset + 36),
                    });
                }
            }
            catch
            {
                // 解析失败按无节处理，等同 Python 的 try/except。
                sections.Clear();
            }
            return sections;
        }

        private static PeSection SectionForRva(List<PeSection> sections, uint rva)
        {
            foreach (var section in sections)
            {
                long size = Math.Max(section.VirtualSize, section.RawSize);
                if (rva >= section.Rva && rva < section.Rva + size)
                {
                    return section;
                }
            }
            return null;
        }

        private static uint? OffsetToRva(List<PeSection> sections, int offset)
        {
            foreach (var section in sections)
            {
                if (offset >= section.RawPtr && offset < section.RawPtr + section.RawSize)
                {
                    return section.Rva + (uint)(offset - section.RawPtr);
                }
            }
            return null;
        }

        /// <summary>
        /// 在可执行节中查找引用目标 RVA 的 RIP 相对 LEA 指令的 RVA。
        /// </summary>
        private static List<uint> RipXrefsToRva(byte[] data, List<PeSection> sections, uint targetRva)
        {
            var xrefs = new List<uint>();
            foreach (var section in sections)
            {
                if ((section.Characteristics & IMAGE_SCN_MEM_EXECUTE) == 0)
                {
                    continue;
                }

                int start = (int)section.RawPtr;
                int end = Math.Min(data.Length, start + (int)section.RawSize);
                for (int index = start; index + 6 < end; index++)
                {
                    uint instructionRva = section.Rva + (uint)(index - start);
                    byte b0 = data[index];

                    // 带 REX 前缀的 LEA（7 字节）：40-4F 8D modrm disp32
                    if (b0 >= 0x40 && b0 <= 0x4F && data[index + 1] == 0x8D && (data[index + 2] & 0xC7) == 0x05)
                    {
                        int disp = BitConverter.ToInt32(data, index + 3);
                        if ((long)instructionRva + 7 + disp == (long)targetRva)
                        {
                            xrefs.Add(instructionRva);
                        }
                    }

                    // 无 REX 前缀的 LEA（6 字节）：8D modrm disp32
                    if (data[index] == 0x8D && (data[index + 1] & 0xC7) == 0x05)
                    {
                        int disp = BitConverter.ToInt32(data, index + 2);
                        if ((long)instructionRva + 6 + disp == (long)targetRva)
                        {
                            xrefs.Add(instructionRva);
                        }
                    }
                }
            }
            return xrefs;
        }

        private static int IndexOf(byte[] data, byte[] pattern, int startIndex)
        {
            if (pattern == null || pattern.Length == 0 || startIndex < 0 || startIndex > data.Length - pattern.Length)
            {
                return -1;
            }
            for (int i = startIndex; i <= data.Length - pattern.Length; i++)
            {
                int j = 0;
                while (j < pattern.Length && data[i + j] == pattern[j])
                {
                    j++;
                }
                if (j == pattern.Length)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 找到门控字节所在的指令序列（test rcx,rcx; je; cmp byte[rip+disp],0; je）的所有起始偏移。
        /// </summary>
        private static List<int> FindGatePatternOffsets(byte[] data)
        {
            var matches = new List<int>();
            int start = 0;
            while (true)
            {
                int i = IndexOf(data, GatePrefix, start);
                if (i < 0)
                {
                    break;
                }
                start = i + 1;

                if (i + 18 > data.Length)
                {
                    continue;
                }
                // 0-4 为前缀(48 85 C9 0F 84)，5-8 通配，9=80, 10=3D, 11-14 为 disp32(通配)，15=00, 16=0F, 17=84。
                if (data[i + 9] == 0x80 && data[i + 10] == 0x3D &&
                    data[i + 15] == 0x00 && data[i + 16] == 0x0F && data[i + 17] == 0x84)
                {
                    matches.Add(i);
                }
            }
            return matches;
        }

        private static uint? ScanQAccessibleActiveRva(string dllPath)
        {
            byte[] data;
            try
            {
                data = File.ReadAllBytes(dllPath);
            }
            catch
            {
                return null;
            }

            var sections = ParsePeSections(data);
            if (sections.Count == 0)
            {
                return null;
            }

            // 1) 定位所有引用 qt.accessibility.core 字符串的 LEA 指令。
            var coreXrefs = new List<uint>();
            int coreStart = 0;
            while (true)
            {
                int offset = IndexOf(data, QAccessibleCoreString, coreStart);
                if (offset < 0)
                {
                    break;
                }
                coreStart = offset + 1;

                uint? coreRva = OffsetToRva(sections, offset);
                if (coreRva.HasValue)
                {
                    coreXrefs.AddRange(RipXrefsToRva(data, sections, coreRva.Value));
                }
            }
            if (coreXrefs.Count == 0)
            {
                return null;
            }

            // 2) 扫描门控字节的 cmp 指令，计算目标 RVA，并校验可执行/可写节与距离。
            var candidates = new List<(long Distance, uint Target)>();
            foreach (int matchOffset in FindGatePatternOffsets(data))
            {
                int dispOffset = matchOffset + 11;

                uint? matchRva = OffsetToRva(sections, matchOffset);
                uint? dispRva = OffsetToRva(sections, dispOffset);
                if (!matchRva.HasValue || !dispRva.HasValue)
                {
                    continue;
                }

                var codeSection = SectionForRva(sections, matchRva.Value);
                if (codeSection == null || (codeSection.Characteristics & IMAGE_SCN_MEM_EXECUTE) == 0)
                {
                    continue;
                }

                int displacement = BitConverter.ToInt32(data, dispOffset);
                long targetRvaLong = (long)dispRva.Value - 2 + 7 + displacement;
                if (targetRvaLong < 0 || targetRvaLong > uint.MaxValue)
                {
                    continue;
                }
                uint targetRva = (uint)targetRvaLong;

                var targetSection = SectionForRva(sections, targetRva);
                if (targetSection == null || (targetSection.Characteristics & IMAGE_SCN_MEM_WRITE) == 0)
                {
                    continue;
                }

                long distance = long.MaxValue;
                foreach (uint xref in coreXrefs)
                {
                    distance = Math.Min(distance, Math.Abs((long)matchRva.Value - (long)xref));
                }
                if (distance <= GateDistanceLimit)
                {
                    candidates.Add((distance, targetRva));
                }
            }

            if (candidates.Count == 0)
            {
                return null;
            }

            candidates.Sort((a, b) => a.Distance.CompareTo(b.Distance));
            // 拒绝歧义：存在第二个距离相同但目标不同的候选时，证据不足。
            if (candidates.Count > 1 && candidates[1].Distance == candidates[0].Distance && candidates[1].Target != candidates[0].Target)
            {
                return null;
            }
            return candidates[0].Target;
        }

        #endregion

        #region 读写内存

        private static bool ReadProcessByte(IntPtr handle, long address, out byte value)
        {
            var buffer = new byte[1];
            IntPtr bytesRead;
            if (!ReadProcessMemory(handle, new IntPtr(address), buffer, new IntPtr(1), out bytesRead) || bytesRead.ToInt64() != 1)
            {
                value = 0;
                return false;
            }
            value = buffer[0];
            return true;
        }

        private static bool WriteProcessByte(IntPtr handle, long address, byte value)
        {
            var buffer = new byte[] { value };
            IntPtr bytesWritten;
            return WriteProcessMemory(handle, new IntPtr(address), buffer, new IntPtr(1), out bytesWritten) && bytesWritten.ToInt64() == 1;
        }

        private ActivationResult Activate(Target target, bool checkOnly)
        {
            uint access = PROCESS_QUERY_LIMITED_INFORMATION | PROCESS_VM_READ | (checkOnly ? 0u : PROCESS_VM_WRITE);
            IntPtr handle = OpenProcess(access, false, (uint)target.Pid);
            if (handle == IntPtr.Zero)
            {
                string message = $"Cannot open PID {target.Pid} (Win32 error {Marshal.GetLastWin32Error()})";
                _logger.Error(message);
                throw new InvalidOperationException(message);
            }

            try
            {
                long address = target.DllBase + target.Rva;

                byte previous;
                if (!ReadProcessByte(handle, address, out previous))
                {
                    string message = $"ReadProcessMemory failed for PID {target.Pid} (Win32 error {Marshal.GetLastWin32Error()})";
                    _logger.Error(message);
                    throw new InvalidOperationException(message);
                }

                if (previous != 0 && previous != 1)
                {
                    string message = $"Refusing PID {target.Pid}: gate byte at Weixin.dll+0x{target.Rva:x} is {previous}, not 0/1";
                    _logger.Error(message);
                    throw new InvalidOperationException(message);
                }

                var result = new ActivationResult
                {
                    ProcessId = target.Pid,
                    ExePath = target.ExePath,
                    DllPath = target.DllPath,
                    DllBaseAddress = target.DllBase,
                    GateRva = target.Rva,
                    RvaSource = target.Source,
                    PreviousValue = previous,
                };

                if (checkOnly || previous == 1)
                {
                    result.Written = false;
                    result.ReadbackValue = previous;
                    result.State = previous == 1 ? "ALREADY_ACTIVE" : "CHECK_ONLY_INACTIVE";
                    _logger.Info(result.State);
                    return result;
                }

                if (!WriteProcessByte(handle, address, 1))
                {
                    string message = $"WriteProcessMemory failed for PID {target.Pid} (Win32 error {Marshal.GetLastWin32Error()})";
                    _logger.Error(message);
                    throw new InvalidOperationException(message);
                }

                byte readback;
                if (!ReadProcessByte(handle, address, out readback) || readback != 1)
                {
                    string message = $"Write verification failed for PID {target.Pid}: readback={readback}";
                    _logger.Error(message);
                    throw new InvalidOperationException(message);
                }

                result.Written = true;
                result.ReadbackValue = readback;
                result.State = "ACTIVATION_WRITTEN";
                _logger.Info(result.State);
                return result;
            }
            finally
            {
                CloseHandle(handle);
            }
        }

        #endregion

        #region UIA 验证

        /// <summary>
        /// 在独立的 STA 线程上探测 UIA，避免卡住的 provider 阻塞本工具（对应 Python 的守护线程探针）。
        /// </summary>
        private UiaVerification VerifyUia(int pid, double timeoutSeconds)
        {
            var done = new ManualResetEventSlim(false);
            UiaVerification result = null;

            var thread = new Thread(() =>
            {
                try
                {
                    using (var automation = new UIA3Automation())
                    {
                        result = ProbeUia(automation, pid);
                    }
                }
                catch (Exception ex)
                {
                    result = new UiaVerification { Detail = $"UIA probe failed: {ex.Message}" };
                }
                finally
                {
                    try { done.Set(); } catch (ObjectDisposedException) { }
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();

            double seconds = Math.Max(timeoutSeconds, 0.1);
            if (!done.Wait(TimeSpan.FromSeconds(seconds)))
            {
                done.Dispose();
                return new UiaVerification { Detail = $"UIA probe timed out after {seconds:g}s" };
            }

            done.Dispose();
            return result;
        }

        private UiaVerification ProbeUia(UIA3Automation automation, int pid)
        {
            var desktop = automation.GetDesktop();
            var window = desktop.FindFirstChild(cf => cf.ByClassName(MainWindowClassName).And(cf.ByProcessId(pid)));
            if (window == null)
            {
                return new UiaVerification { Detail = "mmui::MainWindow not found" };
            }

            var search = window.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByClassName(SearchBoxClassName)).And(cf.ByName(SearchBoxName)));
            var searchList = window.FindFirstDescendant(cf => cf.ByAutomationId(SearchListAutomationId));
            var chatInput = window.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId(ChatInputAutomationId)));

            return new UiaVerification
            {
                MainWindow = true,
                SearchBox = search != null,
                SearchList = searchList != null,
                ChatInput = chatInput != null,
                ChatName = chatInput?.Name ?? "",
                Detail = "mmui::MainWindow found",
            };
        }

        #endregion
    }
}
