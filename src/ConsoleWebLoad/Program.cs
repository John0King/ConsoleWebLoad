﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var R = new CommandParser(args).Parser();
            CommandExecutor.ExcuteCommand(R);
        }

    }
}
