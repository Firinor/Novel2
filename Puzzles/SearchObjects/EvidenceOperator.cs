using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Puzzle.SearchObjects
{
    [RequireComponent(typeof(Image))]
    public class EvidenceOperator : MonoBehaviour, IPointerClickHandler
    {
        public static bool cursorOnImage = false;

        private Image image;

        [SerializeField]
        private DetectiveDeskOperator detectiveDeskOperator;
        [SerializeField]
        private SearchObjectsManager searchObjectsOperator;
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

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector2 evidences = evidencesRectTransform.anchoredPosition;
            detectiveDeskOperator.CheckTheEvidence(eventData.position - startOfImage - evidences);
        }

        public void DisableImage()
        {
            image.raycastTarget = false;
        }
    }
}
