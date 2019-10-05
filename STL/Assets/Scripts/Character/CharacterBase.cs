using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{

    [SerializeField] private SpriteAnimator spriteAnimator;
    CharacterSpriteData characterSpriteData;

    public SpriteAnimator SpriteAnimator => spriteAnimator;

    public void Initalize()
    {
        int index = (int)CharacterSpriteData.AnimationTyps.Level1;
        characterSpriteData = Resources.Load<CharacterSpriteData>("Datas/CharacterSpriteData");
        
        spriteAnimator.Initalize(characterSpriteData);

    }
     
}