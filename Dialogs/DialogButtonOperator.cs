using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class DialogButtonOperator : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textMeshPro;
        [SerializeField]
        private Button button;
        [SerializeField]
        private Image image;

        public void SetWay(DialogNode node, string description, Sprite sprite = null)
        {
            textMeshPro.text = description;
            image.sprite = sprite;
            button.onClick.AddListener(node.StartDialog);
        }
    }
}
