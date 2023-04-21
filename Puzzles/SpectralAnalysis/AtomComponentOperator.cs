using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.PortalBuild
{
    public class AtomComponentOperator : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textComponent;
        [SerializeField]
        private Image colorBall;
        [SerializeField]
        private Image specter;
        [SerializeField]
        private SpectralAnalysisManager portalBuildManager;

        [SerializeField]
        private AtomComponent atomComponent;
        public AtomComponent AtomComponent { get { return atomComponent; } }

        private void Awake()
        {
            Refresh();
        }

        public void SetValue(AtomComponent component, bool visualRefresh = true)
        {
            atomComponent = component;
            if(visualRefresh)
                Refresh();
        }
        public void SetManager(SpectralAnalysisManager portalBuildManager)
        {
            this.portalBuildManager = portalBuildManager;
        }

        public int GetValue()
        {
            return atomComponent.Number;
        }

        public void Refresh()
        {
            if (atomComponent == null)
            {
                atomComponent = new AtomComponent(textComponent.text, colorBall.color, specter.sprite);
                return;
            }

            textComponent.text = atomComponent.Designation;
            colorBall.color = atomComponent.Color;
            if (specter != null && atomComponent.Sprite != null)
                specter.sprite = atomComponent.Sprite;
        }

        public void OnPipette()
        {
            portalBuildManager.SelectNewComponent(this);
        }

        public void DestroyComponent()
        {
            Destroy(gameObject);
        }
    }
}