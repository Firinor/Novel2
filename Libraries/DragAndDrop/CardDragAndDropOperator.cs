using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FirDragAndDrop
{
    public class CardDragAndDropOperator : MonoBehaviour,
        IBeginDragHandler, IDragHandler, IEndDragHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private GameObject dragAndDropObject;

        private bool drag = false;
        private bool cursorOnCard = false;

        private Vector3 startMousePosition;

        public void Start()
        {
            if(dragAndDropObject == null)
                dragAndDropObject = gameObject;
        }
        public void Update()
        {
            if (Input.GetMouseButtonDown(1) && drag)
            {
                drag = false;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            image.raycastTarget = false;
            startMousePosition = Input.mousePosition / CanvasManager.ScaleFactor - transform.localPosition;
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                drag = true;
            }
            else
            {
                eventData.pointerDrag = null;
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (drag && eventData.button == PointerEventData.InputButton.Left)
            {
                transform.localPosition
                    = Input.mousePosition / CanvasManager.ScaleFactor - startMousePosition;
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            //Debug.Log(impulse + " " + impulse.x + " " + impulse.y);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            cursorOnCard = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            cursorOnCard = false;
        }
    }
}
