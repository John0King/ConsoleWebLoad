using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleWebLoad
{
    public class CommandParser
    {
        private string[] args;
        public CommandParser(string[] args)
        {
            this.args = args;
        }
        public CommandResult Parser()
        {
            if(args!=null && args.Length != 0)
            {
                if(args.Length >= 2)
                {
                    return new CommandResult
                    {
                        Command = args[0],
                        args = args[1]
                    };
                }
                else if(args.Length == 1)
                {
                    return new CommandResult
                    {
                        Command = args[0],
                    };
                }
            }
            return new CommandResult()
            {
                Command = "Help"
            };
        }
    }

    public class CommandResult
    {
        public string Command { get; set; }
        public string args { get; set; }
        
    }

    internal class CommandExecutor
    {
        public static void ExcuteCommand(CommandResult result)
        {
            if (string.Equals(result.Command, "Help"))
            {
                ShowHelp();
            }
            else if (string.Equals(result.Command, "s"))
            {
                ExcuteSingle(result);
            }
            else if (string.Equals(result.Command, "l"))
            {
                ExcuteLoop(result);
            }
            else
            {
                ShowNotFound(result, NotFoundType.Command);
            }
        }
        
        static void ShowHelp()
        {
            string log = @"
ConsoleWebLoad [command] [config]

[command]:
    `s` : request once
    `l` : request loop
[config]: the config file location, defalut is current folder's LoadTestConfig.json
";
            Console.Write(log);
            Console.WriteLine();
        }
        static void ShowNotFound(CommandResult result ,NotFoundType type)
        {
            string log = "";
            if(type == NotFoundType.Command)
            {
                log = $"Command `{result.Command}` Not Found, type `help` to see more information.";
            }else
            {
                log = $"Config file `{result.args}` Not found for command `{result.Command}, type `help` to see more information.";
            }
            Console.WriteLine(log);
        }

        static void ExcuteSingle(CommandResult result)
        {
            string path;
            if (string.IsNullOrEmpty(result.args))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "LoadTestConfig.json");
            }
            else
            {
                path = result.args;
            }
            var Config = GetConfig(path);
            if(Config == null)
            {
                ShowNotFound(result, NotFoundType.Args);
            }
            var request = new Requset(Config.TestUrl, Config.TaskSize);
            request.Single();
        }

        static void ExcuteLoop(CommandResult result)
        {
            string path;
            if (string.IsNullOrEmpty(result.args))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "LoadTestConfig.json");
            }
            else
            {
                path = result.args;
            }
            var Config = GetConfig(path);
            if (Config == null)
            {
                ShowNotFound(result, NotFoundType.Args);
            }
            var request = new Requset(Config.TestUrl, Config.TaskSize);
            request.Loop();
        }
        static Config GetConfig(string path)
        {
            Config Config;
            try
            {
                var s = File.Open(path, FileMode.Open);
                string json = "";
                using (StreamReader r = new StreamReader(s))
                {
                    json = r.ReadToEnd();
                }
                Config = JsonConvert.DeserializeObject<Config>(json);
            }
            catch
            {
                Config = null;
            }
            
            return Config;
        }
        private enum NotFoundType
        {
            Command,
            Args
        }
    }

    public class Config
    {
        [JsonProperty("testUrl")]
        public string TestUrl { get; set; }
        [JsonProperty("taskSize")]
        public int TaskSize { get; set; }
    }
}
