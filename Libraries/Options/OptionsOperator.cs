using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsOperator : SinglBehaviour<OptionsOperator>, IOptionsOperator
{
    [SerializeField]
    private AudioMixerGroup mixerMasterGroup;
    [SerializeField]
    public Slider volumeSlider;
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private IScenePanel scenePanel;

    private static bool OnLoad;

    public void Return()
    {
        gameObject.SetActive(false);
    }

    public void RestoreDefault()
    {

    }
    public void FullScreen(Toggle toggle)
    {
        bool v = toggle.isOn;
        OptionsManager.FullScreen = v;

        if (!OnLoad)
            SaveManager.SaveOptions();
    }
    public void ScreenResolution(Dropdown dropdown)
    {
        int i = dropdown.value;
        OptionsManager.CurrentScreenResolution = OptionsManager.GetResolution(i);

        if (!OnLoad)
            SaveManager.SaveOptions(ScreenResolution: i);
    }
    public void MasterVolume()
    {
        if (instance == null)
            return;

        float volume = Mathf.Lerp(-80f, 0, curve.Evaluate(GetVolume()));

        mixerMasterGroup.audioMixer.SetFloat("MasterVolume", volume);
        if (!OnLoad)
            SaveManager.SaveOptions();
    }
    public static float GetVolume()
    {
        if (instance == null)
            return default;

        //volumeSlider minValue = -1, minValue = 1
        return instance.volumeSlider.value / 2 + .5f;
    }
    public void Language(Dropdown dropdown)
    {
        if (instance == null)
            return;
        Languages language = (Languages)dropdown.value;
        if(PlayerManager.Language != language)
        {
            PlayerManager.Language = language;
        }
            

        if (!OnLoad)
            SaveManager.SaveOptions();
    }

    public OptionsParameters GetParameters(int ScreenResolutoin = -1)
    {
        return new OptionsParameters(OptionsManager.FullScreen, ScreenResolutoin, instance.volumeSlider.value, 0);
    }

    public void LoadOptions()
    {
        if (instance == null)
            return;

        OnLoad = true;
        //var parametrs = SaveManager.LoadOptions();
        //instance.fullScreen = parametrs.fullScreen;
        //instance.screenResolution = parametrs.screenResolution;
        //instance.volumeSlider.value = parametrs.volume;
        //instance.language = parametrs.language;
        OnLoad = false;
    }
}