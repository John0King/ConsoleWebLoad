using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWebLoad.Outputers
{
    public class OutputMessage
    {
        public OutputMessage(string categoryMessage) : this(MessageLeve.Error,categoryMessage,string.Empty)
        {
        }
        public OutputMessage(string category,string message):this(MessageLeve.Info,category,message)
        {
        }

        public OutputMessage(MessageLeve leve,string category,string message)
        {
            Leve = leve;
            Category = category;
            Message = message;
        }

        public string Category { get;  }
        public string Message { get; }

        public MessageLeve Leve { get; }
    }

    public enum MessageLeve
    {
        Info,
        Warning,
        Error
    }
}
