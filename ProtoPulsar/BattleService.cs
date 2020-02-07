using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace ProtoPulsar
{
    class BattleService
    {
        TurnManager _turnManager;
        BattleOptionLookupService _boLookup;
        CombatantLookupService _combatantLookup;
        BattlefieldManager _battlefieldManager;
        public BattleService()
        {
            _turnManager = new TurnManager();
            _boLookup = new BattleOptionLookupService();
            _combatantLookup = new CombatantLookupService();
            _battlefieldManager = new BattlefieldManager(_turnManager);

            _boLookup.BulkAddOptions(
                ("Attack", TargetGroup.Single, TargetGroup.Opponent),
                ("Defend", TargetGroup.Self, TargetGroup.Self),
                ("Magic|Fire", TargetGroup.Single, TargetGroup.Opponent),
                ("Magic|Mega Fire", TargetGroup.Team, TargetGroup.Opponent),
                ("Magic|Lightning", TargetGroup.Single, TargetGroup.Opponent),
                ("Items|Potion", TargetGroup.Single, TargetGroup.Self),
                ("Items|Hi-Potion", TargetGroup.Single, TargetGroup.Self),
                ("Items|Revive", TargetGroup.Single, TargetGroup.Down)
            );

            var steve = new TestCombatant("Steve", 18);
            steve.AddBattleOptions(_boLookup, new List<string>()
            {
                "Attack", "Defend", "Magic|Fire", "Magic|Mega Fire", "Items|Potion"
            });
            _combatantLookup.AddCombatant(steve);

            var burt = new TestCombatant("Burt", 12);
            burt.AddBattleOptions(_boLookup, new List<string>()
            {
                "Attack", "Defend", "Magic|Lightning", "Items|Hi-Potion"
            });
            _combatantLookup.AddCombatant(burt);

            var elvis = new TestCombatant("Elvis", 10);
            steve.AddBattleOptions(_boLookup, new List<string>()
            {
                "Attack", "Defend", "Items|Potion"
            });
            _combatantLookup.AddCombatant(elvis);

            _battlefieldManager.AddCombatant(steve, BattleTeam.Player);
            _battlefieldManager.AddCombatant(burt, BattleTeam.Enemy);
            _battlefieldManager.AddCombatant(elvis, BattleTeam.Enemy);

        }

        public string[] PredictTurns(int turns)
        {
            return _turnManager.PredictTurnOrder(turns).ConvertAll(c => c.Id).ToArray();
        }

        public string GetTurnHolder()
        {
            return _turnManager.TurnHolder.Id;
        }

        public string AdvanceTurn()
        {
            return _turnManager.AdvanceTurn().Id;
        }

        public ActionResponse PerformAction(ActionRequest action)
        {
            var response = new ActionResponse();

            //verify action is valid
            var validOptionIds = _turnManager.TurnHolder.BattleOptions.ConvertAll(option => option.Id);

            if (validOptionIds.Contains(action.OptionId))
            {
                response.Message = $"{GetTurnHolder()} performed {action.ActionShortName} on {action.TargetId}";
                response.Message += $"\n{AdvanceTurn()} is up";
            } else
            {
                response.Message = $"{action.ActionShortName} on {action.TargetId} is not a valid action for {GetTurnHolder()}";
            }

            
            return response;
        }

        public List<BattleOptionResponse> GetAvailableActions()
        {
            return _turnManager.TurnHolder.BattleOptions.ConvertAll(opt => mapBattleOptionToResponse(opt));
        }

        private BattleOptionResponse mapBattleOptionToResponse(BattleOption battleOption)
        {
            List<string> targets = new List<string>();
            string defaultTarget = "";

            // map potential targets
            if (battleOption.PotentialTargets == TargetGroup.Self)
            {
                targets.Add(_turnManager.TurnHolder.Id);
            }
            if (battleOption.PotentialTargets == TargetGroup.Single)
            {
                targets.AddRange(_battlefieldManager.GetActiveCombatants().ConvertAll(c => c.Combatant.Id));
            }
            if (battleOption.PotentialTargets == TargetGroup.Team)
            {
                targets.Add("TEAM_ALL");
            }

            // map default target
            if (battleOption.DefaultTarget == TargetGroup.Self)
            {
                defaultTarget = _turnManager.TurnHolder.Id;
            }
            if (battleOption.DefaultTarget == TargetGroup.Opponent)
            {
                defaultTarget = _battlefieldManager.GetMembersOfTeam(BattleTeam.Enemy)[0].Combatant.Id;
            }

            return new BattleOptionResponse(battleOption.Id, targets.ToArray(), defaultTarget);
        }

    }

    class ActionRequest
    {
        public string OptionId;
        public string TargetId;
        public string ActionShortName
        {
            get
            {
                var parsedActionId = OptionId.Split(".");
                return parsedActionId[parsedActionId.Length - 1];
            }
        }

        public ActionRequest(string actionId, string targetId)
        {
            OptionId = actionId;
            TargetId = targetId;
        }
    }

    class ActionResponse
    {
        public string Message;
    }

    class TestCombatant : ICombatant
    {
        public string Id { get; }
        public int Speed { get; }

        public bool Active { get; set; }

        public List<BattleOption> BattleOptions { get; }

        public TestCombatant(string id, int speed, List<BattleOption> battleOptions = null, bool active = true)
        {
            Id = id;
            Speed = speed;
            BattleOptions = battleOptions == null ? new List<BattleOption>() : battleOptions;
            Active = active;
        }

        public void AddBattleOptions(BattleOptionLookupService boLookup, List<string> battleOptionIds)
        {
            BattleOptions.AddRange(battleOptionIds.ConvertAll(boid => boLookup.GetBattleOptionById(boid)));
        }
    }

    public enum TargetGroup
    {
        Self,
        Single,
        Team,
        Opponent,
        Down
    }

    enum BattleTeam
    {
        Player,
        Enemy
    }

}
