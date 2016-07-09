using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;

namespace ConsoleWebLoad
{
    public class Requset
    {

        public Requset(string url, int taskSize)
        {
            this._url = url;
            if (taskSize > 1)
            {
                this.TaskSize = taskSize;
            }

            TimeCosts = new ConcurrentQueue<long>();
        }
        private string _url;
        private int TaskSize = 10000;
        private ConcurrentQueue<long> TimeCosts;
        private HttpClient client = new HttpClient();
        private volatile int TaskIndwx = 0;
        private async Task<long> DoRequest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                await client.GetAsync(_url);
            }
            catch
            {

            }
            sw.Stop();
            TimeCosts.Enqueue(sw.ElapsedMilliseconds);
            return sw.ElapsedMilliseconds;
        }
        /// <summary>
        /// 并行访问 <see cref="TaskSize"/>次
        /// </summary>
        public void Loop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<Task> tasks = new List<Task>();
            int queueSize = 30;
            int index = 0;
            for (int j = 0; j < queueSize; j++)
            {
                tasks.Add(SingleMuti());
                index++;
            }

            Task.WaitAll(tasks.ToArray());
            sw.Stop();
            Console.WriteLine($"Task Queue excuted in {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"avg page time: \t{ sw.ElapsedMilliseconds / TaskSize }ms");
            if (TimeCosts.Count == 0)
            {
                Console.WriteLine($"tasks seems not work");
            }
            else
            {
                Console.WriteLine($"peer request avg page time:\t{ TimeCosts.Sum() } / {TimeCosts.Count } = {  ((double)TimeCosts.Sum()) / ((double)TimeCosts.Count) }ms");
            }
            Console.WriteLine($"url:\t{_url}");
        }
        /// <summary>
        /// 单次访问
        /// </summary>
        /// <param name="index">任务索引</param>
        public async Task Single()
        {
            var time = await DoRequest();
            long item;
            TimeCosts.TryDequeue(out item);
            Console.WriteLine($"Taks (Single)\tCost { time }ms");
            Console.WriteLine($"url:\t{_url}");
        }

        private async Task SingleMuti()
        {

            do
            {
                int index = Interlocked.Increment(ref TaskIndwx);
                if (index > TaskSize)
                {
                    break;
                }
                var time = await DoRequest();
                Console.WriteLine($"Taks {index.ToString().PadRight(4)}\tCost { time }ms");
            } while (true);
        }
    }
}
