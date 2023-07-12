using FirEnum;
using System;
using UnityEngine;
using Zenject;

public class MainMenuManager : SinglBehaviour<MainMenuManager>, IScenePanel
{
    private static GameObject baner;
    private static GameObject credits;
    private static GameObject saves;

    [Inject]
    private MainMenuInformator mainMenuInformator;

    void Awake()
    {
        SingletoneCheck(this);
        SetAllInstance();
    }
    public void SetAllInstance()
    {
        SingletoneCheck(this);//Singltone
        SceneHUB.MenuSceneManagerCell.SetValue(this);

        baner = mainMenuInformator.GetBaner();
        credits = mainMenuInformator.GetCredits();
        saves = mainMenuInformator.GetSaves();
    }

    public static void SwitchPanels(MenuMarks mark)
    {
        if(mark != MenuMarks.options && mark != MenuMarks.off)
            instance.DiactiveAllPanels();
        switch (mark)
        {
            case MenuMarks.baner:
                baner.SetActive(true);
                break;
            case MenuMarks.puzzles:
                //SceneManager.LoadScene("PuzzleRoom");
                break;
            case MenuMarks.credits:
                credits.SetActive(true);
                break;
            case MenuMarks.saves:
                saves.SetActive(true);
                break;
            case MenuMarks.options:
                SceneManager.SwitchPanel(SceneDirection.options);
                break;
            case MenuMarks.off:
                SceneManager.SwitchPanel(SceneDirection.exit);
                break;
            default:
                new Exception("Unrealized bookmark!");
                break;
        }
    }

    public void SwitchPanels(int mark)
    {
        SwitchPanels((MenuMarks)mark);
    }

    public void DiactiveAllPanels()
    {
        baner.SetActive(false);
        credits.SetActive(false);
        saves.SetActive(false);
        SceneManager.DiactiveAllPanels();
    }

    public void BasicPanelSettings()
    {
        SwitchPanels(MenuMarks.baner);
    }
}
