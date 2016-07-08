using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleWebLoad
{
    public class Requset
    {

        public Requset(string url,int taskSize)
        {
            this._url = url;
            if(taskSize > 1)
            {
                this.TaskSize = taskSize;
            }
            TimeCosts = new Queue<long>(TaskSize);
        }
        private string _url;
        private int TaskSize = 10000;
        private Queue<long> TimeCosts ;

        private long DoRequest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                HttpWebRequest wr = HttpWebRequest.CreateHttp(_url);
                wr.GetResponseAsync().GetAwaiter().GetResult();//同步访问
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
            var r = Parallel.For(1, TaskSize, i => { Single(i); });
            sw.Stop();
            Console.WriteLine($"Task Queue excuted in {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"avg page time: \t{ sw.ElapsedMilliseconds / TaskSize }ms");
            if(TimeCosts.Count == 0)
            {
                Console.WriteLine($"tasks seems not work");
            }
            else
            {
                Console.WriteLine($"peer request avg page time:\t{ TimeCosts.Sum() / TimeCosts.Count }ms");
            }
            
        }
        /// <summary>
        /// 单次访问
        /// </summary>
        /// <param name="index">任务索引</param>
        public void Single(int index = 0)
        {
            var time = DoRequest();
            this.TimeCosts.Dequeue();// 从 列表中删除
            Console.WriteLine($"Taks {index.ToString().PadRight(4)}\tCost { time }ms");
        }
    }
}
