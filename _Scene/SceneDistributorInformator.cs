using FirEnum;
using UnityEngine;

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

    void Awake()
    {
        if(SceneManager.instance == null)
            gameObject.transform.SetParent(fallbackTransform);
        else
        {
            SceneManager.SetSceneToPosition(gameObject, scenePosition);
            SceneManager.SetSceneObject(thisSceneMark, thisGameObject);
        }

        //informator11Awake.

        if (!isEnable)
        {
            gameObject.SetActive(false);
        }
    }
}
