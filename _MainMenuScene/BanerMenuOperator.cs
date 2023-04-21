using FirEnum;
using UnityEngine;

public class BanerMenuOperator : MonoBehaviour
{
    public void Play() => MainMenuManager.SwitchPanels(MenuMarks.saves);
    public void Puzzles() => MainMenuManager.SwitchPanels(MenuMarks.puzzles);
    public void Options() => MainMenuManager.SwitchPanels(MenuMarks.options);
    public void Credits() => MainMenuManager.SwitchPanels(MenuMarks.credits);
    public void QuitGame() => MainMenuManager.SwitchPanels(MenuMarks.off);

}
