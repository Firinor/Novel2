using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private SceneManager sceneManager;
    [SerializeField]
    private CanvasManager canvasManager;

    public override void InstallBindings()
    {
        Container.Bind<SceneManager>().FromInstance(sceneManager).AsSingle();
        Container.Bind<CanvasManager>().FromInstance(canvasManager).AsSingle();
    }
}