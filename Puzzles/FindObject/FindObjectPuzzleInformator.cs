using FirUnityEditor;
using UnityEngine;

namespace Puzzle.FindObject
{
    public class FindObjectPuzzleInformator : MonoBehaviour
    {
        [SerializeField, NullCheck]
        private Sprite closedAlchemicalBox;
        public Sprite ClosedAlchemicalBox { get => closedAlchemicalBox; }

        [SerializeField, NullCheck]
        private Sprite openAlchemicalBox;
        public Sprite OpenAlchemicalBox { get => openAlchemicalBox; }

        [SerializeField]
        private RectTransform recipe;
    }
}
