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
        public string _url { get; }
        private readonly object locker = new object();
        private readonly HttpClient _client;
        public QueryRunner(string Url,HttpClient client)
        {
            _url = Url;
            _client = client;
        }
        

        public async ValueTask<QueryResult> Run()
        {
            int index = Interlocked.Increment(ref Counter.QueryCounter);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var isSuccess = await DoRequest().ConfigureAwait(false);
            timer.Stop();
            //lock (locker)
            //{
            string txt = isSuccess ? "Success" : "Faild";
                //Console.WriteLine($" Query<{index}>\t{txt}\t\tTimeCost:{timer.ElapsedMilliseconds}ms\t\turl:{_url}");
            Out.Outputer.AddMessage(new Outputers.OutputMessage(Outputers.MessageLeve.Info,
                $" Query<{index}> ",
                $"\t{txt}\t\tTimeCost:{timer.ElapsedMilliseconds}ms\t\turl:{_url}"));
            //}

            return  new QueryResult()
            {
                Success = isSuccess,
                Timeuse = timer.Elapsed
            };
        }
        private async ValueTask<bool> DoRequest()
        {
            try
            {
                await _client.GetStringAsync(_url).ConfigureAwait(false);
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
