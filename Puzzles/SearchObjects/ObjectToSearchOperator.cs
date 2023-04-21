using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.SearchObjects
{
    public class ObjectToSearchOperator : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        internal void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
            image.SetNativeSize();
        }
        internal void SetRecipeSprite(Sprite sprite)
        {
            SetSprite(sprite);
            image.raycastTarget = false;
            enabled = false;
        }
    }
}