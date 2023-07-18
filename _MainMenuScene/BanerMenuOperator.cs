using FirEnum;
using UnityEngine;
using Zenject;

public class BanerMenuOperator : MonoBehaviour
{
    [Inject]
    private MainMenuManager mainMenuManager;

    public void Play() => mainMenuManager.SwitchPanels(MenuMarks.saves);
    public void Puzzles() => mainMenuManager.SwitchPanels(MenuMarks.puzzles);
    public void Options() => mainMenuManager.SwitchPanels(MenuMarks.options);
    public void Credits() => mainMenuManager.SwitchPanels(MenuMarks.credits);
    public void QuitGame() => mainMenuManager.SwitchPanels(MenuMarks.off);

}
