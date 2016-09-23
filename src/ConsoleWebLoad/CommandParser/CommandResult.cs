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
            options = new List<OptionResult>();
        }
        public string CommnadName { get; internal set; }
        public IEnumerable<OptionResult> options { get; internal set; }
    }
}
