using FirInstanceCell;
using UnityEngine;

public static class DialogHUB
{
    public static GameObject DialogObject => DialogObjectCell.GetValue();
    public static MonoBehaviour DialogManager => DialogManagerCell.GetValue();
    public static MonoBehaviour DialogOperator => DialogOperatorCell.GetValue();
    public static Canvas Canvas => CanvasCell.GetValue();

    public static InstanceCell<GameObject> DialogObjectCell = new InstanceCell<GameObject>();
    public static InstanceCell<MonoBehaviour> DialogManagerCell = new InstanceCell<MonoBehaviour>();
    public static InstanceCell<MonoBehaviour> DialogOperatorCell = new InstanceCell<MonoBehaviour>();
    public static InstanceCell<Canvas> CanvasCell = new InstanceCell<Canvas>();
}
