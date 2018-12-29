using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program2
    {
        /// <summary>
        /// 调用 CancellationTokenSource 对象的 Cancel 方法，
        /// 并不会执行取消操作，而是会将该对象的 CancellationToken 属性 IsCancellationRequested 设置为 true。
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            var t = Do.ExecuteAsync(token);

            Thread.Sleep(3000);   //挂起 3 秒
            source.Cancel();    //传达取消请求

            t.Wait(token);  //等待任务执行完成
            Console.WriteLine($"{nameof(token.IsCancellationRequested)}: {token.IsCancellationRequested}");

            Console.Read();
        }


    }

    internal class Do
    {
        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task ExecuteAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            await Task.Run(() => CircleOutput(token), token);
        }

        /// <summary>
        /// 循环输出
        /// </summary>
        /// <param name="token"></param>
        private static void CircleOutput(CancellationToken token)
        {
            Console.WriteLine($"{nameof(CircleOutput)} 方法开始调用：");

            const int num = 5;
            for (var i = 0; i < num; i++)
            {
                if (token.IsCancellationRequested)  //监控 CancellationToken
                {
                    return;
                }

                Console.WriteLine($"{i + 1}/{num} 完成");
                Thread.Sleep(1000);
            }
        }
    }

}
