using FirUnityEditor;
using UnityEngine;

public class ReadingRoomHUBInformator : SinglBehaviour<ReadingRoomHUBInformator>
{
    [SerializeField, NullCheck]
    private GameObject map;
    [SerializeField, NullCheck]
    private MonoBehaviour readingRoomManager;
    [SerializeField, NullCheck]
    private MonoBehaviour mapCanvasOperator;

    void Awake()
    {
        ReadingRoomHUB.MapCell.SetValue(map);
        ReadingRoomHUB.ReadingRoomManagerCell.SetValue(readingRoomManager);
        ReadingRoomHUB.MapCanvasOperatorCell.SetValue(mapCanvasOperator);
        Destroy(this);
    }
}
