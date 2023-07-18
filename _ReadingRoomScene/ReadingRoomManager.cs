using System;
using UnityEngine;
using Puzzle;
using Puzzle.FindObject;
using Puzzle.TetraQuestion;
using Puzzle.FindDifferences;
using Puzzle.StarMap;
using Puzzle.SearchObjects;
using Puzzle.BossBattle;
using Puzzle.PortalBuild;
using FirEnum;
using Zenject;
using Dialog;

public class ReadingRoomManager : MonoBehaviour, IScenePanel, IReadingSceneManager
{
    #region Instances
    [Inject]
    private static FindObjectManager puzzleFindObjectManager;
    [Inject]
    private static TetraQuestionManager puzzleTetraQuestionManager;
    [Inject]
    private static FindDifferencesManager puzzleFindDifferencesManager;
    [Inject]
    private static SearchObjectsManager puzzleSearchObjectsManager;
    [Inject]
    private static SpectralAnalysisManager puzzleSpectralAnalysisManager;
    [Inject]
    private static StarMapManager puzzleStarMapManager;
    [Inject]
    private static BossBattleManager puzzleBossBattleManager;
    [Inject]
    private static MapCanvasOperator mapCanvasOperator;
    #endregion

    //private CanvasManager canvasManager;
    [Inject]
    private SceneManager sceneManager;
    [Inject]
    private DialogManager dialogManager;
    [Inject]
    private MemoryManager memoryManager;

    void Awake()
    {
        if(!(sceneManager.CurrentScene == SceneMarks.readingRoom
            || sceneManager.CurrentScene == SceneMarks.puzzles))
        {
            gameObject.SetActive(false);
        }
    }

    public void SwitchPanels(ReadingRoomMarks mark)
    {
        if(mark != ReadingRoomMarks.options)
            DiactiveAllPanels();

        switch (mark)
        {
            case ReadingRoomMarks.map:
                //map.SetActive(true);
                break;
            case ReadingRoomMarks.dialog:
                dialogManager.gameObject.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleFindObject:
                puzzleFindObjectManager.gameObject.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleTetraQuestion:
                puzzleTetraQuestionManager.gameObject.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleFindDifferences:
                puzzleFindDifferencesManager.gameObject.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleSearchObjects:
                puzzleSearchObjectsManager.gameObject.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleSpectralAnalysis:
                puzzleSpectralAnalysisManager.gameObject.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleStarMap:
                puzzleStarMapManager.gameObject.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleBossBattle:
                puzzleStarMapManager.gameObject.SetActive(true);
                break;
            case ReadingRoomMarks.options:
                sceneManager.SwitchPanel(SceneDirection.options);
                break;
            case ReadingRoomMarks.off:
                break;
            default:
                new Exception("Unrealized bookmark!");
                break;
        }
    }

    public void SwitchPanels(int mark)
    {
        SwitchPanels((ReadingRoomMarks)mark);
    }
    public void SwitchPanelsToOptions()
    {
        SwitchPanels(ReadingRoomMarks.options);
    }
    public void DiactiveAllPanels()
    {
        //map.SetActive(false);
        dialogManager.gameObject.SetActive(false);
        GameObject[] puzzles = GetNotNullPuzzles();
        for (int i = 0; i < puzzles.Length; i++)
        {
            puzzles[i].SetActive(false);
        }
        //puzzleFindObject.SetActive(false);
        //puzzleTetraQuestion.SetActive(false);
        //puzzleFindDifference.SetActive(false);
        //puzzleSearchObjects.SetActive(false);
        //puzzleSpectralAnalysis.SetActive(false);
        //puzzleStarMap.SetActive(false);
        //puzzleBossBattle.SetActive(false);
        sceneManager.DiactiveAllPanels();
    }

    private GameObject[] GetNotNullPuzzles()
    {
        throw new NotImplementedException();
    }

    public void BasicPanelSettings()
    {
        SwitchPanels(ReadingRoomMarks.map);
    }

    public void CheckMap(RectTransform dialogButtonRectTransform)
    {
        mapCanvasOperator.CorrectScrollbarPosition(dialogButtonRectTransform);
    }

    public async void SwithToPuzzle(InformationPackage puzzleInformationPackage, string additional = "")
    {
        switch (puzzleInformationPackage)
        {
            case FindRecipeIngredientsPackage findRecipeIngredients:
                await memoryManager.LoadScene(SceneMarks.findObject);
                puzzleFindObjectManager.SetPuzzleInformationPackage(findRecipeIngredients);
                SwitchPanels(ReadingRoomMarks.puzzleFindObject);
                break;
            case TetraQuestionPackage tetraQuestion:
                puzzleTetraQuestionManager.SetPuzzleInformationPackage(tetraQuestion);
                SwitchPanels(ReadingRoomMarks.puzzleTetraQuestion);
                break;
            case ImageWithDifferencesPackage findDifferenceImage:
                if(additional == "search")
                {
                    puzzleSearchObjectsManager.SetPuzzleInformationPackage(findDifferenceImage);
                    SwitchPanels(ReadingRoomMarks.puzzleSearchObjects);
                }
                else
                {
                    puzzleFindDifferencesManager.SetPuzzleInformationPackage(findDifferenceImage);
                    SwitchPanels(ReadingRoomMarks.puzzleFindDifferences);
                }
                break;
            case StarMapPackage starMapPackage:
                puzzleStarMapManager.SetPuzzleInformationPackage(starMapPackage);
                SwitchPanels(ReadingRoomMarks.puzzleStarMap);
                break;
            case SpectralAnalysisPackage spectralAnalysisPackage:
                puzzleSpectralAnalysisManager.SetPuzzleInformationPackage(spectralAnalysisPackage);
                SwitchPanels(ReadingRoomMarks.puzzleSpectralAnalysis);
                break;
            case BossBattlePackage bossBattlePackage:
                puzzleBossBattleManager.SetPuzzleInformationPackage(bossBattlePackage);
                SwitchPanels(ReadingRoomMarks.puzzleBossBattle);
                break;
            case null:
                break;
            default:
                break;
        }
        
    }
}
