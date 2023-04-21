using System;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle
{
    [Serializable]
    public class SpectralAnalysisPackage : InformationPackage
    {
        public SpectralAnalysisPackage(bool colorShift, int resipeCount, int ingredientsCount, float allottedTime,
            Sprite puzzleBackground, UnityAction successPuzzleDialog = null, UnityAction failedPuzzleDialog = null)
            : base(puzzleBackground, successPuzzleDialog, failedPuzzleDialog)
        {
            if (ingredientsCount < 2)
            {
                ingredientsCount = 2;
            }
            if (resipeCount > ingredientsCount)
            {
                resipeCount = ingredientsCount;
            }
            if (resipeCount < 1)
            {
                resipeCount = 1;
            }

            this.colorShift = colorShift;
            this.resipeCount = resipeCount;
            this.ingredientsCount = ingredientsCount;
            this.allottedTime = Math.Max(allottedTime, 0);
        }
        [SerializeField]
        private bool colorShift = true;
        [SerializeField]
        [Range(1, 10)]
        private int resipeCount = 1;
        [SerializeField]
        [Range(10, 50)]
        private int ingredientsCount = 2;
        [SerializeField]
        [Range(0, 1024)]
        private float allottedTime = 0;

        public bool ColorShift { get => colorShift; }
        public int RecipeCount { get => resipeCount; }
        public int IngredientsCount { get => Math.Min(Enum.GetNames(typeof(Atom)).Length, ingredientsCount); }
        public float AllottedTime { get => allottedTime; }
    }
}
