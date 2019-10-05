using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioDatas : ScriptableObject
{


    public AudioClip[] BGMAudios;
    public AudioClip[] EffectAudios;
    public GameObject BGMPlayerPrefab;
    public GameObject EffectAudioPrefab;
    public float BGMVolume;
    public float BGMFadeDuration;


    static AudioDatas exampleAsset;
#if UNITY_EDITOR
    [MenuItem("Datas/Create new AudioData ")]
    static void CreateExampleAssetInstance()
    {
        exampleAsset = CreateInstance<AudioDatas>();

        AssetDatabase.CreateAsset(exampleAsset, "Assets/Resources/Datas/AudioData_Created.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}