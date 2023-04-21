using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInformator : SinglBehaviour<MainMenuInformator>
{
    [SerializeField]
    private GameObject baner;
    [SerializeField]
    private GameObject credits;
    [SerializeField]
    private GameObject saves;
    private GameObject options;

    public static GameObject GetBaner()
    {
        return instance.baner;
    }

    public static GameObject GetCredits()
    {
        return instance.credits;
    }

    public static GameObject GetSaves()
    {
        return instance.saves;
    }
}
