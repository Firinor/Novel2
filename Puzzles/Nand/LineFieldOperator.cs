using FirUnityEditor;
using FirUtilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Puzzle.Nand
{
    public class LineFieldOperator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, NullCheck]
        private NandManager nandManager;

        public List<OutputOperator> outputs;

        [HideInInspector]
        public OutputOperator pickedOutput;

        private void Awake()
        {
            outputs = new List<OutputOperator>();
            var foundInputs = gameObject.GetComponentsInChildren<OutputOperator>();
            foreach (var input in foundInputs)
            {
                outputs.Add(input);
            }
        }

        public void Addinput(OutputOperator input)
        {
            outputs.Add(input);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("RecipeOperator pointer enter");
            nandManager.PointerOnField = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("RecipeOperator pointer exit");
            nandManager.PointerOnField = false;
        }
    }
}
