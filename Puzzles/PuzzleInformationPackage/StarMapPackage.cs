using System;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle
{
    [Serializable]
    public class StarMapPackage : InformationPackage
    {
        public StarMapPackage(float allottedTime, int difficulty,
            Sprite puzzleBackground, UnityAction successPuzzleDialog = null, UnityAction failedPuzzleDialog = null)
            : base(puzzleBackground, successPuzzleDialog, failedPuzzleDialog)
        {
            this.allottedTime = Math.Max(allottedTime, 0);
            this.difficulty = difficulty;
        }

        [SerializeField]
        [Range(0, 1024)]
        private float allottedTime = 0;

        public float AllottedTime { get => allottedTime; }

        [SerializeField]
        [Range(0, 2)]
        private int difficulty = 0;

        public int Difficulty { get => difficulty; }
    }
}

