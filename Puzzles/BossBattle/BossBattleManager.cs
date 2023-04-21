using FirUnityEditor;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.BossBattle
{
    public class BossBattleManager : PuzzleOperator
    {
        [SerializeField, NullCheck]
        private BossStatsOperator bossStatsOperator;
        [SerializeField, NullCheck]
        private HeroStatsOperator heroStatsOperator;

        [Space]
        [SerializeField, NullCheck]
        private Image redScreen;
        [SerializeField, NullCheck]
        private Image bossImage;
        [SerializeField, Range(0,1)]
        private float redAlfaCeiling;
        [SerializeField]
        private float redAlfaFillingTime;
        [SerializeField]
        private float redAlfaDecreasingTime;

        [Space]
        [SerializeField]
        private BossBattlePackage bossBattlePackage;

        public void SetPuzzleInformationPackage(BossBattlePackage bossBattlePackage)
        {
            this.bossBattlePackage = bossBattlePackage;
            SetAllStats(bossBattlePackage);
            SetVictoryEvent(bossBattlePackage.successPuzzleAction);
            SetFailEvent(bossBattlePackage.failedPuzzleAction);
            SetBackground(bossBattlePackage.PuzzleBackground);
        }

        private void SetAllStats(BossBattlePackage bossBattlePackage)
        {
            CharacterInformator boss = bossBattlePackage.BossCharacter;
            bossImage.sprite = boss.UnitSprite;
            float coefficient = GetComponent<RectTransform>().rect.height / bossImage.preferredHeight;
            bossImage.transform.localScale = Vector3.one * coefficient * boss.UnitScale;

            bossStatsOperator.SetStats(bossBattlePackage.Boss);
            bossStatsOperator.RefreshFieldSize();
            heroStatsOperator.SetStats(bossBattlePackage.Hero);
            heroStatsOperator.RefreshFieldSize();

            MagicBulletPointerOperator.SetFullRect(GetComponent<RectTransform>());
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetAllStats(bossBattlePackage);
        }

        void Update()
        {
            bool boost = bossStatsOperator.HasNoBullets && heroStatsOperator.HasNoBullets;
            bossStatsOperator.Cooldown(boost);
            heroStatsOperator.Cooldown(boost);
        }

        public override void StartPuzzle()
        {
            enabled = true;
        }

        public override void ClearPuzzle()
        {
            base.ClearPuzzle();
            enabled = false;
        }

        private IEnumerator RedScreen()
        {
            redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, 0f);
            float timer;
            float startTime = Time.time;
            float deltaOfEscapsedTime;
            while (redScreen.color.a < redAlfaCeiling)
            {
                timer = Time.time - startTime;
                deltaOfEscapsedTime = timer / redAlfaFillingTime;

                redScreen.color = 
                    new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, deltaOfEscapsedTime);
                yield return null;
            }

            startTime = Time.time;

            while (redScreen.color.a > 0f)
            {
                timer = Time.time - startTime;
                deltaOfEscapsedTime = redAlfaCeiling - timer * redAlfaCeiling / redAlfaDecreasingTime;

                redScreen.color =
                    new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, deltaOfEscapsedTime);
                yield return null;
            }
            redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, 0f);
        }
        private IEnumerator PaintBossRed()
        {
            bossImage.color = Color.white;
            float timer;
            float startTime = Time.time;
            float deltaOfColor;
            float colorRemnant = 1 - redAlfaCeiling;
            while (bossImage.color.g > colorRemnant)
            {
                timer = Time.time - startTime;
                deltaOfColor = colorRemnant + redAlfaCeiling * (1 - timer / redAlfaFillingTime);

                bossImage.color =
                    new Color(bossImage.color.r, deltaOfColor, deltaOfColor, 1f);
                yield return null;
            }

            startTime = Time.time;

            while (bossImage.color.g < 1f)
            {
                timer = Time.time - startTime;
                deltaOfColor = colorRemnant + redAlfaCeiling * (timer / redAlfaDecreasingTime);

                bossImage.color =
                    new Color(bossImage.color.r, deltaOfColor, deltaOfColor, 1);
                yield return null;
            }
            bossImage.color = Color.white;
        }

        internal void DamagePlayer()
        {
            StopCoroutine(RedScreen());
            StartCoroutine(RedScreen());

            bool isDead = heroStatsOperator.Damage();

            if (isDead)
            {
                LosePuzzle();
            }
        }

        internal void DamageBoss()
        {
            StopCoroutine(PaintBossRed());
            StartCoroutine(PaintBossRed());

            bool isDead = bossStatsOperator.Damage();

            if (isDead)
            {
                SuccessfullySolvePuzzle();
            }
        }

        public override void SuccessfullySolvePuzzle()
        {
            DeactivatePuzzle();
            VictoryButton.SetActive(true);
        }


        protected override void DeactivatePuzzle()
        {
            theTimerIsRunning = false;
            enabled = false;
            bossStatsOperator.RemoveAllBullets();
            heroStatsOperator.RemoveAllBullets();
        }

        public override void LosePuzzle()
        {
            DeactivatePuzzle();
            FailButton.SetActive(true);
        }
    }
}
