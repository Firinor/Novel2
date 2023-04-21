using FirEnum;
using FirUnityEditor;
using System.Collections.Generic;
using UnityEngine;

public class HelpBookManager : MonoBehaviour, IHelpBook
{
    private List<int> listOfPages;

    [SerializeField, NullCheck]
    private GameObject[] ExamPages;
    [SerializeField, NullCheck]
    private GameObject[] PigpenPages;

    [SerializeField, NullCheck]
    private GameObject[] allPages;
    private int pageIndex;
    [SerializeField, NullCheck]
    private GameObject helpBookButton;

    private void OnEnable()
    {
        ShowPage();
    }

    public void AddBookButton()
    {
        listOfPages = new List<int>();
        helpBookButton.SetActive(true);
    }

    public void AddPage(int page)
    {
        listOfPages.Add(page);
    }

    public void AddPage(HelpBookPages page)
    {
        switch (page)
        {
            case HelpBookPages.Exam:
                AddPages(ExamPages);
                break;
            case HelpBookPages.Pigpen:
                AddPages(PigpenPages);
                break;
            default:
                AddPage((int)page);
                break;
        }
    }

    private void AddPages(GameObject[] pagesToAdd)
    {
        foreach(GameObject pageToAdd in pagesToAdd)
        {
            for(int i = 0; i < allPages.Length; i++)
            {
                if (allPages[i] == pageToAdd)
                {
                    listOfPages.Add(i);
                    break;
                }
            }
        }
    }

    public void PageForward()
    {
        pageIndex++;
        if(pageIndex >= listOfPages.Count)
        {
            pageIndex = 0;
        }
        ShowPage();
    }

    public void PageBackward()
    {
        pageIndex--;
        if(pageIndex < 0)
        {
            pageIndex = listOfPages.Count-1;
        }
        ShowPage();
    }

    private void ShowPage()
    {
        DisableAllPages();
        EnablePage(pageIndex);
    }

    private void EnablePage(int pageIndex)
    {
        allPages[listOfPages[pageIndex]].SetActive(true);
    }

    private void DisableAllPages()
    {
        foreach (var page in allPages)
        {
            page.SetActive(false);
        }
    }

    public void HelpBookButtonClick()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
