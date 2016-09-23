using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad.Commands.Internal
{
    public interface IOption
    {
        string OptionName { get; }
        ICollection<string> Tags { get; }
    }
}
