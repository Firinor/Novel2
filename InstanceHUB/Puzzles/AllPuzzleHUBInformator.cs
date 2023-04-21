using FirUnityEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class AllPuzzleHUBInformator : MonoBehaviour
    {
        [SerializeField, NullCheck]
        private GameObject victoryButton;
        [SerializeField, NullCheck]
        private GameObject failButton;
        [SerializeField, NullCheck]
        private GameObject retryButton;

        [SerializeField, NullCheck]
        private GameObject helpButtons;
        [SerializeField, NullCheck]
        private Button exitButton;
        [SerializeField, NullCheck]
        private Button optionsButton;
        [SerializeField, NullCheck]
        private Button skipButton;
        [SerializeField, NullCheck]
        private Button bookButton;

        [SerializeField, NullCheck]
        private TextMeshProUGUI timerText;

        [SerializeField, NullCheck]
        private ParticleSystem successParticleSystem;
        [SerializeField, NullCheck]
        private ParticleSystem successParticleSystem2;
        [SerializeField, NullCheck]
        private ParticleSystem errorParticleSystem;
        [SerializeField, NullCheck]
        private ParticleSystem errorParticleSystem2;


        void Awake()
        {
            AllPuzzleHUB.VictoryButtonCell.SetValue(victoryButton);
            AllPuzzleHUB.FailButtonCell.SetValue(failButton);
            AllPuzzleHUB.RetryButtonCell.SetValue(retryButton);

            AllPuzzleHUB.HelpButtonsCell.SetValue(helpButtons);
            AllPuzzleHUB.ExitButtonCell.SetValue(exitButton);
            AllPuzzleHUB.OptionsButtonCell.SetValue(optionsButton);
            AllPuzzleHUB.SkipButtonCell.SetValue(skipButton);
            AllPuzzleHUB.BookButtonCell.SetValue(bookButton);

            AllPuzzleHUB.TimerTextCell.SetValue(timerText);

            AllPuzzleHUB.SuccessParticleSystemCell.SetValue(successParticleSystem);
            AllPuzzleHUB.SuccessParticleSystem2Cell.SetValue(successParticleSystem2);
            AllPuzzleHUB.ErrorParticleSystemCell.SetValue(errorParticleSystem);
            AllPuzzleHUB.ErrorParticleSystem2Cell.SetValue(errorParticleSystem2);

            Destroy(this);
        }
    }
}
