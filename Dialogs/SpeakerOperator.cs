using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dialog
{
    public class SpeakerOperator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;
        [SerializeField]
        private Image image;
        [SerializeField]
        private Transform imageTransform;
        [SerializeField]
        private Canvas imageCanvas;
        [SerializeField]
        private Color speakerOnBackgroundColor;
        [SerializeField]
        private Vector3 scaleOnBackground;
        [SerializeField]
        [Range(0f, 1f)]
        private float unitScale;
        public CharacterInformator characterInformator { get; private set; }

        private static int backgroundSortingOrder = 2;
        private static int foregroundSortingOrder = 3;
        [SerializeField]
        private bool onBackground;

        [Inject]
        private DialogOperator dialogOperator;

        internal void SetCharacter(CharacterInformator speaker)
        {
            characterInformator = speaker;
            image.sprite = speaker.UnitSprite;
            image.SetNativeSize();
            if (speaker.UnitScale == 0)
                Debug.LogError("Unit Scale = 0!");
            unitScale = speaker.UnitScale;
            ToTheBackground();
        }

        public void ToTheBackground()
        {
            onBackground = true;
            image.color = speakerOnBackgroundColor;
            imageCanvas.sortingOrder = dialogOperator.OrderLayer + backgroundSortingOrder;
            ScaleUnit();
        }

        public void ToTheForeground()
        {
            onBackground = false;
            image.color = Color.white;
            imageCanvas.sortingOrder = dialogOperator.OrderLayer + foregroundSortingOrder;
            ScaleUnit();
        }

        [ContextMenu("ScaleUnit")]
        private void ScaleUnit()
        {
            float coefficient = dialogOperator.RectTransformHeight / image.preferredHeight;
            switch (onBackground)
            {
                case true:
                    imageTransform.localScale = scaleOnBackground * coefficient * unitScale;
                    break;
                case false:
                    imageTransform.localScale = Vector3.one * coefficient * unitScale;
                    break;
            }
        }
    }
}
