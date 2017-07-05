using ConsoleWebLoad.CommandParser;
using ConsoleWebLoad.Commands.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using ConsoleWebLoad.Config;
using ConsoleWebLoad.LoadRunner;

namespace ConsoleWebLoad.Commands
{
    public class LoopCommand : ICommand
    {
        public LoopCommand()
        {
            Options = new List<IOption>();
            var o = new CommandOption();
            o.OptionName = "ConfigFile";
            o.Tags.Add("-c");
            o.Tags.Add("--configfile");
            Options.Add(o);
           
        }
        public string CommandName { get; } = "Loop";
        public IList<IOption> Options{ get; }

        private string ConfigFilePath = Path.Combine( Directory.GetCurrentDirectory(),"LoadConfig.json") ;
        public bool Execute(Dictionary<string,string> options)
        {
            if (options.ContainsKey("ConfigFile"))
            {
                string v = options["ConfigFile"];
                if (Path.IsPathRooted(v))
                {
                    ConfigFilePath = v;
                }else
                {
                    ConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), v);
                }

            }

            var config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText(ConfigFilePath));

            var looper = new LoopRunner(config.TaskSize,config.TestCount,config.TestUrls);
            looper.Run();

            return true;
        }
    }
}
