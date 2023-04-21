using System;

[Serializable]
public struct OptionsParameters
{
    public bool fullScreen;
    public int screenResolution;
    public float volume;
    public int language;

    public OptionsParameters(bool fullScreen, int screenResolution, float volume, int language)
    {
        this.fullScreen = fullScreen;
        this.screenResolution = screenResolution;
        this.volume = volume;
        this.language = language;
    }
}
