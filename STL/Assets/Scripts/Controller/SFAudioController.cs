using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SFAudioController : MonoBehaviour
{
    AudioSource AudioPlayer;
    private void Awake()
    {
        AudioPlayer = GetComponent<AudioSource>();
    }

    void Start()
    {
    }

    public void PlayAudio(AudioClip Clip, float Volume)
    {
        AudioPlayer.clip = Clip;
        AudioPlayer.Play();
        Invoke("DestryAudio", Clip.length);

    }

    void DestryAudio()
    {
        Destroy(gameObject);
    }

}