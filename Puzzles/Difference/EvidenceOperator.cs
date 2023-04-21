using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Puzzle.FindDifferences
{
    [RequireComponent(typeof(Image))]
    public class EvidenceOperator : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public static bool cursorOnImage = false;

        private Image image;

        [SerializeField]
        private DetectiveDeskOperator detectiveDeskOperator;
        [SerializeField]
        private FindDifferencesManager findDifferencesOperator;
        [SerializeField]
        private CursorOnEvidence cursorOnEvidence;
        [SerializeField]
        private RectTransform fullScreenRectTransform;
        [SerializeField]
        private RectTransform detectiveDeskRectTransform;
        [SerializeField]
        private RectTransform evidencesRectTransform;
        private Vector2 startOfImage;

        void Awake()
        {
            image = GetComponent<Image>();
        }
        public void CalculateStartOfImage()
        {
            image.raycastTarget = true;
            Rect fullScreenRect = fullScreenRectTransform.rect;
            Vector2 deskRest = detectiveDeskRectTransform.anchoredPosition;
            Vector2 evidenceRect = GetComponent<RectTransform>().offsetMin;
            startOfImage = new Vector2(-fullScreenRect.x + deskRest.x + evidenceRect.x,
                                    -fullScreenRect.y + deskRest.y +evidenceRect.y);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            cursorOnImage = true;
            Cursor.visible = false;
            findDifferencesOperator.SetCursorOnEvidence(cursorOnEvidence);
            findDifferencesOperator.EnableCursors();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            cursorOnImage = false;
            Cursor.visible = true;
            findDifferencesOperator.DisableCursors();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector2 evidences = evidencesRectTransform.anchoredPosition;
            detectiveDeskOperator.CheckTheEvidence(
                eventData.position/CanvasManager.ScaleFactor - startOfImage - evidences, cursorOnEvidence);
        }

        public void DisableImage()
        {
            image.raycastTarget = false;
        }
    }
}
