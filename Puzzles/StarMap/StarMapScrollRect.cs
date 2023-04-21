using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Puzzle.StarMap
{
    public class StarMapScrollRect : ScrollRect, IPointerDownHandler, IPointerUpHandler
    {
        private GlassBallViewOperator glassBallViewOperator;
        private float cosCoefficient = 1;
        private float sinCoefficient = 1;

        public void SetCoefficient(float angle)
        {
            float radian = math.radians(angle);
            cosCoefficient = math.cos(radian);
            sinCoefficient = math.sin(radian);
        }

        public void SetGlassBallViewOperator(GlassBallViewOperator glassBallViewOperator)
        {
            this.glassBallViewOperator = glassBallViewOperator;
        }

        public override void OnScroll(PointerEventData data)
        {
            //pointBeforeScaling
            RectTransformUtility.ScreenPointToLocalPointInRectangle(glassBallViewOperator.GetRectTransform(),
                    Input.mousePosition, data.pressEventCamera, out Vector2 pointBeforeScaling);
            //Scaling
            float scaleValue = glassBallViewOperator.ZoomScroll(Input.mouseScrollDelta);
            //pointAfterScaling
            RectTransformUtility.ScreenPointToLocalPointInRectangle(glassBallViewOperator.GetRectTransform(),
                    Input.mousePosition, data.pressEventCamera, out Vector2 pointAfterScaling);
            //delta * scaleValue
            Vector2 delta = pointBeforeScaling - pointAfterScaling;
            delta *= scaleValue;
            //We calculate the displacement taking into account the rotation
            delta = new Vector2(delta.x * cosCoefficient + delta.y * -sinCoefficient,
                                delta.y * cosCoefficient + delta.x * sinCoefficient);
            //We shift the map to the mouse cursor
            content.anchoredPosition -= delta;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == 0)
            {
                glassBallViewOperator.StartClick();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (glassBallViewOperator.EndClickOnStartClick())
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(glassBallViewOperator.GetRectTransform(),
                    eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
                glassBallViewOperator.SetCursorPosition(localPoint);
            }

        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && IsActive())
            {
                glassBallViewOperator.ResetClick();
                base.OnBeginDrag(eventData);
            }
        }
    }
}