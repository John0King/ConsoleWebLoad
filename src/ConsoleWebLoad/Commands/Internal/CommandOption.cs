using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad.Commands.Internal
{
    public class CommandOption : IOption
    {
        public CommandOption()
        {
            this.Tags = new List<string>();
        }
        public string OptionName { get; set; }

        public ICollection<string> Tags { get;  }
    }
}
