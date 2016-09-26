using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ConsoleWebLoad.LoadRunner
{
    public class LoopRunner
    {
        private int _taskSize;
        private int _testCount;
        private string[] _TestUrls;

        /// <summary>
        /// 初始化 循环访问器
        /// </summary>
        /// <param name="TaskSize">用户数量（线程数）</param>
        /// <param name="TestSize">任务（测试）数量</param>
        public LoopRunner(int TaskSize,int TestCount, string[] TestUrls)
        {
            _taskSize = TaskSize;
            _testCount = TestCount;
            _TestUrls = TestUrls;
        }

        public async Task Run()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            await RunTest();
            timer.Stop();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Result:>>>>>>>>>>>");
            Console.ResetColor();
            Console.WriteLine($"All Test use Time:{timer.Elapsed.TotalMilliseconds.ToString("f3")}ms");
            Console.WriteLine($"Test Count:{Counter.TestCounter} times\t\tQuery Count:{Counter.QueryCounter} times");
            Console.WriteLine($"Peer Page time:{ (timer.Elapsed.TotalMilliseconds / Counter.QueryCounter).ToString("f3") }ms");
            Console.WriteLine($"Peer Test time:{(timer.Elapsed.TotalMilliseconds / Counter.TestCounter).ToString("f3")}ms");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Result:<<<<<<<<<<<");
            Console.ResetColor();
            var totalMS = Counter.TestResults.Sum(t => t.Timeuse.TotalMilliseconds);
            var totalCount = Counter.TestResults.Sum(t => t.FaildCount + t.SuccessCount);
            Console.WriteLine($"All Test use Time:{totalMS.ToString("f3")}ms");
            Console.WriteLine($"Peer Page time:{ (totalMS / totalCount).ToString("f3") }ms");
            Console.WriteLine($"Peer Test time:{(totalMS / totalCount).ToString("f3")}ms");

            Console.Write($"Error:{Counter.TestResults.Sum(t => t.FaildCount)}");

        }
        private TestResult reslut = new TestResult();
        private async Task RunTest()
        {
            await Task.FromResult(0);
            List<Task> tasks = new List<Task>();
            for(var i = 0; i < _taskSize; i++)
            {
                tasks.Add(InvokeTest());
                
            }
            Task.WaitAll(tasks.ToArray());
        }

        private async Task InvokeTest()
        {
            while (true)
            {
                if (Counter.TestCounter > _testCount)
                {
                    break;
                }
                var test = new TestRunner(_TestUrls);
                var t = await test.Run();
            }
            
        }
    }
}
