using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad.Config
{
    public class ConfigModel
    {
        public string[] TestUrls { get; set; }
        public int TaskSize { get; set; }
        public int TestCount { get; set; }
    }
}
