using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoPulsar
{    
    class BattlefieldManager
    {
        public Battlefield Battlefield { get; }

        private List<BattlePositionData> _battleData = new List<BattlePositionData>();
        private TurnManager _turnManager;

        public BattlefieldManager(TurnManager turnManager)
        {
            Battlefield = new Battlefield();
            _turnManager = turnManager;
        }

        public void AddCombatant(ICombatant combatant, BattleTeam team, bool frontRow = true)
        {
            _battleData.Add(new BattlePositionData(combatant, team, frontRow));
            _turnManager.AddCombatant(combatant);
        }

        public BattlePositionData GetBattleDataForCombatant(string combatantId)
        {
            return _battleData.Where(bd => bd.Combatant.Id == combatantId).First();
        }

        public List<BattlePositionData> GetMembersOfTeam(BattleTeam team)
        {
            return _battleData.Where(bd => bd.Team == team).ToList();
        }

        public List<BattlePositionData> GetActiveCombatants()
        {
            return _battleData.Where(bd => bd.Combatant.Active).ToList();
        }
    }

    class BattlePositionData
    {
        public ICombatant Combatant { get; set; }
        
        public BattleTeam Team { get; set; }
        public bool FrontRow { get; set; }

        public BattlePositionData(ICombatant combatant, BattleTeam team, bool frontRow = true)
        {
            Combatant = combatant;
            Team = team;
            FrontRow = frontRow;
        }


    }

    class Battlefield
    {
        public TeamLayout PlayerTeam { get; }
        public TeamLayout EnemyTeam { get; }

        public Battlefield()
        {
            PlayerTeam = new TeamLayout();
            EnemyTeam = new TeamLayout();
        }

    }

    class TeamLayout
    {
        public List<ICombatant> FrontRow { get; }
        public List<ICombatant> BackRow { get; }

        public TeamLayout()
        {
            FrontRow = new List<ICombatant>();
            BackRow = new List<ICombatant>();
        }
    }
}
