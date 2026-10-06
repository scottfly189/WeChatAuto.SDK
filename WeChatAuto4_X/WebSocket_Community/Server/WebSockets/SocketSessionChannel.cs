using System.Threading.Channels;
using Newtonsoft.Json;
using WeAutoCommon.Enums;

/// <summary>
/// socket管道.
/// </summary>
public class SocketSessionChannel : IDisposable
{
    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    private int socketSessionStarted = 0;
    private readonly ILogger<SocketSessionChannel> logger;
    private volatile bool _disposed = false;
    private Task? _MonitorTask;
    private readonly IServiceProvider provider;
    private Channel<MessagePackageWrapper> channel = Channel.CreateBounded<MessagePackageWrapper>(new BoundedChannelOptions(100)
    {
        SingleReader = true,
        SingleWriter = false,
        FullMode = BoundedChannelFullMode.Wait,
    });

    public SocketSessionChannel(ILogger<SocketSessionChannel> logger, IServiceProvider provider)
    {
        this.logger = logger;
        this.provider = provider;
    }

    public async Task ConsumptionMessage()
    {
        if (Interlocked.CompareExchange(ref socketSessionStarted, 1, 0) == 1)
            return;
        TaskCompletionSource tcs = new TaskCompletionSource();
        _MonitorTask = Task.Run(async () =>
        {
            tcs.SetResult();
            try
            {
                await foreach (var message in this.channel.Reader.ReadAllAsync(cts.Token))
                {
                    var handler = provider.GetRequiredService<MessageHandler>();
                    try
                    {
                        var result = await handler.HandleAsync(message, cts.Token);  //处理请求，并且返回结果
                        await message!.handler!.SendAsync(result);
                    }
                    catch (OperationCanceledException)
                    {
                        // do nothing.
                    }
                    catch (WeChatAutoWebSocketServerExcpetion ex)
                    {
                        RequestData businessError = new RequestData()
                        {
                            Type = "error",
                            RequestId = ex.Request_Id,
                            Data = ex.ToString(),
                        };
                        await message!.handler!.SendAsync(businessError);
                    }
                    catch (Exception ex)
                    {
                        RequestData businessError = new RequestData()
                        {
                            Type = "error",
                            RequestId = message.RequestId,
                            Data = ex.ToString(),
                        };
                        await message!.handler!.SendAsync(businessError);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // do nothing.
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
            }
        });
        await tcs.Task;
    }

    public async Task AddWxMessage(MessagePackageWrapper data)
    {
        await channel.Writer.WriteAsync(data);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~SocketSessionChannel()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;
        _disposed = true;
        if (disposing)
        {
            channel.Writer.Complete();
            cts.Cancel();
            if (_MonitorTask != null)
            {
                if (_MonitorTask.Status == TaskStatus.Running)
                {
                    _MonitorTask.Wait(TimeSpan.FromSeconds(3));
                }
            }
        }
    }
}