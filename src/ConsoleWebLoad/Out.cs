using ConsoleWebLoad.Outputers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWebLoad
{
    public static class Out
    {
        public static IOutputer Outputer = new ConsoleOutputer();

        public static Action FinalAction;
    }
}
