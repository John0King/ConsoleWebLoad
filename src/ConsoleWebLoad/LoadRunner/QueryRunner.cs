using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWebLoad.LoadRunner
{
    public class QueryRunner
    {
        public string _url { get;private set; }
        public QueryRunner(string Url)
        {
            _url = Url;
        }
        private static HttpClient client = new HttpClient();
        private object locker = new object();

        public async Task<QueryResult> Run()
        {
            int index = Interlocked.Increment(ref Counter.QueryCounter);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var isSuccess = await DoRequest();
            timer.Stop();
            lock (locker)
            {
                string txt = isSuccess ? "Success" : "Faild";
                Console.WriteLine($" Query<{index}>\t{txt}\t\tTimeCost:{timer.ElapsedMilliseconds}ms\t\turl:{_url}");
            }
            
            return  new QueryResult()
            {
                Success = isSuccess,
                Timeuse = timer.Elapsed
            };
        }
        private async Task<bool> DoRequest()
        {
            try
            {
                await client.GetStringAsync(_url);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class QueryResult
    {
        public bool Success { get; set; } = false;
        public TimeSpan Timeuse { get; set; }
    }
}
