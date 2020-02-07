using System;
using System.Collections.Generic;
using System.Text;

namespace ProtoPulsar
{

    class BattleOptionResponse
    {
        // UI will separate options into menus with .
        // E.G. Magic.Fire
        public string Id { get; }
        public string[] SelectableTargetIds { get; }
        public string DefaultTarget { get; }
        public BattleOptionResponse(string id, string[] selectableTargets, string defaultTarget)
        {
            Id = id;
            SelectableTargetIds = selectableTargets;
            DefaultTarget = defaultTarget;
        }

        public override string ToString()
        {
            string targets = string.Join(", ", SelectableTargetIds);
            return $"{Id} -> {targets}";
        }
    }

    
}
