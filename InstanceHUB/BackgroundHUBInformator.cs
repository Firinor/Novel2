using FirUnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundHUBInformator : MonoBehaviour
{
    [SerializeField, NullCheck]
    private GIFanimation loadingGIF;
    [SerializeField, NullCheck]
    private Image image;
    [SerializeField, NullCheck]
    private Button button;
    [SerializeField, NullCheck]
    private Canvas canvas;

    void Awake()
    {
        if (BackgroundHUB.LoadingGIFCell.isValueNull())
        {
            BackgroundHUB.LoadingGIFCell.SetValue(loadingGIF);
            BackgroundHUB.ImageCell.SetValue(image);
            BackgroundHUB.ButtonCell.SetValue(button);
            BackgroundHUB.CanvasCell.SetValue(canvas);
        }
        Destroy(this);
    }
}
