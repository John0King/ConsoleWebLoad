using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;

using ConsoleWebLoad.CommandParser;

namespace ConsoleWebLoad
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            Out.Outputer.StartOutput();
            var CmdResult = CommandParser.CommandParser.Parse(args);
            CommandExecutor.Execute(CmdResult);

            await Out.Outputer.WaitAsync();
            if(Out.FinalAction != null)
            {
                Out.FinalAction?.Invoke();
            }
            //var app = new CommandLineApplication();
            //app.Command("run", capp =>
            //{
            //    capp.
            //})

        }

    }
}
