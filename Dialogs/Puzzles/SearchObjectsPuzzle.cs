using Dialog;
using UnityEngine;

namespace Puzzle
{
    public class SearchObjectsPuzzle : DialogNode
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
            ImageWithDifferencesPackage puzzleSearchObjectsPackage;

            if (failFunc)
            {
                puzzleSearchObjectsPackage
                = new ImageWithDifferencesPackage(
                    puzzlePackage.ImageWithDifferences,
                    puzzlePackage.DifferenceCount,
                    puzzlePackage.AllottedTime,
                    puzzleBackground,
                    successPuzzleDialog.StartDialog,
                    failedPuzzleDialog.StartDialog,
                    trashCount: puzzlePackage.TrashCount);
            }
            else if (successFunc)
            {
                puzzleSearchObjectsPackage
                = new ImageWithDifferencesPackage(
                    puzzlePackage.ImageWithDifferences,
                    puzzlePackage.DifferenceCount,
                    puzzlePackage.AllottedTime,
                    puzzleBackground,
                    successPuzzleDialog.StartDialog,
                    trashCount: puzzlePackage.TrashCount);
            }
            else
            {
                puzzleSearchObjectsPackage
                = new ImageWithDifferencesPackage(
                    puzzlePackage.ImageWithDifferences,
                    puzzlePackage.DifferenceCount,
                    puzzlePackage.AllottedTime,
                    puzzleBackground,
                    trashCount: puzzlePackage.TrashCount);
            }

            DialogManager.SwithToPuzzle(puzzleSearchObjectsPackage, additional: "search");
        }
    }
}
