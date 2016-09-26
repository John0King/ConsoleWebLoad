using ConsoleWebLoad.CommandParser;
using ConsoleWebLoad.Commands.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad.Commands
{
    public class HelpCommand : ICommand
    {
        public HelpCommand()
        {
            Options = new List<IOption>();
            var o = new CommandOption();
            o.OptionName = "Help";
            o.Tags.Add("-h");
            o.Tags.Add("--help");
            Options.Add(o);
           
        }
        public string CommandName { get; } = "Help";
        public IList<IOption> Options{ get; }

        public bool Execute(Dictionary<string,string> options)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Console Web Load");
            Console.ResetColor();

            string content = @"
 Help :
 ConsoleWebLoad [command] [option]
 
 [command]:
     Run         Run your test for once.
     Loop        Run your test for the number that you configurated.
     help        show this menu.

 [options]
    -c --configfile set the config file location

";
            Console.Write(content);
            Console.WriteLine();
            return true;
        }
    }
}
