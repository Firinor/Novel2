using FirUnityEditor;
using UnityEngine;

public class SceneHUBInformator : MonoBehaviour
{
    [SerializeField, NullCheck]
    private MonoBehaviour sceneManager;
    [SerializeField, NullCheck]
    private Camera mainCamera;

    void Awake()
    {
        SceneHUB.SceneManagerCell.SetValue(sceneManager);
        SceneHUB.CameraCell.SetValue(mainCamera);

        Destroy(this);
    }
}
