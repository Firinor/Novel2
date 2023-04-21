using FirUnityEditor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.StarMap
{
    public class GlassBallViewOperator : MonoBehaviour
    {
        [SerializeField]
        private float scrollStep = 0.15f;
        [SerializeField, NullCheck]
        private Slider slider;
        [SerializeField, NullCheck]
        private StarMapInGlassBallOperator starMapInGlassBallOperator;
        [SerializeField, NullCheck]
        private StarMapScrollRect starMapScrollRect;
        [SerializeField, NullCheck]
        private StarMapManager starMapOperator;

        [HideInInspector]
        public bool ActivePuzzle;
        private Vector2 mousePoint;
        [SerializeField]
        private int maxMouseDisplacement;

        void Awake()
        {
            starMapScrollRect.SetGlassBallViewOperator(this);
        }

        public float ZoomScroll(Vector2 mouseScrollDelta)
        {
            slider.value += scrollStep * (mouseScrollDelta.y > 0 ? 1 : -1);
            slider.value = Mathf.Clamp(slider.value, slider.minValue, slider.maxValue);
            return slider.value;
        }

        public void SliderZoom()
        {
            starMapInGlassBallOperator.SetImageScale(slider.value);
        }

        public RectTransform GetRectTransform()
        {
            return starMapInGlassBallOperator.GetRectTransform();
        }

        public void SetCursorPosition(Vector2 localPoint)
        {
            starMapInGlassBallOperator.SetCursorPosition(localPoint);
            if(ActivePuzzle)
                starMapOperator.SetButtonActivity();
        }

        public void StartClick()
        {
            mousePoint = Input.mousePosition;
        }
        public bool EndClickOnStartClick()
        {
            Vector2 currentMousePosition = Input.mousePosition;
            bool result = math.abs(mousePoint.x - currentMousePosition.x) < maxMouseDisplacement
                && math.abs(mousePoint.y - currentMousePosition.y) < maxMouseDisplacement;

            ResetClick();

            return result;
        }
        public void ResetClick()
        {
            mousePoint = new Vector2(-2000, -2000);
        }
    }
}
