using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Puzzle.StarMap
{
    public class HelpMapScrollRect : ScrollRect
    {
        private HelpMapOperator helpMapOperator;

        public void SetGlassBallViewOperator(HelpMapOperator helpMapOperator)
        {
            this.helpMapOperator = helpMapOperator;
        }

        public override void OnScroll(PointerEventData data)
        {
            //pointBeforeScaling
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
                    Input.mousePosition, data.pressEventCamera, out Vector2 pointBeforeScaling);
            //Scaling
            float scaleValue = helpMapOperator.ZoomScroll(Input.mouseScrollDelta);
            //pointAfterScaling
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
                    Input.mousePosition, data.pressEventCamera, out Vector2 pointAfterScaling);
            //delta * scaleValue
            Vector2 delta = pointBeforeScaling - pointAfterScaling;
            delta *= scaleValue;
            //We shift the map to the mouse cursor
            content.anchoredPosition -= delta;
            
        }
    }
}