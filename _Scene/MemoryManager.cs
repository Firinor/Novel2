using FirEnum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class MemoryManager
{
    private Dictionary<SceneMarks, bool> scenesInGame;
    private AsyncOperation operation;
    [Inject]
    private GIFanimation loadingGIF;

    public void InitializeSceneDictionary(SceneMarks currentScene)
    {
        scenesInGame = new Dictionary<SceneMarks, bool>();
        
        for(int i = 0; i < UnitySceneManager.sceneCountInBuildSettings; i++)
        {
            scenesInGame.Add((SceneMarks)i, false);
        }

        scenesInGame[currentScene] = true;
    }

    public async Task LoadScene(SceneMarks mark)
    {
        while(operation != null)
        {
            Debug.Log($"Scene {mark} in queue: at {DateTime.Now}");
            await Task.Yield();
        }

        if (scenesInGame[mark])
            return;

        Debug.Log($"Start load scene {mark}: at {DateTime.Now}");
        operation = UnitySceneManager.LoadSceneAsync((int)mark);
        int i = 0;
        while (!operation.isDone)
        {
            i++;
            Debug.Log(i);
            await Task.Yield();
        }

        await Task.Yield();
        scenesInGame[mark] = true;
        operation = null;
        Debug.Log($"End load scene {mark}: at {DateTime.Now}");
    }

    public async void LoadScenes(SceneMarks[] loadingQueue)
    {
        loadingGIF.StartAnimation();

        for (int i = 0; i < loadingQueue.Length;i++)
        {
            await LoadScene(loadingQueue[i]);
        }

        loadingGIF.StopAnimation();
    }

    public void UnloadScene(SceneMarks mark)
    {
        if(scenesInGame[mark])
        {
            scenesInGame[mark] = false;
        }
    }

    public bool isSceneIsReady(SceneMarks scene)
    {
        if(scenesInGame == null)
            return false;

        return scenesInGame[scene];
    }
}
