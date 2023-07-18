using FirEnum;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SaveChoosingOperator : MonoBehaviour
{
    [Inject]
    private SceneManager sceneManager;
    [Inject]
    private MainMenuManager mainMenuManager;

    [SerializeField]
    private SaveManager saveManager;
    [SerializeField]
    private GameObject[] Jars;

    void Start()
    {
        if (saveManager == null)
        {
            saveManager = GameObject.FindGameObjectWithTag("AnyScene").GetComponent<SaveManager>();
        }

        for (int i = 0; i < 3; i++)
        {
            //Jar[i].SetActive(saveManager.FileExists(i));
        }
    }

    public void Return()
    {
        mainMenuManager.SwitchPanels(MenuMarks.baner);
    }

    public void LoadSave(int i)
    {
        DisableAllJars();
        if (Jars[i].activeSelf)
        {
            //SaveManager.Load(i);
        }
        else
        {
            //SaveManager.CreateNewSave(i);//
        }

        mainMenuManager.SwitchPanels(MenuMarks.baner);
        sceneManager.LoadScene(SceneMarks.readingRoom);
    }

    private void DisableAllJars()
    {
        foreach (GameObject jar in Jars)
        {
            jar.GetComponent<Button>().enabled = false;
        }
    }
}
