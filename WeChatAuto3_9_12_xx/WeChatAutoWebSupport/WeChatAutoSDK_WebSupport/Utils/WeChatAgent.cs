using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using WeChatAutoSDK_WebSupport.Models;

namespace WeChatAutoSDK_WebSupport.Utils
{
    public static class WeChatAgent
    {
        private static Task? channelTask;
        private static Channel<AutomationAction> _channel = Channel.CreateBounded<AutomationAction>(new BoundedChannelOptions(1000)
        {
            SingleReader = true,
            SingleWriter = false,
            FullMode = BoundedChannelFullMode.Wait,
        });

        public static async Task WriteAsync(AutomationAction action,CancellationToken token)
        {
            await _channel.Writer.WriteAsync(action,token);
        }
        /// <summary>
        /// 注册一个消费WeChatAgent消息的Action
        /// </summary>
        /// <param name="action">读取到数据后执行的Function</param>
        /// <param name="endCallBack">如果调用者需要返回，则实现此函数</param>
        /// <param name="token">取消令牌</param>
        /// <returns></returns>
        public static async Task RegisterConsumeAction(Func<AutomationAction, CancellationToken, Task<AutomationResult>> action,Action<AutomationResult> endCallBack, CancellationToken token)
        {
            if (channelTask != null)
                return;
            channelTask = Task.Run(async () =>
            {
                while (await _channel.Reader.WaitToReadAsync(token))
                {
                    while (_channel.Reader.TryRead(out var item))
                    {
                        try
                        {
                            var result = await action(item, token);
                            endCallBack(result);
                        }
                        catch (Exception ex)
                        {
                            LogsHelper.LogError(ex);
                        }
                    }
                }
            }, token);
        }

        public static void Dispose()
        {
            _channel.Writer.Complete();
        }
    }
}
