using ConsoleWebLoad.CommandParser;
using ConsoleWebLoad.Commands.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad.Commands
{
    public class RunCommand : ICommand
    {
        public RunCommand()
        {
            Options = new List<IOption>();
            var o = new CommandOption();
            o.OptionName = "ConfigFile";
            o.Tags.Add("-c");
            o.Tags.Add("--configfile");
            Options.Add(o);
           
        }
        public string CommandName { get; } = "Run";
        public IList<IOption> Options{ get; }

        public bool Execute(IEnumerable<OptionResult> options)
        {
            return false;
        }
    }
}
