using FirInstanceCell;
using TMPro;
using UnityEngine;

namespace Puzzle 
{
    public static class AllPuzzleHUB
    {
        public static GameObject VictoryButton => VictoryButtonCell.GetValue();
        public static GameObject FailButton => FailButtonCell.GetValue();
        public static GameObject RetryButton => RetryButtonCell.GetValue();

        public static GameObject HelpButtons => HelpButtonsCell.GetValue();
        public static MonoBehaviour ExitButton => ExitButtonCell.GetValue();
        public static MonoBehaviour SkipButton => SkipButtonCell.GetValue();
        public static MonoBehaviour BookButton => BookButtonCell.GetValue();
        public static MonoBehaviour OptionsButton => OptionsButtonCell.GetValue();

        public static TextMeshProUGUI TimerText => TimerTextCell.GetValue();

        public static ParticleSystem SuccessParticleSystem => SuccessParticleSystemCell.GetValue();
        public static ParticleSystem SuccessParticleSystem2 => SuccessParticleSystem2Cell.GetValue();
        public static ParticleSystem ErrorParticleSystem => ErrorParticleSystemCell.GetValue();
        public static ParticleSystem ErrorParticleSystem2 => ErrorParticleSystem2Cell.GetValue();

        public static InstanceCell<GameObject> VictoryButtonCell = new InstanceCell<GameObject>();
        public static InstanceCell<GameObject> FailButtonCell = new InstanceCell<GameObject>();
        public static InstanceCell<GameObject> RetryButtonCell = new InstanceCell<GameObject>();

        public static InstanceCell<GameObject> HelpButtonsCell = new InstanceCell<GameObject>();
        public static InstanceCell<MonoBehaviour> SkipButtonCell = new InstanceCell<MonoBehaviour>();
        public static InstanceCell<MonoBehaviour> BookButtonCell = new InstanceCell<MonoBehaviour>();
        public static InstanceCell<MonoBehaviour> ExitButtonCell = new InstanceCell<MonoBehaviour>();
        public static InstanceCell<MonoBehaviour> OptionsButtonCell = new InstanceCell<MonoBehaviour>();

        public static InstanceCell<TextMeshProUGUI> TimerTextCell = new InstanceCell<TextMeshProUGUI>();

        public static InstanceCell<ParticleSystem> SuccessParticleSystemCell = new InstanceCell<ParticleSystem>();
        public static InstanceCell<ParticleSystem> SuccessParticleSystem2Cell = new InstanceCell<ParticleSystem>();
        public static InstanceCell<ParticleSystem> ErrorParticleSystemCell = new InstanceCell<ParticleSystem>();
        public static InstanceCell<ParticleSystem> ErrorParticleSystem2Cell = new InstanceCell<ParticleSystem>();
    }
}
