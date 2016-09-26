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
            if (args == null)
            {
                this._args = new string[0];
            }
            else
            {
                this._args = args;
            }
            
        }

        public CommandResult Parse()
        {
            if (_args.Length == 0)
            {
                var r = new CommandResult();
                r.CommnadName = "help";
                return r;
            }
            var r2 = new CommandResult();
            r2.CommnadName = _args[0];
            var optionArr = _args.Skip(1).ToArray();

            ReadOption(r2.options, optionArr);

            return r2;
        }

        private void ReadOption(Dictionary<string,string> dic, string[] args)
        {
            if (args == null||args.Length==0)
            {
                return;
            }
            var c = new OptionCreator(dic);
            for(var i = 0; i < args.Length; i++)
            {
                var op = args[i];
                if (op.StartsWith("-") || op.StartsWith("/"))
                {
                    c.AddKey(op);
                }
                else
                {
                    c.AddValue(op);
                }
            }
            c.Flush();
        }

        private class OptionCreator
        {
            private  Dictionary<string, string> _dic ;
            public OptionCreator(Dictionary<string, string> dic)
            {
                _dic = dic;
            }
            public void AddKey(string k)
            {
                if (LastIsKey)
                {
                    _dic[lastKey]= "";
                    
                }
                
                lastKey = k;
                LastIsKey = true;
                
            }
            public void AddValue(string v)
            {
                if (LastIsKey)
                {
                    _dic[lastKey]= v;
                }
                else
                {
                    _dic[lastKey] = _dic[lastKey] + " " + v;
                }
                LastIsKey = false;
            }
            private bool LastIsKey = false;
            private string lastKey = "";

            public void Flush()
            {
                if (LastIsKey)
                {
                    _dic[lastKey] = "";
                }
            }

        }

        #region 静态实用函数

        public static CommandResult Parse(string[] ags)
        {
            var parser = new CommandParser(ags);
            return parser.Parse();
        }
        #endregion
    }
}
