using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private SoundInformator soundInformator;
    [SerializeField]
    private OptionsOperator optionsOperator;

    public override void InstallBindings()
    {
        Container.Bind<MemoryManager>().AsSingle().NonLazy();
        Container.BindInstance(soundInformator).AsSingle();
        Container.BindInstance(optionsOperator).AsSingle();
    }
}