using UnityEngine;
using UnityEngine.UI;

public enum CursorOnEvidence { left = -1, right = 1 }

namespace Puzzle.FindDifferences
{
    public class DoubleCursorOperator : MonoBehaviour
    {
        [SerializeField]
        private Transform leftCursor;
        [SerializeField]
        private Transform rightCursor;
        [SerializeField]
        private Image leftCursorImage;
        [SerializeField]
        private Image rightCursorImage;

        private float offset;

        [HideInInspector]
        public CursorOnEvidence cursorOnEvidence;

        public void MoveCursor()
        {
            Vector3 mousePosition = Input.mousePosition / CanvasManager.ScaleFactor;
            switch (cursorOnEvidence)
            {
                case CursorOnEvidence.left:
                    leftCursor.localPosition = mousePosition;
                    rightCursor.localPosition = mousePosition + new Vector3(offset, 0, 0);
                    break;
                case CursorOnEvidence.right:
                    rightCursor.localPosition = mousePosition;
                    leftCursor.localPosition = mousePosition + new Vector3(-offset, 0, 0);
                    break;
            }
        }

        public void SetOffset(float offset)
        {
            this.offset = offset;
        }

        public void DisableCursors()
        {
            SwitchCursors(false);
        }
        public void EnableCursors()
        {
            SwitchCursors(true);
        }

        private void SwitchCursors(bool enable)
        {
            rightCursorImage.enabled = enable;
            leftCursorImage.enabled = enable;
        }
    }
}
