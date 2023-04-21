public enum SceneDirection { basic, exit, options }//, changeScene, saves, off }

public interface ILoadingManager
{
    //public void SetAllowSceneActivation(bool b);
    public bool TheSceneHasLoaded();
    public void SwitchPanels(SceneDirection direction);
    public void SetLoadSceneActive();
}
