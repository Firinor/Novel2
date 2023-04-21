using FirUnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Puzzle.StarMap
{
    public class StarMapInGlassBallOperator : MonoBehaviour
    {
        [SerializeField, NullCheck]
        private GlassBallViewOperator glassBallViewOperator;
        [SerializeField, NullCheck]
        private StarMapScrollRect starMapScrollRect;
        [SerializeField, NullCheck]
        private RectTransform rectTransform;
        [SerializeField, NullCheck]
        private RectTransform cursorRectTransform;
        [SerializeField, NullCheck]
        private Image puzzleImage;
        [SerializeField, NullCheck]
        private Image cursorImage;
        private Vector2 defaultSize;

        [SerializeField, NullCheck]
        private Image targetImage;

        void Awake()
        {
            defaultSize = rectTransform.sizeDelta;
        }

        public void SetImageScale(float value)
        {
            rectTransform.localScale = Vector3.one * value;
            cursorRectTransform.localScale = Vector3.one / value;
        }
        public void SetPuzzleSprite(Sprite sprite, bool isEasy)
        {
            puzzleImage.sprite = sprite;
            if (isEasy)
                ResetRotation();
            else
                RandomRotate();
        }

        public void SetAnswerSprite(Sprite sprite)
        {
            targetImage.sprite = sprite;
        }

        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }

        public void SetCursorPosition(Vector2 point)
        {
            cursorRectTransform.anchoredPosition = point;
            if (!cursorImage.enabled)
                SetCursorActivity(true);
        }

        public void SetCursorActivity(bool v)
        {
            cursorImage.enabled = v;
        }

        public void SetTargetActivity(bool v)
        {
            targetImage.enabled = v;
        }

        public Vector2Int GetCursorPoint()
        {
            return new Vector2Int((int)(cursorRectTransform.anchoredPosition.x + defaultSize.x / 2),
                                  (int)(cursorRectTransform.anchoredPosition.y + defaultSize.y / 2));
        }

        private void RandomRotate()
        {
            float rand = Random.Range(0, 360);
            rectTransform.localRotation = Quaternion.Euler(0, 0, rand);
            cursorRectTransform.localRotation = Quaternion.Euler(0, 0, -rand);
            starMapScrollRect.SetCoefficient(rand);
        }

        private void ResetRotation()
        {
            rectTransform.localRotation = Quaternion.identity;
            cursorRectTransform.localRotation = Quaternion.identity;
            starMapScrollRect.SetCoefficient(0);
        }
    }
}