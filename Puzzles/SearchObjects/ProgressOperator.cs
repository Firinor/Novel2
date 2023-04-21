using UnityEngine;

namespace Puzzle.SearchObjects
{
    public class ProgressOperator : MonoBehaviour
    {
        [SerializeField]
        private SearchObjectsManager puzzleOperator;
        [SerializeField]
        private Transform objectsParent;
        public Transform ObjectsParent { get => objectsParent; }

        private ObjectToSearchOperator[] objectsToSearch;
        private int ingredientCount;

        internal void SetObjects(ObjectToSearchOperator[] objectsToSearch)
        {
            this.objectsToSearch = objectsToSearch;
            ingredientCount = objectsToSearch.Length;
        }
    }
}
