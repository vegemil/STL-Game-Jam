using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif
public class CharacterSpriteData : ScriptableObject
{
    public enum AnimationTyps
    {
        Default = 0,
    }

    [System.Serializable]
    public class AnimationClipData
    {
        public float loopDelay;
        public Sprite[] sprites;

    }

    [System.Serializable]
    public class AnimationData
    {
        public AnimationClipData idleData = new AnimationClipData();
        public AnimationClipData walkData = new AnimationClipData();
    }

    public List<AnimationData> animtaionDatas = new List<AnimationData>();

    static CharacterSpriteData exampleAsset;

#if UNITY_EDITOR
    [MenuItem("Datas/Create new CharacterSpriteData ")]
    static void CreateExampleAssetInstance()
    {
        exampleAsset = CreateInstance<CharacterSpriteData>();

        AssetDatabase.CreateAsset(exampleAsset, "Assets/Resources/Datas/CharacterSpriteData_Created.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
