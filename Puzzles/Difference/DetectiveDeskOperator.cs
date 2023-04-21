using FirCleaner;
using FirMath;
using FirUnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.FindDifferences
{
    public class DetectiveDeskOperator : MonoBehaviour
    {
        [SerializeField, NullCheck]
        private FindDifferencesManager findDifferencesOperator;

        [SerializeField, NullCheck]
        private Image startImage;
        [SerializeField, NullCheck]
        private TextMeshProUGUI textMeshProUGUI;
        [SerializeField, NullCheck]
        private RectTransform rectTransform;

        [SerializeField]
        private Dictionary<GameObject, Rect> differences;

        [SerializeField, NullCheck]
        private Button button;

        [SerializeField, NullCheck]
        private Image leftImage;
        [SerializeField, NullCheck]
        private Image rightImage;
        [SerializeField, NullCheck]
        private EvidenceOperator leftEvidenceOperator;
        [SerializeField, NullCheck]
        private EvidenceOperator rightEvidenceOperator;

        [SerializeField]
        private float differenceShowingTime = 4;

        [SerializeField]
        private float screenWidthRatio = 0.95f;//pixels
        [SerializeField]
        private float screenHeightRatio = 0.9f;//pixels

        public void DisableButton()
        {
            ButtonStatus(false);

        }
        public void EndOfAnimation()
        {
            EnableButton();
        }
        public void EnableButton()
        {
            ButtonStatus(true);
        }

        private void ButtonStatus(bool v)
        {
            textMeshProUGUI.enabled = v;
            button.enabled = v;
        }

        public void CreateImages(ImageWithDifferences imageWithDifferences, int differenceCount,
            GameObject differencePrefab, int offset, out float imageOffset)
        {
            differences = new Dictionary<GameObject, Rect>();

            float canvas_x = (CanvasManager.ScreenWidth * screenWidthRatio - offset * 3) / 2;//left, center, right (2 images in a row)
            float canvas_y = CanvasManager.ScreenHeight * screenHeightRatio - offset * 2;//top, bottom (1 image on collum)

            Sprite mainSprite = imageWithDifferences.Sprite;
            float image_x = mainSprite.textureRect.width;
            float image_y = mainSprite.textureRect.height;

            float ratio_x = canvas_x / image_x;
            float ratio_y = canvas_y / image_y;

            float scaleRatio = Math.Min(ratio_x, ratio_y);
            WorkToImage(leftImage, mainSprite, scaleRatio);
            leftImage.GetComponent<RectTransform>().localPosition = new Vector3(-offset / 2, 0);
            WorkToImage(rightImage, mainSprite, scaleRatio);
            rightImage.GetComponent<RectTransform>().localPosition = new Vector3(offset / 2, 0);

            imageOffset = rightImage.GetComponent<RectTransform>().sizeDelta.x + offset;

            leftEvidenceOperator.enabled = true;
            leftEvidenceOperator.CalculateStartOfImage();
            rightEvidenceOperator.enabled = true;
            rightEvidenceOperator.CalculateStartOfImage();

            List<int> differencesIntList = GameMath.AFewCardsFromTheDeck(differenceCount, imageWithDifferences.Differences.Length);
            foreach (var difference in differencesIntList)
            {
                Transform parent = GameMath.HeadsOrTails() ? leftImage.transform : rightImage.transform;
                GameObject newDifference = Instantiate(differencePrefab, parent);

                var differenceComponent = imageWithDifferences.Differences[difference];

                Image differenceImage = newDifference.GetComponent<Image>();
                differenceImage.sprite = differenceComponent.sprite;
                differenceImage.SetNativeSize();

                RectTransform differenceRectTransform = newDifference.GetComponent<RectTransform>();
                differenceRectTransform.sizeDelta *= scaleRatio;
                differenceRectTransform.anchoredPosition =
                    new Vector2(differenceComponent.xShift * scaleRatio, differenceComponent.yShift * scaleRatio);

                differences.Add(newDifference,
                    new Rect(differenceRectTransform.offsetMin, differenceRectTransform.sizeDelta));
            }
        }

        private void WorkToImage(Image image, Sprite sprite, float scaleRatio)
        {
            image.enabled = true;
            image.sprite = sprite;
            image.SetNativeSize();
            image.GetComponent<RectTransform>().sizeDelta *= scaleRatio;
        }

        public void ClearImages()
        {
            leftImage.enabled = false;
            rightImage.enabled = false;
            leftEvidenceOperator.enabled = false;
            rightEvidenceOperator.enabled = false;
        }
        public void DisableImages()
        {
            leftEvidenceOperator.DisableImage();
            rightEvidenceOperator.DisableImage();
        }

        internal void CheckTheEvidence(Vector2 pointOnImage, CursorOnEvidence cursorOnEvidence)
        {
            if (!enabled)
                return;

            foreach(KeyValuePair<GameObject, Rect> difference in differences)
            {
                if (difference.Value.Contains(pointOnImage))
                {
                    StartCoroutine(ButtonAnimating(difference));
                    findDifferencesOperator.ActivateDifference(difference.Key, cursorOnEvidence);
                    differences.Remove(difference.Key);
                    return;
                }
            }

            findDifferencesOperator.ErrorShake(cursorOnEvidence);
        }

        public void DeleteAllDifference()
        {
            if (differences != null)
            {
                GameCleaner.DeleteAllGameObjects(differences);
                //foreach (KeyValuePair<GameObject, Rect> difference in differences)
                //    Destroy(difference.Key);

                differences.Clear();
            }

            GameCleaner.DeleteAllChild(leftImage);
            GameCleaner.DeleteAllChild(rightImage);
        }

        private IEnumerator ButtonAnimating(KeyValuePair<GameObject, Rect> difference)
        {
            float deltaTime = 0.4f;
            WaitForSeconds wait = new WaitForSeconds(deltaTime);
            float timer = 0;

            while (timer < differenceShowingTime)
            {
                yield return wait;
                timer += deltaTime;
                difference.Key.transform.SetParent(leftImage.transform);
                difference.Key.GetComponent<RectTransform>().anchoredPosition = difference.Value.position;
                yield return wait;
                timer += deltaTime;
                difference.Key.transform.SetParent(rightImage.transform);
                difference.Key.GetComponent<RectTransform>().anchoredPosition = difference.Value.position;
            }
            Destroy(difference.Key);
        }
    }
}
