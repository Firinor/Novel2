using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public static class OptionsManager
{
    public static bool FullScreen;
    public static Vector2 CurrentScreenResolution 
    {
        get
        {
            return new Vector2(Screen.width, Screen.height);
        }
        set
        {
            Screen.SetResolution((int)value.x, (int)value.y, FullScreen);
        }
    }

    public static OptionsParameters CurrentOptions;

    public static class ScreenResolution
    {
        public static readonly Vector2 HD = new Vector2(1280, 720);
        public static readonly Vector2 FullHD = new Vector2(1920, 1080);
        public static readonly Vector2 QHD = new Vector2(2560, 1440);
        public static readonly Vector2 UHD = new Vector2(3840, 2160);
    }

    public static Vector2 GetResolution(int i)
    {
        switch (i)
        {
            case 0:
                return ScreenResolution.HD;
            case 1:
                return ScreenResolution.FullHD;
            case 2:
                return ScreenResolution.QHD;
            case 3:
                return ScreenResolution.UHD;
            default:
                return ScreenResolution.FullHD;
        }
    }
}
