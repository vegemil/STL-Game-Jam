using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;



public class AudioManager : Singleton<AudioManager>
{

    public enum BGMType
    {
        Default, 
    }
    public enum EffectType
    {
        Default,
    }


    AudioSource BGMPlayer;
    GameObject BGMPlayerPrefab;
    GameObject EffectPlayerPrefab;
    [SerializeField] GameObject[] CreatedSoundEffects;
    [SerializeField] private AudioDatas AudioDatas;

    public float BGMVolume = 1.0f;
    public float SFVoluem = 0.75f;
    bool bMuteBGM;
    bool bMuteSF;
    public bool IsPausedBGM { get { return !BGMPlayer.isPlaying; } }
    public bool IsMuteBGM { get { return bMuteBGM; } }
    public bool IsMuteSF { get { return bMuteSF; } }

    private void Awake()
    {
        AudioDatas = Resources.Load<AudioDatas>("Datas/AudioData");
        bMuteBGM = false;
        bMuteSF = false;
        BGMPlayerPrefab = AudioDatas.BGMPlayerPrefab;
        EffectPlayerPrefab = AudioDatas.EffectAudioPrefab;
        GetBGMPlayer();
        BGMVolume = BGMPlayer.volume;
    }

    AudioSource GetBGMPlayer()
    {
        if (BGMPlayer == null)
            BGMPlayer = Instantiate(BGMPlayerPrefab).GetComponent<AudioSource>();
        return BGMPlayer;
    }

    public void PlayEffectAudio(EffectType effectType)
    {
        PlayEffectAudio(effectType, null);
    }

    Action OnAudioEnd_Event;
    public void PlayEffectAudio(EffectType effectType, Action EndCallback)
    {
        if (bMuteSF)
            return;
        SFAudioController EffectPlayer = Instantiate(EffectPlayerPrefab).GetComponent<SFAudioController>();
        EffectPlayer.PlayAudio(AudioDatas.EffectAudios[(int)effectType], SFVoluem);
        if (EndCallback != null)
        {
            OnAudioEnd_Event = EndCallback;
            Invoke("OnAudioEnd", AudioDatas.EffectAudios[(int)effectType].length);
        }
    }

    void OnAudioEnd()
    {
        if (OnAudioEnd_Event != null)
            OnAudioEnd_Event();
        OnAudioEnd_Event = null;
    }

    Tweener BGMFadeOutTween;
    Tweener BGMFadeInTween;
    public void PlayBGM(BGMType bgmType)
    {
        if (BGMFadeOutTween != null && BGMFadeOutTween.IsPlaying())
        {
            BGMFadeOutTween.Kill();
            if (BGMFadeInTween != null)
                BGMFadeInTween.Kill();
        }
        else if (BGMFadeInTween != null && BGMFadeInTween.IsPlaying())
        {
            BGMFadeOutTween.Kill();
            BGMFadeInTween.Kill();
        }
        BGMFadeOutTween = GetBGMPlayer().DOFade(0, AudioDatas.BGMFadeDuration);
        BGMFadeOutTween.OnComplete(() =>
        {
            GetBGMPlayer().clip = AudioDatas.BGMAudios[(int)bgmType];
            GetBGMPlayer().Play();
            BGMFadeInTween = GetBGMPlayer().DOFade(AudioDatas.BGMVolume, AudioDatas.BGMFadeDuration); 
        });
    }

    public void StopBGM(bool bActive)
    {
        if (BGMFadeOutTween != null && BGMFadeOutTween.IsPlaying())
        {
            BGMFadeOutTween.Kill();
            if (BGMFadeInTween != null)
                BGMFadeInTween.Kill();
        }
        else if (BGMFadeInTween != null && BGMFadeInTween.IsPlaying())
        {
            BGMFadeOutTween.Kill();
            BGMFadeInTween.Kill();
        }

        BGMFadeOutTween = GetBGMPlayer().DOFade(0, AudioDatas.BGMFadeDuration);
    }

    public void PauseBGM()
    {
        BGMPlayer.Pause();
    }

    public void UnPauseBGM()
    {
        BGMPlayer.UnPause();
    }

    Tweener BGMVolumeTween;
    public void SetBGMVolume(float Volume)
    {
        if (BGMVolumeTween != null && BGMVolumeTween.IsPlaying())
            BGMVolumeTween.Kill();
        BGMVolumeTween = GetBGMPlayer().DOFade(Volume, 1.0f);
    }


    public void SetSFVolume(float Volume)
    {
        SFVoluem = Volume;
    }

    public void SetMuteBGM(bool bMute)
    {
        bMuteBGM = bMute;
        GetBGMPlayer().mute = bMute;
    }

    public void SetMuteSF(bool bMute)
    {
        bMuteSF = bMute;
    }

}