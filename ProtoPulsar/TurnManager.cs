using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoPulsar
{
    class TurnManager
    {
        private List<ICombatant> _combatants;


        private List<ICombatant> _currentRound;

        public TurnManager(List<ICombatant> combatants = null) 
        {
            if (combatants == null)
            {
                _combatants = new List<ICombatant>();
            } 
            else
            {
                _combatants = combatants;
            }
        }

        public List<ICombatant> CalculateRound(int requestedTurns)
        {
            // TODO: base iterations on slowest combatant to save cycles
            var turnList = new List<Turn>();
            _combatants.ForEach(combatant =>
            {
                int turnSpeed = 0;
                for (int turn = 0; turn < requestedTurns; turn++)
                {
                    turnSpeed += combatant.Speed;
                    turnList.Add(new Turn(combatant, turnSpeed));
                }
            });
            turnList.OrderBy(turn => turn.Speed);

            return turnList.OrderBy(turn => turn.Speed)
                .Take(requestedTurns)
                .Select(turn => turn.Target).ToList();
        }

        public void AddCombatant(ICombatant combatant)
        {
            _combatants.Add(combatant);
        }
        
    }

    class Turn
    {
        public ICombatant Target { get; }
        public int Speed { get; }
        public Turn(ICombatant target, int speed)
        {
            Target = target;
            Speed = speed;
        }
    }
}
