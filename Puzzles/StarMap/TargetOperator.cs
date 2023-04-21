using FirMath;
using FirUnityEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.StarMap
{
    public class TargetOperator : MonoBehaviour
    {
        [SerializeField, NullCheck]
        private Button button;
        [SerializeField, NullCheck]
        private StarMapManager starMapOperator;
        [SerializeField, NullCheck]
        private Image keyImage;
        [SerializeField, NullCheck]
        private TextMeshProUGUI keyText;

        public void CheckAnswer()
        {
            SetButtonActivity(false);
            starMapOperator.CheckAnswer();
        }

        public void SetButtonActivity(bool v)
        {
            button.gameObject.SetActive(v);
        }

        public void SetAnswerKey(Sprite sprite)
        {
            keyImage.sprite = sprite;
            GameImage.SetImageWidth(keyImage, keyImage.rectTransform.sizeDelta.x);
            keyText.text = sprite.name;
        }
    }
}