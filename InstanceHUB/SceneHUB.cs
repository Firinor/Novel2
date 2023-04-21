using FirInstanceCell;
using UnityEngine;

public static class SceneHUB
{
    public static MonoBehaviour SceneManager => SceneManagerCell.GetValue();
    public static MonoBehaviour MenuSceneManager => MenuSceneManagerCell.GetValue();
    public static MonoBehaviour ReadingRoomSceneManager => ReadingRoomSceneManagerCell.GetValue();

    public static Camera Camera => CameraCell.GetValue();

    public static InstanceCell<MonoBehaviour> SceneManagerCell = new InstanceCell<MonoBehaviour>();
    public static InstanceCell<MonoBehaviour> MenuSceneManagerCell = new InstanceCell<MonoBehaviour>();
    public static InstanceCell<MonoBehaviour> ReadingRoomSceneManagerCell = new InstanceCell<MonoBehaviour>();

    public static InstanceCell<Camera> CameraCell = new InstanceCell<Camera>();
}
