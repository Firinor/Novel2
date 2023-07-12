using UnityEngine;

public class MainMenuInformator : MonoBehaviour
{
    [SerializeField]
    private GameObject baner;
    [SerializeField]
    private GameObject credits;
    [SerializeField]
    private GameObject saves;

    public GameObject GetBaner()
    {
        return baner;
    }

    public GameObject GetCredits()
    {
        return credits;
    }

    public GameObject GetSaves()
    {
        return saves;
    }
}
