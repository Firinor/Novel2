using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Puzzle
{
    public abstract class PuzzleOperator : MonoBehaviour
    {
        [Inject]
        public Image background;

        [SerializeField]
        protected float leftTime = 120;
        protected bool theTimerIsRunning;

        [Inject]
        public GameObject HelpButtons;
        [Inject]
        private Button exitButton;
        [Inject]
        private Button optionsButton;
        [Inject]
        private Button skipButton;
        [Inject]
        private Button bookButton;
        [Inject]
        public GameObject FailButton;
        [Inject]
        public GameObject VictoryButton;
        [Inject]
        public GameObject RetryButton;

        [Inject]
        public TextMeshProUGUI TimerText;

        [Inject]
        public PuzzleManager puzzleManager;

        protected virtual void OnEnable()
        {
            PreparePuzzle();
        }

        private void PreparePuzzle()
        {
            HelpButtons.SetActive(true);
            skipButton.onClick.RemoveAllListeners();
            skipButton.onClick.AddListener(SuccessfullySolvePuzzle);
            bookButton.onClick.RemoveAllListeners();
            bookButton.onClick.AddListener(OpenHelpBook);
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(PuzzleExit);
            optionsButton.onClick.RemoveAllListeners();
            optionsButton.onClick.AddListener(Options);
            SetRetryEvent();
            ResetTimer();
        }

        public virtual void PuzzleExit()
        {
            DeactivaButtons();
            background.enabled = false;
            gameObject.SetActive(false);
        }
        public virtual void LosePuzzle()
        {
            DeactivatePuzzle();
            FailButton.SetActive(true);
            RetryButton.SetActive(true);
        }
        public virtual void Options()
        {
            puzzleManager.Options();
        }
        public virtual void OpenHelpBook()
        {
            
        }
        public virtual void ClearPuzzle()
        {
            DeactivaButtons();
            TimerText.enabled = false;
        }

        private void DeactivaButtons()
        {
            VictoryButton.SetActive(false);
            FailButton.SetActive(false);
            RetryButton.SetActive(false);
        }
        public virtual void StartPuzzle()
        {

        }

        public virtual void RetryPuzzle()
        {
            ClearPuzzle();
            ResetTimer();
            StartPuzzle();
        }

        public virtual void SuccessfullySolvePuzzle()
        {
            DeactivatePuzzle();
            VictoryButton.SetActive(true);
            RetryButton.SetActive(true);
        }

        protected virtual void DeactivatePuzzle()
        {
            
        }

        protected virtual void SetVictoryEvent(UnityAction victoryAction)
        {
            Button button = VictoryButton.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(PuzzleExit);
            if(victoryAction != null)
                button.onClick.AddListener(victoryAction);
        }
        protected virtual void SetFailEvent(UnityAction failAction)
        {
            Button button = FailButton.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(PuzzleExit);
            if (failAction != null)
                button.onClick.AddListener(failAction);
        }
        protected virtual void SetRetryEvent(UnityAction retryAction = null)
        {
            Button button = RetryButton.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(RetryPuzzle);
            if (retryAction != null)
                button.onClick.AddListener(retryAction);
        }
        protected virtual void SetBackground(Sprite sprite)
        {
            background.enabled = true;
            background.sprite = sprite;
        }

        protected virtual void TimerTick()
        {
            leftTime -= Time.deltaTime;
            TextLeftTime();
            if (leftTime <= 0)
            {
                theTimerIsRunning = false;
                LosePuzzle();
            }
        }

        protected virtual void TextLeftTime()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(leftTime);
            DateTime dateTime = new DateTime(1, 1, 1, 0, timeSpan.Minutes, timeSpan.Seconds);
            TimerText.text = $"{dateTime:m:ss}";
        }

        protected virtual void ResetTimer()
        {
            theTimerIsRunning = false;
            bool leftSomeTime = leftTime > 0;
            TimerText.enabled = leftSomeTime;
            if (leftSomeTime)
                TextLeftTime();
        }
    }
}