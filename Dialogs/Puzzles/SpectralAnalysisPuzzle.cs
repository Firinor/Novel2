using Dialog;
using UnityEngine;

namespace Puzzle
{
    public class SpectralAnalysisPuzzle : DialogNode
    {
        [SerializeField]
        private DialogNode successPuzzleDialog;
        [SerializeField]
        private DialogNode failedPuzzleDialog;
        [SerializeField]
        private Sprite puzzleBackground;
        [SerializeField]
        private SpectralAnalysisPackage puzzlePackage;

        public override void StartDialog()
        {
            bool successFunc = successPuzzleDialog != null;
            bool failFunc = failedPuzzleDialog != null;

            StopDialogSkip();
            SpectralAnalysisPackage spectralAnalysisPackage;

            if (failFunc)
            {
                spectralAnalysisPackage
                    = new SpectralAnalysisPackage(
                        puzzlePackage.ColorShift,
                        puzzlePackage.RecipeCount,
                        puzzlePackage.IngredientsCount,
                        puzzlePackage.AllottedTime,
                        puzzleBackground,
                        successPuzzleDialog.StartDialog,
                        failedPuzzleDialog.StartDialog);
            }
            else if (successFunc)
            {
                spectralAnalysisPackage
                    = new SpectralAnalysisPackage(
                        puzzlePackage.ColorShift,
                        puzzlePackage.RecipeCount,
                        puzzlePackage.IngredientsCount,
                        puzzlePackage.AllottedTime,
                        puzzleBackground,
                        successPuzzleDialog.StartDialog);
            }
            else
            {
                spectralAnalysisPackage
                    = new SpectralAnalysisPackage(
                        puzzlePackage.ColorShift,
                        puzzlePackage.RecipeCount,
                        puzzlePackage.IngredientsCount,
                        puzzlePackage.AllottedTime,
                        puzzleBackground);
            }
            DialogManager.SwithToPuzzle(spectralAnalysisPackage);
        }
    }
}
