using FirInstanceCell;
using UnityEngine;
using UnityEngine.UI;

public static class BackgroundHUB
{
    public static GIFanimation LoadingGIF => LoadingGIFCell.GetValue();
    public static Canvas Canvas => CanvasCell.GetValue();
    public static Image Image => ImageCell.GetValue();
    public static Button Button => ButtonCell.GetValue();

    public static InstanceCell<GIFanimation> LoadingGIFCell = new InstanceCell<GIFanimation>();
    public static InstanceCell<Canvas> CanvasCell = new InstanceCell<Canvas>();
    public static InstanceCell<Image> ImageCell = new InstanceCell<Image>();
    public static InstanceCell<Button> ButtonCell = new InstanceCell<Button>();
}
