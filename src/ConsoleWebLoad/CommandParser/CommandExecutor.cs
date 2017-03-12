using ConsoleWebLoad.Commands.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using ConsoleWebLoad.Commands;

namespace ConsoleWebLoad.CommandParser
{
    public class CommandExecutor
    {
        private CommandResult _result;
        public CommandExecutor(CommandResult command)
        {
            _result = command;
            FindCommands();
        }

        public void Excute()
        {
            var cmd = SelectCommand();
            var Realoption = BuildRealOptions(cmd);
            cmd.Execute(Realoption);
        }
        public static List<ICommand> commands = new List<ICommand>();

        private ICommand SelectCommand()
        {
            foreach (var c in commands)
            {
                if (string.Equals(_result.CommnadName, c.CommandName, StringComparison.OrdinalIgnoreCase))
                {
                    return c;
                }
            }
            return new HelpCommand();
        }

        private void FindCommands()
        {
            var EntryAssembly = Assembly.GetEntryAssembly();
            var Types = EntryAssembly.ExportedTypes;
            foreach (var t in Types)
            {
                var info = t.GetTypeInfo();
                var mt = info.GetInterface(typeof(ICommand).FullName);
                if (mt != null)
                {
                    commands.Add(Activator.CreateInstance(t) as ICommand);
                }
            }
        }

        private Dictionary<string, string> BuildRealOptions(ICommand cmd)
        {
            var R = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var o in cmd.Options)
            {
                foreach (var t in o.Tags)
                {
                    string val = null;
                    bool found = false;
                    foreach (var _r in _result.options)
                    {
                        if (string.Equals(_r.Key, t))
                        {
                            val = _r.Value;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        if(val.EndsWith("\"")&&val.StartsWith("\""))
                        {
                            val = val.Trim('"');
                        }
                        else if(val.EndsWith("'") && val.StartsWith("'"))
                        {
                            val = val.Trim('\'');
                        }
                        R[o.OptionName] = val;
                        break;
                    }
                }
            }
            return R;
        }

        #region 静态实用函数

        public static void Execute(CommandResult command)
        {
            var executor = new CommandExecutor(command);
            executor.Excute();
        }
        #endregion
    }
}
