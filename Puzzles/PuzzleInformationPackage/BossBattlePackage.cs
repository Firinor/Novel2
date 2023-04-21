using System;
using UnityEngine.Events;
using UnityEngine;
using FirUnityEditor;
using Puzzle.BossBattle;

namespace Puzzle
{
    [Serializable]
    public class BossBattlePackage : InformationPackage
    {
        [SerializeField, NullCheck]
        private CharacterInformator boss;
        [SerializeField]
        private BattleStats[] bossStats;
        [SerializeField]
        private BattleStats heroStats;

        public CharacterInformator BossCharacter { get => boss; }
        public BattleStats[] Boss { get => bossStats; }
        public BattleStats Hero { get => heroStats; }


        public BossBattlePackage(CharacterInformator boss,
            BattleStats[] bossStats, BattleStats heroStats,
            Sprite puzzleBackground, UnityAction successPuzzleDialog = null, UnityAction failedPuzzleDialog = null)
            : base(puzzleBackground, successPuzzleDialog, failedPuzzleDialog)
        {
            this.boss = boss;
            this.bossStats = bossStats;
            this.heroStats = heroStats;
        }
    }
}
