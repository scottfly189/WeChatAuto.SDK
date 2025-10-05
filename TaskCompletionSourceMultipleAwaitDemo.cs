using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCompletionSourceDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== TaskCompletionSource 对比示例 ===\n");

            // 示例1：不使用 RunContinuationsAsynchronously
            await DemoWithoutRunContinuationsAsynchronously();
            
            Console.WriteLine("\n" + new string('=', 50) + "\n");
            
            // 示例2：使用 RunContinuationsAsynchronously
            await DemoWithRunContinuationsAsynchronously();
            
            Console.WriteLine("\n" + new string('=', 50) + "\n");
            
            // 示例3：UI线程影响
            await DemonstrateUIThreadImpact();
        }

        static async Task DemoWithoutRunContinuationsAsynchronously()
        {
            Console.WriteLine("1. 不使用 RunContinuationsAsynchronously:");
            
            var tcs = new TaskCompletionSource<string>();
            
            // 启动一个任务来设置结果
            var setterTask = Task.Run(() =>
            {
                Console.WriteLine($"  设置结果的线程ID: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(100); // 模拟一些工作
                tcs.SetResult("任务完成");
            });

            // 等待任务完成并处理结果
            var result = await tcs.Task.ContinueWith(t =>
            {
                Console.WriteLine($"  处理结果的线程ID: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"  结果: {t.Result}");
                return t.Result;
            });

            await setterTask;
        }

        static async Task DemoWithRunContinuationsAsynchronously()
        {
            Console.WriteLine("2. 使用 RunContinuationsAsynchronously:");
            
            var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
            
            // 启动一个任务来设置结果
            var setterTask = Task.Run(() =>
            {
                Console.WriteLine($"  设置结果的线程ID: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(100); // 模拟一些工作
                tcs.SetResult("任务完成");
            });

            // 等待任务完成并处理结果
            var result = await tcs.Task.ContinueWith(t =>
            {
                Console.WriteLine($"  处理结果的线程ID: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"  结果: {t.Result}");
                return t.Result;
            });

            await setterTask;
        }

        static async Task DemonstrateUIThreadImpact()
        {
            Console.WriteLine("3. UI线程影响示例:");
            
            // 模拟UI线程场景
            var uiThreadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"  主线程ID: {uiThreadId}");

            // 不使用 RunContinuationsAsynchronously
            var tcs1 = new TaskCompletionSource<string>();
            var task1 = tcs1.Task.ContinueWith(t =>
            {
                Console.WriteLine($"  不使用RunContinuationsAsynchronously - 执行线程ID: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"  是否在主线程: {Thread.CurrentThread.ManagedThreadId == uiThreadId}");
            });

            // 使用 RunContinuationsAsynchronously
            var tcs2 = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
            var task2 = tcs2.Task.ContinueWith(t =>
            {
                Console.WriteLine($"  使用RunContinuationsAsynchronously - 执行线程ID: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"  是否在主线程: {Thread.CurrentThread.ManagedThreadId == uiThreadId}");
            });

            // 在主线程上设置结果
            tcs1.SetResult("结果1");
            tcs2.SetResult("结果2");

            await Task.WhenAll(task1, task2);
        }
    }
}