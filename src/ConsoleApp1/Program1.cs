using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        /// 异步方法
        /// Task<T>：调用方法要从调用中获取一个 T 类型的值，异步方法的返回类型就必须是Task<T>。
        /// 调用方法从 Task 的 Result 属性获取的就是 T 类型的值。
        /// Task：调用方法不需要从异步方法中取返回值，但是希望检查异步方法的状态，那么可以选择可以返回 Task 类型的对象。
        /// 不过，就算异步方法中包含 return 语句，也不会返回任何东西。
        /// void：调用方法执行异步方法，但又不需要做进一步的交互。 
        /// 异步方法的结构
        ///（1）表达式之前的部分：从方法头到第一个 await 表达式之间的所有代码。
        ///（2）await 表达式：将被异步执行的代码。
        ///（3）表达式之后的部分：await 表达式的后续部分。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Task<int> 异步
            //Task<int> t = Calculator.AddAsync(1, 2);
            //Console.WriteLine($"result: {t.Result}");

            //Task 异步
            //Task t = Calculator.AddAsync(1, 2);
            //t.Wait();
            //Console.WriteLine("AddAsync 方法执行完成");

            //void：调用方法执行异步方法
            //Calculator.AddAsync(1, 2);
            //Thread.Sleep(1000); //挂起1秒钟
            //Console.WriteLine("AddAsync 方法执行完成");

            var t = Do.GetGuidAsync();
            t.Wait();

            Console.Read();

        }
    }
    internal class Calculator
    {
        private static int Add(int n, int m)
        {
            return n + m;
        }

        //public static async Task<int> AddAsync(int n, int m)
        //{
        //    int val = await Task.Run(() => Add(n, m));

        //    return val;
        //}
        //public static async Task AddAsync(int n, int m)
        //{
        //    int val = await Task.Run(() => Add(n, m));
        //    Console.WriteLine($"Result: {val}");
        //}
        public static async void AddAsync(int n, int m)
        {
            int val = await Task.Run(() => Add(n, m));
            Console.WriteLine($"Result: {val}");
        }
        
    }
    internal class Do
    {
        /// <summary>
        /// 获取 Guid
        /// </summary>
        /// <returns></returns>
        private static Guid GetGuid()   //与Func<Guid> 兼容
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// 异步获取 Guid
        /// </summary>
        /// <returns></returns>
        public static async Task GetGuidAsync()
        {
            var myFunc = new Func<Guid>(GetGuid);
            var t1 = await Task.Run(myFunc);//Func<Task>

            var t2 = await Task.Run(new Func<Guid>(GetGuid));////Action

            var t3 = await Task.Run(() => GetGuid());//Func<Task>

            var t4 = await Task.Run(() => Guid.NewGuid());//Func<TResult>

            Console.WriteLine($"t1: {t1}");
            Console.WriteLine($"t2: {t2}");
            Console.WriteLine($"t3: {t3}");
            Console.WriteLine($"t4: {t4}");
        }
    }
}
