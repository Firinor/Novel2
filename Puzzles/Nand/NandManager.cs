using FirUnityEditor;
using UnityEngine;

namespace Puzzle.Nand
{
    public class NandManager : PuzzleOperator
    {
        [SerializeField, NullCheck]
        private NandInformator nandInformator;
        public NandInformator NandInformator { get => nandInformator; }
        [SerializeField, NullCheck]
        private LineFieldOperator fieldOperator;
        public LineFieldOperator LineFieldOperator { get => fieldOperator; }
        [SerializeField, NullCheck]
        private RectTransform nandParent;
        [SerializeField, NullCheck]
        private RectTransform newNandTransform;

        [HideInInspector]
        public bool PointerOnField;

        public NandOperator CreateNewNand()
        {
            var nand = Instantiate(nandInformator.Nand, newNandTransform);
            NandOperator nandOperator = nand.GetComponent<NandOperator>();
            //fieldOperator.AddNand(nandOperator);
            nandOperator.SetNandManager(this);
            nand.transform.SetParent(nandParent);
            return nandOperator;
        }
    }
}
