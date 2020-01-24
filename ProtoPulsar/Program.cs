using System;

namespace ProtoPulsar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("do something");

            Boolean exit = false;

            var parser = new CommandParser();

            var turnManager = new TurnManager();
            // Make test combatants
            TurnManager
            
            while (!exit)
            {
                Console.WriteLine(parser.Parse(Console.ReadLine()));
            }
            
        }
    }

    class TestCombatant : ICombatant
    {
        public string Id { get; }
        public int Speed { get; }

        public TestCombatant(string id, int speed)
        {
            Id = id;
            Speed = speed;
        }
    }
}
