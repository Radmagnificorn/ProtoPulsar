using System;

namespace ProtoPulsar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("do something");

            Boolean exit = false;

            var parser = new CommandParser(new BattleService());

            while (!exit)
            {
                Console.WriteLine(parser.Parse(Console.ReadLine()));
            }
            
        }
    }
}
