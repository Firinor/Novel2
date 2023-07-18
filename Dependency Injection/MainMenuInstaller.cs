using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField]
    private MainMenuInformator mainMenuInformator;

    public override void InstallBindings()
    {
        Container.Bind<MainMenuInformator>().FromInstance(mainMenuInformator).AsSingle();
    }
}
