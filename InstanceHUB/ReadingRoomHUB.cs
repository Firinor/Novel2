using FirInstanceCell;
using UnityEngine;

public static class ReadingRoomHUB
{
    public static GameObject Map => MapCell.GetValue();
    public static MonoBehaviour ReadingRoomManager => ReadingRoomManagerCell.GetValue();
    public static MonoBehaviour MapCanvasOperator => MapCanvasOperatorCell.GetValue();

    public static InstanceCell<GameObject> MapCell = new InstanceCell<GameObject>();
    public static InstanceCell<MonoBehaviour> ReadingRoomManagerCell = new InstanceCell<MonoBehaviour>();
    public static InstanceCell<MonoBehaviour> MapCanvasOperatorCell = new InstanceCell<MonoBehaviour>();
}
