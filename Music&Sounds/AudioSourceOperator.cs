using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AudioType { Button, HeroCard, Background, Unit }

[RequireComponent(typeof(AudioSource))]
public class AudioSourceOperator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource audioSource;
    [SerializeField]
    private AudioType audioType;

    void Awake()
    {
        audioClips = SoundInformator.GetClips(audioType);
        if (audioType == AudioType.Background)
        {
            audioSource = GetAudioSource();
            if (audioSource != null && audioSource.enabled)
            {
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
        }
    }

    private AudioSource GetAudioSource()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        return audioSource;
    }

    public void SetSound(AudioClip clip)
    {
        SetSound(new List<AudioClip>() { clip });
    }

    public void SetSound(List<AudioClip> clips)
    {
        if (clips.Count <= 0)
            return;

        switch (audioType)
        {
            case AudioType.Button:
            case AudioType.HeroCard:
                audioClips = clips;
                break;
            case AudioType.Background:
                audioClips = clips;
                audioSource.clip = audioClips[0];
                audioSource.Play();
                break;
            default:
                new Exception("Unrealized audio type!");
                break;
        }
    }

    public AudioType GetAudioType()
    {
        return audioType;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int SoundNumber = 0;
        switch (audioType)
        {
            case AudioType.Button:
                PlaySound(SoundNumber, false);
                break;
            case AudioType.HeroCard:
                PlaySound(SoundNumber, false);
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        int SoundNumber = 1;
        switch (audioType)
        {
            case AudioType.Button:
                PlaySound(SoundNumber, false);
                break;
            case AudioType.HeroCard:
                PlaySound(SoundNumber, false);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int SoundNumber = 2;
        switch (audioType)
        {
            case AudioType.Button:
                PlaySound(SoundNumber, true);
                break;
            case AudioType.HeroCard:
                PlaySound(SoundNumber, false);
                break;
        }
    }

    public void PlaySound(int SoundNumber, bool global)
    {
        if (CheckClip(SoundNumber))
            if (global)
                SoundInformator.GlobalUIAudioSource.PlayOneShot(audioClips[SoundNumber]);
            else
                audioSource.PlayOneShot(audioClips[SoundNumber]);
    }

    private bool CheckClip(int SoundNumber)
    {
        return audioClips.Count > SoundNumber && audioClips[SoundNumber] != null;
    }
}
