using System;
using UnityEngine;
using UnityEngine.UI;
using FirMath;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirUnityEditor;

namespace Puzzle.FindObject
{
    public class FindObjectManager : PuzzleOperator, IOptionsSwitchHandler
    {
        #region Fields
        [SerializeField, NullCheck]
        private FindObjectPuzzleInformator puzzleInformator;

        [SerializeField]
        private float ingredientFrictionBraking;
        [SerializeField]
        private float ingredientBorder;
        private float recipeOffset;

        [SerializeField, NullCheck]
        private Image box;
        [SerializeField, NullCheck]
        private GameObject ingredientPrefab;

        [SerializeField, NullCheck]
        private Transform ingredientParent;
        private List<AlchemicalIngredientOperator> allIngredients;
        [SerializeField, NullCheck]
        private RectTransform recipeParent;
        [SerializeField, NullCheck]
        private RecipeOperator recipeOperator;
        [SerializeField]
        private Sprite[] alchemicalIngredientsSprites;
        [SerializeField]
        private int recipeIngredientCount = 5;
        [SerializeField]
        private int ingredientInBoxCount = 250;
        [SerializeField]
        private float forseToIngredient;
        private float acceleration = 0.025f;
        public float ForseToIngredient { get => forseToIngredient; }
        public ParticleSystem successParticleSystem { get => AllPuzzleHUB.SuccessParticleSystem; }
        public ParticleSystem errorParticleSystem { get => AllPuzzleHUB.ErrorParticleSystem; }

        [HideInInspector]
        public bool PointerOnRecipe;

        private List<int> recipe;
        private List<AlchemicalIngredientOperator> recipeList;

        [SerializeField, NullCheck]
        private AnimationManager animationManager;
        #endregion

        protected void Awake()
        {
            ResetOptions();

            if (puzzleInformator == null)
            {
                puzzleInformator = GetComponent<FindObjectPuzzleInformator>();
            }
        }

        public void ResetOptions()
        {
            float screenOffset = CanvasManager.ScreenHeight / 2;
            recipeOffset = recipeParent.sizeDelta.y - screenOffset;
        }

        public override void RetryPuzzle()
        {
            OnEnable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ClearPuzzle();
            SetRetryEvent(RetryPuzzle);
            CreateNewRecipe();
            PlayStartAnimations();
        }

        private void PlayStartAnimations()
        {
            animationManager.PlayStart();
        }

        void Update()
        {
            if (theTimerIsRunning && leftTime > 0)
            {
                leftTime -= Time.deltaTime;
                TextLeftTime();
                if (leftTime <= 0)
                {
                    theTimerIsRunning = false;
                    LosePuzzle();
                }
            }
        }

        public override void LosePuzzle()
        {
            DeleteIngredientsInList(allIngredients);
            base.LosePuzzle();
        }
        public override void ClearPuzzle()
        {
            base.ClearPuzzle();
            CloseBox();
            box.GetComponent<Button>().enabled = true;
            DeleteAllIngredients();
        }
        private void DeleteAllIngredients()
        {
            DeleteIngredientsInList(recipeList);
            DeleteIngredientsInList(allIngredients);

        }
        private void DeleteIngredientsInList(List<AlchemicalIngredientOperator> ingredientsList)
        {
            if (ingredientsList != null)
            {
                foreach (AlchemicalIngredientOperator ingredient in ingredientsList)
                    Destroy(ingredient.gameObject);

                ingredientsList.Clear();
            }
        }

        private void CreateNewRecipe()
        {
            DeleteIngredientsInList(recipeList);

            ingredientInBoxCount = Math.Min(ingredientInBoxCount, alchemicalIngredientsSprites.Length);

            recipe = GenerateNewRecipe(recipeIngredientCount, ingredientInBoxCount);
            recipeList = new List<AlchemicalIngredientOperator>();

            foreach (int i in recipe)
            {
                AlchemicalIngredientOperator newRecipeIngridient
                    = Instantiate(ingredientPrefab, recipeParent)
                    .GetComponent<AlchemicalIngredientOperator>();
                newRecipeIngridient.SetRecipeSprite(alchemicalIngredientsSprites[i]);
                recipeList.Add(newRecipeIngridient);
            }
            recipeOperator.SetResipe(recipeList);
        }
        public void SetPuzzleInformationPackage(FindRecipeIngredientsPackage puzzleInformationPackage)
        {
            alchemicalIngredientsSprites = puzzleInformationPackage.Ingredients;
            recipeIngredientCount = puzzleInformationPackage.RecipeDifficulty;
            ingredientInBoxCount = puzzleInformationPackage.IngredientsCount;
            leftTime = puzzleInformationPackage.AllottedTime;
            SetVictoryEvent(puzzleInformationPackage.successPuzzleAction);
            SetFailEvent(puzzleInformationPackage.failedPuzzleAction);
            SetBackground(puzzleInformationPackage.PuzzleBackground);
        }

        public override void StartPuzzle()
        {
            OpenBox();
            theTimerIsRunning = leftTime > 0;

            allIngredients = new List<AlchemicalIngredientOperator>();

            for (int i = 0; i < ingredientInBoxCount; i++)
            {
                AlchemicalIngredientOperator newIngridient
                    = Instantiate(ingredientPrefab, ingredientParent)
                    .GetComponent<AlchemicalIngredientOperator>();
                allIngredients.Add(newIngridient);
                newIngridient.SetSprite(alchemicalIngredientsSprites[i]);
                newIngridient.SetRandomImpulse(forseToIngredient);
                if (recipe.Contains(i))
                {
                    newIngridient.AddToRecipe(recipe.IndexOf(i) + 1);
                }
            }
        }

        public override async void SuccessfullySolvePuzzle()
        {
            await HarvestAllIngredients();
            CloseBox();

            await Task.Delay(500);
            base.SuccessfullySolvePuzzle();
        }

        private void CloseBox()
        {
            box.sprite = puzzleInformator.ClosedAlchemicalBox;
            box.SetNativeSize();
        }
        private void OpenBox()
        {
            box.sprite = puzzleInformator.OpenAlchemicalBox;
            box.SetNativeSize();
            box.GetComponent<Button>().enabled = false;
        }

        private async Task HarvestAllIngredients()
        {
            float border = 0;
            float force = 0;

            List<AlchemicalIngredientOperator> ingredientsToDestroy = new List<AlchemicalIngredientOperator>();

            while (allIngredients != null && allIngredients.Count > 0)
            {
                for(int i = 0; i < 30 && i < allIngredients.Count; i++)
                {
                    if (allIngredients[i].OnBox(border))
                    {
                        allIngredients[i].SetImpulse(0, toZeroPoint: true);
                        ingredientsToDestroy.Add(allIngredients[i]);
                    }
                    else
                    {
                        allIngredients[i].SetImpulse(force, toZeroPoint: true);
                    }
                }
                foreach (AlchemicalIngredientOperator ingredient in ingredientsToDestroy)
                {
                    allIngredients.Remove(ingredient);
                }
                border += acceleration;
                force += acceleration;

                await Task.Yield();
            }
            foreach (AlchemicalIngredientOperator ingredient in ingredientsToDestroy)
            {
                Destroy(ingredient.gameObject);
            }
        }

        private List<int> GenerateNewRecipe(int recipeIngredientCount, int length)
        {
            return GameMath.AFewCardsFromTheDeck(recipeIngredientCount, length);
        }

        internal void ActivateIngredient(int keyIngredientNumber)
        {
            bool TheRecipeIsReady = recipeOperator.ActivateIngredient(keyIngredientNumber);
            if (TheRecipeIsReady)
            {
                SuccessfullySolvePuzzle();
            }
        }

        internal Vector3 CheckImpulse(ref Vector3 pos, ref Vector3 impulse)
        {
            if(BrakingField(ref pos))
                impulse *= ingredientFrictionBraking;

            if (Math.Abs(impulse.x) < ingredientBorder && Math.Abs(impulse.y) < ingredientBorder)
            {
                return Vector3.zero;
            }

            return impulse;
        }

        private bool BrakingField(ref Vector3 pos)
        {
            return pos.y > recipeOffset;
        }

        internal void Particles(Vector3 position, bool success)
        {
            ParticleSystem particleSystem = success ? successParticleSystem : errorParticleSystem;

            RectTransform rectTransform = particleSystem.GetComponent<RectTransform>();
            rectTransform.localPosition = position;

            particleSystem.Play();
        }

        internal void RemoveIngredient(AlchemicalIngredientOperator alchemicalIngredientOperator)
        {
            allIngredients.Remove(alchemicalIngredientOperator);
        }

        public void SkipPuzzle()
        {
            SuccessfullySolvePuzzle();
        }
    }
}