using FirUnityEditor;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.BossBattle
{
    public class StatsOperator : MonoBehaviour
    {
        [SerializeField]
        private BossBattleManager bossBattleOperator;

        [SerializeField]
        private int MaxHealth = 3;

        protected BattleStats stats;
        protected List<BattleStats> statsStages;

        [SerializeField, NullCheck]
        protected Slider slider;
        [SerializeField, NullCheck]
        private TextMeshProUGUI opponentName;
        [SerializeField, NullCheck]
        private Image skill;
        [SerializeField, NullCheck]
        protected Image skillBase;
        [Space]
        [SerializeField, NullCheck]
        private RectTransform bulletParent;
        public bool HasNoBullets
        {
            get => bulletParent.childCount == 0;
        }

        [SerializeField, NullCheck]
        private GameObject bulletPrefab;

        [Space]
        [SerializeField, NullCheck]
        private TextMeshProUGUI speedText;
        [SerializeField, NullCheck]
        private TextMeshProUGUI defenseText;
        [SerializeField, NullCheck]
        private TextMeshProUGUI energyText;

        private Vector2 fieldSize;

        public void SetHP(int value)
        {
            slider.maxValue = value;
            slider.value = value;
            MaxHealth = value;
        }

        public void ActivateSkill()
        {
            if (!skillBase.enabled)
                return;

            skillBase.enabled = false;
            skill.fillAmount = 0;

            for(int i = 0; i < stats.AttackCount; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab, bulletParent);
                newBullet.transform.localPosition = new Vector3(Random.value* fieldSize.x - fieldSize.x/2,
                    Random.value * fieldSize.y - fieldSize.y / 2, 0f);
                newBullet.GetComponentInChildren<MagicBulletPointerOperator>().RefreshRectOffset();
                newBullet.GetComponent<MagicBulletOperator>().SetStats(stats);
            }
        }

        public void RemoveAllBullets()
        {
            List<Transform> bulletsToDestroy = new List<Transform>();
            for(int i = 0; i < bulletParent.childCount; i++)
            {
                bulletsToDestroy.Add(bulletParent.GetChild(i));
            }
            foreach(Transform t in bulletsToDestroy)
            {
                Destroy(t.gameObject);
            }
        }

        public void RefreshFieldSize()
        {
            fieldSize = bulletParent.rect.size;
        }

        public virtual void Cooldown(bool boost)
        {
            if (skillBase.enabled)
                return;

            skill.fillAmount += 1f/stats.TimeToSkill *Time.deltaTime*(boost?3:1);
            if (skill.fillAmount >= 1)
                skillBase.enabled = true;
        }

        protected virtual void SetStatsToText()
        {
            speedText.text = ((int)(100f/stats.TimeToSkill)).ToString();
            energyText.text = stats.AttackCount.ToString();
            defenseText.text = stats.Defence.ToString();
        }

        public virtual bool Damage()
        {
            slider.value--;

            return slider.value <= 0;
        }
    }
}
