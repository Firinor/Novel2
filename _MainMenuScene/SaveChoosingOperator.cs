using FirEnum;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveChoosingOperator : MonoBehaviour
{
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
        MainMenuManager.SwitchPanels(MenuMarks.baner);
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
            //SaveManager.CreateNewSave(i);
        }

        MainMenuManager.SwitchPanels(MenuMarks.baner);
        SceneManager.LoadScene(SceneMarks.readingRoom);
    }

    private void DisableAllJars()
    {
        foreach (GameObject jar in Jars)
        {
            jar.GetComponent<Button>().enabled = false;
        }
    }
}
