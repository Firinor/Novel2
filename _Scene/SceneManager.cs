using FirEnum;
using FirUnityEditor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class SceneManager : MonoBehaviour, ILoadingManager
{
    [Inject(Id = "ReadingRoom")]
    private IScenePanel readingRoomScene;
    [Inject(Id = "Menu")]
    private IScenePanel menuScene;

    [SerializeField]
    private List<SceneMarks> currentSceneQueue;
    [SerializeField]
    private GameObject currentSceneGameObject;

    private SceneMarks sceneToLoad;
    public SceneMarks CurrentScene
    {
        get
        {
            if (currentSceneQueue.Count < 1)
                return default;

            return currentSceneQueue[currentSceneQueue.Count - 1];
        }
    }
    private Dictionary<SceneMarks, GameObject> scenesObject = new Dictionary<SceneMarks, GameObject>();

    [SerializeField]
    private SceneMarks[] LoadingQueue;

    [SerializeField, NullCheck]
    private GameObject optionsPanel;
    private OptionsOperator optionsOperator;
    [SerializeField, NullCheck]
    private LoadingTransitionOperator loadingTransitionOperator;
    [SerializeField, NullCheck]
    private GameObject[] doNotDestroyOnLoad;

    [SerializeField, NullCheck]
    private Transform puzzleParent;
    [SerializeField, NullCheck]
    private Transform mapParent;

    [Inject]
    private ISceneController dialogOperator;
    [Inject]
    private MemoryManager memoryManager;


    void Awake()
    {
        //optionsOperator = optionsPanel.GetComponent<OptionsOperator>();
        ////optionsOperator is disabled. Awake & Start procedures are not suitable
        //optionsOperator.SingletoneCheck(optionsOperator);

        //CanvasManager canvasManager = GetComponent<CanvasManager>();
        //canvasManager.SingletoneCheck(canvasManager);

        foreach (GameObject go in doNotDestroyOnLoad)
        {
            if (go != null)
            {
                DontDestroyOnLoad(go);
            }
        }

        SetSceneObject(CurrentScene, currentSceneGameObject);

        memoryManager.InitializeSceneDictionary(CurrentScene);

        CheckingTheScene();

        memoryManager.LoadScenes(LoadingQueue);
    }

    public void SetSceneObject(SceneMarks mark, GameObject gameObject)
    {
        if(scenesObject == null)
            scenesObject = new Dictionary<SceneMarks, GameObject>();

        if (scenesObject.ContainsKey(mark))
        {
            scenesObject[mark] = gameObject;
        }
        else
        {
            scenesObject.Add(mark, gameObject);
        }
    }

    public void SetSceneToPosition(GameObject gameObject, ScenePosition position)
    {
        switch (position)
        {
            case ScenePosition.MapPosition:
                gameObject.transform.SetParent(mapParent);
                break;
            case ScenePosition.PuzzlePosition:
                gameObject.transform.SetParent(puzzleParent);
                break;
        }
    }

    //public static IEnumerator PreLoadScene(string sceneName)
    //{
    //    yield return null;

    //    instance.operation = UnitySceneManager.LoadSceneAsync(sceneName);
    //    instance.SetAllowSceneActivation(false);
    //    while (!instance.operation.isDone)
    //    {
    //        yield return null;
    //    }
    //}

    public void LoadScene(SceneMarks scene)
    {
        sceneToLoad = scene;

        if(!TheSceneHasLoaded())
            memoryManager.LoadScene(scene);

        loadingTransitionOperator.CurtainDown();
    }

    public bool TheSceneHasLoaded()
    {
        return memoryManager.isSceneIsReady(sceneToLoad);
    }

    //public void SetAllowSceneActivation(bool v)
    //{
    //    instance.operation.allowSceneActivation = v;
    //}

    public void SetLoadSceneActive()
    {
        scenesObject[CurrentScene].SetActive(false);
        scenesObject[sceneToLoad].SetActive(true);
        //MemoryManager.UnloadScene(currentScene);

        AddSceneToQueue(sceneToLoad);
    }

    public void AddSceneToQueue(SceneMarks sceneToLoad)
    {
        currentSceneQueue.Add(sceneToLoad);
    }

    public void SwitchPanel(SceneDirection direction)
    {
        SwitchPanels(direction);
    }

    public void SwitchPanels(SceneDirection direction)
    {
        switch (direction)
        {
            case SceneDirection.options:
                optionsPanel.SetActive(true);
                break;
            case SceneDirection.exit:
                DiactiveAllPanels();
                ExitAction();
                break;
            case SceneDirection.basic:
                SceneMarks currentScene = currentSceneQueue[currentSceneQueue.Count - 1];
                if (currentScene == SceneMarks.readingRoom || currentScene == SceneMarks.puzzles)
                {
                    readingRoomScene.BasicPanelSettings();
                }
                else if (currentScene == SceneMarks.menu)
                {
                    menuScene.BasicPanelSettings();
                }
                break;
            default:
                throw new Exception("Unrealized bookmark!");
        }
    }

    private void ExitAction()
    {
        int queueSceneIndex = currentSceneQueue.Count - 1;
        if(queueSceneIndex < 0)
            throw new Exception("Error on exit button!");

        SceneMarks currentScene = currentSceneQueue[queueSceneIndex];
        if (currentScene == SceneMarks.readingRoom || currentScene == SceneMarks.puzzles)
        {
            //LoadScene("MainMenu");//
        }
        else if (currentScene == SceneMarks.menu || queueSceneIndex == 0)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        else
        {
            scenesObject[currentScene].SetActive(false);
        }
    }

    public void DiactiveAllPanels()
    {
        optionsPanel.SetActive(false);
    }

    public void CheckingTheScene()
    {
        SceneMarks currentScene = currentSceneQueue[currentSceneQueue.Count - 1];
        if (currentScene == SceneMarks.readingRoom || currentScene == SceneMarks.puzzles
            || currentScene == SceneMarks.menu)
        {
            //ISceneController.SetAllInstance();
        }
        else if (currentScene == SceneMarks.findObject
            || currentScene == SceneMarks.tetraQuestion)
        {
            return;
        }
        else
        {
            throw new Exception("Error on checking scene!");
        }
    }

    public void SkipButton()
    {
        dialogOperator.Skip();
    }
}