using ConsoleWebLoad.CommandParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad.Commands.Internal
{
    public interface ICommand
    {
        string CommandName { get; }
        IList<IOption> Options { get;  }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="options">key 是 option 名字， value 是 option 的值</param>
        /// <returns></returns>
        bool Execute(Dictionary<string,string> options);
    }
}
