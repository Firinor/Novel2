using FirMath;
using FirUnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Puzzle.Nand
{
    [RequireComponent(typeof(LineRenderer))]
    public class InputOperator : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField, NullCheck]
        private RectTransform rectTransform;
        [SerializeField, NullCheck]
        private LineRenderer line;
        [SerializeField, NullCheck]
        private LineFieldOperator fieldOperator;
        public LineFieldOperator LineFieldOperator { set { fieldOperator = value; } }
        [SerializeField, NullCheck]
        private NandInformator nandInformator;
        public NandInformator NandInformator { set { nandInformator = value; } }

        [SerializeField, NullCheck]
        private NandOperator nandOperator;
        public NandOperator NandOperator { set { nandOperator = value; } }

        [SerializeField, NullCheck]
        private Image image;

        private Vector2 basePosition;

        private OutputOperator pickedOutput;

        public bool? GetSignal()
        {
            if(pickedOutput == null)
                return false;
            return pickedOutput.Signal;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            ResetLine();
            line.positionCount = 2;
            Vector3 zeroPoint = new Vector3(0, -rectTransform.rect.height / 2, 0);
            line.SetPosition(0, zeroPoint);
            //RefreshLineColor();
            basePosition = GameTransform.GetGlobalPoint(transform);
            //fieldOperator.ResetAllNand();
        }

        public void OnDrag(PointerEventData eventData)
        {
            line.SetPosition(1, ((Vector2)Input.mousePosition - basePosition) / CanvasManager.ScaleFactor);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (fieldOperator.pickedOutput == null)
            {
                ResetLine();
                return;
            }

            pickedOutput = fieldOperator.pickedOutput;
            pickedOutput.OnSignal += Refresh;
            pickedOutput.OnMoveAction += OnMoveAction;
            pickedOutput.OnRemoveAction += ResetLine;
            if(nandOperator != null)
            {
                pickedOutput.OnSignalPhase2 += nandOperator.CalculateSignal;
            }

            OnMoveAction();
            Refresh();
        }

        public void ResetLine()
        {
            line.positionCount = 0;
            if (pickedOutput != null)
            {
                pickedOutput.OnSignal -= Refresh;
                pickedOutput.OnMoveAction -= OnMoveAction;
                pickedOutput.OnRemoveAction -= ResetLine;
                if (nandOperator != null)
                {
                    pickedOutput.OnSignalPhase2 -= nandOperator.CalculateSignal;
                }
            }
            pickedOutput = null;
            Refresh();
        }

        public void OnMoveAction()
        {
            if (pickedOutput == null)
            {
                return;
            }

            Vector2 outputTransformPoint = GameTransform.GetGlobalPoint(pickedOutput.transform);
            Vector2 thisTransformPoint = GameTransform.GetGlobalPoint(transform);
            Vector2 connectPoint = pickedOutput.ConnectPoint;
            line.SetPosition(1, 
                ((outputTransformPoint - thisTransformPoint)/ CanvasManager.ScaleFactor) + connectPoint
                );
        }

        private void Refresh()
        {
            if(nandOperator != null)
                nandOperator.CalculateSignal();

            RefreshLineColor();
            RefreshSprite();
        }
        private void RefreshLineColor()
        {
            Color color;
            if (pickedOutput != null)
            {
                if (pickedOutput.Signal == null)
                    color = nandInformator.NullColor;
                else if (pickedOutput.Signal.Value)
                    color = nandInformator.OnColor;
                else
                    color = nandInformator.OffColor;
            }
            else
            {
                color = nandInformator.OffColor;
            }
            line.startColor = color;
            line.endColor = color;
        }

        public void SetRaycastTarget(bool v)
        {
            image.raycastTarget = v;
        }

        private void RefreshSprite()
        {
            bool? Signal = false;
            if (pickedOutput != null)
            {
                Signal = pickedOutput.Signal;
            }
            image.sprite = nandInformator.GetSignalSprite(Signal);
        }
    }
}
