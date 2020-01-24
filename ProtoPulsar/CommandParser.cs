using System;
using System.Collections.Generic;
using System.Text;

namespace ProtoPulsar
{
    class CommandParser
    {
        public string Parse(string command)
        {
            var parsedCommand = command.Split(" ");

            return parsedCommand[0] switch
            {
                "exit" => "there is no escape",
                "say" => (parsedCommand.Length > 1) ? saySomething(parsedCommand[1]) : "cat got your tongue?",
                _ => "I don't recognize that command"
            };

        }

        private string saySomething(string something)
        {
            return $"the player said {something}";
        }
    }
}
