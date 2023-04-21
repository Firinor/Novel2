using FirCleaner;
using FirUnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.SearchObjects
{
    public class DetectiveDeskOperator : MonoBehaviour
    {
        [SerializeField, NullCheck]
        private SearchObjectsManager searchObjectsOperator;

        [SerializeField, NullCheck]
        private Image startImage;
        [SerializeField, NullCheck]
        private TextMeshProUGUI textMeshProUGUI;
        [SerializeField, NullCheck]
        private RectTransform rectTransform;

        [SerializeField]
        private Dictionary<GameObject, Rect> differences;
        [SerializeField]
        private Dictionary<GameObject, ObjectToSearchOperator> progressDifferences;

        [SerializeField, NullCheck]
        private Button button;

        [SerializeField, NullCheck]
        private Image puzzleImage;
        [SerializeField, NullCheck]
        private EvidenceOperator evidenceOperator;

        [SerializeField]
        private float differenceShowingTime = 1;
        [SerializeField]
        private float differenceSlidingDelay = 0.5f;
        [SerializeField]
        private AnimationCurve differenceSpeedCurve;
        [SerializeField]
        private float differenceSlidingTime = 1;

        [SerializeField]
        private float screenWidthRatio = 0.95f;
        [SerializeField]
        private float screenHeightRatio = 0.9f;

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
        internal void CreateImage(ImageWithDifferences imageWithDifferences,
            List<int> trashObjects, Dictionary<int, ObjectToSearchOperator> desiredObjects, GameObject searchObjectsPrefab)
        {
            differences = new Dictionary<GameObject, Rect>();
            var desiredKeys = new int[desiredObjects.Count];
            desiredObjects.Keys.CopyTo(desiredKeys, 0);
            List<int> desiredKeysList = new List<int>(desiredKeys);
            progressDifferences = new Dictionary<GameObject, ObjectToSearchOperator>();

            Sprite mainSprite = imageWithDifferences.Sprite;

            float canvas_x = CanvasManager.ScreenWidth * screenWidthRatio;
            float canvas_y = CanvasManager.ScreenHeight * screenHeightRatio;

            float image_x = mainSprite.textureRect.width;
            float image_y = mainSprite.textureRect.height;

            float ratio_x = canvas_x / image_x;
            float ratio_y = canvas_y / image_y;

            float scaleRatio = Math.Min(ratio_x, ratio_y);

            WorkToImage(puzzleImage, mainSprite, scaleRatio);

            evidenceOperator.enabled = true;
            evidenceOperator.CalculateStartOfImage();

            foreach (var difference in trashObjects)
            {
                Transform parent = puzzleImage.transform;
                GameObject newDifference = Instantiate(searchObjectsPrefab, parent);

                var differenceComponent = imageWithDifferences.Differences[difference];

                Image differenceImage = newDifference.GetComponent<Image>();
                differenceImage.sprite = differenceComponent.sprite;
                differenceImage.SetNativeSize();

                RectTransform differenceRectTransform = newDifference.GetComponent<RectTransform>();
                differenceRectTransform.sizeDelta *= scaleRatio;
                differenceRectTransform.anchoredPosition =
                    new Vector2(differenceComponent.xShift * scaleRatio, differenceComponent.yShift * scaleRatio);

                if(desiredKeysList.Contains(difference))
                {
                    differences.Add(newDifference,
                        new Rect(differenceRectTransform.offsetMin, differenceRectTransform.sizeDelta));
                    progressDifferences.Add(newDifference, desiredObjects[difference]);
                }
            }
        }

        private void WorkToImage(Image image, Sprite sprite, float scaleRatio)
        {
            image.enabled = true;
            image.sprite = sprite;
            image.SetNativeSize();
            image.GetComponent<RectTransform>().sizeDelta *= scaleRatio;
        }

        public void ClearImage()
        {
            puzzleImage.enabled = false;
            evidenceOperator.enabled = false;
        }
        public void DisableImages()
        {
            evidenceOperator.DisableImage();
        }

        internal void CheckTheEvidence(Vector2 pointOnImage)
        {
            if (!enabled)
                return;

            foreach(KeyValuePair<GameObject, Rect> difference in differences)
            {
                if (difference.Value.Contains(pointOnImage))
                {
                    StartCoroutine(ButtonAnimation(difference.Key));
                    searchObjectsOperator.ActivateDifference();
                    differences.Remove(difference.Key);
                    return;
                }
            }

            searchObjectsOperator.ErrorParticles();
        }

        public void DeleteAllDifference()
        {
            if (differences != null)
            {
                foreach (KeyValuePair<GameObject, Rect> difference in differences)
                    Destroy(difference.Key);

                differences.Clear();
            }

            GameCleaner.DeleteAllChild(puzzleImage);
        }

        private IEnumerator ButtonAnimation(GameObject difference)
        {
            float timer = 0;

            RectTransform differenceTransform = difference.transform as RectTransform;
            RectTransform parentForm = progressDifferences[difference].transform as RectTransform;
            differenceTransform.SetParent(parentForm);

            Vector2 defaultSize = differenceTransform.sizeDelta;

            differenceTransform.pivot = new Vector2(.5f, .5f);
            differenceTransform.localPosition += new Vector3(defaultSize.x/2, defaultSize.y/2);

            yield return new WaitForSeconds(differenceSlidingDelay);

            float currentTime = Time.time;
            float sizeDeltaOfEscapsedTime;

            while (timer < differenceShowingTime)
            {
                timer += Time.time - currentTime;
                currentTime = Time.time;

                sizeDeltaOfEscapsedTime = 1 + timer / differenceShowingTime;

                differenceTransform.sizeDelta = defaultSize * sizeDeltaOfEscapsedTime;
                yield return null;
            }

            yield return new WaitForSeconds(differenceSlidingDelay);

            defaultSize = differenceTransform.sizeDelta;

            timer = 0;
            currentTime = Time.time;
            
            float positionOfElapsedTime;

            differenceTransform.pivot = Vector2.zero;
            differenceTransform.localPosition -= 
                new Vector3(differenceTransform.sizeDelta.x / 2, differenceTransform.sizeDelta.y / 2);
            Vector3 defaultlocalPosition = differenceTransform.localPosition;

            Vector2 deltaSize = parentForm.sizeDelta - differenceTransform.sizeDelta;
            float escapiedTime;

            while (timer < differenceSlidingTime)
            {
                timer += Time.time - currentTime;
                currentTime = Time.time;

                escapiedTime = timer / differenceSlidingTime;
                positionOfElapsedTime = differenceSpeedCurve.Evaluate(escapiedTime);

                differenceTransform.localPosition = defaultlocalPosition * positionOfElapsedTime;
                differenceTransform.sizeDelta = defaultSize + deltaSize * escapiedTime;
                yield return null;
            }

            yield return new WaitForSeconds(differenceSlidingDelay);

            Destroy(progressDifferences[difference].gameObject);
        }
    }
}
