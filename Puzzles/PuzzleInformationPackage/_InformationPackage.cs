using UnityEngine;
using UnityEngine.Events;

namespace Puzzle
{
    public abstract class InformationPackage
    {
        protected InformationPackage(Sprite puzzleBackground,
            UnityAction successPuzzleAction = null, UnityAction failedPuzzleAction = null)
        {
            this.successPuzzleAction = null;
            this.failedPuzzleAction = null;
            if (successPuzzleAction != null)
                this.successPuzzleAction += successPuzzleAction;
            if(failedPuzzleAction != null)
                this.failedPuzzleAction += failedPuzzleAction;

            PuzzleBackground = puzzleBackground;
        }
        public UnityAction successPuzzleAction;
        public UnityAction failedPuzzleAction;
        public Sprite PuzzleBackground { get; }

    }
}
