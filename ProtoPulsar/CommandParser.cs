using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProtoPulsar
{

    class CommandParser
    {
        BattleService _battleService;
        private bool _running = true;

        public CommandParser(BattleService battleService)
        {
            _battleService = battleService;
        }

        public void Run() {
            while (_running)
            {
                Console.Out.Write("Please enter a command: ");
                Console.WriteLine(Parse(Console.ReadLine()));
            }
        }

        public string Parse(string command)
        {
            var parsedCommand = command.Split(" ");

            return parsedCommand[0] switch
            {
                "exit" => exit(),
                "say" => (parsedCommand.Length > 1) ? saySomething(parsedCommand[1]) : "cat got your tongue?",
                "next" => $"It is now {_battleService.AdvanceTurn()}'s turn",
                "predict" => string.Join("\n", _battleService.PredictTurns(int.Parse(parsedCommand[1]))),
                "who" => $"It's {_battleService.GetTurnHolder()}'s turn",
                "actions" => string.Join("\n", _battleService
                    .GetAvailableActions()
                    .ConvertAll(o => o.ToString())
                    .ToArray()),
                "do" => performAction(parsedCommand[1]),
                _ => "I don't recognize that command"
            };

        }

        private string performAction(string action)
        {
            string retString = "";
            var actions = _battleService.GetAvailableActions();
            var selectedAction = actions.FindLast(a => a.Id.Contains(action));
            if (selectedAction != null)
            {
                Console.WriteLine("Please select a target:");
                Console.WriteLine(string.Join(", ", selectedAction.SelectableTargetIds));
                var target = Console.ReadLine();
                retString = _battleService.PerformAction(new ActionRequest(selectedAction.Id, target)).Message;
            } else
            {
                retString = "That is not a valid command";
            }
            Console.WriteLine("");
            return retString;
        }

        private string exit()
        {
            _running = false;
            return "bye";
        }

        private string saySomething(string something)
        {
            return $"the player said {something}";
        }
    }
}
