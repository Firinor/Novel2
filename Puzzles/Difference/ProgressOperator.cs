using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.FindDifferences
{
    public class ProgressOperator : MonoBehaviour
    {
        [SerializeField]
        private Transform progressParent;
        [SerializeField]
        private GameObject indicatorPrefab;

        [SerializeField]
        private Color offIndicatorColor;
        [SerializeField]
        private Color onIndicatorColor;

        private GameObject[] indicators;
        private int counter = 0;
        private int сounterGoal = 0;

        public void ClearProgressСounter()
        {
            if (indicators != null)
            {
                foreach (GameObject indicator in indicators)
                    Destroy(indicator);

                indicators = null;
                counter = 0;
            }

            int childCount = progressParent.childCount;
            while (childCount > 0)
            {
                childCount--;
                Destroy(progressParent.GetChild(childCount).gameObject);
            }
        }

        public void CreateProgressСounter(int count)
        {
            ClearProgressСounter();

            indicators = new GameObject[count];
            сounterGoal = count;
            for (int i = 0; i < count; i++)
            {
                GameObject newIndicator = Instantiate(indicatorPrefab, progressParent);
                indicators[i] = newIndicator;
            }
        }

        public void AddProgress()
        {
            if(counter < сounterGoal)
            {
                indicators[counter].GetComponent<Image>().color = onIndicatorColor;
                counter++;
            }
        }
    }
}
