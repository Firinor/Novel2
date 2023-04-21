using Puzzle.FindDifferences;
using System.Collections.Generic;

namespace Puzzle.BossBattle
{
    public class BossStatsOperator : StatsOperator
    {
        public void SetStats(BattleStats[] battleStages)
        {
            if (battleStages.Length == 0)
                return;

            statsStages = new List<BattleStats>(battleStages);
            statsStages.Sort();
            SetNewStatsStage();
            SetHP(stats.Health);
        }

        private void SetNewStatsStage()
        {
            stats = statsStages[statsStages.Count - 1];
            statsStages.Remove(stats);
            SetStatsToText();
        }

        public override void Cooldown(bool boost)
        {
            if (skillBase.enabled)
                ActivateSkill();

            base.Cooldown(boost);
        }

        public override bool Damage()
        {
            bool result = base.Damage();

            if (statsStages.Count > 0 && slider.value <= statsStages[statsStages.Count - 1].Health)
            {
                SetNewStatsStage();
            }

            return result;
        }
    }
}
