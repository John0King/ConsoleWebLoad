using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ConsoleWebLoad.LoadRunner
{
    public static class Counter
    {
        public static volatile int QueryCounter = 0;
        public static volatile int TestCounter = 0;

        public static ConcurrentQueue<TestResult> TestResults = new ConcurrentQueue<TestResult>();
    }
}
