using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Puzzle.FindObject
{
    public class RecipeOperator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private FindObjectManager puzzleOperator;
        [SerializeField]
        private Image image;
        [SerializeField]
        private Color grey;
        private List<AlchemicalIngredientOperator> recipe;
        private int ingredientCount;

        void Awake()
        {
            image.color = grey;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("RecipeOperator pointer enter");
            image.color = Color.white;
            puzzleOperator.PointerOnRecipe = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("RecipeOperator pointer exit");
            image.color = grey;
            puzzleOperator.PointerOnRecipe = false;
        }
        internal void SetResipe(List<AlchemicalIngredientOperator> recipe)
        {
            this.recipe = recipe;
            ingredientCount = recipe.Count;
        }
        internal bool ActivateIngredient(int keyIngredientNumber)
        {
            AlchemicalIngredientOperator blackIngredient = recipe[keyIngredientNumber - 1];
            blackIngredient.Success();

            //recipe.Remove(blackIngredient);
            ingredientCount--;
            return ingredientCount == 0;
        }
    }
}
