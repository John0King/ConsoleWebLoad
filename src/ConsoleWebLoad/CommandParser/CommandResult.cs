using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad.CommandParser
{
    public class CommandResult
    {
        public CommandResult()
        {
            options = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }
        public string CommnadName { get; internal set; }
        public Dictionary<string,string> options { get; internal set; }
    }
}
