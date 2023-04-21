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

public class ReadingRoomManager : SinglBehaviour<ReadingRoomManager>, IScenePanel, IReadingSceneManager
{
    #region Instances
    private static GameObject dialog => DialogHUB.DialogObject;
    private static GameObject puzzleFindObject => PuzzleHUB.FindObject;
    private static GameObject puzzleTetraQuestion => PuzzleHUB.TetraQuestion;
    private static GameObject puzzleFindDifference => PuzzleHUB.FindDifference;
    private static GameObject puzzleSearchObjects => PuzzleHUB.SearchObjects;
    private static GameObject puzzleSpectralAnalysis => PuzzleHUB.SpectralAnalysis;
    private static GameObject puzzleStarMap => PuzzleHUB.StarMap;
    private static GameObject puzzleBossBattle => PuzzleHUB.BossBattle;

    private static FindObjectManager puzzleFindObjectManager => (FindObjectManager)PuzzleHUB.FindObjectManager;
    private static TetraQuestionManager puzzleTetraQuestionManager => (TetraQuestionManager)PuzzleHUB.TetraQuestionManager;
    private static FindDifferencesManager puzzleFindDifferencesManager => (FindDifferencesManager)PuzzleHUB.FindDifferencesManager;
    private static SearchObjectsManager puzzleSearchObjectsManager => (SearchObjectsManager)PuzzleHUB.SearchObjectsManager;
    private static SpectralAnalysisManager puzzleSpectralAnalysisManager => (SpectralAnalysisManager)PuzzleHUB.SpectralAnalysisManager;
    private static StarMapManager puzzleStarMapManager => (StarMapManager)PuzzleHUB.StarMapManager;
    private static BossBattleManager puzzleBossBattleManager => (BossBattleManager)PuzzleHUB.BossBattleManager;
    private static MapCanvasOperator mapCanvasOperator => (MapCanvasOperator)ReadingRoomHUB.MapCanvasOperator;
    #endregion

    //private CanvasManager canvasManager;

    void Awake()
    {
        if(!(SceneManager.CurrentScene == SceneMarks.readingRoom
            || SceneManager.CurrentScene == SceneMarks.puzzles))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetAllInstance()
    {
        SingletoneCheck(this);
        SceneHUB.ReadingRoomSceneManagerCell.SetValue(this);
    }

    public static void SwitchPanels(ReadingRoomMarks mark)
    {
        if(mark != ReadingRoomMarks.options)
            instance.DiactiveAllPanels();

        switch (mark)
        {
            case ReadingRoomMarks.map:
                //map.SetActive(true);
                break;
            case ReadingRoomMarks.dialog:
                dialog.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleFindObject:
                puzzleFindObject.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleTetraQuestion:
                puzzleTetraQuestion.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleFindDifferences:
                puzzleFindDifference.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleSearchObjects:
                puzzleSearchObjects.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleSpectralAnalysis:
                puzzleSpectralAnalysis.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleStarMap:
                puzzleStarMap.SetActive(true);
                break;
            case ReadingRoomMarks.puzzleBossBattle:
                puzzleStarMap.SetActive(true);
                break;
            case ReadingRoomMarks.options:
                SceneManager.SwitchPanel(SceneDirection.options);
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
        dialog.SetActive(false);
        GameObject[] puzzles = PuzzleHUB.GetNotNullPuzzles();
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
        SceneManager.DiactiveAllPanels();
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
                await MemoryManager.LoadScene(SceneMarks.findObject);
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
