using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad.CommandParser
{
    /*
     * ConsoleWebLoad run -c LoadConfig.json
     * ConsoleWebLoad loop -c loadConfig.json 
     */

    public class CommandParser
    {
        private string[] _args;
        public CommandParser(string[] args)
        {
            this._args = args;
        }

        public IEnumerable<OptionResult> Parse()
        {
            throw new NotImplementedException();
        }

        #region 静态实用函数

        public static IEnumerable<OptionResult> Parse(string[] ags)
        {
            var parser = new CommandParser(ags);
            return parser.Parse();
        }
        #endregion
    }
}
