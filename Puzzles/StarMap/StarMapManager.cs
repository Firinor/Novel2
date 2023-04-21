using FirUnityEditor;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.StarMap
{
    public enum Hemisphere { Northern, Southern, Winter, Spring, Summer, Autumn}

    public class StarMapManager : PuzzleOperator
    {
        #region Fields
        [SerializeField, NullCheck]
        private GameObject targetConstellation;
        [SerializeField, NullCheck]
        private GameObject starMap;
        [SerializeField, NullCheck]
        private GameObject nebula;
        [SerializeField, NullCheck]
        private GameObject helpMap;
        [SerializeField, NullCheck]
        private TargetOperator targetOperator;
        [SerializeField, NullCheck]
        private Image targetImage;
        [SerializeField, NullCheck]
        private GlassBallViewOperator glassBallViewOperator;
        [SerializeField, NullCheck]
        private StarMapInGlassBallOperator starMapInGlassBallOperator;
        [SerializeField, NullCheck]
        private StarMapInformator starMapInformator;

        [SerializeField, Range(0, 2)]
        private int difficulty = 1;

        private CompositeDisposable disposables;
        #endregion

        protected override void OnEnable()
        {
            base.OnEnable();
            ClearPuzzle();

            disposables = new CompositeDisposable();

            Observable.EveryUpdate()
                .Where(_ => theTimerIsRunning && leftTime > 0)
                .Subscribe(_ => TimerTick())
                .AddTo(disposables);

            StartPuzzle();
        }
        public override void LosePuzzle()
        {
            DeactivatePuzzle();
            FailButton.SetActive(true);
        }
        public override void ClearPuzzle()
        {
            base.ClearPuzzle();
            ResetPointerAndButton();
        }
        public void CheckAnswer()
        {
            glassBallViewOperator.ActivePuzzle = false;
            targetImage.enabled = true;
            Vector2Int point = starMapInGlassBallOperator.GetCursorPoint();
            Color answer = targetImage.sprite.texture.GetPixel(point.x, point.y);
            if(answer.a > 0.4f)
            {
                SuccessfullySolvePuzzle();
            }
            else
            {
                LosePuzzle();
            }
        }
        private void ResetPointerAndButton()
        {
            targetOperator.SetButtonActivity(false);
            starMapInGlassBallOperator.SetTargetActivity(false);
            starMapInGlassBallOperator.SetCursorActivity(false);
        }
        public void SetButtonActivity()
        {
            targetOperator.SetButtonActivity(true);
        }
        public void SetPuzzleInformationPackage(StarMapPackage spatMapPackage)
        {
            leftTime = spatMapPackage.AllottedTime;
            difficulty = spatMapPackage.Difficulty;
            SetNewConstellation();
            SetVictoryEvent(spatMapPackage.successPuzzleAction);
            SetFailEvent(spatMapPackage.failedPuzzleAction);
            SetBackground(spatMapPackage.PuzzleBackground);
        }

        private void SetNewConstellation()
        {
            Hemisphere hemisphere = starMapInformator.ChoseHemisphere();
            StarMapInformator.ConstellationsVariant.AnswerSprite answerSprite = starMapInformator.ChoseAnswerSprite(hemisphere);
            SetStarMapPuzzleContent(hemisphere);
            SetStarMapAnswerSprite(answerSprite.sprite);
            SetStarMapKeySprite(answerSprite.constellation);
        }

        private void SetStarMapKeySprite(Constellation constellation)
        {
            Sprite sprite = starMapInformator[constellation];
            targetOperator.SetAnswerKey(sprite);
        }

        private void SetStarMapAnswerSprite(Sprite sprite)
        {
            starMapInGlassBallOperator.SetAnswerSprite(sprite);
        }

        private void SetStarMapPuzzleContent(Hemisphere hemisphere)
        {
            Sprite sprite = starMapInformator[hemisphere].HemispherePuzzleSprite[difficulty];

            bool isEasy = difficulty == 0;
            starMapInGlassBallOperator.SetPuzzleSprite(sprite, isEasy);

            helpMap.GetComponent<HelpMapOperator>().SetListOfPagesActive(isEasy, hemisphere);
        }

        public override void StartPuzzle()
        {
            //keyOperator.CreateHint();
            theTimerIsRunning = leftTime > 0;
            glassBallViewOperator.ActivePuzzle = true;
        }
        public override void SuccessfullySolvePuzzle()
        {
            DeactivatePuzzle();
            VictoryButton.SetActive(true);
        }
        protected override void DeactivatePuzzle()
        {
            theTimerIsRunning = false;
        }

        public override void PuzzleExit()
        {
            OpenStarMap();
            background.enabled = false;
            gameObject.SetActive(false);
        }

        public override void OpenHelpBook()
        {
            if (!helpMap.activeSelf)
            {
                EnabledPuzzle(false);
                EnabledHelpMap(true);
            }
        }
        public void OpenStarMap()
        {
            EnabledHelpMap(false);
            EnabledPuzzle(true);
        }
        private void EnabledHelpMap(bool v)
        {
            helpMap.SetActive(v);
        }

        private void EnabledPuzzle(bool v)
        {
            targetConstellation.SetActive(v);
            starMap.SetActive(v);
            nebula.SetActive(v);
        }
    }
}
