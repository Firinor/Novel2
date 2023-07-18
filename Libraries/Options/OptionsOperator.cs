using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class OptionsOperator : MonoBehaviour, IOptionsOperator
{
    [SerializeField]
    private AudioMixerGroup mixerMasterGroup;
    [SerializeField]
    public Slider volumeSlider;
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private IScenePanel scenePanel;

    [Inject]
    private SaveManager saveManager;

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
            saveManager.SaveOptions();
    }
    public void ScreenResolution(Dropdown dropdown)
    {
        int i = dropdown.value;
        OptionsManager.CurrentScreenResolution = OptionsManager.GetResolution(i);

        if (!OnLoad)
            saveManager.SaveOptions(ScreenResolution: i);
    }
    public void MasterVolume()
    {
        float volume = Mathf.Lerp(-80f, 0, curve.Evaluate(GetVolume()));

        mixerMasterGroup.audioMixer.SetFloat("MasterVolume", volume);
        if (!OnLoad)
            saveManager.SaveOptions();
    }
    public float GetVolume()
    {
        //volumeSlider minValue = -1, minValue = 1
        return volumeSlider.value / 2 + .5f;
    }
    public void Language(Dropdown dropdown)
    {
        Languages language = (Languages)dropdown.value;
        if(PlayerManager.Language != language)
        {
            PlayerManager.Language = language;
        }
            

        if (!OnLoad)
            saveManager.SaveOptions();
    }

    public OptionsParameters GetParameters(int ScreenResolutoin = -1)
    {
        return new OptionsParameters(OptionsManager.FullScreen, ScreenResolutoin, volumeSlider.value, 0);
    }

    public void LoadOptions()
    {
        OnLoad = true;
        //var parametrs = SaveManager.LoadOptions();
        //instance.fullScreen = parametrs.fullScreen;
        //instance.screenResolution = parametrs.screenResolution;
        //instance.volumeSlider.value = parametrs.volume;
        //instance.language = parametrs.language;
        OnLoad = false;
    }
}