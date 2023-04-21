using System;

namespace Puzzle.BossBattle
{
    [Serializable]
    public struct BattleStats : IComparable<BattleStats>
    {
        public int Health;
        public int Defence;
        public int TimeToUpEnergy;
        public int TimeToDownEnergy;
        public float TimeToSkill;
        public int AttackCount;

        public int CompareTo(BattleStats other)
        {
            return Health.CompareTo(other.Health);
        }
    }
}
