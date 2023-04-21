using FirUnityEditor;
using UnityEngine;

namespace Puzzle.Nand
{
    public class NandInformator : MonoBehaviour
    {
        [SerializeField, NullCheck]
        private GameObject nand;
        [SerializeField]
        private Color onColor;
        public Color OnColor { get => onColor; }
        [SerializeField, NullCheck]
        private Sprite onSprite;
        [SerializeField]
        private Color offColor;
        public Color OffColor { get => offColor; }
        [SerializeField, NullCheck]
        private Sprite offSprite;
        [SerializeField]
        private Color nullColor;
        public Color NullColor { get => nullColor; }
        [SerializeField, NullCheck]
        private Sprite nullSprite;

        public GameObject Nand { get => nand; }

        public Sprite GetSignalSprite(bool? signal = null)
        {
            if (signal == null)
                return nullSprite;

            return signal.Value ? onSprite : offSprite;
        }
    }
}
