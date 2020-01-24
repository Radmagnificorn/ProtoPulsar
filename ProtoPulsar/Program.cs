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
            turnManager.AddCombatant(new TestCombatant("Steve", 1));
            turnManager.AddCombatant(new TestCombatant("Burt", 2));
            turnManager.AddCombatant(new TestCombatant("Elvis", 3));

            var round = turnManager.CalculateRound(20);
            round.ForEach(comb => Console.WriteLine(comb.Id));

            while (!exit)
            {
                var command = parser.Parse(Console.ReadLine());

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
