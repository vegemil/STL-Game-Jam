using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CharacterSpriteData : ScriptableObject
{
    public enum AnimationTyps
    {
        Level1 = 0,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
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
