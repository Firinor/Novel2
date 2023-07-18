using FirEnum;
using UnityEngine;
using Zenject;

public class CreditChallengeOperator : MonoBehaviour
{
    [Inject]
    private MainMenuManager mainMenuManager;

    public void Return()
    {
        mainMenuManager.SwitchPanels(MenuMarks.baner);
    }
}
