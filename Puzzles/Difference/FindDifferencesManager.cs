using FirUnityEditor;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Puzzle.FindDifferences
{
    public class FindDifferencesManager : PuzzleOperator, IOptionsSwitchHandler
    {
        #region Fields
        [SerializeField, NullCheck]
        private GameObject differencePrefab;
        [SerializeField, NullCheck]
        private ParticleSystem errorParticleSystem0;
        [SerializeField, NullCheck]
        private ParticleSystem errorParticleSystem1;
        private Dictionary<int, KeyValuePair<ParticleSystem, RectTransform>> errorParticleSystem;
        [SerializeField, NullCheck]
        private ParticleSystem successParticleSystem0;
        [SerializeField, NullCheck]
        private ParticleSystem successParticleSystem1;
        private Dictionary<int, KeyValuePair<ParticleSystem, RectTransform>> successParticleSystem;
        float offsetBetweenCursors;

        [SerializeField, NullCheck]
        private DetectiveDeskOperator detectiveDeskOperator;
        private int minimumImagePixelOffsetFromTheEdge = 30;

        [SerializeField]
        private ImageWithDifferences imageWithDifferences;
        [SerializeField]
        private int differencesCount = 5;
        private int differencesFound;

        [SerializeField, NullCheck]
        private ShakeOperator shakeOperator;
        [SerializeField, NullCheck]
        private ProgressOperator progressOperator;
        [SerializeField, NullCheck]
        private DoubleCursorOperator doubleCursorOperator;
        [SerializeField, NullCheck]
        private AnimationManager animationManager;

        private CompositeDisposable disposables;
        #endregion

        protected void Awake()
        {
            errorParticleSystem = new Dictionary<int, KeyValuePair<ParticleSystem, RectTransform>>();
            successParticleSystem = new Dictionary<int, KeyValuePair<ParticleSystem, RectTransform>>();

            errorParticleSystem.Add(0, new KeyValuePair<ParticleSystem, RectTransform>(errorParticleSystem0,
                errorParticleSystem0.GetComponent<RectTransform>()));
            errorParticleSystem.Add(1, new KeyValuePair<ParticleSystem, RectTransform>(errorParticleSystem1,
                errorParticleSystem1.GetComponent<RectTransform>()));
            successParticleSystem.Add(0, new KeyValuePair<ParticleSystem, RectTransform>(successParticleSystem0,
                successParticleSystem0.GetComponent<RectTransform>()));
            successParticleSystem.Add(1, new KeyValuePair<ParticleSystem, RectTransform>(successParticleSystem1,
                successParticleSystem1.GetComponent<RectTransform>()));
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            ClearPuzzle();
            CreateDifferenceÑounter();
            PlayStartAnimations();

            disposables = new CompositeDisposable();

            Observable.EveryUpdate()
                .Where(_ => EvidenceOperator.cursorOnImage)
                .Subscribe(_ => MoveCursor())
                .AddTo(disposables);
            Observable.EveryUpdate()
                .Where(_ => theTimerIsRunning && leftTime > 0)
                .Subscribe(_ => TimerTick())
                .AddTo(disposables);
        }
        private void CreateDifferenceÑounter()
        {
            differencesCount = Math.Min(imageWithDifferences.Differences.Length, differencesCount);
            progressOperator.CreateProgressÑounter(differencesCount);
        }

        private void PlayStartAnimations()
        {
            animationManager.PlayStart();
        }

        private void MoveCursor()
        {
            doubleCursorOperator.MoveCursor();
        }

        public void ResetOptions()
        {
            
        }

        public override void LosePuzzle()
        {
            DeactivatePuzzle();
            FailButton.SetActive(true);
        }
        public override void ClearPuzzle()
        {
            base.ClearPuzzle();
            detectiveDeskOperator.ClearImages();
            doubleCursorOperator.DisableCursors();
            DeleteAllDifference();
            differencesFound = 0;
        }
        public void DeleteAllDifference()
        {
            detectiveDeskOperator.DeleteAllDifference();
        }
        public void SetPuzzleInformationPackage(ImageWithDifferencesPackage puzzleInformationPackage)
        {
            imageWithDifferences = puzzleInformationPackage.ImageWithDifferences;
            differencesCount = puzzleInformationPackage.DifferenceCount;
            leftTime = puzzleInformationPackage.AllottedTime;
            SetVictoryEvent(puzzleInformationPackage.successPuzzleAction);
            SetFailEvent(puzzleInformationPackage.failedPuzzleAction);
            SetBackground(puzzleInformationPackage.PuzzleBackground);
        }
        public override void StartPuzzle()
        {
            detectiveDeskOperator.enabled = true;
            detectiveDeskOperator.DisableButton();
            detectiveDeskOperator.CreateImages(imageWithDifferences, differencesCount,
                differencePrefab, minimumImagePixelOffsetFromTheEdge, out offsetBetweenCursors);
            doubleCursorOperator.SetOffset(offsetBetweenCursors);
            theTimerIsRunning = leftTime > 0;
        }
        public override void SuccessfullySolvePuzzle()
        {
            DeactivatePuzzle();
            VictoryButton.SetActive(true);
        }
        protected override void DeactivatePuzzle()
        {
            theTimerIsRunning = false;
            detectiveDeskOperator.DisableImages();
            detectiveDeskOperator.enabled = false;
            doubleCursorOperator.DisableCursors();
        }
        public void ActivateDifference(GameObject keyDifference, CursorOnEvidence cursorOnEvidence)
        {
            Particles(true, cursorOnEvidence);

            differencesFound++;
            progressOperator.AddProgress();

            if (differencesFound == differencesCount)
            {
                SuccessfullySolvePuzzle();
            }
        }

        public void Particles(bool success, CursorOnEvidence cursorOnEvidence)
        {
            Particles(success, Input.mousePosition/CanvasManager.ScaleFactor, cursorOnEvidence);
        }
        public void Particles(bool success, Vector3 position, CursorOnEvidence cursorOnEvidence)
        {
            Dictionary<int, KeyValuePair<ParticleSystem, RectTransform>> particleSystem
                = success ? successParticleSystem : errorParticleSystem;

            particleSystem[0].Value.localPosition = position;
            particleSystem[1].Value.localPosition = position 
                + new Vector3(offsetBetweenCursors * -(int)cursorOnEvidence, 0,0);

            particleSystem[0].Key.Play();
            particleSystem[1].Key.Play();
        }

        public void SetCursorOnEvidence(CursorOnEvidence cursorOnEvidence)
        {
            doubleCursorOperator.cursorOnEvidence = cursorOnEvidence;
        }

        public void EnableCursors()
        {
            doubleCursorOperator.EnableCursors();
        }

        public void DisableCursors()
        {
            doubleCursorOperator.DisableCursors();
        }
        public override void PuzzleExit()
        {
            Cursor.visible = true;
            background.enabled = false;
            gameObject.SetActive(false);
        }

        public void ErrorShake(CursorOnEvidence cursorOnEvidence)
        {
            Particles(false, cursorOnEvidence);
            shakeOperator.SetErrorImpulse();
        }
    }
}
