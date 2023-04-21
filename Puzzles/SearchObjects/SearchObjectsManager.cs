using FirCleaner;
using FirMath;
using FirUnityEditor;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Puzzle.SearchObjects
{
    public class SearchObjectsManager : PuzzleOperator
    {
        #region Fields
        [SerializeField, NullCheck]
        private GameObject searchObjectsPrefab;
        [SerializeField, NullCheck]
        private ParticleSystem errorParticleSystem;
        [SerializeField, NullCheck]
        private ParticleSystem successParticleSystem;

        [SerializeField, NullCheck]
        private DetectiveDeskOperator detectiveDeskOperator;

        [SerializeField]
        private ImageWithDifferences imageWithDifferences;
        [SerializeField]
        private int differencesCount = 5;
        [SerializeField]
        private int trashCount = 10;
        private int differencesFound;

        private List<int> trashObjects;
        private Dictionary<int, ObjectToSearchOperator> desiredObjects;
        private ObjectToSearchOperator[] desiredObjectsToSearch
        {
            get
            {
                if (desiredObjects == null)
                    return null;

                var result = new ObjectToSearchOperator[desiredObjects.Count];
                desiredObjects.Values.CopyTo(result, 0);
                return result;
            }
        }

        [SerializeField, NullCheck]
        private ProgressOperator progressOperator;
        [SerializeField, NullCheck]
        private AnimationManager animationManager;

        private CompositeDisposable disposables;
        #endregion

        protected override void OnEnable()
        {
            base.OnEnable();

            ClearPuzzle();
            SetRetryEvent(RetryPuzzle);

            CreateNewObjectsToSearch();
            PlayStartAnimations();

            disposables = new CompositeDisposable();

            Observable.EveryUpdate()
                .Where(_ => theTimerIsRunning && leftTime > 0)
                .Subscribe(_ => TimerTick())
                .AddTo(disposables);
        }

        public override void RetryPuzzle()
        {
            CreateNewObjectsToSearch();
        }

        private void CreateNewObjectsToSearch()
        {
            trashObjects = GenerateNewTrashList(trashCount, imageWithDifferences.Differences.Length);
            List<int> desiredObjectsInts = GenerateNewDesiredList(differencesCount, trashObjects);
            desiredObjects = new Dictionary<int, ObjectToSearchOperator>();

            foreach (int i in desiredObjectsInts)
            {
                ObjectToSearchOperator newObjectToSearch
                    = Instantiate(searchObjectsPrefab, progressOperator.ObjectsParent)
                    .GetComponent<ObjectToSearchOperator>();
                newObjectToSearch.SetRecipeSprite(imageWithDifferences.Differences[i].sprite);
                desiredObjects.Add(i, newObjectToSearch);
            }
            progressOperator.SetObjects(desiredObjectsToSearch);
        }

        private List<int> GenerateNewTrashList(int count, int length)
        {
            return GameMath.AFewCardsFromTheDeck(count, length);
        }
        private List<int> GenerateNewDesiredList(int count, List<int> trashObjects)
        {
            List<int> desiredList = GameMath.AFewCardsFromTheDeck(count, trashObjects.Count);
            List<int> result = new List<int>();
            for (int i = 0; i< count; i++)
            {
                result.Add(trashObjects[desiredList[i]]);
            }

            return result;
        }

        private void DeleteAllInProgressList(ObjectToSearchOperator[] ingredientsList)
        {
            if (ingredientsList != null)
            {
                foreach (ObjectToSearchOperator ingredient in ingredientsList)
                    Destroy(ingredient.gameObject);

                desiredObjects.Clear();
            }
            GameCleaner.DeleteAllChild(progressOperator.ObjectsParent);
        }

        private void PlayStartAnimations()
        {
            animationManager.PlayStart();
        }
        public override void ClearPuzzle()
        {
            base.ClearPuzzle();
            detectiveDeskOperator.ClearImage();
            DeleteAllDifference();
            differencesFound = 0;
            DeleteAllInProgressList(desiredObjectsToSearch);
        }
        public void DeleteAllDifference()
        {
            detectiveDeskOperator.DeleteAllDifference();
        }
        public void SetPuzzleInformationPackage(ImageWithDifferencesPackage puzzleInformationPackage)
        {
            imageWithDifferences = puzzleInformationPackage.ImageWithDifferences;
            differencesCount = puzzleInformationPackage.DifferenceCount;
            trashCount = puzzleInformationPackage.TrashCount;
            leftTime = puzzleInformationPackage.AllottedTime;
            SetVictoryEvent(puzzleInformationPackage.successPuzzleAction);
            SetFailEvent(puzzleInformationPackage.failedPuzzleAction);
            SetBackground(puzzleInformationPackage.PuzzleBackground);
        }
        public override void StartPuzzle()
        {
            detectiveDeskOperator.enabled = true;
            detectiveDeskOperator.DisableButton();
            detectiveDeskOperator.CreateImage(imageWithDifferences, trashObjects, desiredObjects,
                searchObjectsPrefab);
            theTimerIsRunning = leftTime > 0;
        }
        protected override void DeactivatePuzzle()
        {
            theTimerIsRunning = false;
            detectiveDeskOperator.DisableImages();
            detectiveDeskOperator.enabled = false;
        }
        public void ActivateDifference()
        {
            Particles(true);

            differencesFound++;

            if (differencesFound == differencesCount)
            {
                SuccessfullySolvePuzzle();
            }
        }

        public void Particles(bool success)
        {
            Particles(success, Input.mousePosition);
        }
        public void Particles(bool success, Vector3 position)
        {
            ParticleSystem particleSystem = success ? successParticleSystem : errorParticleSystem;

            particleSystem.transform.localPosition = position;

            particleSystem.Play();
        }

        public override void PuzzleExit()
        {
            Cursor.visible = true;
            background.enabled = false;
            gameObject.SetActive(false);
        }

        public void ErrorParticles()
        {
            Particles(false);
        }
    }
}
