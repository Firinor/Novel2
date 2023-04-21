using FirUnityEditor;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Puzzle.Nand
{
    [RequireComponent(typeof(Image))]
    public class OutputOperator : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private bool interactive;
        [SerializeField]
        private bool? signal = false;
        public bool? Signal
        {
            get
            {
                return signal;
            }
        }

        private Vector3 connectPoint;
        public Vector3 ConnectPoint { get { return connectPoint; } }

        [Space(15)]
        [SerializeField, NullCheck]
        private RectTransform rectTransform;
        [SerializeField, NullCheck]
        private LineFieldOperator fieldOperator;
        public LineFieldOperator LineFieldOperator { set { fieldOperator = value; } }
        [SerializeField, NullCheck]
        private NandInformator nandInformator;
        public NandInformator NandInformator { set { nandInformator = value; } }
        
        [SerializeField, NullCheck]
        private Image image;

        public event Action OnSignal;
        public event Action OnSignalPhase2;
        public event Action OnMoveAction;
        public event Action OnRemoveAction;

        void Awake()
        {
            if (image == null)
            {
                image = GetComponent<Image>();
            }
            connectPoint = new Vector3(0, rectTransform.rect.height / 2, 0);
    }

        public void SetSignal(bool? value)
        {
                signal = value;
                RefreshSprite();
                OnSignal?.Invoke();
                OnSignalPhase2?.Invoke();
        }
        private void RefreshSprite()
        {
            if(nandInformator != null)
                image.sprite = nandInformator.GetSignalSprite(signal);
        }

        #region Interfaces
        public void OnPointerClick(PointerEventData eventData)
        {
            if (interactive && signal != null)
            {
                SetSignal(!signal.Value);
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            fieldOperator.pickedOutput = this;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            fieldOperator.pickedOutput = null;
        }
        #endregion

        public void SetRaycastTarget(bool v)
        {
            image.raycastTarget = v;
        }
        public void OnMove()
        {
            OnMoveAction?.Invoke();
        }
        public void OnRemove()
        {
            OnRemoveAction?.Invoke();
        }
    }
}
