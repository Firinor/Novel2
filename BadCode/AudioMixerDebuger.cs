using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerDebuger : MonoBehaviour
{
    public AudioMixer mixer;
    void Update()
    {
        mixer.SetFloat("MasterVolume", Mathf.Lerp(-80f, 0, OptionsOperator.GetVolume()));
        Destroy(gameObject);
    }
}
