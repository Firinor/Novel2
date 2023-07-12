using FirEnum;
using FirUnityEditor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneManager : SinglBehaviour<SceneManager>, ILoadingManager
{
    private IScenePanel readingRoomScene => (IScenePanel)SceneHUB.ReadingRoomSceneManager;
    private IScenePanel menuScene => (IScenePanel)SceneHUB.MenuSceneManager;

    [SerializeField]
    private List<SceneMarks> currentSceneQueue;
    [SerializeField]
    private GameObject currentSceneGameObject;

    private SceneMarks sceneToLoad;
    public static SceneMarks CurrentScene
    {
        get
        {
            if (instance.currentSceneQueue.Count < 1)
                return default;

            return instance.currentSceneQueue[instance.currentSceneQueue.Count - 1];
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

    void Awake()
    {
        if (!SingletoneCheck(this))
            return;

        optionsOperator = optionsPanel.GetComponent<OptionsOperator>();
        //optionsOperator is disabled. Awake & Start procedures are not suitable
        optionsOperator.SingletoneCheck(optionsOperator);

        CanvasManager canvasManager = GetComponent<CanvasManager>();
        canvasManager.SingletoneCheck(canvasManager);

        foreach (GameObject go in doNotDestroyOnLoad)
        {
            if (go != null)
            {
                DontDestroyOnLoad(go);
            }
        }

        SetSceneObject(CurrentScene, currentSceneGameObject);

        MemoryManager.InitializeSceneDictionary(CurrentScene);

        CheckingTheScene();

        MemoryManager.LoadScenes(LoadingQueue);
    }

    public static void SetSceneObject(SceneMarks mark, GameObject gameObject)
    {
        if (instance == null)
            return;

        Dictionary<SceneMarks, GameObject> scenesObject = instance.scenesObject;
        if (scenesObject.ContainsKey(mark))
        {
            scenesObject[mark] = gameObject;
        }
        else
        {
            scenesObject.Add(mark, gameObject);
        }
    }

    public static void SetSceneToPosition(GameObject gameObject, ScenePosition position)
    {
        switch (position)
        {
            case ScenePosition.MapPosition:
                gameObject.transform.SetParent(instance.mapParent);
                break;
            case ScenePosition.PuzzlePosition:
                gameObject.transform.SetParent(instance.puzzleParent);
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

    public static void LoadScene(SceneMarks scene)
    {
        instance.sceneToLoad = scene;

        if(!instance.TheSceneHasLoaded())
            MemoryManager.LoadScene(scene);

        instance.loadingTransitionOperator.CurtainDown();
    }

    public bool TheSceneHasLoaded()
    {
        return MemoryManager.isSceneIsReady(sceneToLoad);
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

    public static void AddSceneToQueue(SceneMarks sceneToLoad)
    {
        instance.currentSceneQueue.Add(sceneToLoad);
    }

    public static void SwitchPanel(SceneDirection direction)
    {
        instance.SwitchPanels(direction);
    }

    public void SwitchPanels(SceneDirection direction)
    {
        switch (direction)
        {
            case SceneDirection.options:
                instance.optionsPanel.SetActive(true);
                break;
            case SceneDirection.exit:
                DiactiveAllPanels();
                ExitAction();
                break;
            case SceneDirection.basic:
                SceneMarks currentScene = instance.currentSceneQueue[instance.currentSceneQueue.Count - 1];
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

    private static void ExitAction()
    {
        int queueSceneIndex = instance.currentSceneQueue.Count - 1;
        if(queueSceneIndex < 0)
            throw new Exception("Error on exit button!");

        SceneMarks currentScene = instance.currentSceneQueue[queueSceneIndex];
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
            instance.scenesObject[currentScene].SetActive(false);
        }
    }

    public static void DiactiveAllPanels()
    {
        instance.optionsPanel.SetActive(false);
    }

    public static void CheckingTheScene()
    {
        SceneMarks currentScene = instance.currentSceneQueue[instance.currentSceneQueue.Count - 1];
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

    public static void SkipButton()
    {
        if (!DialogHUB.DialogOperatorCell.isValueNull())
        {
            ISceneController dialogOperator = (ISceneController)DialogHUB.DialogOperator;
            dialogOperator.Skip();
        }
    }
}