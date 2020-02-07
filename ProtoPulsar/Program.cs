using System;

namespace ProtoPulsar
{
    class Program
    {
        static void Main(string[] args)
        {
            var battleService = new BattleService();

            var parser = new CommandParser(battleService);

            parser.Run();
            
        }
    }
}
