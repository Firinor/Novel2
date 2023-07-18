using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private Canvas mainCanvas;
    [SerializeField]
    private CanvasScaler mainCanvasScaler;

    public float ScreenHeight { get => mainCanvasScaler.referenceResolution.y; }
    public float ScreenWidth { get => mainCanvas.renderingDisplaySize.x / mainCanvas.scaleFactor; }
    public float ScaleFactor { get => mainCanvas.scaleFactor; }
}
