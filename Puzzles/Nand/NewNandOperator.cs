using FirUnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Puzzle.Nand
{
    public class NewNandOperator : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, NullCheck]
        private Image image;
        [SerializeField, NullCheck]
        private NandManager nandManager;

        private NandOperator nandOperator;
        private void Update()
        {
            if (nandManager == null)
            {
                nandManager = FindObjectOfType<NandManager>();
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                nandOperator = nandManager.CreateNewNand();
                nandOperator.OnBeginDrag(eventData);
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            nandOperator.OnDrag(eventData);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            nandOperator.OnEndDrag(eventData);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one * 0.9f;
        }
    }
}
