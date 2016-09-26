using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWebLoad.LoadRunner
{
    public class TestRunner
    {
        private string[] _urls;
        public TestRunner(string[] urls)
        {
            _urls = urls;
        }
        private object locker = new object();
        public async Task<TestResult> Run()
        {
            int index = Interlocked.Increment(ref Counter.TestCounter);
            var r = new TestResult();
            foreach(var url in _urls)
            {
                var q = new QueryRunner(url);
                var QResult = await q.Run();
                r.Timeuse += QResult.Timeuse;
                if (QResult.Success)
                {
                    r.SuccessCount += 1;
                }
                else
                {
                    r.FaildCount += 1;
                }
            }
            Counter.TestResults.Enqueue(r);
            lock (locker)
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" Test<{index}>");
                Console.ResetColor();
                Console.WriteLine($"\tSuccess:{r.SuccessCount}\tFaild:{r.FaildCount}\t\tTimeCost:{r.Timeuse.TotalMilliseconds}ms");
            }
            
            return r;
        }

        
    }

    public class TestResult
    {
        public int FaildCount { get; set; }
        public int SuccessCount { get; set; }
        public TimeSpan Timeuse { get; set; }
    }
}
