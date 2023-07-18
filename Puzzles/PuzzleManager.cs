using FirUnityEditor;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField, NullCheck]
    private IPuzzleSceneManager sceneManager;

    public void Options()
    {
        sceneManager.SwitchPanelsToOptions();
    }
}

