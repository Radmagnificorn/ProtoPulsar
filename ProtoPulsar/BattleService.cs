using System;
using System.Collections.Generic;
using System.Text;

namespace ProtoPulsar
{
    class BattleService
    {
        TurnManager _turnManager;
        public BattleService()
        {
            _turnManager = new TurnManager();

            // Make test combatants
            _turnManager.AddCombatant(new TestCombatant("Steve", 18));
            _turnManager.AddCombatant(new TestCombatant("Burt", 12));
            _turnManager.AddCombatant(new TestCombatant("Elvis", 10));
        }

        public List<string> PredictTurns(int turns)
        {
            return _turnManager.PredictTurnOrder(turns).ConvertAll(c => c.Id);
        }

        public string GetActiveCombatant()
        {
            return _turnManager.ActiveCombatant.Id;
        }

        public string AdvanceTurn()
        {
            return _turnManager.AdvanceTurn().Id;
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
