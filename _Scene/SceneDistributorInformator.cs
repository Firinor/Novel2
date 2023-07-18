using FirEnum;
using UnityEngine;
using Zenject;

public class SceneDistributorInformator : MonoBehaviour
{
    [SerializeField]
    private bool isEnable;
    [SerializeField]
    private ScenePosition scenePosition;
    [SerializeField]
    private Transform fallbackTransform;
    [SerializeField]
    private SceneMarks thisSceneMark;
    [SerializeField]
    private GameObject thisGameObject;
    //[SerializeField]
    //private MonoBehaviour informatorAwake;

    [Inject]
    private SceneManager sceneManager;

    void Awake()
    {
        if(sceneManager == null)
            gameObject.transform.SetParent(fallbackTransform);
        else
        {
            sceneManager.SetSceneToPosition(gameObject, scenePosition);
            sceneManager.SetSceneObject(thisSceneMark, thisGameObject);
        }

        if (!isEnable)
        {
            gameObject.SetActive(false);
        }
    }
}
