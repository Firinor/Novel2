namespace Puzzle.BossBattle
{
    public class HeroStatsOperator : StatsOperator
    {
        public void SetStats(BattleStats stats)
        {
            this.stats = stats;
            SetHP(stats.Health);
            SetStatsToText();
        }
    }
}
