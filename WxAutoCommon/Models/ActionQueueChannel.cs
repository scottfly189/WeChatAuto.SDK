using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace WxAutoCommon.Models
{
    /// <summary>
    /// 动作队列通道
    /// </summary>
    /// <typeparam name="T">动作类型</typeparam>
    public class ActionQueueChannel<T>
    {
        private readonly Channel<T> _channel = Channel.CreateBounded<T>(new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true, // 单个读取器
            SingleWriter = false // 多个写入器
        });
        /// <summary>
        /// 写入动作
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public bool WriteAsync(T item, CancellationToken cancellationToken = default)
        {
            return _channel.Writer.TryWrite(item);
        }
        /// <summary>
        /// 读取动作
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask<T> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await _channel.Reader.ReadAsync(cancellationToken);
        }
        /// <summary>
        /// 测试读取
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default)
        {
            return _channel.Reader.WaitToReadAsync(cancellationToken);
        }
    }
}