using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;



public class AudioManager : Singleton<AudioManager>
{

    public enum BGMType
    {
        None = -1,
        Title = 0,
        State3_4,
        State5_7,
        LastState,
        Ending1,
        Ending2,
        Ending3,

    }
    public enum EffectType
    {
        ClickTitle,
        LeftDoor,
        RightDoor,
        FloorDoor,
        Landing,
        CameraMoveLeft,
        CameraMoveRight,
        CameraMoveDown,
        ChangedCharacter,
    }


    AudioSource BGMPlayer;
    GameObject BGMPlayerPrefab;
    GameObject EffectPlayerPrefab;
    [SerializeField] GameObject[] CreatedSoundEffects;
    [SerializeField] private AudioDatas AudioDatas;

    private BGMType bgmType = BGMType.None;
    public float BGMVolume = 1.0f;
    public float SFVoluem = 0.75f;
    bool bMuteBGM;
    bool bMuteSF;
    public bool IsPausedBGM { get { return !BGMPlayer.isPlaying; } }
    public bool IsMuteBGM { get { return bMuteBGM; } }
    public bool IsMuteSF { get { return bMuteSF; } }

    public BGMType CurrentBGMType=> bgmType;

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
    public void PlayBGM(BGMType newbgmType)
    {
        if (bgmType == newbgmType)
            return;
        bgmType = newbgmType;
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
            GetBGMPlayer().clip = AudioDatas.BGMAudios[(int)newbgmType];
            GetBGMPlayer().Play();
            BGMFadeInTween = GetBGMPlayer().DOFade(AudioDatas.BGMVolume, AudioDatas.BGMFadeDuration); 
        });
    }

    public void SetLoopBGM(bool isLoop)
    {
        GetBGMPlayer().loop = isLoop;
    }

    public void StopBGM()
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