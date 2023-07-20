using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private SoundInformator soundInformator;
    [SerializeField]
    private LanguageInformator languageInformator;

    [SerializeField]
    private SceneManager sceneManager;
    [SerializeField]
    private CanvasManager canvasManager;

    [SerializeField]
    private OptionsOperator optionsOperator;

    public override void InstallBindings()
    {
        Container.Bind<MemoryManager>().AsSingle().NonLazy();

        Container.BindInstance(soundInformator).AsSingle();
        Container.BindInstance(languageInformator).AsSingle();

        Container.BindInstance(sceneManager).AsSingle();
        Container.BindInstance(canvasManager).AsSingle();

        Container.BindInstance(optionsOperator).AsSingle();
    }
}