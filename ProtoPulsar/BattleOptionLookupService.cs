using System;
using System.Collections.Generic;
using System.Text;

namespace ProtoPulsar
{
    class BattleOptionLookupService
    {
        private Dictionary<string, BattleOption> _battleOptions = new Dictionary<string, BattleOption>();

        public BattleOption GetBattleOptionById(string battleOptionId)
        {
            return _battleOptions[battleOptionId];
        }

        public void AddOption(BattleOption option)
        {
            _battleOptions.Add(option.Id, option);
        }

        public void BulkAddOptions(params (string, TargetGroup, TargetGroup)[] bopts)
        {
            Array.ForEach(bopts, (opt) => AddOption(new BattleOption(opt.Item1, opt.Item2, opt.Item3)));
        }
    }
}
