using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace WxAutoCommon.Models
{
    /// <summary>
    /// 动作队列通道
    /// </summary>
    /// <typeparam name="T">动作类型</typeparam>
    public class ActionQueueChannel<T> : IDisposable where T : ChatActionMessage
    {
        private readonly Channel<T> _channel = Channel.CreateBounded<T>(new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true, // 单个读取器,因为考虑到是UI操作，所以不能多个读取器同时读取
            SingleWriter = false // 多个写入器
        });
        /// <summary>
        /// 写入动作
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask WriteAsync(T item, CancellationToken cancellationToken = default)
        {
            await _channel.Writer.WriteAsync(item, cancellationToken);
        }
        /// <summary>
        /// 写入动作并等待
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<object> WriteAndWaitAsync(T item, CancellationToken cancellationToken = default)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);
            item.Tcs = tcs;
            await _channel.Writer.WriteAsync(item, cancellationToken);
            return await tcs.Task;
        }
        /// <summary>
        /// 读取动作
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public async ValueTask<T> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await _channel.Reader.ReadAsync(cancellationToken);
        }
        /// <summary>
        /// 测试读取
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default)
        {
            return _channel.Reader.WaitToReadAsync(cancellationToken);
        }

        public void Dispose()
        {
            _channel.Writer.Complete();
        }
    }
}