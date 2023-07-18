using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundInformator : MonoBehaviour
{
    [SerializeField]
    private AudioClip backgroundMusic;

    [Space]
    [SerializeField]
    private AudioClip buttonOnMouseEnter;
    [SerializeField]
    private AudioClip buttonOnMouseExit;
    [SerializeField]
    private AudioClip buttonOnClic;

    private List<AudioSourceOperator> backgroundMusicSource = new List<AudioSourceOperator>();
    private List<AudioSourceOperator> buttonsSource = new List<AudioSourceOperator>();

    public static AudioSource GlobalUIAudioSource { get; private set; }

    void Awake()
    {
        GlobalUIAudioSource = GetComponent<AudioSource>();

        AudioSourceOperator[] AudioOperators = FindObjectsOfType<AudioSourceOperator>(true);

        for (int i = 0; i < AudioOperators.Length; i++)
        {
            switch (AudioOperators[i].GetAudioType())
            {
                case AudioType.Button:
                    buttonsSource.Add(AudioOperators[i]);
                    break;
                case AudioType.Background:
                    backgroundMusicSource.Add(AudioOperators[i]);
                    break;
                default:
                    new Exception("Unrealized audio type!");
                    break;
            }
        }
    }

    public List<AudioClip> GetClips(AudioType type)
    {
        switch (type)
        {
            case AudioType.Background:
                return GetBackgroundMusic();
            case AudioType.Button:
                return GetButtonsMusic();
        }
        return new List<AudioClip>();
    }

    public List<AudioSourceOperator> GetBackgroundOperators()
    {
        return backgroundMusicSource;
    }

    public List<AudioSourceOperator> GetButtonsOperators()
    {
        return buttonsSource;
    }

    public List<AudioClip> GetBackgroundMusic()
    {
        return new List<AudioClip>() { backgroundMusic };
    }

    public List<AudioClip> GetButtonsMusic()
    {
        return new List<AudioClip>() {
        buttonOnMouseEnter,
        buttonOnMouseExit,
        buttonOnClic };
    }
}

[Serializable]
public class MaterialSounds
{
    [SerializeField]
    public AudioClip unitDeath;
    [SerializeField]
    public AudioClip unitHit;
    [SerializeField]
    public AudioClip unitAttack;

    public AudioClip GetClipByIndex(int i)
    {
        switch (i)
        {
            case 0:
                return unitDeath;
            case 1:
                return unitHit;
            case 2:
                return unitAttack;
        }
        new Exception("Sound value outside the array!");
        return null;
    }
}