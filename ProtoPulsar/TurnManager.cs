using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace ProtoPulsar
{
    class TurnManager
    {
        private List<SpeedPointedCombatant> _combatants;
        private SpeedPointedCombatant _turnHolder;
        public ICombatant TurnHolder
        {
            get
            {
                return _turnHolder.Target;
            }
        }


        public TurnManager(List<ICombatant> combatantList = null)
        {
            if (combatantList == null)
            {
                _combatants = new List<SpeedPointedCombatant>();
            }
            else
            {
                _combatants = combatantList.ConvertAll(c => new SpeedPointedCombatant(c));
            }

        }

        public void AddCombatant(ICombatant combatant)
        {
            _combatants.Add(new SpeedPointedCombatant(combatant));
        }

        private SpeedPointedCombatant SelectNext(List<SpeedPointedCombatant> combatants)
        {
            if (_combatants.Count < 1) return null;
            combatants.ForEach(combatant => combatant.IncrementPoints());
            var onDeck = combatants.OrderByDescending(c => c.Points).First();
            onDeck.ResetPoints();
            return onDeck;
        }

        public ICombatant AdvanceTurn()
        {
            _turnHolder = SelectNext(_combatants);
            return _turnHolder.Target;
        }

        public List<ICombatant> PredictTurnOrder(int numberOfTurns, List<SpeedPointedCombatant> combatants = null)
        {
            if (combatants == null)
            {
                combatants = _combatants;
            }

            var turnOrder = new List<ICombatant>();
            var mockCombatants = combatants.ConvertAll(c => new SpeedPointedCombatant(c));

            for (int turn = 0; turn < numberOfTurns; turn++)
            {
                turnOrder.Add(SelectNext(mockCombatants).Target);
            }

            return turnOrder;
        }



        
        
    }

    class SpeedPointedCombatant
    {
        public ICombatant Target { get; }
        public int Points { get; private set;}
        public SpeedPointedCombatant(ICombatant target, int points = 0)
        {
            Target = target;
            Points = points;
        }

        public SpeedPointedCombatant(SpeedPointedCombatant orderedCombatant)
        {
            Target = orderedCombatant.Target;
            Points = orderedCombatant.Points;
        }

        public void IncrementPoints()
        {
            Points += Target.Speed;
        }

        public void ResetPoints()
        {
            Points = 0;
        }
    }

}
