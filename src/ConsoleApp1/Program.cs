using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    #region 异常处理
    //internal class Program
    //{
    //    private static void Main(string[] args)
    //    {
    //        var t = DoExceptionAsync();
    //        t.Wait();

    //        Console.WriteLine($"{nameof(t.Status)}: {t.Status}");   //任务状态
    //        Console.WriteLine($"{nameof(t.IsCompleted)}: {t.IsCompleted}");     //任务完成状态标识
    //        Console.WriteLine($"{nameof(t.IsFaulted)}: {t.IsFaulted}");     //任务是否有未处理的异常标识

    //        Console.Read();
    //    }

    //    /// <summary>
    //    /// 异常操作
    //    /// </summary>
    //    /// <returns></returns>
    //    private static async Task DoExceptionAsync()
    //    {
    //        try
    //        {
    //            await Task.Run(() => { throw new Exception(); });
    //        }
    //        catch (Exception)
    //        {
    //            Console.WriteLine($"{nameof(DoExceptionAsync)} 出现异常！");
    //        }
    //    }
    //}
    #endregion

    #region 调用方法中同步等待任务
    //internal class Program
    //{
    //    private static void Main(string[] args)
    //    {
    //        var t = CountCharactersAsync("http://www.cnblogs.com/liqingwen/");

    //        t.Wait();   //等待任务结束
    //        Console.WriteLine($"Result is {t.Result}");

    //        Console.Read();
    //    }

    //    /// <summary>
    //    /// 统计字符数量
    //    /// </summary>
    //    /// <param name="address"></param>
    //    /// <returns></returns>
    //    private static async Task<int> CountCharactersAsync(string address)
    //    {
    //        var result = await Task.Run(() => new WebClient().DownloadStringTaskAsync(address));
    //        return result.Length;
    //    }
    //}
    #endregion

    #region 在调用方法中同步等待任务
    ////Wait() 适合用于单一 Task 对象，如果想操作一组对象，可采用 Task 的两个静态方法 WaitAll() 和 WaitAny() 。
    //internal class Program
    //{
    //    private static int time = 0;
    //    //private static void Main(string[] args)
    //    //{
    //    //    var t1 = GetRandomAsync(1);
    //    //    var t2 = GetRandomAsync(2);

    //    //    //IsCompleted 任务完成标识
    //    //    Console.WriteLine($"t1.{nameof(t1.IsCompleted)}: {t1.IsCompleted}");
    //    //    Console.WriteLine($"t2.{nameof(t2.IsCompleted)}: {t2.IsCompleted}");

    //    //    Console.Read();
    //    //}
    //    //private static void Main(string[] args)
    //    //{
    //    //    var t1 = GetRandomAsync(1);
    //    //    var t2 = GetRandomAsync(2);

    //    //    Task<int>[] tasks = new Task<int>[] { t1, t2 };
    //    //    Task.WaitAll(tasks);    //等待任务全部完成，才继续执行

    //    //    //IsCompleted 任务完成标识
    //    //    Console.WriteLine($"t1.{nameof(t1.IsCompleted)}: {t1.IsCompleted}");
    //    //    Console.WriteLine($"t2.{nameof(t2.IsCompleted)}: {t2.IsCompleted}");

    //    //    Console.Read();
    //    //}
    //    private static void Main(string[] args)
    //    {
    //        var t1 = GetRandomAsync(1);
    //        var t2 = GetRandomAsync(2);

    //        Task<int>[] tasks = new Task<int>[] { t1, t2 };
    //        Task.WaitAny(tasks);    //等待任一 Task 完成，才继续执行

    //        //IsCompleted 任务完成标识
    //        Console.WriteLine($"t1.{nameof(t1.IsCompleted)}: {t1.IsCompleted}");
    //        Console.WriteLine($"t2.{nameof(t2.IsCompleted)}: {t2.IsCompleted}");

    //        Console.Read();
    //    }
    //    /// <summary>
    //    /// 获取一个随机数
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    private static async Task<int> GetRandomAsync(int id)
    //    {
    //        var num = await Task.Run(() =>
    //        {
    //            time++;
    //            Thread.Sleep(time * 100);
    //            return new Random().Next();
    //        });

    //        Console.WriteLine($"{id} 已经调用完成");
    //        return num;
    //    }
    //}
    #endregion

    #region 在异步方法中异步等待任务
    //WaitAll() 和 WaitAny() 同步地等待 Task 完成。这次我们使用 Task.WhenAll() 和 Task.WhenAny() 
    //WhenAll() 异步等待集合内的 Task 都完成，不会占用主线程的时间。
    //internal class Program
    //{
    //    private static int time = 0;

    //    private static void Main(string[] args)
    //    {
    //        var t = GetRandomAsync();

    //        Console.WriteLine($"t.{nameof(t.IsCompleted)}: {t.IsCompleted}");
    //        Console.WriteLine($"Result: {t.Result}");

    //        Console.Read();
    //    }

    //    /// <summary>
    //    /// 获取一个随机数
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    private static async Task<int> GetRandomAsync()
    //    {
    //        time++;
    //        var t1 = Task.Run(() =>
    //        {
    //            Thread.Sleep(time * 100);
    //            return new Random().Next();
    //        });

    //        time++;
    //        var t2 = Task.Run(() =>
    //        {
    //            Thread.Sleep(time * 500);
    //            return new Random().Next();
    //        });

    //        //异步等待集合内的 Task 都完成，才进行下一步操作
    //        //await Task.WhenAll(new List<Task<int>>() { t1, t2 });
    //        await Task.WhenAny(new List<Task<int>>() { t1, t2 });

    //        Console.WriteLine($"    t1.{nameof(t1.IsCompleted)}: {t1.IsCompleted}");
    //        Console.WriteLine($"    t2.{nameof(t2.IsCompleted)}: {t2.IsCompleted}");

    //        return t1.Result + t2.Result;
    //    }
    //}
    #endregion

    #region Task.Delay() 暂停执行
    //Task.Delay()和 Thread.Sleep 不同的是，它不会阻塞线程，意味着线程可以继续处理其它工作
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"{nameof(Main)} - start.");
            DoAsync();
            Console.WriteLine($"{nameof(Main)} - end.");

            Console.Read();
        }

        private static async void DoAsync()
        {
            Console.WriteLine($"    {nameof(DoAsync)} - start.");

            await Task.Delay(500);

            Console.WriteLine($"    {nameof(DoAsync)} - end.");
        }
    }
    #endregion
}
