using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class AudioMixerDebuger : MonoBehaviour
{
    public AudioMixer mixer;

    [Inject]
    private OptionsOperator optionsOperator;

    void Update()
    {
        mixer.SetFloat("MasterVolume", Mathf.Lerp(-80f, 0, optionsOperator.GetVolume()));
        Destroy(gameObject);
    }
}
