using FirUnityEditor;
using UnityEngine;

public class PuzzleManager : SinglBehaviour<PuzzleManager>
{
    [SerializeField, NullCheck]
    private IPuzzleSceneManager sceneManager;

    void Awake()
    {
        SingletoneCheck(this);
    }

    public static void Options()
    {
        instance.sceneManager.SwitchPanelsToOptions();
    }
}

