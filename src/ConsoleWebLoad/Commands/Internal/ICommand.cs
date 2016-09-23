using ConsoleWebLoad.CommandParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad.Commands.Internal
{
    interface ICommand
    {
        string CommandName { get; }
        IList<IOption> Options { get;  }
        bool Execute(IEnumerable<OptionResult> options);
    }
}
