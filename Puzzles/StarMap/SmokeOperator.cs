using UnityEngine;

namespace Puzzle.StarMap
{
    public class SmokeOperator : MonoBehaviour
    {
        public void OnAmimationFinish()
        {
            gameObject.SetActive(false);
        }
    }
}
