using ConsoleWebLoad.Commands.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad.CommandParser
{
    public class CommandExecutor
    {
        private IEnumerable<OptionResult> _options;
        public CommandExecutor(IEnumerable<OptionResult> options)
        {
            _options = options;
        }

        public void Excute()
        {
            var cmd = SelectCommand();
            cmd.Execute(_options);
        }

        private ICommand SelectCommand()
        {
            throw new NotImplementedException();
        }

        #region 静态实用函数

        public static void Execute(IEnumerable<OptionResult> options)
        {
            var executor = new CommandExecutor(options);
            executor.Excute();
        }
        #endregion
    }
}
