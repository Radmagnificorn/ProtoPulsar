using System;
using System.Collections.Generic;
using System.Text;

namespace ProtoPulsar
{
    class BattleOption
    {
        public string Id { get; }
        public TargetGroup PotentialTargets { get; }

        public TargetGroup DefaultTarget { get; }

        public BattleOption(string id, TargetGroup targets, TargetGroup defaultTarget)
        {
            Id = id;
            PotentialTargets = targets;
            DefaultTarget = defaultTarget;
        }
    }
}
