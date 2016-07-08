using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.Http;

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
        //HttpClient client = new HttpClient();

        private async Task<long> DoRequest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                //await client.GetAsync(_url);
                HttpWebRequest wr = HttpWebRequest.CreateHttp(_url);
                var t = await wr.GetResponseAsync();
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
            var tasks = new Task[TaskSize];
            for (int i = 0; i < TaskSize; i++)
            {
                tasks[i] = Single(i);
            }
            Task.WaitAll(tasks);
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

        }

        public void l()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<Task> tasks;
            int queueSize = 100;
            int index = 0;
            for (int i = 0; i < TaskSize / queueSize; i++)
            {
                tasks = new List<Task>();
                for (int j = 0; j < queueSize; j++)
                {
                    tasks.Add(Single(index));
                    index++;
                }
                Task.WaitAll(tasks.ToArray());
            }

            tasks = new List<Task>();
            for (int j = 0; j < TaskSize % queueSize; j++)
            {
                tasks.Add(Single(index));
                index++;
            }

            Task.WaitAll(tasks.ToArray());

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
        }

        private async Task Single(int index)
        {
            var time = await DoRequest();
            Console.WriteLine($"Taks {index.ToString().PadRight(4)}\tCost { time }ms");
        }
    }
}
