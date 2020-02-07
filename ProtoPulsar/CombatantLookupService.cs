using System;
using System.Collections.Generic;
using System.Text;

namespace ProtoPulsar
{

    class CombatantLookupService
    {

        private Dictionary<string, ICombatant> _combatants = new Dictionary<string, ICombatant>();

        public void AddCombatant(ICombatant combatant)
        {
            _combatants.Add(combatant.Id, combatant);
        }

        public ICombatant GetCombatantById(string id)
        {
            return _combatants[id];
        }

        public List<ICombatant> GetCombatantsByIds(params string[] ids)
        {
            return (new List<string>(ids)).ConvertAll(id => GetCombatantById(id));
        }

        public List<ICombatant> GetCombatantsByIds(List<string> ids)
        {
            return ids.ConvertAll(id => GetCombatantById(id));
        }
    }
}
