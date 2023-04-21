using UnityEngine;
using UnityEngine.UI;

namespace FirMath
{
    public static class GameImage
    {
        public static void SetImageWidth(Image image, float size)
        {
            if(isFailedCheck(image, size))
                return;

            ProcessImageSize(image, size, false);
        }
        public static void SetImageHeight(Image image, float size)
        {
            if (isFailedCheck(image, size))
                return;

            ProcessImageSize(image, size, true);
        }

        private static bool isFailedCheck(Image image, float size)
        {
            return image == null || size <= 0;
        }

        private static void ProcessImageSize(Image image, float size, bool height)
        {
            image.SetNativeSize();
            float ratio = image.sprite.textureRect.width / image.sprite.textureRect.height;

            RectTransform rectTransform = image.rectTransform;

            rectTransform.sizeDelta = new Vector2(size * (height ? ratio : 1),
                                                  size / (height ? 1 : ratio));
        }
    }
}
