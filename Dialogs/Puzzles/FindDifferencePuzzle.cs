using Dialog;
using UnityEngine;

namespace Puzzle
{
    public class FindDifferencePuzzle : DialogNode
    {
        [SerializeField]
        private DialogNode successPuzzleDialog;
        [SerializeField]
        private DialogNode failedPuzzleDialog;
        [SerializeField]
        private Sprite puzzleBackground;
        [SerializeField]
        private ImageWithDifferencesPackage puzzlePackage;

        public override void StartDialog()
        {
            bool successFunc = successPuzzleDialog != null;
            bool failFunc = failedPuzzleDialog != null;

            StopDialogSkip();
            ImageWithDifferencesPackage puzzleFindDifferencePackage;

            if (failFunc)
            {
                puzzleFindDifferencePackage
                = new ImageWithDifferencesPackage(
                    puzzlePackage.ImageWithDifferences,
                    puzzlePackage.DifferenceCount,
                    puzzlePackage.AllottedTime,
                    puzzleBackground,
                    successPuzzleDialog.StartDialog,
                    failedPuzzleDialog.StartDialog);
            }
            else if (successFunc)
            {
                puzzleFindDifferencePackage
                = new ImageWithDifferencesPackage(
                    puzzlePackage.ImageWithDifferences,
                    puzzlePackage.DifferenceCount,
                    puzzlePackage.AllottedTime,
                    puzzleBackground,
                    successPuzzleDialog.StartDialog);
            }
            else
            {
                puzzleFindDifferencePackage
                = new ImageWithDifferencesPackage(
                    puzzlePackage.ImageWithDifferences,
                    puzzlePackage.DifferenceCount,
                    puzzlePackage.AllottedTime,
                    puzzleBackground);
            }

            DialogManager.SwithToPuzzle(puzzleFindDifferencePackage);
        }
    }
}
