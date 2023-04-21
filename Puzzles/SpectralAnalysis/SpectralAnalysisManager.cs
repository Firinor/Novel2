using FirCleaner;
using FirMath;
using FirUnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Puzzle.PortalBuild
{
    public class SpectralAnalysisManager : PuzzleOperator, IOptionsSwitchHandler
    {
        [SerializeField, NullCheck]
        private MainSpecterOperator mainSpecter;
        [SerializeField, NullCheck]
        private Transform specterComponentsParent;
        [SerializeField, NullCheck]
        private GameObject specterComponentPrefab;
        [SerializeField, NullCheck]
        private RectTransform plusSpecterComponentButton;
        [SerializeField, NullCheck]
        private GameObject boxWithComponent;
        [SerializeField, NullCheck]
        private Transform boxComponentParent;
        [SerializeField, NullCheck]
        private GameObject boxComponentPrefab;
        [SerializeField, NullCheck]
        private Button readyButton;

        [SerializeField, NullCheck]
        private AtomsInformator atomInformator;
        public static AtomsInformator AtomInformator;
        [HideInInspector]
        public AtomComponentOperator specterComponentOperator;

        [SerializeField]
        private SpectralAnalysisPackage portalBuildPackage;

        private List<int> answer;

        protected void Awake()
        {
            if(AtomInformator == null)
                AtomInformator = atomInformator;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ClearPuzzle();
            StartPuzzle();
        }
        public override void StartPuzzle()
        {
            base.StartPuzzle();

            CreateNewObjectsInBox();
            CreateNewSpecterObjects();

            answer = GameMath.AFewCardsFromTheDeck(
                SpecialComplexityChanñe(portalBuildPackage.RecipeCount),
                portalBuildPackage.IngredientsCount);

            mainSpecter.GenetareNewSpecter(answer);
            mainSpecter.SetColorShift(portalBuildPackage.ColorShift);
            readyButton.interactable = true;
        }

        private int SpecialComplexityChanñe(int recipeCount)
        {
            int result = recipeCount;

            if(recipeCount >= Random.Range(minInclusive: 1, maxInclusive: 10))
                result++;

            //Debug.Log(result);

            return result;
        }

        public override void ClearPuzzle()
        {
            base.ClearPuzzle();
            DeleteAllSpecterComponents();
            DeleteAllInBoxComponents();
            mainSpecter.DestroyAnswerAtoms();
            mainSpecter.ResetRecipeRatio();
        }
        private void DeleteAllSpecterComponents()
        {
            plusSpecterComponentButton.SetParent(transform);
            GameCleaner.DeleteAllChild(specterComponentsParent);
            plusSpecterComponentButton.SetParent(specterComponentsParent);
        }

        private void DeleteAllInBoxComponents()
        {
            GameCleaner.DeleteAllChild(boxComponentParent);
        }

        public void SetPuzzleInformationPackage(SpectralAnalysisPackage portalBuildPackage)
        {
            this.portalBuildPackage = portalBuildPackage;
            SetVictoryEvent(portalBuildPackage.successPuzzleAction);
            SetFailEvent(portalBuildPackage.failedPuzzleAction);
            SetBackground(portalBuildPackage.PuzzleBackground);
        }

        public void CheckAnswer()
        {
            readyButton.interactable = false;
            mainSpecter.GenerateAnswerAtoms();
            List<int> playerHand = CheckPlayerHand();
            if (GameMath.IsSetsOfCardsMatch(playerHand, answer, countMustBeSame: false))
            {
                SuccessfullySolvePuzzle();
            }
            else
            {
                LosePuzzle();
            }
        }

        private List<int> CheckPlayerHand()
        {
            List<int> playerHand = new List<int>();
            AtomComponentOperator[] playerAtoms = 
                specterComponentsParent.GetComponentsInChildren<AtomComponentOperator>();

            foreach(AtomComponentOperator atomOperator in playerAtoms)
            {
                playerHand.Add(atomOperator.AtomComponent.Number);
            }

            return playerHand;
        }

        private void CreateNewObjectsInBox()
        {
            for (int i = 0; i < portalBuildPackage.IngredientsCount; i++)
            {
                GameObject Atom = Instantiate(boxComponentPrefab, boxComponentParent);
                AtomComponentOperator atomOperator = Atom.GetComponent<AtomComponentOperator>();
                atomOperator.SetValue(AtomInformator.Atoms[i]);
                atomOperator.SetManager(this);
            }
        }

        private void CreateNewSpecterObjects()
        {
            AddNewSpecterComponent();
        }

        public void AddNewSpecterComponent()
        {
            GameObject Specter = Instantiate(specterComponentPrefab, specterComponentsParent);
            AtomComponentOperator atomOperator = Specter.GetComponent<AtomComponentOperator>();
            atomOperator.SetManager(this);

            plusSpecterComponentButton.SetAsLastSibling();
        }

        public void SelectNewComponent(AtomComponentOperator specterComponentOperator)
        {
            if (boxWithComponent == null)
                Debug.Log("BoxWithComponent == null");

            if (boxWithComponent.activeSelf)
            {
                this.specterComponentOperator.SetValue(specterComponentOperator.AtomComponent);
                BoxWithComponentOff();
            }
            else
            {
                this.specterComponentOperator = specterComponentOperator;
                boxWithComponent.SetActive(true);
            }
        }

        public void BoxWithComponentOff()
        {
            specterComponentOperator = null;
            boxWithComponent.SetActive(false);
        }

        public void ResetOptions()
        {
            
        }
    }
}
