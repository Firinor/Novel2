using Dialog;
using UnityEngine;

namespace Puzzle
{
    public class FindRecipeIngredientsPuzzle : DialogNode
    {
        [SerializeField]
        private DialogNode successPuzzleDialog;
        [SerializeField]
        private DialogNode failedPuzzleDialog;
        [SerializeField]
        private Sprite puzzleBackground;
        [SerializeField]
        private FindRecipeIngredientsPackage puzzlePackage;

        public override void StartDialog()
        {
            bool successFunc = successPuzzleDialog != null;
            bool failFunc = failedPuzzleDialog != null;

            StopDialogSkip();
            FindRecipeIngredientsPackage puzzleFindRecipeIngredientsPackage;

            if (failFunc)
            {
                puzzleFindRecipeIngredientsPackage
                = new FindRecipeIngredientsPackage(
                    puzzlePackage.Ingredients,
                    puzzlePackage.RecipeDifficulty,
                    puzzlePackage.IngredientsCount,
                    puzzlePackage.AllottedTime,
                    puzzleBackground,
                    successPuzzleDialog.StartDialog,
                    failedPuzzleDialog.StartDialog);
            }
            else if (successFunc)
            {
                puzzleFindRecipeIngredientsPackage
                = new FindRecipeIngredientsPackage(
                    puzzlePackage.Ingredients,
                    puzzlePackage.RecipeDifficulty,
                    puzzlePackage.IngredientsCount,
                    puzzlePackage.AllottedTime,
                    puzzleBackground,
                    successPuzzleDialog.StartDialog);
            }
            else
            {
                puzzleFindRecipeIngredientsPackage
                = new FindRecipeIngredientsPackage(
                    puzzlePackage.Ingredients,
                    puzzlePackage.RecipeDifficulty,
                    puzzlePackage.IngredientsCount,
                    puzzlePackage.AllottedTime,
                    puzzleBackground);
            }

            DialogManager.SwithToPuzzle(puzzleFindRecipeIngredientsPackage);
        }
    }
}
