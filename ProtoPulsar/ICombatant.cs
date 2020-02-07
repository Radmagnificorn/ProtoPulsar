using System;
using System.Collections.Generic;
using System.Text;

namespace ProtoPulsar
{
    interface ICombatant
    {
        public string Id { get; }
        public int Speed { get; }

        public bool Active { get; set; }

        public List<BattleOption> BattleOptions { get; }

    }
}
