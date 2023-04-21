using FirUnityEditor;
using System;
using UnityEngine;

namespace Puzzle.BossBattle
{
    public class MagicBulletPointerOperator : MonoBehaviour
    {
        private static Vector2 fullRect;
        private Vector2 worldPosition;
        [SerializeField, NullCheck]
        private RectTransform parentTransform;
        [SerializeField, NullCheck]
        private RectTransform rectTransform;
        [SerializeField, NullCheck]
        private MagicBulletOperator magicBulletOperator;
        private bool expansion = true;

        void Update()
        {
            bool oldExpansion = expansion;

            float distance = Vector3.Distance(Input.mousePosition, worldPosition);

            expansion = distance > rectTransform.sizeDelta.x / 2;

            if (oldExpansion != expansion)
            {
                magicBulletOperator.expansion = expansion;
            }
        }

        public void RefreshRectOffset()
        {
            worldPosition = fullRect + (Vector2)parentTransform.localPosition;
        }

        public static void SetFullRect(RectTransform rectTransform)
        {
            fullRect = rectTransform.rect.size/2;
        }
    }
}
