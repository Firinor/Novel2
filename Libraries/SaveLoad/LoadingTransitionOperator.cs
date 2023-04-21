using FirUnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTransitionOperator : SinglBehaviour<LoadingTransitionOperator>
{
    [SerializeField, NullCheck]
    private GameObject sceneManagerObject;
    private ILoadingManager sceneManager;
    //private static bool needOpenSceneAnimation = false;
    private static bool CloseSceneFlag = false;
    private static bool OpenSceneFlag = false;

    [SerializeField]
    private Sprite[] pullOfSpaces;
    [SerializeField, NullCheck]
    private Image space;
    private static Sprite spaceSprite;
    [SerializeField, NullCheck]
    private Material loadingImageMaterial;
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float playTime;
    private float currentPlayTime;
    [SerializeField]
    private float endPortalPosition = 2f;

    void Awake()
    {
        SingletoneCheck(this);
        sceneManager = sceneManagerObject.GetComponent<ILoadingManager>();
        space = GetComponentInChildren<Image>();
        space.sprite = spaceSprite;
        loadingImageMaterial.SetFloat("InPortal", 0f);
        loadingImageMaterial.SetFloat("Diameter", endPortalPosition);
    }

    void Update()
    {
        if (CloseSceneFlag)
        {
            currentPlayTime += Time.deltaTime;
            float presentage = currentPlayTime / playTime;
            float Diameter = Mathf.Lerp(0f, endPortalPosition, curve.Evaluate(presentage));
            loadingImageMaterial.SetFloat("InPortal", 1f);
            loadingImageMaterial.SetFloat("Diameter", Diameter);
            if (Diameter >= endPortalPosition)
            {
                CloseSceneFlag = false;
                //sceneManager.SetAllowSceneActivation(true);
                OpenScene();
            }
        }
        else if (sceneManager.TheSceneHasLoaded() && OpenSceneFlag)
        {
            sceneManager.SetLoadSceneActive();
            currentPlayTime += Time.deltaTime;
            float presentage = currentPlayTime / playTime;
            float Diameter = Mathf.Lerp(0f, endPortalPosition, curve.Evaluate(presentage));
            loadingImageMaterial.SetFloat("InPortal", 0f);
            loadingImageMaterial.SetFloat("Diameter", Diameter);
            if (Diameter >= endPortalPosition)
            {
                OpenSceneFlag = false;
            }
        }
    }

    public void CurtainDown()
    {
        SetSceneImage();
        CloseScene();
    }

    public void SetSceneImage()
    {
        int SpriteIndex = Random.Range(0, pullOfSpaces.Length);
        if (SpriteIndex > 0)
        {
            spaceSprite = pullOfSpaces[SpriteIndex];
            space.sprite = spaceSprite;
        }
    }

    public void CloseScene()
    {
        CloseSceneFlag = true;
        currentPlayTime = 0f;
    }

    public void OpenScene()
    {
        OpenSceneFlag = true;
        currentPlayTime = 0f;
    }
}