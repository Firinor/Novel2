using Dialog;
using UnityEngine;

namespace Puzzle
{
    public class TetraQuestionPuzzle : DialogNode
    {
        [SerializeField]
        private DialogNode successPuzzleDialog;
        [SerializeField]
        private DialogNode failedPuzzleDialog;
        [SerializeField]
        private Sprite puzzleBackground;
        [SerializeField]
        private TetraQuestionPackage puzzlePackage;

        public override void StartDialog()
        {
            bool successFunc = successPuzzleDialog != null;
            bool failFunc = failedPuzzleDialog != null;

            StopDialogSkip();
            TetraQuestionPackage puzzleTetraQuestionPackage;

            if (failFunc)
            {
                puzzleTetraQuestionPackage
                = new TetraQuestionPackage(
                    puzzlePackage.Questions,
                    puzzleBackground,
                    successPuzzleDialog.StartDialog,
                    failedPuzzleDialog.StartDialog);
            }
            else if (successFunc)
            {
                puzzleTetraQuestionPackage
                = new TetraQuestionPackage(
                    puzzlePackage.Questions,
                    puzzleBackground,
                    successPuzzleDialog.StartDialog);
            }
            else
            {
                puzzleTetraQuestionPackage
                = new TetraQuestionPackage(
                    puzzlePackage.Questions,
                    puzzleBackground);
            }

            DialogManager.SwithToPuzzle(puzzleTetraQuestionPackage);
        }
    }
}

