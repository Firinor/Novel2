using FirUnityEditor;
using Puzzle.FindObject;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle
{
    [Serializable]
    public class FindRecipeIngredientsPackage : InformationPackage
    {
        public FindRecipeIngredientsPackage(Sprite[] ingredients, int recipeDifficulty, int ingredientsCount, float allottedTime,
            Sprite puzzleBackground, UnityAction successPuzzleDialog = null, UnityAction failedPuzzleDialog = null)
            : base(puzzleBackground, successPuzzleDialog, failedPuzzleDialog)
        {
            this.ingredients = ingredients;

            if (ingredientsCount < 2)
            {
                ingredientsCount = 2;
            }
            if (recipeDifficulty > ingredientsCount)
            {
                recipeDifficulty = ingredientsCount;
            }
            if (recipeDifficulty < 1)
            {
                recipeDifficulty = 1;
            }

            this.recipeDifficulty = recipeDifficulty;
            this.ingredientsCount = ingredientsCount;
            this.allottedTime = Math.Max(allottedTime, 0);
        }
        [SerializeField]
        [Range(1, 10)]
        private int recipeDifficulty = 1;
        [SerializeField]
        [Range(10, 1024)]
        private int ingredientsCount = 2;
        [SerializeField]
        [Range(0, 1024)]
        private float allottedTime = 0;
        [SerializeField, NullCheck]
        private Sprite[] ingredients;

        public int RecipeDifficulty { get => recipeDifficulty; }
        public int IngredientsCount { get => Math.Min(ingredients.Length, ingredientsCount); }
        public float AllottedTime { get => allottedTime; }

        public Sprite[] Ingredients { get => ingredients; }
    }
}