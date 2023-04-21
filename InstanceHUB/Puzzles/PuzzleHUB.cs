using FirInstanceCell;
using UnityEngine;

namespace Puzzle
{
    public static class PuzzleHUB
    {
        public static GameObject FindObject => FindObjectCell.GetValue();
        public static GameObject TetraQuestion => TetraQuestionCell.GetValue();
        public static GameObject FindDifference => FindDifferenceCell.GetValue();
        public static GameObject SearchObjects => SearchObjectsCell.GetValue();
        public static GameObject SpectralAnalysis => SpectralAnalysisCell.GetValue();
        public static GameObject StarMap => StarMapCell.GetValue();
        public static GameObject BossBattle => BossBattleCell.GetValue();
        public static GameObject[] GetNotNullPuzzles()
        {
            GameObject[] accumulator = new GameObject[7];
            int count = 0;

            if (!FindObjectCell.isValueNull())
            {
                accumulator[count] = FindObjectCell.GetValue();
                count++;
            }
            if (!TetraQuestionCell.isValueNull())
            {
                accumulator[count] = TetraQuestionCell.GetValue();
                count++;
            }
            if (!FindDifferenceCell.isValueNull())
            {
                accumulator[count] = FindDifferenceCell.GetValue();
                count++;
            }
            if (!SearchObjectsCell.isValueNull())
            {
                accumulator[count] = SearchObjectsCell.GetValue();
                count++;
            }
            if (!SpectralAnalysisCell.isValueNull())
            {
                accumulator[count] = SpectralAnalysisCell.GetValue();
                count++;
            }
            if (!StarMapCell.isValueNull())
            {
                accumulator[count] = StarMapCell.GetValue();
                count++;
            }
            if (!BossBattleCell.isValueNull())
            {
                accumulator[count] = BossBattleCell.GetValue();
                count++;
            }

            GameObject[] result = new GameObject[count];
            for(int i = 0; i < count; i++)
            {
                result[i] = accumulator[i];
            }

            return result;
        }

        public static MonoBehaviour FindObjectManager => FindObjectManagerCell.GetValue();
        public static MonoBehaviour TetraQuestionManager => TetraQuestionManagerCell.GetValue();
        public static MonoBehaviour FindDifferencesManager => FindDifferencesManagerCell.GetValue();
        public static MonoBehaviour SearchObjectsManager => SearchObjectsManagerCell.GetValue();
        public static MonoBehaviour SpectralAnalysisManager => SpectralAnalysisManagerCell.GetValue();
        public static MonoBehaviour StarMapManager => StarMapManagerCell.GetValue();
        public static MonoBehaviour BossBattleManager => BossBattleManagerCell.GetValue();

        public static InstanceCell<GameObject> FindObjectCell = new InstanceCell<GameObject>();
        public static InstanceCell<GameObject> TetraQuestionCell = new InstanceCell<GameObject>();
        public static InstanceCell<GameObject> FindDifferenceCell = new InstanceCell<GameObject>();
        public static InstanceCell<GameObject> SearchObjectsCell = new InstanceCell<GameObject>();
        public static InstanceCell<GameObject> SpectralAnalysisCell = new InstanceCell<GameObject>();
        public static InstanceCell<GameObject> StarMapCell = new InstanceCell<GameObject>();
        public static InstanceCell<GameObject> BossBattleCell = new InstanceCell<GameObject>();
        public static InstanceCell<MonoBehaviour> FindObjectManagerCell = new InstanceCell<MonoBehaviour>();
        public static InstanceCell<MonoBehaviour> TetraQuestionManagerCell = new InstanceCell<MonoBehaviour>();
        public static InstanceCell<MonoBehaviour> FindDifferencesManagerCell = new InstanceCell<MonoBehaviour>();
        public static InstanceCell<MonoBehaviour> SearchObjectsManagerCell = new InstanceCell<MonoBehaviour>();
        public static InstanceCell<MonoBehaviour> SpectralAnalysisManagerCell = new InstanceCell<MonoBehaviour>();
        public static InstanceCell<MonoBehaviour> StarMapManagerCell = new InstanceCell<MonoBehaviour>();
        public static InstanceCell<MonoBehaviour> BossBattleManagerCell = new InstanceCell<MonoBehaviour>();
    }
}
