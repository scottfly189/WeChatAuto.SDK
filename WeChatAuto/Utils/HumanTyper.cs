using System;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using FlaUI.Core.Input;

namespace WeChatAuto.Utils
{


    public class HumanTyper
    {
        private readonly Random _rand = new Random((int)DateTime.Now.Ticks);

        public int MinDelay { get; set; } = 40;   // 最快
        public int MaxDelay { get; set; } = 120;  // 最慢

        public int MistakeRate { get; set; } = 3; // 百分比（3%概率打错）
        public int PauseRate { get; set; } = 5;   // 停顿概率

        public async Task TypeAsync(string text, CancellationToken token = default)
        {
            foreach (var c in text)
            {
                token.ThrowIfCancellationRequested();

                // ===== 1. 随机停顿（像思考）=====
                if (_rand.Next(100) < PauseRate)
                {
                    await Task.Delay(_rand.Next(300, 800), token);
                }

                // ===== 2. 是否打错 =====
                if (_rand.Next(100) < MistakeRate && char.IsLetterOrDigit(c))
                {
                    char wrongChar = GetRandomChar();

                    Keyboard.Type(wrongChar.ToString());

                    await Task.Delay(_rand.Next(50, 150), token);

                    // 回删
                    Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.BACK);
                    Keyboard.Release(FlaUI.Core.WindowsAPI.VirtualKeyShort.BACK);

                    await Task.Delay(_rand.Next(50, 150), token);
                }

                // ===== 3. 正常输入 =====
                Keyboard.Type(c.ToString());

                // ===== 4. 根据字符调整速度 =====
                int delay = GetDelayForChar(c);

                await Task.Delay(delay, token);
            }
        }

        private int GetDelayForChar(char c)
        {
            int baseDelay = _rand.Next(MinDelay, MaxDelay);

            // 标点慢一点
            if (char.IsPunctuation(c))
                baseDelay += 50;

            // 空格稍慢
            if (c == ' ')
                baseDelay += 30;

            return baseDelay;
        }

        private char GetRandomChar()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return chars[_rand.Next(chars.Length)];
        }
    }

}