using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : SinglBehaviour<CanvasManager>
{
    [SerializeField]
    private Canvas mainCanvas;
    [SerializeField]
    private CanvasScaler mainCanvasScaler;

    public static float ScreenHeight { get => instance.mainCanvasScaler.referenceResolution.y; }
    public static float ScreenWidth { get => instance.mainCanvas.renderingDisplaySize.x / instance.mainCanvas.scaleFactor; }
    public static float ScaleFactor { get => instance.mainCanvas.scaleFactor; }
}
